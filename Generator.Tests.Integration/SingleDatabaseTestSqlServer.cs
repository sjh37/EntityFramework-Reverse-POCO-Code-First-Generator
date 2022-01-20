namespace Generator.Tests.Integration
{
    using System.Collections.Generic;
    using Efrpg;
    using Efrpg.FileManagement;
    using Efrpg.Filtering;
    using Efrpg.Templates;
    using Generator.Tests.Common;
    using NUnit.Framework;

    [TestFixture, NonParallelizable]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SqlServer)]
    public class SingleDatabaseTestSqlServer : SingleDatabaseTestBase
    {
        public void SetupSqlServer(
            string database,
            string connectionStringName,
            string dbContextName,
            TemplateType templateType,
            GeneratorType generatorType,
            ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            SetupDatabase(connectionStringName, dbContextName, templateType, generatorType, foreignKeyNamingStrategy);

            Settings.ConnectionString = $"Data Source=(local);Initial Catalog={database};Integrated Security=True;Application Name=Generator";
            Settings.DatabaseType     = DatabaseType.SqlServer;
        }

        [Test, NonParallelizable]
        // Legacy
        [TestCase("EfrpgTest",      ".V3TestE1",   "MyDbContext",      "EfrpgTestDbContext",      TemplateType.Ef6,     ForeignKeyNamingStrategy.Legacy)]
        [TestCase("EfrpgTest",      ".V3TestE2",   "MyDbContext",      "EfrpgTestDbContext",      TemplateType.EfCore2, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("EfrpgTest",      ".V3TestE3",   "MyDbContext",      "EfrpgTestDbContext",      TemplateType.EfCore3, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("EfrpgTest",      ".V3TestE5",   "MyDbContext",      "EfrpgTestDbContext",      TemplateType.EfCore5, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("EfrpgTest",      ".V3TestE6",   "MyDbContext",      "EfrpgTestDbContext",      TemplateType.EfCore6, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("Northwind",      ".V3TestN1",   "MyDbContext",      "MyDbContext",             TemplateType.Ef6,     ForeignKeyNamingStrategy.Legacy)]
        [TestCase("Northwind",      ".V3TestN2",   "MyDbContext",      "MyDbContext",             TemplateType.EfCore2, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("Northwind",      ".V3TestN3",   "MyDbContext",      "MyDbContext",             TemplateType.EfCore3, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("Northwind",      ".V3TestN5",   "MyDbContext",      "MyDbContext",             TemplateType.EfCore5, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("Northwind",      ".V3TestN6",   "MyDbContext",      "MyDbContext",             TemplateType.EfCore6, ForeignKeyNamingStrategy.Legacy)]
        public void ReverseEngineerSqlServer(string database, string singleDbContextSubNamespace, string connectionStringName, string dbContextName, TemplateType templateType, ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            // Arrange
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = (templateType != TemplateType.EfCore2 && templateType != TemplateType.EfCore3);
            SetupSqlServer(database, connectionStringName, dbContextName, templateType, templateType == TemplateType.Ef6 ? GeneratorType.Ef6 : GeneratorType.EfCore, foreignKeyNamingStrategy);
            if(templateType == TemplateType.EfCore5) // Don't do all, as we want a mix of true/false for this field.
                Settings.TrimCharFields = true;
            else
                Settings.TrimCharFields = false;

            Settings.Enumerations = new List<EnumerationSettings>
            {
                new EnumerationSettings
                {
                    Name       = "DaysOfWeek",          // Enum to generate. e.g. "DaysOfWeek" would result in "public enum DaysOfWeek {...}"
                    Table      = "EnumTest.DaysOfWeek", // Database table containing enum values. e.g. "DaysOfWeek"
                    NameField  = "TypeName",            // Column containing the name for the enum. e.g. "TypeName"
                    ValueField = "TypeId"               // Column containing the values for the enum. e.g. "TypeId"
                },
                new EnumerationSettings
                {
                    Name       = "Invalid",
                    Table      = "x",
                    NameField  = "y",
                    ValueField = "z"
                },
                new EnumerationSettings
                {
                    Name       = "CarOptions",
                    Table      = "EnumsWithStringAsValue",
                    NameField  = "enum_name",
                    ValueField = "value"
                }
            };

            // Act
            Run(database, singleDbContextSubNamespace, typeof(NullFileManager), null);

            // Assert
            CompareAgainstTestComparison(database);
        }

        [Test, NonParallelizable]
        [TestCase("EfrpgTest", ".V3FilterTest", "EfrpgTest", "EfrpgDbContext", false, TemplateType.EfCore3, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("EfrpgTest", ".V5FilterTest", "EfrpgTest", "EfrpgDbContext", false, TemplateType.EfCore5, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("EfrpgTest", ".V6FilterTest", "EfrpgTest", "EfrpgDbContext", false, TemplateType.EfCore6, ForeignKeyNamingStrategy.Legacy)]
        public void MultipleIncludeFilters(string database, string singleDbContextSubNamespace, string connectionStringName, string dbContextName, bool publicTestComparison, TemplateType templateType, ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            // Arrange
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = (templateType != TemplateType.EfCore2 && templateType != TemplateType.EfCore3);
            SetupSqlServer(database, connectionStringName, dbContextName, templateType, templateType == TemplateType.Ef6 ? GeneratorType.Ef6 : GeneratorType.EfCore, foreignKeyNamingStrategy);
            Settings.AddUnitTestingDbContext = false;
            
            FilterSettings.SchemaFilters.Add(new RegexIncludeFilter("dbo.*"));
            FilterSettings.SchemaFilters.Add(new RegexIncludeFilter("Beta.*"));
            
            FilterSettings.TableFilters.Add(new RegexIncludeFilter("^[Cc]ar.*"));
            FilterSettings.TableFilters.Add(new RegexIncludeFilter("Rebel.*"));
            FilterSettings.TableFilters.Add(new RegexIncludeFilter("Harish.*"));

            // Act
            var filename = database + "IncludeFilter";
            Run(filename, singleDbContextSubNamespace, typeof(NullFileManager), null);

            // Assert
            CompareAgainstTestComparison(filename);
        }
    }
}