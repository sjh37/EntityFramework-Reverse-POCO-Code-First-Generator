using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using Efrpg.LanguageMapping;

namespace Efrpg.Readers
{
    public class MySqlDatabaseReader : DatabaseReader
    {
        // MySQL system schemas to exclude from all queries
        private const string ExcludedSchemas = "'information_schema','mysql','performance_schema','sys'";

        public MySqlDatabaseReader(DbProviderFactory factory, IDatabaseToPropertyType databaseToPropertyType)
            : base(factory, databaseToPropertyType)
        {
            StoredProcedureParameterDbType = new Dictionary<string, string>
            {
                { string.Empty,          "VarChar" }, // default
                { "bigint unsigned",     "Decimal" },
                { "bigint",              "Int64" },
                { "binary",              "Binary" },
                { "bit",                 "Bit" },
                { "blob",                "Blob" },
                { "bool",                "Byte" },
                { "boolean",             "Byte" },
                { "char",                "String" },
                { "date",                "Date" },
                { "datetime",            "DateTime" },
                { "decimal",             "Decimal" },
                { "double unsigned",     "Decimal" },
                { "double",              "Double" },
                { "enum",                "String" },
                { "float unsigned",      "Decimal" },
                { "float",               "Float" },
                { "int unsigned",        "Int64" },
                { "int",                 "Int32" },
                { "integer unsigned",    "Int64" },
                { "integer",             "Int32" },
                { "longblob",            "LongBlob" },
                { "longtext",            "LongText" },
                { "mediumblob",          "MediumBlob" },
                { "mediumint",           "Int32" },
                { "mediumtext",          "MediumText" },
                { "numeric",             "Decimal" },
                { "real",                "Double" },
                { "set",                 "String" },
                { "smallint unsigned",   "Int32" },
                { "smallint",            "Int16" },
                { "text",                "Text" },
                { "time",                "Time" },
                { "timestamp",           "Timestamp" },
                { "tinyblob",            "TinyBlob" },
                { "tinyint unsigned",    "Byte" },
                { "tinyint",             "SByte" },
                { "tinytext",            "TinyText" },
                { "varbinary",           "VarBinary" },
                { "varchar",             "VarChar" },
                { "year",                "Int16" }
            };
        }

