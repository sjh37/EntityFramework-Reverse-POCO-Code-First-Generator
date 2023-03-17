using Efrpg.LanguageMapping;
using System.Collections.Generic;
using System.Data.Common;

namespace Efrpg.Readers
{
    public class SQLiteDatabaseReader : DatabaseReader
    {
        public SQLiteDatabaseReader(DbProviderFactory factory, IDatabaseToPropertyType databaseToPropertyType)
            : base(factory, databaseToPropertyType)
        {
            StoredProcedureParameterDbType = new Dictionary<string, string>
            {
                { string.Empty,     "TEXT" }, // default
                { "byte",           "INTEGER" },
                { "ByteEnum",       "INTEGER" },
                { "DateTime",       "TEXT" },
                { "DateTimeOffset", "TEXT" },
                { "decimal",        "TEXT" },
                { "double",         "REAL" },
                { "float",          "REAL" },
                { "Guid",           "TEXT" },
                { "int",            "INTEGER" },
                { "IntEnum",        "INTEGER" },
                { "long",           "INTEGER" },
                { "LongEnum",       "INTEGER" },
                { "sbyte",          "INTEGER" },
                { "SByteEnum",      "INTEGER" },
                { "short",          "INTEGER" },
                { "ShortEnum",      "INTEGER" },
                { "string",         "TEXT" },
                { "TimeSpan",       "TEXT" },
                { "uint",           "INTEGER" },
                { "UIntEnum",       "INTEGER" },
                { "ulong",          "INTEGER" },
                { "ULongEnum",      "INTEGER" },
                { "ushort",         "INTEGER" },
                { "UShortEnum",     "INTEGER" }
            };
        }

        protected override string TableSQL()
        {
            return @"
SELECT 'main' AS SchemaName,
       tbl_name AS TableName,
       TableType,
       0 AS TableTemporalType,
       cid AS Ordinal,
       ColumnName,
       IsNullable,
       REPLACE(type, '(' || length || ')', '') AS TypeName,
       type as XXXXX,
       CASE WHEN INSTR(type, ',') > 0 THEN 0 ELSE length END AS [MaxLength],
       CASE WHEN INSTR(type, ',') > 0 THEN SUBSTR(type, INSTR(type, ',') - 1) ELSE 0 END AS Precision,
       dflt_value AS [Default],
       0 AS DateTimePrecision,
       CASE WHEN INSTR(type, ',') > 0 THEN SUBSTR(type, 1, INSTR(type, ',') - 1) ELSE 0 END AS Scale,
       pk AS IsIdentity,
       0 AS IsRowGuid,
       0 AS IsComputed,
       0 AS GeneratedAlwaysType,
       0 AS IsStoreGenerated,
       pk AS PrimaryKey,
       pk AS PrimaryKeyOrdinal,
       0 AS IsForeignKey
FROM (SELECT m.tbl_name,
             CASE WHEN m.type = 'table' THEN 'BASE TABLE' ELSE 'VIEW' END AS TableType,
             c.cid,
             c.name AS ColumnName,
             LOWER(c.type) AS type,
             1 - c.""notnull"" AS IsNullable,
             c.dflt_value,
             c.pk,
             SUBSTR(c.type, INSTR(c.type, '(') + 1, INSTR(c.type, ')') - INSTR(c.type, '(') - 1) AS length
      FROM sqlite_master m
               JOIN PRAGMA_TABLE_INFO(m.name) c
      WHERE m.type IN ('table', 'view')
        AND m.name NOT LIKE 'sqlite_%')
ORDER BY tbl_name, cid;";
        }

        protected override string ForeignKeySQL()
        {
            return @"
SELECT  fkData.FK_Table,
        fkData.FK_Column,
        fkData.PK_Table,
        fkData.PK_Column,
        fkData.Constraint_Name,
        fkData.fkSchema,
        fkData.pkSchema,
        fkData.primarykey,
        fkData.ORDINAL_POSITION,
        fkData.CascadeOnDelete,
        fkData.IsNotEnforced
FROM    (SELECT FK.name AS FK_Table,
                FkCol.name AS FK_Column,
                PK.name AS PK_Table,
                PkCol.name AS PK_Column,
                OBJECT_NAME(f.object_id) AS Constraint_Name,
                SCHEMA_NAME(FK.schema_id) AS fkSchema,
                SCHEMA_NAME(PK.schema_id) AS pkSchema,
                PkCol.name AS primarykey,
                k.constraint_column_id AS ORDINAL_POSITION,
                CASE WHEN f.delete_referential_action = 1 THEN 1
                     ELSE 0
                END AS CascadeOnDelete,
                f.is_disabled AS IsNotEnforced,
                ROW_NUMBER() OVER (PARTITION BY FK.name, FkCol.name, PK.name, PkCol.name, SCHEMA_NAME(FK.schema_id), SCHEMA_NAME(PK.schema_id) ORDER BY f.object_id) AS n
         FROM   sys.objects AS PK
                INNER JOIN sys.foreign_keys AS f
                    INNER JOIN sys.foreign_key_columns AS k
                        ON k.constraint_object_id = f.object_id
                    INNER JOIN sys.indexes AS i
                        ON f.referenced_object_id = i.object_id
                           AND f.key_index_id = i.index_id
                    ON PK.object_id = f.referenced_object_id
                INNER JOIN sys.objects AS FK
                    ON f.parent_object_id = FK.object_id
                INNER JOIN sys.columns AS PkCol
                    ON f.referenced_object_id = PkCol.object_id
                       AND k.referenced_column_id = PkCol.column_id
                INNER JOIN sys.columns AS FkCol
                    ON f.parent_object_id = FkCol.object_id
                       AND k.parent_column_id = FkCol.column_id) fkData
WHERE   fkData.n = 1 -- Remove duplicate foreign keys";
        }

