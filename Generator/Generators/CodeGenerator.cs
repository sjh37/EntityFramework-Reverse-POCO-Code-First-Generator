using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Efrpg.FileManagement;
using Efrpg.Filtering;
using Efrpg.TemplateModels;
using Efrpg.Templates;

namespace Efrpg.Generators
{
    public class CodeGenerator
    {
        private   readonly Generator                               _generator;
        private   readonly IDbContextFilter                        _filter;
        private   readonly List<TableTemplateData>                 _tables;
        private   readonly List<StoredProcTemplateData>            _storedProcs;
        protected readonly List<string>                            GlobalUsings;
        protected readonly Template                                Template;
        private   readonly List<TableValuedFunctionsTemplateData>  _tableValuedFunctions;
        private   readonly List<ScalarValuedFunctionsTemplateData> _scalarValuedFunctions;
        private   readonly List<string>                            _tableValuedFunctionComplexTypes;

        private readonly bool _hasTables, _hasStoredProcs, _hasTableValuedFunctions, _hasScalarValuedFunctions, _hasTableValuedFunctionComplexTypes, _hasEnums;

        public CodeGenerator(Generator generator, IDbContextFilter filter)
        {
#pragma warning disable IDE0016 // Use 'throw' expression
            if (generator == null)   throw new ArgumentNullException(nameof(generator));
            if (filter == null)      throw new ArgumentNullException(nameof(filter));
#pragma warning restore IDE0016 // Use 'throw' expression

            var isEfCore  = Settings.GeneratorType == GeneratorType.EfCore;
            var isEfCore3 = Settings.IsEfCore3();

            _generator = generator;
            _filter    = filter;

            _tables = filter.Tables
                .Where(t => !t.IsMapping && (t.HasPrimaryKey || (t.IsView && isEfCore3)))
                .OrderBy(x => x.NameHumanCase)
                .Select(tbl => new TableTemplateData(tbl))
                .ToList();

            if (filter.IncludeStoredProcedures)
            {
                _storedProcs = filter.StoredProcs
                    .Where(s => s.IsStoredProcedure)
                    .OrderBy(x => x.NameHumanCase)
                    .Select(sp => new StoredProcTemplateData(
                        sp.ReturnModels.Count > 0,
                        sp.ReturnModels.Count == 1,
                        sp.ReturnModels.Count > 1,
                        sp.WriteStoredProcReturnType(_filter),
                        sp.WriteStoredProcReturnModelName(filter),
                        sp.WriteStoredProcFunctionName(filter),
                        sp.WriteStoredProcFunctionParams(false),
                        sp.WriteStoredProcFunctionParams(true),
                        sp.StoredProcHasOutParams() || sp.ReturnModels.Count == 0,
                        sp.WriteStoredProcFunctionOverloadCall(),
                        sp.WriteStoredProcFunctionSetSqlParameters(false),
                        sp.WriteStoredProcFunctionSetSqlParameters(true),
                        sp.ReturnModels.Count == 1
                            ? // exec
                            string.Format("EXEC @procResult = [{0}].[{1}] {2}", sp.Schema.DbName, sp.DbName, sp.WriteStoredProcFunctionSqlAtParams()).Trim()
                            : string.Format("[{0}].[{1}]", sp.Schema.DbName, sp.DbName),
                        sp.ReturnModels.Count == 1
                            ? // Async exec
                            string.Format("EXEC [{0}].[{1}] {2}", sp.Schema.DbName, sp.DbName, sp.WriteStoredProcFunctionSqlAtParams()).Trim()
                            : string.Format("[{0}].[{1}]", sp.Schema.DbName, sp.DbName),
                        sp.WriteStoredProcReturnModelName(_filter),
                        sp.WriteStoredProcFunctionSqlParameterAnonymousArray(true, true),
                        sp.WriteStoredProcFunctionSqlParameterAnonymousArray(false, true),
                        sp.WriteStoredProcFunctionDeclareSqlParameter(true),
                        sp.WriteStoredProcFunctionDeclareSqlParameter(false),
                        sp.Parameters.OrderBy(x => x.Ordinal).Select(sp.WriteStoredProcSqlParameterName).ToList(),
                        sp.ReturnModels.Count,
                        string.Format("EXEC @procResult = [{0}].[{1}] {2}", sp.Schema.DbName, sp.DbName, sp.WriteStoredProcFunctionSqlAtParams())
                    ))
                    .ToList();
            } else
                _storedProcs = new List<StoredProcTemplateData>();

            if (filter.IncludeTableValuedFunctions)
            {
                _tableValuedFunctions = filter.StoredProcs
                    .Where(s => s.IsTableValuedFunction)
                    .OrderBy(x => x.NameHumanCase)
                    .Select(tvf => new TableValuedFunctionsTemplateData(
                        tvf.ReturnModels.Count == 1 && tvf.ReturnModels[0].Count == 1,
                        tvf.ReturnModels.Count == 1 && tvf.ReturnModels[0].Count == 1 ? tvf.ReturnModels[0][0].ColumnName : null,
                        tvf.WriteStoredProcFunctionName(_filter),
                        tvf.WriteStoredProcReturnModelName(_filter),
                        tvf.WriteStoredProcFunctionParams(false),
                        tvf.DbName,
                        tvf.Schema.DbName,
                        isEfCore ? tvf.WriteStoredProcFunctionDeclareSqlParameter(false) : tvf.WriteTableValuedFunctionDeclareSqlParameter(),
                        isEfCore
                            ? tvf.WriteStoredProcFunctionSqlParameterAnonymousArray(false, false)
                            : tvf.WriteTableValuedFunctionSqlParameterAnonymousArray(),
                        isEfCore ? tvf.WriteNetCoreTableValuedFunctionsSqlAtParams() : tvf.WriteStoredProcFunctionSqlAtParams(),
                        isEfCore3 ? "FromSqlRaw" : "FromSql",
                        isEfCore3 ? "Set" : "Query",
                        isEfCore3 ? "Entity" : "Query",
                        isEfCore3 ? ".HasNoKey()" : string.Empty
                    ))
                    .ToList();

                _tableValuedFunctionComplexTypes = filter.StoredProcs
                    .Where(s => s.IsTableValuedFunction &&
                                !Settings.StoredProcedureReturnTypes.ContainsKey(s.NameHumanCase) &&
                                !Settings.StoredProcedureReturnTypes.ContainsKey(s.DbName))
                    .OrderBy(x => x.NameHumanCase)
                    .Select(x => x.WriteStoredProcReturnModelName(_filter))
                    .ToList();
            }
            else
            {
                _tableValuedFunctions = new List<TableValuedFunctionsTemplateData>();
                _tableValuedFunctionComplexTypes = new List<string>();
            }

            if (filter.IncludeScalarValuedFunctions)
            {
                _scalarValuedFunctions = filter.StoredProcs
                    .Where(s => s.IsScalarValuedFunction &&
                                s.Parameters.Any(x => x.Mode == StoredProcedureParameterMode.Out))
                    .OrderBy(x => x.NameHumanCase)
                    .Select(svf => new ScalarValuedFunctionsTemplateData(
                        svf.WriteStoredProcFunctionName(_filter),
                        svf.Parameters.Where(x => x.Mode == StoredProcedureParameterMode.Out).OrderBy(x => x.Ordinal).FirstOrDefault()?.PropertyType,
                        svf.WriteStoredProcFunctionParams(false),
                        svf.DbName,
                        svf.Schema.DbName
                    ))
                    .ToList();
            } else
                _scalarValuedFunctions = new List<ScalarValuedFunctionsTemplateData>();

            var returnModelsUsed = new List<string>();
            foreach(var sp in _storedProcs)
            {
                if(returnModelsUsed.Contains(sp.ReturnModelName))
                    sp.CreateDbSetForReturnModel = false;
                else
                    returnModelsUsed.Add(sp.ReturnModelName);
            }

            _hasTables                          = _tables.Any();
            _hasStoredProcs                     = _storedProcs.Any();
            _hasTableValuedFunctions            = _tableValuedFunctions.Any();
            _hasScalarValuedFunctions           = _scalarValuedFunctions.Any();
            _hasTableValuedFunctionComplexTypes = _tableValuedFunctionComplexTypes.Any();
            _hasEnums                           = filter.Enums.Any();

            GlobalUsings = new List<string>();
            Template = TemplateFactory.Create();
            CalcGlobalUsings();
        }

