using System;
using System.IO;
using System.Linq;
using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    /// <summary>
    ///     The picker's model, run against the shipped Database.tt and a small database that has one of everything:
    ///     a table the template's own filter excludes, a view, a stored procedure, both kinds of function.
    /// </summary>
    [TestFixture]
    public class ObjectSelectionTests
    {
        private const string Payload =
            "<EfrpgResult schemaVersion=\"1\" defaultSchema=\"dbo\">" +
            "<Tables>" +
            "<Row schemaName=\"dbo\" tableName=\"Customers\" isView=\"false\" columnName=\"Id\" />" +
            "<Row schemaName=\"dbo\" tableName=\"Customers\" isView=\"false\" columnName=\"Name\" />" +
            "<Row schemaName=\"dbo\" tableName=\"Order Details\" isView=\"false\" columnName=\"Id\" />" +
            "<Row schemaName=\"dbo\" tableName=\"Orders\" isView=\"false\" columnName=\"Id\" />" +
            "<Row schemaName=\"dbo\" tableName=\"AspNetUsers\" isView=\"false\" columnName=\"Id\" />" +
            "<Row schemaName=\"dbo\" tableName=\"__EFMigrationsHistory\" isView=\"false\" columnName=\"Id\" />" +
            "<Row schemaName=\"dbo\" tableName=\"vOrders\" isView=\"true\" columnName=\"Id\" />" +
            "</Tables>" +
            "<StoredProcedures>" +
            "<Row schema=\"dbo\" name=\"GetCustomer\" isStoredProcedure=\"true\" />" +
            "<Row schema=\"dbo\" name=\"GetOrders\" isStoredProcedure=\"true\" />" +
            "<Row schema=\"dbo\" name=\"fnOrders\" isTableValuedFunction=\"true\" />" +
            "<Row schema=\"dbo\" name=\"fnTotal\" isScalarValuedFunction=\"true\" />" +
            "</StoredProcedures>" +
            "</EfrpgResult>";

        private static DatabaseSchema Schema()
        {
            return DatabaseSchema.Parse(Payload);
        }

        /// <summary>
        ///     The same database with an Audit schema holding two tables and a procedure, and an Archive schema
        ///     holding one table.
        /// </summary>
        private static DatabaseSchema SchemaWithAudit()
        {
            return DatabaseSchema.Parse(Payload
                .Replace("</Tables>",
                    "<Row schemaName=\"Audit\" tableName=\"Log\" isView=\"false\" columnName=\"Id\" />" +
                    "<Row schemaName=\"Audit\" tableName=\"Trail\" isView=\"false\" columnName=\"Id\" />" +
                    "<Row schemaName=\"Archive\" tableName=\"Old\" isView=\"false\" columnName=\"Id\" />" +
                    "</Tables>")
                .Replace("</StoredProcedures>",
                    "<Row schema=\"Audit\" name=\"Purge\" isStoredProcedure=\"true\" />" +
                    "</StoredProcedures>"));
        }

        private static ObjectSelection Fresh(DatabaseSchema schema)
        {
            return ObjectSelection.Create(schema, TemplateFilterDocument.Parse(RepositoryFiles.DatabaseTemplate()));
        }

        private static void Untick(ObjectSelection selection, DatabaseSchema schema, string schemaName)
        {
            selection.SelectAll(schema.Objects.Where(o => o.Schema == schemaName).ToList(), false);
        }

        /// <summary>The picker's own live lines for one list; the template's commented-out examples do not count.</summary>
        private static string[] PickerLines(string text, string list)
        {
            return text.Split('\n')
                .Where(l => l.Contains("FilterSettings." + list + ".Add(") && l.Contains(TemplateFilterDocument.PickerMarker))
                .ToArray();
        }

        [Test]
        public void Apply_UntickingAWholeSchemaWritesASchemaExcludeAndNoNames()
        {
            var schema    = SchemaWithAudit();
            var before    = RepositoryFiles.DatabaseTemplate();
            var selection = Fresh(schema);

            Untick(selection, schema, "Audit");
            var after = selection.Apply();

            Assert.That(AddedLines(before, after), Is.EqualTo(new[]
            {
                "    FilterSettings.SchemaFilters.Add(new RegexExcludeFilter(@\"^(?:Audit)$\")); // " +
                TemplateFilterDocument.PickerMarker + ": right-click the .tt to change this"
            }));

            var reopened = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(after));
            Assert.That(reopened.Choices.Where(c => c.Object.Schema == "Audit").All(c => c.IsSelected == false && c.CanChange), Is.True);
            Assert.That(Choice(reopened, "Customers").IsSelected, Is.True);
            Assert.That(reopened.Apply(), Is.EqualTo(after));
        }

        [Test]
        public void Apply_KeepsASchemaWhileAnythingInItIsTicked()
        {
            var schema    = SchemaWithAudit();
            var selection = Fresh(schema);

            Untick(selection, schema, "Audit");
            selection.Select(schema.Objects.Single(o => o.Name == "Purge"), true);
            var after = selection.Apply();

            Assert.That(PickerLines(after, "SchemaFilters"), Is.Empty);
            Assert.That(after, Does.Contain("TableFilters.Add(new RegexExcludeFilter(@\"^(?:Log|Trail)$\")"));
        }

        [Test]
        public void Apply_WritesASchemaIncludeWhenFewerSchemasAreOn()
        {
            var schema    = SchemaWithAudit();
            var before    = RepositoryFiles.DatabaseTemplate();
            var selection = Fresh(schema);

            Untick(selection, schema, "dbo");
            Untick(selection, schema, "Archive");
            var after = selection.Apply();

            var added = AddedLines(before, after);
            Assert.That(added, Has.Some.Contains("SchemaFilters.Add(new RegexIncludeFilter(@\"^(?:Audit)$\")"));
            Assert.That(added.Where(l => l.Contains("TableFilters") || l.Contains("StoredProcedureFilters")), Is.Empty, "the dbo and Archive names are covered by the schema line");

            var reopened = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(after));
            Assert.That(Choice(reopened, "Customers").IsSelected, Is.False);
            Assert.That(Choice(reopened, "Log").IsSelected, Is.True);
            Assert.That(reopened.Apply(), Is.EqualTo(after));
        }

        [Test]
        public void Apply_TickingSomethingBackInASchemaRemovesTheSchemaLine()
        {
            var schema = SchemaWithAudit();
            var first  = Fresh(schema);

            Untick(first, schema, "Audit");
            var narrowed = first.Apply();

            var second = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(narrowed));
            second.Select(schema.Objects.Single(o => o.Name == "Log"), true);
            var after = second.Apply();

            Assert.That(PickerLines(after, "SchemaFilters"), Is.Empty);
            Assert.That(after, Does.Contain("TableFilters.Add(new RegexExcludeFilter(@\"^(?:Trail)$\")"));
            Assert.That(after, Does.Contain("StoredProcedureFilters.Add(new RegexExcludeFilter(@\"^(?:Purge)$\")"));
        }

        /// <summary>When the user narrows schemas themselves, the picker only ever adds names within those.</summary>
        [Test]
        public void Apply_NeverWritesASchemaLineBesideTheUsersOwnSchemaInclude()
        {
            var schema   = SchemaWithAudit();
            var template = RepositoryFiles.DatabaseTemplate().Replace(
                "    //FilterSettings.SchemaFilters.Add(new RegexIncludeFilter(\"dbo.*\"));",
                "    FilterSettings.SchemaFilters.Add(new RegexIncludeFilter(\"dbo.*\"));");
            var selection = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(template));

            Assert.That(Choice(selection, "Log").CanChange, Is.False);

            Untick(selection, schema, "dbo");
            var after = selection.Apply();

            Assert.That(PickerLines(after, "SchemaFilters"), Is.Empty);
        }

        [Test]
        public void Apply_UntickingOneSchemaOfTwoExcludesItRatherThanIncludingTheOther()
        {
            var schema    = SchemaWithAudit();
            var selection = Fresh(schema);

            Untick(selection, schema, "Audit");
            Untick(selection, schema, "Archive");
            selection.Select(schema.Objects.Single(o => o.Name == "Old"), true);

            Assert.That(PickerLines(selection.Apply(), "SchemaFilters").Single(), Does.Contain("RegexExcludeFilter(@\"^(?:Audit)$\")"));
        }

        private static ObjectSelection Shipped()
        {
            return ObjectSelection.Create(Schema(), TemplateFilterDocument.Parse(RepositoryFiles.DatabaseTemplate()));
        }

        private static DatabaseObject Object(DatabaseSchema schema, string name)
        {
            return schema.Objects.Single(o => o.Name == name);
        }

        private static ObjectChoice Choice(ObjectSelection selection, string name)
        {
            return selection.Choices.Single(c => c.Object.Name == name);
        }

        private static string[] AddedLines(string before, string after)
        {
            var old = before.Split('\n').Select(l => l.TrimEnd()).ToList();
            return after.Split('\n').Select(l => l.TrimEnd()).Where(l => !old.Contains(l)).ToArray();
        }

        [Test]
        public void Create_ShowsWhatTheShippedTemplateGeneratesToday()
        {
            var selection = Shipped();

            Assert.That(Choice(selection, "Customers").IsSelected, Is.True);
            Assert.That(Choice(selection, "Customers").CanChange, Is.True);
            Assert.That(Choice(selection, "vOrders").IsSelected, Is.True);
            Assert.That(Choice(selection, "GetCustomer").IsSelected, Is.True);
            Assert.That(Choice(selection, "fnOrders").IsSelected, Is.False, "table-valued functions default to off");
            Assert.That(Choice(selection, "fnOrders").CanChange, Is.True);
            Assert.That(Choice(selection, "fnTotal").IsSelected, Is.False, "scalar functions default to off");
            Assert.That(selection.HasChanges, Is.False);
        }

        [Test]
        public void Create_LocksWhatTheTemplatesOwnFiltersExclude()
        {
            var selection = Shipped();

            var aspNet = Choice(selection, "AspNetUsers");
            Assert.That(aspNet.IsSelected, Is.False);
            Assert.That(aspNet.CanChange, Is.False);
            Assert.That(aspNet.Reason, Does.Contain("AspNet.*"));

            Assert.That(Choice(selection, "__EFMigrationsHistory").CanChange, Is.False);

            Assert.That(() => selection.Select(Object(Schema(), "AspNetUsers"), true), Throws.InvalidOperationException);
        }

        /// <summary>
        ///     The persistence decision in one test: one table unticked out of four free objects is written as the
        ///     one name to exclude, not the three to include, and nothing else moves.
        /// </summary>
        [Test]
        public void Apply_WritesTheShorterListAsAnExcludeWhenOneTableIsUnticked()
        {
            var schema    = Schema();
            var before    = RepositoryFiles.DatabaseTemplate();
            var selection = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(before));

            selection.Select(Object(schema, "Orders"), false);
            var after = selection.Apply();

            Assert.That(selection.HasChanges, Is.True);
            Assert.That(AddedLines(before, after), Is.EqualTo(new[]
            {
                "    FilterSettings.TableFilters.Add(new RegexExcludeFilter(@\"^(?:Orders)$\")); // " +
                TemplateFilterDocument.PickerMarker + ": right-click the .tt to change this"
            }));
            Assert.That(after.Split('\n').Length, Is.EqualTo(before.Split('\n').Length + 1));
        }

        /// <summary>Two ticked and two unticked: the tie goes to the include list, which is the safer of the two.</summary>
        [Test]
        public void Apply_WritesAnIncludeListWhenNoMoreAreTickedThanUnticked()
        {
            var schema    = Schema();
            var before    = RepositoryFiles.DatabaseTemplate();
            var selection = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(before));

            selection.Select(Object(schema, "Customers"), false);
            selection.Select(Object(schema, "Orders"), false);
            var after = selection.Apply();

            Assert.That(AddedLines(before, after).Single(), Does.Contain("RegexIncludeFilter(@\"^(?:Order\\ Details|vOrders)$\")"));
        }

        /// <summary>
        ///     A user's own exclude, a picker exclude and a picker include all read back correctly: the user's
        ///     locks, the picker's are just unticked, and re-saving reproduces the same text.
        /// </summary>
        [Test]
        public void Apply_ReadsItsOwnExcludeListBackAsUnticked()
        {
            var schema = Schema();
            var first  = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(RepositoryFiles.DatabaseTemplate()));

            first.Select(Object(schema, "Orders"), false);
            var saved = first.Apply();

            var second = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(saved));

            Assert.That(Choice(second, "Orders").IsSelected, Is.False);
            Assert.That(Choice(second, "Orders").CanChange, Is.True);
            Assert.That(Choice(second, "AspNetUsers").CanChange, Is.False, "the user's own exclude still locks");
            Assert.That(second.Apply(), Is.EqualTo(saved));
        }

        [Test]
        public void Apply_TickingNothingWritesAnIncludeThatMatchesNothing()
        {
            var schema    = Schema();
            var before    = RepositoryFiles.DatabaseTemplate();
            var selection = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(before));

            selection.SelectAll(DatabaseObjectKind.Table, false);
            selection.SelectAll(DatabaseObjectKind.View, false);
            var after = selection.Apply();

            Assert.That(AddedLines(before, after), Has.Some.Contains("RegexIncludeFilter(@\"" + ObjectSelection.NothingPattern + "\")"));

            var reopened = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(after));
            Assert.That(reopened.Choices.Where(c => c.Object.Kind == DatabaseObjectKind.Table && c.CanChange).All(c => c.IsSelected == false), Is.True);
        }

        [Test]
        public void Apply_WritesNothingWhenEverythingFreeIsTicked()
        {
            var schema    = Schema();
            var before    = RepositoryFiles.DatabaseTemplate();
            var selection = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(before));

            selection.Select(Object(schema, "Orders"), false);
            selection.Select(Object(schema, "Orders"), true);

            Assert.That(selection.Apply(), Is.EqualTo(before));
        }

        [Test]
        public void Apply_SwitchesAWholeCategoryOffThroughItsFlagRatherThanARegex()
        {
            var schema    = Schema();
            var before    = RepositoryFiles.DatabaseTemplate();
            var selection = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(before));

            selection.Select(Object(schema, "vOrders"), false);
            var after = selection.Apply();

            Assert.That(selection.IsKindEnabled(DatabaseObjectKind.View), Is.False);
            Assert.That(AddedLines(before, after), Is.EqualTo(new[] { "    FilterSettings.IncludeViews                 = false;" }));
            Assert.That(after, Does.Not.Contain(TemplateFilterDocument.PickerMarker));
        }

        [Test]
        public void Apply_SwitchesFunctionsOnThroughTheirFlag()
        {
            var schema    = Schema();
            var before    = RepositoryFiles.DatabaseTemplate();
            var selection = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(before));

            selection.Select(Object(schema, "fnOrders"), true);
            var after = selection.Apply();

            Assert.That(AddedLines(before, after), Is.EqualTo(new[] { "    FilterSettings.IncludeTableValuedFunctions  = true; // If true, for EF6 install the \"EntityFramework.CodeFirstStoreFunctions\" NuGet Package." }));
        }

        /// <summary>
        ///     The generator reads stored procedures whenever a function flag is on, so wanting only a function
        ///     needs an include list that names it - and the flag the template's comment says to set.
        /// </summary>
        [Test]
        public void Apply_NarrowsToAFunctionAloneWithAnIncludeList()
        {
            var schema    = Schema();
            var before    = RepositoryFiles.DatabaseTemplate();
            var selection = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(before));

            selection.SelectAll(DatabaseObjectKind.StoredProcedure, false);
            selection.Select(Object(schema, "fnOrders"), true);
            var after = selection.Apply();

            var added = AddedLines(before, after);
            Assert.That(added, Has.Length.EqualTo(2));
            Assert.That(added[0], Does.StartWith("    FilterSettings.IncludeTableValuedFunctions  = true;"));
            Assert.That(added[1], Does.StartWith("    FilterSettings.StoredProcedureFilters.Add(new RegexIncludeFilter(@\"^(?:fnOrders)$\"));"));
            Assert.That(TemplateFilterDocument.Parse(after).Flag(FilterFlag.IncludeStoredProcedures), Is.True);
        }

        [Test]
        public void Apply_SwitchesStoredProceduresOffWhenNoRoutineIsWanted()
        {
            var schema    = Schema();
            var before    = RepositoryFiles.DatabaseTemplate();
            var selection = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(before));

            selection.SelectAll(DatabaseObjectKind.StoredProcedure, false);
            var after = selection.Apply();

            Assert.That(AddedLines(before, after), Is.EqualTo(new[] { "    FilterSettings.IncludeStoredProcedures      = false;" }));
        }

        /// <summary>Save, reopen, and the ticks are exactly what was saved. Then save again and nothing moves.</summary>
        [Test]
        public void Apply_RoundTripsThroughTheFile()
        {
            var schema = Schema();
            var first  = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(RepositoryFiles.DatabaseTemplate()));

            first.Select(Object(schema, "Orders"), false);
            first.Select(Object(schema, "vOrders"), false);
            first.Select(Object(schema, "GetOrders"), false);
            first.Select(Object(schema, "fnTotal"), true);
            var saved = first.Apply();

            var second = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(saved));

            Assert.That(Choice(second, "Customers").IsSelected, Is.True);
            Assert.That(Choice(second, "Orders").IsSelected, Is.False);
            Assert.That(Choice(second, "Orders").CanChange, Is.True);
            Assert.That(Choice(second, "vOrders").IsSelected, Is.False);
            Assert.That(Choice(second, "GetCustomer").IsSelected, Is.True);
            Assert.That(Choice(second, "GetOrders").IsSelected, Is.False);
            Assert.That(Choice(second, "fnOrders").IsSelected, Is.False);
            Assert.That(Choice(second, "fnTotal").IsSelected, Is.True);
            Assert.That(second.Apply(), Is.EqualTo(saved));
        }

        [Test]
        public void Apply_TickingEverythingBackRemovesThePickersLines()
        {
            var schema   = Schema();
            var original = RepositoryFiles.DatabaseTemplate();
            var first    = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(original));

            first.Select(Object(schema, "Orders"), false);
            var narrowed = first.Apply();

            var second = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(narrowed));
            second.Select(Object(schema, "Orders"), true);

            Assert.That(second.Apply(), Is.EqualTo(original));
        }

        /// <summary>With an include filter of the user's, nothing free is generated until ticked, and the user's line stays.</summary>
        [Test]
        public void Create_HonoursTheUsersOwnIncludeFilter()
        {
            var schema   = Schema();
            var template = RepositoryFiles.DatabaseTemplate().Replace(
                "    //FilterSettings.TableFilters.Add(new RegexIncludeFilter(\"^[Cc]ustomer.*\"));",
                "    FilterSettings.TableFilters.Add(new RegexIncludeFilter(\"^[Cc]ustomer.*\"));");
            var selection = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(template));

            var customers = Choice(selection, "Customers");
            Assert.That(customers.IsSelected, Is.True);
            Assert.That(customers.CanChange, Is.False);
            Assert.That(customers.Reason, Does.Contain("^[Cc]ustomer.*"));
            Assert.That(Choice(selection, "Orders").IsSelected, Is.False);
            Assert.That(Choice(selection, "Orders").CanChange, Is.True);

            selection.Select(Object(schema, "Orders"), true);
            var after = selection.Apply();

            Assert.That(AddedLines(template, after).Single(), Does.Contain("@\"^(?:Orders)$\""));
            Assert.That(after, Does.Contain("new RegexIncludeFilter(\"^[Cc]ustomer.*\")"));
        }

        [Test]
        public void Create_SaysSoWhenAFilterCannotBeEvaluated()
        {
            var schema   = Schema();
            var template = RepositoryFiles.DatabaseTemplate().Replace(
                "    FilterSettings.TableFilters.Add(new RegexExcludeFilter(\"AspNet.*\"));",
                "    FilterSettings.TableFilters.Add(new RegexExcludeFilter(new Regex(\"AspNet.*\", RegexOptions.IgnoreCase)));");
            var selection = ObjectSelection.Create(schema, TemplateFilterDocument.Parse(template));

            Assert.That(Choice(selection, "Customers").IsSelected, Is.Null);
            Assert.That(Choice(selection, "Customers").CanChange, Is.False);
            Assert.That(Choice(selection, "Customers").Reason, Does.Contain("RegexOptions.IgnoreCase"));
            Assert.That(Choice(selection, "GetCustomer").IsSelected, Is.True, "the stored procedure list is unaffected");

            selection.Select(Object(schema, "GetOrders"), false);

            Assert.That(AddedLines(template, selection.Apply()).Single(), Does.Contain("StoredProcedureFilters"));
        }

        [Test]
        public void Patterns_AreEscapedSortedAndWrapped()
        {
            var names = Enumerable.Range(0, 40).Select(i => "SomeFairlyLongTableName" + i).Concat(new[] { "Order Details", "A.B", "$x" }).ToList();

            var patterns = ObjectSelection.Patterns(names);

            Assert.That(patterns.Count, Is.GreaterThan(1));
            Assert.That(patterns, Has.All.StartsWith("^(?:").And.All.EndsWith(")$"));
            Assert.That(patterns[0], Does.StartWith("^(?:\\$x|A\\.B|Order\\ Details|"));

            var joined = new System.Text.RegularExpressions.Regex(string.Join("|", patterns));
            Assert.That(names.All(joined.IsMatch), Is.True);
            Assert.That(joined.IsMatch("SomeFairlyLongTableName"), Is.False);
            Assert.That(joined.IsMatch("Order"), Is.False);
        }

        /// <summary>
        ///     Every real template in the repository, with the captured EfrpgTest payload: opening the picker and
        ///     saving without touching anything must not change a byte.
        /// </summary>
        [Test]
        public void Apply_WithoutChangesLeavesEveryRealTemplateUntouched()
        {
            var schema = DatabaseSchema.Parse(RepositoryFiles.WireContractPayload());

            foreach (var path in RepositoryFiles.TemplateFixtures())
            {
                var text     = File.ReadAllText(path);
                var document = TemplateFilterDocument.Parse(text);

                if (document.RefusalReason != null)
                    continue;

                var selection = ObjectSelection.Create(schema, document);

                Assert.That(selection.HasChanges, Is.False, path);
                Assert.That(selection.Apply(), Is.EqualTo(text), path);
            }
        }
    }
}
