using System.Collections.Generic;

namespace Efrpg.LanguageMapping
{
    public class SqlServerToJavascript : IDatabaseToPropertyType
    {
        // [Database type] = Language type
        public Dictionary<string, string> GetMapping()
        {
            return new Dictionary<string, string>
            {
                { string.Empty,       "string" }, // default
                { "bigint",           "Number" },
                { "binary",           "string" },
                { "bit",              "boolean" },
                { "date",             "string" },
                { "datetime",         "string" },
                { "datetime2",        "string" },
                { "datetimeoffset",   "string" },
                { "decimal",          "Number" },
                { "float",            "Number" },
                { "geography",        Settings.DisableGeographyTypes ? string.Empty : "string" },
                { "geometry",         Settings.DisableGeographyTypes ? string.Empty : "string" },
                { "hierarchyid",      "string" },
                { "image",            "string" },
                { "int",              "Number" },
                { "money",            "Number" },
                { "numeric",          "Number" },
                { "real",             "Number" },
                { "smalldatetime",    "string" },
                { "smallint",         "Number" },
                { "smallmoney",       "Number" },
                { "table type",       string.Empty },
                { "time",             "string" },
                { "timestamp",        "string" },
                { "tinyint",          "Number" },
                { "uniqueidentifier", "string" },
                { "varbinary",        "string" },
                { "varbinary(max)",   "string" }
            };
        }
    }
}
