using System.Collections.Generic;

namespace Efrpg.Filtering
{
    // Filtering **************************************************************************************************************************
    // These settings are only used by the single context filter SingleContextFilter (Settings.GenerateSingleDbContext = true)
    // Please read https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/wiki/Filtering
    // For multi-context filtering (Settings.GenerateSingleDbContext = false), please read https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/wiki/Generating-multiple-database-contexts-in-a-single-go
    // Use the following filters to exclude or include schemas/tables/views/columns/stored procedures.
    // You can have as many as you like, and mix and match them.
    // They run in the order defined below. For help with Regex's try https://regexr.com
    // Feel free to add more filter types and include them below.
    public static class FilterSettings
    {
        public static bool IncludeViews;
        public static bool IncludeSynonyms;
        public static bool IncludeStoredProcedures;
        public static bool IncludeTableValuedFunctions;
        public static bool IncludeScalarValuedFunctions;

        public static readonly List<IFilterType<Schema>>          SchemaFilters;
        public static readonly List<IFilterType<Table>>           TableFilters;
        public static readonly List<IFilterType<Column>>          ColumnFilters;
        public static readonly List<IFilterType<StoredProcedure>> StoredProcedureFilters;

        static FilterSettings()
        {
            SchemaFilters          = new List<IFilterType<Schema>>();
            TableFilters           = new List<IFilterType<Table>>();
            ColumnFilters          = new List<IFilterType<Column>>();
            StoredProcedureFilters = new List<IFilterType<StoredProcedure>>();
        }

        public static void Reset()
        {
            SchemaFilters         .RemoveAll(x => true);
            TableFilters          .RemoveAll(x => true);
            ColumnFilters         .RemoveAll(x => true);
            StoredProcedureFilters.RemoveAll(x => true);
        }

        public static void AddDefaults()
        {
            IncludeViews                 = true;
            IncludeSynonyms              = false;
            IncludeStoredProcedures      = true;
            IncludeTableValuedFunctions  = false; // If true, for EF6 install the "EntityFramework.CodeFirstStoreFunctions" NuGet Package.
            IncludeScalarValuedFunctions = false;

            AddDefaultSchemaFilters();
            AddDefaultTableFilters();
            AddDefaultColumnFilters();
            AddDefaultStoredProcedureFilters();
        }

        public static void CheckSettings()
        {
            if (IncludeTableValuedFunctions || IncludeScalarValuedFunctions)
                IncludeStoredProcedures = true; // Must be set if table/scalar functions are wanted
        }

        public static void AddDefaultSchemaFilters()
        {
            SchemaFilters.AddRange(new List<IFilterType<Schema>>
            {
                new PeriodFilter(), // Keep this first as EF does not allow schemas to contain a period character

                // To include the only the schemas 'dbo' and 'events'
                //new RegexIncludeFilter("^dbo$|^events$"),

                // Add your own code to these custom filter classes
                new SchemaFilter(),
                new HasNameFilter(FilterType.Schema)
            });
        }

        public static void AddDefaultTableFilters()
        {
            TableFilters.AddRange(new List<IFilterType<Table>>
            {
                //new PeriodFilter(), // Keep this first as EF does not allow tables to contain a period character

                // To include all the customer tables, but not the customer billing tables
                //new RegexExcludeFilter(".*[Bb]illing.*"), // This excludes all tables with 'billing' anywhere in the name
                //new RegexIncludeFilter("^[Cc]ustomer.*"), // This includes any remaining tables with names beginning with 'customer'

                // To exclude all tables that contain '_FR_' or begin with 'data_'
                //new RegexExcludeFilter("(.*_FR_.*)|(^data_.*)"),

                // Pass in your own custom Regex
                //new RegexIncludeFilter(new Regex("^tableName1$|^tableName2$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(200))),

                // Add your own code to these custom filter classes
                new TableFilter(),
                new HasNameFilter(FilterType.Table),
            });
        }

        public static void AddDefaultColumnFilters()
        {
            ColumnFilters.AddRange(new List<IFilterType<Column>>
            {
                // Exclude any columns that begin with 'FK_'
                //new RegexExcludeFilter("^FK_.*$"),

                // Add your own code to these custom filter classes
                new ColumnFilter(),
                new HasNameFilter(FilterType.Column),
            });
        }


        public static void AddDefaultStoredProcedureFilters()
        {
            StoredProcedureFilters.AddRange(new List<IFilterType<StoredProcedure>>
            {
                new PeriodFilter(), // Keep this first as EF does not allow stored procedures to contain a period character

                // Add your own code to these custom filter classes
                new StoredProcedureFilter(),
                new HasNameFilter(FilterType.StoredProcedure)
            });
        }
    }
}