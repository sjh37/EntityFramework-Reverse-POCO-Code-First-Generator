using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Efrpg.FileManagement;
using Efrpg.Filtering;
using Efrpg.Licensing;
using Efrpg.Readers;
using Efrpg.Templates;

// ReSharper disable UseStringInterpolation

namespace Efrpg.Generators
{
    public abstract class Generator
    {
        public bool InitialisationOk { get; private set; }

        protected DatabaseReader DatabaseReader;
        protected FileHeaderFooter FileHeaderFooter;
        public readonly IDbContextFilterList FilterList;

        protected abstract bool AllowFkToNonPrimaryKey();
        protected abstract bool FkMustHaveSameNumberOfColumnsAsPrimaryKey();
        protected abstract void SetupEntity(Column c);
        protected abstract void SetupConfig(Column c);
        public abstract string PrimaryKeyModelBuilder(Table table);
        public abstract List<string> IndexModelBuilder(Table t);
        public abstract string IndexModelBuilder(Column c);
        protected abstract string GetHasMethod(Relationship relationship, IList<Column> fkCols, IList<Column> pkCols, bool isNotEnforced, bool fkHasUniqueConstraint);
        protected abstract string GetWithMethod(Relationship relationship, IList<Column> fkCols, string fkPropName, string manyToManyMapping, string mapKey,
            bool includeReverseNavigation, string hasMethod, string pkTableNameHumanCase, string fkTableNameHumanCase, string primaryKeyColumns, bool fkHasUniqueConstraint);
        protected abstract string GetCascadeOnDelete(bool cascadeOnDelete);
        protected abstract string GetForeignKeyConstraintName(string foreignKeyConstraintName);


        private DbProviderFactory _factory;
        public bool HasAcademiclicense;
        public bool HasTriallicense;
        private readonly StringBuilder _preHeaderInfo;
        private readonly string _codeGeneratedAttribute;
        private readonly FileManagementService _fileManagementService;
        private readonly Type _fileManagerType;
        private const int Indent = 4;

        protected Generator(FileManagementService fileManagementService, Type fileManagerType)
        {
            _fileManagementService  = fileManagementService;
            _fileManagerType        = fileManagerType;
            InitialisationOk        = false;
            _factory                = null;
            DatabaseReader          = null;
            FileHeaderFooter        = null;
            _preHeaderInfo          = new StringBuilder(1024);
            _codeGeneratedAttribute = string.Format("[GeneratedCode(\"EF.Reverse.POCO.Generator\", \"{0}\")]", EfrpgVersion.Version());
            FilterList              = new DbContextFilterList();
        }

        public void Init(string singleDbContextSubNamespace)
        {
            var providerName = "unknown";

            try
            {
                var license = ReadAndValidatelicense();
                if(license == null)
                    return;

                providerName = DatabaseProvider.GetProvider(Settings.DatabaseType);
                BuildPreHeaderInfo(license);

                _factory = DbProviderFactories.GetFactory(providerName);
                if (_factory == null)
                {
                    _fileManagementService.Error("Database factory is null, cannot continue");
                    return;
                }

                DatabaseReader = DatabaseReaderFactory.Create(_factory);
                if (DatabaseReader == null)
                {
                    _fileManagementService.Error("Cannot create a database reader due to unknown database type.");
                    return;
                }

                DatabaseReader.Init();

                if (Settings.IncludeConnectionSettingComments)
                    _preHeaderInfo.Append(DatabaseDetails());

                if (Settings.UseDataAnnotations)
                {
                    Settings.AdditionalNamespaces.Add("System.ComponentModel.DataAnnotations");
                    Settings.AdditionalNamespaces.Add("System.ComponentModel.DataAnnotations.Schema");
                }

                HasAcademiclicense = license.licenseType == licenseType.Academic;
                HasTriallicense    = license.licenseType == licenseType.Trial;
                InitialisationOk = FilterList.ReadDbContextSettings(DatabaseReader, singleDbContextSubNamespace);
                _fileManagementService.Init(FilterList.GetFilters(), _fileManagerType);
            }
            catch (Exception x)
            {
                var error = FormatError(x);
                Console.WriteLine(error);

                _fileManagementService.Error(_preHeaderInfo.ToString());
                _fileManagementService.Error(string.Empty);
                _fileManagementService.Error("// ------------------------------------------------------------------------------------------------");
                _fileManagementService.Error(string.Format("// WARNING: Failed to load provider \"{0}\" - {1}", providerName, error));
                _fileManagementService.Error("// Allowed providers:");
                foreach (DataRow fc in DbProviderFactories.GetFactoryClasses().Rows)
                {
                    var s = string.Format("//    \"{0}\"", fc[2]);
                    _fileManagementService.Error(s);
                }
                _fileManagementService.Error(string.Empty);
                _fileManagementService.Error("/*" + x.StackTrace + "*/");
                _fileManagementService.Error("// ------------------------------------------------------------------------------------------------");
                _fileManagementService.Error(string.Empty);
            }
        }

        private license ReadAndValidatelicense()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var file = Path.Combine(path, "ReversePOCO.txt");
            const string obtainAt = "// Please obtain your license file at www.ReversePOCO.co.uk, and place it in your documents folder shown above.";

            if (!File.Exists(file))
            {
                _fileManagementService.Error(string.Format("// license file {0} not found.", file));
                _fileManagementService.Error(obtainAt);
                return TriallicenseFallback();
            }

            var validator = new licenseValidator();
            if(!validator.Validate(File.ReadAllText(file)))
            {
                _fileManagementService.Error(validator.Expired
                    ? string.Format("// Your license file {0} has expired.", file)
                    : string.Format("// Your license file {0} is not valid.", file));

                _fileManagementService.Error(obtainAt);
                return TriallicenseFallback();
            }

            return validator.license; // Thank you for having a valid license and supporting this product :-)
        }

        private license TriallicenseFallback()
        {
            _fileManagementService.Error("// Defaulting to Trial version.");
            return new license(string.Empty, string.Empty, licenseType.Trial, "1", DateTime.MaxValue);
        }

        public string DatabaseDetails()
        {
            return DatabaseReader.GetDatabaseDetails();
        }

        public void LoadEnums()
        {
            if (_factory == null || DatabaseReader == null || !Settings.ElementsToGenerate.HasFlag(Elements.Enum))
                return;

            try
            {
                if (Settings.GenerateSingleDbContext)
                {
                    // Single-context
                    if (Settings.Enumerations == null)
                        return; // No enums required

                    var enumerations = DatabaseReader.ReadEnums(Settings.Enumerations);
                    if (enumerations.Count <= 0)
                        return; // No enums in database

                    foreach (var filterKeyValuePair in FilterList.GetFilters())
                    {
                        filterKeyValuePair.Value.Enums.AddRange(enumerations);
                    }
                }
                else
                {
                    // Multi-context
                    foreach (var filterKeyValuePair in FilterList.GetFilters())
                    {
                        var multiContextSetting = ((MultiContextFilter) filterKeyValuePair.Value).GetSettings();
                        if (multiContextSetting?.Enumerations == null || multiContextSetting.Enumerations.Count == 0)
                            continue;

                        var enumerations = DatabaseReader.ReadEnums(multiContextSetting.Enumerations);
                        if (enumerations.Count > 0)
                            filterKeyValuePair.Value.Enums.AddRange(enumerations);
                    }
                }
            }
            catch (Exception x)
            {
                var error = FormatError(x);
                _fileManagementService.Error(string.Empty);
                _fileManagementService.Error("// -----------------------------------------------------------------------------------------");
                _fileManagementService.Error(string.Format("// Failed to read enumeration tables in LoadEnums() - {0}", error));
                _fileManagementService.Error("/*" + x.StackTrace + "*/");
                _fileManagementService.Error("// -----------------------------------------------------------------------------------------");
                _fileManagementService.Error(string.Empty);
            }
        }
        
