namespace Efrpg.Readers
{
    public class RawStoredProcedureReturnColumn
    {
        public string ColumnName { get; set; }
        public string DataTypeFullName { get; set; }
        public string DataTypeName { get; set; }
        public string DataTypeNamespace { get; set; }
        public string DataTypeAssemblyQualifiedName { get; set; }
        public bool AllowDBNull { get; set; }
        public bool Unique { get; set; }
        public byte Precision { get; set; }
        public byte Scale { get; set; }

        public RawStoredProcedureReturnColumn()
        {
            ColumnName = string.Empty;
            DataTypeFullName = string.Empty;
            DataTypeName = string.Empty;
            DataTypeNamespace = string.Empty;
            DataTypeAssemblyQualifiedName = string.Empty;
        }

        public RawStoredProcedureReturnColumn(
            string columnName,
            string dataTypeFullName,
            string dataTypeName,
            string dataTypeNamespace,
            string dataTypeAssemblyQualifiedName,
            bool allowDBNull,
            bool unique,
            byte precision,
            byte scale)
        {
            ColumnName = columnName;
            DataTypeFullName = dataTypeFullName;
            DataTypeName = dataTypeName;
            DataTypeNamespace = dataTypeNamespace;
            DataTypeAssemblyQualifiedName = dataTypeAssemblyQualifiedName;
            AllowDBNull = allowDBNull;
            Unique = unique;
            Precision = precision;
            Scale = scale;
        }
    }
}
