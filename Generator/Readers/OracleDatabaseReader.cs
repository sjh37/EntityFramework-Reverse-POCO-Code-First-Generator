using System.Collections.Generic;
using System.Data.Common;
using Efrpg.LanguageMapping;

namespace Efrpg.Readers
{
    public class OracleDatabaseReader : DatabaseReader
    {
        // Oracle system schemas to exclude from all queries
        private const string ExcludedSchemas =
            "'sys','system','outln','dip','oracle_ocm','dbsnmp','appqossys','wmsys','exfsys'," +
            "'ctxsys','anonymous','xdb','xdbadmin','olapsys','ordplugins','orddata'," +
            "'si_informtn_schema','mdsys','ordsys','sysman','mgmt_view','flows_files'," +
            "'lbacsys','ojvmsys','sysaux','audsys','gsmadmin_internal','gsmcatuser'," +
            "'gsmuser','remote_scheduler_agent','apex_public_user'";

        public OracleDatabaseReader(DbProviderFactory factory, IDatabaseToPropertyType databaseToPropertyType)
            : base(factory, databaseToPropertyType)
        {
        }

        protected override string TableSQL()
        {
            return $@"
SELECT  c.OWNER AS ""SchemaName"",
        c.TABLE_NAME AS ""TableName"",
        CASE WHEN o.OBJECT_TYPE = 'VIEW' THEN 'VIEW' ELSE 'BASE TABLE' END AS ""TableType"",
        0 AS ""TableTemporalType"",
        c.COLUMN_ID AS ""Ordinal"",
        c.COLUMN_NAME AS ""ColumnName"",
        CASE WHEN c.NULLABLE = 'Y' THEN 1 ELSE 0 END AS ""IsNullable"",
        LOWER(c.DATA_TYPE) AS ""TypeName"",
        NVL(c.CHAR_LENGTH, 0) AS ""MaxLength"",
        NVL(c.DATA_PRECISION, 0) AS ""Precision"",
        NVL(TO_CHAR(c.DATA_DEFAULT), '') AS ""Default"",
        0 AS ""DateTimePrecision"",
        NVL(c.DATA_SCALE, 0) AS ""Scale"",
        CASE WHEN c.IDENTITY_COLUMN = 'YES' THEN 1 ELSE 0 END AS ""IsIdentity"",
        0 AS ""IsRowGuid"",
        CASE WHEN c.VIRTUAL_COLUMN = 'YES' THEN 1 ELSE 0 END AS ""IsComputed"",
        0 AS ""GeneratedAlwaysType"",
        CASE WHEN c.IDENTITY_COLUMN = 'YES' OR c.VIRTUAL_COLUMN = 'YES' THEN 1 ELSE 0 END AS ""IsStoreGenerated"",
        CASE WHEN pkc.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END AS ""PrimaryKey"",
        NVL(pkc.POSITION, 0) AS ""PrimaryKeyOrdinal"",
        CASE WHEN fkc.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END AS ""IsForeignKey"",
        NULL AS ""SynonymTriggerName""
FROM    ALL_TAB_COLUMNS c
        INNER JOIN ALL_OBJECTS o
            ON o.OWNER       = c.OWNER
               AND o.OBJECT_NAME = c.TABLE_NAME
               AND o.OBJECT_TYPE IN ('TABLE', 'VIEW')
        LEFT OUTER JOIN (
            SELECT ac.OWNER, ac.TABLE_NAME, acc.COLUMN_NAME, acc.POSITION
            FROM   ALL_CONSTRAINTS ac
                   INNER JOIN ALL_CONS_COLUMNS acc
                       ON acc.OWNER           = ac.OWNER
                          AND acc.CONSTRAINT_NAME = ac.CONSTRAINT_NAME
            WHERE  ac.CONSTRAINT_TYPE = 'P'
        ) pkc
            ON pkc.OWNER       = c.OWNER
               AND pkc.TABLE_NAME  = c.TABLE_NAME
               AND pkc.COLUMN_NAME = c.COLUMN_NAME
        LEFT OUTER JOIN (
            SELECT DISTINCT ac.OWNER, ac.TABLE_NAME, acc.COLUMN_NAME
            FROM   ALL_CONSTRAINTS ac
                   INNER JOIN ALL_CONS_COLUMNS acc
                       ON acc.OWNER           = ac.OWNER
                          AND acc.CONSTRAINT_NAME = ac.CONSTRAINT_NAME
            WHERE  ac.CONSTRAINT_TYPE = 'R'
        ) fkc
            ON fkc.OWNER       = c.OWNER
               AND fkc.TABLE_NAME  = c.TABLE_NAME
               AND fkc.COLUMN_NAME = c.COLUMN_NAME
WHERE   LOWER(c.OWNER) NOT IN ({ExcludedSchemas})
        AND LOWER(c.TABLE_NAME) NOT IN ('edmmetadata', '__migrationhistory', '__efmigrationshistory')
ORDER BY c.OWNER, c.TABLE_NAME, c.COLUMN_ID";
        }

