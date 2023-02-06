using System.Collections.Generic;
using System.Linq;
using Efrpg.Filtering;
using Efrpg.TemplateModels;

namespace Efrpg.Templates
{
    /// <summary>
    /// {{Mustache}} template documentation available at https://github.com/jehugaleahsa/mustache-sharp
    /// </summary>
    public class TemplateEfCore5 : Template
    {
        public override string Usings()
        {
            return @"
{{#each this}}
using {{this}};{{#newline}}
{{/each}}";
        }

        public override List<string> DatabaseContextInterfaceUsings(InterfaceModel data)
        {
            var usings = new List<string>
            {
                "System",
                "System.Data",
                "System.Threading.Tasks",
                "System.Threading"
            };

            if (Settings.IncludeCodeGeneratedAttribute)
                usings.Add("System.CodeDom.Compiler");

            if (data.tables.Any() || data.hasStoredProcs)
            {
                usings.Add("Microsoft.EntityFrameworkCore");
                usings.Add("Microsoft.EntityFrameworkCore.Infrastructure");
                usings.Add("System.Linq");
            }

            if (data.hasStoredProcs)
                usings.Add("System.Collections.Generic");

            if (!Settings.UseInheritedBaseInterfaceFunctions)
            {
                usings.Add("System.Collections.Generic");
                usings.Add("Microsoft.EntityFrameworkCore.ChangeTracking");
                usings.Add("System.Linq");
                usings.Add("System.Linq.Expressions");
            }

            return usings;
        }

        public override string DatabaseContextInterface()
        {
            return @"
{{interfaceModifier}} interface {{DbContextInterfaceName}} : {{DbContextInterfaceBaseClasses}}{{#newline}}
{{{#newline}}

{{#each tables}}
    DbSet<{{DbSetName}}> {{PluralTableName}} { get; set; }{{Comment}}{{#newline}}
{{/each}}

{{#if AdditionalContextInterfaceItems}}
{{#newline}}
    // Additional interface items{{#newline}}
{{/if}}
{{#each AdditionalContextInterfaceItems}}
    {{this}}{{#newline}}
{{/each}}


{{#if addSaveChanges}}
{{#newline}}
    int SaveChanges();{{#newline}}
    int SaveChanges(bool acceptAllChangesOnSuccess);{{#newline}}
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));{{#newline}}
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));{{#newline}}
    DatabaseFacade Database { get; }{{#newline}}
    DbSet<TEntity> Set<TEntity>() where TEntity : class;{{#newline}}
    string ToString();{{#newline}}{{#newline}}

    EntityEntry Add(object entity);{{#newline}}
    EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;{{#newline}}
    Task AddRangeAsync(params object[] entities);{{#newline}}
    Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default);{{#newline}}
    ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;{{#newline}}
    ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default);{{#newline}}
    void AddRange(IEnumerable<object> entities);{{#newline}}
    void AddRange(params object[] entities);{{#newline}}{{#newline}}

    EntityEntry Attach(object entity);{{#newline}}
    EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;{{#newline}}
    void AttachRange(IEnumerable<object> entities);{{#newline}}
    void AttachRange(params object[] entities);{{#newline}}{{#newline}}

    EntityEntry Entry(object entity);{{#newline}}
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;{{#newline}}{{#newline}}

    TEntity Find<TEntity>(params object[] keyValues) where TEntity : class;{{#newline}}
    ValueTask<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : class;{{#newline}}
    ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class;{{#newline}}
    ValueTask<object> FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken);{{#newline}}
    ValueTask<object> FindAsync(Type entityType, params object[] keyValues);{{#newline}}
    object Find(Type entityType, params object[] keyValues);{{#newline}}{{#newline}}

    EntityEntry Remove(object entity);{{#newline}}
    EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;{{#newline}}
    void RemoveRange(IEnumerable<object> entities);{{#newline}}
    void RemoveRange(params object[] entities);{{#newline}}{{#newline}}

    EntityEntry Update(object entity);{{#newline}}
    EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;{{#newline}}
    void UpdateRange(IEnumerable<object> entities);{{#newline}}
    void UpdateRange(params object[] entities);{{#newline}}{{#newline}}

    IQueryable<TResult> FromExpression<TResult> (Expression<Func<IQueryable<TResult>>> expression);{{#newline}}
{{/if}}


{{#if hasStoredProcs}}
{{#newline}}
    // Stored Procedures{{#newline}}
{{#each storedProcs}}
{{#if HasReturnModels}}

{{#if MultipleReturnModels}}
    // {{ReturnType}} {{FunctionName}}({{WriteStoredProcFunctionParamsFalseTrue}}); Cannot be created as EF Core does not yet support stored procedures with multiple result sets.{{#newline}}
{{#else}}
    {{ReturnType}} {{FunctionName}}({{WriteStoredProcFunctionParamsFalseTrue}});{{#newline}}
{{/if}}
{{#if SingleReturnModel}}
    {{ReturnType}} {{FunctionName}}({{WriteStoredProcFunctionParamsTrueTrue}});{{#newline}}
{{/if}}
{{#else}}
    int {{FunctionName}}({{WriteStoredProcFunctionParamsTrue}});{{#newline}}
{{/if}}

{{#if AsyncFunctionCannotBeCreated}}
    // {{FunctionName}}Async() cannot be created due to having out parameters, or is relying on the procedure result ({{ReturnType}}){{#newline}}
{{#else}}
{{#if MultipleReturnModels}}
    // Task<{{ReturnType}}> {{FunctionName}}Async({{WriteStoredProcFunctionParamsFalseTrue}}); Cannot be created as EF Core does not yet support stored procedures with multiple result sets.{{#newline}}
{{#else}}
    Task<{{ReturnType}}> {{FunctionName}}Async({{WriteStoredProcFunctionParamsFalseTrue}});{{#newline}}
{{/if}}
{{/if}}
{{#newline}}
{{/each}}
{{/if}}

{{#if hasTableValuedFunctions}}
{{#newline}}
    // Table Valued Functions{{#newline}}
{{#each tableValuedFunctions}}
    IQueryable<{{ReturnClassName}}> {{ExecName}}({{WriteStoredProcFunctionParamsFalseTrue}}); // {{Schema}}.{{Name}}{{#newline}}
{{/each}}
{{/if}}

{{#if hasScalarValuedFunctions}}
{{#newline}}
    // Scalar Valued Functions{{#newline}}
{{#each scalarValuedFunctions}}
    {{ReturnType}} {{ExecName}}({{WriteStoredProcFunctionParamsFalseTrue}}); // {{Schema}}.{{Name}}{{#newline}}
{{/each}}
{{/if}}

}";
        }

        public override List<string> DatabaseContextUsings(ContextModel data)
        {
            var usings = new List<string>
            {
                "System",
                "System.Data",
                "System.Data.SqlTypes",
                "Microsoft.EntityFrameworkCore",
                "System.Threading.Tasks",
                "System.Threading"
            };

            switch (Settings.DatabaseType)
            {
                case DatabaseType.SqlServer:
                case DatabaseType.SqlCe:
                case DatabaseType.Plugin:
                    usings.Add("Microsoft.Data.SqlClient");
                    break;
                case DatabaseType.PostgreSQL:
                    break;
                case DatabaseType.MySql:
                    break;
                case DatabaseType.Oracle:
                    break;
            }

            if (Settings.IncludeCodeGeneratedAttribute)
                usings.Add("System.CodeDom.Compiler");

            if (data.tables.Any() || data.hasStoredProcs)
            {
                usings.Add("System.Linq");
            }

            if (data.hasStoredProcs)
                usings.Add("System.Collections.Generic");

            if(Settings.OnConfiguration == OnConfiguration.Configuration)
                usings.Add("Microsoft.Extensions.Configuration");

            if (!Settings.UseInheritedBaseInterfaceFunctions)
            {
                usings.Add("System.Collections.Generic");
                usings.Add("Microsoft.EntityFrameworkCore.ChangeTracking");
            }

            return usings;
        }

        public override string DatabaseContext()
        {
            return @"
{{DbContextClassModifiers}} class {{DbContextName}} : {{DbContextBaseClass}}{{contextInterface}}{{#newline}}
{{{#newline}}
{{#if OnConfigurationUsesConfiguration}}
    private readonly IConfiguration _configuration;{{#newline}}{{#newline}}
{{/if}}

{{#if AddParameterlessConstructorToDbContext}}
    public {{DbContextName}}(){{#newline}}
    {{{#newline}}
{{#if DbContextClassIsPartial}}
        InitializePartial();{{#newline}}
{{/if}}
    }{{#newline}}{{#newline}}
{{/if}}

    public {{DbContextName}}(DbContextOptions<{{DbContextName}}> options){{#newline}}
        : base(options){{#newline}}
    {{{#newline}}
{{#if DbContextClassIsPartial}}
        InitializePartial();{{#newline}}
{{/if}}
    }{{#newline}}{{#newline}}

{{#if OnConfigurationUsesConfiguration}}
    public {{DbContextName}}(IConfiguration configuration){{#newline}}
    {{{#newline}}
        _configuration = configuration;{{#newline}}
{{#if DbContextClassIsPartial}}
        InitializePartial();{{#newline}}
{{/if}}
    }{{#newline}}{{#newline}}
{{/if}}

{{#each tables}}
    {{DbSetModifier}} DbSet<{{DbSetName}}> {{PluralTableName}} { get; set; }{{Comment}}{{#newline}}
{{/each}}
{{#newline}}

{{#if OnConfigurationUsesConfiguration}}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){{#newline}}
    {{{#newline}}
        if (!optionsBuilder.IsConfigured && _configuration != null){{#newline}}
        {{{#newline}}
            optionsBuilder.{{UseDatabaseProvider}}(_configuration.GetConnectionString(@""{{ConnectionStringName}}""){{ConnectionStringActions}});{{#newline}}
{{#if UseLazyLoadingProxies}}
            optionsBuilder.UseLazyLoadingProxies();{{#newline}}
{{/if}}
        }{{#newline}}
    }{{#newline}}{{#newline}}
{{/if}}

{{#if OnConfigurationUsesConnectionString}}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){{#newline}}
    {{{#newline}}
        if (!optionsBuilder.IsConfigured){{#newline}}
        {{{#newline}}
            optionsBuilder.{{UseDatabaseProvider}}(@""{{ConnectionString}}""{{ConnectionStringActions}});{{#newline}}
{{#if UseLazyLoadingProxies}}
            optionsBuilder.UseLazyLoadingProxies();{{#newline}}
{{/if}}
        }{{#newline}}
    }{{#newline}}{{#newline}}
{{/if}}


    public bool IsSqlParameterNull({{SqlParameter}} param){{#newline}}
    {{{#newline}}
        var sqlValue = param.SqlValue;{{#newline}}
        var nullableValue = sqlValue as INullable;{{#newline}}
        if (nullableValue != null){{#newline}}
            return nullableValue.IsNull;{{#newline}}
        return (sqlValue == null || sqlValue == DBNull.Value);{{#newline}}
    }{{#newline}}{{#newline}}


    protected override void OnModelCreating(ModelBuilder modelBuilder){{#newline}}
    {{{#newline}}
        base.OnModelCreating(modelBuilder);{{#newline}}

{{#if hasSequences}}
{{#newline}}
{{#each Sequences}}
        modelBuilder.HasSequence<{{DataType}}>(""{{Name}}"", ""{{Schema}}"").StartsAt({{StartValue}}).IncrementsBy({{IncrementValue}}).IsCyclic({{IsCycleEnabled}})
{{#if hasMinValue}}
.HasMin({{MinValue}})
{{/if}}
{{#if hasMaxValue}}
.HasMax({{MaxValue}})
{{/if}}
;{{#newline}}
{{/each}}
{{/if}}

{{#if hasTables}}
{{#newline}}
{{#each tables}}
        modelBuilder.ApplyConfiguration(new {{DbSetConfigName}}());{{#newline}}
{{/each}}
{{/if}}

{{#if hasStoredProcs}}
{{#newline}}
{{#each storedProcs}}
{{#if SingleReturnModel}}
        modelBuilder.{{StoredProcModelBuilderCommand}}<{{ReturnModelName}}>(){{StoredProcModelBuilderPostCommand}};{{#newline}}
{{/if}}
{{/each}}
{{/if}}

{{#if hasTableValuedFunctions}}
{{#newline}}
        // Table Valued Functions{{#newline}}
{{#each tableValuedFunctions}}
{{#if IncludeModelBuilder}}
        modelBuilder.{{ModelBuilderCommand}}<{{ReturnClassName}}>(){{ModelBuilderPostCommand}};{{#newline}}
{{/if}}
{{/each}}
{{/if}}

{{#if DbContextClassIsPartial}}
{{#newline}}
        OnModelCreatingPartial(modelBuilder);{{#newline}}
{{/if}}

    }{{#newline}}
{{#newline}}



{{#if DbContextClassIsPartial}}
{{#newline}}
    partial void InitializePartial();{{#newline}}
    partial void DisposePartial(bool disposing);{{#newline}}
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);{{#newline}}
    static partial void OnCreateModelPartial(ModelBuilder modelBuilder, string schema);{{#newline}}
{{/if}}


{{#if hasStoredProcs}}
{{#newline}}
    // Stored Procedures{{#newline}}
{{#each storedProcs}}
{{#if HasReturnModels}}

{{#if MultipleReturnModels}}
    // public {{ReturnType}} {{FunctionName}}({{WriteStoredProcFunctionParamsFalseFalse}}) Cannot be created as EF Core does not yet support stored procedures with multiple result sets.{{#newline}}
{{#else}}
    public {{ReturnType}} {{FunctionName}}({{WriteStoredProcFunctionParamsFalseFalse}}){{#newline}}
    {{{#newline}}
        int procResult;{{#newline}}
        return {{FunctionName}}({{WriteStoredProcFunctionOverloadCall}});{{#newline}}
    }{{#newline}}{{#newline}}
{{/if}}

{{#if SingleReturnModel}}
    public {{ReturnType}} {{FunctionName}}({{WriteStoredProcFunctionParamsTrueFalse}}){{#newline}}
    {{{#newline}}
{{WriteStoredProcFunctionDeclareSqlParameterTrue}}
        const string sqlCommand = ""{{Exec}}"";{{#newline}}
        var procResultData = {{QueryString}}<{{ReturnModelName}}>(){{#newline}}
            .{{FromSql}}(sqlCommand{{WriteStoredProcFunctionSqlParameterAnonymousArrayTrue}}){{#newline}}
            .ToList();{{#newline}}{{#newline}}
{{WriteStoredProcFunctionSetSqlParametersFalse}}
        procResult = (int) procResultParam.Value;{{#newline}}
        return procResultData;{{#newline}}
    }{{#newline}}
{{/if}}

{{#else}}
    public int {{FunctionName}}({{WriteStoredProcFunctionParamsTrueFalse}}){{#newline}}
    {{{#newline}}
{{WriteStoredProcFunctionDeclareSqlParameterTrue}}{{#newline}}
        Database.{{ExecuteSqlCommand}}(""{{ExecWithNoReturnModel}}""{{WriteStoredProcFunctionSqlParameterAnonymousArrayTrue}});{{#newline}}
{{#newline}}
{{WriteStoredProcFunctionSetSqlParametersFalse}}
        return (int)procResultParam.Value;{{#newline}}
    }{{#newline}}
{{/if}}
{{#newline}}

{{#if AsyncFunctionCannotBeCreated}}
    // {{FunctionName}}Async() cannot be created due to having out parameters, or is relying on the procedure result ({{ReturnType}}){{#newline}}
{{#newline}}
{{#else}}
{{#if MultipleReturnModels}}
    // public async Task<{{ReturnType}}> {{FunctionName}}Async({{WriteStoredProcFunctionParamsFalseFalse}}) Cannot be created as EF Core does not yet support stored procedures with multiple result sets.{{#newline}}
{{#else}}
    public async Task<{{ReturnType}}> {{FunctionName}}Async({{WriteStoredProcFunctionParamsFalseFalse}}){{#newline}}
    {{{#newline}}
{{WriteStoredProcFunctionDeclareSqlParameterFalse}}
{{WriteStoredProcFunctionSetSqlParametersFalse}}
        const string sqlCommand = ""{{AsyncExec}}"";{{#newline}}
        var procResultData = await {{QueryString}}<{{ReturnModelName}}>(){{#newline}}
            .{{FromSql}}(sqlCommand{{WriteStoredProcFunctionSqlParameterAnonymousArrayFalse}}){{#newline}}
            .ToListAsync();{{#newline}}{{#newline}}

        return procResultData;{{#newline}}
    }{{#newline}}
{{/if}}
{{#newline}}
{{/if}}
{{/each}}
{{/if}}

{{#if hasTableValuedFunctions}}
{{#newline}}
    // Table Valued Functions{{#newline}}
{{#each tableValuedFunctions}}
{{#newline}}
    // {{Schema}}.{{Name}}{{#newline}}
    public IQueryable<{{ReturnClassName}}> {{ExecName}}({{WriteStoredProcFunctionParamsFalseFalse}}){{#newline}}
    {{{#newline}}
        return {{QueryString}}<{{ReturnClassName}}>(){{#newline}}
            .{{FromSql}}(""SELECT * FROM [{{Schema}}].[{{Name}}]({{WriteStoredProcFunctionSqlAtParams}})""{{WriteTableValuedFunctionSqlParameterAnonymousArray}}){{#newline}}
            .AsNoTracking();{{#newline}}
    }{{#newline}}
{{/each}}
{{/if}}

{{#if hasScalarValuedFunctions}}
{{#newline}}
    // Scalar Valued Functions{{#newline}}
{{#each scalarValuedFunctions}}
{{#newline}}
    [DbFunction(""{{Name}}"", ""{{Schema}}"")]{{#newline}}
    public {{ReturnType}} {{ExecName}}({{WriteStoredProcFunctionParamsFalseFalse}}){{#newline}}
    {{{#newline}}
        throw new Exception(""Don't call this directly. Use LINQ to call the scalar valued function as part of your query"");{{#newline}}
    }{{#newline}}
{{/each}}
{{/if}}
}";
        }

        public override List<string> DatabaseContextFactoryUsings(FactoryModel data)
        {
            var usings = new List<string>
            {
                "Microsoft.EntityFrameworkCore.Design"
            };
            if (Settings.IncludeCodeGeneratedAttribute)
                usings.Add("System.CodeDom.Compiler");
            return usings;
        }

        public override string DatabaseContextFactory()
        {
            return @"
{{classModifier}} class {{contextName}}Factory : IDesignTimeDbContextFactory<{{contextName}}>{{#newline}}
{{{#newline}}
    public {{contextName}} CreateDbContext(string[] args){{#newline}}
    {{{#newline}}
        return new {{contextName}}();{{#newline}}
    }{{#newline}}
}";
        }

        public override List<string> FakeDatabaseContextUsings(FakeContextModel data, IDbContextFilter filter)
        {
            var usings = new List<string>
            {
                "System",
                "System.Data",
                "System.Threading.Tasks",
                "System.Threading",
                "Microsoft.EntityFrameworkCore.Infrastructure"
            };

            if (Settings.IncludeCodeGeneratedAttribute)
                usings.Add("System.CodeDom.Compiler");

            if (data.tables.Any() || data.hasStoredProcs)
            {
                usings.Add("System.Linq");
                usings.Add("Microsoft.EntityFrameworkCore");
            }

            if (data.hasStoredProcs)
                usings.Add("System.Collections.Generic");

            if (!Settings.UseInheritedBaseInterfaceFunctions)
            {
                usings.Add("System.Collections.Generic");
                usings.Add("Microsoft.EntityFrameworkCore.ChangeTracking");
                usings.Add("System.Linq");
                usings.Add("System.Linq.Expressions");
            }

            if (Settings.DatabaseType == DatabaseType.SqlCe)
            {
                usings.Add("Microsoft.Data.SqlClient");
                //usings.Add("System.DBNull");
                usings.Add("System.Data.SqlTypes");
            }

            return usings;
        }

        public override string FakeDatabaseContext()
        {
            return @"
{{DbContextClassModifiers}} class Fake{{DbContextName}}{{contextInterface}}{{#newline}}
{{{#newline}}

{{#each tables}}
    {{DbSetModifier}} DbSet<{{DbSetName}}> {{PluralTableName}} { get; set; }{{Comment}}{{#newline}}
{{/each}}
{{#newline}}

    public Fake{{DbContextName}}(){{#newline}}
    {{{#newline}}
        _database = new FakeDatabaseFacade(new {{DbContextName}}());{{#newline}}
{{#newline}}

{{#each tables}}
        {{PluralTableName}} = new FakeDbSet<{{DbSetName}}>({{DbSetPrimaryKeys}});{{#newline}}
{{/each}}
{{#newline}}

{{#if DbContextClassIsPartial}}
        InitializePartial();{{#newline}}
{{/if}}
    }{{#newline}}

{{#newline}}
    public int SaveChangesCount { get; private set; }{{#newline}}
    public virtual int SaveChanges(){{#newline}}
    {{{#newline}}
        ++SaveChangesCount;{{#newline}}
        return 1;{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual int SaveChanges(bool acceptAllChangesOnSuccess){{#newline}}
    {{{#newline}}
        return SaveChanges();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken){{#newline}}
    {{{#newline}}
        ++SaveChangesCount;{{#newline}}
        return Task<int>.Factory.StartNew(() => 1, cancellationToken);{{#newline}}
    }{{#newline}}

    public virtual Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken){{#newline}}
    {{{#newline}}
        ++SaveChangesCount;{{#newline}}
        return Task<int>.Factory.StartNew(x => 1, acceptAllChangesOnSuccess, cancellationToken);{{#newline}}
    }{{#newline}}{{#newline}}


{{#if DbContextClassIsPartial}}
    partial void InitializePartial();{{#newline}}
{{#newline}}
{{/if}}

    protected virtual void Dispose(bool disposing){{#newline}}
    {{{#newline}}
    }{{#newline}}{{#newline}}

    public void Dispose(){{#newline}}
    {{{#newline}}
        Dispose(true);{{#newline}}
    }{{#newline}}{{#newline}}

    private DatabaseFacade _database;{{#newline}}
    public DatabaseFacade Database { get { return _database; } }{{#newline}}{{#newline}}

    public DbSet<TEntity> Set<TEntity>() where TEntity : class{{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public override string ToString(){{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual EntityEntry Add(object entity){{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class{{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual Task AddRangeAsync(params object[] entities){{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual async Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default){{#newline}}
    {{{#newline}}
        await Task.CompletedTask;{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual async ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class{{#newline}}
    {{{#newline}}
        await Task.CompletedTask;{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual async ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default){{#newline}}
    {{{#newline}}
        await Task.CompletedTask;{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual void AddRange(IEnumerable<object> entities){{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual void AddRange(params object[] entities){{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual EntityEntry Attach(object entity){{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class{{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual void AttachRange(IEnumerable<object> entities){{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual void AttachRange(params object[] entities){{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual EntityEntry Entry(object entity){{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class{{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual TEntity Find<TEntity>(params object[] keyValues) where TEntity : class{{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual ValueTask<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : class{{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class{{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual ValueTask<object> FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken){{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual ValueTask<object> FindAsync(Type entityType, params object[] keyValues){{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual object Find(Type entityType, params object[] keyValues){{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual EntityEntry Remove(object entity){{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class{{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual void RemoveRange(IEnumerable<object> entities){{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual void RemoveRange(params object[] entities){{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual EntityEntry Update(object entity){{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class{{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual void UpdateRange(IEnumerable<object> entities){{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual void UpdateRange(params object[] entities){{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

    public virtual IQueryable<TResult> FromExpression<TResult> (Expression<Func<IQueryable<TResult>>> expression){{#newline}}
    {{{#newline}}
        throw new NotImplementedException();{{#newline}}
    }{{#newline}}{{#newline}}

{{#if hasStoredProcs}}
{{#newline}}
    // Stored Procedures{{#newline}}
{{#each storedProcs}}

{{#if HasReturnModels}}
{{#newline}}
{{#if CreateDbSetForReturnModel}}
    public DbSet<{{ReturnModelName}}> {{ReturnModelName}} { get; set; }{{#newline}}
{{/if}}
    public {{ReturnType}} {{FunctionName}}({{WriteStoredProcFunctionParamsFalseFalse}}){{#newline}}
    {{{#newline}}
        int procResult;{{#newline}}
        return {{FunctionName}}({{WriteStoredProcFunctionOverloadCall}});{{#newline}}
    }{{#newline}}{{#newline}}

    public {{ReturnType}} {{FunctionName}}({{WriteStoredProcFunctionParamsTrueFalse}}){{#newline}}
    {{{#newline}}
{{WriteStoredProcFunctionSetSqlParametersTrue}}
        procResult = 0;{{#newline}}
        return new {{ReturnType}}();{{#newline}}
    }{{#newline}}

{{#newline}}
{{#if AsyncFunctionCannotBeCreated}}
    // {{FunctionName}}Async() cannot be created due to having out parameters, or is relying on the procedure result ({{ReturnType}}){{#newline}}
{{#newline}}
{{#else}}
    public Task<{{ReturnType}}> {{FunctionName}}Async({{WriteStoredProcFunctionParamsFalseFalse}}){{#newline}}
    {{{#newline}}
        int procResult;{{#newline}}
        return Task.FromResult({{FunctionName}}({{WriteStoredProcFunctionOverloadCall}}));{{#newline}}
    }{{#newline}}
{{/if}}

{{#else}}
{{#newline}}
    public int {{FunctionName}}({{WriteStoredProcFunctionParamsTrueFalse}}){{#newline}}
    {{{#newline}}
{{WriteStoredProcFunctionSetSqlParametersTrue}}
        return 0;{{#newline}}
    }{{#newline}}
{{#newline}}
{{#if AsyncFunctionCannotBeCreated}}
    // {{FunctionName}}Async() cannot be created due to having out parameters, or is relying on the procedure result ({{ReturnType}}){{#newline}}
{{#else}}
    public Task<int> {{FunctionName}}Async({{WriteStoredProcFunctionParamsFalseFalse}}){{#newline}}
    {{{#newline}}
{{WriteStoredProcFunctionSetSqlParametersTrue}}
        return Task.FromResult(0);{{#newline}}
    }{{#newline}}
{{/if}}
{{/if}}
{{/each}}
{{/if}}



{{#if hasTableValuedFunctions}}
{{#newline}}
    // Table Valued Functions{{#newline}}
{{#each tableValuedFunctions}}
{{#newline}}
    // {{Schema}}.{{Name}}{{#newline}}
    public IQueryable<{{ReturnClassName}}> {{ExecName}}({{WriteStoredProcFunctionParamsFalseFalse}}){{#newline}}
    {{{#newline}}
        return new List<{{ReturnClassName}}>().AsQueryable();{{#newline}}
    }{{#newline}}
{{/each}}
{{/if}}

{{#if hasScalarValuedFunctions}}
{{#newline}}
    // Scalar Valued Functions{{#newline}}
{{#each scalarValuedFunctions}}
{{#newline}}
    // {{Schema}}.{{Name}}{{#newline}}
    public {{ReturnType}} {{ExecName}}({{WriteStoredProcFunctionParamsFalseFalse}}){{#newline}}
    {{{#newline}}
        return default({{ReturnType}});{{#newline}}
    }{{#newline}}
{{/each}}
{{/if}}
}";
        }

        public override List<string> FakeDbSetUsings(FakeDbSetModel data)
        {
            var usings = new List<string>
            {
                "System",
                "System.Collections",
                "System.ComponentModel",
                "System.Linq",
                "System.Linq.Expressions",
                "System.Reflection",
                "System.Collections.ObjectModel",
                "System.Collections.Generic",
                "System.Threading",
                "System.Threading.Tasks",
                "Microsoft.EntityFrameworkCore",
                "Microsoft.EntityFrameworkCore.Query",
                "Microsoft.EntityFrameworkCore.Query.Internal",
                "Microsoft.EntityFrameworkCore.Infrastructure",
                "Microsoft.EntityFrameworkCore.ChangeTracking",
                "Microsoft.EntityFrameworkCore.Storage",
                "Microsoft.EntityFrameworkCore.Metadata"
            };

            if (Settings.IncludeCodeGeneratedAttribute)
                usings.Add("System.CodeDom.Compiler");

            return usings;
        }

        public override string FakeDbSet()
        {
            return @"
// ************************************************************************{{#newline}}
// Fake DbSet{{#newline}}
// Implementing Find:{{#newline}}
//      The Find method is difficult to implement in a generic fashion. If{{#newline}}
//      you need to test code that makes use of the Find method it is{{#newline}}
//      easiest to create a test DbSet for each of the entity types that{{#newline}}
//      need to support find. You can then write logic to find that{{#newline}}
//      particular type of entity, as shown below:{{#newline}}
//      public class FakeBlogDbSet : FakeDbSet<Blog>{{#newline}}
//      {{{#newline}}
//          public override Blog Find(params object[] keyValues){{#newline}}
//          {{{#newline}}
//              var id = (int) keyValues.Single();{{#newline}}
//              return this.SingleOrDefault(b => b.BlogId == id);{{#newline}}
//          }{{#newline}}
//      }{{#newline}}
//      Read more about it here: https://msdn.microsoft.com/en-us/data/dn314431.aspx{{#newline}}
{{DbContextClassModifiers}} class FakeDbSet<TEntity> :{{#newline}}
    DbSet<TEntity>,{{#newline}}
    IQueryable<TEntity>,{{#newline}}
    IAsyncEnumerable<TEntity>,{{#newline}}
    IListSource,{{#newline}}
    IResettableService{{#newline}}
    where TEntity : class{{#newline}}
{{{#newline}}
    private readonly PropertyInfo[] _primaryKeys;{{#newline}}
    private ObservableCollection<TEntity> _data;{{#newline}}
    private IQueryable _query;{{#newline}}
    public override IEntityType EntityType { get; }{{#newline}}{{#newline}}

    public FakeDbSet(){{#newline}}
    {{{#newline}}
        _primaryKeys = null;{{#newline}}
        _data        = new ObservableCollection<TEntity>();{{#newline}}
        _query       = _data.AsQueryable();{{#newline}}

{{#if DbContextClassIsPartial}}
        InitializePartial();{{#newline}}
{{/if}}
    }{{#newline}}{{#newline}}

    public FakeDbSet(params string[] primaryKeys){{#newline}}
    {{{#newline}}
        _primaryKeys = typeof(TEntity).GetProperties().Where(x => primaryKeys.Contains(x.Name)).ToArray();{{#newline}}
        _data        = new ObservableCollection<TEntity>();{{#newline}}
        _query       = _data.AsQueryable();{{#newline}}
{{#if DbContextClassIsPartial}}
        InitializePartial();{{#newline}}
{{/if}}
    }{{#newline}}{{#newline}}

    public override TEntity Find(params object[] keyValues){{#newline}}
    {{{#newline}}
        if (_primaryKeys == null){{#newline}}
            throw new ArgumentException(""No primary keys defined"");{{#newline}}
        if (keyValues.Length != _primaryKeys.Length){{#newline}}
            throw new ArgumentException(""Incorrect number of keys passed to Find method"");{{#newline}}{{#newline}}

        var keyQuery = this.AsQueryable();{{#newline}}
        keyQuery = keyValues{{#newline}}
            .Select((t, i) => i){{#newline}}
            .Aggregate(keyQuery,{{#newline}}
                (current, x) =>{{#newline}}
                    current.Where(entity => _primaryKeys[x].GetValue(entity, null).Equals(keyValues[x])));{{#newline}}{{#newline}}

        return keyQuery.SingleOrDefault();{{#newline}}
    }{{#newline}}{{#newline}}

    public override ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken){{#newline}}
    {{{#newline}}
        return new ValueTask<TEntity>(Task<TEntity>.Factory.StartNew(() => Find(keyValues), cancellationToken));{{#newline}}
    }{{#newline}}{{#newline}}

    public override ValueTask<TEntity> FindAsync(params object[] keyValues){{#newline}}
    {{{#newline}}
        return new ValueTask<TEntity>(Task<TEntity>.Factory.StartNew(() => Find(keyValues)));{{#newline}}
    }{{#newline}}{{#newline}}

    IAsyncEnumerator<TEntity> IAsyncEnumerable<TEntity>.GetAsyncEnumerator(CancellationToken cancellationToken){{#newline}}
    {{{#newline}}
        return GetAsyncEnumerator(cancellationToken);{{#newline}}
    }{{#newline}}{{#newline}}

    public override EntityEntry<TEntity> Add(TEntity entity){{#newline}}
    {{{#newline}}
        _data.Add(entity);{{#newline}}
        return null;{{#newline}}
    }{{#newline}}{{#newline}}

    public override ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default){{#newline}}
    {{{#newline}}
        return new ValueTask<EntityEntry<TEntity>>(Task<EntityEntry<TEntity>>.Factory.StartNew(() => Add(entity), cancellationToken));{{#newline}}
    }{{#newline}}{{#newline}}

    public override void AddRange(params TEntity[] entities){{#newline}}
    {{{#newline}}
        if (entities == null) throw new ArgumentNullException(""entities"");{{#newline}}
        foreach (var entity in entities){{#newline}}
            _data.Add(entity);{{#newline}}
    }{{#newline}}{{#newline}}

    public override void AddRange(IEnumerable<TEntity> entities){{#newline}}
    {{{#newline}}
        if (entities == null) throw new ArgumentNullException(""entities"");{{#newline}}
        foreach (var entity in entities){{#newline}}
            _data.Add(entity);{{#newline}}
    }{{#newline}}{{#newline}}

    public override Task AddRangeAsync(params TEntity[] entities){{#newline}}
    {{{#newline}}
        if (entities == null) throw new ArgumentNullException(""entities"");{{#newline}}
        return Task.Factory.StartNew(() => AddRange(entities));{{#newline}}
    }{{#newline}}{{#newline}}

    public override Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default){{#newline}}
    {{{#newline}}
        if (entities == null) throw new ArgumentNullException(""entities"");{{#newline}}
        return Task.Factory.StartNew(() => AddRange(entities), cancellationToken);{{#newline}}
    }{{#newline}}{{#newline}}

    public override EntityEntry<TEntity> Attach(TEntity entity){{#newline}}
    {{{#newline}}
        if (entity == null) throw new ArgumentNullException(""entity"");{{#newline}}
        return Add(entity);{{#newline}}
    }{{#newline}}{{#newline}}

    public override void AttachRange(params TEntity[] entities){{#newline}}
    {{{#newline}}
        if (entities == null) throw new ArgumentNullException(""entities"");{{#newline}}
        AddRange(entities);{{#newline}}
    }{{#newline}}{{#newline}}

    public override void AttachRange(IEnumerable<TEntity> entities){{#newline}}
    {{{#newline}}
        if (entities == null) throw new ArgumentNullException(""entities"");{{#newline}}
        AddRange(entities);{{#newline}}
    }{{#newline}}{{#newline}}

    public override EntityEntry<TEntity> Remove(TEntity entity){{#newline}}
    {{{#newline}}
        _data.Remove(entity);{{#newline}}
        return null;{{#newline}}
    }{{#newline}}{{#newline}}

    public override void RemoveRange(params TEntity[] entities){{#newline}}
    {{{#newline}}
        if (entities == null) throw new ArgumentNullException(""entities"");{{#newline}}
        foreach (var entity in entities.ToList()){{#newline}}
            _data.Remove(entity);{{#newline}}
    }{{#newline}}{{#newline}}

    public override void RemoveRange(IEnumerable<TEntity> entities){{#newline}}
    {{{#newline}}
        RemoveRange(entities.ToArray());{{#newline}}
    }{{#newline}}{{#newline}}

    public override EntityEntry<TEntity> Update(TEntity entity){{#newline}}
    {{{#newline}}
        _data.Remove(entity);{{#newline}}
        _data.Add(entity);{{#newline}}
        return null;{{#newline}}
    }{{#newline}}{{#newline}}

    public override void UpdateRange(params TEntity[] entities){{#newline}}
    {{{#newline}}
        if (entities == null) throw new ArgumentNullException(""entities"");{{#newline}}
        RemoveRange(entities);{{#newline}}
        AddRange(entities);{{#newline}}
    }{{#newline}}{{#newline}}

    public override void UpdateRange(IEnumerable<TEntity> entities){{#newline}}
    {{{#newline}}
        if (entities == null) throw new ArgumentNullException(""entities"");{{#newline}}
        var array = entities.ToArray();
        RemoveRange(array);{{#newline}}
        AddRange(array);{{#newline}}
    }{{#newline}}{{#newline}}

    bool IListSource.ContainsListCollection => true;{{#newline}}{{#newline}}

    public IList GetList(){{#newline}}
    {{{#newline}}
        return _data;{{#newline}}
    }{{#newline}}{{#newline}}

    IList IListSource.GetList(){{#newline}}
    {{{#newline}}
        return _data;{{#newline}}
    }{{#newline}}{{#newline}}

    Type IQueryable.ElementType{{#newline}}
    {{{#newline}}
        get { return _query.ElementType; }{{#newline}}
    }{{#newline}}{{#newline}}

    Expression IQueryable.Expression{{#newline}}
    {{{#newline}}
        get { return _query.Expression; }{{#newline}}
    }{{#newline}}{{#newline}}

    IQueryProvider IQueryable.Provider{{#newline}}
    {{{#newline}}
        get { return new FakeDbAsyncQueryProvider<TEntity>(_data); }{{#newline}}
    }{{#newline}}{{#newline}}

    IEnumerator IEnumerable.GetEnumerator(){{#newline}}
    {{{#newline}}
        return _data.GetEnumerator();{{#newline}}
    }{{#newline}}{{#newline}}

    IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator(){{#newline}}
    {{{#newline}}
        return _data.GetEnumerator();{{#newline}}
    }{{#newline}}{{#newline}}

    IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken)){{#newline}}
    {{{#newline}}
        return new FakeDbAsyncEnumerator<TEntity>(this.AsEnumerable().GetEnumerator());{{#newline}}
    }{{#newline}}{{#newline}}

    public void ResetState(){{#newline}}
    {{{#newline}}
        _data  = new ObservableCollection<TEntity>();{{#newline}}
        _query = _data.AsQueryable();{{#newline}}
    }{{#newline}}{{#newline}}

    public Task ResetStateAsync(CancellationToken cancellationToken = new CancellationToken()){{#newline}}
    {{{#newline}}
        return Task.Factory.StartNew(() => ResetState());{{#newline}}
    }{{#newline}}

{{#if DbContextClassIsPartial}}
{{#newline}}
    partial void InitializePartial();{{#newline}}
{{/if}}
}{{#newline}}{{#newline}}


{{DbContextClassModifiers}} class FakeDbAsyncQueryProvider<TEntity> : FakeQueryProvider<TEntity>, IAsyncEnumerable<TEntity>, IAsyncQueryProvider{{#newline}}
{{{#newline}}
    public FakeDbAsyncQueryProvider(Expression expression) : base(expression){{#newline}}
    {{{#newline}}
    }{{#newline}}{{#newline}}

    public FakeDbAsyncQueryProvider(IEnumerable<TEntity> enumerable) : base(enumerable){{#newline}}
    {{{#newline}}
    }{{#newline}}{{#newline}}

    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken){{#newline}}
    {{{#newline}}
        var expectedResultType = typeof(TResult).GetGenericArguments()[0];{{#newline}}
        var executionResult = typeof(IQueryProvider){{#newline}}
            .GetMethods(){{#newline}}
            .First(method => method.Name == nameof(IQueryProvider.Execute) && method.IsGenericMethod){{#newline}}
            .MakeGenericMethod(expectedResultType){{#newline}}
            .Invoke(this, new object[] { expression });{{#newline}}{{#newline}}

        return (TResult) typeof(Task).GetMethod(nameof(Task.FromResult)){{#newline}}
            ?.MakeGenericMethod(expectedResultType){{#newline}}
            .Invoke(null, new[] { executionResult });{{#newline}}
    }{{#newline}}{{#newline}}

    public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default){{#newline}}
    {{{#newline}}
        return new FakeDbAsyncEnumerator<TEntity>(this.AsEnumerable().GetEnumerator());{{#newline}}
    }{{#newline}}
}{{#newline}}{{#newline}}


{{DbContextClassModifiers}} class FakeDbAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>{{#newline}}
{{{#newline}}
    public FakeDbAsyncEnumerable(IEnumerable<T> enumerable){{#newline}}
        : base(enumerable){{#newline}}
    {{{#newline}}
    }{{#newline}}{{#newline}}

    public FakeDbAsyncEnumerable(Expression expression){{#newline}}
        : base(expression){{#newline}}
    {{{#newline}}
    }{{#newline}}{{#newline}}

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken()){{#newline}}
    {{{#newline}}
        return new FakeDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());{{#newline}}
    }{{#newline}}{{#newline}}

    IAsyncEnumerator<T> IAsyncEnumerable<T>.GetAsyncEnumerator(CancellationToken cancellationToken){{#newline}}
    {{{#newline}}
        return GetAsyncEnumerator(cancellationToken);{{#newline}}
    }{{#newline}}{{#newline}}

    IEnumerator IEnumerable.GetEnumerator(){{#newline}}
    {{{#newline}}
        return this.AsEnumerable().GetEnumerator();{{#newline}}
    }{{#newline}}
}{{#newline}}{{#newline}}


{{DbContextClassModifiers}} class FakeDbAsyncEnumerator<T> : IAsyncEnumerator<T>{{#newline}}
{{{#newline}}
    private readonly IEnumerator<T> _inner;{{#newline}}{{#newline}}

    public FakeDbAsyncEnumerator(IEnumerator<T> inner){{#newline}}
    {{{#newline}}
        _inner = inner;{{#newline}}
    }{{#newline}}{{#newline}}

    public T Current{{#newline}}
    {{{#newline}}
        get { return _inner.Current; }{{#newline}}
    }{{#newline}}{{#newline}}

    public ValueTask<bool> MoveNextAsync(){{#newline}}
    {{{#newline}}
        return new ValueTask<bool>(_inner.MoveNext());{{#newline}}
    }{{#newline}}{{#newline}}

    public ValueTask DisposeAsync(){{#newline}}
    {{{#newline}}
        _inner.Dispose();{{#newline}}
        return new ValueTask(Task.CompletedTask);{{#newline}}
    }{{#newline}}
}{{#newline}}{{#newline}}


public abstract class FakeQueryProvider<T> : IOrderedQueryable<T>, IQueryProvider{{#newline}}
{{{#newline}}
    private IEnumerable<T> _enumerable;{{#newline}}{{#newline}}

    protected FakeQueryProvider(Expression expression){{#newline}}
    {{{#newline}}
        Expression = expression;{{#newline}}
    }{{#newline}}{{#newline}}

    protected FakeQueryProvider(IEnumerable<T> enumerable){{#newline}}
    {{{#newline}}
        _enumerable = enumerable;{{#newline}}
        Expression = enumerable.AsQueryable().Expression;{{#newline}}
    }{{#newline}}{{#newline}}

    public IQueryable CreateQuery(Expression expression){{#newline}}
    {{{#newline}}
        if (expression is MethodCallExpression m){{#newline}}
        {{{#newline}}
            var resultType = m.Method.ReturnType; // it should be IQueryable<T>{{#newline}}
            var tElement = resultType.GetGenericArguments().First();{{#newline}}
            return (IQueryable) CreateInstance(tElement, expression);{{#newline}}
        }{{#newline}}{{#newline}}

        return CreateQuery<T>(expression);{{#newline}}
    }{{#newline}}{{#newline}}

    public IQueryable<TEntity> CreateQuery<TEntity>(Expression expression){{#newline}}
    {{{#newline}}
        return (IQueryable<TEntity>) CreateInstance(typeof(TEntity), expression);{{#newline}}
    }{{#newline}}{{#newline}}

    private object CreateInstance(Type tElement, Expression expression){{#newline}}
    {{{#newline}}
        var queryType = GetType().GetGenericTypeDefinition().MakeGenericType(tElement);{{#newline}}
        return Activator.CreateInstance(queryType, expression);{{#newline}}
    }{{#newline}}{{#newline}}

    public object Execute(Expression expression){{#newline}}
    {{{#newline}}
        return CompileExpressionItem<object>(expression);{{#newline}}
    }{{#newline}}{{#newline}}

    public TResult Execute<TResult>(Expression expression){{#newline}}
    {{{#newline}}
        return CompileExpressionItem<TResult>(expression);{{#newline}}
    }{{#newline}}{{#newline}}

    IEnumerator<T> IEnumerable<T>.GetEnumerator(){{#newline}}
    {{{#newline}}
        if (_enumerable == null) _enumerable = CompileExpressionItem<IEnumerable<T>>(Expression);{{#newline}}
        return _enumerable.GetEnumerator();{{#newline}}
    }{{#newline}}{{#newline}}

    IEnumerator IEnumerable.GetEnumerator(){{#newline}}
    {{{#newline}}
        if (_enumerable == null) _enumerable = CompileExpressionItem<IEnumerable<T>>(Expression);{{#newline}}
        return _enumerable.GetEnumerator();{{#newline}}
    }{{#newline}}{{#newline}}

    public Type ElementType => typeof(T);{{#newline}}{{#newline}}

    public Expression Expression { get; }{{#newline}}{{#newline}}

    public IQueryProvider Provider => this;{{#newline}}{{#newline}}

    private static TResult CompileExpressionItem<TResult>(Expression expression){{#newline}}
    {{{#newline}}
        var visitor = new FakeExpressionVisitor();{{#newline}}
        var body = visitor.Visit(expression);{{#newline}}
        var f = Expression.Lambda<Func<TResult>>(body ?? throw new InvalidOperationException(string.Format(""{0} is null"", nameof(body))), (IEnumerable<ParameterExpression>) null);{{#newline}}
        return f.Compile()();{{#newline}}
    }{{#newline}}
}{{#newline}}{{#newline}}


{{DbContextClassModifiers}} class FakeExpressionVisitor : ExpressionVisitor{{#newline}}
{{{#newline}}
}{{#newline}}{{#newline}}

public class FakeDatabaseFacade : DatabaseFacade{{#newline}}
{{{#newline}}
    public FakeDatabaseFacade(DbContext context) : base(context){{#newline}}
    {{{#newline}}
    }{{#newline}}{{#newline}}

    public override bool EnsureCreated(){{#newline}}
    {{{#newline}}
        return true;{{#newline}}
    }{{#newline}}{{#newline}}

    public override Task<bool> EnsureCreatedAsync(CancellationToken cancellationToken = new CancellationToken()){{#newline}}
    {{{#newline}}
        return Task.FromResult(EnsureCreated());{{#newline}}
    }{{#newline}}{{#newline}}

    public override bool EnsureDeleted(){{#newline}}
    {{{#newline}}
        return true;{{#newline}}
    }{{#newline}}{{#newline}}

    public override Task<bool> EnsureDeletedAsync(CancellationToken cancellationToken = new CancellationToken()){{#newline}}
    {{{#newline}}
        return Task.FromResult(EnsureDeleted());{{#newline}}
    }{{#newline}}{{#newline}}

    public override bool CanConnect(){{#newline}}
    {{{#newline}}
        return true;{{#newline}}
    }{{#newline}}{{#newline}}

    public override Task<bool> CanConnectAsync(CancellationToken cancellationToken = new CancellationToken()){{#newline}}
    {{{#newline}}
        return Task.FromResult(CanConnect());{{#newline}}
    }{{#newline}}{{#newline}}

    public override IDbContextTransaction BeginTransaction(){{#newline}}
    {{{#newline}}
        return new FakeDbContextTransaction();{{#newline}}
    }{{#newline}}{{#newline}}

    public override Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = new CancellationToken()){{#newline}}
    {{{#newline}}
        return Task.FromResult(BeginTransaction());{{#newline}}
    }{{#newline}}{{#newline}}

    public override void CommitTransaction(){{#newline}}
    {{{#newline}}
    }{{#newline}}{{#newline}}

    public override Task CommitTransactionAsync(CancellationToken cancellationToken = new CancellationToken()){{#newline}}
    {{{#newline}}
        return Task.CompletedTask;{{#newline}}
    }{{#newline}}{{#newline}}

    public override void RollbackTransaction(){{#newline}}
    {{{#newline}}
    }{{#newline}}{{#newline}}

    public override Task RollbackTransactionAsync(CancellationToken cancellationToken = new CancellationToken()){{#newline}}
    {{{#newline}}
        return Task.CompletedTask;{{#newline}}
    }{{#newline}}{{#newline}}

    public override IExecutionStrategy CreateExecutionStrategy(){{#newline}}
    {{{#newline}}
        return null;{{#newline}}
    }{{#newline}}{{#newline}}

    public override string ToString(){{#newline}}
    {{{#newline}}
        return string.Empty;{{#newline}}
    }{{#newline}}
}{{#newline}}{{#newline}}

public class FakeDbContextTransaction : IDbContextTransaction{{#newline}}
{{{#newline}}
    public Guid TransactionId => Guid.NewGuid();{{#newline}}
    public void Commit() { }{{#newline}}
    public void Rollback() { }{{#newline}}
    public Task CommitAsync(CancellationToken cancellationToken = new CancellationToken()) => Task.CompletedTask;{{#newline}}
    public Task RollbackAsync(CancellationToken cancellationToken = new CancellationToken()) => Task.CompletedTask;{{#newline}}
    public void Dispose() { }{{#newline}}
    public ValueTask DisposeAsync() => default;{{#newline}}
}";
        }

        public override List<string> PocoUsings(PocoModel data)
        {
            var usings = new List<string>
            {
                "System",
                "System.Collections.Generic",
                "System.Threading",
                "System.Threading.Tasks",
            };

            if (Settings.IncludeCodeGeneratedAttribute)
                usings.Add("System.CodeDom.Compiler");

            if(data.HasHierarchyId)
                usings.Add("Microsoft.EntityFrameworkCore");

            return usings;
        }

        public override string Poco()
        {
            return @"
{{#if UseHasNoKey}}
{{#else}}
{{#if HasNoPrimaryKey}}
// The table '{{Name}}' is not usable by entity framework because it{{#newline}}
// does not have a primary key. It is listed here for completeness.{{#newline}}
{{/if}}
{{/if}}

{{ClassComment}}
{{ExtendedComments}}
{{ClassAttributes}}
{{ClassModifier}} class {{NameHumanCaseWithSuffix}}{{BaseClasses}}{{#newline}}
{{{#newline}}
{{InsideClassBody}}

{{#each Columns}}
{{#if AddNewLineBefore}}{{#newline}}{{/if}}
{{#if HasSummaryComments}}
    /// <summary>{{#newline}}
    /// {{SummaryComments}}{{#newline}}
    /// </summary>{{#newline}}
{{/if}}
{{#each Attributes}}
    {{this}}{{#newline}}
{{/each}}
    public {{#if OverrideModifier}}override {{/if}}{{WrapIfNullable}} {{NameHumanCase}} { get; {{PrivateSetterForComputedColumns}}set; }{{PropertyInitialisers}}{{InlineComments}}{{#newline}}
{{/each}}

{{#if HasReverseNavigation}}
{{#newline}}
    // Reverse navigation{{#newline}}

{{#each ReverseNavigationProperty}}

{{#if ReverseNavHasComment}}
{{#newline}}
    /// <summary>{{#newline}}
    /// {{ReverseNavComment}}{{#newline}}
    /// </summary>{{#newline}}
{{/if}}

{{#each AdditionalReverseNavigationsDataAnnotations}}
    [{{this}}]{{#newline}}
{{/each}}

{{#each AdditionalDataAnnotations}}
    [{{this}}]{{#newline}}
{{/each}}

    {{Definition}}{{#newline}}
{{/each}}
{{/if}}


{{#if HasForeignKey}}
{{#newline}}
{{ForeignKeyTitleComment}}

{{#each ForeignKeys}}

{{#if HasFkComment}}
{{#newline}}
    /// <summary>{{#newline}}
    /// {{FkComment}}{{#newline}}
    /// </summary>{{#newline}}
{{/if}}

{{#each AdditionalForeignKeysDataAnnotations}}
    [{{this}}]{{#newline}}
{{/each}}

{{#each AdditionalDataAnnotations}}
    [{{this}}]{{#newline}}
{{/each}}

    {{Definition}}{{#newline}}
{{/each}}
{{/if}}

{{#if CreateConstructor}}
{{#newline}}
    public {{NameHumanCaseWithSuffix}}(){{#newline}}
    {{{#newline}}

{{#each ColumnsWithDefaults}}
        {{NameHumanCase}} = {{Default}};{{#newline}}
{{/each}}

{{#each ReverseNavigationCtor}}
        {{this}}{{#newline}}
{{/each}}

{{#if EntityClassesArePartial}}
        InitializePartial();{{#newline}}
{{/if}}

    }{{#newline}}

{{#if EntityClassesArePartial}}
{{#newline}}
    partial void InitializePartial();{{#newline}}
{{/if}}

{{/if}}

}{{#newline}}
";
        }

        public override List<string> PocoConfigurationUsings(PocoConfigurationModel data)
        {
            var usings = new List<string>
            {
                "Microsoft.EntityFrameworkCore",
                "Microsoft.EntityFrameworkCore.Metadata.Builders"
            };

            if (Settings.IncludeCodeGeneratedAttribute)
                usings.Add("System.CodeDom.Compiler");

            if (Settings.TrimCharFields)
                usings.Add("Microsoft.EntityFrameworkCore.Storage.ValueConversion");

            if(data.UsesDictionary)
                usings.Add("System.Collections.Generic");

            return usings;
        }

        public override string PocoConfiguration()
        {
            return @"
{{ClassComment}}
{{ClassModifier}} class {{ConfigurationClassName}} : IEntityTypeConfiguration<{{NameHumanCaseWithSuffix}}>{{#newline}}
{{{#newline}}

    public void Configure(EntityTypeBuilder<{{NameHumanCaseWithSuffix}}> builder){{#newline}}
    {{{#newline}}
{{#if HasSchema}}
        builder.{{ToTableOrView}}(""{{Name}}"", ""{{Schema}}"");{{#newline}}
{{#else}}
        builder.{{ToTableOrView}}(""{{Name}}"");{{#newline}}
{{/if}}
        {{PrimaryKeyNameHumanCase}}{{#newline}}{{#newline}}

{{#each Columns}}
        {{this}}{{#newline}}
{{/each}}

{{#if HasForeignKey}}
{{#newline}}
        // Foreign keys{{#newline}}
{{#each ForeignKeys}}
        {{this}}{{#newline}}
{{/each}}
{{/if}}

{{#each MappingConfiguration}}
        builder.{{this}}{{#newline}}
{{/each}}

{{#if HasIndexes}}
{{#newline}}
{{#each Indexes}}
        {{this}}{{#newline}}
{{/each}}
{{/if}}

{{#if ConfigurationClassesArePartial}}
{{#newline}}
        InitializePartial(builder);{{#newline}}
{{/if}}

    }{{#newline}}

{{#if ConfigurationClassesArePartial}}
{{#newline}}
    partial void InitializePartial(EntityTypeBuilder<{{NameHumanCaseWithSuffix}}> builder);{{#newline}}
{{/if}}

}{{#newline}}";
        }

        public override List<string> StoredProcReturnModelUsings()
        {
            var usings = new List<string>
            {
                "System",
                "System.Collections.Generic"
            };

            if (Settings.IncludeCodeGeneratedAttribute)
                usings.Add("System.CodeDom.Compiler");

            return usings;
        }

        public override string StoredProcReturnModels()
        {
            return @"
{{ResultClassModifiers}} class {{WriteStoredProcReturnModelName}}{{#newline}}
{{{#newline}}
{{#if SingleModel}}
{{#each SingleModelReturnColumns}}
    {{this}}{{#newline}}
{{/each}}
{{#else}}
{{#each MultipleModelReturnColumns}}
    public class ResultSetModel{{Model}}{{#newline}}
    {{{#newline}}
{{#each ReturnColumns}}
        {{this}}{{#newline}}
{{/each}}
    }{{#newline}}
    public List<ResultSetModel{{Model}}> ResultSet{{Model}};{{#newline}}
{{/each}}
{{/if}}
}{{#newline}}
";
        }

        public override List<string> EnumUsings()
        {
            var usings = new List<string>();

            if (Settings.IncludeCodeGeneratedAttribute)
                usings.Add("System.CodeDom.Compiler");

            return usings;
        }

        public override string Enums()
        {
            return @"
{{#each EnumAttributes}}
{{this}}{{#newline}}
{{/each}}
public enum {{EnumName}}{{#newline}}
{{{#newline}}
{{#each Items}}
{{#each Attributes}}
    {{this}}{{#newline}}
{{/each}}
    {{Key}} = {{Value}},{{#newline}}
{{/each}}
}{{#newline}}
";
        }
    }
}