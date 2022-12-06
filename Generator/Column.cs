﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Efrpg.Readers;

namespace Efrpg
{
    public class Column : EntityName
    {
        public string DisplayName; // Name used in the data annotation [Display(Name = "<DisplayName> goes here")]
        public bool OverrideModifier = false; // Adds 'override' to the property declaration
        public List<string> Attributes = new List<string>(); // List of attributes to add to this columns poco property
        public bool Hidden; // If true, does not generate any code for this column.
        public bool ExistsInBaseClass; // If true, does not generate the property for this column as it will exist in a base class

        public int Scale;
        public string PropertyType;
        public string SqlPropertyType;

        public int DateTimePrecision;
        public string Default;
        public string HasDefaultValueSql;
        public int MaxLength;
        public int Precision;
        public int Ordinal;
        public int PrimaryKeyOrdinal;
        public string ExtendedProperty;
        public string SummaryComments;
        public string InlineComments;
        public string UniqueIndexName;
        public bool AllowEmptyStrings = true;

        public bool IsIdentity;
        public bool IsRowGuid;
        public bool IsComputed;
        public ColumnGeneratedAlwaysType GeneratedAlwaysType;
        public bool IsNullable;
        public bool IsPrimaryKey;
        public bool IsUniqueConstraint;
        public bool IsUnique;
        public bool IsStoreGenerated;
        public bool IsRowVersion;
        public bool IsConcurrencyToken; //  Manually set via callback
        public bool IsFixedLength;
        public bool IsUnicode;
        public bool IsMaxLength;
        public bool IsForeignKey;
        public bool IsSpatial;

        public string Config;
        public List<string> ConfigFk = new List<string>();
        public List<PropertyAndComments> EntityFk = new List<PropertyAndComments>();

        public List<RawIndex> Indexes = new List<RawIndex>();

        public Table ParentTable;

        public static readonly List<string> NotNullable = new List<string>
        {
            Settings.AllowNullStrings ? "" : "string",
            "byte[]",
            "datatable",
            "system.data.datatable",
            "object",
            "microsoft.sqlserver.types.sqlgeography",
            "microsoft.sqlserver.types.sqlgeometry",
            "sqlgeography",
            "sqlgeometry",
            "system.data.entity.spatial.dbgeography",
            "system.data.entity.spatial.dbgeometry",
            "dbgeography",
            "dbgeometry",
            "system.data.entity.hierarchy.hierarchyid",
            "hierarchyid",
            "nettopologysuite.geometries.point",
            "nettopologysuite.geometries.geometry"
        };
        
        public static readonly List<string> StoredProcedureNotNullable = new List<string>
        {
            "string",
            "byte[]",
            "datatable",
            "system.data.datatable",
            "object",
            "microsoft.sqlserver.types.sqlgeography",
            "microsoft.sqlserver.types.sqlgeometry",
            "sqlgeography",
            "sqlgeometry",
            "system.data.entity.spatial.dbgeography",
            "system.data.entity.spatial.dbgeometry",
            "dbgeography",
            "dbgeometry",
            "system.data.entity.hierarchy.hierarchyid",
            "hierarchyid",
            "nettopologysuite.geometries.point",
            "nettopologysuite.geometries.geometry"
        };

        public static readonly List<string> CanUseSqlServerIdentityColumn = new List<string>
        {
            "sbyte",
            "short",
            "smallint",
            "int",
            "long"
        };
        
        public static readonly List<string> ExcludedHasColumnType = new List<string>
        {
            "user-defined"
        };

        public void ResetNavigationProperties()
        {
            ConfigFk = new List<string>();
            EntityFk = new List<PropertyAndComments>();
        }

        public bool IsColumnNullable()
        {
            return IsNullable && !NotNullable.Contains(PropertyType.ToLower());
        }

