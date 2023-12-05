using System.Collections.Generic;

namespace Efrpg.Readers
{
    public class RawSequence
    {
        public readonly string Schema;
        public readonly string Name;
        public readonly string DataType;
        public readonly string StartValue;
        public readonly string IncrementValue;
        public readonly string MinValue;
        public readonly string MaxValue;
        public readonly string IsCycleEnabled;

        public readonly bool hasMinValue;
        public readonly bool hasMaxValue;

        public List<RawSequenceTableMapping> TableMapping;

        public RawSequence(string schema, string name, string dataType, string startValue, string incrementValue, string minValue, string maxValue, bool isCycleEnabled)
        {
            Schema = schema;
            Name = name;
            DataType = dataType;
            StartValue = startValue;
            IncrementValue = incrementValue;
            MinValue = minValue;
            MaxValue = maxValue;
            IsCycleEnabled = isCycleEnabled ? "true" : "false";
            TableMapping = new List<RawSequenceTableMapping>();

            hasMinValue = MinMaxValueCache.GetMinValue(dataType) != minValue;
            hasMaxValue = MinMaxValueCache.GetMaxValue(dataType) != maxValue;
        }
    }
}