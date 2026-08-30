using Efrpg;
using Efrpg.FileManagement;
using Efrpg.Filtering;
using Efrpg.Templates;
using Generator.Tests.Common;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Common;

namespace Generator.Tests.Integration
{
    [TestFixture]
    [NonParallelizable]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.PostgreSql)]
    public class SingleDatabaseTestPostgreSql : SingleDatabaseTestBase
    {
        public void SetupPostgreSQL(string database, string connectionStringName, string dbContextName, TemplateType templateType, GeneratorType generatorType)
        {
            SetupDatabase(connectionStringName, dbContextName, templateType, generatorType);

            Settings.ConnectionString = $"Server=127.0.0.1;Port=5432;Database={database};User Id=testuser;Password=testtesttest;";
            Settings.DatabaseType = DatabaseType.PostgreSQL;

            Settings.Enumerations = new List<EnumerationSettings>
            {
                new EnumerationSettings
                {
                    Name = "Status",
                    Table = "another.status",
                    NameField = "name",
                    ValueField = "id",
                    GroupField = string.Empty // Or specify your own
                }
            };

        }

        [Test]
        public void CheckNorthwindConnection()
        {
            var factory = DbProviderFactories.GetFactory("Npgsql");
            Assert.IsNotNull(factory);

            using (var conn = factory.CreateConnection())
            {
                Assert.IsNotNull(conn);
                conn.ConnectionString = "Server=127.0.0.1;Port=5432;Database=Northwind;User Id=testuser;Password=testtesttest;";
                conn.Open();

                var cmd = conn.CreateCommand();
                Assert.IsNotNull(cmd);

                cmd.CommandText = "select count(*) from products";
                var result = cmd.ExecuteScalar();
                Assert.IsNotNull(result);
                Assert.IsTrue((long) result > 1);
            }
        }

        [Test]
        [NonParallelizable]
        [TestCase("EfrpgTest", "EfrpgTest", false, false)]
        [TestCase("Northwind", "Northwind", false, false)]
        [TestCase("Northwind", "Northwind", true, false)]
        [TestCase("Northwind", "Northwind", false, true)]
        [TestCase("Northwind", "Northwind", true, true)]
        public void ReverseEngineerPostgreSQL_EfCore(string filenameBase, string database, bool allowNullStrings, bool nullableReverseNavigationProperties)
        {
            // Arrange
            // Per-case settings must come after SetupPostgreSQL: SetupDatabase resets the leak-prone settings
            // (AllowNullStrings et al.) to defaults, so anything assigned before it is clobbered.
            SetupPostgreSQL(database, "MyDbContext", "MyDbContext", TemplateType.EfCore8, GeneratorType.EfCore);
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = false;
            Settings.AllowNullStrings = allowNullStrings;
            // Historically this assignment ran before SetupPostgreSQL, whose SetupDatabase call overrode it to true -
            // so every TestComparison golden for this test was generated with NullableReverseNavigationProperties on,
            // regardless of the test case parameter. Keep true so the goldens still describe the output; the
            // nullableReverseNavigationProperties parameter only contributes to the comparison filename.
            Settings.NullableReverseNavigationProperties = true;

            // Act
            var filename = filenameBase +
                           (allowNullStrings ? "Ans" : string.Empty) +
                           (nullableReverseNavigationProperties ? "Nrnp" : string.Empty);
            Run(filename, ".PostgreSQL", null);

            // Assert
            CompareAgainstTestComparison(filename);
        }

        [Test]
        public void ReverseEngineerPostgreSQL_Ef6()
        {
            // Arrange
            SetupPostgreSQL("EfrpgTest", "MyEf6DbContext", "MyEf6DbContext", TemplateType.Ef6, GeneratorType.Ef6);
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = false;

            // Act
            Run("EfrpgTest", ".PostgreSQL", null);

            // Assert
            CompareAgainstTestComparison("EfrpgTest");
        }
    }
}