using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Efrpg;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit.DocSamples
{
    /// <summary>
    ///     Keeps the wiki's settings index honest in both directions: every setting has a page, and no page
    ///     documents a setting that does not exist.
    /// </summary>
    /// <remarks>
    ///     The second direction is the one that caught real problems. The August 2026 audit found
    ///     <c>Settings.ForeignKeyNamingStrategy</c> documented in full - type, default, an enum and two code
    ///     examples - for a member that has never existed, and <c>Settings.ApplyColumnCustomizations</c> cited as
    ///     the helper to call from <c>UpdateColumn</c>, which has also never existed. Both had been on the wiki
    ///     for long enough that nobody questioned them.
    ///     Like <see cref="WikiSnippetDriftTests"/> this skips when the wiki repository is not checked out beside
    ///     this one.
    /// </remarks>
    [TestFixture, NonParallelizable]
    [Category(Constants.CI)]
    public class SettingsPageCoverageTests
    {
        [Test]
        public void Every_setting_is_listed_in_the_wiki_index()
        {
            var wiki = WikiFolder();
            if (wiki == null)
                Assert.Ignore("The wiki repository is not checked out beside this one.");

            var index = File.ReadAllText(Path.Combine(wiki, "Settings-Reference.md"));
            var listed = new HashSet<string>(
                Regex.Matches(index, @"`Settings\.(?<name>[A-Za-z0-9_]+)`")
                     .Cast<Match>()
                     .Select(m => m.Groups["name"].Value),
                StringComparer.Ordinal);

            var missing = SettingNames().Where(n => !listed.Contains(n)).ToList();

            Assert.IsEmpty(missing,
                "These settings exist but no row in Settings-Reference.md links to a page for them: " +
                string.Join(", ", missing));
        }

        [Test]
        public void No_wiki_page_documents_a_setting_that_does_not_exist()
        {
            var wiki = WikiFolder();
            if (wiki == null)
                Assert.Ignore("The wiki repository is not checked out beside this one.");

            var real = new HashSet<string>(SettingNames(), StringComparer.Ordinal);
            foreach (var extra in KnownNonSettings)
                real.Add(extra);

            var invented = new List<string>();

            foreach (var page in Directory.GetFiles(wiki, "*.md"))
            {
                var text = File.ReadAllText(page);

                // Only headings, which is where a page claims a setting exists. Prose mentions a lot of
                // things in passing - removed settings, FilterSettings members - and flagging those would
                // make this test useless noise.
                foreach (Match m in Regex.Matches(text, @"^#{1,4} .*?Settings\.(?<name>[A-Za-z0-9_]+)", RegexOptions.Multiline))
                {
                    var name = m.Groups["name"].Value;
                    if (!real.Contains(name))
                        invented.Add(string.Format("{0}: Settings.{1}", Path.GetFileName(page), name));
                }
            }

            Assert.IsEmpty(invented,
                "These wiki headings name a member of Settings that does not exist: " + string.Join(", ", invented));
        }

        /// <summary>
        ///     Names a heading may legitimately use that are not fields on Settings: helper methods, the
        ///     settings removed in v4 which the wiki still documents as removed, and page titles that group
        ///     several settings under a wildcard.
        /// </summary>
        private static readonly string[] KnownNonSettings =
        {
            "DatabaseReaderPlugin", "FileManagerType",  // Removed in v4; documented as such
            "MultiContextAllFields",                    // The shared page's wildcard title
            "OnConfiguration",                          // Also an enum type name
            "Runtime",                                  // Settings.Runtime-Values page title
        };

        private static IEnumerable<string> SettingNames()
        {
            return typeof(Settings)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => !f.IsLiteral)
                .Select(f => f.Name)
                .Concat(typeof(Settings)
                    .GetProperties(BindingFlags.Public | BindingFlags.Static)
                    .Select(p => p.Name))
                .Distinct(StringComparer.Ordinal)
                .OrderBy(n => n, StringComparer.Ordinal);
        }

        private static string WikiFolder()
        {
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            while (dir != null && !File.Exists(Path.Combine(dir.FullName, "EF.Reverse.POCO.GeneratorV4.sln")))
                dir = dir.Parent;

            if (dir == null || dir.Parent == null)
                return null;

            var wiki = Path.Combine(dir.Parent.FullName, dir.Name + ".wiki");
            return Directory.Exists(wiki) ? wiki : null;
        }
    }
}