        protected override string ForeignKeySQL()
        {
            return $@"
SELECT  fkcc.TABLE_NAME AS ""FK_Table"",
        fkcc.COLUMN_NAME AS ""FK_Column"",
        pkcc.TABLE_NAME AS ""PK_Table"",
        pkcc.COLUMN_NAME AS ""PK_Column"",
        fk.CONSTRAINT_NAME AS ""Constraint_Name"",
        pkcc.OWNER AS ""pkSchema"",
        fkcc.OWNER AS ""fkSchema"",
        pkcc.COLUMN_NAME AS ""primarykey"",
        fkcc.POSITION AS ""ORDINAL_POSITION"",
        CASE WHEN fk.DELETE_RULE = 'CASCADE' THEN 1 ELSE 0 END AS ""CascadeOnDelete"",
        0 AS ""IsNotEnforced""
FROM    ALL_CONSTRAINTS fk
        INNER JOIN ALL_CONS_COLUMNS fkcc
            ON fkcc.OWNER           = fk.OWNER
               AND fkcc.CONSTRAINT_NAME = fk.CONSTRAINT_NAME
        INNER JOIN ALL_CONSTRAINTS pk
            ON pk.CONSTRAINT_NAME   = fk.R_CONSTRAINT_NAME
               AND pk.OWNER         = fk.R_OWNER
        INNER JOIN ALL_CONS_COLUMNS pkcc
            ON pkcc.OWNER           = pk.OWNER
               AND pkcc.CONSTRAINT_NAME = pk.CONSTRAINT_NAME
               AND pkcc.POSITION    = fkcc.POSITION
WHERE   fk.CONSTRAINT_TYPE = 'R'
        AND LOWER(fk.OWNER) NOT IN ({ExcludedSchemas})
ORDER BY fkcc.OWNER, fkcc.TABLE_NAME, fkcc.POSITION";
        }

        protected override string ExtendedPropertySQL()
        {
            return $@"
SELECT  c.OWNER AS ""schema"",
        c.TABLE_NAME AS ""table"",
        c.COLUMN_NAME AS ""column"",
        'Comment' AS ""propertyname"",
        c.COMMENTS AS ""property""
FROM    ALL_COL_COMMENTS c
WHERE   c.COMMENTS IS NOT NULL
        AND LOWER(c.OWNER) NOT IN ({ExcludedSchemas})";
        }

        protected override string DoesExtendedPropertyTableExistSQL()
        {
            // ALL_COL_COMMENTS always exists in Oracle; no check needed
            return string.Empty;
        }

