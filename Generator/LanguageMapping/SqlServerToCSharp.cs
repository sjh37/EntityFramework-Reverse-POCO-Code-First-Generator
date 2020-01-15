using System.Collections.Generic;
using Efrpg.Templates;

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
                { "geography",        Settings.TemplateType == TemplateType.Ef6 ? "Spatial.DbGeography" : "NetTopologySuite.Geometries.Point" },
                { "geometry",         Settings.TemplateType == TemplateType.Ef6 ? "Spatial.DbGeometry"  : "NetTopologySuite.Geometries.Geometry" },
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

        public List<string> SpatialTypes()
        {
            return new List<string> { "geography", "geometry" };
        }
    }
}