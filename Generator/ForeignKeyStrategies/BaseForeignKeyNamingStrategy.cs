using Efrpg.Filtering;

namespace Efrpg.ForeignKeyStrategies
{
    public class BaseForeignKeyNamingStrategy
    {
        protected readonly Table _table;
        protected readonly IDbContextFilter _filter;

        public BaseForeignKeyNamingStrategy(IDbContextFilter filter, Table table)
        {
            _filter = filter;
            _table  = table;
        }

        protected static string CheckForUserSpecifiedName(bool isParent, ForeignKey foreignKey)
        {
            // User specified name via AddRelationship
            if (isParent && !string.IsNullOrEmpty(foreignKey.ParentName))
                return foreignKey.ParentName;

            // User specified name via AddRelationship
            if (!isParent && !string.IsNullOrEmpty(foreignKey.ChildName))
                return foreignKey.ChildName;

            return null;
        }
    }
}