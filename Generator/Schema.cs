namespace Efrpg
{
    public class Schema : EntityName
    {
        public Schema(string dbName)
        {
            DbName        = dbName;
            NameHumanCase = dbName;
        }
    }
}