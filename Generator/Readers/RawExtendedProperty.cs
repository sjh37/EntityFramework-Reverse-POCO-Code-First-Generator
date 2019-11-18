namespace Efrpg.Readers
{
    public class RawExtendedProperty
    {
        public readonly string SchemaName;
        public readonly string TableName;
        public readonly string ColumnName;
        public readonly string ExtendedProperty;
        public readonly bool   TableLevelExtendedComment;

        public RawExtendedProperty(string schemaName, string tableName, string columnName, string extendedProperty)
        {
            SchemaName       = schemaName;
            TableName        = tableName;
            ColumnName       = columnName;
            ExtendedProperty = extendedProperty;

            TableLevelExtendedComment = string.IsNullOrEmpty(columnName);
        }
    }
}