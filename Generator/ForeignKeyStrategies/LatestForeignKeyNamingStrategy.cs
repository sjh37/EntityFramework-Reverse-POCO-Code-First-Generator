using Efrpg.Filtering;

namespace Efrpg.ForeignKeyStrategies
{
    public class LatestForeignKeyNamingStrategy : BaseForeignKeyNamingStrategy, IForeignKeyNamingStrategy
    {
        public LatestForeignKeyNamingStrategy(IDbContextFilter filter, Table table)
            : base(filter, table)
        {
        }

        public string GetUniqueForeignKeyName(bool isParent, string tableNameHumanCase, ForeignKey foreignKey, bool checkForFkNameClashes, bool makeSingular,
            Relationship relationship)
        {
            // todo
            return "todo";
        }

        public void ResetNavigationProperties()
        {
            // todo
        }
    }
}