        public void CleanUpDefault()
        {
            if (string.IsNullOrWhiteSpace(Default) || IsSpatial)
            {
                Default = string.Empty;
                return;
            }

            // Remove outer brackets
            while (Default.First() == '(' && Default.Last() == ')' && Default.Length > 2)
            {
                Default = Default.Substring(1, Default.Length - 2);
            }

            // Check for sequence
            var lower = Default.ToLower();
            if (lower.Contains("next value for"))
            {
                HasDefaultValueSql = Default.Trim();
                Default = string.Empty;
                return;
            }

            // Remove unicode prefix
            if (IsUnicode && Default.StartsWith("N") &&
                !Default.Equals("NULL", StringComparison.InvariantCultureIgnoreCase))
                Default = Default.Substring(1, Default.Length - 1);

            if (Default.First() == '\'' && Default.Last() == '\'' && Default.Length >= 2)
                Default = string.Format("\"{0}\"", Default.Substring(1, Default.Length - 2));

            lower = Default.ToLower();
            var lowerPropertyType = PropertyType.ToLower();

            // Cleanup default
            switch (lowerPropertyType)
            {
                case "bool":
                    Default = (Default == "0" || lower == "\"false\"" || lower == "false") ? "false" : "true";
                    break;

                case "string":
                case "datetime":
                case "datetime2":
                case "system.datetime":
                case "timespan":
                case "system.timespan":
                case "datetimeoffset":
                case "system.datetimeoffset":
                    if (Default.First() != '"')
                        Default = string.Format("\"{0}\"", Default);
                    if (Default.Contains('\\') || Default.Contains('\r') || Default.Contains('\n'))
                        Default = "@" + Default;
                    else
                        Default = string.Format("\"{0}\"",
                            Default.Substring(1, Default.Length - 2)
                                .Replace("\"", "\\\"")); // #281 Default values must be escaped if contain double quotes
                    break;

                case "long":
                case "short":
                case "int":
                case "double":
                case "float":
                case "decimal":
                case "byte":
                case "guid":
                case "system.guid":
                    if (Default.First() == '\"' && Default.Last() == '\"' && Default.Length > 2)
                        Default = Default.Substring(1, Default.Length - 2);
                    break;

                case "byte[]":
                case "system.data.entity.spatial.dbgeography":
                case "system.data.entity.spatial.dbgeometry":
                case "nettopologysuite.geometries.point":
                case "nettopologysuite.geometries.geometry":
                    Default = string.Empty;
                    break;
            }

            // Ignore defaults we cannot interpret (we would need SQL to C# compiler)
            if (lower.StartsWith("create default"))
            {
                Default = string.Empty;
                return;
            }

            if (string.IsNullOrWhiteSpace(Default))
            {
                Default = string.Empty;
                return;
            }

            // Validate default
            switch (lowerPropertyType)
            {
                case "long":
                    long l;
                    if (!long.TryParse(Default, out l))
                        Default = string.Empty;
                    break;

                case "short":
                    short s;
                    if (!short.TryParse(Default, out s))
                        Default = string.Empty;
                    break;

                case "int":
                    int i;
                    if (!int.TryParse(Default, out i))
                        Default = string.Empty;
                    break;

                case "datetime":
                case "datetime2":
                case "system.datetime":
                    DateTime dt;
                    if (!DateTime.TryParse(Default, out dt))
                        Default = (lower.Contains("getdate()") || lower.Contains("sysdatetime"))
                            ? "DateTime.Now"
                            : (lower.Contains("getutcdate()") || lower.Contains("sysutcdatetime"))
                                ? "DateTime.UtcNow"
                                : string.Empty;
                    else
                        Default = string.Format("DateTime.Parse({0})", Default);
                    break;

                case "datetimeoffset":
                case "system.datetimeoffset":
                    DateTimeOffset dto;
                    if (!DateTimeOffset.TryParse(Default, out dto))
                        Default = (lower.Contains("getdate()") || lower.Contains("sysdatetimeoffset"))
                            ? "DateTimeOffset.Now"
                            : (lower.Contains("getutcdate()") || lower.Contains("sysutcdatetime"))
                                ? "DateTimeOffset.UtcNow"
                                : string.Empty;
                    else
                        Default = string.Format("DateTimeOffset.Parse({0})", Default);
                    break;

                case "timespan":
                case "system.timespan":
                    TimeSpan ts;
                    Default = TimeSpan.TryParse(Default, out ts)
                        ? string.Format("TimeSpan.Parse({0})", Default)
                        : string.Empty;
                    break;

                case "double":
                    double d;
                    if (!double.TryParse(Default, out d))
                        Default = string.Empty;
                    if (Default.ToLowerInvariant().EndsWith("."))
                        Default += "0";
                    break;

                case "float":
                    float f;
                    if (!float.TryParse(Default, out f))
                        Default = string.Empty;
                    if (!Default.ToLowerInvariant().EndsWith("f"))
                        Default += "f";
                    break;

                case "decimal":
                    decimal dec;
                    if (!decimal.TryParse(Default, out dec))
                        Default = string.Empty;
                    else
                        Default += "m";
                    break;

                case "byte":
                    byte b;
                    if (!byte.TryParse(Default, out b))
                        Default = string.Empty;
                    break;

                case "bool":
                    bool x;
                    if (!bool.TryParse(Default, out x))
                        Default = string.Empty;
                    break;

                case "string":
                    if (lower.Contains("newid()") || lower.Contains("newsequentialid()"))
                        Default = "Guid.NewGuid().ToString()";
                    if (lower.StartsWith("space("))
                        Default = "\"\"";
                    if (lower == "null")
                        Default = string.Empty;
                    break;

                case "guid":
                case "system.guid":
                    if (lower.Contains("newid()") || lower.Contains("newsequentialid()"))
                        Default = "Guid.NewGuid()";
                    else if (lower.Contains("null"))
                        Default = "null";
                    else
                        Default = string.Format("Guid.Parse(\"{0}\")", Default);
                    break;
            }
        }

