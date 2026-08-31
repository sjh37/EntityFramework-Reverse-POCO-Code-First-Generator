using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    /// <summary>
    ///     Reading a .tt into the dialog and writing the dialog back out. Both directions are tested against the
    ///     real shipped Database.tt, because that is the file every user starts from.
    /// </summary>
    [TestFixture]
    public class TemplateConfigurationTests
    {
        private static TemplateSettingsFile Shipped()
        {
            return new TemplateSettingsFile(RepositoryFiles.DatabaseTemplate());
        }

        [Test]
        public void ReadFrom_TheShippedTemplateGivesItsOwnDefaults()
        {
            var configuration = TemplateConfiguration.ReadFrom(Shipped(), "Fallback");

            Assert.That(configuration.Database.Name, Is.EqualTo("SqlServer"));
            Assert.That(configuration.Template.Name, Is.EqualTo("EfCore10"));
            Assert.That(configuration.ConnectionString, Is.EqualTo(DatabaseTarget.Default.ConnectionString));
            Assert.That(configuration.DbContextName, Is.EqualTo("MyDbContext"));
        }

        /// <summary>
        ///     The whole point of reading first: a user reopening the dialog to change one field must not have the
        ///     rest of their template quietly reset to the defaults.
        /// </summary>
        [Test]
        public void ReadingAndWritingBackWithoutChangingAnythingLeavesTheFileByteForByteIdentical()
        {
            var original = RepositoryFiles.DatabaseTemplate();

            var text = TemplateConfiguration.ReadFrom(new TemplateSettingsFile(original), "Fallback")
                .ApplyTo(new TemplateSettingsFile(original));

            Assert.That(text, Is.EqualTo(original));
        }

        [Test]
        public void ReadFrom_AnUnrecognisedEnumMemberFallsBackToTheDefault()
        {
            var settings = new TemplateSettingsFile(
                "    Settings.DatabaseType = DatabaseType.Informix;\r\n" +
                "    Settings.TemplateType = TemplateType.EfCore4;\r\n");

            var configuration = TemplateConfiguration.ReadFrom(settings, "Fallback");

            Assert.That(configuration.Database.Name, Is.EqualTo(DatabaseTarget.Default.Name));
            Assert.That(configuration.Template.Name, Is.EqualTo(TemplateTarget.Default.Name));
        }

        [Test]
        public void ReadFrom_AMissingDbContextNameUsesTheFallback()
        {
            var configuration = TemplateConfiguration.ReadFrom(new TemplateSettingsFile(string.Empty), "NorthwindDbContext");

            Assert.That(configuration.DbContextName, Is.EqualTo("NorthwindDbContext"));
        }

        [Test]
        public void ApplyTo_WritesEveryAnswer()
        {
            var settings = Shipped();

            new TemplateConfiguration(DatabaseTarget.Find("Oracle"), TemplateTarget.Find("Ef6"),
                "Data Source=localhost:1521/pdb1;User Id=hr;Password=secret;", "HrDbContext", "Hr.Data").ApplyTo(settings);

            Assert.That(settings.GetEnum("DatabaseType"), Is.EqualTo("Oracle"));
            Assert.That(settings.GetEnum("TemplateType"), Is.EqualTo("Ef6"));
            Assert.That(settings.GetEnum("GeneratorType"), Is.EqualTo("Ef6"));
            Assert.That(settings.GetString("ConnectionString"), Is.EqualTo("Data Source=localhost:1521/pdb1;User Id=hr;Password=secret;"));
            Assert.That(settings.GetString("DbContextName"), Is.EqualTo("HrDbContext"));
            Assert.That(settings.GetString("ConnectionStringName"), Is.EqualTo("HrDbContext"));
            Assert.That(settings.GetString("Namespace"), Is.EqualTo("Hr.Data"));
        }

        /// <summary>
        ///     The pairing that produces code which does not compile when it is got wrong, checked here on the file
        ///     rather than only on the lookup table.
        /// </summary>
        [Test]
        public void ApplyTo_WritesTheGeneratorTypeThatGoesWithTheTemplate()
        {
            var settings = Shipped();

            new TemplateConfiguration(DatabaseTarget.Default, TemplateTarget.Find("FileBasedEf6"),
                "Data Source=(local);Initial Catalog=Northwind", "MyDbContext", string.Empty).ApplyTo(settings);

            Assert.That(settings.GetEnum("TemplateType"), Is.EqualTo("FileBasedEf6"));
            Assert.That(settings.GetEnum("GeneratorType"), Is.EqualTo("Ef6"));
        }

        /// <summary>
        ///     Settings.Namespace ships as the bare identifier DefaultNamespace, so writing a namespace has to
        ///     replace the whole right-hand side rather than the contents of a literal that is not there.
        /// </summary>
        [Test]
        public void ApplyTo_AnEmptyNamespaceRestoresDefaultNamespace()
        {
            var settings = Shipped();

            new TemplateConfiguration(DatabaseTarget.Default, TemplateTarget.Default,
                "Data Source=(local);Initial Catalog=Northwind", "MyDbContext", "Accounts.Billing").ApplyTo(settings);
            Assert.That(settings.GetString("Namespace"), Is.EqualTo("Accounts.Billing"));

            new TemplateConfiguration(DatabaseTarget.Default, TemplateTarget.Default,
                "Data Source=(local);Initial Catalog=Northwind", "MyDbContext", string.Empty).ApplyTo(settings);

            Assert.That(settings.GetExpression("Namespace"),
                Is.EqualTo(TemplateConfiguration.DefaultNamespaceExpression));
        }

        [Test]
        public void ReadFrom_TheShippedTemplateReportsNoNamespaceBecauseItUsesDefaultNamespace()
        {
            Assert.That(TemplateConfiguration.ReadFrom(Shipped(), "Fallback").Namespace, Is.Empty);
        }

        [TestCase("", true)]
        [TestCase("Accounts", true)]
        [TestCase("Accounts.Billing", true)]
        [TestCase("_private.Thing1", true)]
        [TestCase("Accounts.", false)]
        [TestCase("1Accounts", false)]
        [TestCase("Accounts Billing", false)]
        [TestCase("\"; Settings.ConnectionString = \"x", false)]
        public void HasValidNamespace_AcceptsOnlyDottedIdentifiers(string candidate, bool expected)
        {
            var configuration = new TemplateConfiguration(DatabaseTarget.Default, TemplateTarget.Default,
                "Data Source=(local);Initial Catalog=Northwind", "MyDbContext", candidate);

            Assert.That(configuration.HasValidNamespace, Is.EqualTo(expected));
        }

        /// <summary>
        ///     What goes into Settings.Namespace becomes C# in the .tt, so anything that is not a namespace is
        ///     left alone rather than written and broken.
        /// </summary>
        [Test]
        public void ApplyTo_LeavesAnInvalidNamespaceAlone()
        {
            var settings = Shipped();

            new TemplateConfiguration(DatabaseTarget.Default, TemplateTarget.Default,
                "Data Source=(local);Initial Catalog=Northwind", "MyDbContext", "not a namespace").ApplyTo(settings);

            Assert.That(settings.GetExpression("Namespace"),
                Is.EqualTo(TemplateConfiguration.DefaultNamespaceExpression));
        }

        [Test]
        public void ApplyTo_AnEmptyDbContextNameLeavesTheNamesAlone()
        {
            var settings = Shipped();

            new TemplateConfiguration(DatabaseTarget.Default, TemplateTarget.Default,
                "Data Source=(local);Initial Catalog=Northwind", string.Empty, string.Empty).ApplyTo(settings);

            Assert.That(settings.GetString("DbContextName"), Is.EqualTo("MyDbContext"));
        }

        /// <summary>
        ///     A named SQL Server instance survives the trip out to the file and back, which is what proves the
        ///     escaping and unescaping are inverses rather than merely both present.
        /// </summary>
        [Test]
        public void AConnectionStringWithBackslashesSurvivesARoundTrip()
        {
            const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Northwind;Password=a""b\";

            var settings = Shipped();
            new TemplateConfiguration(DatabaseTarget.Default, TemplateTarget.Default, connectionString, "X", string.Empty)
                .ApplyTo(settings);

            Assert.That(settings.GetString("ConnectionString"), Is.EqualTo(connectionString));
        }
    }
}
