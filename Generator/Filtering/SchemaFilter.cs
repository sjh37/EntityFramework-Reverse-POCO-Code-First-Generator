using System;

namespace Efrpg.Filtering
{
    public class SchemaFilter : IFilterType<EntityName>
    {
        // Filtering of schema using a function.
        // Return true to exclude the schema, return false to include it
        public bool IsExcluded(EntityName schema)
        {
            // Example: Exclude any schema with 'audit' anywhere in its name.
            //if (schema.Name.ToLowerInvariant().Contains("audit"))
            //    return true;

            return false;
        }
    }
}