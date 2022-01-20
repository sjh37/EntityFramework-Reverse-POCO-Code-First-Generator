using System.Collections.Generic;
using System.Linq;

namespace Efrpg.Filtering
{

    public class MultiContextFilter : DbContextFilter
    {
        private readonly MultiContextSettings _settings;
        private readonly MultiContextNameNormalisation _normalisation;

        private readonly List<string> _allowedSchemas;
        private readonly List<string> _allowedTables;
        private readonly Dictionary<string, List<string>> _allowedColumns; // Key = table name, value = list of columns
        private readonly List<string> _allowedStoredProcs; // Stored procedures
        private readonly List<string> _allowedFunctions; // Table/Scalar valued functions

        public MultiContextFilter(MultiContextSettings settings)
        {
            _settings = settings;

            IncludeViews                 = settings.IncludeViews();
            IncludeSynonyms              = false;
            IncludeTableValuedFunctions  = settings.IncludeFunctions(); // If true, for EF6 install the "EntityFramework.CodeFirstStoreFunctions" Nuget Package.
            IncludeScalarValuedFunctions = IncludeTableValuedFunctions; // Scalar/Table function filters are not separate in this filter.
            IncludeStoredProcedures      = IncludeScalarValuedFunctions || IncludeTableValuedFunctions || settings.IncludeStoredProcedures();
            SubNamespace                 = settings.GetNamespace();

            _allowedSchemas     = new List<string>();
            _allowedTables      = new List<string>();
            _allowedColumns     = new Dictionary<string, List<string>>();
            _allowedStoredProcs = new List<string>();
            _allowedFunctions   = new List<string>();

            // Pre-process the settings
            _normalisation = new MultiContextNameNormalisation(settings.BaseSchema);
            _allowedSchemas.Add(_normalisation.DefaultSchema);

            // Tables
            foreach (var t in settings.Tables)
            {
                var tableName = _normalisation.Normalise(t.Name);
                SchemaAndName tableDbName = null;
                if (!string.IsNullOrEmpty(t.DbName))
                {
                    t.DbName = t.DbName.Replace("[", "").Replace("]", "");
                    tableDbName = _normalisation.Normalise(t.DbName);
                    _allowedTables.Add(tableDbName.ToString());

                    // Override schema with the one defined in DbName
                    tableName.Schema = tableDbName.Schema;
                }
                _allowedTables.Add(tableName.ToString());


                SchemaAndName tablePluralName = null;
                if (!string.IsNullOrEmpty(t.PluralName))
                {
                    tablePluralName = _normalisation.Normalise(t.PluralName);
                    tablePluralName.Schema = tableName.Schema;
                    _allowedTables.Add(tablePluralName.ToString());
                }


                var cols = new List<string>();
                foreach (var c in t.Columns)
                {
                    var columnName = _normalisation.Normalise(c.Name);
                    cols.Add(columnName.Name.ToLowerInvariant());

                    if (!string.IsNullOrEmpty(c.DbName))
                    {
                        c.DbName = c.DbName.Replace("[", "").Replace("]", "");
                        var columnDbName = _normalisation.Normalise(c.DbName);
                        cols.Add(columnDbName.Name.ToLowerInvariant());
                    }
                }

                if (cols.Any())
                {
                    cols = cols.Distinct().ToList();
                    _allowedColumns.Add(tableName.ToString(), cols);

                    if (tableDbName != null && !_allowedColumns.ContainsKey(tableDbName.ToString()))
                        _allowedColumns.Add(tableDbName.ToString(), cols);

                    if (tablePluralName != null && !_allowedColumns.ContainsKey(tablePluralName.ToString()))
                        _allowedColumns.Add(tablePluralName.ToString(), cols);
                }
            }
            _allowedTables = _allowedTables.Distinct().ToList();

            // Find schemas used in tables
            var tableSchemas = _allowedTables
                .Where(x => x.Contains('.'))
                .Select(t => t.Split('.').First().ToLowerInvariant())
                .Distinct()
                .ToList();
            _allowedSchemas.AddRange(tableSchemas);
            _allowedSchemas = _allowedSchemas.Distinct().ToList();


            // Stored procedures
            foreach (var sp in settings.StoredProcedures)
            {
                var spName = _normalisation.Normalise(sp.Name);
                if (!string.IsNullOrEmpty(sp.DbName))
                {
                    var spDbName = _normalisation.Normalise(sp.DbName);
                    _allowedStoredProcs.Add(spDbName.ToString());

                    // Override schema with the one defined in DbName
                    spName.Schema = spDbName.Schema;
                }
                _allowedStoredProcs.Add(spName.ToString());
            }
            _allowedStoredProcs = _allowedStoredProcs.Distinct().ToList();


            // Functions
            foreach (var f in settings.Functions)
            {
                var funcName = _normalisation.Normalise(f.Name);
                if (!string.IsNullOrEmpty(f.DbName))
                {
                    var funcDbName = _normalisation.Normalise(f.DbName);
                    _allowedFunctions.Add(funcDbName.ToString());

                    // Override schema with the one defined in DbName
                    funcName.Schema = funcDbName.Schema;
                }
                _allowedFunctions.Add(funcName.ToString());
            }
            _allowedFunctions = _allowedFunctions.Distinct().ToList();
        }

