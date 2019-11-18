using System.Collections.Generic;
using Efrpg;
using Efrpg.Filtering;

namespace Generator.Tests.Unit
{
    public class TestContextFilter : SingleContextFilter
    {
        public TestContextFilter()
        {
            SchemaFilters.AddRange(new List<IFilterType<Schema>>
            {
                new RegexIncludeFilter("^dbo$|^events$"), // Only include the schemas 'dbo' and 'events'
            });

            TableFilters.AddRange(new List<IFilterType<Table>>
            {
                // Exclude filters
                new RegexExcludeFilter("(.*_FR_.*)|(^data_.*)"),  // Exclude all tables that contain '_FR_' or begin with 'data_'
                new RegexExcludeFilter(".*[Bb]illing.*"), // This excludes all tables with 'billing' anywhere in the name

                // Include filters
                new RegexIncludeFilter("^[Cc]ustomer.*"), // This includes any remaining tables with names beginning with 'customer'
            });

            ColumnFilters.AddRange(new List<IFilterType<Column>>
            {
                // Exclude any columns that begin with 'FK_'
                new RegexExcludeFilter("^FK_.*$"),
            });
        }
    }
}