        protected override string TableSQL()
        {
            return $@"
SELECT  T.TABLE_SCHEMA AS SchemaName,
        T.TABLE_NAME AS TableName,
        T.TABLE_TYPE AS TableType,
        CAST(0 AS UNSIGNED) AS TableTemporalType,
        C.ORDINAL_POSITION AS Ordinal,
        C.COLUMN_NAME AS ColumnName,
        CASE WHEN C.IS_NULLABLE = 'YES' THEN CAST(1 AS UNSIGNED) ELSE CAST(0 AS UNSIGNED) END AS IsNullable,
        C.DATA_TYPE AS TypeName,
        COALESCE(C.CHARACTER_MAXIMUM_LENGTH, 0) AS MaxLength,
        CAST(COALESCE(C.NUMERIC_PRECISION, 0) AS UNSIGNED) AS `Precision`,
        COALESCE(C.COLUMN_DEFAULT, '') AS `Default`,
        CAST(COALESCE(C.DATETIME_PRECISION, 0) AS UNSIGNED) AS DateTimePrecision,
        COALESCE(C.NUMERIC_SCALE, 0) AS Scale,
        CASE WHEN C.EXTRA LIKE '%auto_increment%' THEN CAST(1 AS UNSIGNED) ELSE CAST(0 AS UNSIGNED) END AS IsIdentity,
        CAST(0 AS UNSIGNED) AS IsRowGuid,
        CASE WHEN C.GENERATION_EXPRESSION IS NOT NULL AND C.GENERATION_EXPRESSION <> '' THEN CAST(1 AS UNSIGNED) ELSE CAST(0 AS UNSIGNED) END AS IsComputed,
        CAST(0 AS UNSIGNED) AS GeneratedAlwaysType,
        CASE WHEN C.EXTRA LIKE '%auto_increment%' OR (C.GENERATION_EXPRESSION IS NOT NULL AND C.GENERATION_EXPRESSION <> '') THEN CAST(1 AS UNSIGNED) ELSE CAST(0 AS UNSIGNED) END AS IsStoreGenerated,
        CASE WHEN PK.COLUMN_NAME IS NOT NULL THEN CAST(1 AS UNSIGNED) ELSE CAST(0 AS UNSIGNED) END AS PrimaryKey,
        COALESCE(PK.ORDINAL_POSITION, 0) AS PrimaryKeyOrdinal,
        CASE WHEN FK.COLUMN_NAME IS NOT NULL THEN CAST(1 AS UNSIGNED) ELSE CAST(0 AS UNSIGNED) END AS IsForeignKey,
        NULL AS SynonymTriggerName
FROM    INFORMATION_SCHEMA.TABLES T
        INNER JOIN INFORMATION_SCHEMA.COLUMNS C
            ON T.TABLE_SCHEMA = C.TABLE_SCHEMA
               AND T.TABLE_NAME = C.TABLE_NAME
        LEFT OUTER JOIN (
            SELECT  KCU.TABLE_SCHEMA,
                    KCU.TABLE_NAME,
                    KCU.COLUMN_NAME,
                    KCU.ORDINAL_POSITION
            FROM    INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU
                    INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC
                        ON KCU.CONSTRAINT_SCHEMA = TC.CONSTRAINT_SCHEMA
                           AND KCU.CONSTRAINT_NAME = TC.CONSTRAINT_NAME
                           AND KCU.TABLE_SCHEMA = TC.TABLE_SCHEMA
                           AND KCU.TABLE_NAME = TC.TABLE_NAME
            WHERE   TC.CONSTRAINT_TYPE = 'PRIMARY KEY'
        ) PK
            ON C.TABLE_SCHEMA = PK.TABLE_SCHEMA
               AND C.TABLE_NAME = PK.TABLE_NAME
               AND C.COLUMN_NAME = PK.COLUMN_NAME
        LEFT OUTER JOIN (
            SELECT DISTINCT
                    KCU.TABLE_SCHEMA,
                    KCU.TABLE_NAME,
                    KCU.COLUMN_NAME
            FROM    INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU
                    INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC
                        ON KCU.CONSTRAINT_SCHEMA = TC.CONSTRAINT_SCHEMA
                           AND KCU.CONSTRAINT_NAME = TC.CONSTRAINT_NAME
                           AND KCU.TABLE_SCHEMA = TC.TABLE_SCHEMA
                           AND KCU.TABLE_NAME = TC.TABLE_NAME
            WHERE   TC.CONSTRAINT_TYPE = 'FOREIGN KEY'
        ) FK
            ON C.TABLE_SCHEMA = FK.TABLE_SCHEMA
               AND C.TABLE_NAME = FK.TABLE_NAME
               AND C.COLUMN_NAME = FK.COLUMN_NAME
WHERE   T.TABLE_TYPE IN ('BASE TABLE', 'VIEW')
        AND T.TABLE_SCHEMA NOT IN ({ExcludedSchemas})
        AND T.TABLE_NAME NOT IN ('edmmetadata', '__migrationhistory', '__efmigrationshistory', '__refactorlog')
ORDER BY T.TABLE_SCHEMA, T.TABLE_NAME, C.ORDINAL_POSITION";
        }

        protected override string ForeignKeySQL()
        {
            return $@"
SELECT  KCU.TABLE_NAME AS FK_Table,
        KCU.COLUMN_NAME AS FK_Column,
        KCU.REFERENCED_TABLE_NAME AS PK_Table,
        KCU.REFERENCED_COLUMN_NAME AS PK_Column,
        KCU.CONSTRAINT_NAME AS Constraint_Name,
        KCU.REFERENCED_TABLE_SCHEMA AS pkSchema,
        KCU.TABLE_SCHEMA AS fkSchema,
        KCU.REFERENCED_COLUMN_NAME AS primarykey,
        KCU.ORDINAL_POSITION AS ORDINAL_POSITION,
        CASE WHEN RC.DELETE_RULE = 'CASCADE' THEN CAST(1 AS UNSIGNED) ELSE CAST(0 AS UNSIGNED) END AS CascadeOnDelete,
        CAST(0 AS UNSIGNED) AS IsNotEnforced
FROM    INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU
        INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC
            ON KCU.CONSTRAINT_SCHEMA = TC.CONSTRAINT_SCHEMA
               AND KCU.CONSTRAINT_NAME = TC.CONSTRAINT_NAME
               AND KCU.TABLE_SCHEMA = TC.TABLE_SCHEMA
               AND KCU.TABLE_NAME = TC.TABLE_NAME
        INNER JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC
            ON KCU.CONSTRAINT_SCHEMA = RC.CONSTRAINT_SCHEMA
               AND KCU.CONSTRAINT_NAME = RC.CONSTRAINT_NAME
WHERE   TC.CONSTRAINT_TYPE = 'FOREIGN KEY'
        AND KCU.REFERENCED_TABLE_NAME IS NOT NULL
ORDER BY KCU.TABLE_SCHEMA, KCU.TABLE_NAME, KCU.ORDINAL_POSITION";
        }

