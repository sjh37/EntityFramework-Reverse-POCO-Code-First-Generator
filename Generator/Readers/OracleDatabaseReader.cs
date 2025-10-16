using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using Efrpg.LanguageMapping;

namespace Efrpg.Readers
{
    public class OracleDatabaseReader : DatabaseReader
    {
        public OracleDatabaseReader(DbProviderFactory factory, IDatabaseToPropertyType databaseToPropertyType)
            : base(factory, databaseToPropertyType)
        {
            StoredProcedureParameterDbType = new Dictionary<string, string>
            {
                { string.Empty, "Varchar2" }, // default
                { "bfile", "BFile" },
                { "binary_double", "Double" },
                { "binary_float", "Single" },
                { "binary_integer", "Int64" },
                { "blob", "Blob" },
                { "char", "Char" },
                { "clob", "Clob" },
                { "date", "Date" },
                { "float", "Double" },
                { "interval day to second", "IntervalDS" },
                { "interval year to month", "IntervalYM" },
                { "long", "Long" },
                { "long raw", "LongRaw" },
                { "nchar", "NChar" },
                { "nclob", "NClob" },
                { "number", "Decimal" },
                { "nvarchar2", "NVarchar2" },
                { "pls_integer", "Int32" },
                { "raw", "Raw" },
                { "real", "Single" },
                { "rowid", "Varchar2" },
                { "timestamp", "TimeStamp" },
                { "timestamp with local time zone", "TimeStampLTZ" },
                { "timestamp with time zone", "TimeStampTZ" },
                { "urowid", "Varchar2" },
                { "varchar2", "Varchar2" },
                { "xmltype", "XmlType" }
            };
        }

