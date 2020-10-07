using Efrpg.Filtering;

namespace Efrpg.ForeignKeyStrategies
{
    // Not complete. Please use LegacyForeignKeyNamingStrategy
    public class LatestForeignKeyNamingStrategy : BaseForeignKeyNamingStrategy, IForeignKeyNamingStrategy
    {
        public LatestForeignKeyNamingStrategy(IDbContextFilter filter, Table table)
            : base(filter, table)
        {
        }

        public string GetUniqueForeignKeyName(bool isParent, string tableNameHumanCase, ForeignKey foreignKey, bool checkForFkNameClashes, bool makeSingular,
            Relationship relationship)
        {
            var userSpecifiedName = CheckForUserSpecifiedName(isParent, foreignKey);
            if (!string.IsNullOrEmpty(userSpecifiedName))
                return userSpecifiedName;

            var addReverseNavigationUniquePropName = checkForFkNameClashes &&
                                                     (_table.DbName == foreignKey.FkTableName ||
                                                      (_table.DbName == foreignKey.PkTableName && foreignKey.IncludeReverseNavigation));

            // Attempt 1
            var fkName = (Settings.UsePascalCase ? Inflector.ToTitleCase(foreignKey.FkColumn) : foreignKey.FkColumn).Replace(" ", string.Empty).Replace("$", string.Empty);
            var name = Settings.ForeignKeyName(tableNameHumanCase, foreignKey, fkName, relationship, 1);
            if (addReverseNavigationUniquePropName || !checkForFkNameClashes)
            {
                foreignKey.UniqueName = name;
            }

            // todo
            return name;
        }

        public void ResetNavigationProperties()
        {
            // todo
        }
    }
}