        protected override string ExtendedPropertySQL()
        {
            // MySQL uses COLUMN_COMMENT as the equivalent of extended properties
            return $@"
SELECT  C.TABLE_SCHEMA AS `schema`,
        C.TABLE_NAME AS `table`,
        C.COLUMN_NAME AS `column`,
        'Comment' AS propertyname,
        C.COLUMN_COMMENT AS property
FROM    INFORMATION_SCHEMA.COLUMNS C
WHERE   C.TABLE_SCHEMA NOT IN ({ExcludedSchemas})
        AND C.COLUMN_COMMENT IS NOT NULL
        AND C.COLUMN_COMMENT <> ''";
        }

        protected override string DoesExtendedPropertyTableExistSQL()
        {
            // INFORMATION_SCHEMA.COLUMNS always exists; no check needed
            return string.Empty;
        }

        protected override string IndexSQL()
        {
            return $@"
SELECT  S.TABLE_SCHEMA AS TableSchema,
        S.TABLE_NAME AS TableName,
        S.INDEX_NAME AS IndexName,
        S.SEQ_IN_INDEX AS KeyOrdinal,
        S.COLUMN_NAME AS ColumnName,
        CASE WHEN S.NON_UNIQUE = 0 THEN CAST(1 AS UNSIGNED) ELSE CAST(0 AS UNSIGNED) END AS IsUnique,
        CASE WHEN S.INDEX_NAME = 'PRIMARY' THEN CAST(1 AS UNSIGNED) ELSE CAST(0 AS UNSIGNED) END AS IsPrimaryKey,
        CAST(0 AS UNSIGNED) AS IsUniqueConstraint,
        CAST(0 AS UNSIGNED) AS IsClustered,
        (SELECT COUNT(*) FROM INFORMATION_SCHEMA.STATISTICS S2
         WHERE  S2.TABLE_SCHEMA = S.TABLE_SCHEMA
                AND S2.TABLE_NAME  = S.TABLE_NAME
                AND S2.INDEX_NAME  = S.INDEX_NAME) AS ColumnCount,
        '' AS FilterDefinition,
        '' AS IncludedColumns
FROM    INFORMATION_SCHEMA.STATISTICS S
WHERE   S.TABLE_SCHEMA NOT IN ({ExcludedSchemas})
        AND S.TABLE_NAME NOT IN ('edmmetadata', '__migrationhistory', '__efmigrationshistory', '__refactorlog')
ORDER BY S.TABLE_SCHEMA, S.TABLE_NAME, S.INDEX_NAME, S.SEQ_IN_INDEX";
        }

        public override bool CanReadStoredProcedures()
        {
            return true;
        }

        protected override string StoredProcedureSQL()
        {
            return $@"
SELECT  R.ROUTINE_SCHEMA AS SPECIFIC_SCHEMA,
        R.ROUTINE_NAME AS SPECIFIC_NAME,
        R.ROUTINE_TYPE AS ROUTINE_TYPE,
        LOWER(COALESCE(R.DTD_IDENTIFIER, '')) AS RETURN_DATA_TYPE,
        COALESCE(P.ORDINAL_POSITION, 0) AS ORDINAL_POSITION,
        COALESCE(P.PARAMETER_MODE, '') AS PARAMETER_MODE,
        COALESCE(P.PARAMETER_NAME, '') AS PARAMETER_NAME,
        LOWER(COALESCE(P.DATA_TYPE, '')) AS DATA_TYPE,
        COALESCE(P.CHARACTER_MAXIMUM_LENGTH, 0) AS CHARACTER_MAXIMUM_LENGTH,
        COALESCE(P.NUMERIC_PRECISION, 0) AS NUMERIC_PRECISION,
        COALESCE(P.NUMERIC_SCALE, 0) AS NUMERIC_SCALE,
        COALESCE(P.DATETIME_PRECISION, 0) AS DATETIME_PRECISION,
        COALESCE(P.DTD_IDENTIFIER, '') AS USER_DEFINED_TYPE,
        NULL AS PARAMETER_DEFAULT
FROM    INFORMATION_SCHEMA.ROUTINES R
        LEFT JOIN INFORMATION_SCHEMA.PARAMETERS P
            ON R.ROUTINE_SCHEMA = P.SPECIFIC_SCHEMA
               AND R.ROUTINE_NAME  = P.SPECIFIC_NAME
               AND P.ORDINAL_POSITION > 0
WHERE   R.ROUTINE_SCHEMA NOT IN ({ExcludedSchemas})
        AND R.ROUTINE_TYPE IN ('PROCEDURE', 'FUNCTION')
ORDER BY R.ROUTINE_SCHEMA, R.ROUTINE_NAME, P.ORDINAL_POSITION";
        }

