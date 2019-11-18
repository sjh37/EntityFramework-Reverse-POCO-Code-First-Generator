using System.Collections.Generic;

namespace Efrpg
{
    public class Enumeration
    {
        public readonly string EnumName;
        public readonly List<KeyValuePair<string, string>> Items;

        public Enumeration(string enumName, List<KeyValuePair<string, string>> items)
        {
            EnumName = enumName;
            Items    = items;
        }
    }
}