        public MultiContextSettings GetSettings()
        {
            return _settings;
        }

        public override bool IsExcluded(EntityName item)
        {
            if (string.IsNullOrEmpty(item.DbName))
                return true;

            // Schema
            var schema = item as Schema;
            if (schema != null)
                return !_allowedSchemas.Contains(item.DbName.ToLowerInvariant());


            // Table
            var table = item as Table;
            if (table != null)
            {
                var search = $"{table.Schema.DbName}.{table.DbName}".ToLowerInvariant();
                if (_allowedTables.Contains(search))
                    return false; // Allowed

                if (!string.IsNullOrEmpty(table.NameHumanCase))
                {
                    search = $"{table.Schema.DbName}.{table.NameHumanCase}".ToLowerInvariant();
                    if (_allowedTables.Contains(search))
                        return false; // Allowed
                }

                return true; // Excluded
            }


            // Column
            var column = item as Column;
            if (column != null)
            {
                var key = $"{column.ParentTable.Schema.DbName}.{column.ParentTable.DbName}".ToLowerInvariant();
                if (!_allowedColumns.ContainsKey(key) && !string.IsNullOrEmpty(column.ParentTable.NameHumanCase))
                {
                    key = $"{column.ParentTable.Schema.DbName}.{column.ParentTable.NameHumanCase}".ToLowerInvariant();
                    if (!_allowedColumns.ContainsKey(key))
                        return true; // Excluded as could not find table
                }

                var cols = _allowedColumns[key];
                if (cols.Contains(item.DbName.ToLowerInvariant()))
                    return false; // Allowed

                if (!string.IsNullOrEmpty(item.NameHumanCase) && cols.Contains(item.NameHumanCase.ToLowerInvariant()))
                    return false; // Allowed

                return true; // Excluded
            }


            // Stored procedure
            var sp = item as StoredProcedure;
            if (sp != null)
            {
                var search = $"{sp.Schema.DbName}.{sp.DbName}".ToLowerInvariant();
                if (sp.IsStoredProcedure && _allowedStoredProcs.Contains(search))
                    return false; // Allowed
                if ((sp.IsTableValuedFunction || sp.IsScalarValuedFunction) && _allowedFunctions.Contains(search))
                    return false; // Allowed

                if (!string.IsNullOrEmpty(sp.NameHumanCase))
                {
                    search = $"{sp.Schema.DbName}.{sp.NameHumanCase}".ToLowerInvariant();
                    if (sp.IsStoredProcedure && _allowedStoredProcs.Contains(search))
                        return false; // Allowed
                    if ((sp.IsTableValuedFunction || sp.IsScalarValuedFunction) && _allowedFunctions.Contains(search))
                        return false; // Allowed
                }

                return true; // Excluded
            }

            return true; // Always exclude unless found
        }