        public void LoadSequences()
        {
            if (_factory == null || DatabaseReader == null || !Settings.ElementsToGenerate.HasFlag(Elements.Context))
                return;

            try
            {
                var sequences = DatabaseReader.ReadSequences();
                if (sequences.Count <= 0)
                    return; // No sequences in database
                
                foreach (var filterKeyValuePair in FilterList.GetFilters())
                {
                    filterKeyValuePair.Value.Sequences.AddRange(sequences);
                }
            }
            catch (Exception x)
            {
                var error = FormatError(x);
                _fileManagementService.Error(string.Empty);
                _fileManagementService.Error("// -----------------------------------------------------------------------------------------");
                _fileManagementService.Error(string.Format("// Failed to read sequences in LoadSequences() - {0}", error));
                _fileManagementService.Error("/*" + x.StackTrace + "*/");
                _fileManagementService.Error("// -----------------------------------------------------------------------------------------");
                _fileManagementService.Error(string.Empty);
            }
        }

        public void ReadDatabase()
        {
            LoadTables();
            LoadStoredProcs();
            LoadEnums();
            LoadSequences();
        }

        public void LoadTables()
        {
            if (_factory == null || DatabaseReader == null ||
                !(Settings.ElementsToGenerate.HasFlag(Elements.Poco) ||
                  Settings.ElementsToGenerate.HasFlag(Elements.Context) ||
                  Settings.ElementsToGenerate.HasFlag(Elements.Interface) ||
                  Settings.ElementsToGenerate.HasFlag(Elements.PocoConfiguration)))
                return;

            try
            {
                var includeSynonyms = FilterList.IncludeSynonyms();

                var rawTables      = DatabaseReader.ReadTables(includeSynonyms);
                var rawIndexes     = DatabaseReader.ReadIndexes();
                var rawForeignKeys = DatabaseReader.ReadForeignKeys(includeSynonyms);

                // For unit testing
                //foreach (var ri in rawIndexes.OrderBy(x => x.TableName).ThenBy(x => x.IndexName)) Console.WriteLine(ri.Dump());
                //foreach (var rfk in rawForeignKeys) Console.WriteLine(rfk.Dump());

                AddTablesToFilters(rawTables);
                IdentifyUniqueForeignKeys(rawIndexes, rawForeignKeys);
                AddIndexesToFilters(rawIndexes);
                SetPrimaryKeys();
                AddForeignKeysToFilters(rawForeignKeys);

                if (Settings.IncludeExtendedPropertyComments != CommentsStyle.None)
                    AddExtendedPropertiesToFilters(DatabaseReader.ReadExtendedProperties());

                SetupEntityAndConfig(); // Must be done last
            }
            catch (Exception x)
            {
                var error = FormatError(x);
                _fileManagementService.Error(string.Empty);
                _fileManagementService.Error("// -----------------------------------------------------------------------------------------");
                _fileManagementService.Error(string.Format("// Failed to read database schema in LoadTables() - {0}", error));
                _fileManagementService.Error("/*" + x.StackTrace + "*/");
                _fileManagementService.Error("// -----------------------------------------------------------------------------------------");
                _fileManagementService.Error(string.Empty);
            }
        }

        public static void IdentifyUniqueForeignKeys(List<RawIndex> rawIndexes, List<RawForeignKey> rawForeignKeys)
        {
            if (rawIndexes == null || rawForeignKeys == null || !rawIndexes.Any() || !rawForeignKeys.Any())
                return;

            var uniqueForeignKeys = from i in rawIndexes.Where(x => x.IsUniqueConstraint)
                join fk1 in rawForeignKeys
                    on new { X1 = i.Schema, X2 = i.TableName } equals new { X1 = fk1.PkSchema, X2 = fk1.PkTableName }
                select fk1;

            foreach (var fk in uniqueForeignKeys)
                fk.HasUniqueConstraint = true;
        }

        // Create tables from raw data for each of the DbContextFilters

        private void AddTablesToFilters(List<RawTable> rawTables)
        {
            if (rawTables == null || !rawTables.Any())
                return;

            var tablesNames = rawTables
                .Select(x => new { x.SchemaName, x.TableName, x.IsView })
                .Distinct()
                .OrderBy(x => x.SchemaName)
                .ThenBy(x => x.TableName)
                .ToList();

            var deleteFilteredOutFiles = Settings.FileManagerType == FileManagerType.Custom && Settings.GenerateSeparateFiles;

            foreach (var filterKeyValuePair in FilterList.GetFilters())
            {
                var filter = filterKeyValuePair.Value;

                foreach (var tn in tablesNames)
                {
                    var exclude = tn.IsView && !filter.IncludeViews;
                    if(exclude && !deleteFilteredOutFiles)
                        continue;

                    // Check if schema is excluded
                    var schema = new Schema(tn.SchemaName);
                    if (filter.IsExcluded(schema))
                    {
                        exclude = true;
                        if (!deleteFilteredOutFiles)
                            continue;
                    }

                    // Check if table is excluded
                    var table = new Table(filter, schema, tn.TableName, tn.IsView);
                    if (filter.IsExcluded(table))
                    {
                        exclude = true;
                        if (!deleteFilteredOutFiles)
                            continue;
                    }

                    // Handle table names with underscores - singularise just the last word
                    var tableName = filter.TableRename(tn.TableName, tn.SchemaName, tn.IsView);
                    var singularCleanTableName = Inflector.MakeSingular(DatabaseReader.CleanUp(tableName));
                    table.NameHumanCase = (Settings.UsePascalCase ? Inflector.ToTitleCase(singularCleanTableName) : singularCleanTableName).Replace(" ", "").Replace("$", "").Replace(".", "");

                    if (Settings.PrependSchemaName && string.Compare(table.Schema.DbName, Settings.DefaultSchema, StringComparison.OrdinalIgnoreCase) != 0)
                        table.NameHumanCase = table.Schema.DbName + "_" + table.NameHumanCase;

                    if (filter.IsExcluded(table)) // Retest exclusion after table rename
                    {
                        exclude = true;
                        if (!deleteFilteredOutFiles)
                            continue;
                    }

                    if(exclude)
                    {
                        FileManagementService.DeleteFile(table.NameHumanCaseWithSuffix() + Settings.FileExtension); // Poco
                        FileManagementService.DeleteFile(table.NameHumanCaseWithSuffix() + Settings.ConfigurationClassName + Settings.FileExtension); // Poco config
                        continue;
                    }

                    // Check for table or C# name clashes
                    if (DatabaseReader.ReservedKeywords.Contains(table.NameHumanCase) ||
                        (Settings.UsePascalCase && filter.Tables.Find(x => x.NameHumanCase == table.NameHumanCase) != null))
                    {
                        table.NameHumanCase += "1";
                    }

                    // Create columns for table
                    foreach (var rawTable in rawTables
                        .Where(x => x.SchemaName == tn.SchemaName && x.TableName == tn.TableName)
                        .OrderBy(x => x.Ordinal))
                    {
                        var column = DatabaseReader.CreateColumn(rawTable, table, filter);
                        if (column != null)
                            table.Columns.Add(column);
                    }

                    // Check for property name clashes in columns
                    foreach (var c in table.Columns.Where(c => table.Columns.FindAll(x => x.NameHumanCase == c.NameHumanCase).Count > 1))
                    {
                        var n = 1;
                        var original = c.NameHumanCase;
                        c.NameHumanCase = original + n++;

                        // Check if the above resolved the name clash, if not, use next value
                        while (c.ParentTable.Columns.Count(c2 => c2.NameHumanCase == c.NameHumanCase) > 1)
                            c.NameHumanCase = original + n++;
                    }

                    filter.Tables.Add(table);
                }

                foreach (var table in filter.Tables)
                {
                    if (table.IsView)
                        filter.ViewProcessing(table);

                    filter.UpdateTable(table);

                    foreach (var column in table.Columns)
                        filter.UpdateColumn(column, table);

                    table.Suffix = Settings.TableSuffix;
                }
            }
        }