        protected void CalcGlobalUsings()
        {
            GlobalUsings.AddRange(Settings.AdditionalNamespaces.Where(x => !string.IsNullOrEmpty(x)).Distinct());

            if ((Settings.ElementsToGenerate.HasFlag(Elements.PocoConfiguration) ||
                 Settings.ElementsToGenerate.HasFlag(Elements.Context) ||
                 Settings.ElementsToGenerate.HasFlag(Elements.Interface)) &&
                (!Settings.ElementsToGenerate.HasFlag(Elements.Poco) && !string.IsNullOrWhiteSpace(Settings.PocoNamespace)))
                GlobalUsings.Add(Settings.PocoNamespace);

            if (Settings.ElementsToGenerate.HasFlag(Elements.PocoConfiguration) &&
                (!Settings.ElementsToGenerate.HasFlag(Elements.Context) && !string.IsNullOrWhiteSpace(Settings.ContextNamespace)))
                GlobalUsings.Add(Settings.ContextNamespace);

            if (Settings.ElementsToGenerate.HasFlag(Elements.Context) &&
                (!Settings.ElementsToGenerate.HasFlag(Elements.Interface) && !string.IsNullOrWhiteSpace(Settings.InterfaceNamespace)))
                GlobalUsings.Add(Settings.InterfaceNamespace);

            if (Settings.ElementsToGenerate.HasFlag(Elements.Context) &&
                (!Settings.ElementsToGenerate.HasFlag(Elements.PocoConfiguration) && !string.IsNullOrWhiteSpace(Settings.PocoConfigurationNamespace)))
                GlobalUsings.Add(Settings.PocoConfigurationNamespace);
        }

