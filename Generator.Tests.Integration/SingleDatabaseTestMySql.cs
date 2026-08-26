using System.Collections.Generic;
using Efrpg;
using Efrpg.Filtering;
using Efrpg.Templates;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Integration
{
    /// <summary>
    ///     Reverse engineers the MySQL EfrpgTest database, which is seeded into the efrpg-mysql container by
    ///     TestDatabases/MySQL/docker-compose.yml.
    /// </summary>
    /// <remarks>
    ///     The container loads TestDatabases/MySQL/EfrpgTest.sql on its very first start only, so after that
    ///     script changes the database has to be thrown away and rebuilt with "docker compose down -v" followed
    ///     by "docker compose up -d". A stale container is the usual reason these goldens stop matching.
    /// </remarks>
    [TestFixture]
    [NonParallelizable]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.MySql)]
    public class SingleDatabaseTestMySql : SingleDatabaseTestBase
    {
        public void SetupMySql(string database, string connectionStringName, string dbContextName, TemplateType templateType,
            GeneratorType generatorType, ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            SetupDatabase(connectionStringName, dbContextName, templateType, generatorType, foreignKeyNamingStrategy);

            Settings.ConnectionString = $"Server=localhost;Port=3306;Database={database};User Id=root;Password=efrpgTest123;";
            Settings.DatabaseType = DatabaseType.MySql;

            Settings.Enumerations = new List<EnumerationSettings>
            {
                new EnumerationSettings
                {
                    Name = "Status",
                    Table = "EfrpgTest.Status",
                    NameField = "Name",
                    ValueField = "Id",
                    GroupField = string.Empty
                }
            };
        }

        [Test]
        [NonParallelizable]
        [TestCase(ForeignKeyNamingStrategy.Current, false, false)]
        [TestCase(ForeignKeyNamingStrategy.Current, true,  false)]
        [TestCase(ForeignKeyNamingStrategy.Current, false, true)]
        public void ReverseEngineerMySql_EfCore(ForeignKeyNamingStrategy foreignKeyNamingStrategy, bool useDataAnnotations, bool allowNullStrings)
        {
            // Arrange
            // Per-case settings must come after SetupMySql: SetupDatabase resets the leak-prone settings
            // (AllowNullStrings et al.) to defaults, so anything assigned before it is clobbered.
            SetupMySql("EfrpgTest", "MyDbContext", "MyDbContext", TemplateType.EfCore8, GeneratorType.EfCore, foreignKeyNamingStrategy);
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = false;
            Settings.UseDataAnnotations = useDataAnnotations;
            Settings.AllowNullStrings = allowNullStrings;

            // Act
            var filename = "EfrpgTest" +
                           (useDataAnnotations ? "Da" : string.Empty) +
                           (allowNullStrings ? "Ans" : string.Empty);
            Run(filename, ".MySql", null);

            // Assert
            CompareAgainstTestComparison(filename);
        }

        [Test]
        [NonParallelizable]
        public void NonPascalCased()
        {
            // Arrange - MySQL identifiers are commonly lower_snake_case, so leaving them alone is a realistic
            // choice here in a way it is not on SQL Server.
            SetupMySql("EfrpgTest", "My_db_context", "Efrpg_db_context", TemplateType.EfCore8, GeneratorType.EfCore,
                ForeignKeyNamingStrategy.Current);
            Settings.GenerateSeparateFiles = false;
            Settings.UsePascalCase = false;
            Settings.UseMappingTables = false;

            // Act
            const string filename = "NonPascalCased";
            Run(filename, ".MySql", null);

            // Assert
            CompareAgainstTestComparison(filename);
        }
    }
}
