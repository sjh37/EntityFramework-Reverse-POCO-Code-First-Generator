using System;
using System.Collections.Generic;
using Efrpg.FileManagement;

namespace Efrpg.Generators
{
    // Fill in the following functions with your own code if not using EF6 / EfCore
    // Take a look at GeneratorEf6 and GeneratorEfCore to see what's been done for those.
    public class GeneratorCustom : Generator
    {
        public GeneratorCustom(FileManagementService fileManagementService, Type fileManagerType)
            : base(fileManagementService, fileManagerType)
        {
        }

        protected override bool AllowFkToNonPrimaryKey()
        {
            return false;
        }

        protected override bool FkMustHaveSameNumberOfColumnsAsPrimaryKey()
        {
            return true;
        }

        protected override void SetupEntity(Column c)
        {
        }

        protected override void SetupConfig(Column c)
        {
            c.Config = string.Empty;
        }

        public override string PrimaryKeyModelBuilder(Table table)
        {
            return null;
        }

        public override List<string> IndexModelBuilder(Table t)
        {
            return null;
        }

        public override string IndexModelBuilder(Column c)
        {
            return null;
        }

        protected override string GetHasMethod(Relationship relationship, IList<Column> fkCols, IList<Column> pkCols, bool isNotEnforced, bool fkHasUniqueConstraint)
        {
            return null;
        }

        protected override string GetWithMethod(Relationship relationship, IList<Column> fkCols, string fkPropName, string manyToManyMapping, string mapKey,
            bool includeReverseNavigation, string hasMethod, string pkTableNameHumanCase, string fkTableNameHumanCase, string primaryKeyColumns, bool fkHasUniqueConstraint)
        {
            return string.Empty;
        }

        protected override string GetCascadeOnDelete(bool cascadeOnDelete)
        {
            return string.Empty;
        }

        protected override string GetForeignKeyConstraintName(string foreignKeyConstraintName)
        {
            return string.Empty;
        }
    }
}