using System.Collections.Generic;
using Efrpg.Templates;

namespace Efrpg.LanguageMapping
{
    public class MySqlToCSharp : IDatabaseToPropertyType
    {
        public Dictionary<string, string> GetMapping()
        {
            var geographyType = Settings.TemplateType == TemplateType.Ef6 ? "DbGeography" : "NetTopologySuite.Geometries.Point";
            var geometryType = Settings.TemplateType == TemplateType.Ef6 ? "DbGeometry" : "NetTopologySuite.Geometries.Geometry";

            // [Database type] = Language type
            return new Dictionary<string, string>
            {
                { string.Empty,        "string" }, // default
                { "bigint unsigned",   "decimal" },
                { "bigint",            "long" },
                { "binary",            "byte[]" },
                { "bit",               "long" },
                { "bit(1)",            "bool" },
                { "blob",              "byte[]" },
                { "bool",              "bool" },
                { "boolean",           "bool" },
                { "char byte",         "byte[]" },
                { "char",              "string" },
                { "character varying", "string" },
                { "date",              "DateTime" },
                { "datetime",          "DateTime" },
                { "datetimeoffset",    "DateTimeOffset" },
                { "dec",               "decimal" },
                { "decimal",           "decimal" },
                { "double unsigned",   "decimal" },
                { "double",            "double" },
                { "enum",              "string" },
                { "fixed",             "decimal" },
                { "float unsigned",    "decimal" },
                { "float",             "double" },
                { "geography",         Settings.DisableGeographyTypes ? string.Empty : geographyType },
                { "geometry",          Settings.DisableGeographyTypes ? string.Empty : geometryType },
                { "int unsigned",      "long" },
                { "int",               "int" },
                { "integer unsigned",  "long" },
                { "integer",           "int" },
                { "longblob",          "byte[]" },
                { "longtext",          "string" },
                { "mediumblob",        "byte[]" },
                { "mediumint",         "int" },
                { "mediumtext",        "string" },
                { "national char",     "string" },
                { "national varchar",  "string" },
                { "nchar",             "string" },
                { "numeric",           "decimal" },
                { "nvarchar",          "string" },
                { "real",              "double" },
                { "serial",            "decimal" },
                { "set",               "string" },
                { "smallint unsigned", "int" },
                { "smallint",          "short" },
                { "text",              "string" },
                { "time",              "TimeSpan" },
                { "timestamp",         "DateTime" },
                { "tinyblob",          "byte[]" },
                { "tinyint unsigned",  "byte" },
                { "tinyint",           "SByte" },
                { "tinytext",          "string" },
                { "varbinary",         "byte[]" },
                { "varchar",           "string" },
                { "year",              "short" }
            };
        }

        public List<string> SpatialTypes()
        {
            return new List<string>
            {
                "geography", "geometry", "point", "linestring", "polygon", "multipoint", "multilinestring", "multipolygon", "geometrycollection"
            };
        }

        public List<string> PrecisionTypes()
        {
            return new List<string> { "float", "datetime", "time", "timestamp", "year" };
        }

        public List<string> PrecisionAndScaleTypes()
        {
            return new List<string> { "decimal", "numeric" };
        }
    }
}