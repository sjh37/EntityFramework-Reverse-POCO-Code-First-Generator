using System.Collections.Generic;

namespace Efrpg.LanguageMapping
{
    public interface IDatabaseToPropertyType
    {
        Dictionary<string, string> GetMapping(); // [Database type] = Language type

        /// <summary>
        /// A list of the database types that are spatial.
        /// </summary>
        List<string> SpatialTypes();

        /// <summary>
        /// A list of the database types that contain precision.
        /// </summary>
        List<string> PrecisionTypes();

        /// <summary>
        /// A list of the database types that contain precision and scale.
        /// </summary>
        List<string> PrecisionAndScaleTypes();
    }
}