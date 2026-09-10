using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using Efrpg;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    /// <summary>
    ///     settings-metadata.v4.json tells the Visual Studio GUI which settings exist, what type each one is and what
    ///     the tooltip should say. BuildTT regenerates it from Efrpg.Settings and Database.tt, so the checked-in copy
    ///     goes stale exactly the way the .ttinclude does - silently, and only for users, because nothing in the
    ///     generator reads it.
    /// </summary>
    /// <remarks>
    ///     The Database.tt scan below is deliberately a simple regex rather than a call into BuildTT's parser. A test
    ///     that reused the parser would agree with it about a setting it had dropped, which is the one failure worth
    ///     catching. It is allowed to be cruder than the real thing: it only has to name the settings, not understand
    ///     multi-line delegate bodies, and a false positive would show up as a loud failure rather than a quiet gap.
    /// </remarks>
    [TestFixture]
    [Category(Constants.CI)]
    public class SettingsMetadataTests
    {
        private const string TemplateFolder = "EntityFramework.Reverse.POCO.Generator";
        private const string V4Metadata     = "settings-metadata.v4.json";
        private const string V3Metadata     = "settings-metadata.v3.json";

        private static readonly Regex Assignment = new Regex(@"^\s*(//\s*)?Settings\.(?<name>\w+)\s*=(?!=)");

        [Test]
        public void DatabaseTt_EverySettingItAssignsIsInTheMetadata()
        {
            var assigned = SettingsAssignedInDatabaseTt();
            var described = MetadataSettingNames(V4Metadata);

            var missing = assigned.Where(name => !described.Contains(name)).ToList();

            Assert.That(missing, Is.Empty,
                "Database.tt assigns these settings but settings-metadata.v4.json does not describe them, so the GUI " +
                "would show a .tt file it cannot edit. Run BuildTT: " + string.Join(", ", missing));
        }

        [Test]
        public void Settings_EveryPublicMemberIsInTheMetadata()
        {
            var described = MetadataSettingNames(V4Metadata);

            var missing = SettingsMembers().Where(name => !described.Contains(name)).ToList();

            Assert.That(missing, Is.Empty,
                "Efrpg.Settings has these members and settings-metadata.v4.json does not, so the checked-in metadata " +
                "predates them. Run BuildTT: " + string.Join(", ", missing));
        }

        [Test]
        public void Metadata_DescribesNothingThatSettingsNoLongerHas()
        {
            var members = new HashSet<string>(SettingsMembers(), StringComparer.Ordinal);

            var stale = MetadataSettingNames(V4Metadata).Where(name => !members.Contains(name)).ToList();

            Assert.That(stale, Is.Empty,
                "settings-metadata.v4.json describes these, but Efrpg.Settings no longer has them, so the GUI would " +
                "offer settings that do not compile. Run BuildTT: " + string.Join(", ", stale));
        }

        /// <summary>
        ///     The v3 file is hand-maintained and frozen, and is the only thing keeping v3-only settings out of a v4
        ///     template. Overwriting it with a copy of the v4 file would be silent, so check the difference that
        ///     matters rather than the whole file.
        /// </summary>
        [Test]
        public void V3Metadata_StillDescribesTheSettingsV4Dropped()
        {
            using (var json = JsonDocument.Parse(File.ReadAllText(MetadataPath(V3Metadata))))
            {
                var names = MetadataSettingNames(V3Metadata);

                Assert.That(json.RootElement.GetProperty("templateVersion").GetString(), Is.EqualTo("v3"));
                Assert.That(names, Does.Contain("FileManagerType"));
                Assert.That(names, Does.Contain("DatabaseReaderPlugin"));
            }
        }

        [Test]
        public void Metadata_EveryEnumSettingListsItsMembers()
        {
            using (var json = JsonDocument.Parse(File.ReadAllText(MetadataPath(V4Metadata))))
            {
                var empty = json.RootElement
                    .GetProperty("settings")
                    .EnumerateArray()
                    .Where(x => x.GetProperty("kind").GetString() == "enum")
                    .Where(x => x.GetProperty("enumMembers").GetArrayLength() == 0)
                    .Select(x => x.GetProperty("name").GetString())
                    .ToList();

                Assert.That(empty, Is.Empty,
                    "These are enums with no members listed, so the GUI has nothing to put in the dropdown: " +
                    string.Join(", ", empty));
            }
        }

        /// <summary>
        ///     The trailing // comment on an enum setting is the only place Database.tt lists the values a user may
        ///     pick from, and it is what the GUI will show. It rotted silently once already: the old
        ///     ForeignKeyNamingStrategy setting read "Please use Legacy for now, Latest (not yet ready)" long after
        ///     its members had been renamed to Current and Beta, so the comment named two values that did not exist
        ///     and neither of the two that did. That setting has since been deleted; this test is what remains of it.
        /// </summary>
        [Test]
        public void Metadata_EveryEnumSettingNamesAllItsMembersInTheHelpText()
        {
            using (var json = JsonDocument.Parse(File.ReadAllText(MetadataPath(V4Metadata))))
            {
                var gaps = new List<string>();

                foreach (var setting in json.RootElement.GetProperty("settings").EnumerateArray())
                {
                    if (setting.GetProperty("kind").GetString() != "enum")
                        continue;

                    var help    = setting.GetProperty("help").GetString();
                    var named   = NamesIn(help);
                    var missing = setting
                        .GetProperty("enumMembers")
                        .EnumerateArray()
                        .Select(x => x.GetProperty("name").GetString())
                        .Where(x => !named.Contains(x))
                        .ToList();

                    if (missing.Any())
                        gaps.Add(setting.GetProperty("name").GetString() + " omits " + string.Join(", ", missing));
                }

                Assert.That(gaps, Is.Empty,
                    "These enum settings have members their help text never mentions, so the GUI would offer a value " +
                    "Database.tt does not document, or document one that does not exist. Fix the trailing comment in " +
                    "Generator/Settings.cs and in the footer of BuildTT/BuildTT.cs, then run BuildTT:" +
                    Environment.NewLine + string.Join(Environment.NewLine, gaps));
            }
        }

        /// <summary>
        ///     Words in a help comment, with "EfCore8-10" expanded to EfCore8, EfCore9 and EfCore10 - the shorthand
        ///     Database.tt uses so the reader is not given eight near-identical names to scan.
        /// </summary>
        private static HashSet<string> NamesIn(string help)
        {
            var names = new HashSet<string>(StringComparer.Ordinal);
            if (string.IsNullOrEmpty(help))
                return names;

            foreach (Match word in Regex.Matches(help, @"[A-Za-z][A-Za-z0-9]*"))
                names.Add(word.Value);

            foreach (Match range in Regex.Matches(help, @"(?<stem>[A-Za-z]+)(?<from>\d+)-(?<to>\d+)"))
            {
                var stem = range.Groups["stem"].Value;
                for (var n = int.Parse(range.Groups["from"].Value); n <= int.Parse(range.Groups["to"].Value); n++)
                    names.Add(stem + n);
            }

            return names;
        }

        private static IEnumerable<string> SettingsAssignedInDatabaseTt()
        {
            var lines = File.ReadAllLines(Path.Combine(RepositoryRoot(), TemplateFolder, "Database.tt"));

            return lines
                .Select(line => Assignment.Match(line))
                .Where(match => match.Success)
                .Select(match => match.Groups["name"].Value)
                .Distinct(StringComparer.Ordinal)
                .ToList();
        }

        private static HashSet<string> MetadataSettingNames(string filename)
        {
            using (var json = JsonDocument.Parse(File.ReadAllText(MetadataPath(filename))))
            {
                return new HashSet<string>(
                    json.RootElement
                        .GetProperty("settings")
                        .EnumerateArray()
                        .Select(x => x.GetProperty("name").GetString()),
                    StringComparer.Ordinal);
            }
        }

        /// <summary>
        ///     Must stay in step with SettingsMetadataWriter.SettingsMembers, which decides what BuildTT emits.
        /// </summary>
        private static IEnumerable<string> SettingsMembers()
        {
            var settings = typeof(Settings);

            var fields = settings
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => !x.IsLiteral)
                .Select(x => x.Name);

            var properties = settings
                .GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.CanWrite)
                .Select(x => x.Name);

            return fields.Concat(properties).ToList();
        }

        private static string MetadataPath(string filename)
        {
            return Path.Combine(RepositoryRoot(), TemplateFolder, filename);
        }

        private static string RepositoryRoot()
        {
            var folder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            while (folder != null && !File.Exists(Path.Combine(folder.FullName, "EF.Reverse.POCO.GeneratorV4.sln")))
                folder = folder.Parent;

            Assert.That(folder, Is.Not.Null,
                "Could not find EF.Reverse.POCO.GeneratorV4.sln above " + AppDomain.CurrentDomain.BaseDirectory +
                ". These tests read the checked-in metadata from the working tree, not from the output folder.");

            return folder.FullName;
        }
    }
}
