using System.Collections.Generic;
using Efrpg;
using Efrpg.Filtering;

namespace Generator.Tests.Unit
{
    public class TestContextFilter : SingleContextFilter
    {
        public TestContextFilter()
        {
            EnumerationSchemaFilters.AddRange(new List<IFilterType<EnumSchemaSource>>
            {
                new RegexIncludeFilter("^Enum$")
            });

            EnumerationTableFilters.AddRange(new List<IFilterType<EnumTableSource>>
            {
                new RegexExcludeFilter("^ProductType$")
            });

            SchemaFilters.AddRange(new List<IFilterType<Schema>>
            {
                // Only include the schemas 'dbo' and 'events'
                new RegexIncludeFilter("^dbo$"),
                new RegexIncludeFilter("^events$")
            });

            TableFilters.AddRange(new List<IFilterType<Table>>
            {
                // Exclude filters
                new RegexExcludeFilter("(.*_FR_.*)|(^data_.*)"),  // Exclude all tables that contain '_FR_' or begin with 'data_'
                new RegexExcludeFilter(".*[Bb]illing.*"), // This excludes all tables with 'billing' anywhere in the name

                // Include filters
                new RegexIncludeFilter("^[Cc]ustomer.*"), // This includes any remaining tables with names beginning with 'customer'
                new RegexIncludeFilter("^Order.*"), // This includes any remaining tables with names beginning with 'customer'
            });

            ColumnFilters.AddRange(new List<IFilterType<Column>>
            {
                // Exclude any columns that begin with 'FK_'
                new RegexExcludeFilter("^FK_.*$"),
            });
        }
    }
}