        protected override string ExtendedPropertySQL()
        {
            return @"
SELECT  s.name AS [schema],
    t.name AS [table],
    c.name AS [column],
    value AS [property]
FROM    sys.extended_properties AS ep
    INNER JOIN sys.tables AS t
        ON ep.major_id = t.object_id
    INNER JOIN sys.schemas AS s
        ON s.schema_id = t.schema_id
    LEFT JOIN sys.columns AS c
        ON ep.major_id = c.object_id
            AND ep.minor_id = c.column_id
WHERE   class = 1
ORDER BY t.name";
        }

        protected override string DoesExtendedPropertyTableExistSQL()
        {
            return string.Empty;
        }

        protected override string IndexSQL()
        {
            return @"
SELECT  SCHEMA_NAME(t.schema_id) AS TableSchema,
    t.name AS TableName,
    ind.name AS IndexName,
    ic.key_ordinal AS KeyOrdinal,
    col.name AS ColumnName,
    ind.is_unique AS IsUnique,
    ind.is_primary_key AS IsPrimaryKey,
    ind.is_unique_constraint AS IsUniqueConstraint,
    CASE WHEN ind.[type] = 1 AND ind.is_primary_key = 1 THEN 1 ELSE 0 END AS IsClustered,
    (
        SELECT COUNT(1)
        FROM   sys.index_columns i
        WHERE  i.object_id = ind.object_id
            AND i.index_id = ind.index_id
    ) AS ColumnCount
FROM    sys.tables t
    INNER JOIN sys.indexes ind
        ON ind.object_id = t.object_id
    INNER JOIN sys.index_columns ic
        ON ind.object_id = ic.object_id
            AND ind.index_id = ic.index_id
    INNER JOIN sys.columns col
        ON ic.object_id = col.object_id
            AND ic.column_id = col.column_id
WHERE   t.is_ms_shipped = 0
    AND ind.ignore_dup_key = 0
    AND ic.key_ordinal > 0
    AND t.name NOT LIKE 'sysdiagram%'";
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
            return @"
SELECT * FROM MultiContext.Context;
SELECT * FROM MultiContext.[Table];
SELECT * FROM MultiContext.[Column];
SELECT * FROM MultiContext.StoredProcedure;
SELECT * FROM MultiContext.[Function];
SELECT * FROM MultiContext.Enumeration;
SELECT * FROM MultiContext.ForeignKey;";
        }

        protected override string EnumSQL(string table, string nameField, string valueField)
        {
            return string.Format("SELECT {0} as NameField, {1} as ValueField, * FROM {2};", nameField, valueField, table);
        }

        protected override string SequenceSQL()
        {
            return string.Empty;
        }

        protected override string TriggerSQL()
        {
            return @"
SELECT S.name SchemaName, O.name TableName, T.name TriggerName
FROM sys.triggers T
    LEFT JOIN sys.all_objects O
        ON T.parent_id = O.object_id
    LEFT JOIN sys.schemas S
        ON S.schema_id = O.schema_id
WHERE T.type = 'TR'
      AND T.is_disabled = 0
      AND S.name IS NOT NULL
      AND O.name IS NOT NULL
ORDER BY SchemaName, TableName, TriggerName;";
        }

        protected override string[] MemoryOptimisedSQL()
        {
            return new string[]
            {
                "SELECT compatibility_level FROM sys.databases WHERE name = DB_NAME();",
                "SELECT CAST(SERVERPROPERTY(N'IsXTPSupported') AS BIT) AS IsXTPSupported;",
                "SELECT SCHEMA_NAME(schema_id) SchemaName, name TableName FROM sys.tables WHERE is_memory_optimized = 1;"
            };
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
            try
            {
                var cmd = GetCmd(conn);
                if (cmd != null)
                {
                    cmd.CommandText = "SELECT SCHEMA_NAME()";
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            return rdr[0].ToString();
                        }
                    }
                }
            }
            catch
            {
                // Ignored
            }

            return "dbo";
        }

        protected override string SpecialQueryFlags()
        {
            return string.Empty;
        }

        protected override bool HasTemporalTableSupport()
        {
            return false;
        }

        protected override string ReadDatabaseEditionSQL()
        {
            return "SELECT 'SQLite' AS Edition, '' AS EngineEdition, '' AS ProductVersion;";
        }

        public override void ReadStoredProcReturnObjects(List<StoredProcedure> procs)
        {
            throw new System.NotImplementedException();

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