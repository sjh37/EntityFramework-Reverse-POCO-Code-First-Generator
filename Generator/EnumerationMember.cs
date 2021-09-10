using System.Collections.Generic;

namespace Efrpg
{
    public class EnumerationMember
    {
        public readonly string Key;
        public readonly string Value;
        public readonly Dictionary<string, object> AllValues;

        public List<string> Attributes = new List<string>();

        public EnumerationMember(string key, string value, Dictionary<string, object> allValues)
        {
            Key = key;
            Value = value;
            AllValues = allValues;
        }
    }
}