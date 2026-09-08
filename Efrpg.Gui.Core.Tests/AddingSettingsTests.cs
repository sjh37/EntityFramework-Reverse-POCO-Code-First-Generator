using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    /// <summary>
    ///     A setting the template does not have can still be set from the editor: the line is added beside the
    ///     settings it belongs with, shaped like the lines around it. A commented-out one is switched on.
    /// </summary>
    [TestFixture]
    public class AddingSettingsTests
    {
        private static SettingsCatalogue V4 => SettingsCatalogue.Load(RepositoryFiles.SettingsMetadata("v4"));

        private static string[] Lines(string text)
        {
            return text.Split('\n');
        }

        /// <summary>The shipped template with one setting's line taken out, and what that line's value was.</summary>
        private static string Without(string name, out string valueText)
        {
            var original = RepositoryFiles.DatabaseTemplate();
            var line     = new Regex(@"^[ \t]*Settings\." + name + @"[ \t]*=[ \t]*(?<value>[^\r\n]*?);[^\r\n]*\r?\n", RegexOptions.Multiline);
            var match    = line.Match(original);

            Assert.That(match.Success, Is.True, "Database.tt should set " + name + " on one line");

            valueText = match.Groups["value"].Value;
            return original.Remove(match.Index, match.Length);
        }

        /// <summary>Sets an item through whichever typed setter its kind has, from a right-hand side.</summary>
        private static void SetFromText(SettingEditorItem item, string valueText)
        {
            switch (item.Kind)
            {
                case SettingKind.Boolean:
                {
                    bool value;
                    Assert.That(SettingValue.TryReadBoolean(valueText, out value), Is.True);
                    item.SetBoolean(value);
                    return;
                }
                case SettingKind.Text:
                {
                    string value;
                    bool verbatim;
                    Assert.That(SettingValue.TryReadText(valueText, out value, out verbatim), Is.True);
                    item.SetText(value);
                    return;
                }
                case SettingKind.Number:
                {
                    int value;
                    Assert.That(SettingValue.TryReadNumber(valueText, out value), Is.True);
                    item.SetNumber(value);
                    return;
                }
                case SettingKind.Enumeration:
                {
                    IReadOnlyList<string> members;
                    Assert.That(SettingValue.TryReadEnum(valueText, item.Definition, out members), Is.True);
                    item.SetMembers(members);
                    return;
                }
                default:
                    Assert.Fail("No setter for " + item.Kind);
                    return;
            }
        }

        /// <summary>
        ///     The strongest statement of "beside its neighbours, shaped like them": delete a line from the shipped
        ///     template, add the setting back through the editor with the same value, and get the original file
        ///     byte for byte - alignment, help comment and line ending included.
        /// </summary>
        [TestCase("UseFileScopedNamespaces")]
        [TestCase("GenerateSeparateFiles")]
        [TestCase("DbContextName")]
        [TestCase("ConnectionStringName")]
        [TestCase("CommandTimeout")]
        [TestCase("UseDataAnnotations")]
        public void RemovingALineAndAddingItBackThroughTheEditorRestoresTheFileByteForByte(string name)
        {
            string valueText;
            var reduced = Without(name, out valueText);
            var session = SettingsEditSession.Load(reduced, V4);
            var item    = session.Find(name);

            Assert.That(item.IsAbsent, Is.True);
            Assert.That(item.IsEditable, Is.True, item.ReadOnlyReason);
            Assert.That(item.Hint, Does.Contain("adds the line"));

            SetFromText(item, valueText);

            Assert.That(item.IsChanged, Is.True);
            Assert.That(session.Apply(), Is.EqualTo(RepositoryFiles.DatabaseTemplate()));
        }

        /// <summary>
        ///     ElementsToGenerate is alone in its section, so there is no neighbour to sit beside; it goes directly
        ///     under the section heading. Database.tt has a comment line between the two, so this is one line
        ///     away from byte for byte, which is as close as a heading can get it.
        /// </summary>
        [Test]
        public void ASettingAloneInItsSectionLandsUnderItsHeading()
        {
            string valueText;
            var reduced = Without("ElementsToGenerate", out valueText);
            var session = SettingsEditSession.Load(reduced, V4);

            SetFromText(session.Find("ElementsToGenerate"), valueText);
            var lines = Lines(session.Apply());

            var heading = Array.FindIndex(lines, l => l.TrimStart().StartsWith("// Elements to generate", StringComparison.Ordinal));
            var original = Lines(RepositoryFiles.DatabaseTemplate()).Single(l => l.TrimStart().StartsWith("Settings.ElementsToGenerate", StringComparison.Ordinal));

            Assert.That(heading, Is.GreaterThan(0));
            Assert.That(lines[heading + 1], Is.EqualTo(original));
        }

        [Test]
        public void TwoNeighbouringSettingsCanBeAddedBackInOneSave()
        {
            string first, second;
            var reduced = Without("ConnectionStringName", out first);
            reduced     = Remove(reduced, "DbContextName", out second);
            var session = SettingsEditSession.Load(reduced, V4);

            SetFromText(session.Find("ConnectionStringName"), first);
            SetFromText(session.Find("DbContextName"), second);

            Assert.That(session.Apply(), Is.EqualTo(RepositoryFiles.DatabaseTemplate()));
        }

        private static string Remove(string text, string name, out string valueText)
        {
            var line  = new Regex(@"^[ \t]*Settings\." + name + @"[ \t]*=[ \t]*(?<value>[^\r\n]*?);[^\r\n]*\r?\n", RegexOptions.Multiline);
            var match = line.Match(text);

            Assert.That(match.Success, Is.True);
            valueText = match.Groups["value"].Value;
            return text.Remove(match.Index, match.Length);
        }

        /// <summary>
        ///     A setting Database.tt never had goes under its own section heading, not at the end of the file. Its
        ///     section's other settings all sit inside <c>if (Settings.GenerateSeparateFiles)</c>, and a line added
        ///     beside them would inherit that condition - so the heading is the anchor and the line lands outside.
        /// </summary>
        [Test]
        public void ASettingWhoseNeighboursAreNestedLandsUnderItsSectionHeadingInstead()
        {
            var original = RepositoryFiles.DatabaseTemplate();
            var session  = SettingsEditSession.Load(original, V4);
            var item     = session.Find("OwnedEntityFolder");

            Assert.That(item.IsAbsent, Is.True);
            Assert.That(item.IsEditable, Is.True, item.ReadOnlyReason);

            item.SetText("Owned");
            var after = session.Apply();

            var before = Lines(original);
            var lines  = Lines(after);
            Assert.That(lines.Length, Is.EqualTo(before.Length + 1));

            var added = lines.Select((l, i) => new { l, i }).Single(x => x.l.Contains("Settings.OwnedEntityFolder"));
            Assert.That(added.l.TrimEnd(), Does.StartWith("    Settings.OwnedEntityFolder"));
            Assert.That(added.l, Does.Contain("= \"Owned\"; // "));

            // Directly under the "// Generate files in sub-folders ****" heading, and before the if.
            var heading = Array.FindIndex(before, l => l.TrimStart().StartsWith("// " + item.Section, StringComparison.Ordinal));
            Assert.That(heading, Is.GreaterThan(0));
            Assert.That(added.i, Is.EqualTo(heading + 1));
            Assert.That(lines[added.i + 1].TrimStart(), Does.StartWith("if (Settings.GenerateSeparateFiles)"));
        }

        [Test]
        public void ACommentedOutSettingIsSwitchedOnWhenChanged()
        {
            var original = RepositoryFiles.DatabaseTemplate();
            var session  = SettingsEditSession.Load(original, V4);
            var item     = session.Find("DbContextInterfaceName");

            Assert.That(item.IsCommentedOut, Is.True);
            Assert.That(item.IsEditable, Is.True, item.ReadOnlyReason);
            Assert.That(item.Hint, Does.Contain("switches the line on"));

            item.SetText("INorthwindContext");
            var after = session.Apply();

            var changed = Lines(original).Zip(Lines(after), (a, b) => new { a, b }).Where(x => x.a != x.b).ToList();
            Assert.That(Lines(after).Length, Is.EqualTo(Lines(original).Length));
            Assert.That(changed.Count, Is.EqualTo(1));
            Assert.That(changed[0].a, Does.StartWith("    //Settings.DbContextInterfaceName     = \"IMyDbContext\"; // Defaults to"));
            Assert.That(changed[0].b, Does.StartWith("    Settings.DbContextInterfaceName     = \"INorthwindContext\"; // Defaults to"));
        }

        [Test]
        public void SettingACommentedOutValueToWhatItAlreadySaysStillSwitchesItOn()
        {
            var session = SettingsEditSession.Load(RepositoryFiles.DatabaseTemplate(), V4);
            var item    = session.Find("DbContextInterfaceName");

            item.SetText("IMyDbContext");

            Assert.That(item.IsChanged, Is.True);
            Assert.That(session.Apply(), Does.Contain("    Settings.DbContextInterfaceName     = \"IMyDbContext\";"));
        }

        [Test]
        public void UndoOnAnAddedSettingLeavesTheFileAlone()
        {
            var original = RepositoryFiles.DatabaseTemplate();
            var session  = SettingsEditSession.Load(original, V4);
            var item     = session.Find("UsePascalCaseForEnumMembers");

            item.SetBoolean(false);
            item.Revert();

            Assert.That(item.IsChanged, Is.False);
            Assert.That(session.Apply(), Is.EqualTo(original));
        }

        /// <summary>Absent is not a licence: what the generator fills in itself, and code, stay read-only.</summary>
        [TestCase("DefaultSchema", "Set by the generator")]
        [TestCase("PrependSchemaNameForTable", "callback")]
        public void WhatCannotBeWrittenAsAValueStaysReadOnlyEvenWhenAbsent(string name, string reason)
        {
            var item = SettingsEditSession.Load(RepositoryFiles.DatabaseTemplate(), V4).Find(name);

            Assert.That(item.IsAbsent, Is.True);
            Assert.That(item.IsEditable, Is.False);
            Assert.That(item.ReadOnlyReason, Does.Contain(reason));
        }

        [Test]
        public void AddingToATemplateWithNoSettingsBlockIsRefusedRatherThanAppended()
        {
            var session = SettingsEditSession.Load("<#@ include file=\"EF.Reverse.POCO.v4.ttinclude\" #>\r\n<#\r\n#>\r\n", V4);

            session.Find("UseFileScopedNamespaces").SetBoolean(true);

            Assert.That(() => session.Apply(), Throws.InvalidOperationException.With.Message.Contains("no Settings block"));
        }

        [Test]
        public void AddingKeepsAnLfFileLf()
        {
            var lf      = RepositoryFiles.DatabaseTemplate().Replace("\r\n", "\n");
            var session = SettingsEditSession.Load(lf, V4);

            session.Find("UsePascalCaseForEnumMembers").SetBoolean(true);

            Assert.That(session.Apply(), Does.Not.Contain("\r"));
        }
    }
}
