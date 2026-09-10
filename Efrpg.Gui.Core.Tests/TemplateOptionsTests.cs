using System.Linq;
using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    [TestFixture]
    public class TemplateOptionsTests
    {
        private static TemplateSettingsFile Shipped()
        {
            return new TemplateSettingsFile(RepositoryFiles.DatabaseTemplate());
        }

        [Test]
        public void ReadFrom_TheShippedTemplate_GivesTheDefaults()
        {
            var options = TemplateOptions.ReadFrom(Shipped());

            Assert.That(options.GenerateSeparateFiles, Is.False);
            Assert.That(options.UseFileScopedNamespaces, Is.False);
            Assert.That(options.AddUnitTestingDbContext, Is.True);
            Assert.That(options.FakeDbContextInDebugOnlyMode, Is.False);
        }

        [Test]
        public void ApplyTo_FlippingEveryOption_RewritesTheFourLinesAndNothingElse()
        {
            var settings = Shipped();
            var before   = settings.Text.Split('\n');

            new TemplateOptions(true, true, false, true).ApplyTo(settings);

            var after   = settings.Text.Split('\n');
            var options = TemplateOptions.ReadFrom(settings);
            Assert.That(after.Length, Is.EqualTo(before.Length));
            Assert.That(before.Where((l, i) => l != after[i]).Count(), Is.EqualTo(4));
            Assert.That(options.GenerateSeparateFiles, Is.True);
            Assert.That(options.UseFileScopedNamespaces, Is.True);
            Assert.That(options.AddUnitTestingDbContext, Is.False);
            Assert.That(options.FakeDbContextInDebugOnlyMode, Is.True);
        }

        [Test]
        public void ApplyTo_WritingWhatWasRead_LeavesTheFileByteForByteIdentical()
        {
            var settings = Shipped();
            var original = settings.Text;

            TemplateOptions.ReadFrom(settings).ApplyTo(settings);

            Assert.That(settings.Text, Is.EqualTo(original));
        }

        [Test]
        public void ReadFrom_AnOptionSetToAnExpression_ReadsAsTheDefault()
        {
            var settings = new TemplateSettingsFile("    Settings.AddUnitTestingDbContext = Environment.MachineName == \"BUILD\";\r\n");

            var options = TemplateOptions.ReadFrom(settings);

            Assert.That(options.AddUnitTestingDbContext, Is.True);
        }

        [Test]
        public void ApplyTo_AnOptionSetToAnExpression_IsLeftAlone()
        {
            var text     = "    Settings.AddUnitTestingDbContext = Environment.MachineName == \"BUILD\";\r\n";
            var settings = new TemplateSettingsFile(text);

            new TemplateOptions(false, false, false, false).ApplyTo(settings);

            Assert.That(settings.Text, Is.EqualTo(text));
        }

        [Test]
        public void ApplyTo_AnAbsentOption_IsNotAdded()
        {
            var settings = new TemplateSettingsFile("    Settings.DbContextName = \"MyDbContext\";\r\n");

            new TemplateOptions(true, true, true, true).ApplyTo(settings);

            Assert.That(settings.Text, Does.Not.Contain("GenerateSeparateFiles"));
        }
    }
}
