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
                { string.Empty,                     "string" },   // default
                { "bfile",                          "byte[]" },
                { "binary_double",                  "double" },   // IEEE 754 double precision
                { "binary_float",                   "float" },    // IEEE 754 single precision
                { "binary_integer",                 "int" },      // PL/SQL 32-bit integer
                { "blob",                           "byte[]" },
                { "char",                           "string" },
                { "clob",                           "string" },
                { "date",                           "DateTime" }, // Oracle DATE includes time component
                { "decimal",                        "decimal" },
                { "double precision",               "double" },   // alias for BINARY_DOUBLE
                { "float",                          "double" },   // ANSI float = binary double
                { "int",                            "long" },     // Oracle INT = NUMBER(38)
                { "integer",                        "long" },     // Oracle INTEGER = NUMBER(38)
                { "interval day to second",         "TimeSpan" },
                { "interval year to month",         "string" },   // no .NET equivalent; use string
                { "long raw",                       "byte[]" },
                { "long",                           "string" },   // Oracle LONG is a deprecated character type
                { "nchar",                          "string" },
                { "nclob",                          "string" },
                { "number",                         "decimal" },
                { "numeric",                        "decimal" },
                { "nvarchar2",                      "string" },
                { "pls_integer",                    "int" },      // PL/SQL 32-bit integer
                { "raw",                            "byte[]" },
                { "real",                           "float" },    // ANSI REAL = single precision
                { "rowid",                          "string" },
                { "smallint",                       "long" },     // Oracle SMALLINT = NUMBER(38)
                { "timestamp with local time zone", "DateTimeOffset" },
                { "timestamp with time zone",       "DateTimeOffset" },
                { "timestamp",                      "DateTime" },
                { "urowid",                         "string" },
                { "varchar",                        "string" },
                { "varchar2",                       "string" },
                { "xmltype",                        "string" }
            };
        }

        public List<string> SpatialTypes()
        {
            return new List<string> { "sdo_geometry" };
        }

        public List<string> PrecisionTypes()
        {
            return new List<string> { "float", "timestamp", "timestamp with time zone", "timestamp with local time zone" };
        }

        public List<string> PrecisionAndScaleTypes()
        {
            return new List<string> { "number", "decimal", "numeric" };
        }
    }
}