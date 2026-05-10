namespace Efrpg
{
    /// <summary>
    /// Defines a mapping that groups database columns sharing a common prefix into an EF Core owned entity.
    /// Columns whose DbName starts with <see cref="ColumnPrefix"/> are hidden from direct POCO generation
    /// and instead contribute to an owned entity property of type <see cref="PropertyType"/>.
    /// A <c>builder.OwnsOne(...)</c> configuration block is generated automatically.
    /// </summary>
    public class OwnedEntityMapping
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
        /// The column name prefix used to identify which columns belong to this owned entity.
        /// All columns whose <c>DbName</c> starts with this value (case-insensitive) are grouped.
        /// Trailing underscores or separators are stripped when deriving owned entity property names.
        /// Example: "BillingAddress_" matches columns BillingAddress_Street, BillingAddress_City, etc.
        /// </summary>
        public string ColumnPrefix;

        /// <summary>
        /// The name of the navigation property to generate on the owning entity.
        /// Example: "BillingAddress" generates <c>public Address BillingAddress { get; set; }</c>.
        /// </summary>
        public string PropertyName;

        /// <summary>
        /// The C# type name of the owned entity class (e.g., "Address").
        /// The generator automatically produces one class file per unique type name found across all mappings.
        /// If you prefer to supply the class yourself, place it outside the generated output folder.
        /// </summary>
        public string PropertyType;

    }
}