        private void AddIndexesToFilters(List<RawIndex> rawIndexes)
        {
            if (rawIndexes == null || !rawIndexes.Any())
                return;

            var indexTables = rawIndexes
                .Select(x => new { x.Schema, x.TableName })
                .Distinct()
                .OrderBy(x => x.Schema)
                .ThenBy(x => x.TableName)
                .ToList();

            foreach (var filterKeyValuePair in FilterList.GetFilters())
            {
                var filter = filterKeyValuePair.Value;

                Table t = null;
                foreach (var indexTable in indexTables)
                {
                    // Lookup table
                    if (t == null || t.DbName != indexTable.TableName || t.Schema.DbName != indexTable.Schema)
                        t = filter.Tables.GetTable(indexTable.TableName, indexTable.Schema);

                    if (t == null)
                        continue;

                    // Find indexes for table
                    t.Indexes = rawIndexes.Where(x => x.Schema == indexTable.Schema && x.TableName == indexTable.TableName)
                            .OrderBy(o => o.ColumnCount)
                            .ThenBy(o => o.KeyOrdinal)
                            .ToList();

                    // Set index on column
                    foreach (var index in t.Indexes)
                    {
                        var col = t.Columns.Find(x => x.DbName == index.ColumnName);
                        if (col == null)
                            continue;

                        col.Indexes.Add(index);

                        col.IsPrimaryKey       = col.IsPrimaryKey       || index.IsPrimaryKey;
                        col.IsUniqueConstraint = col.IsUniqueConstraint || (index.IsUniqueConstraint && index.ColumnCount == 1);
                        col.IsUnique           = col.IsUnique           || (index.IsUnique           && index.ColumnCount == 1);
                    }

                    // Check if table has any primary keys
                    if (t.PrimaryKeys.Any())
                        continue; // Already has a primary key, ignore this unique index / constraint

                    // Find unique indexes for table
                    var uniqueIndexKeys = t.Indexes
                        .Where(x => x.IsUnique || x.IsPrimaryKey || x.IsUniqueConstraint)
                        .OrderBy(o => o.ColumnCount)
                        .ThenBy(o => o.KeyOrdinal);

                    // Process only the first index with the lowest unique column count
                    string indexName = null;
                    foreach (var key in uniqueIndexKeys)
                    {
                        if (indexName == null)
                            indexName = key.IndexName;

                        if (indexName != key.IndexName)
                            break; // First unique index with lowest column count has been processed, exit.

                        var col = t.Columns.Find(x => x.DbName == key.ColumnName);
                        if (col != null && !col.IsNullable && !col.Hidden && !col.IsPrimaryKey)
                        {
                            col.IsPrimaryKey       = true;
                            col.IsUniqueConstraint = true;
                            col.IsUnique           = true;
                            col.UniqueIndexName    = indexName;
                        }
                    }
                }
            }
        }

        private void AddForeignKeysToFilters(List<RawForeignKey> rawForeignKeys)
        {
            if (Settings.GenerateSingleDbContext && (rawForeignKeys == null || !rawForeignKeys.Any()))
                return;

            if (rawForeignKeys == null)
                rawForeignKeys = new List<RawForeignKey>();
            //else
                //SortForeignKeys(rawForeignKeys);

            foreach (var filterKeyValuePair in FilterList.GetFilters())
            {
                var filter = filterKeyValuePair.Value;
                var fks = new List<RawForeignKey>();
                fks.AddRange(rawForeignKeys/*.OrderBy(x => x.SortOrder).ThenBy(x => x.FkTableName).ThenBy(x => x.PkTableName)*/);

                if (!Settings.GenerateSingleDbContext)
                {
                    var multiContextSetting = ((MultiContextFilter) filter).GetSettings();
                    if (multiContextSetting?.ForeignKeys != null)
                    {
                        fks.AddRange(multiContextSetting.ForeignKeys.Select(x =>
                            new RawForeignKey(x.ConstraintName, x.ParentName, x.ChildName,
                                x.PkColumn, x.FkColumn, x.PkSchema, x.PkTableName,
                                x.FkSchema, x.FkTableName, x.Ordinal, x.CascadeOnDelete,
                                x.IsNotEnforced, x.HasUniqueConstraint)));
                    }
                }

                if (!fks.Any())
                    continue;

                var foreignKeys = new List<ForeignKey>();
                foreach (var rawForeignKey in fks)
                {
                    var fkTableNameFiltered = filter.TableRename(rawForeignKey.FkTableName, rawForeignKey.FkSchema, false);
                    var pkTableNameFiltered = filter.TableRename(rawForeignKey.PkTableName, rawForeignKey.PkSchema, false);

                    var fk = new ForeignKey(rawForeignKey, fkTableNameFiltered, pkTableNameFiltered);

                    var filteredFk = filter.ForeignKeyFilter(fk);
                    if (filteredFk != null)
                    {
                        if (Settings.ForeignKeyFilterFunc != null)
                            filteredFk = Settings.ForeignKeyFilterFunc(filteredFk);

                        if (filteredFk != null)
                            foreignKeys.Add(filteredFk);
                    }
                }

                IdentifyForeignKeys(foreignKeys, filter.Tables);
                Settings.AddExtraForeignKeys?.Invoke(filter, this, foreignKeys, filter.Tables);

                // Work out if there are any foreign key relationship naming clashes
                ProcessForeignKeys(foreignKeys, true, filter);

                // Mappings tables can only be true for Ef6
                if (Settings.UseMappingTables && !(Settings.IsEf6() || Settings.IsEfCore5Plus()))
                    Settings.UseMappingTables = false;
                
                if (Settings.UseMappingTables)
                    filter.Tables.IdentifyMappingTables(foreignKeys, true, DatabaseReader.IncludeSchema);

                // Now we know our foreign key relationships and have worked out if there are any name clashes,
                // re-map again with intelligently named relationships.
                filter.Tables.ResetNavigationProperties();

                ProcessForeignKeys(foreignKeys, false, filter);
                if (Settings.UseMappingTables)
                    filter.Tables.IdentifyMappingTables(foreignKeys, false, DatabaseReader.IncludeSchema);
            }
        }

        /*private void SortForeignKeys(List<RawForeignKey> rawForeignKeys)
        {
            foreach (var fk in rawForeignKeys)
            {
                fk.SortOrder = 10;
                
                var fkColumn = fk.FkColumn.ToLowerInvariant();
                var pkTable = fk.PkTableName.ToLowerInvariant();
                
                // Matches exactly
                if (fkColumn == pkTable)
                {
                    fk.SortOrder = 1;
                    continue;
                }

                // Matches without 'id'
                if(fkColumn.EndsWith("id") && fkColumn.Remove(fkColumn.Length - 2, 2) == pkTable)
                {
                    fk.SortOrder = 2;
                    continue;
                }

                // Matches if trimmed
                if(fkColumn.Length > pkTable.Length && fkColumn.Substring(0, pkTable.Length) == pkTable)
                {
                    fk.SortOrder = 3;
                    continue;
                }
            }
        }*/

