using System.Collections.Generic;

namespace Efrpg.LanguageMapping
{
    public class OracleToCSharp : IDatabaseToPropertyType
    {
        // [Database type] = Language type
        public Dictionary<string, string> GetMapping()
        {
            return new Dictionary<string, string>
            {
                { string.Empty,                     "string" }, // default
                { "binary_double",                  "decimal" },
                { "binary_float",                   "double" },
                { "binary_integer",                 "long" },
                { "blob",                           "byte[]" },
                { "char",                           "string" },
                { "clob",                           "string" },
                { "date",                           "DateTime" },
                { "float",                          "double" },
                { "interval day to second",         "decimal" },
                { "interval year to month",         "decimal" },
                { "long raw",                       "byte[]" },
                { "long",                           "long" },
                { "nchar",                          "string" },
                { "nclob",                          "string" },
                { "number",                         "decimal" },
                { "nvarchar2",                      "string" },
                { "pls_integer",                    "long" },
                { "raw",                            "byte[]" },
                { "real",                           "float" },
                { "rowid",                          "string" },
                { "timestamp with local time zone", "DateTime" },
                { "timestamp with time zone",       "DateTime" },
                { "timestamp",                      "DateTime" },
                { "urowid",                         "string" },
                { "varchar2",                       "string" },
                { "xmltype",                        "string" }
            };
        }

        public List<string> SpatialTypes()
        {
            return new List<string> { "sdo_geometry" };
        }
    }
}