using System.Collections.Generic;

namespace Efrpg
{
    /// <summary>
    /// Represents a group of table columns that have been mapped to an EF Core owned entity
    /// via a <see cref="OwnedEntityMapping"/>. Stored on <see cref="Table.OwnedEntities"/>.
    /// </summary>
    public class OwnedEntity
    {
        /// <summary>The navigation property name on the owning entity (e.g. "BillingAddress").</summary>
        public string PropertyName;

        /// <summary>The C# type name of the owned entity class (e.g. "Address").</summary>
        public string PropertyType;

        /// <summary>The column prefix used for matching (e.g. "BillingAddress_").</summary>
        public string ColumnPrefix;

        /// <summary>The database columns that form this owned entity.</summary>
        public List<Column> Columns;
    }
}