        public override string TableRename(string name, string schema, bool isView)
        {
            var tableSettings = FindTableSetting(name, schema);
            if (tableSettings != null && !string.IsNullOrWhiteSpace(tableSettings.Name))
                return tableSettings.Name;

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

        public override void UpdateTable(Table table)
        {
            var t = FindTableSetting(table.DbName, table.Schema.DbName);
            if (t != null)
            {
                if (!string.IsNullOrEmpty(t.Description))
                    table.AdditionalComment = t.Description;

                if (!string.IsNullOrEmpty(t.PluralName))
                    table.PluralNameOverride = t.PluralName;

                if (!string.IsNullOrEmpty(t.DbSetModifier))
                    table.DbSetModifier = t.DbSetModifier;

                if (!string.IsNullOrEmpty(t.Attributes))
                    table.Attributes.AddRange(t.Attributes.Split(Settings.MultiContextAttributeDelimiter));
            }

            // Callback to Settings, which can be set within <database>.tt
            Settings.UpdateTable?.Invoke(table);

            if (t.AllFields != null)
            {
                Settings.MultiContextAllFieldsTableProcessing?.Invoke(table, t.AllFields);

                // Examples of how to use additional custom fields from the MultiContext.[Table] table
                // VARCHAR example
                /*if (t.AllFields.ContainsKey("Notes"))
                {
                    var o = t.AllFields["Notes"];
                    if (string.IsNullOrEmpty(table.AdditionalComment))
                        table.AdditionalComment = string.Empty;

                    table.AdditionalComment += string.Format(" Test = {0}", o.ToString());
                }*/
            }
        }

        public override void UpdateColumn(Column column, Table table)
        {
            var t = FindTableSetting(table.DbName, table.Schema.DbName);
            if (t != null)
            {
                var name = column.DbName.ToLowerInvariant();
                var nameHumanCase = column.NameHumanCase.ToLowerInvariant();

                var col = FindColumnSetting(t, name, nameHumanCase);
                if (col != null)
                {
                    column.NameHumanCase = col.Name;

                    if (!string.IsNullOrEmpty(col.Attributes))
                        column.Attributes.AddRange(col.Attributes.Split(Settings.MultiContextAttributeDelimiter));

                    if (col.OverrideModifier != null)
                        column.OverrideModifier = col.OverrideModifier.Value;

                    if (col.IsPrimaryKey != null)
                        column.IsPrimaryKey = col.IsPrimaryKey.Value;

                    if (!string.IsNullOrEmpty(col.EnumType))
                    {
                        column.PropertyType = col.EnumType;
                        if (!string.IsNullOrEmpty(column.Default))
                            column.Default = "(" + col.EnumType + ") " + column.Default;
                    }

                    if (col.IsNullable != null)
                        column.IsNullable = col.IsNullable.Value;

                    if (!string.IsNullOrEmpty(col.PropertyType))
                        column.PropertyType = col.PropertyType;

                    if (col.AllFields != null)
                    {
                        Settings.MultiContextAllFieldsColumnProcessing?.Invoke(column, table, col.AllFields);

                        // Examples of how to use additional custom fields from the MultiContext.[Column] table
                        // INT example
                        /*if (col.AllFields.ContainsKey("DummyInt"))
                        {
                            var o = col.AllFields["DummyInt"];
                            column.ExtendedProperty += string.Format(" DummyInt = {0}", (int) o);
                        }*/

                        // VARCHAR example
                        /*if (col.AllFields.ContainsKey("Test"))
                        {
                            var o = col.AllFields["Test"];
                            column.ExtendedProperty += string.Format(" Test = {0}", o.ToString());
                        }*/

                        // DATETIME example
                        /*if (col.AllFields.ContainsKey("date_of_birth"))
                        {
                            var o = col.AllFields["date_of_birth"];
                            var date = Convert.ToDateTime(o);
                            column.ExtendedProperty += string.Format(" date_of_birth = {0}", date.ToLongDateString());
                        }*/
                    }
                }
            }

            // Callback to Settings, which can be set within <database>.tt
            Settings.UpdateColumn?.Invoke(column, table, null);
        }

        public override void AddEnum(Table table)
        {

        }
        
        public override void UpdateEnum(Enumeration enumeration)
        {

        }

        public override void UpdateEnumMember(EnumerationMember enumerationMember)
        {

        }

        public override void ViewProcessing(Table view)
        {
            // Find the multi-context settings for this view
            var t = FindTableSetting(view.DbName, view.Schema.DbName);
            if (t != null)
            {
                // Find the multi-context columns which have a setting in IsPrimaryKey
                var requiredPrimaryKeys = t.Columns
                    .Where(c => c.IsPrimaryKey.HasValue)
                    .Select(c => new
                    {
                        Name         = _normalisation.Normalise(c.Name, c.DbName).Name.ToLowerInvariant(),
                        DbName       = _normalisation.Normalise(c.DbName)?.Name.ToLowerInvariant(),
                        IsPrimaryKey = c.IsPrimaryKey.Value
                    })
                    .ToList();

                if (!requiredPrimaryKeys.Any())
                    return;

                // Find the column settings which have an IsPrimaryKey set to true/false;
                var requiredFalse = requiredPrimaryKeys.Where(x => !x.IsPrimaryKey).ToList();
                var requiredTrue  = requiredPrimaryKeys.Where(x => x.IsPrimaryKey).ToList();
                var dbNamesFalse  = requiredFalse.Where(x => !string.IsNullOrEmpty(x.DbName)).Select(x => x.DbName).ToList();
                var dbNamesTrue   = requiredTrue .Where(x => !string.IsNullOrEmpty(x.DbName)).Select(x => x.DbName).ToList();
                var colFalse      = requiredFalse.Select(x => x.Name).ToList();
                var colTrue       = requiredTrue .Select(x => x.Name).ToList();

                foreach (var col in view.Columns.Where(c => dbNamesFalse.Contains(c.DbName.ToLowerInvariant()) || colFalse.Contains(c.NameHumanCase.ToLowerInvariant())))
                    col.IsPrimaryKey = false;

                foreach (var col in view.Columns.Where(c => dbNamesTrue.Contains(c.DbName.ToLowerInvariant()) || colTrue.Contains(c.NameHumanCase.ToLowerInvariant())))
                {
                    col.IsPrimaryKey = true;
                    col.IsNullable   = false;
                }
            }

            // Callback to Settings, which can be set within <database>.tt
            Settings.ViewProcessing?.Invoke(view);
        }

        public override string StoredProcedureRename(StoredProcedure sp)
        {
            var storedProcSetting = FindStoredProcSetting(sp);
            if (storedProcSetting != null)
            {
                if (storedProcSetting.AllFields != null)
                {
                    Settings.MultiContextAllFieldsStoredProcedureProcessing?.Invoke(sp, storedProcSetting.AllFields);

                    // Examples of how to use additional custom fields from the MultiContext.[Table] table
                    // VARCHAR example
                    /*if (storedProcSetting.AllFields.ContainsKey("CustomRename"))
                    {
                        var o = storedProcSetting.AllFields["CustomRename"];
                        sp.NameHumanCase = o.ToString();
                    }*/
                }
                return storedProcSetting.Name;
            }

            var functionSetting = FindFunctionSetting(sp);
            if (functionSetting != null)
            {
                if (functionSetting.AllFields != null)
                {
                    Settings.MultiContextAllFieldsFunctionProcessing?.Invoke(sp, functionSetting.AllFields);

                    // Examples of how to use additional custom fields from the MultiContext.[Table] table
                    // VARCHAR example
                    /*if (functionSetting.AllFields.ContainsKey("CustomRename"))
                    {
                        var o = functionSetting.AllFields["CustomRename"];
                        sp.NameHumanCase = o.ToString();
                    }*/
                }
                return functionSetting.Name;
            }

            // Callback to Settings, which can be set within <database>.tt
            if (Settings.StoredProcedureRename != null)
                return Settings.StoredProcedureRename(sp);

            return sp.NameHumanCase;
        }

        public override string StoredProcedureReturnModelRename(string name, StoredProcedure sp)
        {
            var procSetting = FindStoredProcSetting(sp);
            if (procSetting != null && !string.IsNullOrWhiteSpace(procSetting.ReturnModel))
                return procSetting.ReturnModel;

            // Callback to Settings, which can be set within <database>.tt
            if (Settings.StoredProcedureReturnModelRename != null)
                return Settings.StoredProcedureReturnModelRename(name, sp);

            return name;
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

        private MultiContextTableSettings FindTableSetting(string name, string schema)
        {
            var search = $"{schema}.{name}".ToLowerInvariant();
            return _settings.Tables
                .FirstOrDefault(t => search == _normalisation.Normalise(t.Name, t.DbName).ToString() ||
                                     search == _normalisation.Normalise(t.DbName)?.ToString());
        }

        private MultiContextColumnSettings FindColumnSetting(MultiContextTableSettings table, string dbName, string nameHumanCase)
        {
            return table.Columns
                .FirstOrDefault(t => nameHumanCase == _normalisation.Normalise(t.Name, t.DbName).Name.ToLowerInvariant() ||
                                     dbName        == _normalisation.Normalise(t.DbName)?.Name.ToLowerInvariant());
        }

        private MultiContextStoredProcedureSettings FindStoredProcSetting(StoredProcedure sp)
        {
            var search = $"{sp.Schema.DbName}.{sp.DbName}".ToLowerInvariant();
            return _settings.StoredProcedures
                .FirstOrDefault(t => search == _normalisation.Normalise(t.Name, t.DbName).ToString() ||
                                     search == _normalisation.Normalise(t.DbName)?.ToString());
        }

        private MultiContextFunctionSettings FindFunctionSetting(StoredProcedure sp)
        {
            var search = $"{sp.Schema.DbName}.{sp.DbName}".ToLowerInvariant();
            return _settings.Functions
                .FirstOrDefault(t => search == _normalisation.Normalise(t.Name, t.DbName).ToString() ||
                                     search == _normalisation.Normalise(t.DbName)?.ToString());
        }
    }
}