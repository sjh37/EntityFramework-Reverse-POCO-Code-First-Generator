namespace Efrpg.Readers
{
    public class RawTable
    {
        // Table
        public readonly string SchemaName;
        public readonly string TableName;
        public readonly bool   IsView;

        // Column
        public readonly int    Scale;
        public readonly string TypeName;
        public readonly bool   IsNullable;
        public readonly int    MaxLength;
        public readonly int    DateTimePrecision;
        public readonly int    Precision;
        public readonly bool   IsIdentity;
        public readonly bool   IsComputed;
        public readonly bool   IsRowGuid;
        public readonly byte   GeneratedAlwaysType;
        public readonly bool   IsStoreGenerated;
        public readonly int    PrimaryKeyOrdinal;
        public readonly bool   PrimaryKey;
        public readonly bool   IsForeignKey;
        public readonly int    Ordinal;
        public readonly string ColumnName;
        public readonly string Default;

        public RawTable(string schemaName, string tableName, bool isView, int scale,
            string typeName, bool isNullable, int maxLength, int dateTimePrecision, int precision,
            bool isIdentity, bool isComputed, bool isRowGuid, byte generatedAlwaysType,
            bool isStoreGenerated, int primaryKeyOrdinal, bool primaryKey, bool isForeignKey,
            int ordinal, string columnName, string @default)
        {
            // Table
            SchemaName = schemaName;
            TableName  = tableName;
            IsView     = isView;

            // Column
            Scale               = scale;
            TypeName            = typeName;
            IsNullable          = isNullable;
            MaxLength           = maxLength;
            DateTimePrecision   = dateTimePrecision;
            Precision           = precision;
            IsIdentity          = isIdentity;
            IsComputed          = isComputed;
            IsRowGuid           = isRowGuid;
            GeneratedAlwaysType = generatedAlwaysType;
            IsStoreGenerated    = isStoreGenerated;
            PrimaryKeyOrdinal   = primaryKeyOrdinal;
            PrimaryKey          = primaryKey;
            IsForeignKey        = isForeignKey;
            Ordinal             = ordinal;
            ColumnName          = columnName;
            Default             = @default;
        }
    }
}