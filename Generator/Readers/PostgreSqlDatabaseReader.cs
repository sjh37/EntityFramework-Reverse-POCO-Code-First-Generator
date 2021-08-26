using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
SELECT  DISTINCT
        T.TABLE_SCHEMA AS ""SchemaName"",
        T.TABLE_NAME AS ""TableName"",
        T.TABLE_TYPE AS ""TableType"",
        CAST(0 AS SMALLINT) AS ""TableTemporalType"",
        C.ORDINAL_POSITION AS ""Ordinal"",
        C.COLUMN_NAME AS ""ColumnName"",
        CASE WHEN LOWER(C.IS_NULLABLE) = 'yes' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS ""IsNullable"",
        C.DATA_TYPE AS ""TypeName"",
        COALESCE(C.CHARACTER_MAXIMUM_LENGTH, 0) AS ""MaxLength"",
        CAST(COALESCE(C.NUMERIC_PRECISION, 0) AS INT) AS ""Precision"",
        COALESCE(C.COLUMN_DEFAULT, '') AS ""Default"",
        CAST(COALESCE(C.DATETIME_PRECISION, 0) AS INT) AS ""DateTimePrecision"",
        COALESCE(C.NUMERIC_SCALE, 0) AS ""Scale"",
        CASE WHEN LOWER(C.is_identity) = 'yes' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)END AS ""IsIdentity"",
        CAST(0 AS BIT) AS ""IsRowGuid"",
        CASE WHEN LOWER(C.is_generated) = 'never' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS ""IsComputed"",
        CAST(0 AS SMALLINT) AS ""GeneratedAlwaysType"",
        CAST(CASE WHEN LOWER(C.is_identity) = 'yes' OR LOWER(C.is_generated) <> 'never' THEN 1 ELSE 0 END AS BIT) AS IsStoreGenerated,
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
                   AND LOWER(tc.CONSTRAINT_TYPE) = 'primary key'
            ON pk.TABLE_SCHEMA    = C.TABLE_SCHEMA
               AND pk.TABLE_NAME  = C.TABLE_NAME
               AND pk.COLUMN_NAME = C.COLUMN_NAME
        LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS tcfk
            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE fk
                ON tcfk.CONSTRAINT_SCHEMA   = fk.TABLE_SCHEMA
                   AND tcfk.TABLE_NAME      = fk.TABLE_NAME
                   AND tcfk.CONSTRAINT_NAME = fk.CONSTRAINT_NAME
                   AND LOWER(tcfk.CONSTRAINT_TYPE) = 'foreign key'
            ON fk.TABLE_SCHEMA    = C.TABLE_SCHEMA
               AND fk.TABLE_NAME  = C.TABLE_NAME
               AND fk.COLUMN_NAME = C.COLUMN_NAME
WHERE   (LOWER(T.TABLE_TYPE) = 'base table' OR LOWER(T.TABLE_TYPE) = 'view')
        AND (LOWER(T.TABLE_SCHEMA) NOT IN ('pg_catalog', 'information_schema'))
        AND (LOWER(T.TABLE_NAME) NOT IN ('edmmetadata', '__migrationhistory', '__efmigrationshistory', '__refactorlog'))
ORDER BY T.TABLE_NAME, C.COLUMN_NAME, C.ORDINAL_POSITION;";
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
        CASE WHEN LOWER(fk.DELETE_RULE) = 'cascade' THEN 1 ELSE 0 END AS ""CascadeOnDelete"",
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
WHERE   LOWER(tc.CONSTRAINT_TYPE) = 'foreign key'";
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
        ON t.oid = ix.indrelid AND LOWER(t.relkind) = 'r'
    INNER JOIN pg_class i
        ON i.oid = ix.indexrelid
    INNER JOIN pg_attribute a
        ON a.attrelid = t.oid AND a.attnum = ANY(ix.indkey)
    INNER JOIN pg_namespace n
        ON n.oid = t.relnamespace