        protected override string TableSQL()
        {
            return @"
SELECT  ATAB.OWNER AS SchemaName,
        ATAB.TABLE_NAME AS TableName,
        CASE WHEN ATAB.TEMPORARY = 'Y' THEN 'TABLE' 
             WHEN AVIEW.VIEW_NAME IS NOT NULL THEN 'VIEW' 
             ELSE 'BASE TABLE' 
        END AS TableType,
        0 AS TableTemporalType,
        ACOL.COLUMN_ID AS Ordinal,
        ACOL.COLUMN_NAME AS ColumnName,
        CASE WHEN ACOL.NULLABLE = 'Y' THEN 1 ELSE 0 END AS IsNullable,
        ACOL.DATA_TYPE AS TypeName,
        NVL(ACOL.CHAR_LENGTH, 0) AS MaxLength,
        NVL(ACOL.DATA_PRECISION, 0) AS Precision,
        NVL(ACOL.DATA_DEFAULT, '') AS Default,
        NVL(ACOL.DATA_SCALE, 0) AS DateTimePrecision,
        NVL(ACOL.DATA_SCALE, 0) AS Scale,
        CASE WHEN AIDCOL.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END AS IsIdentity,
        0 AS IsRowGuid,
        CASE WHEN ACOL.VIRTUAL_COLUMN = 'YES' THEN 1 ELSE 0 END AS IsComputed,
        0 AS GeneratedAlwaysType,
        CASE WHEN AIDCOL.COLUMN_NAME IS NOT NULL OR ACOL.VIRTUAL_COLUMN = 'YES' THEN 1 ELSE 0 END AS IsStoreGenerated,
        CASE WHEN APKCOL.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END AS PrimaryKey,
        NVL(APKCOL.POSITION, 0) AS PrimaryKeyOrdinal,
        CASE WHEN AFKCOL.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END AS IsForeignKey,
        NULL AS SynonymTriggerName
FROM    ALL_TABLES ATAB
        INNER JOIN ALL_TAB_COLUMNS ACOL
            ON ATAB.OWNER = ACOL.OWNER
               AND ATAB.TABLE_NAME = ACOL.TABLE_NAME
        LEFT OUTER JOIN ALL_VIEWS AVIEW
            ON ATAB.OWNER = AVIEW.OWNER
               AND ATAB.TABLE_NAME = AVIEW.VIEW_NAME
        LEFT OUTER JOIN (
            SELECT  ACC.OWNER, ACC.TABLE_NAME, ACC.COLUMN_NAME, ACC.POSITION
            FROM    ALL_CONSTRAINTS AC
                    INNER JOIN ALL_CONS_COLUMNS ACC
                        ON AC.OWNER = ACC.OWNER
                           AND AC.CONSTRAINT_NAME = ACC.CONSTRAINT_NAME
            WHERE   AC.CONSTRAINT_TYPE = 'P'
        ) APKCOL
            ON ACOL.OWNER = APKCOL.OWNER
               AND ACOL.TABLE_NAME = APKCOL.TABLE_NAME
               AND ACOL.COLUMN_NAME = APKCOL.COLUMN_NAME
        LEFT OUTER JOIN (
            SELECT DISTINCT ACC.OWNER, ACC.TABLE_NAME, ACC.COLUMN_NAME
            FROM    ALL_CONSTRAINTS AC
                    INNER JOIN ALL_CONS_COLUMNS ACC
                        ON AC.OWNER = ACC.OWNER
                           AND AC.CONSTRAINT_NAME = ACC.CONSTRAINT_NAME
            WHERE   AC.CONSTRAINT_TYPE = 'R'
        ) AFKCOL
            ON ACOL.OWNER = AFKCOL.OWNER
               AND ACOL.TABLE_NAME = AFKCOL.TABLE_NAME
               AND ACOL.COLUMN_NAME = AFKCOL.COLUMN_NAME
        LEFT OUTER JOIN (
            SELECT  ATC.OWNER, ATC.TABLE_NAME, ATC.COLUMN_NAME
            FROM    ALL_TAB_IDENTITY_COLS ATC
            WHERE   (ATC.GENERATION_TYPE = 'ALWAYS' OR ATC.GENERATION_TYPE = 'BY DEFAULT')
        ) AIDCOL
            ON ACOL.OWNER = AIDCOL.OWNER
               AND ACOL.TABLE_NAME = AIDCOL.TABLE_NAME
               AND ACOL.COLUMN_NAME = AIDCOL.COLUMN_NAME
WHERE   ATAB.OWNER NOT IN ('SYS', 'SYSTEM', 'OUTLN', 'DBSNMP', 'APPQOSSYS', 'DBSFWUSER', 'GGSYS', 
                           'ANONYMOUS', 'CTXSYS', 'DVSYS', 'DVF', 'GSMADMIN_INTERNAL', 'LBACSYS', 
                           'MDSYS', 'OLAPSYS', 'ORACLE_OCM', 'ORDDATA', 'ORDPLUGINS', 'ORDSYS', 
                           'SI_INFORMTN_SCHEMA', 'SPATIAL_CSW_ADMIN_USR', 'SPATIAL_WFS_ADMIN_USR', 
                           'WMSYS', 'XDB', 'XS$NULL', 'FLOWS_FILES', 'APEX_PUBLIC_USER', 'APEX_040000',
                           'APEX_040200', 'RMAN$CATALOG')
        AND ATAB.TABLE_NAME NOT IN ('EDMMETADATA', '__MIGRATIONHISTORY', '__EFMIGRATIONSHISTORY', '__REFACTORLOG')
        AND ATAB.NESTED = 'NO'
        AND ATAB.SECONDARY = 'N'
ORDER BY ATAB.OWNER, ATAB.TABLE_NAME, ACOL.COLUMN_ID";
        }

        protected override string ForeignKeySQL()
        {
            return @"
SELECT  AFKC.TABLE_NAME AS FK_Table,
        AFKC.COLUMN_NAME AS FK_Column,
        APKC.TABLE_NAME AS PK_Table,
        APKC.COLUMN_NAME AS PK_Column,
        AFC.CONSTRAINT_NAME AS Constraint_Name,
        APKC.OWNER AS pkSchema,
        AFKC.OWNER AS fkSchema,
        APKC.COLUMN_NAME AS primarykey,
        AFKC.POSITION AS ORDINAL_POSITION,
        CASE WHEN AFC.DELETE_RULE = 'CASCADE' THEN 1 ELSE 0 END AS CascadeOnDelete,
        CASE WHEN AFC.STATUS = 'DISABLED' THEN 1 ELSE 0 END AS IsNotEnforced
FROM    ALL_CONSTRAINTS AFC
        INNER JOIN ALL_CONS_COLUMNS AFKC
            ON AFC.OWNER = AFKC.OWNER
               AND AFC.CONSTRAINT_NAME = AFKC.CONSTRAINT_NAME
        INNER JOIN ALL_CONSTRAINTS APC
            ON AFC.R_OWNER = APC.OWNER
               AND AFC.R_CONSTRAINT_NAME = APC.CONSTRAINT_NAME
        INNER JOIN ALL_CONS_COLUMNS APKC
            ON APC.OWNER = APKC.OWNER
               AND APC.CONSTRAINT_NAME = APKC.CONSTRAINT_NAME
               AND AFKC.POSITION = APKC.POSITION
WHERE   AFC.CONSTRAINT_TYPE = 'R'
ORDER BY AFKC.OWNER, AFKC.TABLE_NAME, AFC.CONSTRAINT_NAME, AFKC.POSITION";
        }

