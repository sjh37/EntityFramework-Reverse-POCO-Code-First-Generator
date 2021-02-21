namespace Efrpg.Readers
{
    public class RawIndex
    {
        public readonly string Schema;
        public readonly string TableName;
        public readonly string IndexName;
        public readonly byte   KeyOrdinal;
        public readonly string ColumnName;
        public readonly int    ColumnCount;
        public readonly bool   IsUnique;
        public readonly bool   IsPrimaryKey;
        public readonly bool   IsUniqueConstraint;
        public readonly bool   IsClustered;

        public RawIndex(string schema, string tableName, string indexName, byte keyOrdinal,
            string columnName, int columnCount, bool isUnique, bool isPrimaryKey,
            bool isUniqueConstraint, bool isClustered)
        {
            Schema             = schema;
            TableName          = tableName;
            IndexName          = indexName;
            KeyOrdinal         = keyOrdinal;
            ColumnName         = columnName;
            ColumnCount        = columnCount;
            IsUnique           = isUnique;
            IsPrimaryKey       = isPrimaryKey;
            IsUniqueConstraint = isUniqueConstraint;
            IsClustered        = isClustered;
        }

        public string Dump()
        {
            return string.Format("new RawIndex(\"{0}\", \"{1}\", \"{2}\", {3}, \"{4}\", {5}, {6}, {7}, {8}, {9}),",
                Schema, TableName, IndexName, KeyOrdinal, ColumnName, ColumnCount, IsUnique ? "true" : "false", IsPrimaryKey ? "true" : "false",
                IsUniqueConstraint ? "true" : "false", IsClustered ? "true" : "false");
        }
    }
}