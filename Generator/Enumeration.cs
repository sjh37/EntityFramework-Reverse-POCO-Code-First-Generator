using System.Collections.Generic;

namespace Efrpg
{
    public class Enumeration
    {
        public readonly string EnumName;
        public readonly List<EnumerationMember> Items;

        public List<string> EnumAttributes = new List<string>();

        public Enumeration(string enumName, List<EnumerationMember> items)
        {
            EnumName = enumName;
            Items    = items;
        }
    }
}