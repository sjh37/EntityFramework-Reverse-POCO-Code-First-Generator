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
            return @"
SELECT  T.TABLE_SCHEMA AS ""SchemaName"",
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
        CASE WHEN C.is_generated = 'NEVER' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS ""IsComputed"",
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
            return @"
SELECT  tc.TABLE_NAME AS ""FK_Table"",
        kcu.COLUMN_NAME AS ""FK_Column"",
        ccu.TABLE_NAME AS ""PK_Table"",
        ccu.COLUMN_NAME AS ""PK_Column"",
        tc.CONSTRAINT_NAME AS ""Constraint_Name"",
        ccu.TABLE_SCHEMA AS ""fkSchema"",
        tc.TABLE_SCHEMA AS ""pkSchema"",
        ccu.COLUMN_NAME as ""primarykey"",
        kcu.ORDINAL_POSITION AS ""ORDINAL_POSITION"",
        CASE WHEN fk.DELETE_RULE = 'CASCADE' THEN 1 ELSE 0 END AS ""CascadeOnDelete"",
        CAST(0 AS BIT) AS ""IsNotEnforced""
FROM    INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
        INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu
            ON tc.CONSTRAINT_NAME       = kcu.CONSTRAINT_NAME
               AND tc.TABLE_SCHEMA      = kcu.TABLE_SCHEMA
        INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ccu
            ON ccu.CONSTRAINT_NAME      = tc.CONSTRAINT_NAME
               AND ccu.TABLE_SCHEMA     = tc.TABLE_SCHEMA
        INNER JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS fk
            ON fk.CONSTRAINT_CATALOG    = ccu.CONSTRAINT_CATALOG
               AND fk.CONSTRAINT_SCHEMA = ccu.CONSTRAINT_SCHEMA
               AND fk.CONSTRAINT_NAME   = ccu.CONSTRAINT_NAME
WHERE   tc.CONSTRAINT_TYPE = 'FOREIGN KEY'";
        }

        protected override string ExtendedPropertySQL()
        {
            return @"
SELECT  c.TABLE_SCHEMA as ""schema"",
        c.TABLE_NAME as ""table"",
        c.COLUMN_NAME as ""column"",
        pgd.description as ""property""
FROM    pg_catalog.pg_statio_all_tables st
        INNER JOIN pg_catalog.pg_description pgd
            ON (pgd.objoid = st.relid)
        INNER JOIN INFORMATION_SCHEMA.COLUMNS c
            ON (
                   pgd.objsubid       = c.ORDINAL_POSITION
                   AND c.TABLE_SCHEMA = st.schemaname
                   AND c.TABLE_NAME   = st.relname
               );";
        }

        protected override string DoesExtendedPropertyTableExistSQL()
        {
            return string.Empty;
        }

        protected override string IndexSQL()
        {
            return @"
SELECT n.nspname AS ""TableSchema"",
    t.relname AS ""TableName"",
    i.relname AS ""IndexName"",
    a.attnum AS ""KeyOrdinal"",
    a.attname AS ""ColumnName"",
    ix.indisunique AS ""IsUnique"",
    ix.indisprimary AS ""IsPrimaryKey"",
    0 AS ""IsUniqueConstraint"",
    ix.indisclustered AS ""IsClustered"",
    ix.indnatts AS ""ColumnCount""
FROM
    pg_index ix
    INNER JOIN pg_class t
        ON t.oid = ix.indrelid AND t.relkind = 'r'
    INNER JOIN pg_class i
        ON i.oid = ix.indexrelid
    INNER JOIN pg_attribute a
        ON a.attrelid = t.oid AND a.attnum = ANY(ix.indkey)
    INNER JOIN pg_namespace n
        ON n.oid = t.relnamespace
WHERE n.nspname NOT IN ('pg_catalog', 'information_schema')
      AND t.relname NOT IN ('EdmMetadata', '__MigrationHistory', '__EFMigrationsHistory', '__RefactorLog')
      AND ix.indcheckxmin = false
      AND ix.indisvalid = true
ORDER BY t.relname, i.relname;";
        }

        public override bool CanReadStoredProcedures()
        {
            return true;
        }

        protected override string StoredProcedureSQL()
        {
            return @"
SELECT R.specific_schema AS ""SPECIFIC_SCHEMA"",
       R.routine_name AS ""SPECIFIC_NAME"",
       R.routine_type AS ""ROUTINE_TYPE"",
       R.data_type AS ""RETURN_DATA_TYPE"",
       P.ordinal_position AS ""ORDINAL_POSITION"",
       P.parameter_mode AS ""PARAMETER_MODE"",
       P.parameter_name AS ""PARAMETER_NAME"",
       P.data_type AS ""DATA_TYPE"",
       COALESCE(P.character_maximum_length, 0) AS ""CHARACTER_MAXIMUM_LENGTH"",
       COALESCE(P.numeric_precision, 0) AS ""NUMERIC_PRECISION"",
       COALESCE(P.numeric_scale, 0) AS ""NUMERIC_SCALE"",
       COALESCE(P.datetime_precision, 0) AS ""DATETIME_PRECISION"",
       CASE WHEN P.udt_schema <> 'pg_catalog' THEN P.udt_schema || '.' || P.udt_name ELSE P.udt_name END AS ""USER_DEFINED_TYPE""
FROM information_schema.routines R
    LEFT JOIN information_schema.parameters P
          ON R.specific_schema = P.specific_schema
             AND R.specific_name = P.specific_name
WHERE R.routine_schema NOT IN ('pg_catalog', 'information_schema')
      AND R.routine_type IN ('PROCEDURE','FUNCTION')
ORDER BY R.specific_schema, R.routine_name, R.routine_type;";
        }

        protected override string ReadDatabaseEditionSQL()
        {
            return @"SELECT version() as ""Edition"", '' as ""EngineEdition"", '' as ""ProductVersion"";";
        }

        protected override string MultiContextSQL()
        {
            return string.Empty;
        }

        protected override string EnumSQL(string table, string nameField, string valueField)
        {
            return string.Format(@"SELECT ""{0}"" as ""NameField"", ""{1}"" as ""ValueField"" FROM ""{2}"";", nameField, valueField, table);
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

        protected override string DefaultSchema(DbConnection conn)
        {
            return "public";
        }

        protected override string SpecialQueryFlags()
        {
            return string.Empty;
        }

        protected override bool HasTemporalTableSupport()
        {
            return false;
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