        public static string ToDisplayName(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            var sb = new StringBuilder(30);
            str = Regex.Replace(str, @"[^a-zA-Z0-9]", " "); // Anything that is not a letter or digit, convert to a space
            str = Regex.Replace(str, @"([A-Z])([A-Z])([a-z])|([a-z])([A-Z])", "$1$4 $2$3$5"); // Add space between case changes
            
            var hasUpperCased = false;
            var lastChar = '\0';
            foreach (var original in str.Trim())
            {
                var c = original;
                if (lastChar == '\0')
                {
                    c = char.ToUpperInvariant(original);
                }
                else
                {
                    var isLetter = char.IsLetter(original);
                    var isDigit = char.IsDigit(original);
                    var isWhiteSpace = !isLetter && !isDigit;

                    // Is this char is different to last time
                    var isDifferent = false;
                    if (isLetter && !char.IsLetter(lastChar))
                        isDifferent = true;
                    else if (isDigit && !char.IsDigit(lastChar))
                        isDifferent = true;
                    else if (char.IsUpper(original) && !char.IsUpper(lastChar))
                        isDifferent = true;

                    if (isDifferent || isWhiteSpace)
                        sb.Append(' '); // Add a space

                    if (hasUpperCased && isLetter)
                        c = char.ToLowerInvariant(original);
                }
                lastChar = original;
                if (!hasUpperCased && char.IsUpper(c))
                    hasUpperCased = true;
                sb.Append(c);
            }
            str = sb.ToString();
            str = Regex.Replace(str, @"\s+", " ").Trim(); // Multiple white space to one space
            str = Regex.Replace(str, @"\bid\b", "ID"); //  Make ID word uppercase
            return str;
        }

        public string WrapIfNullable()
        {
            if (!IsColumnNullable())
                return PropertyType;

            return string.Format(Settings.NullableShortHand ? "{0}?" : "System.Nullable<{0}>", PropertyType);
        }
    }
}