using System.Collections.Generic;

namespace Efrpg.LanguageMapping
{
    public class SqlServerToCSharp : IDatabaseToPropertyType
    {
        // [Database type] = Language type
        public Dictionary<string, string> GetMapping()
        {
            return new Dictionary<string, string>
            {
                { string.Empty,       "string" }, // default
                { "bigint",           "long" },
                { "binary",           "byte[]" },
                { "bit",              "bool" },
                { "date",             "DateTime" },
                { "datetime",         "DateTime" },
                { "datetime2",        "DateTime" },
                { "datetimeoffset",   "DateTimeOffset" },
                { "decimal",          "decimal" },
                { "float",            "double" },
                { "geography",        Settings.DisableGeographyTypes ? string.Empty : "Spatial.DbGeography" },
                { "geometry",         Settings.DisableGeographyTypes ? string.Empty : "Spatial.DbGeometry" },
                { "hierarchyid",      "Hierarchy.HierarchyId" },
                { "image",            "byte[]" },
                { "int",              "int" },
                { "money",            "decimal" },
                { "numeric",          "decimal" },
                { "real",             "float" },
                { "smalldatetime",    "DateTime" },
                { "smallint",         "short" },
                { "smallmoney",       "decimal" },
                { "table type",       "DataTable" },
                { "time",             "TimeSpan" },
                { "timestamp",        "byte[]" },
                { "tinyint",          "byte" },
                { "uniqueidentifier", "Guid" },
                { "varbinary",        "byte[]" },
                { "varbinary(max)",   "byte[]" }
            };
        }
    }
}