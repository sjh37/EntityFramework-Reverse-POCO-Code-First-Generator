using Efrpg;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    /// <summary>
    ///     Two tables in one schema can legitimately differ only by case - PostgreSQL's <c>categories</c> and
    ///     <c>"CATEGORIES"</c> are distinct objects, and MySQL on a case sensitive filesystem, Oracle with quoted
    ///     identifiers and SQL Server under a case sensitive collation can all do the same. <c>GetTable</c> used to
    ///     match case insensitively with <c>SingleOrDefault</c>, so it found both and threw; <c>LoadTables()</c>
    ///     swallowed that and abandoned the rest of the model, and every table in the database was then reported as
    ///     having no primary key because <c>SetPrimaryKeys()</c> never ran.
    /// </summary>
    [TestFixture]
    [Category(Constants.CI)]
    public class TablesGetTableTests
    {
        private static Tables TwoTablesDifferingOnlyByCase()
        {
            return new Tables
            {
                new Table(null, new Schema("public"), "categories", false),
                new Table(null, new Schema("public"), "CATEGORIES", false)
            };
        }

        [Test]
        public void GetTable_TwoTablesDifferingOnlyByCase_DoesNotThrow()
        {
            var tables = TwoTablesDifferingOnlyByCase();

            Assert.That(() => tables.GetTable("categories", "public"), Throws.Nothing);
        }

        [Test]
        public void GetTable_TwoTablesDifferingOnlyByCase_ReturnsTheExactMatch()
        {
            var tables = TwoTablesDifferingOnlyByCase();

            var found = tables.GetTable("CATEGORIES", "public");

            Assert.That(found.DbName, Is.EqualTo("CATEGORIES"));
        }

        [Test]
        public void GetTable_TwoTablesDifferingOnlyByCase_ReturnsTheOtherExactMatch()
        {
            var tables = TwoTablesDifferingOnlyByCase();

            var found = tables.GetTable("categories", "public");

            Assert.That(found.DbName, Is.EqualTo("categories"));
        }

        [Test]
        public void GetTable_NoExactMatch_FallsBackToCaseInsensitive()
        {
            // Dialects that fold case may report a different casing from the one used to create the object.
            var tables = new Tables { new Table(null, new Schema("dbo"), "Categories", false) };

            var found = tables.GetTable("CATEGORIES", "DBO");

            Assert.That(found.DbName, Is.EqualTo("Categories"));
        }

        [Test]
        public void GetTable_SameNameInTwoSchemas_ReturnsTheOneInTheRequestedSchema()
        {
            var tables = new Tables
            {
                new Table(null, new Schema("public"), "duplicated_name", false),
                new Table(null, new Schema("another"), "duplicated_name", false)
            };

            var found = tables.GetTable("duplicated_name", "another");

            Assert.That(found.Schema.DbName, Is.EqualTo("another"));
        }

        [Test]
        public void GetTable_NoMatch_ReturnsNull()
        {
            var tables = TwoTablesDifferingOnlyByCase();

            Assert.That(tables.GetTable("nope", "public"), Is.Null);
        }
    }
}
