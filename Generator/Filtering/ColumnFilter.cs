namespace Efrpg.Filtering
{
    public class ColumnFilter : IFilterType<Column>
    {
        // Filtering of columns using a function.
        // Return true to exclude the column, return false to include it.
        public bool IsExcluded(Column c)
        {
            // Example: Exclude any columns whose table is in 'dbo' schema and column name starts with 'bank'
            //if(c.ParentTable.Schema.Equals("dbo", StringComparison.InvariantCultureIgnoreCase) &&
            //   c.NameHumanCase.ToLowerInvariant().StartsWith("bank"))
            //    return true;

            return false;
        }
    }
}