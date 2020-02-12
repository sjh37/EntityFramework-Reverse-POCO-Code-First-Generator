using System.Collections.Generic;

namespace Efrpg.LanguageMapping
{
    public class PostgresToCSharp : IDatabaseToPropertyType
    {
        // [Database type] = Language type
        public Dictionary<string, string> GetMapping()
        {
            return new Dictionary<string, string>
            {
                { string.Empty,                  "string" }, // default
                { "bigint",                      "long" },
                { "bigserial",                   "long" },
                { "bit varying",                 "BitArray" },
                { "bit(1)",                      "bool" },
                { "bit(n)",                      "BitArray" },
                { "bool",                        "bool" },
                { "boolean",                     "bool" },
                { "box",                         "NpgsqlBox" },
                { "bytea",                       "byte[]" },
                { "char",                        "char" },
                { "character varying",           "string" },
                { "character",                   "string" },
                { "cid",                         "uint" },
                { "cidr",                        "NpgsqlInet" },
                { "circle",                      "NpgsqlCircle" },
                { "citext",                      "string" },
                { "date",                        "DateTime" },
                { "decimal",                     "decimal" },
                { "double precision",            "double" },
                { "float4",                      "float" },
                { "float8",                      "double" },
                { "geometry",                    Settings.DisableGeographyTypes ? string.Empty : "PostgisGeometry" },
                { "hstore",                      "Dictionary<string, string>" },
                { "inet",                        "NpgsqlInet" },
                { "int",                         "int" },
                { "int2",                        "short" },
                { "int4",                        "int" },
                { "int8",                        "long" },
                { "integer",                     "int" },
                { "interval",                    "TimeSpan" },
                { "json",                        "string" },
                { "jsonb",                       "string" },
                { "line",                        "NpgsqlLine" },
                { "lseg",                        "NpgsqlLSeg" },
                { "macaddr",                     "PhysicalAddress" },
                { "money",                       "decimal" },
                { "name",                        "string" },
                { "numeric",                     "decimal" },
                { "oid",                         "uint" },
                { "oidvector",                   "uint[]" },
                { "path",                        "NpgsqlPath" },
                { "point",                       "NpgsqlPoint" },
                { "polygon",                     "NpgsqlPolygon" },
                { "real",                        "float" },
                { "record",                      "object[]" },
                { "serial",                      "int" },
                { "serial4",                     "int" },
                { "serial8",                     "long" },
                { "smallint",                    "short" },
                { "text",                        "string" },
                { "time",                        "TimeSpan" },
                { "time with time zone",         "TimeSpan" },
                { "time without time zone",      "TimeSpan" },
                { "timetz",                      "TimeSpan" },
                { "timestamp",                   "DateTime" },
                { "timestamp with time zone",    "DateTime" },
                { "timestamp without time zone", "DateTime" },
                { "timestamptz",                 "DateTime" },
                { "tsquery",                     "NpgsqlTsQuery" },
                { "tsvector",                    "NpgsqlTsVector" },
                { "uuid",                        "Guid" },
                { "varbit",                      "BitArray" },
                { "xid",                         "uint" },
                { "xml",                         "string" }

                //{ "composite types",      "T" },
                //{ "range subtypes",     "NpgsqlRange<TElement>" },
                //{ "enum types", "TEnum" },
                //{ "array types",        "Array (of element type)" },
            };
        }

        public List<string> SpatialTypes()
        {
            return new List<string> { "geometry", "point", "line", "lseg", "box", "path", "polygon", "circle" };
        }
    }
}