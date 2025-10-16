using Efrpg;
using Efrpg.FileManagement;
using Efrpg.Templates;
using Generator.Tests.Common;
using NUnit.Framework;
using System.Data.Common;

namespace Generator.Tests.Integration
{
    [TestFixture]
    [NonParallelizable]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.PostgreSql)]
    public class SingleDatabaseTestPostgreSql : SingleDatabaseTestBase
    {
        public void SetupPostgreSQL(string database, string connectionStringName, string dbContextName, TemplateType templateType, GeneratorType generatorType,
            ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            SetupDatabase(connectionStringName, dbContextName, templateType, generatorType, foreignKeyNamingStrategy);

            Settings.ConnectionString = $"Server=127.0.0.1;Port=5432;Database={database};User Id=testuser;Password=testtesttest;";
            Settings.DatabaseType = DatabaseType.PostgreSQL;
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
        [TestCase(ForeignKeyNamingStrategy.Current, "EfrpgTest", "EfrpgTest")]
        [TestCase(ForeignKeyNamingStrategy.Current, "Northwind", "Northwind")]
        [TestCase(ForeignKeyNamingStrategy.Current, "PostgisTest", "postgis_test")]
        //[TestCase(ForeignKeyNamingStrategy.LatestMyDbContext
        public void ReverseEngineerPostgreSQL_EfCore(ForeignKeyNamingStrategy foreignKeyNamingStrategy, string filename, string database)
        {
            // Arrange
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = false;
            SetupPostgreSQL(database, "MyDbContext", "MyDbContext", TemplateType.EfCore8, GeneratorType.EfCore, foreignKeyNamingStrategy);

            // Act
            Run(filename, ".PostgreSQL", typeof(EfCoreFileManager), null);

            // Assert
            CompareAgainstTestComparison(filename);
        }

        [Test]
        public void ReverseEngineerPostgreSQL_Ef6()
        {
            // Arrange
            SetupPostgreSQL("EfrpgTest", "MyEf6DbContext", "MyEf6DbContext", TemplateType.Ef6, GeneratorType.Ef6, ForeignKeyNamingStrategy.Current);
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = false;

            // Act
            Run("EfrpgTest", ".PostgreSQL", typeof(EfCoreFileManager), null);

            // Assert
            CompareAgainstTestComparison("EfrpgTest");
        }
    }
}