        public bool CanWriteInterface()
        {
            return Settings.ElementsToGenerate.HasFlag(Elements.Interface) &&
                   !string.IsNullOrWhiteSpace(Settings.DbContextInterfaceName) &&
                   (_hasTables || _hasStoredProcs || _hasTableValuedFunctions || _hasScalarValuedFunctions);
        }

        public bool CanWriteFactory()
        {
            return Settings.ElementsToGenerate.HasFlag(Elements.Context) &&
                   Settings.AddIDbContextFactory &&
                   (_hasTables || _hasStoredProcs || _hasTableValuedFunctions || _hasScalarValuedFunctions);
        }

        public bool CanWriteContext()
        {
            return Settings.ElementsToGenerate.HasFlag(Elements.Context) &&
                   (_hasTables || _hasStoredProcs || _hasTableValuedFunctions || _hasScalarValuedFunctions);
        }

        public bool CanWriteFakeContext()
        {
            return Settings.AddUnitTestingDbContext &&
                   Settings.ElementsToGenerate.HasFlag(Elements.Context) &&
                   (_hasTables || _hasStoredProcs || _hasTableValuedFunctions || _hasScalarValuedFunctions);
        }

        public bool CanWritePoco()
        {
            return Settings.ElementsToGenerate.HasFlag(Elements.Poco) && _hasTables;
        }

        public bool CanWritePocoConfiguration()
        {
            return Settings.ElementsToGenerate.HasFlag(Elements.PocoConfiguration) && _hasTables;
        }

