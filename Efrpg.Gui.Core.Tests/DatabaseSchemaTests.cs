using System;
using System.Linq;
using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    /// <summary>
    ///     Parsed against the captured wire contract payload rather than hand-written XML, so this fails alongside
    ///     WireContractTests if the tool's output ever moves.
    /// </summary>
    [TestFixture]
    public class DatabaseSchemaTests
    {
        private static DatabaseSchema Real()
        {
            return DatabaseSchema.Parse(RepositoryFiles.WireContractPayload());
        }

        [Test]
        public void Parse_ReadsTheHeader()
        {
            var schema = Real();

            Assert.That(schema.DefaultSchema, Is.EqualTo("dbo"));
            Assert.That(schema.SchemaVersion, Is.EqualTo(EfrpgToolGate.RequiredSchemaVersion));
            Assert.That(schema.ToolVersion, Is.Not.Empty);
            Assert.That(schema.CanReadStoredProcedures, Is.True);
        }

        [Test]
        public void Parse_FindsTablesAndStoredProcedures()
        {
            var schema = Real();

            Assert.That(schema.Count(DatabaseObjectKind.Table), Is.GreaterThan(0));
            Assert.That(schema.Count(DatabaseObjectKind.StoredProcedure), Is.GreaterThan(0));
        }

        /// <summary>
        ///     Every row under Tables is a column, so a table with four columns appears four times in the payload
        ///     and must appear once in the picker.
        /// </summary>
        [Test]
        public void Parse_CollapsesTheColumnRowsIntoOneEntryPerTable()
        {
            var names = Real().Objects
                .Where(o => o.Kind == DatabaseObjectKind.Table || o.Kind == DatabaseObjectKind.View)
                .Select(o => o.FullName)
                .ToList();

            Assert.That(names.Distinct(StringComparer.OrdinalIgnoreCase).Count(), Is.EqualTo(names.Count));
        }

        [Test]
        public void Parse_QualifiesNamesWithTheirSchema()
        {
            Assert.That(Real().Objects.Select(o => o.FullName), Has.Some.Contains("."));
        }

        [Test]
        public void Parse_OrdersBySchemaThenName()
        {
            var objects = Real().Objects.ToList();

            Assert.That(objects, Is.Ordered);
        }

        /// <summary>A synonym is an alias for something already in the list, so listing it twice is misleading.</summary>
        [Test]
        public void Parse_SkipsSynonyms()
        {
            var payload =
                "<EfrpgResult schemaVersion=\"1\"><Tables>" +
                "<Row schemaName=\"dbo\" tableName=\"Real\" isView=\"false\" isSynonym=\"false\" columnName=\"a\" />" +
                "<Row schemaName=\"dbo\" tableName=\"Alias\" isView=\"false\" isSynonym=\"true\" columnName=\"a\" />" +
                "</Tables></EfrpgResult>";

            Assert.That(DatabaseSchema.Parse(payload).Objects.Select(o => o.Name), Is.EqualTo(new[] { "Real" }));
        }

        [Test]
        public void Parse_TellsViewsFromTables()
        {
            var payload =
                "<EfrpgResult><Tables>" +
                "<Row schemaName=\"dbo\" tableName=\"T\" isView=\"false\" columnName=\"a\" />" +
                "<Row schemaName=\"dbo\" tableName=\"V\" isView=\"true\" columnName=\"a\" />" +
                "</Tables></EfrpgResult>";

            var schema = DatabaseSchema.Parse(payload);

            Assert.That(schema.Of(DatabaseObjectKind.Table).Single().Name, Is.EqualTo("T"));
            Assert.That(schema.Of(DatabaseObjectKind.View).Single().Name, Is.EqualTo("V"));
        }

        [Test]
        public void Parse_TellsFunctionsFromStoredProcedures()
        {
            var payload =
                "<EfrpgResult><StoredProcedures>" +
                "<Row schema=\"dbo\" name=\"P\" isStoredProcedure=\"true\" />" +
                "<Row schema=\"dbo\" name=\"F\" isStoredProcedure=\"false\" isTableValuedFunction=\"true\" />" +
                "</StoredProcedures></EfrpgResult>";

            var schema = DatabaseSchema.Parse(payload);

            Assert.That(schema.Of(DatabaseObjectKind.StoredProcedure).Single().Name, Is.EqualTo("P"));
            Assert.That(schema.Of(DatabaseObjectKind.Function).Single().Name, Is.EqualTo("F"));
        }

        [Test]
        public void Parse_KeepsTheErrorsTheToolReported()
        {
            Assert.That(Real().Errors, Is.Not.Empty);
        }

        /// <summary>
        ///     A database with no schemas - SQLite - must not produce names that start with a dot.
        /// </summary>
        [Test]
        public void FullName_OmitsAnEmptySchema()
        {
            Assert.That(new DatabaseObject(string.Empty, "Customers", DatabaseObjectKind.Table).FullName,
                Is.EqualTo("Customers"));
        }

        /// <summary>
        ///     Attributes this parser does not know about are ignored, which is what lets a newer tool serve an
        ///     older GUI - the same forward-compatibility rule the wire format itself is built on.
        /// </summary>
        [Test]
        public void Parse_IgnoresAttributesItDoesNotKnow()
        {
            var payload =
                "<EfrpgResult somethingNew=\"1\"><Tables>" +
                "<Row schemaName=\"dbo\" tableName=\"T\" isView=\"false\" columnName=\"a\" alsoNew=\"2\" />" +
                "</Tables><SomethingElseEntirely /></EfrpgResult>";

            Assert.That(DatabaseSchema.Parse(payload).Objects.Single().Name, Is.EqualTo("T"));
        }

        /// <summary>
        ///     A tool that wrote a message to stdout instead of stderr would otherwise be parsed as an empty
        ///     database, which reads to the user as "your database has no tables".
        /// </summary>
        [TestCase("")]
        [TestCase("Unhandled exception: something went wrong")]
        [TestCase("<SomethingElse />")]
        public void Parse_RefusesAnythingThatIsNotAnEfrpgResult(string payload)
        {
            Assert.That(() => DatabaseSchema.Parse(payload), Throws.TypeOf<FormatException>());
        }
    }
}
