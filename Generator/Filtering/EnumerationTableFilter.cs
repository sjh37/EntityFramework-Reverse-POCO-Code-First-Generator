using System;

namespace Efrpg.Filtering
{
    public class EnumerationTableFilter : IFilterType<EnumTableSource>
    {
        // Filtering of stored procedures using a function.
        // Return true to exclude the stored procedure, return false to include it.
        public bool IsExcluded(EnumTableSource enumTableSource)
        {
            // Example: Exclude any stored procedure in dbo schema with "order" in its name.
            //if(sp.Schema.DbName.Equals("dbo", StringComparison.InvariantCultureIgnoreCase) && sp.NameHumanCase.ToLowerInvariant().Contains("order"))
            //   return false;

            return false;
        }
    }
}