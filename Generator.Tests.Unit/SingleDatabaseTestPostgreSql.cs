using System.Data.Common;
using Efrpg;
using Efrpg.FileManagement;
using Efrpg.Templates;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture, NonParallelizable]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.PostgreSql)]
    public class SingleDatabaseTestPostgreSql : SingleDatabaseTestBase
    {
        public void SetupPostgreSQL(string database, string connectionStringName, string dbContextName, TemplateType templateType, GeneratorType generatorType, ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            SetupDatabase(connectionStringName, dbContextName, templateType, generatorType, foreignKeyNamingStrategy);

            Settings.ConnectionString = $"Server=127.0.0.1;Port=5432;Database={database};User Id=testuser;Password=testtesttest;";
            Settings.DatabaseType     = DatabaseType.PostgreSQL;
        }
        
        [Test]
        public void CheckPostgreSQLConnection()
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
                Assert.IsTrue((long)result > 1);
            }
        }

        [Test, NonParallelizable]
        [TestCase(ForeignKeyNamingStrategy.Legacy)]
        //[TestCase(ForeignKeyNamingStrategy.Latest)]
        public void ReverseEngineerPostgreSQL(ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            // Arrange
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = false;
            SetupPostgreSQL("Northwind", "MyDbContext", "MyDbContext", TemplateType.EfCore3, GeneratorType.EfCore, foreignKeyNamingStrategy);

            // Act
            var filename = "Northwind";
            Run(filename, ".PostgreSQL", typeof(EfCoreFileManager), null);

            // Assert
            CompareAgainstTestComparison(filename);
        }
    }
}