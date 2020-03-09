using System.Collections.Generic;
using System.Data.Common;
using Efrpg.LanguageMapping;

namespace Efrpg.Readers
{
    public class PostgreSqlDatabaseReader : DatabaseReader
    {
        public PostgreSqlDatabaseReader(DbProviderFactory factory, IDatabaseToPropertyType databaseToPropertyType)
            : base(factory, databaseToPropertyType)
        {
            StoredProcedureParameterDbType = null;
        }

        protected override string TableSQL()
        {
            return @"SELECT  T.TABLE_SCHEMA AS ""SchemaName"",
        T.TABLE_NAME AS ""TableName"",
        T.TABLE_TYPE AS ""TableType"",
        CAST(0 AS SMALLINT) AS ""TableTemporalType"",
        C.ORDINAL_POSITION AS ""Ordinal"",
        C.COLUMN_NAME AS ""ColumnName"",
        CASE WHEN C.IS_NULLABLE = 'YES' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS ""IsNullable"",
        C.DATA_TYPE AS ""TypeName"",
        COALESCE(C.CHARACTER_MAXIMUM_LENGTH, 0) AS ""MaxLength"",
        CAST(COALESCE(C.NUMERIC_PRECISION, 0) AS INT) AS ""Precision"",
        COALESCE(C.COLUMN_DEFAULT, '') AS ""Default"",
        CAST(COALESCE(C.DATETIME_PRECISION, 0) AS INT) AS ""DateTimePrecision"",
        COALESCE(C.NUMERIC_SCALE, 0) AS ""Scale"",
        CASE WHEN C.is_identity = 'YES' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)END AS ""IsIdentity"",
        CAST(0 AS BIT) AS ""IsRowGuid"",
        CASE WHEN C.is_generated = 'NEVER' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT)
        END AS ""IsComputed"",
        CAST(0 AS SMALLINT) AS ""GeneratedAlwaysType"",
        CAST(CASE WHEN C.is_identity = 'YES' OR C.is_generated <> 'NEVER' THEN 1 ELSE 0 END AS BIT) AS IsStoreGenerated,
		CAST(CASE WHEN pk.ordinal_position > 0 THEN 1 ELSE 0 END AS bit) as ""PrimaryKey"",
        COALESCE(pk.ordinal_position, 0) ""PrimaryKeyOrdinal"",
		CAST(CASE WHEN fk.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END AS bit) AS ""IsForeignKey""
FROM    INFORMATION_SCHEMA.TABLES T
        INNER JOIN INFORMATION_SCHEMA.COLUMNS C
            ON T.TABLE_SCHEMA   = C.TABLE_SCHEMA
               AND C.TABLE_NAME = T.TABLE_NAME
        LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE pk
                ON tc.CONSTRAINT_SCHEMA   = pk.TABLE_SCHEMA
                   AND tc.TABLE_NAME      = pk.TABLE_NAME
                   AND tc.CONSTRAINT_NAME = pk.CONSTRAINT_NAME
                   AND tc.CONSTRAINT_TYPE = 'PRIMARY KEY'
            ON pk.TABLE_SCHEMA    = C.TABLE_SCHEMA
               AND pk.TABLE_NAME  = C.TABLE_NAME
               AND pk.COLUMN_NAME = C.COLUMN_NAME
        LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS tcfk
            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE fk
                ON tcfk.CONSTRAINT_SCHEMA   = fk.TABLE_SCHEMA
                   AND tcfk.TABLE_NAME      = fk.TABLE_NAME
                   AND tcfk.CONSTRAINT_NAME = fk.CONSTRAINT_NAME
                   AND tcfk.CONSTRAINT_TYPE = 'FOREIGN KEY'
            ON fk.TABLE_SCHEMA    = C.TABLE_SCHEMA
               AND fk.TABLE_NAME  = C.TABLE_NAME
               AND fk.COLUMN_NAME = C.COLUMN_NAME
WHERE   (T.TABLE_TYPE = 'BASE TABLE' OR T.TABLE_TYPE = 'VIEW')
        AND (T.TABLE_SCHEMA NOT IN ('pg_catalog', 'information_schema'))
        AND (T.TABLE_NAME NOT IN ('EdmMetadata', '__MigrationHistory', '__EFMigrationsHistory', '__RefactorLog'))
ORDER BY C.TABLE_NAME, C.COLUMN_NAME, C.ORDINAL_POSITION;";
        }

        protected override string ForeignKeySQL()
        {
            return string.Empty;
        }

        protected override string ExtendedPropertySQL()
        {
            return string.Empty;
        }

        protected override string DoesExtendedPropertyTableExistSQL()
        {
            return string.Empty;
        }

        protected override string IndexSQL()
        {
            return string.Empty;
        }

        public override bool CanReadStoredProcedures()
        {
            return false;
        }

        protected override string StoredProcedureSQL()
        {
            return string.Empty;
        }

        protected override string ReadDatabaseEditionSQL()
        {
            return string.Empty;
        }

        protected override string MultiContextSQL()
        {
            return string.Empty;
        }

        protected override string EnumSQL(string table, string nameField, string valueField)
        {
            return string.Empty;
        }

        protected override string SynonymTableSQLSetup()
        {
            return string.Empty;
        }

        protected override string SynonymTableSQL()
        {
            return string.Empty;
        }

        protected override string SynonymForeignKeySQLSetup()
        {
            return string.Empty;
        }

        protected override string SynonymForeignKeySQL()
        {
            return string.Empty;
        }

        protected override string SynonymStoredProcedureSQLSetup()
        {
            return string.Empty;
        }

        protected override string SynonymStoredProcedureSQL()
        {
            return string.Empty;
        }

        protected override string SpecialQueryFlags()
        {
            return string.Empty;
        }

        public override void ReadStoredProcReturnObjects(List<StoredProcedure> procs)
        {
            throw new System.NotImplementedException();
        }

        public override void Init()
        {
            base.Init();
        }
    }
}