using System.Collections.Generic;
using Efrpg;
using Efrpg.FileManagement;
using Efrpg.Filtering;
using Efrpg.Templates;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Integration
{
    [TestFixture]
    [NonParallelizable]
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

            Settings.ConnectionString =
                $"Data Source=(local);Initial Catalog={database};Integrated Security=True;Encrypt=false;TrustServerCertificate=true;Application Name=Generator";
            Settings.DatabaseType = DatabaseType.SqlServer;
        }

        [Test]
        // Legacy
        [TestCase("EfrpgTest", ".V3TestE1", "MyDbContext", "EfrpgTestDbContext", TemplateType.Ef6, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("EfrpgTest", ".V3TestE2", "MyDbContext", "EfrpgTestDbContext", TemplateType.EfCore2, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("EfrpgTest", ".V3TestE3", "MyDbContext", "EfrpgTestDbContext", TemplateType.EfCore3, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("EfrpgTest", ".V3TestE5", "MyDbContext", "EfrpgTestDbContext", TemplateType.EfCore5, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("EfrpgTest", ".V3TestE6", "MyDbContext", "EfrpgTestDbContext", TemplateType.EfCore6, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("EfrpgTest", ".V3TestE7", "MyDbContext", "EfrpgTestDbContext", TemplateType.EfCore7, ForeignKeyNamingStrategy.Legacy)]
        public void ReverseEngineerSqlServer(string database, string singleDbContextSubNamespace, string connectionStringName, string dbContextName,
            TemplateType templateType, ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            // Arrange
            SetupSqlServer(database, connectionStringName, dbContextName, templateType,
                templateType == TemplateType.Ef6 ? GeneratorType.Ef6 : GeneratorType.EfCore, foreignKeyNamingStrategy);
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = templateType != TemplateType.EfCore2 && templateType != TemplateType.EfCore3;
            if (templateType == TemplateType.EfCore5) // Don't do all, as we want a mix of true/false for this field.
                Settings.TrimCharFields = true;
            else
                Settings.TrimCharFields = false;

            Settings.Enumerations = new List<EnumerationSettings>
            {
                new EnumerationSettings
                {
                    Name = "DaysOfWeek", // Enum to generate. e.g. "DaysOfWeek" would result in "public enum DaysOfWeek {...}"
                    Table = "EnumTest.DaysOfWeek", // Database table containing enum values. e.g. "DaysOfWeek"
                    NameField = "TypeName", // Column containing the name for the enum. e.g. "TypeName"
                    ValueField = "TypeId" // Column containing the values for the enum. e.g. "TypeId"
                },
                new EnumerationSettings
                {
                    Name = "Invalid",
                    Table = "x",
                    NameField = "y",
                    ValueField = "z"
                },
                new EnumerationSettings
                {
                    Name = "CarOptions",
                    Table = "EnumsWithStringAsValue",
                    NameField = "enum_name",
                    ValueField = "value"
                }
            };

            var enumDefinitions = new List<EnumDefinition>
            {
                new EnumDefinition { Schema = "EnumTest", Table = "OpenDays", Column = "TypeId", EnumType = "DaysOfWeek" }
            };

            // Act
            Run(database, singleDbContextSubNamespace, typeof(NullFileManager), null, enumDefinitions);

            // Assert
            CompareAgainstTestComparison(database);
        }

        [Test]
        [TestCase(TemplateType.EfCore6, ".V4TestE6")]
        [TestCase(TemplateType.EfCore7, ".V4TestE7")]
        public void NonPascalCased(TemplateType templateType, string singleDbContextSubNamespace)
        {
            // Arrange
            SetupSqlServer("EfrpgTest", "My_db_context", "Efrpg_db_context", templateType, GeneratorType.EfCore, ForeignKeyNamingStrategy.Legacy);
            Settings.GenerateSeparateFiles = false;
            Settings.UsePascalCase = false;

            // Act
            const string filename = "NonPascalCased";
            Run(filename, singleDbContextSubNamespace, typeof(NullFileManager), null);

            // Assert
            CompareAgainstTestComparison(filename);
        }

        [Test]
        [TestCase("EfrpgTest", ".V3FilterTest", "EfrpgTest", "EfrpgDbContext", false, TemplateType.EfCore3, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("EfrpgTest", ".V5FilterTest", "EfrpgTest", "EfrpgDbContext", false, TemplateType.EfCore5, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("EfrpgTest", ".V6FilterTest", "EfrpgTest", "EfrpgDbContext", false, TemplateType.EfCore6, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("EfrpgTest", ".V7FilterTest", "EfrpgTest", "EfrpgDbContext", false, TemplateType.EfCore7, ForeignKeyNamingStrategy.Legacy)]
        public void MultipleIncludeFilters(string database, string singleDbContextSubNamespace, string connectionStringName, string dbContextName,
            bool publicTestComparison, TemplateType templateType, ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            // Arrange
            SetupSqlServer(database, connectionStringName, dbContextName, templateType,
                templateType == TemplateType.Ef6 ? GeneratorType.Ef6 : GeneratorType.EfCore, foreignKeyNamingStrategy);
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = templateType != TemplateType.EfCore2 && templateType != TemplateType.EfCore3;
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