        public bool CanWriteStoredProcReturnModel()
        {
            return Settings.ElementsToGenerate.HasFlag(Elements.Poco) && (_hasStoredProcs || _hasTableValuedFunctions);
        }

        public bool CanWriteEnums()
        {
            return Settings.ElementsToGenerate.HasFlag(Elements.Enum) && _hasEnums;
        }

        public string GenerateUsings(List<string> usings)
        {
            return !usings.Any() ? null : Template.Transform(Template.Usings(), usings);
        }

        public CodeOutput GenerateInterface()
        {
            var filename = Settings.DbContextInterfaceName + Settings.FileExtension;
            if (!CanWriteInterface())
            {
                FileManagementService.DeleteFile(filename);
                return null;
            }

            var data = new InterfaceModel
            {
                interfaceModifier               = Settings.DbContextInterfaceModifiers ?? "public partial",
                DbContextInterfaceName          = Settings.DbContextInterfaceName,
                DbContextInterfaceBaseClasses   = Settings.DbContextInterfaceBaseClasses,
                DbContextName                   = Settings.DbContextName,
                tables                          = _tables.Where(x => x.DbSetModifier == "public").ToList(),
                AdditionalContextInterfaceItems = Settings.AdditionalContextInterfaceItems.Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList(),
                addSaveChanges                  = !Settings.UseInheritedBaseInterfaceFunctions,
                storedProcs                     = _storedProcs,
                hasStoredProcs                  = _hasStoredProcs,
                tableValuedFunctions            = _tableValuedFunctions,
                scalarValuedFunctions           = _scalarValuedFunctions,
                hasTableValuedFunctions         = _hasTableValuedFunctions && _filter.IncludeTableValuedFunctions,
                hasScalarValuedFunctions        = _hasScalarValuedFunctions && _filter.IncludeScalarValuedFunctions
            };

            var co = new CodeOutput(filename, "Database context interface", GlobalUsings);
            co.AddUsings(Template.DatabaseContextInterfaceUsings(data));
            co.AddCode(Template.Transform(Template.DatabaseContextInterface(), data));

            return co;
        }

