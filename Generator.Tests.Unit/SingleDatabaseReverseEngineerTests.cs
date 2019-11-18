using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Efrpg;
using Efrpg.FileManagement;
using Efrpg.Filtering;
using Efrpg.Generators;
using Efrpg.Pluralization;
using Efrpg.Templates;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture, NonParallelizable]
    public class SingleDatabaseReverseEngineerTests
    {
        public void SetupSqlServer(string database, string connectionStringName, string dbContextName, TemplateType templateType, GeneratorType generatorType)
        {
            Settings.TemplateType               = templateType;
            Settings.GeneratorType              = generatorType;
            Settings.ConnectionString           = $"Data Source=(local);Initial Catalog={database};Integrated Security=True;Application Name=Generator";
            Settings.DatabaseType               = DatabaseType.SqlServer;
            Settings.ConnectionStringName       = connectionStringName;
            Settings.DbContextName              = dbContextName;
            Settings.GenerateSingleDbContext    = true;
            Settings.MultiContextSettingsPlugin = null;
            Settings.Enumerations               = null;
            Settings.PrependSchemaName          = true;
        }

        public void SetupSqlCe(string database, string connectionStringName, string dbContextName, TemplateType templateType, GeneratorType generatorType)
        {
            Settings.TemplateType               = templateType;
            Settings.GeneratorType              = generatorType;
            Settings.ConnectionString           = @"Data Source=C:\S\ReversePocoWebsite\Generator\EntityFramework.Reverse.POCO.Generator\App_Data\" + database;
            Settings.DatabaseType               = DatabaseType.SqlCe;
            Settings.ConnectionStringName       = connectionStringName;
            Settings.DbContextName              = dbContextName;
            Settings.GenerateSingleDbContext    = true;
            Settings.MultiContextSettingsPlugin = null;
            Settings.Enumerations               = null;
            Settings.PrependSchemaName          = true;
        }

        public void Run(string filename, string singleDbContextSubNamespace, Type fileManagerType, string subFolder)
        {
            Inflector.PluralisationService   = new EnglishPluralizationService();
            Settings.GenerateSingleDbContext = true;

            FilterSettings.Reset();
            FilterSettings.AddDefaults();
            FilterSettings.CheckSettings();

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (!string.IsNullOrEmpty(subFolder))
                path = Path.Combine(path, subFolder);
            Settings.Root = path;
            var fullPath = Path.Combine(path, $"{filename}_{Settings.DatabaseType}_{Settings.TemplateType}.cs");
            
            // Delete old generated files
            if (File.Exists(fullPath))
                File.Delete(fullPath);
            if (!string.IsNullOrEmpty(subFolder))
            {
                foreach (var old in Directory.GetFiles(Settings.Root))
                    File.Delete(old);
            }

            var outer          = new GeneratedTextTransformation();
            var fileManagement = new FileManagementService(outer);
            var generator      = GeneratorFactory.Create(fileManagement, fileManagerType, singleDbContextSubNamespace);

            // Turn on everything for testing
            foreach (var filter in generator.FilterList.GetFilters())
            {
                filter.Value.IncludeViews                 = true;
                filter.Value.IncludeSynonyms              = true;
                filter.Value.IncludeStoredProcedures      = true;
                filter.Value.IncludeTableValuedFunctions  = true;
                filter.Value.IncludeScalarValuedFunctions = true;
            }

            var stopwatch          = new Stopwatch();
            var stopwatchGenerator = new Stopwatch();

            stopwatch.Start();
            generator.ReadDatabase();

            stopwatchGenerator.Start();
            generator.GenerateCode();
            stopwatchGenerator.Stop();

            stopwatch.Stop();

            Console.WriteLine("Duration: {0:F1} seconds, Generator {1:F1} seconds", stopwatch.ElapsedMilliseconds / 1000.0, stopwatchGenerator.ElapsedMilliseconds / 1000.0);
            Console.WriteLine($"Writing to {fullPath}");
            Console.WriteLine();

            if (outer.FileData.Length > 0)
            {
                using (var sw = new StreamWriter(fullPath))
                {
                    sw.Write(outer.FileData.ToString());
                }
            }

            fileManagement.Process(true);
        }

        [Test, NonParallelizable]
        [TestCase("Northwind",      ".V3TestA", "MyDbContext",      "MyDbContext",             true,  TemplateType.Ef6)]
        [TestCase("EfrpgTest",      ".V3TestB", "MyDbContext",      "EfrpgTestDbContext",      false, TemplateType.Ef6)]
        [TestCase("EfrpgTestLarge", ".V3TestC", "MyLargeDbContext", "EfrpgTestLargeDbContext", false, TemplateType.Ef6)]
        [TestCase("fred",           ".V3TestD", "fred",             "FredDbContext",           false, TemplateType.Ef6)]
        [TestCase("Northwind",      ".V3TestE", "MyDbContext",      "MyDbContext",             true,  TemplateType.EfCore2)]
        [TestCase("EfrpgTest",      ".V3TestF", "MyDbContext",      "EfrpgTestDbContext",      false, TemplateType.EfCore2)]
        [TestCase("EfrpgTest",      ".V3TestG", "MyDbContext",      "EfrpgTestDbContext",      false, TemplateType.EfCore3)] // ef core 3
        [TestCase("EfrpgTestLarge", ".V3TestH", "MyLargeDbContext", "EfrpgTestLargeDbContext", false, TemplateType.EfCore2)]
        [TestCase("fred",           ".V3TestI", "fred",             "FredDbContext",           false, TemplateType.EfCore2)]
        [TestCase("fred",           ".V3TestJ", "fred",             "FredDbContext",           false, TemplateType.EfCore3)] // ef core 3
        public void ReverseEngineerSqlServer(string database, string singleDbContextSubNamespace, string connectionStringName, string dbContextName, bool publicTestComparison, TemplateType templateType)
        {
            // Arrange
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = (templateType != TemplateType.EfCore2 && templateType != TemplateType.EfCore3);
            SetupSqlServer(database, connectionStringName, dbContextName, templateType, templateType == TemplateType.Ef6 ? GeneratorType.Ef6 : GeneratorType.EfCore);

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
        public void ReverseEngineerSqlCe()
        {
            // Arrange
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = true;
            SetupSqlCe("NorthwindSqlCe40.sdf", "MyDbContext", "MyDbContext", TemplateType.Ef6, GeneratorType.Ef6);

            // Act
            var filename = "Northwind";
            Run(filename, ".SqlCE", typeof(NullFileManager), null);

            // Assert
            CompareAgainstTestComparison(filename, true);
        }

        [Test, NonParallelizable]
        [TestCase(false, TemplateType.EfCore2)]
        [TestCase(false, TemplateType.EfCore3)]
        [TestCase(true, TemplateType.EfCore2)]
        [TestCase(true, TemplateType.EfCore3)]
        public void ReverseEngineerSqlCe_EfCore(bool separateFiles, TemplateType templateType)
        {
            // Arrange
            Settings.GenerateSeparateFiles = separateFiles;
            Settings.UseMappingTables = false;
            SetupSqlCe("NorthwindSqlCe40.sdf", "MyDbContext", "MyDbContext", templateType, GeneratorType.EfCore);

            // Act
            var filename = "Northwind";
            var subFolder = templateType == TemplateType.EfCore2 ? "TestComparison\\EfCore2NorthwindSqlCe40" : "TestComparison\\EfCore3NorthwindSqlCe40";
            Run(filename, ".SqlCE", typeof(CustomFileManager), subFolder);

            // Assert
            if(separateFiles)
                CompareAgainstFolderTestComparison(subFolder);
            else
                CompareAgainstTestComparison(filename, true);
        }

        private static void CompareAgainstFolderTestComparison(string subFolder)
        {
            var testRootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "OneDrive\\Documents");
            if (!string.IsNullOrEmpty(subFolder))
                testRootPath = Path.Combine(testRootPath, subFolder);

            Console.WriteLine("Reading from: " + testRootPath);
            Console.WriteLine();

            var testComparisonFiles = Directory.GetFiles(testRootPath);
            var generatedFiles = Directory.GetFiles(Settings.Root);

            Assert.AreEqual(testComparisonFiles.Length, generatedFiles.Length);

            foreach (var comparisonFile in testComparisonFiles)
            {
                var filename       = Path.GetFileName(comparisonFile);
                var generatedPath  = Path.Combine(Settings.Root, filename);
                var testComparison = File.ReadAllText(comparisonFile);
                var generated      = File.ReadAllText(generatedPath);

                Console.WriteLine(comparisonFile);
                Console.WriteLine(generatedPath);
                Console.WriteLine();

                Assert.AreEqual(testComparison, generated);
            }
        }

        private static void CompareAgainstTestComparison(string database, bool publicTestComparison)
        {
            var comparisonFile     = $"{database}_{Settings.DatabaseType}_{Settings.TemplateType}.cs";
            var testRootPath       = publicTestComparison ? AppDomain.CurrentDomain.BaseDirectory : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "OneDrive\\Documents");
            var testComparisonPath = Path.Combine(testRootPath, $"TestComparison\\{comparisonFile}");
            var testComparison     = File.ReadAllText(testComparisonPath);
            var generatedPath      = Path.Combine(Settings.Root, comparisonFile);
            var generated          = File.ReadAllText(generatedPath);

            Console.WriteLine(testComparisonPath);
            Console.WriteLine(generatedPath);

            Assert.AreEqual(testComparison, generated);
        }
    }
}