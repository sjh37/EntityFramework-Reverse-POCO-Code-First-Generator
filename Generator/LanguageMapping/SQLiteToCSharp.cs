using System.Collections.Generic;

namespace Efrpg.LanguageMapping
{
    public class SqLiteToCSharp : IDatabaseToPropertyType
    {
        // [Database type] = Language type
        public Dictionary<string, string> GetMapping()
        {
            return new Dictionary<string, string>
            {
                { string.Empty, "string" }, // default
                { "bigint", "long" },
                { "blob", "byte[]" },
                { "boolean", "bool" },
                { "character", "string" },
                { "clob", "string" },
                { "date", "DateTime" },
                { "datetime", "DateTime" },
                { "decimal", "double" },
                { "double", "double" },
                { "double precision", "double" },
                { "float", "double" },
                { "int", "long" },
                { "int2", "long" },
                { "int8", "long" },
                { "integer", "long" },
                { "mediumint", "long" },
                { "native character", "string" },
                { "nchar", "string" },
                { "numeric", "decimal" },
                { "nvarchar", "string" },
                { "real", "double" },
                { "smallint", "int" },
                { "text", "string" },
                { "unsigned big int", "long" },
                { "varchar", "string" },
                { "varying character", "string" }
            };
        }

        public List<string> SpatialTypes()
        {
            return new List<string>();
        }

        public List<string> PrecisionTypes()
        {
            return new List<string>();
        }

        public List<string> PrecisionAndScaleTypes()
        {
            return new List<string>();
        }
    }
}