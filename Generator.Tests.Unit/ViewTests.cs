namespace Generator.Tests.Unit
{
    using Common;
    using Efrpg;
    using Efrpg.FileManagement;
    using Efrpg.Filtering;
    using Efrpg.Generators;
    using Efrpg.Readers;
    using Efrpg.Templates;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture, NonParallelizable]
    [Category(Constants.CI)]
    public class ViewTests
    {
        private List<RawTable> _rawTables;
        private GeneratorCustom _sut;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var fileManagement = new FileManagementService(new GeneratedTextTransformation());
            var databaseReader = new FakeDatabaseReader();

            _rawTables = new List<RawTable>();
            _sut = new GeneratorCustom(fileManagement, typeof(NullFileManager));
            _sut.Init(databaseReader, string.Empty);
        }

        [Test]
        [TestCase("a.b.c")]
        [TestCase("a b c")]
        public void InvalidSchema(string schema)
        {
            _rawTables.Add(new RawTable(schema, "test", false, false, 0, "int", false, 1, 1, 1, false, false, false, 0, false, 1, true, false, null, 1, "name", "123"));

            _sut.LoadTables();
        }

        [Test]
        [TestCase("view.with.multiple.periods")]
        [TestCase("view with multiple spaces")]
        public void InvalidName(string name)
        {
            _rawTables.Add(new RawTable("dbo", name, false, false, 0, "int", false, 1, 1, 1, false, false, false, 0, false, 1, true, false, null, 1, "name", "123"));
            _sut.LoadTables();
        }

        // Issue #654: TableRename of a view must generate ToTable (or [Table]) using the original DB name,
        // not the renamed C# class name. Without this, EF cannot find the underlying view.
        [Test]
        [TestCase(TemplateType.Ef6,     false)] // UseDataAnnotations = false -> ToTable in configuration
        [TestCase(TemplateType.Ef6,     true)]  // UseDataAnnotations = true  -> [Table] attribute on POCO
        [TestCase(TemplateType.EfCore8, false)]
        [TestCase(TemplateType.EfCore8, true)]
        public void RenamedViewUsesOriginalDbNameInMapping(TemplateType templateType, bool useDataAnnotations)
        {
            // Arrange
            Settings.TemplateType         = templateType;
            Settings.GeneratorType        = templateType == TemplateType.Ef6 ? GeneratorType.Ef6 : GeneratorType.EfCore;
            Settings.UseDataAnnotations   = useDataAnnotations;
            Settings.ElementsToGenerate   = Elements.Poco | Elements.PocoConfiguration;

            FilterSettings.Reset();
            FilterSettings.AddDefaults();
            FilterSettings.CheckSettings();

            var fileManagement = new FileManagementService(new GeneratedTextTransformation());
            var generator = templateType == TemplateType.Ef6
                ? (Generator) new GeneratorEf6(fileManagement, typeof(NullFileManager))
                : (Generator) new GeneratorEfCore(fileManagement, typeof(NullFileManager));

            // Create a view with original DB name "vwSiteNotification", renamed to "SiteNotificationView"
            var schema = new Schema("dbo");
            var view = new Table(null, schema, "vwSiteNotification", true)
            {
                NameHumanCase = "SiteNotificationView"
            };

            var pkCol = new Column
            {
                DbName        = "SiteId",
                NameHumanCase = "SiteId",
                IsPrimaryKey  = true,
                PropertyType  = "int",
                Config        = "builder.Property(x => x.SiteId).IsRequired();",
                ParentTable   = view
            };
            view.Columns.Add(pkCol);
            view.SetPrimaryKeys();

            var filter = new SingleContextFilter();
            filter.Tables.Add(view);

            var codeGen = new CodeGenerator(generator, filter);

            if (useDataAnnotations)
            {
                // Act
                var output = codeGen.GeneratePoco(view);

                // Assert: [Table("vwSiteNotification"...)] must appear on the POCO, not the renamed class name
                Assert.IsNotNull(output, "GeneratePoco returned null");
                var code = string.Join(Environment.NewLine, output.Code);
                StringAssert.Contains("[Table(\"vwSiteNotification\",",   code);
                StringAssert.DoesNotContain("[Table(\"SiteNotificationView\"", code);
            }
            else
            {
                // Act
                var output = codeGen.GeneratePocoConfiguration(view);

                // Assert: ToTable("vwSiteNotification"...) must appear, not ToTable("SiteNotificationView"...)
                Assert.IsNotNull(output, "GeneratePocoConfiguration returned null");
                var code = string.Join(Environment.NewLine, output.Code);
                StringAssert.Contains("\"vwSiteNotification\"", code);
                StringAssert.DoesNotContain("\"SiteNotificationView\"", code);
            }
        }

        // TODO add TestCases above for No primary key, with invalid schema & invalid name
    }
}