        private void AddExtendedPropertiesToFilters(List<RawExtendedProperty> extendedProperties)
        {
            if (extendedProperties == null || !extendedProperties.Any())
                return;

            var commentsInSummaryBlock = Settings.IncludeExtendedPropertyComments == CommentsStyle.InSummaryBlock;
            var multiLine              = new Regex("[\r\n]+", RegexOptions.Compiled);
            var whiteSpace             = new Regex("\\s+", RegexOptions.Compiled);

            foreach (var filterKeyValuePair in FilterList.GetFilters())
            {
                var filter = filterKeyValuePair.Value;

                Table t = null;
                foreach (var extendedProperty in extendedProperties)
                {
                    // Lookup table
                    if (t == null || t.DbName != extendedProperty.TableName || t.Schema.DbName != extendedProperty.SchemaName)
                        t = filter.Tables.GetTable(extendedProperty.TableName, extendedProperty.SchemaName);

                    if (t == null)
                        continue;

                    if (extendedProperty.TableLevelExtendedComment)
                    {
                        // Table level extended comment
                        t.ExtendedProperty.Add(multiLine.Replace(extendedProperty.ExtendedProperty, "\r\n    /// "));
                        continue;
                    }

                    // Column level extended comment
                    var col = t.Columns.Find(x => x.DbName == extendedProperty.ColumnName);
                    if (col == null)
                        continue;

                    if (commentsInSummaryBlock)
                        col.ExtendedProperty = multiLine.Replace(extendedProperty.ExtendedProperty, "\r\n        /// ");
                    else
                        col.ExtendedProperty = whiteSpace.Replace(multiLine.Replace(extendedProperty.ExtendedProperty, " "), " ");
                }
            }
        }

        private void SetPrimaryKeys()
        {
            foreach (var filterKeyValuePair in FilterList.GetFilters())
            {
                var filter = filterKeyValuePair.Value;
                foreach (var table in filter.Tables)
                {
                    table.SetPrimaryKeys();
                }

                if (HasTriallicense)
                    filter.Tables.TrimForTriallicense();
            }
        }

        private void SetupEntityAndConfig()
        {
            foreach (var filterKeyValuePair in FilterList.GetFilters())
            {
                var filter = filterKeyValuePair.Value;
                foreach (var table in filter.Tables)
                {
                    table.Columns.ForEach(SetupEntityAndConfig);
                }
            }
        }

        public void LoadStoredProcs()
        {
            if (_factory == null || DatabaseReader == null || !DatabaseReader.CanReadStoredProcedures())
                return;

            try
            {
                var deleteFilteredOutFiles = Settings.FileManagerType == FileManagerType.Custom && Settings.GenerateSeparateFiles;

                var spFilters = FilterList
                    .GetFilters()
                    .Where(x => x.Value.IncludeStoredProcedures || x.Value.IncludeTableValuedFunctions || x.Value.IncludeScalarValuedFunctions)
                    .ToList();

                if (!spFilters.Any())
                    return;

                var includeSynonyms = FilterList.IncludeSynonyms();
                var rawStoredProcs = DatabaseReader.ReadStoredProcs(includeSynonyms);

                // Only call stored procedures to obtain the return models that are not filtered out
                // We don't want to do this for every db context we are generating as that is inefficient
                var procs = rawStoredProcs
                    .Select(sp => new { sp.Schema, sp.Name, sp.IsTableValuedFunction, sp.IsScalarValuedFunction, sp.IsStoredProcedure })
                    .Distinct()
                    .OrderBy(x => x.Schema)
                    .ThenBy(x => x.Name);

                var storedProcs = new List<StoredProcedure>();
                foreach (var proc in procs)
                {
                    var sp = new StoredProcedure
                    {
                        DbName                 = proc.Name,
                        NameHumanCase          = (Settings.UsePascalCase ? Inflector.ToTitleCase(proc.Name) : proc.Name).Replace(" ", "").Replace("$", ""),
                        Schema                 = new Schema(proc.Schema),
                        IsTableValuedFunction  = proc.IsTableValuedFunction,
                        IsScalarValuedFunction = proc.IsScalarValuedFunction,
                        IsStoredProcedure      = proc.IsStoredProcedure
                    };
                    sp.NameHumanCase = DatabaseReader.CleanUp(sp.NameHumanCase);
                    if (Settings.PrependSchemaName && (string.Compare(proc.Schema, Settings.DefaultSchema, StringComparison.OrdinalIgnoreCase) != 0))
                        sp.NameHumanCase = proc.Schema + "_" + sp.NameHumanCase;

                    sp.Parameters.AddRange(rawStoredProcs
                        .Where(x => x.Parameter != null &&
                                    x.Schema == proc.Schema &&
                                    x.Name == proc.Name)
                        .Select(x => x.Parameter));

                    sp.HasSpatialParameter = sp.Parameters.Any(x => x.IsSpatial);

                    if (Settings.DisableGeographyTypes && sp.HasSpatialParameter)
                        continue; // Ignore stored procedure due to spatial parameter

                    // Check to see if this stored proc is to be kept by any of the filters
                    if (spFilters.All(x => x.Value.IsExcluded(sp)))
                    {
                        if(deleteFilteredOutFiles)
                            FileManagementService.DeleteFile(sp.WriteStoredProcReturnModelName(spFilters[0].Value) + Settings.FileExtension);

                        continue; // All Db Context exclude this stored proc, ignore it as nobody wants it
                    }

                    storedProcs.Add(sp);
                }

                if (!storedProcs.Any())
                    return; // No stored procs to read the return model for, so exit

                // Read in the return objects for the wanted stored proc
                DatabaseReader.ReadStoredProcReturnObjects(storedProcs);

                // Check if any of the stored proc return models have spatial types
                /*foreach (var sp in storedProcs)
                {
                    if(sp.ReturnModels.Any())
                    {
                        // todo
                    }
                }*/

                // Force generation of Async stored procs
                /*foreach (var sp in storedProcs.Where(x => !x.IsTableValuedFunction && !x.ReturnModels.Any()))
                {
                    sp.ReturnModels.Add(new List<DataColumn> { new DataColumn("result", typeof(int)) });
                }*/

                // Remove stored procs where the return model type contains spaces and cannot be mapped
                // Also need to remove any TVF functions with parameters that are non scalar types, such as DataTable
                var validStoredProcs = new List<StoredProcedure>();
                foreach (var sp in storedProcs)
                {
                    if (!sp.ReturnModels.Any())
                    {
                        validStoredProcs.Add(sp);
                        continue;
                    }

                    if (sp.ReturnModels.Any(returnColumns => returnColumns.Any(c => c.ColumnName.Contains(" "))))
                        continue; // Invalid, ignore stored procedure

                    if (sp.IsTableValuedFunction && sp.Parameters.Any(c => c.PropertyType == "DataTable"))
                        continue; // Invalid, ignore

                    validStoredProcs.Add(sp); // Valid, keep this stored proc
                }

                // Update the list of stored procs for each of the db context filters that want them
                foreach (var filterKeyValuePair in spFilters)
                {
                    var filter = filterKeyValuePair.Value;
                    foreach (var sp in validStoredProcs)
                    {
                        if (!filter.IsExcluded(sp))
                        {
                            if (HasTriallicense)
                            {
                                const int n = 1 + 2 + 3 + 4;
                                if (filter.StoredProcs.Count < n)
                                    filter.StoredProcs.Add(sp);
                            }
                            else
                                filter.StoredProcs.Add(sp);
                        }
                        else
                        {
                            if (deleteFilteredOutFiles)
                                FileManagementService.DeleteFile(sp.WriteStoredProcReturnModelName(filter) + Settings.FileExtension);
                        }
                    }
                }
            }
            catch (Exception x)
            {
                var error = FormatError(x);
                _fileManagementService.Error(string.Empty);
                _fileManagementService.Error("// -----------------------------------------------------------------------------------------");
                _fileManagementService.Error(string.Format("// Failed to read database schema for stored procedures - {0}", error));
                _fileManagementService.Error("/*" + x.StackTrace + "*/");
                _fileManagementService.Error("// -----------------------------------------------------------------------------------------");
                _fileManagementService.Error(string.Empty);
            }
        }

        /// <summary>AddRelationship overload for single-column foreign-keys.</summary>
        public void AddRelationship(IDbContextFilter filter, List<ForeignKey> fkList, Tables tablesAndViews, string name, string pkSchema, string pkTable, string pkColumn, string fkSchema, string fkTable, string fkColumn, string parentName, string childName, bool isUnique)
        {
            AddRelationship(filter, fkList, tablesAndViews, name, pkSchema, pkTable, new[] { pkColumn }, fkSchema, fkTable, new[] { fkColumn }, parentName, childName, isUnique);
        }

