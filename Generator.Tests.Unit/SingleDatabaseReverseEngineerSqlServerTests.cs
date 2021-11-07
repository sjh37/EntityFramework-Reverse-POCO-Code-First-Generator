using Efrpg;
using Efrpg.FileManagement;
using Efrpg.Filtering;
using Efrpg.Templates;
using NUnit.Framework;
using System.Collections.Generic;

namespace Generator.Tests.Unit
{
    [TestFixture, NonParallelizable]
    [Category(Constants.DbType.SqlServer)]
    public class SingleDatabaseReverseEngineerSqlServerTests : ReverseEngineerShared
    {


        public void SetSettings(string database, string connectionStringName, string dbContextName, TemplateType templateType, GeneratorType generatorType, ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            connectionStringName ??= "Default";
           
            Settings.ForeignKeyNamingStrategy   = foreignKeyNamingStrategy;
            Settings.TemplateType               = templateType;
            Settings.GeneratorType              = generatorType;
            Settings.ConnectionString           = ConfigurationExtensions.GetConnectionString(connectionStringName, database);
            Settings.DatabaseType               = DatabaseType.SqlServer;
            Settings.ConnectionStringName       = connectionStringName;
            Settings.DbContextName              = dbContextName;
            Settings.GenerateSingleDbContext    = true;
            Settings.MultiContextSettingsPlugin = null;
            Settings.Enumerations               = null;
            Settings.PrependSchemaName          = true;
            Settings.DisableGeographyTypes      = false;
            Settings.AddUnitTestingDbContext    = true;

            FilterSettings.Reset();
            FilterSettings.AddDefaults();
            FilterSettings.CheckSettings();
        }



