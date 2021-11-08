using System;
using System.Diagnostics;
using System.IO;
using Efrpg;
using Efrpg.FileManagement;
using Efrpg.Generators;
using Efrpg.Pluralization;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    public class ReverseEngineerShared
    {
        protected void Run(string filename, string singleDbContextSubNamespace, Type fileManagerType, string subFolder = null)
        {
            Inflector.PluralisationService   = new EnglishPluralizationService();
            Settings.GenerateSingleDbContext = true;

            var path = Path.Combine(Path.GetTempPath(), "POCO_Generator_Tests");
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (!string.IsNullOrEmpty(subFolder))
                path = Path.Combine(path, subFolder);

            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);

            Settings.Root = path;

            var fullPath = Path.Combine(path, FileGenerator.GetFileOnSettings(filename));
            
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
            Assert.IsNotNull(generator);
            Assert.IsNotNull(generator.FilterList);
            var filters = generator.FilterList.GetFilters();
            Assert.IsNotNull(filters);
            foreach (var filter in filters)
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

        protected static void CompareAgainstFolderTestComparison(string subfolder)
        {
            var testRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestComparison");

            if (!string.IsNullOrWhiteSpace(subfolder))
            {
                testRootPath = Path.Combine(testRootPath, subfolder);
            }

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

        protected virtual void CompareAgainstTestComparison(string database, bool publicTestComparison = false)
        {
            var comparisonFile      = FileGenerator.GetFileOnSettings(database);

            var generatedPath       = Path.Combine(Settings.Root, comparisonFile).Then(_ =>
                                    {
                                        if (!File.Exists(_)) throw new FileNotFoundException("File not found", _);
                                    });
            var generated           = File.ReadAllText(generatedPath);

            var testComparisonPath  = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestComparison", comparisonFile).Then(_ =>
                                    {
                                        if (!File.Exists(_)) throw new FileNotFoundException($"File not found [{_}]", _);
                                    });
            var testComparison      = File.ReadAllText(testComparisonPath);
                
            Console.WriteLine(testComparisonPath);
            Console.WriteLine(generatedPath);


            Assert.AreEqual(testComparison, generated);
        }


    }
}