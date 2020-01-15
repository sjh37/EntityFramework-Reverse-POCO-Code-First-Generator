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
                { string.Empty,        "string" }, // default
                { "bigint",            "long" },
                { "bit varying",       "BitArray" },
                { "bit(1)",            "bool" },
                { "bit(n)",            "BitArray" },
                { "boolean",           "bool" },
                { "box",               "NpgsqlBox" },
                { "bytea",             "byte[]" },
                { "char",              "char" },
                { "character varying", "string" },
                { "character",         "string" },
                { "cid",               "uint" },
                { "cidr",              "NpgsqlInet" },
                { "circle",            "NpgsqlCircle" },
                { "citext",            "string" },
                { "date",              "DateTime" },
                { "double precision",  "double" },
                { "geometry",          "PostgisGeometry" },
                { "hstore",            "Dictionary<string, string>" },
                { "inet",              "NpgsqlInet" },
                { "integer",           "int" },
                { "interval",          "TimeSpan" },
                { "json",              "string" },
                { "jsonb",             "string" },
                { "line",              "NpgsqlLine" },
                { "lseg",              "NpgsqlLSeg" },
                { "macaddr",           "PhysicalAddress" },
                { "money",             "decimal" },
                { "name",              "string" },
                { "numeric",           "decimal" },
                { "oid",               "uint" },
                { "oidvector",         "uint[]" },
                { "path",              "NpgsqlPath" },
                { "point",             "NpgsqlPoint" },
                { "polygon",           "NpgsqlPolygon" },
                { "real",              "float" },
                { "record",            "object[]" },
                { "smallint",          "short" },
                { "text",              "string" },
                { "time",              "TimeSpan" },
                { "timestamp",         "DateTime" },
                { "tsquery",           "NpgsqlTsQuery" },
                { "tsvector",          "NpgsqlTsVector" },
                { "uuid",              "Guid" },
                { "xid",               "uint" },
                { "xml",               "string" }

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