using System.Collections.Generic;
using System.Linq;

namespace Efrpg.Filtering
{
    // Unless table/column/stored proc/etc is explicitly listed here, it will be excluded.
    public class MultiContextSettings
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Namespace { get; set; } // Optional
        public string TemplatePath { get; set; }
        public string Filename { get; set; }

        // Defaults to use if not specified for an object
        public string BaseSchema { get; set; }

        public Dictionary<string, object> AllFields { get; set; } // Here you will find all fields, including any extra custom fields not listed above

        public List<MultiContextTableSettings> Tables { get; set; }
        public List<MultiContextStoredProcedureSettings> StoredProcedures { get; set; }
        public List<MultiContextFunctionSettings> Functions { get; set; }
        public List<EnumerationSettings> Enumerations { get; set; }
        public List<MultiContextForeignKeySettings> ForeignKeys { get; set; }

        public MultiContextSettings()
        {
            Tables = new List<MultiContextTableSettings>();
            StoredProcedures = new List<MultiContextStoredProcedureSettings>();
            Functions = new List<MultiContextFunctionSettings>();
            Enumerations = new List<EnumerationSettings>();
            ForeignKeys = new List<MultiContextForeignKeySettings>();
        }

        public bool IncludeViews()
        {
            return Tables.Any();
        }

        public bool IncludeStoredProcedures()
        {
            return StoredProcedures.Any();
        }

        public bool IncludeFunctions()
        {
            return Functions.Any();
        }

        public string GetNamespace()
        {
            if (!string.IsNullOrEmpty(Namespace))
                return Namespace;

            return !string.IsNullOrEmpty(Name) ? Name : string.Empty;
        }
    }

    public class MultiContextTableSettings
    {
        public string Name { get; set; }
        public string Description { get; set; } // [optional] Comment added to table class
        public string PluralName { get; set; } // [optional] Override auto-plural name
        public string DbName { get; set; } // [optional] Name of table in database. Specify only if the db table name is different from the "Name" property.
        public string Attributes { get; set; } // [optional] Use a tilda ~ delimited list of attributes to add to this table property. e.g. [CustomSecurity(Security.ReadOnly)]~[AnotherAttribute]~[Etc]
                                               //            The tilda ~ delimiter used in Attributes can be changed if you set Settings.MultiContextAttributeDelimiter = '~'; to something else.
        public string DbSetModifier { get; set; } // [optional] Will override setting of table.DbSetModifier. Default is "public".

        public Dictionary<string, object> AllFields { get; set; } // Here you will find all fields, including any extra custom fields not listed above
        public List<MultiContextColumnSettings> Columns { get; set; }
    }

    public class MultiContextColumnSettings
    {
        public string Name { get; set; }
        public string DbName { get; set; } // [optional] Name of column in database. Specify only if the db column name is different from the "Name" property.
        public bool? IsPrimaryKey { get; set; } // [optional] Useful for views as views don't have primary keys.
        public bool? OverrideModifier { get; set; } // [optional] Adds "override" modifier.
        public string EnumType { get; set; } // [optional] Use enum type instead of data type
        public string Attributes { get; set; } // [optional] Use a tilda ~ delimited list of attributes to add to a poco property. e.g. [CustomSecurity(Security.ReadOnly)]~[Required]
        public string PropertyType { get; set; } // [optional] Will override setting of column.PropertyType
        public bool? IsNullable { get; set; } // [optional] Will override setting of column.IsNullable

        public Dictionary<string, object> AllFields { get; set; } // Here you will find all fields, including any extra custom fields not listed above
    }

    public class MultiContextStoredProcedureSettings
    {
        public string Name { get; set; }
        public string DbName { get; set; } // [optional] Name of stored proc in database. Specify only if the db stored proc name is different from the "Name" property.
        public string ReturnModel { get; set; } // [optional] Specify a return model for stored proc

        public Dictionary<string, object> AllFields { get; set; } // Here you will find all fields, including any extra custom fields not listed above
    }

    public class MultiContextFunctionSettings
    {
        public string Name { get; set; }
        public string DbName { get; set; } // [optional] Name of function in database. Specify only if the db function name is different from the "Name" property.

        public Dictionary<string, object> AllFields { get; set; } // Here you will find all fields, including any extra custom fields not listed above
    }

    /// <summary>
    /// Create enumeration from database table
    /// public enum Name
    /// {
    ///     NameField = ValueField,
    ///     etc
    /// }
    /// </summary>
    public class EnumerationSettings
    {
        public string Name { get; set; } // Enum to generate. e.g. "DaysOfWeek" would result in "public enum DaysOfWeek {...}" if the GroupField is set to a value then {GroupField} must be used in this name. e.g. "DaysOfWeek{GroupField}"
        public string Table { get; set; } // Database table containing enum values. e.g. "DaysOfWeek"
        public string NameField { get; set; } // Column containing the name for the enum. e.g. "TypeName"
        public string ValueField { get; set; } // Column containing the values for the enum. e.g. "TypeId"
        public string GroupField { get; set; } // [optional] Column containing the group name for the enum. This is used if multiple Enums are in the same table. if this is populated, use {GroupField} in the Name property. e.g. "{GroupField}Enum"

        public Dictionary<string, object> AllFields { get; set; } // Here you will find all fields, including any extra custom fields not listed above
    }

    /// <summary>
    /// Existing foreign keys will be read and used as normal from the source database, however you can specify extra foreign keys here.
    /// Define extra navigation relationships, such as views, since views don’t have relationships.
    /// Specify names as defined in the database, not how they will be named in C#
    /// </summary>
    public class MultiContextForeignKeySettings
    {
        public string ConstraintName { get; set; } // Name of the foreign key
        public string ParentName { get; set; } // [optional] Name of the parent foreign key property. If NULL it will be generated.
        public string ChildName { get; set; } // [optional] Name of the child foreign key property. If NULL it will be generated.

        public string PkSchema { get; set; } // [optional] Will default to MultiContext.Context.BaseSchema
        public string PkTableName { get; set; }
        public string PkColumn { get; set; }

        public string FkSchema { get; set; } // [optional] Will default to MultiContext.Context.BaseSchema
        public string FkTableName { get; set; }
        public string FkColumn { get; set; }

        public int Ordinal { get; set; } // Order of this item
        public bool CascadeOnDelete { get; set; } // If false will add .WillCascadeOnDelete(false)
        public bool IsNotEnforced { get; set; } // If not enforced, it means foreign key is optional. .HasOptional(...) or .HasRequired(...)
        public bool HasUniqueConstraint { get; set; } // True if this FK points to columns that have a unique constraint against them
    }
}