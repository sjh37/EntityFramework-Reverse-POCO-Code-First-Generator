using System;

namespace Efrpg.Filtering
{
    public class SchemaFilter : IFilterType<EntityName>
    {
        // Filtering of schema using a function.
        // Return true to exclude the schema, return false to include it
        public bool IsExcluded(EntityName schema)
        {
            // Exclude schema with a name of 'MultiContext' as this is reserved by the generator for multi-context generation.
            // See https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/wiki/Generating-multiple-database-contexts-in-a-single-go
            if (schema.DbName.Equals("MultiContext", StringComparison.InvariantCultureIgnoreCase))
                return true;

            // Example: Exclude any schema with 'audit' anywhere in its name.
            //if (schema.Name.ToLowerInvariant().Contains("audit"))
            //    return true;

            return false;
        }
    }
}