        [Test, NonParallelizable]
        // Legacy
        [TestCase("Northwind",      ".V3TestA", "MyDbContext",      "MyDbContext",              true,  TemplateType.Ef6,     ForeignKeyNamingStrategy.Legacy)]
        [TestCase("EfrpgTest",      ".V3TestB", "MyDbContext",      "EfrpgTestDbContext",       false, TemplateType.Ef6,     ForeignKeyNamingStrategy.Legacy)]
        [TestCase("EfrpgTestLarge", ".V3TestC", "MyLargeDbContext", "EfrpgTestLargeDbContext",  false, TemplateType.Ef6,     ForeignKeyNamingStrategy.Legacy, IgnoreReason = "WIP unknown db (not in scripts)")]
        [TestCase("fred",           ".V3TestD", "Default",          "FredDbContext",            false, TemplateType.Ef6,     ForeignKeyNamingStrategy.Legacy, IgnoreReason = "WIP unknown db (not in scripts)")]
        [TestCase("Northwind",      ".V3TestE", "MyDbContext",      "MyDbContext",              true,  TemplateType.EfCore2, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("Northwind",      ".V3TestK", "MyDbContext",      "MyDbContext",              true,  TemplateType.EfCore3, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("EfrpgTest",      ".V3TestF", "MyDbContext",      "EfrpgTestDbContext",       false, TemplateType.EfCore2, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("EfrpgTest",      ".V3TestG", "MyDbContext",      "EfrpgTestDbContext",       false, TemplateType.EfCore3, ForeignKeyNamingStrategy.Legacy)] // ef core 3
        [TestCase("EfrpgTestLarge", ".V3TestH", "Default",          "EfrpgTestLargeDbContext",  false, TemplateType.EfCore2, ForeignKeyNamingStrategy.Legacy, IgnoreReason = "WIP unknown db (not in scripts)")]
        [TestCase("fred",           ".V3TestI", "Default",          "FredDbContext",            false, TemplateType.EfCore2, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("fred",           ".V3TestJ", "Default",          "FredDbContext",            false, TemplateType.EfCore3, ForeignKeyNamingStrategy.Legacy)] // ef core 3
        [TestCase("fred",           ".V3TestK", "Default",          "FredDbContext",            false, TemplateType.EfCore5, ForeignKeyNamingStrategy.Legacy)] // ef core 5
        // Latest
        [TestCase("Northwind",      ".V3TestA2", "MyDbContext",      "MyDbContext",             true,  TemplateType.Ef6,     ForeignKeyNamingStrategy.Latest, IgnoreReason = "(document why disabled)")]
        [TestCase("EfrpgTest",      ".V3TestB2", "MyDbContext",      "EfrpgTestDbContext",      false, TemplateType.Ef6,     ForeignKeyNamingStrategy.Latest, IgnoreReason = "(document why disabled)")]
        [TestCase("EfrpgTestLarge", ".V3TestC2", "MyLargeDbContext", "EfrpgTestLargeDbContext", false, TemplateType.Ef6,     ForeignKeyNamingStrategy.Latest, IgnoreReason = "(document why disabled)")]
        [TestCase("fred",           ".V3TestD2", "fred",             "FredDbContext",           false, TemplateType.Ef6,     ForeignKeyNamingStrategy.Latest, IgnoreReason = "(document why disabled)")]
        [TestCase("Northwind",      ".V3TestE2", "MyDbContext",      "MyDbContext",             true,  TemplateType.EfCore2, ForeignKeyNamingStrategy.Latest, IgnoreReason = "(document why disabled)")]
        [TestCase("Northwind",      ".V3TestK2", "MyDbContext",      "MyDbContext",             true,  TemplateType.EfCore3, ForeignKeyNamingStrategy.Latest, IgnoreReason = "(document why disabled)")]
        [TestCase("EfrpgTest",      ".V3TestF2", "MyDbContext",      "EfrpgTestDbContext",      false, TemplateType.EfCore2, ForeignKeyNamingStrategy.Latest, IgnoreReason = "(document why disabled)")]
        [TestCase("EfrpgTest",      ".V3TestG2", "MyDbContext",      "EfrpgTestDbContext",      false, TemplateType.EfCore3, ForeignKeyNamingStrategy.Latest, IgnoreReason = "(document why disabled)")] // ef core 3
        [TestCase("EfrpgTestLarge", ".V3TestH2", "MyLargeDbContext", "EfrpgTestLargeDbContext", false, TemplateType.EfCore2, ForeignKeyNamingStrategy.Latest, IgnoreReason = "(document why disabled)")]
        [TestCase("fred",           ".V3TestI2", "fred",             "FredDbContext",           false, TemplateType.EfCore2, ForeignKeyNamingStrategy.Latest, IgnoreReason = "(document why disabled)")]
        [TestCase("fred",           ".V3TestJ2", "fred",             "FredDbContext",           false, TemplateType.EfCore3, ForeignKeyNamingStrategy.Latest, IgnoreReason = "(document why disabled)")] // ef core 3

        public void ReverseEngineer(string database, string singleDbContextSubNamespace, string connectionStringName, string dbContextName, bool publicTestComparison, TemplateType templateType, ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            // Arrange
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = (templateType != TemplateType.EfCore2 && templateType != TemplateType.EfCore3);
            SetSettings(database, connectionStringName, dbContextName, templateType, templateType == TemplateType.Ef6 ? GeneratorType.Ef6 : GeneratorType.EfCore, foreignKeyNamingStrategy);
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
            CompareAgainstTestComparison(database, publicTestComparison);
        }
        
        [Test, NonParallelizable]
        [TestCase("fred", ".V3FilterTest1", "fred", "FredDbContext", false, TemplateType.EfCore3, ForeignKeyNamingStrategy.Legacy, IgnoreReason = "WIP ?")] // ef core 3
        [TestCase("fred", ".V3FilterTest1", "fred", "FredDbContext", false, TemplateType.EfCore3, ForeignKeyNamingStrategy.Latest, IgnoreReason = "WIP ?")] // ef core 3
        public void MultipleIncludeFilters(string database, string singleDbContextSubNamespace, string connectionStringName, string dbContextName, bool publicTestComparison, TemplateType templateType, ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            // Arrange
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = (templateType != TemplateType.EfCore2 && templateType != TemplateType.EfCore3);
            SetSettings(database, connectionStringName, dbContextName, templateType, templateType == TemplateType.Ef6 ? GeneratorType.Ef6 : GeneratorType.EfCore, foreignKeyNamingStrategy);
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
            CompareAgainstTestComparison(filename, publicTestComparison);
        }
    }
}