using System.Collections.Generic;

namespace Efrpg.Readers
{
    public static class MinMaxValueCache
    {
        private static readonly Dictionary<string, string> minValues = new Dictionary<string, string>
        {
            { "sbyte",   sbyte  .MinValue.ToString() },
            { "byte",    byte   .MinValue.ToString() },
            { "short",   short  .MinValue.ToString() },
            { "ushort",  ushort .MinValue.ToString() },
            { "int",     int    .MinValue.ToString() },
            { "uint",    uint   .MinValue.ToString() },
            { "long",    long   .MinValue.ToString() },
            { "ulong",   ulong  .MinValue.ToString() },
            { "float",   float  .MinValue.ToString() },
            { "double",  double .MinValue.ToString() },
            { "decimal", decimal.MinValue.ToString() }
        };

        private static readonly Dictionary<string, string> maxValues = new Dictionary<string, string>
        {
            { "sbyte",   sbyte  .MaxValue.ToString() },
            { "byte",    byte   .MaxValue.ToString() },
            { "short",   short  .MaxValue.ToString() },
            { "ushort",  ushort .MaxValue.ToString() },
            { "int",     int    .MaxValue.ToString() },
            { "uint",    uint   .MaxValue.ToString() },
            { "long",    long   .MaxValue.ToString() },
            { "ulong",   ulong  .MaxValue.ToString() },
            { "float",   float  .MaxValue.ToString() },
            { "double",  double .MaxValue.ToString() },
            { "decimal", decimal.MaxValue.ToString() }
        };

        public static string GetMinValue(string type)
        {
            string value;
            return minValues.TryGetValue(type, out value) ? value : null;
        }

        public static string GetMaxValue(string type)
        {
            string value;
            return maxValues.TryGetValue(type, out value) ? value : null;
        }
    }
}
