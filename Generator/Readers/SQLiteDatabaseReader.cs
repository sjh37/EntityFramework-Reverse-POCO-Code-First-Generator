using System;
using System.Collections.Generic;
using System.Data.Common;
using Efrpg.LanguageMapping;

namespace Efrpg.Readers
{
    public class SqLiteDatabaseReader : DatabaseReader
    {
        public SqLiteDatabaseReader(DbProviderFactory factory, IDatabaseToPropertyType databaseToPropertyType)
            : base(factory, databaseToPropertyType)
        {
            StoredProcedureParameterDbType = new Dictionary<string, string>
            {
                { string.Empty, "TEXT" }, // default
                { "byte", "INTEGER" },
                { "ByteEnum", "INTEGER" },
                { "DateTime", "TEXT" },
                { "DateTimeOffset", "TEXT" },
                { "decimal", "TEXT" },
                { "double", "REAL" },
                { "float", "REAL" },
                { "Guid", "TEXT" },
                { "int", "INTEGER" },
                { "IntEnum", "INTEGER" },
                { "long", "INTEGER" },
                { "LongEnum", "INTEGER" },
                { "sbyte", "INTEGER" },
                { "SByteEnum", "INTEGER" },
                { "short", "INTEGER" },
                { "ShortEnum", "INTEGER" },
                { "string", "TEXT" },
                { "TimeSpan", "TEXT" },
                { "uint", "INTEGER" },
                { "UIntEnum", "INTEGER" },
                { "ulong", "INTEGER" },
                { "ULongEnum", "INTEGER" },
                { "ushort", "INTEGER" },
                { "UShortEnum", "INTEGER" }
            };
        }

        protected override string TableSQL()
        {
            return @"
SELECT 'main' AS SchemaName,
       TableName,
       TableType,
       0 AS TableTemporalType,
       Ordinal,
       ColumnName,
       IsNullable,
       REPLACE(type, '(' || length || ')', '') AS TypeName,
       length AS [MaxLength],
       Precision,
       [Default],
       0 AS DateTimePrecision,
       Scale,
       pk AS IsIdentity,
       0 AS IsRowGuid,
       0 AS IsComputed,
       0 AS GeneratedAlwaysType,
       0 AS IsStoreGenerated,
       pk AS PrimaryKey,
       pk AS PrimaryKeyOrdinal,
       0 AS IsForeignKey,
       NULL AS SynonymTriggerName
FROM (SELECT m.tbl_name AS TableName,
       CASE WHEN m.type = 'table' THEN 'BASE TABLE' ELSE 'VIEW' END AS TableType,
       c.cid AS Ordinal,
       c.name AS ColumnName,
       CASE WHEN INSTR(c.type, '(') > 0 THEN SUBSTR(LOWER(c.type), 0, INSTR(c.type, '(')) ELSE LOWER(c.type) END AS type,
       1 - c.""notnull"" AS IsNullable,
       c.dflt_value AS [Default],
       c.pk AS pk,
       CAST(SUBSTR(c.type, INSTR(c.type, '(') + 1, INSTR(c.type, ')') - INSTR(c.type, '(') - 1) AS INTEGER) AS length,
       CAST(SUBSTR(c.type, INSTR(c.type, '(') + 1, INSTR(c.type, ',') - INSTR(c.type, '(') - 1) AS INTEGER) AS Precision,
       CAST(SUBSTR(c.type, INSTR(c.type, ',') + 1, INSTR(c.type, ')') - INSTR(c.type, ',') - 1) AS INTEGER) AS Scale
FROM sqlite_master m
         JOIN PRAGMA_TABLE_INFO(m.name) c
WHERE m.type IN ('table', 'view')
  AND m.name NOT LIKE 'sqlite_%')
ORDER BY TableName, Ordinal;";
        }

