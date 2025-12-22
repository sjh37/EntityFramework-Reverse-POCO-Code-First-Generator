using System.Collections.Generic;
using Efrpg.Templates;

namespace Efrpg.LanguageMapping
{
    public class SqlServerToCSharp : IDatabaseToPropertyType
    {
        // [Database type] = Language type
        public Dictionary<string, string> GetMapping()
        {
            var isEf6 = Settings.IsEf6();
            var geographyType = isEf6 ? "DbGeography" : "NetTopologySuite.Geometries.Point";
            var geometryType  = isEf6 ? "DbGeometry"  : "NetTopologySuite.Geometries.Geometry";

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
                { "geography",        Settings.DisableGeographyTypes ? string.Empty : geographyType },
                { "geometry",         Settings.DisableGeographyTypes ? string.Empty : geometryType },
                { "hierarchyid",      "Hierarchy.HierarchyId" },
                { "image",            "byte[]" },
                { "int",              "int" },
                { "json",             "string" }, // SQL Server 2025 / Azure SQL native json type
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
                { "vector",           "SqlVector<float>" }, // SQL Server 2025 / Azure SQL vector type for AI/ML (requires Microsoft.Data.SqlClient.Types namespace)
                { "varbinary",        "byte[]" },
                { "varbinary(max)",   "byte[]" }
            };
        }

        public List<string> SpatialTypes()
        {
            return new List<string> { "geography", "geometry" };
        }

        public List<string> PrecisionTypes()
        {
            return new List<string> { "float", "datetime2", "datetimeoffset" };
        }

        public List<string> PrecisionAndScaleTypes()
        {
            return new List<string> { "decimal", "numeric" };
        }
    }
}