using System.Collections.Generic;

namespace Efrpg.LanguageMapping
{
    public class SQLiteToCSharp : IDatabaseToPropertyType
    {
        // [Database type] = Language type
        public Dictionary<string, string> GetMapping()
        {
            return new Dictionary<string, string>
            {
                { string.Empty, "string" }, // default
                { "blob",       "byte[]" },
                { "real",       "double" },
                { "integer",    "int" },
                { "text",       "string" }
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