        protected override string ExtendedPropertySQL()
        {
            return @"
SELECT  ATC.OWNER AS schema,
        ATC.TABLE_NAME AS table,
        ATC.COLUMN_NAME AS column,
        ATC.COMMENTS AS property
FROM    ALL_COL_COMMENTS ATC
WHERE   ATC.OWNER NOT IN ('SYS', 'SYSTEM', 'OUTLN', 'DBSNMP', 'APPQOSSYS', 'DBSFWUSER', 'GGSYS', 
                          'ANONYMOUS', 'CTXSYS', 'DVSYS', 'DVF', 'GSMADMIN_INTERNAL', 'LBACSYS', 
                          'MDSYS', 'OLAPSYS', 'ORACLE_OCM', 'ORDDATA', 'ORDPLUGINS', 'ORDSYS', 
                          'SI_INFORMTN_SCHEMA', 'SPATIAL_CSW_ADMIN_USR', 'SPATIAL_WFS_ADMIN_USR', 
                          'WMSYS', 'XDB', 'XS$NULL', 'FLOWS_FILES', 'APEX_PUBLIC_USER', 'APEX_040000',
                          'APEX_040200', 'RMAN$CATALOG')
        AND ATC.COMMENTS IS NOT NULL
        AND LENGTH(TRIM(ATC.COMMENTS)) > 0";
        }

        protected override string DoesExtendedPropertyTableExistSQL()
        {
            return string.Empty;
        }

        protected override string IndexSQL()
        {
            return @"
SELECT  AI.TABLE_OWNER AS TableSchema,
        AI.TABLE_NAME AS TableName,
        AI.INDEX_NAME AS IndexName,
        AIC.COLUMN_POSITION AS KeyOrdinal,
        AIC.COLUMN_NAME AS ColumnName,
        CASE WHEN AI.UNIQUENESS = 'UNIQUE' THEN 1 ELSE 0 END AS IsUnique,
        CASE WHEN AC.CONSTRAINT_TYPE = 'P' THEN 1 ELSE 0 END AS IsPrimaryKey,
        CASE WHEN AC.CONSTRAINT_TYPE = 'U' THEN 1 ELSE 0 END AS IsUniqueConstraint,
        0 AS IsClustered,
        (SELECT COUNT(*) FROM ALL_IND_COLUMNS AIC2 
         WHERE AIC2.INDEX_OWNER = AI.OWNER 
           AND AIC2.INDEX_NAME = AI.INDEX_NAME) AS ColumnCount
FROM    ALL_INDEXES AI
        INNER JOIN ALL_IND_COLUMNS AIC
            ON AI.OWNER = AIC.INDEX_OWNER
               AND AI.INDEX_NAME = AIC.INDEX_NAME
        LEFT OUTER JOIN ALL_CONSTRAINTS AC
            ON AI.OWNER = AC.OWNER
               AND AI.INDEX_NAME = AC.INDEX_NAME
WHERE   AI.TABLE_OWNER NOT IN ('SYS', 'SYSTEM', 'OUTLN', 'DBSNMP', 'APPQOSSYS', 'DBSFWUSER', 'GGSYS', 
                               'ANONYMOUS', 'CTXSYS', 'DVSYS', 'DVF', 'GSMADMIN_INTERNAL', 'LBACSYS', 
                               'MDSYS', 'OLAPSYS', 'ORACLE_OCM', 'ORDDATA', 'ORDPLUGINS', 'ORDSYS', 
                               'SI_INFORMTN_SCHEMA', 'SPATIAL_CSW_ADMIN_USR', 'SPATIAL_WFS_ADMIN_USR', 
                               'WMSYS', 'XDB', 'XS$NULL', 'FLOWS_FILES', 'APEX_PUBLIC_USER', 'APEX_040000',
                               'APEX_040200', 'RMAN$CATALOG')
        AND AI.INDEX_TYPE <> 'LOB'
ORDER BY AI.TABLE_OWNER, AI.TABLE_NAME, AI.INDEX_NAME, AIC.COLUMN_POSITION";
        }

