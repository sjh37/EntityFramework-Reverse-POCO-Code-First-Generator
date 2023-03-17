using System.Data.Common;
using Efrpg;
using Efrpg.Templates;
using Generator.Tests.Common;
using NUnit.Framework;
using Efrpg.FileManagement;
using Efrpg.Readers;
using Microsoft.Data.Sqlite;

namespace Generator.Tests.Integration
{
    [TestFixture]
    [NonParallelizable]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SQLite)]
    public class SingleDatabaseTestSQLite : SingleDatabaseTestBase
    {
        // Using a name and a shared cache allows multiple connections to access the same in-memory database
        const string ConnectionString = "Data Source=InMemorySample;Mode=Memory;Cache=Shared";
        
        // The in-memory database only persists while a connection is open to it.
        // To manage its lifetime, keep one open connection around for as long as you need it.
        private readonly SqliteConnection _masterConnection;

        public SingleDatabaseTestSQLite()
        {
            _masterConnection = new SqliteConnection(ConnectionString);
            _masterConnection.Open();

            var cmd = _masterConnection.CreateCommand();
            cmd.CommandText =
                @"
CREATE TABLE Efrpg
(
    Id    INTEGER PRIMARY KEY,
    vc    varchar(12)  NULL,
    nvc   nvarchar(12) NULL,
    int1  INTEGER  DEFAULT 123,
    real1 REAL,
    text1 TEXT,
    text2 TEXT(20) DEFAULT 'Hello',
    blob1 BLOB,
    blob2 BLOB(37),
    num1  NUMERIC,
    num2  NUMERIC(10, 5)
);
CREATE TABLE EfrpgItems
(
    Id        INTEGER PRIMARY KEY,
    EfrpgId   INTEGER NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (EfrpgId) REFERENCES 'Efrpg' (Id)
        ON DELETE NO ACTION ON UPDATE NO ACTION
);
CREATE INDEX [IX_Efrpg_vc] ON [Efrpg] ([vc]);";
            cmd.ExecuteNonQuery();
        }

        [Test]
        public void ReverseEngineer()
        {
            // Arrange
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = false;
            Settings.ConnectionString = ConnectionString;
            Settings.DatabaseType = DatabaseType.SQLite;
            SetupDatabase("MyDbContext", "MyDbContext", TemplateType.EfCore7, GeneratorType.EfCore, ForeignKeyNamingStrategy.Legacy);

            //var a = new Microsoft.Data.Sqlite.SqliteFactory();
            //var factory = DbProviderFactories.GetFactory("System.Data.SQLite");
            var providerName = DatabaseProvider.GetProvider(Settings.DatabaseType);
            var factory = DbProviderFactories.GetFactory(providerName);
            var databaseReader = DatabaseReaderFactory.Create(factory);


            // Act
            Run("InMemory", ".SQLite", typeof(EfCoreFileManager), null);

            // Assert
            CompareAgainstTestComparison("InMemoryTest");
        }
    }
}