        protected override string IndexSQL()
        {
            return $@"
SELECT  i.OWNER AS ""TableSchema"",
        i.TABLE_NAME AS ""TableName"",
        i.INDEX_NAME AS ""IndexName"",
        ic.COLUMN_POSITION AS ""KeyOrdinal"",
        ic.COLUMN_NAME AS ""ColumnName"",
        CASE WHEN i.UNIQUENESS = 'UNIQUE' THEN 1 ELSE 0 END AS ""IsUnique"",
        CASE WHEN c.CONSTRAINT_TYPE = 'P' THEN 1 ELSE 0 END AS ""IsPrimaryKey"",
        CASE WHEN c.CONSTRAINT_TYPE = 'U' THEN 1 ELSE 0 END AS ""IsUniqueConstraint"",
        0 AS ""IsClustered"",
        cnt.COLUMN_COUNT AS ""ColumnCount"",
        '' AS ""FilterDefinition"",
        '' AS ""IncludedColumns""
FROM    ALL_INDEXES i
        INNER JOIN ALL_IND_COLUMNS ic
            ON ic.INDEX_OWNER = i.OWNER
               AND ic.INDEX_NAME  = i.INDEX_NAME
        INNER JOIN (
            SELECT INDEX_OWNER, INDEX_NAME, COUNT(*) AS COLUMN_COUNT
            FROM   ALL_IND_COLUMNS
            GROUP BY INDEX_OWNER, INDEX_NAME
        ) cnt
            ON cnt.INDEX_OWNER = i.OWNER
               AND cnt.INDEX_NAME  = i.INDEX_NAME
        LEFT OUTER JOIN ALL_CONSTRAINTS c
            ON c.INDEX_OWNER = i.OWNER
               AND c.INDEX_NAME  = i.INDEX_NAME
               AND c.CONSTRAINT_TYPE IN ('P', 'U')
WHERE   LOWER(i.OWNER) NOT IN ({ExcludedSchemas})
        AND LOWER(i.TABLE_NAME) NOT IN ('edmmetadata', '__migrationhistory', '__efmigrationshistory')
        AND i.INDEX_TYPE NOT IN ('LOB', 'DOMAIN')
ORDER BY i.TABLE_NAME, i.INDEX_NAME, ic.COLUMN_POSITION";
        }

        public override bool CanReadStoredProcedures()
        {
            return true;
        }

        protected override string StoredProcedureSQL()
        {
            // Position 0 in ALL_ARGUMENTS is the function return value (not a real parameter).
            // We exclude it with POSITION > 0 and look it up per-proc with a correlated subquery.
            return $@"
SELECT  p.OWNER AS ""SPECIFIC_SCHEMA"",
        p.OBJECT_NAME AS ""SPECIFIC_NAME"",
        CASE WHEN p.OBJECT_TYPE = 'PROCEDURE' THEN 'PROCEDURE' ELSE 'FUNCTION' END AS ""ROUTINE_TYPE"",
        NVL(
            (SELECT LOWER(a2.DATA_TYPE)
             FROM   ALL_ARGUMENTS a2
             WHERE  a2.OWNER        = p.OWNER
                    AND a2.OBJECT_NAME = p.OBJECT_NAME
                    AND a2.PACKAGE_NAME IS NULL
                    AND a2.POSITION   = 0
                    AND ROWNUM        = 1),
            '') AS ""RETURN_DATA_TYPE"",
        NVL(a.POSITION, 0) AS ""ORDINAL_POSITION"",
        CASE
            WHEN a.IN_OUT = 'IN'     THEN 'IN'
            WHEN a.IN_OUT = 'OUT'    THEN 'OUT'
            ELSE 'INOUT'
        END AS ""PARAMETER_MODE"",
        NVL(a.ARGUMENT_NAME, '') AS ""PARAMETER_NAME"",
        LOWER(NVL(a.DATA_TYPE, '')) AS ""DATA_TYPE"",
        NVL(a.DATA_LENGTH, 0) AS ""CHARACTER_MAXIMUM_LENGTH"",
        NVL(a.PRECISION, 0) AS ""NUMERIC_PRECISION"",
        NVL(a.SCALE, 0) AS ""NUMERIC_SCALE"",
        0 AS ""DATETIME_PRECISION"",
        NVL(a.TYPE_NAME, '') AS ""USER_DEFINED_TYPE"",
        NULL AS ""PARAMETER_DEFAULT""
FROM    ALL_PROCEDURES p
        LEFT OUTER JOIN ALL_ARGUMENTS a
            ON a.OWNER        = p.OWNER
               AND a.OBJECT_NAME  = p.OBJECT_NAME
               AND a.PACKAGE_NAME IS NULL
               AND a.POSITION    > 0
WHERE   p.OBJECT_TYPE IN ('PROCEDURE', 'FUNCTION')
        AND p.PROCEDURE_NAME IS NULL
        AND LOWER(p.OWNER) NOT IN ({ExcludedSchemas})
ORDER BY p.OWNER, p.OBJECT_NAME, a.POSITION";
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
            var formattedTable = table.Contains(".")
                ? $"\"{table.Split('.')[0]}\".\"{table.Split('.')[1]}\""
                : $"\"{table}\"";

            return !string.IsNullOrEmpty(groupField)
                ? $"SELECT \"{nameField}\" AS \"NameField\", \"{valueField}\" AS \"ValueField\", \"{groupField}\" AS \"GroupField\" FROM {formattedTable}"
                : $"SELECT \"{nameField}\" AS \"NameField\", \"{valueField}\" AS \"ValueField\" FROM {formattedTable}";
        }

