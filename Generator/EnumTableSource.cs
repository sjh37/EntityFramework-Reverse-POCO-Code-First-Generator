namespace Efrpg
{
    public class EnumTableSource : EntityName
    {
        public EnumSchemaSource Schema;

        public EnumTableSource(EnumSchemaSource schema, string dbName)
        {
            Schema = schema;
            DbName = dbName;
        }
    }
}
