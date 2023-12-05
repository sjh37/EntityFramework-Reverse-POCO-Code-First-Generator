namespace Efrpg.Readers
{
    public class RawSequenceTableMapping
    {
        public readonly string TableSchema;
        public readonly string TableName;

        public RawSequenceTableMapping(string tableSchema, string tableName)
        {
            TableSchema = tableSchema;
            TableName = tableName;
        }
    }
}