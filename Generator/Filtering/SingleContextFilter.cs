using System.Collections.Generic;
using System.Linq;

namespace Efrpg.Filtering
{
    /// <summary>
    /// Filtering can now be done via one or more Regex's and one or more functions.
    /// Gone are the days of a single do-it-all regex, you can now split them up into many smaller Regex's.
    /// It's now up to you how to want to mix and match them.
    /// </summary>
    public class SingleContextFilter : DbContextFilter
    {
        protected readonly List<IFilterType<Schema>>          SchemaFilters;
        protected readonly List<IFilterType<Table>>           TableFilters;
        protected readonly List<IFilterType<Column>>          ColumnFilters;
        protected readonly List<IFilterType<StoredProcedure>> StoredProcedureFilters;
        protected readonly List<IFilterType<EnumTableSource>> EnumerationTableFilters;
        protected readonly List<IFilterType<EnumSchemaSource>>EnumerationSchemaFilters;

        private bool _hasMergedIncludeFilters;

        public SingleContextFilter()
        {
            IncludeViews                 = FilterSettings.IncludeViews;
            IncludeSynonyms              = FilterSettings.IncludeSynonyms;
            IncludeTableValuedFunctions  = FilterSettings.IncludeTableValuedFunctions;
            IncludeScalarValuedFunctions = FilterSettings.IncludeScalarValuedFunctions;
            IncludeStoredProcedures      = IncludeScalarValuedFunctions || IncludeTableValuedFunctions || FilterSettings.IncludeStoredProcedures;

            SchemaFilters            = FilterSettings.SchemaFilters;
            TableFilters             = FilterSettings.TableFilters;
            ColumnFilters            = FilterSettings.ColumnFilters;
            StoredProcedureFilters   = FilterSettings.StoredProcedureFilters;
            EnumerationTableFilters  = FilterSettings.EnumerationTableFilters;
            EnumerationSchemaFilters = FilterSettings.EnumerationSchemaFilters;
            _hasMergedIncludeFilters = false;

            EnumDefinitions = new List<EnumDefinition>();
            Settings.AddEnumDefinitions?.Invoke(EnumDefinitions);
        }

        public override bool IsExcluded(EntityName item)
        {
            if(!_hasMergedIncludeFilters)
            {
                MergeIncludeFilters();
                _hasMergedIncludeFilters = true;
            }

            var schema = item as Schema;
            if (schema != null)
                return SchemaFilters.Any(filter => filter.IsExcluded(schema));

            var enumSchemaSource = item as EnumSchemaSource;
            if (enumSchemaSource != null)
            {
                return EnumerationSchemaFilters.Any(filter => filter.IsExcluded(enumSchemaSource));
            }


            var enumTableSource = item as EnumTableSource;
            if (enumTableSource != null)
            {
                return EnumerationTableFilters.Any(filter => filter.IsExcluded(enumTableSource)) || EnumerationSchemaFilters.Any(filter => filter.IsExcluded(enumTableSource.Schema));
            }

            var table = item as Table;
            if (table != null)
            {
                return TableFilters.Any(filter => filter.IsExcluded(table)) || SchemaFilters.Any(filter => filter.IsExcluded(table.Schema));
            }

            var column = item as Column;
            if (column != null)
                return ColumnFilters.Any(filter => filter.IsExcluded(column));

            var sp = item as StoredProcedure;
            if (sp != null)
                return StoredProcedureFilters.Any(filter => filter.IsExcluded(sp)) || SchemaFilters.Any(filter => filter.IsExcluded(sp.Schema));

            return false;
        }

        public override string TableRename(string name, string schema, bool isView)
        {
            // Callback to Settings, which can be set within <database>.tt
            if (Settings.TableRename != null)
                return Settings.TableRename(name, schema, isView);

            return name;
        }

        public override string MappingTableRename(string mappingTable, string tableName, string entityName)
        {
            // Callback to Settings, which can be set within <database>.tt
            if (Settings.MappingTableRename != null)
                return Settings.MappingTableRename(mappingTable, tableName, entityName);

            return entityName;
        }

        public List<EnumDefinition> EnumDefinitions;


        public override void UpdateTable(Table table)
        {
            // Callback to Settings, which can be set within <database>.tt
            Settings.UpdateTable?.Invoke(table);
        }

        public override void UpdateColumn(Column column, Table table)
        {
            // Callback to Settings, which can be set within <database>.tt
            Settings.UpdateColumn?.Invoke(column, table, EnumDefinitions);
        }

        public override void UpdateEnum(Enumeration enumeration)
        {
            Settings.UpdateEnum?.Invoke(enumeration);
        }

        public override void UpdateEnumMember(EnumerationMember enumerationMember)
        {
            Settings.UpdateEnumMember?.Invoke(enumerationMember);
        }

        public override void ViewProcessing(Table view)
        {
            // Callback to Settings, which can be set within <database>.tt
            Settings.ViewProcessing?.Invoke(view);
        }

        public override string StoredProcedureRename(StoredProcedure sp)
        {
            // Callback to Settings, which can be set within <database>.tt
            if (Settings.StoredProcedureRename != null)
                return Settings.StoredProcedureRename(sp);

            return sp.NameHumanCase; // Do nothing by default
        }

        public override string StoredProcedureReturnModelRename(string name, StoredProcedure sp)
        {
            // Callback to Settings, which can be set within <database>.tt
            if (Settings.StoredProcedureReturnModelRename != null)
                return Settings.StoredProcedureReturnModelRename(name, sp);

            return name; // Do nothing by default
        }

        public override ForeignKey ForeignKeyFilter(ForeignKey fk)
        {
            // Return null to exclude this foreign key, or set IncludeReverseNavigation = false
            // to include the foreign key but not generate reverse navigation properties.
            // Example, to exclude all foreign keys for the Categories table, use:
            //if (fk.PkTableName == "Categories")
            //    return null;

            // Example, to exclude reverse navigation properties for tables ending with Type, use:
            //if (fk.PkTableName.EndsWith("Type"))
            //    fk.IncludeReverseNavigation = false;

            // You can also change the access modifier of the foreign-key's navigation property:
            //if(fk.PkTableName == "Categories")
            //     fk.AccessModifier = "internal";

            return fk;
        }

        public override string[] ForeignKeyAnnotationsProcessing(Table fkTable, Table pkTable, string propName, string fkPropName)
        {
            // Callback to Settings, which can be set within <database>.tt
            if (Settings.ForeignKeyAnnotationsProcessing != null)
                return Settings.ForeignKeyAnnotationsProcessing(fkTable, pkTable, propName, fkPropName);

            return null;
        }

        private void MergeIncludeFilters()
        {
            MergeIncludeFilters(SchemaFilters);
            MergeIncludeFilters(TableFilters);
            MergeIncludeFilters(ColumnFilters);
            MergeIncludeFilters(StoredProcedureFilters);
            
            MergeIncludeFilters(EnumerationSchemaFilters);
            MergeIncludeFilters(EnumerationTableFilters);
        }

        private static void MergeIncludeFilters<T>(List<IFilterType<T>> filters)
        {
            var list = filters
                .Where(x => x.GetType() == typeof(RegexIncludeFilter))
                .Select(x => (RegexIncludeFilter) x)
                .ToList();

            if (list.Count < 2)
                return; // Nothing to merge

            var singleRegex = string.Join("|", list.Select(x => x.Pattern()));
            filters.RemoveAll(filter => filter.GetType() == typeof(RegexIncludeFilter));
            var singleIncludeFilter = (IFilterType<T>) new RegexIncludeFilter(singleRegex);
            filters.Add(singleIncludeFilter);
        }
    }
}