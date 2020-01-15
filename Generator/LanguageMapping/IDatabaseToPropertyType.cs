using System.Collections.Generic;

namespace Efrpg.LanguageMapping
{
    public interface IDatabaseToPropertyType
    {
        Dictionary<string, string> GetMapping(); // [Database type] = Language type
        List<string> SpatialTypes();
    }
}