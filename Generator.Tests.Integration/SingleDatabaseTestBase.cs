using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Efrpg;
using Efrpg.FileManagement;
using Efrpg.Filtering;
using Efrpg.Generators;
using Efrpg.Pluralization;
using Efrpg.Templates;
using NUnit.Framework;

namespace Generator.Tests.Integration
{
    public abstract class SingleDatabaseTestBase
    {
        protected static void SetupDatabase(
            string connectionStringName,
            string dbContextName,
            TemplateType templateType,
            GeneratorType generatorType,
            ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            Settings.ForeignKeyNamingStrategy = foreignKeyNamingStrategy;
            Settings.TemplateType = templateType;
            Settings.GeneratorType = generatorType;
            Settings.ConnectionStringName = connectionStringName;
            Settings.DbContextName = dbContextName;
            Settings.GenerateSingleDbContext = true;
            Settings.MultiContextSettingsPlugin = null;
            Settings.Enumerations = null;
            Settings.PrependSchemaName = true;
            Settings.DisableGeographyTypes = false;
            Settings.AddUnitTestingDbContext = true;
            Settings.UsePascalCase = true;
            Settings.UseMappingTables = false;

            ResetFilters();
        }

        protected static void Run(string filename, string singleDbContextSubNamespace, Type fileManagerType, string subFolder,
            List<EnumDefinition> enumDefinitions = null)
        {
            Inflector.PluralisationService = new EnglishPluralizationService();
            Settings.GenerateSingleDbContext = true;

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (!string.IsNullOrEmpty(subFolder))
                path = Path.Combine(path, subFolder);

            Settings.Root = path;
            var fullPath = Path.Combine(path, $"{filename}_{Settings.DatabaseType}_{Settings.TemplateType}_Fk{Settings.ForeignKeyNamingStrategy}.cs");

            // Delete old generated files
            if (File.Exists(fullPath))
                File.Delete(fullPath);
            if (!string.IsNullOrEmpty(subFolder))
                foreach (var old in Directory.GetFiles(Settings.Root))
                    File.Delete(old);

            var outer = new GeneratedTextTransformation();
            var fileManagement = new FileManagementService(outer);
            var generator = GeneratorFactory.Create(fileManagement, fileManagerType, singleDbContextSubNamespace);

            // Turn on everything for testing
            Assert.IsNotNull(generator);
            Assert.IsNotNull(generator.FilterList);
            var filters = generator.FilterList.GetFilters();
            Assert.IsNotNull(filters);
            foreach (var filter in filters)
            {
                filter.Value.IncludeViews = true;
                filter.Value.IncludeSynonyms = true;
                filter.Value.IncludeStoredProcedures = true;
                filter.Value.IncludeTableValuedFunctions = true;
                filter.Value.IncludeScalarValuedFunctions = true;

                if (filter.Value is SingleContextFilter singleContextFilter)
                    singleContextFilter.EnumDefinitions = enumDefinitions;
            }

            var stopwatch = new Stopwatch();
            var stopwatchGenerator = new Stopwatch();

            stopwatch.Start();
            generator.ReadDatabase();

            stopwatchGenerator.Start();
            generator.GenerateCode();
            stopwatchGenerator.Stop();

            stopwatch.Stop();

            Console.WriteLine("Duration: {0:F1} seconds, Generator {1:F1} seconds", stopwatch.ElapsedMilliseconds / 1000.0,
                stopwatchGenerator.ElapsedMilliseconds / 1000.0);
            Console.WriteLine($"Writing to {fullPath}");
            Console.WriteLine();

            if (outer.FileData.Length > 0)
                using (var sw = new StreamWriter(fullPath))
                {
                    sw.Write(outer.FileData.ToString());
                }

            fileManagement.Process(true);
        }

        protected static void CompareAgainstFolderTestComparison(string subFolder)
        {
            var testRootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "OneDrive\\Documents");
            if (!string.IsNullOrEmpty(subFolder))
                testRootPath = Path.Combine(testRootPath, subFolder);

            Console.WriteLine("Reading from: " + testRootPath);
            Console.WriteLine();

            var testComparisonFiles = Directory.GetFiles(testRootPath);
            var generatedFiles = Directory.GetFiles(Settings.Root);

            Assert.AreEqual(testComparisonFiles.Length, generatedFiles.Length);

            foreach (var comparisonFile in testComparisonFiles.Where(x => x.ToLower().EndsWith("Audit")))
            {
                var filename = Path.GetFileName(comparisonFile);
                var generatedPath = Path.Combine(Settings.Root, filename);
                var testComparison = File.ReadAllText(comparisonFile);
                var generated = File.ReadAllText(generatedPath);

                Console.WriteLine(comparisonFile);
                Console.WriteLine(generatedPath);
                Console.WriteLine();

                Assert.AreEqual(testComparison, generated);
            }
        }

        protected static void CompareAgainstTestComparison(string database)
        {
            var comparisonFile = $"{database}_{Settings.DatabaseType}_{Settings.TemplateType}_Fk{Settings.ForeignKeyNamingStrategy}.cs";
            var testRootPath = AppDomain.CurrentDomain.BaseDirectory;
            var testComparisonPath = Path.Combine(testRootPath, $"TestComparison\\{comparisonFile}");
            var testComparison = File.ReadAllText(testComparisonPath);
            var generatedPath = Path.Combine(Settings.Root, comparisonFile);
            var generated = File.ReadAllText(generatedPath);

            Console.WriteLine(testComparisonPath);
            Console.WriteLine(generatedPath);

            Assert.AreEqual(testComparison, generated);
        }

        protected static void ResetFilters()
        {
            FilterSettings.Reset();
            FilterSettings.AddDefaults();
            FilterSettings.CheckSettings();
        }
    }
}