        public CodeOutput GenerateContext()
        {
            var filename = Settings.DbContextName + Settings.FileExtension;
            if (!CanWriteContext())
            {
                FileManagementService.DeleteFile(filename);
                return null;
            }

            var indexes = new List<string>();
            var hasSpatialTypes = false;
            foreach (var table in _tables)
            {
                var columns = table.Table.Columns
                    .Where(x => !x.Hidden && !string.IsNullOrEmpty(x.Config))
                    .OrderBy(x => x.Ordinal)
                    .ToList();

                if (!Settings.DisableGeographyTypes && !hasSpatialTypes)
                    hasSpatialTypes = columns.Any(x => x.IsSpatial);

                indexes.AddRange(columns
                    .Select(_generator.IndexModelBuilder)
                    .Where(x => !string.IsNullOrWhiteSpace(x)));
            }

            var isEfCore3 = Settings.IsEfCore3();

            var data = new ContextModel
            {
                DbContextClassModifiers                = Settings.DbContextClassModifiers,
                DbContextName                          = Settings.DbContextName,
                DbContextBaseClass                     = Settings.DbContextBaseClass,
                AddParameterlessConstructorToDbContext = Settings.AddParameterlessConstructorToDbContext,
                HasDefaultConstructorArgument          = !string.IsNullOrEmpty(Settings.DefaultConstructorArgument),
                DefaultConstructorArgument             = Settings.DefaultConstructorArgument,
                ConfigurationClassName                 = Settings.ConfigurationClassName,
                ConnectionString                       = Settings.ConnectionString,
                ConnectionStringName                   = Settings.ConnectionStringName,
                ConnectionStringActions                = hasSpatialTypes && Settings.TemplateType != TemplateType.Ef6 ? ", x => x.UseNetTopologySuite()" : "",
                contextInterface                       = string.IsNullOrWhiteSpace(Settings.DbContextInterfaceName) ? "" : ", " + Settings.DbContextInterfaceName,
                setInitializer                         = string.Format("<{0}>(null);", Settings.DbContextName),
                DbContextClassIsPartial                = Settings.DbContextClassIsPartial(),
                SqlCe                                  = Settings.DatabaseType == DatabaseType.SqlCe,
                tables                                 = _tables,
                hasTables                              = _hasTables,
                indexes                                = indexes,
                hasIndexes                             = indexes.Any(),
                storedProcs                            = _storedProcs,
                hasStoredProcs                         = _hasStoredProcs,
                tableValuedFunctionComplexTypes        = _tableValuedFunctionComplexTypes,
                hasTableValuedFunctionComplexTypes     = _hasTableValuedFunctionComplexTypes,
                AdditionalContextInterfaceItems        = Settings.AdditionalContextInterfaceItems.Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList(),
                addSaveChanges                         = !Settings.UseInheritedBaseInterfaceFunctions,
                tableValuedFunctions                   = _tableValuedFunctions,
                scalarValuedFunctions                  = _scalarValuedFunctions,
                hasTableValuedFunctions                = _hasTableValuedFunctions && _filter.IncludeTableValuedFunctions,
                hasScalarValuedFunctions               = _hasScalarValuedFunctions && _filter.IncludeScalarValuedFunctions,
                IncludeObjectContextConstructor        = !Settings.DbContextBaseClass.Contains("IdentityDbContext"),
                QueryString                            = isEfCore3 ? "Set"           : "Query",
                FromSql                                = isEfCore3 ? "FromSqlRaw"    : "FromSql",
                ExecuteSqlCommand                      = isEfCore3 ? "ExecuteSqlRaw" : "ExecuteSqlCommand",
                StoredProcModelBuilderCommand          = isEfCore3 ? "Entity"        : "Query",
                StoredProcModelBuilderPostCommand      = isEfCore3 ? ".HasNoKey()"   : string.Empty,
                OnConfigurationUsesConfiguration       = Settings.OnConfiguration == OnConfiguration.Configuration,
                OnConfigurationUsesConnectionString    = Settings.OnConfiguration == OnConfiguration.ConnectionString
            };

            var co = new CodeOutput(filename, "Database context", GlobalUsings);
            co.AddUsings(Template.DatabaseContextUsings(data));
            co.AddCode(Template.Transform(Template.DatabaseContext(), data));

            return co;
        }

        public CodeOutput GenerateFakeContext()
        {
            var filename = "Fake" + Settings.DbContextName + Settings.FileExtension;
            if (!CanWriteFakeContext())
            {
                FileManagementService.DeleteFile(filename);
                return null;
            }

            var data = new FakeContextModel
            {
                DbContextClassModifiers  = Settings.DbContextClassModifiers, DbContextName = Settings.DbContextName, DbContextBaseClass = Settings.DbContextBaseClass,
                contextInterface         = string.IsNullOrWhiteSpace(Settings.DbContextInterfaceName) ? "" : " : " + Settings.DbContextInterfaceName,
                DbContextClassIsPartial  = Settings.DbContextClassIsPartial(),
                tables                   = _tables,
                storedProcs              = _storedProcs,
                hasStoredProcs           = _hasStoredProcs,
                tableValuedFunctions     = _tableValuedFunctions,
                scalarValuedFunctions    = _scalarValuedFunctions,
                hasTableValuedFunctions  = _hasTableValuedFunctions && _filter.IncludeTableValuedFunctions,
                hasScalarValuedFunctions = _hasScalarValuedFunctions && _filter.IncludeScalarValuedFunctions
            };

            var co = new CodeOutput(filename, "Fake Database context", GlobalUsings);
            co.AddUsings(Template.FakeDatabaseContextUsings(data, _filter));
            co.AddCode(Template.Transform(Template.FakeDatabaseContext(), data));

            return co;
        }

