using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    /// <summary>
    ///     The wizard writes the user's answers into a .tt that is authoritative, usually in source control, and
    ///     often already customised. Everything it does not deliberately change must survive byte for byte, so most
    ///     of these tests are about what is left alone rather than what is written.
    /// </summary>
    [TestFixture]
    public class TemplateSettingWriterTests
    {
        /// <summary>Copied from the real Database.tt, alignment and trailing comment included.</summary>
        private const string Template =
            "<#@ include file=\"EF.Reverse.POCO.v4.ttinclude\" #>\r\n" +
            "<#\r\n" +
            "    Settings.DatabaseType                 = DatabaseType.SqlServer; // SqlServer, SQLite, PostgreSQL\r\n" +
            "    Settings.ConnectionString             = \"Data Source=(local);Initial Catalog=**TODO**;Integrated Security=True\"; // reverse engineer your database\r\n" +
            "    Settings.DbContextName                = \"MyDbContext\"; // Class name for the DbContext\r\n" +
            "    //Settings.DbContextInterfaceName     = \"IMyDbContext\"; // Defaults to \"I\" + DbContextName\r\n" +
            "#>\r\n";

        [Test]
        public void TrySetString_WritesTheValue()
        {
            var writer = new TemplateSettingWriter(Template);

            var written = writer.TrySetString("DbContextName", "NorthwindDbContext");

            Assert.That(written, Is.True);
            Assert.That(writer.Text, Does.Contain("= \"NorthwindDbContext\"; // Class name for the DbContext"));
        }

        [Test]
        public void TrySetString_ChangesExactlyOneLine()
        {
            var writer = new TemplateSettingWriter(Template);

            writer.TrySetString("DbContextName", "NorthwindDbContext");

            var before = Template.Split('\n');
            var after  = writer.Text.Split('\n');
            var changed = 0;
            for (var i = 0; i < before.Length; i++)
                if (before[i] != after[i])
                    changed++;

            Assert.That(after.Length, Is.EqualTo(before.Length), "The line count must not change.");
            Assert.That(changed, Is.EqualTo(1), "Exactly one line may differ.");
        }

        [Test]
        public void TrySetString_KeepsTheAlignmentAndTheTrailingComment()
        {
            var writer = new TemplateSettingWriter(Template);

            writer.TrySetString("ConnectionString", "Data Source=(local);Initial Catalog=Northwind");

            Assert.That(writer.Text, Does.Contain(
                "    Settings.ConnectionString             = \"Data Source=(local);Initial Catalog=Northwind\"; // reverse engineer your database"));
        }

        [Test]
        public void TrySetString_PreservesCrLf()
        {
            var writer = new TemplateSettingWriter(Template);

            writer.TrySetString("DbContextName", "X");

            Assert.That(writer.Text, Does.Not.Contain("\n\n"));
            Assert.That(writer.Text.Replace("\r\n", string.Empty), Does.Not.Contain("\n"), "No bare LF may be introduced.");
        }

        /// <summary>
        ///     A named SQL Server instance is Data Source=.\SQLEXPRESS, and an unescaped backslash produces a .tt
        ///     that does not compile - which the user would meet as a build error in generated code, far from here.
        /// </summary>
        [Test]
        public void TrySetString_EscapesBackslashesSoTheTemplateStillCompiles()
        {
            var writer = new TemplateSettingWriter(Template);

            writer.TrySetString("ConnectionString", @"Data Source=.\SQLEXPRESS;Initial Catalog=Northwind");

            Assert.That(writer.Text, Does.Contain(@"= ""Data Source=.\\SQLEXPRESS;Initial Catalog=Northwind"";"));
        }

        [Test]
        public void TrySetString_EscapesQuotes()
        {
            var writer = new TemplateSettingWriter(Template);

            writer.TrySetString("ConnectionString", "Data Source=(local);Password=a\"b");

            Assert.That(writer.Text, Does.Contain("= \"Data Source=(local);Password=a\\\"b\";"));
        }

        /// <summary>
        ///     A commented-out setting is a user saying "not this one". Writing to it would silently turn an inactive
        ///     line into an active one.
        /// </summary>
        [Test]
        public void TrySetString_LeavesACommentedOutSettingAlone()
        {
            var writer = new TemplateSettingWriter(Template);

            var written = writer.TrySetString("DbContextInterfaceName", "IWhatever");

            Assert.That(written, Is.False);
            Assert.That(writer.Text, Is.EqualTo(Template));
        }

        /// <summary>
        ///     DatabaseType is an enum, not a string literal. Rewriting it as though it were would produce a .tt that
        ///     does not compile, so it must be refused rather than mangled.
        /// </summary>
        [Test]
        public void TrySetString_RefusesASettingThatIsNotAStringLiteral()
        {
            var writer = new TemplateSettingWriter(Template);

            var written = writer.TrySetString("DatabaseType", "SqlServer");

            Assert.That(written, Is.False);
            Assert.That(writer.Text, Is.EqualTo(Template));
        }

        [Test]
        public void TrySetString_UnknownSettingChangesNothing()
        {
            var writer = new TemplateSettingWriter(Template);

            var written = writer.TrySetString("NoSuchSetting", "x");

            Assert.That(written, Is.False);
            Assert.That(writer.Text, Is.EqualTo(Template));
        }

        [Test]
        public void IsUnconfigured_IsTrueWhileThePlaceholderRemains()
        {
            var writer = new TemplateSettingWriter(Template);

            Assert.That(writer.IsUnconfigured, Is.True);
        }

        [Test]
        public void IsUnconfigured_IsFalseOnceARealConnectionStringIsWritten()
        {
            var writer = new TemplateSettingWriter(Template);

            writer.TrySetString("ConnectionString", "Data Source=(local);Initial Catalog=Northwind");

            Assert.That(writer.IsUnconfigured, Is.False);
        }
    }
}
