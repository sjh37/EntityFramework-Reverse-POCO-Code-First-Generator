using Efrpg;
using Efrpg.FileManagement;
using Efrpg.Filtering;
using Efrpg.Templates;
using NUnit.Framework;
using System.Data.Common;
using System.IO;

namespace Generator.Tests.Unit
{
    [TestFixture, NonParallelizable]
    [Category(Constants.DbType.SqlCe)]
    public class SingleDatabaseReverseEngineerSqlCeTests : ReverseEngineerShared
    {
        private const string NorthwindSqlCeSdfFile = @".\App_Data\NorthwindSqlCe40.sdf";

        public void SetSettings(string database, string connectionStringName, string dbContextName, TemplateType templateType, GeneratorType generatorType, ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            var fi = new FileInfo(database);

            var builder = new DbConnectionStringBuilder
            {
                ["Data Source"] = fi.FullName
            };

            Settings.ForeignKeyNamingStrategy   = foreignKeyNamingStrategy;
            Settings.TemplateType               = templateType;
            Settings.GeneratorType              = generatorType;
            Settings.ConnectionString           = builder.ConnectionString;
            Settings.DatabaseType               = DatabaseType.SqlCe;
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
            var fi = NorthwindSqlCeFile();

            var comparisonFile = GeneratedFile(database);
            
            var content = File.ReadAllText(comparisonFile.FullName).Replace(fi.FullName, NorthwindSqlCeSdfFile);

            File.WriteAllText(comparisonFile.FullName, content);
            
            base.CompareAgainstTestComparison(database, publicTestComparison);
        }

        [Test, NonParallelizable]
        [TestCase(ForeignKeyNamingStrategy.Legacy)]
        [TestCase(ForeignKeyNamingStrategy.Latest, IgnoreReason = "WIP")]
        [Category(Constants.DbType.SqlCe)]
        public void ReverseEngineer(ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            // Arrange
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = true;

            SetSettings(NorthwindSqlCeSdfFile, "MyDbContext", "MyDbContext", TemplateType.Ef6, GeneratorType.Ef6, foreignKeyNamingStrategy);

            // Act
            var filename = "Northwind";
            Run(filename, ".SqlCE", typeof(NullFileManager));

            // Assert
            CompareAgainstTestComparison(filename, true);
        }

        [Test, NonParallelizable]
        // Legacy
        [TestCase(false, TemplateType.EfCore2, ForeignKeyNamingStrategy.Legacy)]
        [TestCase(false, TemplateType.EfCore3, ForeignKeyNamingStrategy.Legacy)]
        [TestCase(true, TemplateType.EfCore2, ForeignKeyNamingStrategy.Legacy, IgnoreReason = @"Artifact files should be in project (TestComparison\EfCore2NorthwindSqlCe40)")]
        [TestCase(true, TemplateType.EfCore3, ForeignKeyNamingStrategy.Legacy, IgnoreReason = @"Artifact files should be in project (TestComparison\EfCore3NorthwindSqlCe40)")]
        // Latest
        [TestCase(false, TemplateType.EfCore2, ForeignKeyNamingStrategy.Latest, IgnoreReason = "WIP")]
        [TestCase(false, TemplateType.EfCore3, ForeignKeyNamingStrategy.Latest, IgnoreReason = "WIP")]
        [TestCase(true, TemplateType.EfCore2, ForeignKeyNamingStrategy.Latest, IgnoreReason = "WIP")]
        [TestCase(true, TemplateType.EfCore3, ForeignKeyNamingStrategy.Latest, IgnoreReason = "WIP")]
        [Category(Constants.DbType.SqlCe)]
        public void ReverseEngineer(bool separateFiles, TemplateType templateType, ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            // Arrange
            Settings.GenerateSeparateFiles = separateFiles;
            Settings.UseMappingTables = false;
            SetSettings(NorthwindSqlCeSdfFile, "MyDbContext", "MyDbContext", templateType, GeneratorType.EfCore, foreignKeyNamingStrategy);

            // Act
            var filename = "Northwind";
            var subFolder = $@"{templateType}NorthwindSqlCe40";

            Run(filename, ".SqlCE", typeof(CustomFileManager), subFolder);

            // Assert
            if(separateFiles)
                CompareAgainstFolderTestComparison(subFolder);
            else
                CompareAgainstTestComparison(filename);
        }

        private FileInfo NorthwindSqlCeFile()
        {
            //from the current .\bin folder

            var fi = new FileInfo($@".\App_Data\NorthwindSqlCe40.sdf")
                .Then(_ =>
                {
                    if (!_.Exists)
                    {
                        throw new FileNotFoundException($"Could find file. [{_.FullName}]", _.FullName);
                    }
                });

            //Console.WriteLine($">>   {fi.FullName}");

            return fi;
        }

        public static FileInfo GeneratedFile(string database)
        {
            var comparisonFile = FileGenerator.GetFileOnSettings(database);
            
            return new FileInfo(Path.Combine(Settings.Root, comparisonFile));
        }
    }
}