        public void AddRelationship(IDbContextFilter filter, List<ForeignKey> fkList, Tables tablesAndViews, string relationshipName, 
            string pkSchema, string pkTableName, string[] pkColumns, 
            string fkSchema, string fkTableName, string[] fkColumns,
            string parentName, string childName, bool isUnique)
        {
            // Argument validation:
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            if (fkList == null) throw new ArgumentNullException(nameof(fkList));
            if (tablesAndViews == null) throw new ArgumentNullException(nameof(tablesAndViews));
            if (string.IsNullOrEmpty(relationshipName)) throw new ArgumentNullException(nameof(relationshipName));
            if (string.IsNullOrEmpty(pkSchema)) throw new ArgumentNullException(nameof(pkSchema));
            if (string.IsNullOrEmpty(pkTableName)) throw new ArgumentNullException(nameof(pkTableName));
            if (pkColumns == null) throw new ArgumentNullException(nameof(pkColumns));
            if (pkColumns.Length == 0 || pkColumns.Any(string.IsNullOrEmpty)) throw new ArgumentException("Invalid primary-key columns: No primary-key column names are specified, or at least one primary-key column name is empty.", nameof(pkColumns));
            if (string.IsNullOrEmpty(fkSchema)) throw new ArgumentNullException(nameof(fkSchema));
            if (string.IsNullOrEmpty(fkTableName)) throw new ArgumentNullException(nameof(fkTableName));
            if (fkColumns == null) throw new ArgumentNullException(nameof(fkColumns));
            if (fkColumns.Length != pkColumns.Length || fkColumns.Any(string.IsNullOrEmpty)) throw new ArgumentException("Invalid foreign-key columns:Foreign-key column list has a different number of columns than the primary-key column list, or at least one foreign-key column name is empty.", nameof(pkColumns));

            //////////////////

            var pkTable = tablesAndViews.GetTable(pkTableName, pkSchema);
            if (pkTable == null)
                throw new ArgumentException("Couldn't find table " + pkSchema + "." + pkTableName);

            var fkTable = tablesAndViews.GetTable(fkTableName, fkSchema);
            if (fkTable == null)
                throw new ArgumentException("Couldn't find table " + fkSchema + "." + fkTableName);

            // Ensure all columns exist:
            foreach (var pkCol in pkColumns)
            {
                if (pkTable.Columns.SingleOrDefault(c => c.DbName == pkCol) == null)
                    throw new ArgumentException("The relationship primary-key column \"" + pkCol + "\" does not exist in table or view " + pkSchema + "." + pkTableName);
            }
            foreach (var fkCol in fkColumns)
            {
                if (fkTable.Columns.SingleOrDefault(c => c.DbName == fkCol) == null)
                    throw new ArgumentException("The relationship foreign-key column \"" + fkCol + "\" does not exist in table or view " + fkSchema + "." + fkTableName);
            }

            for (var i = 0; i < pkColumns.Length; i++)
            {
                var pkc = pkColumns[i];
                var fkc = fkColumns[i];

                var fkTableNameFiltered = filter.TableRename(fkTableName, fkSchema, fkTable.IsView);
                var pkTableNameFiltered = filter.TableRename(pkTableName, pkSchema, pkTable.IsView);

                var fk = new ForeignKey(
                    fkTable.DbName,
                    fkSchema,
                    pkTable.DbName,
                    pkSchema,
                    fkc,
                    pkc,
                    "AddRelationship: " + relationshipName,
                    fkTableNameFiltered,
                    pkTableNameFiltered,
                    int.MaxValue,
                    false,
                    false,
                    parentName,
                    childName,
                    isUnique
                ) { IncludeReverseNavigation = true };

                fkList.Add(fk);
                fkTable.HasForeignKey = true;
            }
        }

        private void ProcessForeignKeys(List<ForeignKey> fkList, bool checkForFkNameClashes, IDbContextFilter filter)
        {
            var constraints = fkList.Select(x => x.FkSchema + "." + x.ConstraintName).Distinct();
            foreach (var constraint in constraints)
            {
                var foreignKeys = fkList
                    .Where(x => string.Format("{0}.{1}", x.FkSchema, x.ConstraintName).Equals(constraint, StringComparison.InvariantCultureIgnoreCase))
                    .ToList();

                var foreignKey = foreignKeys.First();
                var fkTable = filter.Tables.GetTable(foreignKey.FkTableName, foreignKey.FkSchema);
                if (fkTable == null || fkTable.IsMapping || !fkTable.HasForeignKey)
                    continue;

                var pkTable = filter.Tables.GetTable(foreignKey.PkTableName, foreignKey.PkSchema);
                if (pkTable == null || pkTable.IsMapping)
                    continue;

                var fkCols = foreignKeys.Select(x => new ColumnAndForeignKey
                    {
                        ForeignKey = x,
                        Column = fkTable.Columns.Find(n => string.Equals(n.DbName, x.FkColumn, StringComparison.InvariantCultureIgnoreCase))
                    })
                    .Where(x => x.Column != null)
                    .OrderBy(o => o.ForeignKey.Ordinal)
                    .ToList();

                if (!fkCols.Any())
                    continue;

                if (FkMustHaveSameNumberOfColumnsAsPrimaryKey() || AllowFkToNonPrimaryKey())
                {
                    // Check FK has same number of columns as the primary key it points to
                    var pks  = pkTable.PrimaryKeys         .OrderBy(x => x.PropertyType).ThenBy(y => y.DbName).ToArray();
                    var cols = fkCols.Select(x => x.Column).OrderBy(x => x.PropertyType).ThenBy(y => y.DbName).ToArray();

                    if (FkMustHaveSameNumberOfColumnsAsPrimaryKey() && pks.Length != cols.Length)
                    {
                        // Also check unique constraints
                        if (!AllowFkToNonPrimaryKey())
                            continue;

                        if(!fkCols.All(x => x.ForeignKey.HasUniqueConstraint))
                            continue;
                    }

                    if (!AllowFkToNonPrimaryKey() && pks.Where((pk, n) => pk.PropertyType != cols[n].PropertyType).Any())
                        continue;
                }

                var pkCols = foreignKeys.Select(x => new ColumnAndForeignKey
                    {
                        ForeignKey = x,
                        Column = pkTable.Columns.Find(n => string.Equals(n.DbName, x.PkColumn, StringComparison.InvariantCultureIgnoreCase))
                    })
                    .Where(x => x.Column != null)
                    .OrderBy(o => o.ForeignKey.Ordinal)
                    .ToList();

                if (!pkCols.Any())
                    continue;

                var allPkColsArePrimaryKeys = pkCols.All(c => c.Column.IsPrimaryKey);
                if (!AllowFkToNonPrimaryKey() && !allPkColsArePrimaryKeys)
                    continue; // Cannot have a FK to a non-primary key

                var relationship = CalcRelationship(pkTable, fkTable, fkCols, pkCols);
                if (relationship == Relationship.DoNotUse)
                    continue;

                var pkTableHumanCaseWithSuffix = foreignKey.PkTableHumanCase(pkTable.Suffix);
                var pkTableHumanCase           = foreignKey.PkTableHumanCase(null);
                var fkHasUniqueConstraint      = pkCols.All(x => x.ForeignKey.HasUniqueConstraint) && relationship == Relationship.OneToOne;

                if (fkHasUniqueConstraint && pkCols.Any(x => x.Column.IsNullable))
                    continue; // This would force the column to be not null

                var flipRelationship       = FlipRelationship(relationship);
                var fkMakePropNameSingular = relationship == Relationship.OneToOne;
                var pkPropName             = fkTable.GetUniqueForeignKeyName(true,  pkTableHumanCase,      foreignKey, checkForFkNameClashes, true,                   relationship);
                var fkPropName             = pkTable.GetUniqueForeignKeyName(false, fkTable.NameHumanCase, foreignKey, checkForFkNameClashes, fkMakePropNameSingular, flipRelationship);

                var fkd = new PropertyAndComments
                {
                    AdditionalDataAnnotations = filter.ForeignKeyAnnotationsProcessing(fkTable, pkTable, pkPropName, fkPropName),
                    
                    PropertyName = pkPropName,

                    Definition = string.Format("public {0}{1} {2} {3}{4}", 
                        Table.GetLazyLoadingMarker(),
                        pkTableHumanCaseWithSuffix,
                        pkPropName,
                        "{ get; set; }",
                        Settings.IncludeComments != CommentsStyle.None ? " // " + foreignKey.ConstraintName : string.Empty),

                    Comments = string.Format("Parent {0} pointed by [{1}].({2}) ({3})",
                        pkTableHumanCase,
                        fkTable.DbName,
                        string.Join(", ", fkCols.Select(x => "[" + x.Column.NameHumanCase + "]").Distinct().ToArray()),
                        foreignKey.ConstraintName)
                };

                var firstFkCol = fkCols.First();
                firstFkCol.Column.EntityFk.Add(fkd);

                string manyToManyMapping, mapKey;
                if (foreignKeys.Count > 1)
                {
                    manyToManyMapping = string.Format("c => new {{ {0} }}", string.Join(", ", fkCols.Select(x => "c." + x.Column.NameHumanCase).Distinct().ToArray()));
                    mapKey = string.Format("{0}", string.Join(",", fkCols.Select(x => "\"" + x.Column.DbName + "\"").Distinct().ToArray()));
                }
                else
                {
                    manyToManyMapping = string.Format("c => c.{0}", firstFkCol.Column.NameHumanCase);
                    mapKey = string.Format("\"{0}\"", firstFkCol.Column.DbName);
                }

                var primaryKeyColumns = string.Empty;
                if (!allPkColsArePrimaryKeys)
                { 
                    if (pkCols.Count > 1)
                        primaryKeyColumns = string.Format("p => new {{ {0} }}", string.Join(", ", pkCols.Select(x => "p." + x.Column.NameHumanCase).Distinct().ToArray()));
                    else
                        primaryKeyColumns = string.Format("p => p.{0}", pkCols.First().Column.NameHumanCase);
                }

                var fkCols2 = fkCols.Select(c => c.Column).ToList();
                var pkCols2 = pkCols.Select(c => c.Column).ToList();

                var rel = GetRelationship(relationship, fkCols2, pkCols2, pkPropName, fkPropName, manyToManyMapping, mapKey, foreignKey.CascadeOnDelete, foreignKey.IncludeReverseNavigation, foreignKey.IsNotEnforced, foreignKey.ConstraintName, pkTableHumanCase, fkTable.NameHumanCase, primaryKeyColumns, fkHasUniqueConstraint);
                var com = Settings.IncludeComments != CommentsStyle.None && string.IsNullOrEmpty(GetForeignKeyConstraintName("x")) ? " // " + foreignKey.ConstraintName : string.Empty;
                firstFkCol.Column.ConfigFk.Add(string.Format("{0};{1}", rel, com));

                if (foreignKey.IncludeReverseNavigation)
                    pkTable.AddReverseNavigation(relationship, fkTable, fkPropName, string.Format("{0}.{1}", fkTable.DbName, foreignKey.ConstraintName), foreignKeys);
            }
        }