        public override bool CanReadStoredProcedures()
        {
            return true;
        }

        protected override string StoredProcedureSQL()
        {
            return @"
SELECT  AP.OWNER AS SPECIFIC_SCHEMA,
        AP.OBJECT_NAME AS SPECIFIC_NAME,
        CASE WHEN AP.PROCEDURE_NAME IS NULL THEN 'FUNCTION' ELSE 'PROCEDURE' END AS ROUTINE_TYPE,
        NVL((SELECT AA2.DATA_TYPE FROM ALL_ARGUMENTS AA2 
             WHERE AA2.OWNER = AP.OWNER 
               AND AA2.OBJECT_NAME = AP.OBJECT_NAME 
               AND AA2.POSITION = 0 
               AND ROWNUM = 1), '') AS RETURN_DATA_TYPE,
        NVL(AA.POSITION, 0) AS ORDINAL_POSITION,
        CASE 
            WHEN AA.IN_OUT = 'IN' THEN 'IN'
            WHEN AA.IN_OUT = 'OUT' THEN 'OUT'
            WHEN AA.IN_OUT = 'IN/OUT' THEN 'INOUT'
            ELSE ''
        END AS PARAMETER_MODE,
        NVL(AA.ARGUMENT_NAME, '') AS PARAMETER_NAME,
        NVL(AA.DATA_TYPE, '') AS DATA_TYPE,
        NVL(AA.CHAR_LENGTH, 0) AS CHARACTER_MAXIMUM_LENGTH,
        NVL(AA.DATA_PRECISION, 0) AS NUMERIC_PRECISION,
        NVL(AA.DATA_SCALE, 0) AS NUMERIC_SCALE,
        0 AS DATETIME_PRECISION,
        CASE WHEN AA.TYPE_OWNER IS NOT NULL AND AA.TYPE_NAME IS NOT NULL 
             THEN AA.TYPE_OWNER || '.' || AA.TYPE_NAME 
             ELSE '' 
        END AS USER_DEFINED_TYPE
FROM    ALL_PROCEDURES AP
        LEFT OUTER JOIN ALL_ARGUMENTS AA
            ON AP.OWNER = AA.OWNER
               AND AP.OBJECT_NAME = AA.OBJECT_NAME
               AND (AP.PROCEDURE_NAME = AA.PACKAGE_NAME OR AP.PROCEDURE_NAME IS NULL)
WHERE   AP.OWNER NOT IN ('SYS', 'SYSTEM', 'OUTLN', 'DBSNMP', 'APPQOSSYS', 'DBSFWUSER', 'GGSYS', 
                         'ANONYMOUS', 'CTXSYS', 'DVSYS', 'DVF', 'GSMADMIN_INTERNAL', 'LBACSYS', 
                         'MDSYS', 'OLAPSYS', 'ORACLE_OCM', 'ORDDATA', 'ORDPLUGINS', 'ORDSYS', 
                         'SI_INFORMTN_SCHEMA', 'SPATIAL_CSW_ADMIN_USR', 'SPATIAL_WFS_ADMIN_USR', 
                         'WMSYS', 'XDB', 'XS$NULL', 'FLOWS_FILES', 'APEX_PUBLIC_USER', 'APEX_040000',
                         'APEX_040200', 'RMAN$CATALOG')
        AND AP.OBJECT_TYPE IN ('PROCEDURE', 'FUNCTION')
ORDER BY AP.OWNER, AP.OBJECT_NAME, AA.POSITION";
        }

        protected override string ReadDatabaseEditionSQL()
        {
            return "SELECT BANNER AS Edition, EDITION AS EngineEdition, VERSION AS ProductVersion FROM V$VERSION CROSS JOIN V$INSTANCE WHERE BANNER LIKE 'Oracle%'";
        }

