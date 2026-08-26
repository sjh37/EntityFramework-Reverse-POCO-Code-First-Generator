using System.Collections.Generic;

namespace Efrpg.Readers
{
    // Raw enumeration rows read from the database by the efrpg tool.
    // The Generator transforms these into Enumeration/EnumerationMember objects,
    // applying casing (Inflector) and description rules on the T4 side. See EnumBuilder.
    public class RawEnum
    {
        public int Index { get; set; } // Index into the EnumerationSettings list that was sent to the tool
        public List<RawEnumRow> Rows { get; set; } = new List<RawEnumRow>();
    }

    public class RawEnumRow
    {
        public string Name { get; set; } = string.Empty;  // raw value of EnumerationSettings.NameField
        public string Value { get; set; } = string.Empty; // raw value of EnumerationSettings.ValueField
        public string Group { get; set; } = string.Empty; // raw value of EnumerationSettings.GroupField (empty if not used)
        public Dictionary<string, string> AllValues { get; set; } = new Dictionary<string, string>(); // every column from ordinal 2 onwards
    }
}
