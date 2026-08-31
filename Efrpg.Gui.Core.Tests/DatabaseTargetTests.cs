using System.Linq;
using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    /// <summary>
    ///     The dropdown has to offer every database the generator supports, and each entry has to hand the user a
    ///     connection string of the right shape for that database. Both are checked here against the generator
    ///     rather than against this list's own author.
    /// </summary>
    [TestFixture]
    public class DatabaseTargetTests
    {
        /// <summary>
        ///     A database type added to the generator and not to this list would simply be missing from the
        ///     dropdown, with nothing anywhere to say so.
        /// </summary>
        [Test]
        public void EveryDatabaseTypeTheGeneratorSupportsIsOffered()
        {
            var offered = DatabaseTarget.All.Select(t => t.Name).OrderBy(n => n);

            Assert.That(offered, Is.EqualTo(RepositoryFiles.EnumMembers("DatabaseType").OrderBy(n => n)),
                "DatabaseTarget.All no longer matches the DatabaseType enum. Add the missing entry, with a "
                + "connection string of the right shape for that database.");
        }

        [Test]
        public void TheDefaultIsSqlServerAsTheShippedTemplateAlreadySays()
        {
            Assert.That(DatabaseTarget.Default.Name, Is.EqualTo("SqlServer"));
        }

        /// <summary>
        ///     The dialog refuses OK while a placeholder remains, so a default with none would let a user through
        ///     with a connection string pointing at whatever host happened to be written into this file.
        /// </summary>
        [TestCaseSource(nameof(AllTargets))]
        public void EveryDefaultConnectionStringAsksForSomething(DatabaseTarget target)
        {
            Assert.That(target.ConnectionString, Does.Contain(TemplateSettingWriter.Placeholder));
        }

        [TestCaseSource(nameof(AllTargets))]
        public void EveryTargetExplainsWhatToReplace(DatabaseTarget target)
        {
            Assert.That(target.Hint, Does.Contain(TemplateSettingWriter.Placeholder));
            Assert.That(target.DisplayName, Is.Not.Empty);
        }

        /// <summary>
        ///     The dialog swaps the connection string when the database changes only while the box still holds an
        ///     untouched default, so two targets sharing one default would make the swap ambiguous.
        /// </summary>
        [Test]
        public void TheDefaultsAreDistinct()
        {
            Assert.That(DatabaseTarget.All.Select(t => t.ConnectionString).Distinct().Count(),
                Is.EqualTo(DatabaseTarget.All.Count));
        }

        [Test]
        public void IsUntouchedDefault_RecognisesEveryDefault()
        {
            Assert.That(DatabaseTarget.All.All(t => DatabaseTarget.IsUntouchedDefault(t.ConnectionString)), Is.True);
        }

        [Test]
        public void IsUntouchedDefault_IsFalseOnceTheUserHasFilledThePlaceholderIn()
        {
            var edited = DatabaseTarget.Default.ConnectionString.Replace(TemplateSettingWriter.Placeholder, "Northwind");

            Assert.That(DatabaseTarget.IsUntouchedDefault(edited), Is.False);
        }

        [Test]
        public void Find_IsCaseSensitiveBecauseTheEnumMemberNameIsWrittenIntoTheTemplate()
        {
            Assert.That(DatabaseTarget.Find("PostgreSQL"), Is.Not.Null);
            Assert.That(DatabaseTarget.Find("postgresql"), Is.Null);
        }

        /// <summary>
        ///     The whole reason the dropdown exists: the providers agree on nothing, so each default must be built
        ///     from that provider's own keywords.
        /// </summary>
        [TestCase("SqlServer",  "Initial Catalog=")]
        [TestCase("SQLite",     "Data Source=")]
        [TestCase("PostgreSQL", "Port=5432")]
        [TestCase("MySql",      "Port=3306")]
        [TestCase("Oracle",     ":1521/")]
        public void TheConnectionStringUsesTheProvidersOwnKeywords(string name, string expected)
        {
            Assert.That(DatabaseTarget.Find(name)!.ConnectionString, Does.Contain(expected));
        }

        private static DatabaseTarget[] AllTargets => DatabaseTarget.All.ToArray();
    }
}
