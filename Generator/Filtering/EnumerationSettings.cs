namespace Efrpg.Filtering
{
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
        public string DescriptionField { get; set; } // [optional] Column containing the description for each enum member. When set, a [Description("...")] attribute is emitted for members whose description is non-empty.
        public bool GenerateDescriptionFromName { get; set; } // [optional] If true and no description is available from DescriptionField, a [Description("...")] attribute is generated from the enum member name converted to human-readable text.
    }
}
