using Efrpg;
using Efrpg.FileManagement;
using Efrpg.Filtering;
using Efrpg.Templates;
using NUnit.Framework;
using System.Data.Common;

namespace Generator.Tests.Unit
{
    [TestFixture, NonParallelizable]
    [Category(Constants.DbType.PostgreSql)]
    public class SingleDatabaseReverseEngineerPostgreSqlTests : ReverseEngineerShared
    {
        public void SetSettings(string database, string connectionStringName, string dbContextName, TemplateType templateType, GeneratorType generatorType, ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            var factory = DbProviderFactories.GetFactory("Npgsql");
            Assert.IsNotNull(factory);

            var builder = new DbConnectionStringBuilder
            {
                ["Server"] = "localhost",
                ["Port"] = "5432",
                ["Database"] = database.ToLower(),
                ["User Id"] = "testuser",
                ["Password"] = "testtesttest"
            };

            Settings.ForeignKeyNamingStrategy   = foreignKeyNamingStrategy;
            Settings.TemplateType               = templateType;
            Settings.GeneratorType              = generatorType;
            Settings.ConnectionString           = builder.ConnectionString;
            Settings.DatabaseType               = DatabaseType.PostgreSQL;
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

        [Test]
        public void CheckPostgreSQLConnection()
        {
            var factory = DbProviderFactories.GetFactory("Npgsql");
            Assert.IsNotNull(factory);

            var builder = new DbConnectionStringBuilder
            {
                ["Server"] = "localhost",
                ["Port"] = "5432",
                ["Database"] = "Northwind".ToLower(),
                ["User Id"] = "testuser",
                ["Password"] = "testtesttest"
            };

            using (var conn = factory.CreateConnection())
            {
                Assert.IsNotNull(conn);
                conn.ConnectionString = builder.ConnectionString;
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
        [TestCase(ForeignKeyNamingStrategy.Legacy, IgnoreReason = "Schema not complete modify postgre_northwind.sql or comparison file")]
        [TestCase(ForeignKeyNamingStrategy.Latest, IgnoreReason = "WIP")]
        [Category(Constants.DbType.PostgreSql)]
        public void ReverseEngineer(ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            // Arrange
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = false;
            SetSettings("Northwind", "MyDbContext", "MyDbContext", TemplateType.EfCore3, GeneratorType.EfCore, foreignKeyNamingStrategy);

            // Act
            var filename = "Northwind";
            Run(filename, ".PostgreSQL", typeof(CustomFileManager), null);

            // Assert
            CompareAgainstTestComparison(filename);
        }
    }
}