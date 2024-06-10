﻿namespace Generator.Tests.Unit
{
    using System.Collections.Generic;
    using System.Data.Common;
    using Efrpg;
    using Efrpg.Readers;

    public class FakeDatabaseReader : DatabaseReader
    {
        public FakeDatabaseReader()
            : base(new FakeDbProviderFactory(), null)
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
            return true;
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
            return string.Empty;
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

        public override void ReadStoredProcReturnObjects(List<StoredProcedure> procs) { }
    }
}