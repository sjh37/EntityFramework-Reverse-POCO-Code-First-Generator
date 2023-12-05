namespace Efrpg.Readers
{
    public class RawTrigger
    {
        public readonly string SchemaName;
        public readonly string TableName;
        public readonly string TriggerName;

        public RawTrigger(string schemaName, string tableName, string triggerName)
        {
            SchemaName = schemaName;
            TableName = tableName;
            TriggerName = triggerName;
        }
    }
}