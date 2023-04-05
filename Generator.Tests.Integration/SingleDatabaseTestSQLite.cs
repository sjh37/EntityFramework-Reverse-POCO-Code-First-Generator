using System.IO;
using System.Threading;
using Efrpg;
using Efrpg.Templates;
using Generator.Tests.Common;
using NUnit.Framework;
using Efrpg.FileManagement;
using Microsoft.Data.Sqlite;

namespace Generator.Tests.Integration
{
    [TestFixture]
    [NonParallelizable]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SQLite)]
    public class SingleDatabaseTestSQLite : SingleDatabaseTestBase
    {
        private static readonly string Filename = Path.Combine(Path.GetTempPath(), "Efrpg.db");
        private static readonly string ConnectionString = "Data Source=" + Filename;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            if (File.Exists(Filename))
                File.Delete(Filename);

            var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText =
                @"
PRAGMA foreign_keys = ON;

CREATE TABLE Efrpg
(
    Id     INTEGER,

    text1  TEXT,
    text2  CHARACTER(22),
    text3  VARCHAR(33),
    text4  VARYING CHARACTER(44),
    text5  NCHAR(55),
    text6  NATIVE CHARACTER(66),
    text7  NVARCHAR(77),
    text8  TEXT(88) DEFAULT 'Hello',
    text9  CLOB,

    int1   INTEGER  DEFAULT 123,
    int2   INT UNIQUE,
    int3   SMALLINT,
    int4   MEDIUMINT,
    int5   BIGINT,
    int6   UNSIGNED BIG INT,
    int7   INT2,
    INT8   INT8,

    blob1  BLOB,
    blob2  BLOB(22),

    real1  REAL CHECK (real1 > 123),
    real2  DOUBLE,
    real3  DOUBLE PRECISION,
    real4  FLOAT,

    num1   NUMERIC,
    num2   DECIMAL(10, 5),
    num22  DECIMAL (10, 5),
    num222 DECIMAL  (10,   5),
    num3   BOOLEAN,

    date1   DATE,
    date2   DATETIME,

    CONSTRAINT [PK_Efrpg] PRIMARY KEY (Id)
);

CREATE TABLE EfrpgItems
(
    Id        INTEGER PRIMARY KEY,
    EfrpgId   INTEGER NOT NULL,
    Test      INT     NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (EfrpgId) REFERENCES [Efrpg] (Id)
        ON DELETE CASCADE ON UPDATE NO ACTION
);

CREATE INDEX [IX_Efrpg] ON [Efrpg] ([TEXT3]);
CREATE INDEX [IX_Efrpg_Composite] ON [Efrpg] (int1, int2);

CREATE VIEW ThisIsAView AS
SELECT Id, text1, int1, blob1, real1, num1
FROM Efrpg;

CREATE TRIGGER efrpg_trigger
    AFTER INSERT
    ON Efrpg
BEGIN
    SELECT 'Test';
END;

INSERT INTO Efrpg (Id, int1, int2)
VALUES (1, 2, 3),
       (2, 3, 4);
INSERT INTO EfrpgItems (Id, EfrpgId, Test)
VALUES (1, 1, 3),
       (2, 2, 4);
";
            cmd.ExecuteNonQuery();
            connection.Close();
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

            // Act
            Run("EfrpgSQLite", ".SQLite", typeof(EfCoreFileManager), null);

            // Assert
            CompareAgainstTestComparison("SQLiteTest");
        }
    }
}