namespace Efrpg.Filtering
{
    public class MultiContextNameNormalisation
    {
        public readonly string DefaultSchema;

        public MultiContextNameNormalisation(string defaultSchema)
        {
            DefaultSchema = string.IsNullOrWhiteSpace(defaultSchema) ? Settings.DefaultSchema?.Trim().ToLowerInvariant() : defaultSchema.Trim().ToLowerInvariant();
        }

        public SchemaAndName Normalise(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            var parts = name.Split('.');
            var numParts = parts.Length;
            var schema = numParts >= 2 ? parts[numParts - 2].Trim() : DefaultSchema;
            var final = parts[numParts - 1].Trim();
            return new SchemaAndName(schema, final);
        }

        public SchemaAndName Normalise(string name, string dbName)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            var db = Normalise(dbName);

            var parts = name.Split('.');
            var numParts = parts.Length;
            var schema = db?.Schema ?? DefaultSchema;
            if (numParts >= 2)
                schema = parts[numParts - 2].Trim();
            var final = parts[numParts - 1].Trim();
            return new SchemaAndName(schema, final);
        }
    }
}