        public CodeOutput GenerateFakeDbSet()
        {
            var filename = "FakeDbSet" + Settings.FileExtension;
            if (!CanWriteFakeContext())
            {
                FileManagementService.DeleteFile(filename);
                return null;
            }

            var data = new FakeDbSetModel
            {
                DbContextClassModifiers = Settings.DbContextClassModifiers,
                DbContextClassIsPartial = Settings.DbContextClassIsPartial(),
                IsEfCore2               = Settings.IsEfCore2(),
                IsEfCore3               = Settings.IsEfCore3()
            };

            var co = new CodeOutput(filename, "Fake DbSet", GlobalUsings);
            co.AddUsings(Template.FakeDbSetUsings(data));
            co.AddCode(Template.Transform(Template.FakeDbSet(), data));

            return co;
        }

        public CodeOutput GenerateFactory()
        {
            var filename = Settings.DbContextName + "Factory" + Settings.FileExtension;
            if (!CanWriteFactory())
            {
                FileManagementService.DeleteFile(filename);
                return null;
            }

            var data = new FactoryModel
            {
                classModifier = Settings.DbContextClassModifiers,
                contextName = Settings.DbContextName
            };

            var co = new CodeOutput(filename, "Database context factory", GlobalUsings);
            co.AddUsings(Template.DatabaseContextFactoryUsings(data));
            co.AddCode(Template.Transform(Template.DatabaseContextFactory(), data));
            return co;
        }