        private void IdentifyForeignKeys(List<ForeignKey> fkList, Tables tables)
        {
            foreach (var foreignKey in fkList)
            {
                var fkTable = tables.GetTable(foreignKey.FkTableName, foreignKey.FkSchema);
                if (fkTable == null)
                    continue; // Could be filtered out

                var pkTable = tables.GetTable(foreignKey.PkTableName, foreignKey.PkSchema);
                if (pkTable == null)
                    continue; // Could be filtered out

                var fkCol = fkTable.Columns.Find(n => string.Equals(n.DbName, foreignKey.FkColumn, StringComparison.InvariantCultureIgnoreCase));
                if (fkCol == null)
                    continue; // Could not find fk column

                var pkCol = pkTable.Columns.Find(n => string.Equals(n.DbName, foreignKey.PkColumn, StringComparison.InvariantCultureIgnoreCase));
                if (pkCol == null)
                    continue; // Could not find pk column

                fkTable.HasForeignKey = true;
            }
        }

        private string GetRelationship(Relationship relationship, IList<Column> fkCols, IList<Column> pkCols, string pkPropName, string fkPropName,
            string manyToManyMapping, string mapKey, bool cascadeOnDelete, bool includeReverseNavigation, bool isNotEnforced, string foreignKeyConstraintName,
            string pkTableNameHumanCase, string fkTableNameHumanCase, string primaryKeyColumns, bool fkHasUniqueConstraint)
        {
            var hasMethod = GetHasMethod(relationship, fkCols, pkCols, isNotEnforced, fkHasUniqueConstraint);
            if (hasMethod == null)
                return string.Empty; // Relationship not supported

            var withMethod = GetWithMethod(relationship, fkCols, fkPropName, manyToManyMapping, mapKey, includeReverseNavigation, hasMethod, pkTableNameHumanCase, fkTableNameHumanCase, primaryKeyColumns, fkHasUniqueConstraint);

            return string.Format("{0}(a => a.{1}){2}{3}{4}",
                hasMethod,
                pkPropName,
                withMethod,
                GetCascadeOnDelete(cascadeOnDelete),
                GetForeignKeyConstraintName(foreignKeyConstraintName));
        }

        // Calculates the relationship between a child table and it's parent table.
        public static Relationship CalcRelationship(Table parentTable, Table childTable, List<ColumnAndForeignKey> fkCols, List<ColumnAndForeignKey> pkCols)
        {
            var childTableCols  = fkCols.Select(c => c.Column).ToList();
            var parentTableCols = pkCols.Select(c => c.Column).ToList();

            if (childTableCols.Count == 1 && parentTableCols.Count == 1)
                return CalcRelationshipSingle(parentTable, childTable, childTableCols.First(), parentTableCols.First());

            // This relationship has multiple composite keys

            // childTable FK columns are exactly the primary key (they are part of primary key, and no other columns are primary keys)
            // TODO: we could also check if they are a unique index
            var childTableColumnsAllPrimaryKeys = (childTableCols.Count == childTableCols.Count(x => x.IsPrimaryKey)) && 
                                                  (childTableCols.Count == childTable.PrimaryKeys.Count());

            // parentTable columns are exactly the primary key (they are part of primary key, and no other columns are primary keys)
            // TODO: we could also check if they are a unique index
            var parentTableColumnsAllPrimaryKeys = (parentTableCols.Count == parentTableCols.Count(x => x.IsPrimaryKey)) && 
                                                   (parentTableCols.Count == parentTable.PrimaryKeys.Count());

            // childTable FK columns are not only FK but also the whole PK (not only part of PK); parentTable columns are the whole PK (not only part of PK) - so it's 1:1
            if (childTableColumnsAllPrimaryKeys && parentTableColumnsAllPrimaryKeys)
                return Relationship.OneToOne;

            // Check if covered by a unique constraint
            if(fkCols.All(x => x.ForeignKey.HasUniqueConstraint))
                return Relationship.OneToOne;

            // Check if covered by a unique index on PK table
            if (parentTableCols.All(x => x.IsUnique))
                return Relationship.OneToOne;

            return Relationship.ManyToOne;
        }

        // Calculates the relationship between a child table and it's parent table.
        public static Relationship CalcRelationshipSingle(Table parentTable, Table childTable, Column childTableCol, Column parentTableCol)
        {
            if (!childTableCol.IsPrimaryKey && !childTableCol.IsUniqueConstraint)
                return Relationship.ManyToOne;

            if (!parentTableCol.IsPrimaryKey && !parentTableCol.IsUniqueConstraint)
                return Relationship.ManyToOne;

            if (childTable.PrimaryKeys.Count() != 1)
                return Relationship.ManyToOne;

            if (parentTable.PrimaryKeys.Count() != 1)
                return Relationship.ManyToOne;

            return Relationship.OneToOne;
        }

