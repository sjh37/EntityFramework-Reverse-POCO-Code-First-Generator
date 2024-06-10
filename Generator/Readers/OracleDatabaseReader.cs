using System.Collections.Generic;
using System.Data.Common;
using Efrpg.LanguageMapping;

namespace Efrpg.Readers
{
    public class OracleDatabaseReader : DatabaseReader
    {
        public OracleDatabaseReader(DbProviderFactory factory, IDatabaseToPropertyType databaseToPropertyType)
            : base(factory, databaseToPropertyType)
        {
        }

        protected override string TableSQL()
        {
            return string.Empty;
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

        protected override string SysTypesSQL()
        {
            return string.Empty;
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
            return string.Empty;
        }

        protected override string SequenceSQL()
        {
            return string.Empty;
        }

        protected override string TriggerSQL()
        {
            return string.Empty;
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
            throw new System.NotImplementedException();
        }

        public override void Init()
        {
            base.Init();
        }
    }
}