using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Efrpg;
using Efrpg.FileManagement;
using Efrpg.Filtering;
using Efrpg.Generators;
using Efrpg.Pluralization;
using Efrpg.Readers;
using Efrpg.Templates;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture, NonParallelizable]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SqlServer)]
    public class MultiContextDatabaseReverseEngineerTests
    {
        private string[] _generatedFileNames;
        private string[] _generatedFullPaths;

        public void SetupSqlServer(string database, string connectionStringName, string dbContextName, string plugin, bool generateSeparateFiles, TemplateType templateType)
        {
            Settings.TemplateType               = templateType;
            Settings.GeneratorType              = GeneratorType.Ef6;
            Settings.ConnectionString           = $"Data Source=(local);Initial Catalog={database};Integrated Security=True;Application Name=Generator";
            Settings.DatabaseType               = DatabaseType.SqlServer;
            Settings.ConnectionStringName       = connectionStringName;
            Settings.DbContextName              = dbContextName;
            Settings.GenerateSingleDbContext    = false; // Use multiple db context generation
            Settings.GenerateSeparateFiles      = generateSeparateFiles;
            Settings.MultiContextSettingsPlugin = null;

            if (!string.IsNullOrWhiteSpace(plugin))
                SetupPlugin(plugin);
        }

        public void SetupPlugin(string plugin)
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri      = new UriBuilder(codeBase);
            var path     = Uri.UnescapeDataString(uri.Path);
            var fullPath = Path.GetFullPath(path);

            if (string.IsNullOrEmpty(plugin))
                Settings.MultiContextSettingsPlugin = null;
            else
                Settings.MultiContextSettingsPlugin = fullPath + ",Generator.Tests.Unit." + plugin;
        }

        public void Run(string filename, Type fileManagerType, string subFolder)
        {
            Inflector.PluralisationService = new EnglishPluralizationService();

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (!string.IsNullOrEmpty(subFolder))
                path = Path.Combine(path, subFolder);
            Settings.Root = path;

            var outer          = new GeneratedTextTransformation();
            var fileManagement = new FileManagementService(outer);
            var generator      = GeneratorFactory.Create(fileManagement, fileManagerType);
            Assert.IsNotNull(generator);

            List<MultiContextSettings> multiDbSettings;
            if (string.IsNullOrWhiteSpace(Settings.MultiContextSettingsPlugin))
            {
                multiDbSettings = generator.FilterList.GetMultiContextSettings();
            }
            else
            {
                var plugin = (IMultiContextSettingsPlugin) AssemblyHelper.LoadPlugin(Settings.MultiContextSettingsPlugin);
                multiDbSettings = plugin.ReadSettings();
            }

            var filters = generator.FilterList.GetFilters();
            Assert.IsNotNull(filters);
            var keys = filters.Select(x => x.Key).ToArray();
            _generatedFileNames = keys.Select(x => $"{filename}_{Settings.DatabaseType}_{Settings.TemplateType}_{x}.cs").ToArray();
            _generatedFullPaths = _generatedFileNames.Select(x => Path.Combine(path, x)).ToArray();

            // Set the fileManager to generate these filenames for testing
            for(var n = 0; n < keys.Length; n++)
            {
                fileManagement.UseFileManager(keys[n]);
                fileManagement.StartNewFile(_generatedFileNames[n]);
            }

            // Delete old generated files
            foreach (var fullPath in _generatedFullPaths)
            {
                Console.WriteLine($"Deleting   {fullPath}");
                if (File.Exists(fullPath))
                    File.Delete(fullPath);
            }

            if (!string.IsNullOrEmpty(subFolder))
            {
                foreach (var old in Directory.GetFiles(Settings.Root))
                    File.Delete(old);
            }

            // Turn on everything for testing
            foreach (var filter in generator.FilterList.GetFilters())
            {
                filter.Value.IncludeViews                 = true;
                filter.Value.IncludeSynonyms              = true;
                filter.Value.IncludeStoredProcedures      = true;
                filter.Value.IncludeTableValuedFunctions  = true;
                filter.Value.IncludeScalarValuedFunctions = true;
            }

            var stopwatch = new Stopwatch();
            var stopwatchGenerator = new Stopwatch();

            stopwatch.Start();
            generator.ReadDatabase();

            stopwatchGenerator.Start();
            generator.GenerateCode();
            stopwatchGenerator.Stop();

            stopwatch.Stop();

            Console.WriteLine("Duration: {0:F1} seconds, Generator {1:F1} seconds", stopwatch.ElapsedMilliseconds / 1000.0, stopwatchGenerator.ElapsedMilliseconds / 1000.0);
            foreach (var file in _generatedFullPaths)
            {
                Console.WriteLine($"Writing to {file}");
            }
            Console.WriteLine();

            if (outer.FileData.Length > 0 && _generatedFullPaths.Length == 1)
            {
                using (var sw = new StreamWriter(_generatedFullPaths[0]))
                {
                    sw.Write(outer.FileData.ToString());
                }
            }

            fileManagement.Process(true);
        }

        [Test, NonParallelizable]
        [TestCase("Northwind", "MyDbContext", "MyDbContext", "MultiContextSettingsPluginNorthwind", true, false, TemplateType.Ef6)]
        public void UsePlugin_SingleFiles(string database, string connectionStringName, string dbContextName, string plugin, bool publicTestComparison, bool generateSeparateFiles, TemplateType templateType)
        {
            // Arrange
            SetupSqlServer(database, connectionStringName, dbContextName, plugin, generateSeparateFiles, templateType);

            // Act
            Run(database, typeof(CustomFileManager), null);

            // Assert
            CompareAgainstTestComparison(publicTestComparison);
        }

        [Test, NonParallelizable]
        [TestCase("EfrpgTest", "EfrpgTest_Settings", "MyDbContext", true, false, TemplateType.Ef6)]
        public void UseSqlSettingsDatabase(string database, string multiContextDatabase, string connectionStringName, bool publicTestComparison, bool generateSeparateFiles, TemplateType templateType)
        {
            // Arrange
            SetupSqlServer(database, connectionStringName, null, null, generateSeparateFiles, templateType);
            Settings.MultiContextSettingsConnectionString = string.IsNullOrWhiteSpace(multiContextDatabase) ? null : $"Data Source=(local);Initial Catalog={multiContextDatabase};Integrated Security=True;Application Name=Generator";

            // Act
            Run(database, typeof(CustomFileManager), null);

            // Assert
            CompareAgainstTestComparison(publicTestComparison);
        }

        /*private static void CompareAgainstFolderTestComparison(string subFolder)
        {
            var testRootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "OneDrive-Personal\\OneDrive\\Documents");
            if (!string.IsNullOrEmpty(subFolder))
                testRootPath = Path.Combine(testRootPath, subFolder);

            var testComparisonFiles = Directory.GetFiles(testRootPath);
            var generatedFiles = Directory.GetFiles(Settings.Root);

            Assert.AreEqual(testComparisonFiles.Length, generatedFiles.Length);

            foreach (var comparisonFile in testComparisonFiles)
            {
                var filename = Path.GetFileName(comparisonFile);
                var generatedPath = Path.Combine(Settings.Root, filename);
                var testComparison = File.ReadAllText(comparisonFile);
                var generated = File.ReadAllText(generatedPath);

                Console.WriteLine(comparisonFile);
                Console.WriteLine(generatedPath);
                Console.WriteLine();

                Assert.AreEqual(testComparison, generated);
                Console.WriteLine("*** OK ***");
                Console.WriteLine("----------------------");
            }
        }*/

        private void CompareAgainstTestComparison(bool publicTestComparison)
        {
            for(var n = 0; n < _generatedFileNames.Length; n++)
            {
                var file = _generatedFileNames[n];
                var testRootPath = publicTestComparison
                    ? AppDomain.CurrentDomain.BaseDirectory
                    : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "OneDrive-Personal\\OneDrive\\Documents");
                var testComparisonPath = Path.Combine(testRootPath, $"TestComparison\\{file}");
                var testComparison = File.ReadAllText(testComparisonPath);
                var generated = File.ReadAllText(_generatedFullPaths[n]);

                Console.WriteLine(testComparisonPath);
                Console.WriteLine(_generatedFullPaths[n]);

                Assert.AreEqual(testComparison, generated);
                Console.WriteLine("*** OK ***");
                Console.WriteLine("----------------------");
            }
        }
    }
}