        public static Relationship FlipRelationship(Relationship relationship)
        {
            switch (relationship)
            {
                case Relationship.OneToOne:   return Relationship.OneToOne;
                case Relationship.OneToMany:  return Relationship.ManyToOne;
                case Relationship.ManyToOne:  return Relationship.OneToMany;
                case Relationship.ManyToMany: return Relationship.ManyToMany;
                case Relationship.DoNotUse:   return Relationship.DoNotUse;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(relationship), relationship, null);
            }
        }

        public void SetupEntityAndConfig(Column c)
        {
            SetupEntity(c);
            SetupConfig(c);
        }

        public void GenerateCode()
        {
            try
            {
                var fallback = Settings.TemplateFolder;
                foreach (var filter in FilterList.GetFilters())
                {
                    _fileManagementService.UseFileManager(filter.Key);
                    if (!Settings.GenerateSingleDbContext)
                    {
                        // Multi-context
                        Settings.DbContextInterfaceName = null;
                        Settings.DbContextName = ((MultiContextFilter) filter.Value).GetSettings().Name ?? filter.Key;

                        if (Settings.TemplateType == TemplateType.FileBasedCore3 ||
                            Settings.TemplateType == TemplateType.FileBasedCore5)
                        {
                            // Use file based templates, set the path
                            var multiContextSetting = ((MultiContextFilter) filter.Value).GetSettings();
                            if (multiContextSetting != null && !string.IsNullOrEmpty(multiContextSetting.TemplatePath))
                                Settings.TemplateFolder = multiContextSetting.TemplatePath;
                        }
                    }

                    GenerateCode(filter.Value);
                    Settings.TemplateFolder = fallback; // Reset back
                }
            }
            catch (Exception x)
            {
                var error = FormatError(x);
                _fileManagementService.Error(string.Empty);
                _fileManagementService.Error("// -----------------------------------------------------------------------------------------");
                _fileManagementService.Error(string.Format("// Failed to generate the code in GenerateCode() - {0}", error));
                _fileManagementService.Error("/*" + x.StackTrace + "*/");
                _fileManagementService.Error("// -----------------------------------------------------------------------------------------");
                _fileManagementService.Error(string.Empty);
            }
        }

        private void GenerateCode(IDbContextFilter filter)
        {
            var codeGenerator = new CodeGenerator(this, filter);

            const string contextInterface = "contextInterface:";
            const string contextFactory   = "contextFactory:";
            const string contextClass     = "contextClass:";
            const string contextFakeClass = "contextFakeClass:";
            const string contextFakeDbSet = "contextFakeDbSet:";
            const string pocoClass        = "pocoClass:";
            const string pocoConfiguration= "pocoConfiguration:";
            const string spReturnModels   = "spReturnModel:";
            const string enumType         = "enumType:";

            var codeOutputList = new CodeOutputList();
            codeOutputList.Add(contextInterface, codeGenerator.GenerateInterface());
            codeOutputList.Add(contextFactory,   codeGenerator.GenerateFactory());
            codeOutputList.Add(contextClass,     codeGenerator.GenerateContext());
            codeOutputList.Add(contextFakeClass, codeGenerator.GenerateFakeContext());
            codeOutputList.Add(contextFakeDbSet, codeGenerator.GenerateFakeDbSet());

            var isEfCore3Plus = Settings.IsEfCore3Plus();

            foreach (var table in filter.Tables
                .Where(t => !t.IsMapping)
                .OrderBy(x => x.NameHumanCase))
            {
                // Write poco class, even if it has no primary key, for completeness.
                codeOutputList.Add(pocoClass + table.NameHumanCase, codeGenerator.GeneratePoco(table));

                // Only write the config if it has a primary key
                if (table.HasPrimaryKey || (table.IsView && isEfCore3Plus))
                    codeOutputList.Add(pocoConfiguration + table.NameHumanCase, codeGenerator.GeneratePocoConfiguration(table));
            }

            foreach (var sp in filter.StoredProcs
                .Where(x => x.ReturnModels.Count > 0 && 
                            x.ReturnModels.Any(returnColumns => returnColumns.Any()) && 
                            !Settings.StoredProcedureReturnTypes.ContainsKey(x.NameHumanCase) && 
                            !Settings.StoredProcedureReturnTypes.ContainsKey(x.DbName))
                .OrderBy(x => x.NameHumanCase))
            {
                var key = spReturnModels + sp.WriteStoredProcReturnModelName(filter);
                codeOutputList.Add(key, codeGenerator.GenerateStoredProcReturnModel(sp));
            }

            foreach (var enumeration in filter.Enums)
            {
                codeOutputList.Add(enumType + enumeration.EnumName, codeGenerator.GenerateEnum(enumeration));
            }

            FileHeaderFooter = new FileHeaderFooter(filter.SubNamespace);
            if (!Settings.GenerateSeparateFiles)
            {
                var preHeader = _preHeaderInfo.ToString();
                if(!string.IsNullOrWhiteSpace(preHeader))
                    _fileManagementService.WriteLine(preHeader.Trim());

                var header = FileHeaderFooter.Header;
                if (!string.IsNullOrWhiteSpace(header))
                    _fileManagementService.WriteLine(header.Trim());

                var usings = codeGenerator.GenerateUsings(codeOutputList.GetUsings());
                if (!string.IsNullOrWhiteSpace(usings))
                {
                    _fileManagementService.WriteLine("");
                    _fileManagementService.WriteLine(usings.Trim());
                }

                var ns = FileHeaderFooter.Namespace;
                if (!string.IsNullOrWhiteSpace(ns))
                {
                    _fileManagementService.WriteLine("");
                    _fileManagementService.WriteLine(ns.Trim());
                }
            }

            // Write the pre header info with the database context and it's interface
            if(codeOutputList.Files.ContainsKey(contextInterface)) WriteCodeOutput(codeGenerator, codeOutputList.Files[contextInterface], true, firstInGroup: true);
            if(codeOutputList.Files.ContainsKey(contextClass))     WriteCodeOutput(codeGenerator, codeOutputList.Files[contextClass], true, firstInGroup: true);
            if(codeOutputList.Files.ContainsKey(contextFactory))   WriteCodeOutput(codeGenerator, codeOutputList.Files[contextFactory], true);
            if(codeOutputList.Files.ContainsKey(contextFakeClass)) WriteCodeOutput(codeGenerator, codeOutputList.Files[contextFakeClass], true, firstInGroup: true);
            if(codeOutputList.Files.ContainsKey(contextFakeDbSet)) WriteCodeOutput(codeGenerator, codeOutputList.Files[contextFakeDbSet], true);

            WriteCodeOutputForGroup(codeGenerator, "POCO classes", true,
                codeOutputList.Files
                    .Where(x => x.Key.StartsWith(pocoClass))
                    .OrderBy(x => x.Key)
                    .Select(x => x.Value)
                    .ToList());

            WriteCodeOutputForGroup(codeGenerator, "POCO Configuration", true,
                codeOutputList.Files
                    .Where(x => x.Key.StartsWith(pocoConfiguration))
                    .OrderBy(x => x.Key)
                    .Select(x => x.Value)
                    .ToList());

            WriteCodeOutputForGroup(codeGenerator, "Enumerations", true,
                codeOutputList.Files
                    .Where(x => x.Key.StartsWith(enumType))
                    .OrderBy(x => x.Key)
                    .Select(x => x.Value)
                    .ToList());

            WriteCodeOutputForGroup(codeGenerator, "Stored procedure return models", true,
                codeOutputList.Files
                    .Where(x => x.Key.StartsWith(spReturnModels))
                    .OrderBy(x => x.Key)
                    .Select(x => x.Value)
                    .ToList());

            if (!Settings.GenerateSeparateFiles)
                _fileManagementService.WriteLine(FileHeaderFooter.Footer);
        }

        private void WriteCodeOutputForGroup(CodeGenerator codeGenerator, string regionNameForGroup, bool writePreHeaderInfo, List<CodeOutput> list)
        {
            var count = 0;
            var max = list.Count;
            foreach (var co in list)
            {
                ++count;
                WriteCodeOutput(codeGenerator, co, writePreHeaderInfo, regionNameForGroup, count == 1, count == max);
            }
        }

        private void WriteCodeOutput(CodeGenerator codeGenerator, CodeOutput code, bool writePreHeaderInfo, string regionNameForGroup = null, bool firstInGroup = false, bool lastInGroup = false)
        {
            if (Settings.GenerateSeparateFiles)
            {
                _fileManagementService.EndBlock();
                _fileManagementService.StartNewFile(code.Filename);

                // If generating separate files, check if the db context is the same name as the tt filename.
                // If it is the same, force writing to outer.
                if (Path.GetFileNameWithoutExtension(code.Filename).Equals(Settings.TemplateFile, StringComparison.CurrentCultureIgnoreCase))
                    _fileManagementService.ForceWriteToOuter = true;

                if (writePreHeaderInfo)
                {
                    var preHeader = _preHeaderInfo.ToString();
                    if (!string.IsNullOrWhiteSpace(preHeader))
                        _fileManagementService.WriteLine(preHeader.Trim());
                }

                var header = FileHeaderFooter.Header;
                if (!string.IsNullOrWhiteSpace(header))
                    _fileManagementService.WriteLine(header.Trim());

                var usings = codeGenerator.GenerateUsings(code.GetUsings());
                if (!string.IsNullOrWhiteSpace(usings))
                {
                    _fileManagementService.WriteLine("");
                    _fileManagementService.WriteLine(usings.Trim());
                }

                var ns = FileHeaderFooter.Namespace;
                if (!string.IsNullOrWhiteSpace(ns))
                {
                    _fileManagementService.WriteLine("");
                    _fileManagementService.WriteLine(ns.Trim());
                }
            }

            WriteLines(IndentCode(code, regionNameForGroup, firstInGroup, lastInGroup));

            if (Settings.GenerateSeparateFiles)
                _fileManagementService.WriteLine(FileHeaderFooter.Footer);

            _fileManagementService.ForceWriteToOuter = false;
        }

        private List<string> IndentCode(CodeOutput output, string regionNameForGroup, bool firstInGroup, bool lastInGroup)
        {
            if (output == null)
                return null;

            var indentNum = Settings.UseNamespace ? 1 : 0;
            var useRegion = !Settings.GenerateSeparateFiles && !string.IsNullOrEmpty(output.Region);

            var lines = new List<string>();
            if (Settings.UseRegions && useRegion)
            {
                lines.Add(IndentedStringBuilder(indentNum, "#region " + output.Region));
                lines.Add(null); // Add blank line after the region
            }
            else
            {
                if (Settings.UseRegions && !Settings.GenerateSeparateFiles && firstInGroup && !string.IsNullOrEmpty(regionNameForGroup))
                {
                    lines.Add(IndentedStringBuilder(indentNum, "#region " + regionNameForGroup));
                    lines.Add(null); // Add blank line after the region group
                }
            }

            if (firstInGroup && (HasAcademiclicense || HasTriallicense))
            {
                lines.Add(IndentedStringBuilder(indentNum, "// ****************************************************************************************************"));
                lines.Add(IndentedStringBuilder(indentNum, "// This is not a commercial license, therefore only a few tables/views/stored procedures are generated."));
                lines.Add(IndentedStringBuilder(indentNum, "// ****************************************************************************************************"));
                lines.Add(null);
            }

            if (Settings.IncludeCodeGeneratedAttribute)
                lines.Add(IndentedStringBuilder(indentNum, _codeGeneratedAttribute));

            lines.AddRange(IndentedStringBuilder(indentNum, output.Code));

            if (useRegion || (!Settings.GenerateSeparateFiles && lastInGroup && !string.IsNullOrEmpty(regionNameForGroup)))
            {
                if (Settings.UseRegions)
                {
                    lines.Add(null); // Include blank line after #endregion
                    lines.Add(IndentedStringBuilder(indentNum, "#endregion"));
                    lines.Add(null); // Include blank line after #endregion
                }
                else
                {
                    lines.Add(null); // Include blank line

                }
            }

            return lines;
        }

        protected string IndentedStringBuilder(int indentNum, string line)
        {
            var indent = new string(' ', indentNum * Indent);
            return string.IsNullOrWhiteSpace(line) ? string.Empty : string.Format("{0}{1}", indent, line);
        }

        protected IEnumerable<string> IndentedStringBuilder(int indentNum, List<string> lines)
        {
            var indent = new string(' ', indentNum * Indent);

            return
            (
                from line in lines
                select string.IsNullOrWhiteSpace(line) ? string.Empty : string.Format("{0}{1}", indent, line)
            );
        }

        private void WriteLines(List<string> lines)
        {
            foreach (var line in lines)
                _fileManagementService.WriteLine(line);
        }

        private static string FormatError(Exception ex)
        {
            return ex.Message.Replace("\r\n", "\n").Replace("\n", " ");
        }

        private void BuildPreHeaderInfo(license license)
        {
            if (Settings.ShowLicenseInfo)
            {
                _preHeaderInfo.Append("// This code was generated by EntityFramework Reverse POCO Generator");
                if (Settings.IncludeGeneratorVersionInCode)
                {
                    _preHeaderInfo.Append(" ");
                    _preHeaderInfo.Append(EfrpgVersion.Version());
                }

                _preHeaderInfo.AppendLine(" (http://www.reversepoco.co.uk/).");

                _preHeaderInfo.AppendLine("// Created by Simon Hughes (https://about.me/simon.hughes).");
                _preHeaderInfo.AppendLine("//");
                _preHeaderInfo.AppendLine(string.Format("// {0}{1}", licenseConstants.RegisteredTo, license.RegisteredTo));
                _preHeaderInfo.AppendLine(string.Format("// {0}{1}", licenseConstants.Company, license.Company));
                _preHeaderInfo.AppendLine(string.Format("// {0}{1}", licenseConstants.licenseType, license.GetlicenseType()));
                _preHeaderInfo.AppendLine(string.Format("// {0}{1}", licenseConstants.Numlicenses, license.Numlicenses));
                _preHeaderInfo.AppendLine(string.Format("// {0}{1}", licenseConstants.ValidUntil,
                    license.ValidUntil.ToString(licenseConstants.ExpiryFormat).ToUpperInvariant()));
                _preHeaderInfo.AppendLine("//");
            }

            if (Settings.IncludeConnectionSettingComments)
            {
                _preHeaderInfo.AppendLine("// The following connection settings were used to generate this file:");

                if (!string.IsNullOrEmpty(Settings.ConnectionStringName))
                    _preHeaderInfo.AppendLine(string.Format("//     Connection String Name: \"{0}\"", Settings.ConnectionStringName));

                _preHeaderInfo.AppendLine(string.Format("//     Connection String:      \"{0}\"", ZapPassword(Settings.ConnectionString)));

                if (!Settings.GenerateSingleDbContext)
                {
                    var conn = string.IsNullOrWhiteSpace(Settings.MultiContextSettingsConnectionString) ? Settings.ConnectionString : Settings.MultiContextSettingsConnectionString;
                    _preHeaderInfo.AppendLine(string.Format("//     Multi-context settings: \"{0}\"", ZapPassword(conn)));
                }
                _preHeaderInfo.AppendLine("//");
            }
        }

        private string ZapPassword(string conn)
        {
            var rx = new Regex("password=[^\";]*", RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return rx.Replace(conn, "password=**zapped**;");
        }
    }
}