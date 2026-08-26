using System.Collections.Generic;
using Efrpg;
using Efrpg.Filtering;
using Efrpg.Templates;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Integration
{
    /// <summary>
    ///     Reverse engineers the Oracle EfrpgTest schema in the efrpg-oracle container.
    /// </summary>
    /// <remarks>
    ///     Unlike MySQL and PostgreSQL there is no schema in an Oracle connection string: the user is the schema.
    ///     TestDatabases/Oracle/docker-compose.yml only seeds northwind.sql, so EfrpgTest.sql has to be loaded by
    ///     hand into its own user - see the header of that script for the sqlplus line.
    /// </remarks>
    [TestFixture]
    [NonParallelizable]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.Oracle)]
    public class SingleDatabaseTestOracle : SingleDatabaseTestBase
    {
        public void SetupOracle(string schema, string connectionStringName, string dbContextName, TemplateType templateType,
            GeneratorType generatorType, ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            SetupDatabase(connectionStringName, dbContextName, templateType, generatorType, foreignKeyNamingStrategy);

            Settings.ConnectionString = $"User Id={schema};Password=abc123;Data Source=localhost:1521/pdb1;";
            Settings.DatabaseType = DatabaseType.Oracle;

            Settings.Enumerations = new List<EnumerationSettings>
            {
                new EnumerationSettings
                {
                    Name = "Status",
                    Table = "EFRPGTEST.STATUS",
                    NameField = "NAME",
                    ValueField = "ID",
                    GroupField = string.Empty
                }
            };
        }

        [Test]
        [NonParallelizable]
        [TestCase(ForeignKeyNamingStrategy.Current, false, false)]
        [TestCase(ForeignKeyNamingStrategy.Current, true,  false)]
        [TestCase(ForeignKeyNamingStrategy.Current, false, true)]
        public void ReverseEngineerOracle_EfCore(ForeignKeyNamingStrategy foreignKeyNamingStrategy, bool useDataAnnotations, bool allowNullStrings)
        {
            // Arrange
            // Per-case settings must come after SetupOracle: SetupDatabase resets the leak-prone settings
            // (AllowNullStrings et al.) to defaults, so anything assigned before it is clobbered.
            SetupOracle("efrpgtest", "MyDbContext", "MyDbContext", TemplateType.EfCore8, GeneratorType.EfCore, foreignKeyNamingStrategy);
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = false;
            Settings.UseDataAnnotations = useDataAnnotations;
            Settings.AllowNullStrings = allowNullStrings;

            // Act
            var filename = "EfrpgTest" +
                           (useDataAnnotations ? "Da" : string.Empty) +
                           (allowNullStrings ? "Ans" : string.Empty);
            Run(filename, ".Oracle", null);

            // Assert
            CompareAgainstTestComparison(filename);
        }

        [Test]
        [NonParallelizable]
        public void NonPascalCased()
        {
            // Arrange - Oracle folds unquoted identifiers to UPPER_SNAKE_CASE, so this is the setting that decides
            // whether the generated model reads as C# or as a catalogue dump. Worth a golden of its own.
            SetupOracle("efrpgtest", "My_db_context", "Efrpg_db_context", TemplateType.EfCore8, GeneratorType.EfCore,
                ForeignKeyNamingStrategy.Current);
            Settings.GenerateSeparateFiles = false;
            Settings.UsePascalCase = false;
            Settings.UseMappingTables = false;

            // Act
            const string filename = "NonPascalCased";
            Run(filename, ".Oracle", null);

            // Assert
            CompareAgainstTestComparison(filename);
        }
    }
}
