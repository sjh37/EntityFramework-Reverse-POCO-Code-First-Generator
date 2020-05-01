using System;

namespace Efrpg.Filtering
{
    public class TableFilter : IFilterType<Table>
    {
        // Filtering of tables using a function.
        // Return true to exclude the table, return false to include it.
        public bool IsExcluded(Table t)
        {
            // Example: Exclude any table in 'dbo' schema and with 'order' anywhere in its name.
            //if(t.Schema.DbName.Equals("dbo", StringComparison.InvariantCultureIgnoreCase) && t.NameHumanCase.ToLowerInvariant().Contains("order"))
            //    return true;

            return false;
        }
    }
}