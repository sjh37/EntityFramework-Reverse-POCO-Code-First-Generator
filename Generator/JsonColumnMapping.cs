namespace Efrpg
{
    /// <summary>
    /// Defines a mapping between a JSON database column and a C# POCO class type.
    /// This allows JSON columns to be mapped to specific types instead of just string.
    /// </summary>
    public class JsonColumnMapping
    {
        /// <summary>
        /// The database schema name (e.g., "dbo"). Use "*" to match any schema.
        /// </summary>
        public string Schema;

        /// <summary>
        /// The table name. Use "*" to match any table.
        /// </summary>
        public string Table;

        /// <summary>
        /// The column name to apply the mapping to.
        /// </summary>
        public string Column;

        /// <summary>
        /// The C# type to use for this JSON column
        /// This type should be a valid C# type name including namespace if needed.
        /// </summary>
        // e.g., "MyCustomClass", "List<MyClass>", etc.
        public string PropertyType;

        /// <summary>
        /// Optional: Additional namespace(s) required for the PropertyType.
        /// Multiple namespaces can be separated by semicolons.
        /// </summary>
        public string AdditionalNamespace;
    }
}
