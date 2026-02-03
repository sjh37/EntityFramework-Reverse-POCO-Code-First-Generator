namespace Efrpg.Readers
{
    public class RawExtendedProperty
    {
        public readonly string SchemaName;
        public readonly string TableName;
        public readonly string ColumnName;
        public readonly string PropertyName;
        public readonly string ExtendedProperty;
        public readonly bool TableLevelExtendedComment;

        public RawExtendedProperty(string schemaName, string tableName, string columnName, string propertyName, string extendedProperty)
        {
            SchemaName = schemaName;
            TableName = tableName;
            ColumnName = columnName;
            PropertyName = propertyName;
            ExtendedProperty = extendedProperty;

            TableLevelExtendedComment = string.IsNullOrEmpty(columnName);
        }
    }
}