        protected override string ReadDatabaseEditionSQL()
        {
            return "SELECT VERSION() AS Edition, '' AS EngineEdition, VERSION() AS ProductVersion";
        }

        protected override string MultiContextSQL()
        {
            return string.Empty;
        }

        protected override string EnumSQL(string table, string nameField, string valueField, string groupField)
        {
            if (string.IsNullOrEmpty(table) || string.IsNullOrEmpty(nameField) || string.IsNullOrEmpty(valueField))
                return string.Empty;

            var parts = table.Split('.');
            var qualifiedTable = parts.Length > 1
                ? $"`{EscapeIdentifier(parts[0])}`.`{EscapeIdentifier(parts[1])}`"
                : $"`{EscapeIdentifier(table)}`";

            var sql = !string.IsNullOrEmpty(groupField)
                ? $"SELECT `{EscapeIdentifier(nameField)}` AS NameField, `{EscapeIdentifier(valueField)}` AS ValueField, `{EscapeIdentifier(groupField)}` AS GroupField FROM {qualifiedTable}"
                : $"SELECT `{EscapeIdentifier(nameField)}` AS NameField, `{EscapeIdentifier(valueField)}` AS ValueField FROM {qualifiedTable}";

            return sql;
        }

        protected override string SequenceSQL()
        {
            // MySQL uses AUTO_INCREMENT instead of sequence objects; handled in TableSQL
            return string.Empty;
        }

        protected override string TriggerSQL()
        {
            return $@"
SELECT DISTINCT
        TRIGGER_SCHEMA AS SchemaName,
        EVENT_OBJECT_TABLE AS TableName,
        TRIGGER_NAME AS TriggerName
FROM    INFORMATION_SCHEMA.TRIGGERS
WHERE   TRIGGER_SCHEMA NOT IN ({ExcludedSchemas})
ORDER BY TRIGGER_SCHEMA, EVENT_OBJECT_TABLE, TRIGGER_NAME";
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
            var cmd = GetCmd(conn);
            if (cmd != null)
            {
                cmd.CommandText = "SELECT DATABASE()";
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        var schema = rdr[0].ToString();
                        if (!string.IsNullOrEmpty(schema))
                            return schema;
                    }
                }
            }
            return "mysql";
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
            return true;
        }

        public override void ReadStoredProcReturnObjects(List<StoredProcedure> procs)
        {
            if (procs == null || !procs.Any())
                return;

            using (var conn = _factory.CreateConnection())
            {
                if (conn == null)
                    return;

                conn.ConnectionString = Settings.ConnectionString;
                conn.Open();

                foreach (var proc in procs.Where(p => p.IsStoredProcedure))
                    ReadProcedureReturnObject(conn, proc);
            }
        }

        private void ReadProcedureReturnObject(DbConnection conn, StoredProcedure proc)
        {
            try
            {
                var cmd = GetCmd(conn);
                if (cmd == null)
                    return;

                var paramList = proc.Parameters
                    .OrderBy(p => p.Ordinal)
                    .Select(p => p.Mode == StoredProcedureParameterMode.Out ? "@" + p.Name : "NULL");

                cmd.CommandText = $"CALL `{EscapeIdentifier(proc.Schema.DbName)}`.`{EscapeIdentifier(proc.DbName)}`({string.Join(", ", paramList)})";
                cmd.CommandType = CommandType.Text;

                var ds = new DataSet();
                try
                {
                    using (var da = _factory.CreateDataAdapter())
                    {
                        if (da != null)
                        {
                            da.SelectCommand = cmd;
                            da.Fill(ds);
                        }
                    }
                }
                catch
                {
                    // Procedure may need specific parameters; best-effort only
                }

                foreach (var p in proc.Parameters)
                    p.NameHumanCase = Regex.Replace(p.NameHumanCase, @"[^A-Za-z0-9@\s]*", string.Empty);

                for (var count = 0; count < ds.Tables.Count; count++)
                    proc.ReturnModels.Add(ds.Tables[count].Columns.Cast<DataColumn>().ToList());

                proc.MergeModelsIfAllSame();
                Settings.ReadStoredProcReturnObjectCompleted(proc);
            }
            catch (Exception ex)
            {
                Settings.ReadStoredProcReturnObjectException(ex, proc);
            }
        }

        // Escapes a MySQL identifier by doubling backticks it may contain
        private static string EscapeIdentifier(string identifier)
        {
            return string.IsNullOrEmpty(identifier) ? identifier : identifier.Replace("`", "``");
        }

        public override void Init()
        {
            base.Init();
        }
    }
}