        public CodeOutput GeneratePoco(Table table)
        {
            var filename = table.NameHumanCaseWithSuffix() + Settings.FileExtension;
            if (!CanWritePoco())
            {
                FileManagementService.DeleteFile(filename);
                return null;
            }

            var isEfCore3 = Settings.IsEfCore3();

            var data = new PocoModel
            {
                UseHasNoKey             = isEfCore3 && table.IsView && !table.HasPrimaryKey,
                HasNoPrimaryKey         = !table.HasPrimaryKey,
                Name                    = table.DbName,
                NameHumanCaseWithSuffix = table.NameHumanCaseWithSuffix(),
                ClassModifier           = Settings.EntityClassesModifiers,
                ClassComment            = table.WriteComments(),
                ExtendedComments        = table.WriteExtendedComments(),
                ClassAttributes         = table.WriteClassAttributes(),
                BaseClasses             = table.BaseClasses,
                InsideClassBody         = table.WriteInsideClassBody(),
                Columns = table.Columns
                    .Where(x => !x.Hidden && !x.ExistsInBaseClass)
                    .OrderBy(x => x.Ordinal)
                    .Select((col, index) => new PocoColumnModel
                    {
                        AddNewLineBefore                = index > 0 && (((Settings.IncludeExtendedPropertyComments == CommentsStyle.InSummaryBlock || Settings.IncludeComments == CommentsStyle.InSummaryBlock) && !string.IsNullOrEmpty(col.SummaryComments)) || (col.Attributes != null && col.Attributes.Any())),
                        HasSummaryComments              = (Settings.IncludeExtendedPropertyComments == CommentsStyle.InSummaryBlock || Settings.IncludeComments == CommentsStyle.InSummaryBlock) && !string.IsNullOrEmpty(col.SummaryComments),
                        SummaryComments                 = !string.IsNullOrEmpty(col.SummaryComments) ? SecurityElement.Escape(col.SummaryComments) : null,
                        Attributes                      = col.Attributes,
                        OverrideModifier                = col.OverrideModifier,
                        WrapIfNullable                  = col.WrapIfNullable(),
                        NameHumanCase                   = col.NameHumanCase,
                        PrivateSetterForComputedColumns = Settings.UsePrivateSetterForComputedColumns && col.IsComputed ? "private " : string.Empty,
                        PropertyInitialisers            = Settings.UsePropertyInitialisers ? (string.IsNullOrWhiteSpace(col.Default) ? string.Empty : string.Format(" = {0};", col.Default)) : string.Empty,
                        InlineComments                  = col.InlineComments
                    })
                    .ToList(),
                HasReverseNavigation      = table.ReverseNavigationProperty.Count > 0,
                ReverseNavigationProperty = table.ReverseNavigationProperty
                    .OrderBy(x => x.Definition)
                    .Select(x => new PocoReverseNavigationPropertyModel
                    {
                        ReverseNavHasComment                        = Settings.IncludeComments != CommentsStyle.None && !string.IsNullOrEmpty(x.Comments),
                        ReverseNavComment                           = Settings.IncludeComments != CommentsStyle.None ? x.Comments : string.Empty,
                        AdditionalReverseNavigationsDataAnnotations = Settings.AdditionalReverseNavigationsDataAnnotations,
                        AdditionalDataAnnotations                   = x.AdditionalDataAnnotations,
                        Definition                                  = x.Definition
                    })
                    .ToList(),
                HasForeignKey          = table.HasForeignKey,
                ForeignKeyTitleComment = Settings.IncludeComments != CommentsStyle.None && table.Columns.SelectMany(x => x.EntityFk).Any() ? "    // Foreign keys" + Environment.NewLine : string.Empty,
                ForeignKeys            = table.Columns
                    .SelectMany(x => x.EntityFk)
                    .OrderBy(o => o.Definition)
                    .Select(x => new PocoForeignKeyModel
                    {
                        HasFkComment                         = Settings.IncludeComments != CommentsStyle.None && !string.IsNullOrEmpty(x.Comments),
                        FkComment                            = x.Comments,
                        AdditionalForeignKeysDataAnnotations = Settings.AdditionalForeignKeysDataAnnotations,
                        AdditionalDataAnnotations            = x.AdditionalDataAnnotations,
                        Definition                           = x.Definition
                    })
                    .ToList(),
                CreateConstructor = !Settings.UsePropertyInitialisers &&
                                    (
                                        table.Columns.Any(c => c.Default != string.Empty && !c.Hidden) ||
                                        table.ReverseNavigationCtor.Any() ||
                                        Settings.EntityClassesArePartial()
                                    ),
                ColumnsWithDefaults = table.Columns
                    .Where(c => c.Default != string.Empty && !c.Hidden)
                    .OrderBy(x => x.Ordinal)
                    .Select(x => new PocoColumnsWithDefaultsModel { NameHumanCase = x.NameHumanCase, Default = x.Default })
                    .ToList(),
                ReverseNavigationCtor   = table.ReverseNavigationCtor,
                EntityClassesArePartial = Settings.EntityClassesArePartial()
            };

            var co = new CodeOutput(filename, null, GlobalUsings);
            co.AddUsings(Template.PocoUsings(data));
            co.AddCode(Template.Transform(Template.Poco(), data));
            return co;
        }

