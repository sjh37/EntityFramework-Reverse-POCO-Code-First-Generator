namespace Efrpg.Filtering
{
    public class SchemaAndName
    {
        public string Schema;
        public string Name;

        public SchemaAndName(string schema, string name)
        {
            Schema = schema;
            Name   = name;
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? string.Empty : $"{Schema}.{Name}".ToLowerInvariant();
        }
    }
}