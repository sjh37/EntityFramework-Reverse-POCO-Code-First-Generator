using System;
using System.IO;
using System.Linq;
using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    /// <summary>
    ///     The round trip. This is the feature that can silently destroy a paying customer's customisation, so it
    ///     gets the paranoia the wire contract tests get.
    /// </summary>
    [TestFixture]
    public class SettingsEditSessionTests
    {
        private static SettingsCatalogue V4 => SettingsCatalogue.Load(RepositoryFiles.SettingsMetadata("v4"));

        private static SettingsEditSession Shipped()
        {
            return SettingsEditSession.Load(RepositoryFiles.DatabaseTemplate(), V4);
        }

        /// <summary>Real templates from this repository, none of them written for this test.</summary>
        private static string[] Fixtures => RepositoryFiles.TemplateFixtures().ToArray();

        // ---------------------------------------------------------------- the two properties that matter

        /// <summary>
        ///     Load and save with no change. Anything but byte-for-byte identical means the editor rewrites files
        ///     it was only asked to read, and every customer's next commit is a wall of noise.
        /// </summary>
        [TestCaseSource(nameof(Fixtures))]
        public void LoadingAndSavingWithNoChangeLeavesTheFileByteForByteIdentical(string path)
        {
            var original = File.ReadAllText(path);

            var text = SettingsEditSession.Load(original, V4).Apply();

            Assert.That(text, Is.EqualTo(original));
        }

        /// <summary>
        ///     Change one setting, and exactly one line differs. Not "about one line" - one.
        /// </summary>
        [TestCaseSource(nameof(Fixtures))]
        public void ChangingOneSettingChangesExactlyOneLine(string path)
        {
            var original = File.ReadAllText(path);
            var session  = SettingsEditSession.Load(original, V4);

            var item = session.Items.FirstOrDefault(i => i.IsEditable && i.Kind == SettingKind.Boolean);
            if (item == null)
                Assert.Ignore("This template sets no editable boolean.");

            item.SetBoolean(!item.BooleanValue);

            var before = original.Split('\n');
            var after  = session.Apply().Split('\n');

            Assert.That(after.Length, Is.EqualTo(before.Length), "the line count must not change");
            Assert.That(before.Where((l, i) => l != after[i]).Count(), Is.EqualTo(1),
                "exactly one line may differ, and it must be Settings." + item.Name);
        }

        [TestCaseSource(nameof(Fixtures))]
        public void TheLineEndingsAreNeverTouched(string path)
        {
            var original = File.ReadAllText(path);
            var session  = SettingsEditSession.Load(original, V4);

            var item = session.Items.FirstOrDefault(i => i.IsEditable && i.Kind == SettingKind.Boolean);
            if (item == null)
                Assert.Ignore("This template sets no editable boolean.");

            item.SetBoolean(!item.BooleanValue);
            var text = session.Apply();

            Assert.That(CountCrLf(text), Is.EqualTo(CountCrLf(original)));
            Assert.That(CountBareLf(text), Is.EqualTo(CountBareLf(original)));
        }

        // ---------------------------------------------------------------- what must be left alone

        /// <summary>
        ///     A custom ForeignKeyName lambda is somebody's naming convention for their whole schema. Losing it is
        ///     the worst thing this feature could do.
        /// </summary>
        [Test]
        public void ACustomForeignKeyNameLambdaSurvivesUntouched()
        {
            var original = RepositoryFiles.DatabaseTemplate();
            Assume.That(original, Does.Contain("Settings.ForeignKeyName"));

            var session = SettingsEditSession.Load(original, V4);
            var item    = session.Find("ForeignKeyName");

            Assert.That(item.IsEditable, Is.False);
            Assert.That(item.ReadOnlyReason, Is.Not.Null);

            session.Find("UsePascalCase").SetBoolean(false);

            Assert.That(session.Apply(), Does.Contain("Settings.ForeignKeyName"));
        }

        /// <summary>
        ///     FilterSettings lines are not Settings.* assignments at all, so the parser must not see them and the
        ///     writer must never move them.
        /// </summary>
        [Test]
        public void RegexFilterSettingsSurviveUntouched()
        {
            var original = RepositoryFiles.DatabaseTemplate();
            var filters  = original.Split('\n').Where(l => l.Contains("FilterSettings.")).ToList();
            Assume.That(filters, Is.Not.Empty);

            var session = SettingsEditSession.Load(original, V4);
            session.Find("UsePascalCase").SetBoolean(false);

            var after = session.Apply().Split('\n').Where(l => l.Contains("FilterSettings.")).ToList();

            Assert.That(after, Is.EqualTo(filters));

            // FilterSettings.TableFilters.Add(...) is not a Settings.* assignment, so the parser must not
            // have picked one up as a value it could rewrite. (FilterCount is a real setting, so a name
            // test would be wrong here - this checks the lines instead.)
            var lines = original.Replace("\r\n", "\n").Split('\n');
            Assert.That(session.Document.Assignments.Select(a => lines[a.LineNumber - 1].TrimStart()),
                Has.None.StartWith("FilterSettings"));
        }

        /// <summary>
        ///     Settings.Namespace is declared a string but ships as the bare identifier DefaultNamespace. Offering
        ///     a textbox for it would replace working code with a quoted approximation of it.
        /// </summary>
        [Test]
        public void AStringSettingHoldingAnExpressionIsReadOnly()
        {
            var item = Shipped().Find("Namespace");

            Assert.That(item.Assignment, Is.Not.Null);
            Assert.That(item.IsEditable, Is.False);
            Assert.That(item.ReadOnlyReason, Does.Contain("expression"));
        }

        [Test]
        public void ASettingBuiltWithAMethodCallIsReadOnly()
        {
            var item = Shipped().Find("TemplateFolder");

            Assert.That(item.CurrentValueText, Does.Contain("Path.Combine"));
            Assert.That(item.IsEditable, Is.False);
        }

        [Test]
        public void ACommentedOutSettingIsShownAndSaysWhyItIsNotEditable()
        {
            var item = Shipped().Find("DbContextInterfaceName");

            Assert.That(item.Assignment, Is.Not.Null);
            Assert.That(item.Assignment.IsCommentedOut, Is.True);
            Assert.That(item.ReadOnlyReason, Does.Contain("Commented out"));
        }

        [TestCase("ForeignKeyName")]
        [TestCase("UpdateColumn")]
        [TestCase("AddEnumDefinitions")]
        public void CallbacksAreNeverEditable(string name)
        {
            Assert.That(Shipped().Find(name).IsEditable, Is.False);
        }

        [Test]
        public void EditingAMultiLineAssignmentIsRefusedRatherThanTruncated()
        {
            var session    = Shipped();
            var multiLine  = session.Items.First(i => i.Assignment != null && i.Assignment.SpansMultipleLines);

            Assert.That(multiLine.IsEditable, Is.False);
            Assert.That(() => multiLine.SetText("nope"), Throws.TypeOf<InvalidOperationException>());
        }

        // ---------------------------------------------------------------- what must be editable

        [Test]
        public void TheShippedTemplateOffersPlentyToEdit()
        {
            var session = Shipped();

            Assert.That(session.Items.Count, Is.EqualTo(session.Catalogue.Settings.Count));
            Assert.That(session.Items.Count(i => i.IsEditable), Is.GreaterThan(40));
        }

        [Test]
        public void ABooleanRoundTrips()
        {
            var session = Shipped();
            var item    = session.Find("GenerateSeparateFiles");

            Assert.That(item.IsEditable, Is.True);
            Assert.That(item.BooleanValue, Is.False);

            item.SetBoolean(true);

            Assert.That(SettingsEditSession.Load(session.Apply(), V4).Find("GenerateSeparateFiles").BooleanValue,
                Is.True);
        }

        [Test]
        public void AStringRoundTripsWithItsEscaping()
        {
            var session = Shipped();
            var item    = session.Find("ConnectionString");

            item.SetText(@"Data Source=.\SQLEXPRESS;Initial Catalog=North""wind");

            Assert.That(SettingsEditSession.Load(session.Apply(), V4).Find("ConnectionString").TextValue,
                Is.EqualTo(@"Data Source=.\SQLEXPRESS;Initial Catalog=North""wind"));
        }

        /// <summary>
        ///     A template written with @"" folder paths keeps them, so the diff is the value and not the style.
        /// </summary>
        [Test]
        public void AVerbatimStringStaysVerbatim()
        {
            var session = SettingsEditSession.Load(
                "<#\r\n    Settings.ContextFolder = @\"\"; // sub-folder\r\n#>\r\n", V4);

            session.Find("ContextFolder").SetText(@"Data\Context");

            Assert.That(session.Apply(), Does.Contain("= @\"Data\\Context\"; // sub-folder"));
        }

        [Test]
        public void AnEnumRoundTrips()
        {
            var session = Shipped();
            var item    = session.Find("DatabaseType");

            Assert.That(item.IsEditable, Is.True);
            Assert.That(item.SelectedMembers, Is.EqualTo(new[] { "SqlServer" }));

            item.SetMembers(new[] { "PostgreSQL" });

            Assert.That(session.Apply(), Does.Contain("= DatabaseType.PostgreSQL;"));
        }

        /// <summary>
        ///     ElementsToGenerate is a combination, and the editor has to read all of it and write all of it back.
        /// </summary>
        [Test]
        public void AFlagsEnumReadsAndWritesEveryMember()
        {
            var session = Shipped();
            var item    = session.Find("ElementsToGenerate");

            Assert.That(item.SelectedMembers.Count, Is.GreaterThan(1));
            Assert.That(item.IsEditable, Is.True);

            item.SetMembers(new[] { "Poco", "Context" });

            Assert.That(session.Apply(), Does.Contain("= Elements.Poco | Elements.Context;"));
        }

        [Test]
        public void AnEmptyFlagsSelectionWritesTheZeroMember()
        {
            var session = Shipped();
            var item    = session.Find("ElementsToGenerate");

            item.SetMembers(new string[0]);

            Assert.That(item.CurrentValueText, Is.EqualTo("Elements.None"));
        }

        [Test]
        public void SeveralChangesInOneSessionAllLand()
        {
            var session = Shipped();

            session.Find("GenerateSeparateFiles").SetBoolean(true);
            session.Find("DbContextName").SetText("NorthwindDbContext");
            session.Find("DatabaseType").SetMembers(new[] { "MySql" });

            var text = session.Apply();
            var reloaded = SettingsEditSession.Load(text, V4);

            Assert.That(reloaded.Find("GenerateSeparateFiles").BooleanValue, Is.True);
            Assert.That(reloaded.Find("DbContextName").TextValue, Is.EqualTo("NorthwindDbContext"));
            Assert.That(reloaded.Find("DatabaseType").SelectedMembers, Is.EqualTo(new[] { "MySql" }));

            var before = RepositoryFiles.DatabaseTemplate().Split('\n');
            var after  = text.Split('\n');
            Assert.That(before.Where((l, i) => l != after[i]).Count(), Is.EqualTo(3));
        }

        [Test]
        public void RevertingPutsTheOriginalValueBack()
        {
            var session = Shipped();
            var item    = session.Find("GenerateSeparateFiles");

            item.SetBoolean(true);
            Assert.That(session.HasChanges, Is.True);

            item.Revert();

            Assert.That(session.HasChanges, Is.False);
            Assert.That(session.Apply(), Is.EqualTo(RepositoryFiles.DatabaseTemplate()));
        }

        // ---------------------------------------------------------------- finding things

        /// <summary>
        ///     118 settings and nobody remembers the names, so search covers the help text too.
        /// </summary>
        [Test]
        public void SearchFindsASettingByItsHelpTextRatherThanItsName()
        {
            var results = Shipped().Search("suppresses");

            Assert.That(results, Is.Not.Empty);
            Assert.That(results.Select(r => r.Name), Has.None.Contains("suppresses"),
                "the term must be reached through the help text, not the name");
        }

        [Test]
        public void SearchFindsASettingByName()
        {
            Assert.That(Shipped().Search("DbContextName").Select(i => i.Name), Does.Contain("DbContextName"));
        }

        [Test]
        public void SearchNarrowsWithEveryWord()
        {
            var session = Shipped();

            Assert.That(session.Search("context name").Count, Is.LessThan(session.Search("context").Count));
        }

        [Test]
        public void AnEmptySearchReturnsEverything()
        {
            var session = Shipped();

            Assert.That(session.Search("   ").Count, Is.EqualTo(session.Items.Count));
        }

        [Test]
        public void SectionsComeOutInDeclarationOrder()
        {
            var sections = Shipped().Sections;

            Assert.That(sections, Is.Not.Empty);
            Assert.That(sections.First(), Is.EqualTo("Settings"));
        }

        // ---------------------------------------------------------------- v3

        /// <summary>
        ///     Almost the whole installed base is still on v3, whose settings differ. Loading a v3 template against
        ///     the v3 catalogue has to work as well as v4 does.
        /// </summary>
        [Test]
        public void AV3TemplateLoadsAgainstTheV3Catalogue()
        {
            var catalogue = SettingsCatalogue.Load(RepositoryFiles.SettingsMetadata("v3"));
            var original  = RepositoryFiles.V3Template();

            var session = SettingsEditSession.Load(original, catalogue);

            Assert.That(session.Find("FileManagerType"), Is.Not.Null, "v3 has FileManagerType and v4 does not");
            Assert.That(session.Find("FileManagerType").IsEditable, Is.True);
            Assert.That(session.Apply(), Is.EqualTo(original));
        }

        private static int CountCrLf(string text)
        {
            return text.Split(new[] { "\r\n" }, StringSplitOptions.None).Length - 1;
        }

        private static int CountBareLf(string text)
        {
            return text.Replace("\r\n", string.Empty).Count(c => c == '\n');
        }
    }
}