        protected override string SequenceSQL()
        {
            // Uses ALL_TAB_IDENTITY_COLS (Oracle 12c+) to map sequences to tables.
            // Non-identity sequences that are not referenced by identity columns get NULL table mappings.
            return $@"
SELECT  s.SEQUENCE_OWNER AS ""Schema"",
        s.SEQUENCE_NAME AS ""Name"",
        'number' AS ""DataType"",
        TO_CHAR(s.MIN_VALUE) AS ""StartValue"",
        TO_CHAR(s.INCREMENT_BY) AS ""IncrementValue"",
        TO_CHAR(s.MIN_VALUE) AS ""MinValue"",
        TO_CHAR(s.MAX_VALUE) AS ""MaxValue"",
        CASE WHEN s.CYCLE_FLAG = 'Y' THEN 1 ELSE 0 END AS ""IsCycleEnabled"",
        NVL(ic.OWNER, '') AS ""TableSchema"",
        NVL(ic.TABLE_NAME, '') AS ""TableName""
FROM    ALL_SEQUENCES s
        LEFT OUTER JOIN ALL_TAB_IDENTITY_COLS ic
            ON ic.SEQUENCE_OWNER = s.SEQUENCE_OWNER
               AND ic.SEQUENCE_NAME  = s.SEQUENCE_NAME
WHERE   LOWER(s.SEQUENCE_OWNER) NOT IN ({ExcludedSchemas})
ORDER BY s.SEQUENCE_OWNER, s.SEQUENCE_NAME";
        }

        protected override string TriggerSQL()
        {
            return $@"
SELECT DISTINCT
        t.OWNER AS ""SchemaName"",
        t.TABLE_NAME AS ""TableName"",
        t.TRIGGER_NAME AS ""TriggerName""
FROM    ALL_TRIGGERS t
WHERE   LOWER(t.OWNER) NOT IN ({ExcludedSchemas})
        AND t.BASE_OBJECT_TYPE = 'TABLE'
ORDER BY t.OWNER, t.TABLE_NAME, t.TRIGGER_NAME";
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
                cmd.CommandText = "SELECT SYS_CONTEXT('USERENV','CURRENT_SCHEMA') FROM DUAL";
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                        return rdr[0].ToString();
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
            // Oracle REF CURSOR return type detection is not implemented
        }

        public override void Init()
        {
            base.Init();
        }
    }
}