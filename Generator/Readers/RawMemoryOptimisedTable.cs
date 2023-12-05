namespace Efrpg.Readers
{
    public class RawMemoryOptimisedTable
    {
        public readonly string SchemaName;
        public readonly string TableName;

        public RawMemoryOptimisedTable(string schemaName, string tableName)
        {
            SchemaName = schemaName;
            TableName = tableName;
        }
    }
}