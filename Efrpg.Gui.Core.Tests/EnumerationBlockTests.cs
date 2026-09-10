using System;
using System.Linq;
using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    /// <summary>
    ///     Appending enums to Settings.Enumerations: the entry lands as the last element, nothing already in the
    ///     block moves, and the result still parses as one assignment the editor can switch off and on.
    /// </summary>
    [TestFixture]
    public class EnumerationBlockTests
    {
        private static SettingsCatalogue V4 => SettingsCatalogue.Load(RepositoryFiles.SettingsMetadata("v4"));

        private static readonly EnumerationEntry DaysOfWeek =
            new EnumerationEntry("DaysOfWeek", "EnumTest.DaysOfWeek", "TypeName", "TypeId", null);

        [Test]
        public void Append_OnTheShippedTemplate_AddsTheEntryLastAndKeepsEverythingElse()
        {
            var original = RepositoryFiles.DatabaseTemplate();
            var document = TemplateSettingsDocument.Parse(original);
            var before   = document.StatementText(document.Find("Enumerations"));

            var appended = EnumerationBlock.Append(document, DaysOfWeek);

            var after = appended.StatementText(appended.Find("Enumerations")).Replace("\r\n", "\n").Split('\n');
            var head  = before.Replace("\r\n", "\n").Split('\n');
            Assert.That(after.Take(head.Length - 1), Is.EqualTo(head.Take(head.Length - 1)), "every existing line is untouched");
            Assert.That(after.Last().Trim(), Is.EqualTo("};"), "the block still closes the same way");
            Assert.That(string.Join("\n", after), Does.Contain("        new EnumerationSettings\n        {\n            Name       = \"DaysOfWeek\",\n            Table      = \"EnumTest.DaysOfWeek\",\n            NameField  = \"TypeName\",\n            ValueField = \"TypeId\"\n        },\n    };"));
            Assert.That(appended.Text.Replace("\r\n", "\n").Split('\n').Length, Is.EqualTo(original.Replace("\r\n", "\n").Split('\n').Length + 7));
        }

        [Test]
        public void Append_KeepsTheFileLineEndings()
        {
            var lf = TemplateSettingsDocument.Parse(RepositoryFiles.DatabaseTemplate().Replace("\r\n", "\n"));

            var appended = EnumerationBlock.Append(lf, DaysOfWeek);

            Assert.That(appended.Text, Does.Not.Contain("\r"));
        }

        [Test]
        public void Append_TwiceInARow_GivesTwoEntriesInOrder()
        {
            var document = TemplateSettingsDocument.Parse(RepositoryFiles.DatabaseTemplate());
            var second   = new EnumerationEntry("OrderStatus", "dbo.OrderStatus", "Name", "Id", null);

            var appended = EnumerationBlock.Append(EnumerationBlock.Append(document, DaysOfWeek), second);
            var text     = appended.StatementText(appended.Find("Enumerations"));

            Assert.That(text.IndexOf("\"DaysOfWeek\""), Is.LessThan(text.IndexOf("\"OrderStatus\"")));
            Assert.That(appended.Find("Enumerations").SpansMultipleLines, Is.True);
        }

        [Test]
        public void Append_AGroupedEnum_WritesTheGroupField()
        {
            var document = TemplateSettingsDocument.Parse(RepositoryFiles.DatabaseTemplate());
            var grouped  = new EnumerationEntry("{GroupField}Type", "dbo.Lookups", "Name", "Id", "GroupName");

            var text = EnumerationBlock.Append(document, grouped).Text;

            Assert.That(text, Does.Contain("ValueField = \"Id\",\n            GroupField = \"GroupName\"\n").Or.Contain("ValueField = \"Id\",\r\n            GroupField = \"GroupName\"\r\n"));
        }

        [Test]
        public void CannotAppendReason_ExplainsAnAbsentACommentedOutAndAForeignBlock()
        {
            var shipped = TemplateSettingsDocument.Parse(RepositoryFiles.DatabaseTemplate());
            Assert.That(EnumerationBlock.CannotAppendReason(shipped), Is.Null);

            var off = shipped.WithCommentedOut(shipped.Find("Enumerations"));
            Assert.That(EnumerationBlock.CannotAppendReason(off), Does.Contain("commented out"));

            var gone = shipped.WithoutAssignment(shipped.Find("Enumerations"));
            Assert.That(EnumerationBlock.CannotAppendReason(gone), Does.Contain("not in this template"));

            var foreign = TemplateSettingsDocument.Parse("<#\n    Settings.Enumerations = BuildEnums();\n#>");
            Assert.That(EnumerationBlock.CannotAppendReason(foreign), Does.Contain("cannot be appended"));
        }

        [Test]
        public void MentionsTable_FindsATableAlreadyInTheBlock()
        {
            var document = TemplateSettingsDocument.Parse(RepositoryFiles.DatabaseTemplate());
            var appended = EnumerationBlock.Append(document, DaysOfWeek);

            Assert.That(EnumerationBlock.MentionsTable(appended, "EnumTest.DaysOfWeek"), Is.True);
            Assert.That(EnumerationBlock.MentionsTable(appended, "dbo.Nothing"), Is.False);
        }

        [Test]
        public void Session_QueuesAnEnumAndWritesItAfterTheOtherEdits()
        {
            var session = SettingsEditSession.Load(RepositoryFiles.DatabaseTemplate(), V4);
            session.Find("GenerateSeparateFiles").SetBoolean(true);
            session.AddEnumeration(DaysOfWeek);

            Assert.That(session.HasChanges, Is.True);
            Assert.That(session.ChangeCount, Is.EqualTo(2));

            var reloaded = SettingsEditSession.Load(session.Apply(), V4);
            Assert.That(reloaded.Find("GenerateSeparateFiles").BooleanValue, Is.True);
            Assert.That(EnumerationBlock.MentionsTable(reloaded.Document, "EnumTest.DaysOfWeek"), Is.True);
        }

        [Test]
        public void Session_SwitchesACommentedOutBlockOnBeforeAppending()
        {
            var shipped = TemplateSettingsDocument.Parse(RepositoryFiles.DatabaseTemplate());
            var off     = shipped.WithCommentedOut(shipped.Find("Enumerations")).Text;
            var session = SettingsEditSession.Load(off, V4);

            session.AddEnumeration(DaysOfWeek);
            var reloaded = SettingsEditSession.Load(session.Apply(), V4);

            Assert.That(reloaded.Find("Enumerations").IsAssigned, Is.True);
            Assert.That(EnumerationBlock.MentionsTable(reloaded.Document, "EnumTest.DaysOfWeek"), Is.True);
        }

        [Test]
        public void Session_AnInvalidEntryIsRefusedUpFront()
        {
            var session = SettingsEditSession.Load(RepositoryFiles.DatabaseTemplate(), V4);

            Assert.That(() => session.AddEnumeration(new EnumerationEntry("Bad Name", "dbo.T", "Name", "Id", null)),
                Throws.InvalidOperationException.With.Message.Contains("identifier"));
        }

        [TestCase("order_status", "OrderStatus")]
        [TestCase("ORDER_STATUS", "OrderStatus")]
        [TestCase("OrderStatus", "OrderStatus")]
        [TestCase("orderStatus", "OrderStatus")]
        [TestCase("1st_thing", "_1stThing")]
        public void PascalCase_NormalisesTableNames(string table, string expected)
        {
            Assert.That(EnumerationBlock.PascalCase(table), Is.EqualTo(expected));
        }

        [Test]
        public void Suggest_PicksTheIntegralKeyAndTheFirstTextColumn()
        {
            var table = new DatabaseObject("dbo", "order_status", DatabaseObjectKind.Table, new[]
            {
                new DatabaseColumn("id", "int", true, 1),
                new DatabaseColumn("code", "nvarchar", false, 2),
                new DatabaseColumn("sort_order", "int", false, 3)
            });

            var entry = EnumerationBlock.Suggest(table);

            Assert.That(entry.Name, Is.EqualTo("OrderStatus"));
            Assert.That(entry.Table, Is.EqualTo("dbo.order_status"));
            Assert.That(entry.NameField, Is.EqualTo("code"));
            Assert.That(entry.ValueField, Is.EqualTo("id"));
            Assert.That(entry.IsValid, Is.True);
        }

        [Test]
        public void Candidates_AreEveryTableInNameOrderWithViewsLeftOut()
        {
            var schema = DatabaseSchema.Parse(RepositoryFiles.WireContractPayload());

            var candidates = EnumerationBlock.Candidates(schema);

            Assert.That(candidates, Has.All.Property("Kind").EqualTo(DatabaseObjectKind.Table));
            Assert.That(candidates.Count, Is.EqualTo(schema.Count(DatabaseObjectKind.Table)));
            Assert.That(candidates.Select(t => t.FullName), Is.Ordered.Using((System.Collections.Generic.IComparer<string>) StringComparer.OrdinalIgnoreCase));
        }

        [Test]
        public void Schema_CarriesColumnsForTables()
        {
            var schema = DatabaseSchema.Parse(RepositoryFiles.WireContractPayload());
            var table  = schema.Of(DatabaseObjectKind.Table).First();

            Assert.That(table.Columns, Is.Not.Empty);
            Assert.That(table.Columns.Select(c => c.Ordinal), Is.Ordered);
            Assert.That(schema.Of(DatabaseObjectKind.StoredProcedure).All(p => p.Columns.Count == 0), Is.True);
        }

        [Test]
        public void Entry_ProblemsAreNamedInTheOrderAUserFillsTheFormIn()
        {
            Assert.That(new EnumerationEntry("X", "", "n", "v", null).Problem, Does.Contain("table"));
            Assert.That(new EnumerationEntry("X", "t", "", "v", null).Problem, Does.Contain("name"));
            Assert.That(new EnumerationEntry("X", "t", "n", "", null).Problem, Does.Contain("value"));
            Assert.That(new EnumerationEntry("X", "t", "n", "n", null).Problem, Does.Contain("differ"));
            Assert.That(new EnumerationEntry("", "t", "n", "v", null).Problem, Does.Contain("name"));
            Assert.That(new EnumerationEntry("Status", "t", "n", "v", "g").Problem, Does.Contain("{GroupField}"));
            Assert.That(new EnumerationEntry("{GroupField}Type", "t", "n", "v", "g").IsValid, Is.True);
        }
    }
}