        protected override string MultiContextSQL()
        {
            return string.Empty;
        }

        protected override string EnumSQL(string table, string nameField, string valueField, string groupField)
        {
            if (string.IsNullOrEmpty(table) || string.IsNullOrEmpty(nameField) || string.IsNullOrEmpty(valueField))
                return string.Empty;

            var tableParts = table.Split('.');
            string schema = tableParts.Length > 1 ? tableParts[0] : string.Empty;
            string tableName = tableParts.Length > 1 ? tableParts[1] : table;

            var sql = string.Format("SELECT \"{0}\" AS NameField, \"{1}\" AS ValueField", nameField, valueField);

            if (!string.IsNullOrEmpty(groupField))
                sql += string.Format(", \"{0}\" AS GroupField", groupField);

            if (!string.IsNullOrEmpty(schema))
                sql += string.Format(" FROM \"{0}\".\"{1}\"", schema, tableName);
            else
                sql += string.Format(" FROM \"{0}\"", tableName);

            sql += string.Format(" ORDER BY \"{0}\"", valueField);

            return sql;
        }

        protected override string SequenceSQL()
        {
            return @"
SELECT  SEQUENCE_OWNER AS SchemaName,
        SEQUENCE_NAME AS SequenceName,
        MIN_VALUE AS MinValue,
        MAX_VALUE AS MaxValue,
        INCREMENT_BY AS IncrementBy,
        CYCLE_FLAG AS IsCyclic,
        CACHE_SIZE AS CacheSize
FROM    ALL_SEQUENCES
WHERE   SEQUENCE_OWNER NOT IN ('SYS', 'SYSTEM', 'OUTLN', 'DBSNMP', 'APPQOSSYS', 'DBSFWUSER', 'GGSYS', 
                               'ANONYMOUS', 'CTXSYS', 'DVSYS', 'DVF', 'GSMADMIN_INTERNAL', 'LBACSYS', 
                               'MDSYS', 'OLAPSYS', 'ORACLE_OCM', 'ORDDATA', 'ORDPLUGINS', 'ORDSYS', 
                               'SI_INFORMTN_SCHEMA', 'SPATIAL_CSW_ADMIN_USR', 'SPATIAL_WFS_ADMIN_USR', 
                               'WMSYS', 'XDB', 'XS$NULL', 'FLOWS_FILES', 'APEX_PUBLIC_USER', 'APEX_040000',
                               'APEX_040200', 'RMAN$CATALOG')
ORDER BY SEQUENCE_OWNER, SEQUENCE_NAME";
        }