        protected override string ForeignKeySQL()
        {
            return @"
SELECT m.name AS FK_Table,
       p.[from] AS FK_Column,
       p.[table] AS PK_Table,
       p.[to] AS PK_Column,
       'FK_' || TRIM(m.name) || '_' || TRIM(p.[to]) as Constraint_Name,
       'main' as fkSchema,
       'main' as pkSchema,
       p.[to] AS primarykey,
       p.id + 1 as ORDINAL_POSITION,
       case when P.on_delete is 'CASCADE' then 1 else 0 end as CascadeOnDelete,
       0 as IsNotEnforced
FROM sqlite_master m
         JOIN PRAGMA_FOREIGN_KEY_LIST(m.name) p ON m.name != p.[table]
WHERE m.type = 'table'
ORDER BY m.name;";
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
            return @"
WITH split(TableName, IndexName, KeyOrdinal, ColumnName, csv) AS (
    SELECT TableName,
         IndexName,
         KeyOrdinal,
         '',
         column_name || ','
    FROM (SELECT TableName,
               IndexName,
               0 AS KeyOrdinal,
               SUBSTR(sql, INSTR(sql, '(') + 1, INSTR(sql, ')') - INSTR(sql, '(') - 1) AS column_name
        FROM (SELECT tbl_name AS TableName,
                     name AS IndexName,
                     REPLACE(REPLACE(sql, ']', ''), '[', '') AS sql
              FROM sqlite_master
              WHERE type = 'index'
                AND name NOT LIKE 'sqlite_%'))
    UNION ALL
    SELECT TableName,
         IndexName,
         KeyOrdinal + 1,
         SUBSTR(csv, 0, INSTR(csv, ',')),
         SUBSTR(csv, INSTR(csv, ',') + 1)
    FROM split
    WHERE csv != '')
SELECT 'main' AS TableSchema,
       s.TableName,
       s.IndexName,
       s.KeyOrdinal,
       TRIM(s.ColumnName) as ColumnName,
       (SELECT MAX(KeyOrdinal)
        FROM split x
        WHERE x.TableName = s.TableName
          AND x.IndexName = s.IndexName) AS ColumnCount,
       0 AS IsUnique,
       0 AS IsPrimaryKey,
       0 AS IsUniqueConstraint,
       0 as IsClustered
FROM split s
WHERE ColumnName != ''";
        }

        public override bool CanReadStoredProcedures()
        {
            return false;
        }

        protected override string StoredProcedureSQL()
        {
            return string.Empty;
        }

        protected override string MultiContextSQL()
        {
            return string.Empty;
        }

        protected override string EnumSQL(string table, string nameField, string valueField, string groupField)
        {
            return string.Format("SELECT {0} as NameField, {1} as ValueField,{3} * FROM {2};", nameField, valueField, table, !string.IsNullOrEmpty(groupField) ? $@" {groupField} as GroupField," : @" '' as GroupField,");
        }

        protected override string SequenceSQL()
        {
            return string.Empty;
        }

        protected override string TriggerSQL()
        {
            return @"
SELECT 'main' AS SchemaName, tbl_name AS TableName, name AS TriggerName
FROM sqlite_master
WHERE type = 'trigger'";
        }

        protected override string[] MemoryOptimisedSQL()
        {
            return null;
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
            return "main";
        }

        protected override string SpecialQueryFlags()
        {
            return string.Empty;
        }

        protected override bool HasTemporalTableSupport()
        {
            return false;
        }

        public override bool HasIdentityColumnSupport()
        {
            return false;
        }

        protected override string ReadDatabaseEditionSQL()
        {
            return "SELECT 'SQLite' AS Edition, '' AS EngineEdition, '' AS ProductVersion;";
        }

        public override void ReadStoredProcReturnObjects(List<StoredProcedure> procs)
        {
            throw new NotImplementedException();
        }

        public override void Init()
        {
            base.Init();
            Settings.PrependSchemaName = false;
            IncludeSchema = false;
            DoNotSpecifySizeForMaxLength = true;
        }
    }
}