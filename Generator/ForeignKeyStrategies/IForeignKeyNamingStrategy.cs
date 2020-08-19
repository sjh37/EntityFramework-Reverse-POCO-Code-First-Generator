namespace Efrpg.ForeignKeyStrategies
{
    public interface IForeignKeyNamingStrategy
    {
        string GetUniqueForeignKeyName(bool isParent, string tableNameHumanCase, ForeignKey foreignKey, bool checkForFkNameClashes,
            bool makeSingular, Relationship relationship);

        void ResetNavigationProperties();
    }
}