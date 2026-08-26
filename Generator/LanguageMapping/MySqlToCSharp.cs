using System.Collections.Generic;

namespace Efrpg.LanguageMapping
{
    public class MySqlToCSharp : IDatabaseToPropertyType
    {
        public Dictionary<string, string> GetMapping()
        {
            var isEf6 = Settings.IsEf6();
            var geographyType = isEf6 ? "DbGeography" : "NetTopologySuite.Geometries.Point";
            var geometryType = isEf6 ? "DbGeometry" : "NetTopologySuite.Geometries.Geometry";
            var nts = "NetTopologySuite.Geometries."; // EF6 has one type for every shape, EF Core has one each

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
                { "decimal unsigned",  "decimal" },
                { "decimal",           "decimal" },
                { "double unsigned",   "decimal" },
                { "double",            "double" },
                { "enum",              "string" },
                { "fixed",             "decimal" },
                { "float unsigned",    "decimal" },
                { "float",             "double" },
                { "geography",         Settings.DisableGeographyTypes ? string.Empty : geographyType },
                { "geometry",          Settings.DisableGeographyTypes ? string.Empty : geometryType },
                { "geometrycollection", Settings.DisableGeographyTypes ? string.Empty : (isEf6 ? "DbGeometry" : nts + "GeometryCollection") },
                { "int unsigned",      "long" },
                { "int",               "int" },
                { "integer unsigned",  "long" },
                { "integer",           "int" },
                { "json",              "string" },
                { "linestring",        Settings.DisableGeographyTypes ? string.Empty : (isEf6 ? "DbGeometry" : nts + "LineString") },
                { "longblob",          "byte[]" },
                { "longtext",          "string" },
                { "mediumblob",        "byte[]" },
                { "mediumint unsigned", "int" },
                { "mediumint",         "int" },
                { "mediumtext",        "string" },
                { "multilinestring",   Settings.DisableGeographyTypes ? string.Empty : (isEf6 ? "DbGeometry" : nts + "MultiLineString") },
                { "multipoint",        Settings.DisableGeographyTypes ? string.Empty : (isEf6 ? "DbGeometry" : nts + "MultiPoint") },
                { "multipolygon",      Settings.DisableGeographyTypes ? string.Empty : (isEf6 ? "DbGeometry" : nts + "MultiPolygon") },
                { "national char",     "string" },
                { "national varchar",  "string" },
                { "nchar",             "string" },
                { "numeric",           "decimal" },
                { "nvarchar",          "string" },
                { "point",             Settings.DisableGeographyTypes ? string.Empty : (isEf6 ? "DbGeometry" : nts + "Point") },
                { "polygon",           Settings.DisableGeographyTypes ? string.Empty : (isEf6 ? "DbGeometry" : nts + "Polygon") },
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
                { "tinyint(1)",        "bool" },
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