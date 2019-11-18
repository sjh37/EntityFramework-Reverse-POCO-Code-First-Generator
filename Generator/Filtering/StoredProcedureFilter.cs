using System;

namespace Efrpg.Filtering
{
    public class StoredProcedureFilter : IFilterType<StoredProcedure>
    {
        // Filtering of stored procedures using a function.
        // Return true to exclude the stored procedure, return false to include it.
        public bool IsExcluded(StoredProcedure sp)
        {
            // Example: Exclude any stored procedure in dbo schema with "order" in its name.
            //if(sp.Schema.Name.Equals("dbo", StringComparison.InvariantCultureIgnoreCase) && sp.NameHumanCase.ToLowerInvariant().Contains("order"))
            //    return false;

            return false;
        }
    }
}