        public CodeOutput GeneratePocoConfiguration(Table table)
        {
            var filename = table.NameHumanCaseWithSuffix() + Settings.ConfigurationClassName + Settings.FileExtension;
            if (!CanWritePocoConfiguration())
            {
                FileManagementService.DeleteFile(filename);
                return null;
            }

            var columns = table.Columns
                .Where(x => !x.Hidden && !string.IsNullOrEmpty(x.Config))
                .OrderBy(x => x.Ordinal)
                .ToList();

            var isEfCore3 = Settings.IsEfCore3();

            var foreignKeys = columns.SelectMany(x => x.ConfigFk).OrderBy(o => o).ToList();
            var primaryKey  = _generator.PrimaryKeyModelBuilder(table);

            var indexes    = _generator.IndexModelBuilder(table);
            var hasIndexes = indexes != null && indexes.Any();

            var data = new PocoConfigurationModel
            {
                UseHasNoKey               = isEfCore3 && table.IsView && !table.HasPrimaryKey,
                Name                      = table.DbName,
                ToTableOrView             = (isEfCore3 && table.IsView && !table.HasPrimaryKey) ? "ToView" : "ToTable",
                ConfigurationClassName    = table.NameHumanCaseWithSuffix() + Settings.ConfigurationClassName,
                NameHumanCaseWithSuffix   = table.NameHumanCaseWithSuffix(),
                Schema                    = table.Schema.DbName,
                PrimaryKeyNameHumanCase   = primaryKey ?? table.PrimaryKeyNameHumanCase(),
                HasSchema                 = !string.IsNullOrEmpty(table.Schema.DbName),
                ClassModifier             = Settings.ConfigurationClassesModifiers,
                ClassComment              = table.WriteComments(),
                Columns                   = columns.Select(x => x.Config).ToList(),
                HasReverseNavigation      = table.ReverseNavigationProperty.Count > 0,
                ReverseNavigationProperty = table.ReverseNavigationProperty
                    .OrderBy(x => x.Definition)
                    .Select(x => new PocoReverseNavigationPropertyModel
                    {
                        ReverseNavHasComment                        = Settings.IncludeComments != CommentsStyle.None && !string.IsNullOrEmpty(x.Comments),
                        ReverseNavComment                           = Settings.IncludeComments != CommentsStyle.None ? x.Comments : string.Empty,
                        AdditionalReverseNavigationsDataAnnotations = Settings.AdditionalReverseNavigationsDataAnnotations,
                        AdditionalDataAnnotations                   = x.AdditionalDataAnnotations,
                        Definition                                  = x.Definition
                    })
                    .ToList(),

                HasForeignKey                  = foreignKeys.Any(),
                ForeignKeys                    = foreignKeys,
                MappingConfiguration           = table.MappingConfiguration,
                ConfigurationClassesArePartial = Settings.ConfigurationClassesArePartial(),
                Indexes                        = indexes,
                HasIndexes                     = hasIndexes
            };

            var co = new CodeOutput(filename, null, GlobalUsings);
            co.AddUsings(Template.PocoConfigurationUsings(data));
            co.AddCode(Template.Transform(Template.PocoConfiguration(), data));
            return co;
        }

        public CodeOutput GenerateStoredProcReturnModel(StoredProcedure sp)
        {
            var filename = sp.WriteStoredProcReturnModelName(_filter) + Settings.FileExtension;
            if (!CanWriteStoredProcReturnModel())
            {
                FileManagementService.DeleteFile(filename);
                return null;
            }

            var multipleModelReturnColumns = new List<MultipleModelReturnColumns>();
            var model = 0;
            foreach (var returnModel in sp.ReturnModels)
            {
                multipleModelReturnColumns.Add(new MultipleModelReturnColumns(++model, returnModel.Select(sp.WriteStoredProcReturnColumn).ToList()));
            }

            var data = new StoredProcReturnModel
            {
                ResultClassModifiers           = Settings.ResultClassModifiers,
                WriteStoredProcReturnModelName = sp.WriteStoredProcReturnModelName(_filter),
                SingleModel                    = sp.ReturnModels.Count == 1,
                SingleModelReturnColumns       = sp.ReturnModels
                    .First()
                    .Select(sp.WriteStoredProcReturnColumn)
                    .ToList(),
                MultipleModelReturnColumns     = multipleModelReturnColumns
            };

            var co = new CodeOutput(filename, null, GlobalUsings);
            co.AddUsings(Template.StoredProcReturnModelUsings());
            co.AddCode(Template.Transform(Template.StoredProcReturnModels(), data));
            return co;
        }

        public CodeOutput GenerateEnum(Enumeration enumeration)
        {
            var filename = enumeration.EnumName + Settings.FileExtension;
            if (!CanWriteEnums())
            {
                FileManagementService.DeleteFile(filename);
                return null;
            }

            var co = new CodeOutput(filename, null, null);
            co.AddCode(Template.Transform(Template.Enums(), enumeration));

            return co;
        }
    }
}