        protected override string TriggerSQL()
        {
            return @"
SELECT  OWNER AS TableSchema,
        TABLE_NAME AS TableName,
        TRIGGER_NAME AS TriggerName,
        TRIGGERING_EVENT || ' ' || TRIGGER_TYPE AS TriggerType
FROM    ALL_TRIGGERS
WHERE   OWNER NOT IN ('SYS', 'SYSTEM', 'OUTLN', 'DBSNMP', 'APPQOSSYS', 'DBSFWUSER', 'GGSYS', 
                      'ANONYMOUS', 'CTXSYS', 'DVSYS', 'DVF', 'GSMADMIN_INTERNAL', 'LBACSYS', 
                      'MDSYS', 'OLAPSYS', 'ORACLE_OCM', 'ORDDATA', 'ORDPLUGINS', 'ORDSYS', 
                      'SI_INFORMTN_SCHEMA', 'SPATIAL_CSW_ADMIN_USR', 'SPATIAL_WFS_ADMIN_USR', 
                      'WMSYS', 'XDB', 'XS$NULL', 'FLOWS_FILES', 'APEX_PUBLIC_USER', 'APEX_040000',
                      'APEX_040200', 'RMAN$CATALOG')
        AND BASE_OBJECT_TYPE = 'TABLE'
ORDER BY OWNER, TABLE_NAME, TRIGGER_NAME";
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
            return @"
SELECT  ASYN.OWNER AS SchemaName,
        ASYN.SYNONYM_NAME AS TableName,
        'SN' AS TableType,
        0 AS TableTemporalType,
        ACOL.COLUMN_ID AS Ordinal,
        ACOL.COLUMN_NAME AS ColumnName,
        CASE WHEN ACOL.NULLABLE = 'Y' THEN 1 ELSE 0 END AS IsNullable,
        ACOL.DATA_TYPE AS TypeName,
        NVL(ACOL.CHAR_LENGTH, 0) AS MaxLength,
        NVL(ACOL.DATA_PRECISION, 0) AS Precision,
        NVL(ACOL.DATA_DEFAULT, '') AS Default,
        NVL(ACOL.DATA_SCALE, 0) AS DateTimePrecision,
        NVL(ACOL.DATA_SCALE, 0) AS Scale,
        0 AS IsIdentity,
        0 AS IsRowGuid,
        0 AS IsComputed,
        0 AS GeneratedAlwaysType,
        0 AS IsStoreGenerated,
        CASE WHEN APKCOL.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END AS PrimaryKey,
        NVL(APKCOL.POSITION, 0) AS PrimaryKeyOrdinal,
        0 AS IsForeignKey,
        NULL AS SynonymTriggerName
FROM    ALL_SYNONYMS ASYN
        INNER JOIN ALL_TAB_COLUMNS ACOL
            ON NVL(ASYN.TABLE_OWNER, ASYN.OWNER) = ACOL.OWNER
               AND ASYN.TABLE_NAME = ACOL.TABLE_NAME
        LEFT OUTER JOIN (
            SELECT  ACC.OWNER, ACC.TABLE_NAME, ACC.COLUMN_NAME, ACC.POSITION
            FROM    ALL_CONSTRAINTS AC
                    INNER JOIN ALL_CONS_COLUMNS ACC
                        ON AC.OWNER = ACC.OWNER
                           AND AC.CONSTRAINT_NAME = ACC.CONSTRAINT_NAME
            WHERE   AC.CONSTRAINT_TYPE = 'P'
        ) APKCOL
            ON ACOL.OWNER = APKCOL.OWNER
               AND ACOL.TABLE_NAME = APKCOL.TABLE_NAME
               AND ACOL.COLUMN_NAME = APKCOL.COLUMN_NAME
WHERE   ASYN.OWNER NOT IN ('SYS', 'SYSTEM', 'PUBLIC')
ORDER BY ASYN.OWNER, ASYN.SYNONYM_NAME, ACOL.COLUMN_ID";
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
                cmd.CommandText = "SELECT SYS_CONTEXT('USERENV','CURRENT_SCHEMA') FROM DUAL";
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return rdr[0].ToString();
                    }
                }
            }
            return "system";
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

                foreach (var proc in procs.Where(p => !p.IsScalarValuedFunction))
                    ReadStoredProcReturnObject(conn, proc);
            }
        }

        private void ReadStoredProcReturnObject(DbConnection conn, StoredProcedure proc)
        {
            try
            {
                var cmd = GetCmd(conn);
                if (cmd == null)
                    return;

                // For Oracle functions, the return type is already captured in the metadata
                if (!proc.IsStoredProcedure)
                {
                    // Functions return a single value, already handled by return type from ALL_ARGUMENTS
                    return;
                }

                // For procedures, Oracle can have REF CURSOR out parameters
                // Analyzing these would require executing the procedure or inspecting PL/SQL source
                // For now, we'll rely on the parameter metadata from ALL_ARGUMENTS
                
                // Build a basic call signature
                var paramList = new List<string>();
                foreach (var param in proc.Parameters.OrderBy(p => p.Ordinal))
                {
                    if (param.Mode == StoredProcedureParameterMode.In || 
                        param.Mode == StoredProcedureParameterMode.InOut)
                    {
                        paramList.Add("NULL");
                    }
                }

                // Tidy up parameters
                foreach (var p in proc.Parameters)
                    p.NameHumanCase = Regex.Replace(p.NameHumanCase, @"[^A-Za-z0-9@\s]*", string.Empty);

                // Oracle procedures with REF CURSOR parameters would need special handling
                // This is a complex scenario that may require manual configuration
                Settings.ReadStoredProcReturnObjectCompleted(proc);
            }
            catch (Exception ex)
            {
                // Stored procedure analysis failed
                Settings.ReadStoredProcReturnObjectException(ex, proc);
            }
        }

        public override void Init()
        {
            base.Init();
        }
    }
}