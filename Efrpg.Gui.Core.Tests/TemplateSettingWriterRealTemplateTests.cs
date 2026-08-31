using System.Linq;
using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    /// <summary>
    ///     The same writer, run against the actual Database.tt that ships, rather than a fixture that resembles it.
    /// </summary>
    /// <remarks>
    ///     A fixture only proves the writer works on what its author imagined. Database.tt is generated from the
    ///     footer in BuildTT/BuildTT.cs and its formatting changes whenever a setting is added or the alignment is
    ///     tidied - so the file the wizard actually edits is the one worth testing against. If BuildTT ever emits
    ///     these settings differently, this fails and the wizard is fixed before a user meets a mangled template.
    /// </remarks>
    [TestFixture]
    public class TemplateSettingWriterRealTemplateTests
    {
        private static string ShippedTemplate()
        {
            return RepositoryFiles.DatabaseTemplate();
        }

        [Test]
        public void TheShippedTemplateIsUnconfigured()
        {
            Assert.That(new TemplateSettingWriter(ShippedTemplate()).IsUnconfigured, Is.True,
                "A freshly generated Database.tt must still carry the placeholder, or the wizard has nothing to detect.");
        }

        /// <summary>
        ///     Every setting the wizard writes must actually be writable in the shipped template. A rename or a
        ///     change of shape in BuildTT's footer would otherwise leave the wizard silently doing nothing.
        /// </summary>
        [TestCase("ConnectionString")]
        [TestCase("ConnectionStringName")]
        [TestCase("DbContextName")]
        public void TheWizardCanWrite(string settingName)
        {
            var writer = new TemplateSettingWriter(ShippedTemplate());

            Assert.That(writer.TrySetString(settingName, "WizardWroteThis"), Is.True,
                "Settings." + settingName + " is not a single-line string assignment in the shipped Database.tt any " +
                "more, so the wizard can no longer set it.");
        }

        [TestCase("DatabaseType",  "Oracle")]
        [TestCase("TemplateType",  "Ef6")]
        [TestCase("GeneratorType", "Ef6")]
        public void TheWizardCanWriteTheEnums(string settingName, string memberName)
        {
            var writer = new TemplateSettingWriter(ShippedTemplate());

            Assert.That(writer.TrySetEnum(settingName, memberName), Is.True,
                "Settings." + settingName + " is not a single-line enum assignment in the shipped Database.tt any " +
                "more, so the wizard can no longer set it.");

            Assert.That(writer.Text, Does.Contain(settingName.Replace("Settings.", string.Empty) + "." + memberName));
        }

        /// <summary>
        ///     The dialog opens on its own defaults, so if those drift from what the shipped template already says
        ///     the user is shown one thing and, on Skip, left with another.
        /// </summary>
        [Test]
        public void TheDialogDefaultsMatchWhatTheShippedTemplateAlreadySays()
        {
            var template = ShippedTemplate();

            Assert.That(template, Does.Contain("DatabaseType." + DatabaseTarget.Default.Name + ";"));
            Assert.That(template, Does.Contain("TemplateType." + TemplateTarget.Default.Name + ";"));
            Assert.That(template, Does.Contain("GeneratorType." + TemplateTarget.Default.GeneratorTypeName + ";"));
            Assert.That(template, Does.Contain("= \"" + DatabaseTarget.Default.ConnectionString + "\";"));
        }

        [Test]
        public void WritingTheConnectionStringChangesOneLineAndClearsThePlaceholder()
        {
            var original = ShippedTemplate();
            var writer   = new TemplateSettingWriter(original);

            writer.TrySetString("ConnectionString", @"Data Source=.\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True");

            var before = original.Split('\n');
            var after  = writer.Text.Split('\n');

            Assert.That(after.Length, Is.EqualTo(before.Length));
            Assert.That(before.Where((l, i) => l != after[i]).Count(), Is.EqualTo(1));
            Assert.That(writer.IsUnconfigured, Is.False);
        }

        /// <summary>
        ///     Everything the wizard writes in one go, which is the only combination that actually ships.
        /// </summary>
        [Test]
        public void WritingEverythingTheWizardWritesChangesSixLinesAndNothingElse()
        {
            var original = ShippedTemplate();
            var writer   = new TemplateSettingWriter(original);

            writer.TrySetString("ConnectionString", "Data Source=localhost:1521/pdb1;User Id=hr;Password=secret;");
            writer.TrySetEnum("DatabaseType", "Oracle");
            writer.TrySetEnum("TemplateType", "Ef6");
            writer.TrySetEnum("GeneratorType", "Ef6");
            writer.TrySetString("DbContextName", "HrDbContext");
            writer.TrySetString("ConnectionStringName", "HrDbContext");

            var before = original.Split('\n');
            var after  = writer.Text.Split('\n');

            Assert.That(after.Length, Is.EqualTo(before.Length));
            Assert.That(before.Where((l, i) => l != after[i]).Count(), Is.EqualTo(6));
            Assert.That(writer.IsUnconfigured, Is.False);
        }

        /// <summary>
        ///     The include directive on line 1 is what the generator and the Phase 3 editor identify the file by.
        /// </summary>
        [Test]
        public void WritingLeavesTheIncludeDirectiveIntact()
        {
            var writer = new TemplateSettingWriter(ShippedTemplate());

            writer.TrySetString("ConnectionString", "Data Source=(local);Initial Catalog=Northwind");

            Assert.That(writer.Text.Split('\n').First().Trim(),
                Is.EqualTo("<#@ include file=\"EF.Reverse.POCO.v4.ttinclude\" #>"));
        }
    }
}