WHERE LOWER(n.nspname) NOT IN ('pg_catalog', 'information_schema')
      AND LOWER(t.relname) NOT IN ('edmmetadata', '__migrationhistory', '__efmigrationshistory', '__refactorlog')
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
       CASE WHEN LOWER(P.udt_schema) <> 'pg_catalog' THEN P.udt_schema || '.' || P.udt_name ELSE P.udt_name END AS ""USER_DEFINED_TYPE""
FROM information_schema.routines R
    LEFT JOIN information_schema.parameters P
          ON R.specific_schema = P.specific_schema
             AND R.specific_name = P.specific_name
WHERE LOWER(R.routine_schema) NOT IN ('pg_catalog', 'information_schema')
      AND LOWER(R.routine_type) IN ('procedure','function')
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

        protected override string SequenceSQL()
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

        protected override string DefaultSchema(DbConnection conn)
        {
            return "public";
        }

        protected override string DefaultCollation(DbConnection conn)
        {
            return null;
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
            using (var conn = _factory.CreateConnection())
            {
                if (conn == null)
                    return;

                conn.ConnectionString = Settings.ConnectionString;
                conn.Open();

                var cmd = GetCmd(conn);
                if (cmd == null)
                    return;

                // Only functions return result sets in PostgreSQL
                foreach (var sp in procs.Where(x => !x.IsStoredProcedure && !x.IsScalarValuedFunction))
                    ReadFunctionReturnObject(cmd, sp);

                // Tidy up
                cmd.CommandText = "DROP TABLE IF EXISTS efrpg_temp_table;";
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        private void ReadFunctionReturnObject(DbCommand cmd, StoredProcedure proc)
        {
            try
            {
                const string structured = "Structured";
                var sb = new StringBuilder(255);
                sb.AppendLine("DO $$");

                foreach (var param in proc.Parameters.Where(x => x.SqlDbType.Equals(structured, StringComparison.InvariantCultureIgnoreCase)))
                {
                    sb.AppendLine(string.Format("DECLARE {0} {1};", param.Name, param.UserDefinedTypeName));
                }

                sb.AppendLine("BEGIN");
                sb.AppendLine("  DROP TABLE IF EXISTS efrpg_temp_table;");
                sb.AppendLine("  CREATE TEMPORARY TABLE efrpg_temp_table AS");
                sb.Append(string.Format("  SELECT * FROM \"{0}\".\"{1}\"(", proc.Schema.DbName, proc.DbName));

                foreach (var param in proc.Parameters)
                {
                    sb.Append(string.Format("{0}, ",
                        param.SqlDbType.Equals(structured, StringComparison.InvariantCultureIgnoreCase)
                            ? param.Name
                            : "default"));
                }

                if (proc.Parameters.Count > 0)
                    sb.Length -= 2;

                sb.AppendLine(");");
                sb.AppendLine("END $$;");
                sb.AppendLine("SELECT * FROM efrpg_temp_table;");

                var ds = new DataSet();
                using (var sqlAdapter = _factory.CreateDataAdapter())
                {
                    if (sqlAdapter == null)
                        return;

                    cmd.CommandText = sb.ToString();
                    sqlAdapter.SelectCommand = cmd;
                    if(cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();
                    sqlAdapter.SelectCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo);
                    cmd.Connection.Close();
                    sqlAdapter.FillSchema(ds, SchemaType.Source, "MyTable");
                }

                // Tidy up parameters
                foreach (var p in proc.Parameters)
                    p.NameHumanCase = Regex.Replace(p.NameHumanCase, @"[^A-Za-z0-9@\s]*", string.Empty);

                for (var count = 0; count < ds.Tables.Count; count++)
                {
                    proc.ReturnModels.Add(ds.Tables[count].Columns.Cast<DataColumn>().ToList());
                }
            }
            catch (Exception)
            {
                // Function does not have a return type
            }
        }

        public override void Init()
        {
            base.Init();
        }
    }
}