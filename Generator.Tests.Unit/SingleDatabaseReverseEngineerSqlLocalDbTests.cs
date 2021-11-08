using Efrpg;
using Efrpg.FileManagement;
using Efrpg.Filtering;
using Efrpg.Templates;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace Generator.Tests.Unit
{
    [TestFixture, NonParallelizable]
    [Category(Constants.DbType.SqlLocalDb)]
    [Ignore(@"Use only if you have (localdb)\MSSQLLocalDB installed")]
    public class SingleDatabaseReverseEngineerSqlLocalDbTests : ReverseEngineerShared
    {
        public void SetSettings(string database, string connectionStringName, string dbContextName, TemplateType templateType, GeneratorType generatorType, ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            connectionStringName ??= "Default";
           
            Settings.ForeignKeyNamingStrategy   = foreignKeyNamingStrategy;
            Settings.TemplateType               = templateType;
            Settings.GeneratorType              = generatorType;
            Settings.ConnectionString           = ConfigurationExtensions.GetConnectionString(connectionStringName, database, @"(localdb)\MSSQLLocalDB");
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

        protected override void CompareAgainstTestComparison(string database, bool publicTestComparison = false)
        {
            var comparisonFile = GeneratedFile(database);
            
            var content = File.ReadAllText(comparisonFile.FullName).Replace(@"Data Source=(localdb)\MSSQLLocalDB", @"Data Source=(local)");

            File.WriteAllText(comparisonFile.FullName, content);
            
            base.CompareAgainstTestComparison(database, publicTestComparison);
        }

        public static FileInfo GeneratedFile(string database)
        {
            var comparisonFile = FileGenerator.GetFileOnSettings(database);
            
            return new FileInfo(Path.Combine(Settings.Root, comparisonFile));
        }

        [Test, NonParallelizable]
        // Legacy
        [TestCase("Northwind",     ".V3TestA", "MyDbContext",  "MyDbContext", true,  TemplateType.Ef6,     ForeignKeyNamingStrategy.Legacy)]
        [TestCase("Northwind",     ".V3TestE", "MyDbContext",  "MyDbContext", true,  TemplateType.EfCore2, ForeignKeyNamingStrategy.Legacy)]
        [TestCase("Northwind",     ".V3TestK", "MyDbContext",  "MyDbContext", true,  TemplateType.EfCore3, ForeignKeyNamingStrategy.Legacy)]
        // Latest
        ////[TestCase("Northwind", ".V3TestA2", "MyDbContext", "MyDbContext", true,  TemplateType.Ef6,     ForeignKeyNamingStrategy.Latest, IgnoreReason = "(document why disabled)")]
        ////[TestCase("Northwind", ".V3TestE2", "MyDbContext", "MyDbContext", true,  TemplateType.EfCore2, ForeignKeyNamingStrategy.Latest, IgnoreReason = "(document why disabled)")]
        ////[TestCase("Northwind", ".V3TestK2", "MyDbContext", "MyDbContext", true,  TemplateType.EfCore3, ForeignKeyNamingStrategy.Latest, IgnoreReason = "(document why disabled)")]
        public void ReverseEngineer(string database, string singleDbContextSubNamespace, string connectionStringName, string dbContextName, bool publicTestComparison, TemplateType templateType, ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            // Arrange
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = (templateType != TemplateType.EfCore2 && templateType != TemplateType.EfCore3);
            SetSettings(database, connectionStringName, dbContextName, templateType, templateType == TemplateType.Ef6 ? GeneratorType.Ef6 : GeneratorType.EfCore, foreignKeyNamingStrategy);
            
            // Don't do all, as we want a mix of true/false for this field.
            Settings.TrimCharFields = templateType == TemplateType.EfCore5;

            Settings.Enumerations = new List<EnumerationSettings>
            {
                new()
                {
                    Name       = "DaysOfWeek",          // Enum to generate. e.g. "DaysOfWeek" would result in "public enum DaysOfWeek {...}"
                    Table      = "EnumTest.DaysOfWeek", // Database table containing enum values. e.g. "DaysOfWeek"
                    NameField  = "TypeName",            // Column containing the name for the enum. e.g. "TypeName"
                    ValueField = "TypeId"               // Column containing the values for the enum. e.g. "TypeId"
                },
                new()
                {
                    Name       = "Invalid",
                    Table      = "x",
                    NameField  = "y",
                    ValueField = "z"
                },
                new()
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
    }
}