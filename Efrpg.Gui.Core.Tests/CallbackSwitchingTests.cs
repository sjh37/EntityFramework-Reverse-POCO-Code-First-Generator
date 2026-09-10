using System;
using System.Collections.Generic;
using System.Linq;
using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    /// <summary>
    ///     The Callbacks page: a code setting is switched off by commenting its statement out and on by uncommenting
    ///     it, or by writing its default body when the template has nothing to uncomment. Off then on must give the
    ///     file back byte for byte, because the user's code is the thing being protected.
    /// </summary>
    [TestFixture]
    public class CallbackSwitchingTests
    {
        private static SettingsCatalogue V4 => SettingsCatalogue.Load(RepositoryFiles.SettingsMetadata("v4"));

        private static SettingsEditSession Shipped()
        {
            return SettingsEditSession.Load(RepositoryFiles.DatabaseTemplate(), V4);
        }

        public static IEnumerable<string> AssignedCodeSettings()
        {
            return Shipped().Items.Where(i => i.IsCode && i.Assignment != null).Select(i => i.Name).OrderBy(n => n).ToList();
        }

        public static IEnumerable<string> AbsentCodeSettings()
        {
            return Shipped().Items.Where(i => i.IsCode && i.Assignment == null).Select(i => i.Name).OrderBy(n => n).ToList();
        }

        [Test]
        public void TheShippedTemplateHasCodeSettingsOfBothKinds()
        {
            Assert.That(AssignedCodeSettings().Count(), Is.GreaterThan(15));
            Assert.That(AbsentCodeSettings().Count(), Is.GreaterThan(3));
        }

        [TestCaseSource(nameof(AssignedCodeSettings))]
        public void SwitchingOff_CommentsOutEveryLineOfTheStatement(string name)
        {
            var session  = Shipped();
            var original = session.Document.Text;
            var item     = session.Find(name);
            var first    = item.Assignment.LineNumber;
            var last     = item.Assignment.EndLineNumber;

            item.SetAssigned(false);
            var text = session.Apply();

            var lines = text.Replace("\r\n", "\n").Split('\n');
            Assert.That(lines.Length, Is.EqualTo(original.Replace("\r\n", "\n").Split('\n').Length), "no line is added or removed");
            for (var i = first - 1; i < last; i++)
                Assert.That(lines[i].TrimStart(), Does.StartWith("//"), "line " + (i + 1));

            var reloaded = SettingsEditSession.Load(text, V4).Find(name);
            Assert.That(reloaded.IsAssigned, Is.False);
            Assert.That(reloaded.Assignment, Is.Not.Null, "the statement is still found, commented out");
            Assert.That(reloaded.Assignment.EndLineNumber, Is.EqualTo(last), "the whole statement is still one assignment");
        }

        [TestCaseSource(nameof(AssignedCodeSettings))]
        public void SwitchingOffThenOn_GivesTheFileBackByteForByte(string name)
        {
            var original = RepositoryFiles.DatabaseTemplate();
            var session  = SettingsEditSession.Load(original, V4);
            session.Find(name).SetAssigned(false);
            var off = SettingsEditSession.Load(session.Apply(), V4);

            off.Find(name).SetAssigned(true);

            Assert.That(off.Apply(), Is.EqualTo(original));
        }

        [TestCaseSource(nameof(AbsentCodeSettings))]
        public void SwitchingOnAnAbsentSetting_WritesItsDefaultBodyAsALiveStatement(string name)
        {
            var session = Shipped();
            var item    = session.Find(name);
            Assert.That(item.Code, Does.StartWith("Settings." + name + " = "), "the page can show what will be written");

            item.SetAssigned(true);
            var text = session.Apply();

            var reloaded = SettingsEditSession.Load(text, V4).Find(name);
            Assert.That(reloaded.IsAssigned, Is.True);
            Assert.That(reloaded.Assignment.IsCommentedOut, Is.False);

            var statement = reloaded.Code.Replace("\r\n", "\n").Split('\n');
            Assert.That(statement[0], Does.StartWith("    Settings." + name + " = "), "indented like its neighbours");
            Assert.That(statement[statement.Length - 1].TrimEnd(), Does.EndWith(";"));
            Assert.That(statement.Where(l => l.Trim().Length > 0), Has.All.StartWith("    "), "every line carries the block's indentation");
        }

        [TestCaseSource(nameof(AbsentCodeSettings))]
        public void AnAddedSettingSwitchesOffAndOnAgainLikeAnyOther(string name)
        {
            var session = Shipped();
            session.Find(name).SetAssigned(true);
            var added = session.Apply();

            var off = SettingsEditSession.Load(added, V4);
            off.Find(name).SetAssigned(false);
            var on = SettingsEditSession.Load(off.Apply(), V4);
            Assert.That(on.Find(name).IsAssigned, Is.False);
            on.Find(name).SetAssigned(true);

            Assert.That(on.Apply(), Is.EqualTo(added));
        }

        [Test]
        public void ABodyFromSettingsCsArrivesWithItsMembersQualified()
        {
            var session = Shipped();
            session.Find("ColumnIdentity").SetAssigned(true);

            var text = session.Apply();

            Assert.That(text, Does.Contain("Settings.IsEfCore8Plus()"));
            Assert.That(text, Does.Contain("Settings.HiLoSequences?"));
            Assert.That(text, Does.Contain("Settings.DatabaseType == DatabaseType.MySql"));
        }

        [Test]
        public void ACommentedOutMultiLineStatementIsParsedAsOneAssignment()
        {
            var document   = TemplateSettingsDocument.Parse(RepositoryFiles.DatabaseTemplate());
            var assignment = document.Find("UpdateColumn");

            var off = document.WithCommentedOut(assignment).Find("UpdateColumn");

            Assert.That(off, Is.Not.Null);
            Assert.That(off.IsCommentedOut, Is.True);
            Assert.That(off.SpansMultipleLines, Is.True);
            Assert.That(off.EndLineNumber, Is.EqualTo(assignment.EndLineNumber));
        }

        [Test]
        public void SettingTheStateTheFileAlreadyHasIsNotAChange()
        {
            var session = Shipped();
            var item    = session.Find("UpdateColumn");

            item.SetAssigned(true);
            Assert.That(item.IsChanged, Is.False);

            item.SetAssigned(false);
            item.SetAssigned(true);
            Assert.That(item.IsChanged, Is.False);
            Assert.That(session.HasChanges, Is.False);
        }

        [Test]
        public void SwitchingOnAnAbsentSettingWithNoDefaultBodyIsRefused()
        {
            var catalogue = SettingsCatalogue.Load("{\"templateVersion\":\"v4\",\"settings\":[{\"name\":\"Mystery\",\"kind\":\"callback\",\"section\":\"Call-backs\"}]}");
            var item      = SettingsEditSession.Load(RepositoryFiles.DatabaseTemplate(), catalogue).Find("Mystery");

            Assert.That(() => item.SetAssigned(true), Throws.InvalidOperationException);
        }

        [Test]
        public void AValueEditAndASwitchInOneSessionBothLand()
        {
            var session = Shipped();
            session.Find("GenerateSeparateFiles").SetBoolean(true);
            session.Find("ViewProcessing").SetAssigned(false);

            var reloaded = SettingsEditSession.Load(session.Apply(), V4);

            Assert.That(reloaded.Find("GenerateSeparateFiles").BooleanValue, Is.True);
            Assert.That(reloaded.Find("ViewProcessing").IsAssigned, Is.False);
        }

        [Test]
        public void LineNumberOf_FindsTheStatementInTheSavedText()
        {
            var session = Shipped();
            session.Find("ColumnIdentity").SetAssigned(true);
            var text = session.Apply();

            var line = SettingsEditSession.LineNumberOf(text, "ColumnIdentity");

            Assert.That(line, Is.GreaterThan(0));
            Assert.That(text.Replace("\r\n", "\n").Split('\n')[line - 1], Does.Contain("Settings.ColumnIdentity ="));
            Assert.That(SettingsEditSession.LineNumberOf(text, "NoSuchSetting"), Is.EqualTo(0));
        }

        [Test]
        public void EveryCodeSettingHasAWikiPage()
        {
            var pages = Shipped().Items.Where(i => i.IsCode).Select(i => i.Definition.WikiPage).Distinct().ToList();

            Assert.That(pages, Has.All.StartWith("Settings"));
            Assert.That(pages, Has.None.EqualTo("Settings-Reference"), "every code setting should link to its own page");
        }
    }
}
