﻿// <auto-generated>

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Tester.Integration.EfCore3.File_based_templates
{
    #region Database context interface

    public interface ITestDbContext : IDisposable
    {
        DbSet<ColumnName> ColumnNames { get; set; } // ColumnNames
        DbSet<Stafford_Boo> Stafford_Boos { get; set; } // Boo
        DbSet<Stafford_ComputedColumn> Stafford_ComputedColumns { get; set; } // ComputedColumns
        DbSet<Stafford_Foo> Stafford_Foos { get; set; } // Foo
        DbSet<Synonyms_Child> Synonyms_Children { get; set; } // Child
        DbSet<Synonyms_Parent> Synonyms_Parents { get; set; } // Parent
        DbSet<UserInfo> UserInfoes { get; set; } // UserInfo
        DbSet<UserInfoAttribute> UserInfoAttributes { get; set; } // UserInfoAttributes

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
        DatabaseFacade Database { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        string ToString();

        // Stored Procedures
        List<Synonyms_SimpleStoredProcReturnModel> Synonyms_SimpleStoredProc(int? inputInt);
        List<Synonyms_SimpleStoredProcReturnModel> Synonyms_SimpleStoredProc(int? inputInt, out int procResult);
        Task<List<Synonyms_SimpleStoredProcReturnModel>> Synonyms_SimpleStoredProcAsync(int? inputInt);


        // Table Valued Functions
        IQueryable<CsvToIntReturnModel> CsvToInt(string array, string array2); // dbo.CsvToInt

        // Scalar Valued Functions
        decimal UdfNetSale(int? quantity, decimal? listPrice, decimal? discount); // dbo.udfNetSale
    }

    #endregion

    #region Database context

    public class TestDbContext : DbContext, ITestDbContext
    {
        public TestDbContext()
        {
        }

        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
        }

        public DbSet<ColumnName> ColumnNames { get; set; } // ColumnNames
        public DbSet<Stafford_Boo> Stafford_Boos { get; set; } // Boo
        public DbSet<Stafford_ComputedColumn> Stafford_ComputedColumns { get; set; } // ComputedColumns
        public DbSet<Stafford_Foo> Stafford_Foos { get; set; } // Foo
        public DbSet<Synonyms_Child> Synonyms_Children { get; set; } // Child
        public DbSet<Synonyms_Parent> Synonyms_Parents { get; set; } // Parent
        public DbSet<UserInfo> UserInfoes { get; set; } // UserInfo
        public DbSet<UserInfoAttribute> UserInfoAttributes { get; set; } // UserInfoAttributes

        public bool IsSqlParameterNull(SqlParameter param)
        {
            var sqlValue = param.SqlValue;
            var nullableValue = sqlValue as INullable;
            if (nullableValue != null)
                return nullableValue.IsNull;
            return (sqlValue == null || sqlValue == DBNull.Value);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ColumnNameConfiguration());
            modelBuilder.ApplyConfiguration(new Stafford_BooConfiguration());
            modelBuilder.ApplyConfiguration(new Stafford_ComputedColumnConfiguration());
            modelBuilder.ApplyConfiguration(new Stafford_FooConfiguration());
            modelBuilder.ApplyConfiguration(new Synonyms_ChildConfiguration());
            modelBuilder.ApplyConfiguration(new Synonyms_ParentConfiguration());
            modelBuilder.ApplyConfiguration(new UserInfoConfiguration());
            modelBuilder.ApplyConfiguration(new UserInfoAttributeConfiguration());

            modelBuilder.Entity<Synonyms_SimpleStoredProcReturnModel>().HasNoKey();

            // Table Valued Functions
            modelBuilder.Entity<CsvToIntReturnModel>().HasNoKey();
        }


        // Stored Procedures
        public List<Synonyms_SimpleStoredProcReturnModel> Synonyms_SimpleStoredProc(int? inputInt)
        {
            int procResult;
            return Synonyms_SimpleStoredProc(inputInt, out procResult);
        }

        public List<Synonyms_SimpleStoredProcReturnModel> Synonyms_SimpleStoredProc(int? inputInt, out int procResult)
        {
            var inputIntParam = new SqlParameter { ParameterName = "@InputInt", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = inputInt.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!inputInt.HasValue)
                inputIntParam.Value = DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            const string sqlCommand = "EXEC @procResult = [Synonyms].[SimpleStoredProc] @InputInt";
            var procResultData = Set<Synonyms_SimpleStoredProcReturnModel>()
                .FromSqlRaw(sqlCommand, inputIntParam, procResultParam)
                .ToList();

            procResult = (int) procResultParam.Value;
            return procResultData;
        }

        public async Task<List<Synonyms_SimpleStoredProcReturnModel>> Synonyms_SimpleStoredProcAsync(int? inputInt)
        {
            var inputIntParam = new SqlParameter { ParameterName = "@InputInt", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = inputInt.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!inputInt.HasValue)
                inputIntParam.Value = DBNull.Value;

            const string sqlCommand = "EXEC [Synonyms].[SimpleStoredProc] @InputInt";
            var procResultData = await Set<Synonyms_SimpleStoredProcReturnModel>()
                .FromSqlRaw(sqlCommand, inputIntParam)
                .ToListAsync();

            return procResultData;
        }


        // Table Valued Functions

        // dbo.CsvToInt
        public IQueryable<CsvToIntReturnModel> CsvToInt(string array, string array2)
        {
            return Set<CsvToIntReturnModel>()
                .FromSqlRaw("SELECT * FROM [CsvToInt]({0}, {1})", array, array2)
                .AsNoTracking();
        }

        // Scalar Valued Functions

        [DbFunction("udfNetSale", "dbo")]
        public decimal UdfNetSale(int? quantity, decimal? listPrice, decimal? discount)
        {
            throw new Exception("Don't call this directly. Use LINQ to call the scalar valued function as part of your query");
        }
    }

    #endregion

    #region Database context factory

    public class TestDbContextFactory : IDesignTimeDbContextFactory<TestDbContext>
    {
        public TestDbContext CreateDbContext(string[] args)
        {
            return new TestDbContext();
        }
    }

    #endregion

    #region Fake Database context

    public class FakeTestDbContext : ITestDbContext
    {
        public DbSet<ColumnName> ColumnNames { get; set; } // ColumnNames
        public DbSet<Stafford_Boo> Stafford_Boos { get; set; } // Boo
        public DbSet<Stafford_ComputedColumn> Stafford_ComputedColumns { get; set; } // ComputedColumns
        public DbSet<Stafford_Foo> Stafford_Foos { get; set; } // Foo
        public DbSet<Synonyms_Child> Synonyms_Children { get; set; } // Child
        public DbSet<Synonyms_Parent> Synonyms_Parents { get; set; } // Parent
        public DbSet<UserInfo> UserInfoes { get; set; } // UserInfo
        public DbSet<UserInfoAttribute> UserInfoAttributes { get; set; } // UserInfoAttributes

        public FakeTestDbContext()
        {
            _database = null;

            ColumnNames = new FakeDbSet<ColumnName>("C36");
            Stafford_Boos = new FakeDbSet<Stafford_Boo>("Id");
            Stafford_ComputedColumns = new FakeDbSet<Stafford_ComputedColumn>("Id");
            Stafford_Foos = new FakeDbSet<Stafford_Foo>("Id");
            Synonyms_Children = new FakeDbSet<Synonyms_Child>("ChildId");
            Synonyms_Parents = new FakeDbSet<Synonyms_Parent>("ParentId");
            UserInfoes = new FakeDbSet<UserInfo>("Id");
            UserInfoAttributes = new FakeDbSet<UserInfoAttribute>("Id");

        }

        public int SaveChangesCount { get; private set; }
        public virtual int SaveChanges()
        {
            ++SaveChangesCount;
            return 1;
        }

        public virtual int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return SaveChanges();
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            ++SaveChangesCount;
            return Task<int>.Factory.StartNew(() => 1, cancellationToken);
        }
        public virtual Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken)
        {
            ++SaveChangesCount;
            return Task<int>.Factory.StartNew(x => 1, acceptAllChangesOnSuccess, cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private DatabaseFacade _database;
        public DatabaseFacade Database { get { return _database; } }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        // Stored Procedures

        public DbSet<Synonyms_SimpleStoredProcReturnModel> Synonyms_SimpleStoredProcReturnModel { get; set; }
        public List<Synonyms_SimpleStoredProcReturnModel> Synonyms_SimpleStoredProc(int? inputInt)
        {
            int procResult;
            return Synonyms_SimpleStoredProc(inputInt, out procResult);
        }

        public List<Synonyms_SimpleStoredProcReturnModel> Synonyms_SimpleStoredProc(int? inputInt, out int procResult)
        {
            procResult = 0;
            return new List<Synonyms_SimpleStoredProcReturnModel>();
        }

        public Task<List<Synonyms_SimpleStoredProcReturnModel>> Synonyms_SimpleStoredProcAsync(int? inputInt)
        {
            int procResult;
            return Task.FromResult(Synonyms_SimpleStoredProc(inputInt, out procResult));
        }

        // Table Valued Functions

        // dbo.CsvToInt
        public IQueryable<CsvToIntReturnModel> CsvToInt(string array, string array2)
        {
            return new List<CsvToIntReturnModel>().AsQueryable();
        }

        // Scalar Valued Functions

        // dbo.udfNetSale
        public decimal UdfNetSale(int? quantity, decimal? listPrice, decimal? discount)
        {
            return default(decimal);
        }
    }

    #endregion

    #region Fake DbSet

    // ************************************************************************
    // Fake DbSet
    // Implementing Find:
    //      The Find method is difficult to implement in a generic fashion. If
    //      you need to test code that makes use of the Find method it is
    //      easiest to create a test DbSet for each of the entity types that
    //      need to support find. You can then write logic to find that
    //      particular type of entity, as shown below:
    //      public class FakeBlogDbSet : FakeDbSet<Blog>
    //      {
    //          public override Blog Find(params object[] keyValues)
    //          {
    //              var id = (int) keyValues.Single();
    //              return this.SingleOrDefault(b => b.BlogId == id);
    //          }
    //      }
    //      Read more about it here: https://msdn.microsoft.com/en-us/data/dn314431.aspx
    public class FakeDbSet<TEntity> : DbSet<TEntity>, IQueryable<TEntity>, IAsyncEnumerable<TEntity>, IListSource where TEntity : class
    {
        private readonly PropertyInfo[] _primaryKeys;
        private readonly ObservableCollection<TEntity> _data;
        private readonly IQueryable _query;

        public FakeDbSet()
        {
            _primaryKeys = null;
            _data        = new ObservableCollection<TEntity>();
            _query       = _data.AsQueryable();
        }

        public FakeDbSet(params string[] primaryKeys)
        {
            _primaryKeys = typeof(TEntity).GetProperties().Where(x => primaryKeys.Contains(x.Name)).ToArray();
            _data        = new ObservableCollection<TEntity>();
            _query       = _data.AsQueryable();
        }

        public override TEntity Find(params object[] keyValues)
        {
            if (_primaryKeys == null)
                throw new ArgumentException("No primary keys defined");
            if (keyValues.Length != _primaryKeys.Length)
                throw new ArgumentException("Incorrect number of keys passed to Find method");

            var keyQuery = this.AsQueryable();
            keyQuery = keyValues
                .Select((t, i) => i)
                .Aggregate(keyQuery,
                    (current, x) =>
                        current.Where(entity => _primaryKeys[x].GetValue(entity, null).Equals(keyValues[x])));

            return keyQuery.SingleOrDefault();
        }

        public override ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken)
        {
            return new ValueTask<TEntity>(Task<TEntity>.Factory.StartNew(() => Find(keyValues), cancellationToken));
        }

        public override ValueTask<TEntity> FindAsync(params object[] keyValues)
        {
            return new ValueTask<TEntity>(Task<TEntity>.Factory.StartNew(() => Find(keyValues)));
        }

        IAsyncEnumerator<TEntity> IAsyncEnumerable<TEntity>.GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return GetAsyncEnumerator(cancellationToken);
        }

        public override EntityEntry<TEntity> Add(TEntity entity)
        {
            _data.Add(entity);
            return null;
        }

        public override void AddRange(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            foreach (var entity in entities.ToList())
                _data.Add(entity);
        }

        public override void AddRange(IEnumerable<TEntity> entities)
        {
            AddRange(entities.ToArray());
        }

        public override Task AddRangeAsync(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return Task.Factory.StartNew(() => AddRange(entities));
        }

        public override void AttachRange(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            AddRange(entities);
        }

        public override void RemoveRange(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            foreach (var entity in entities.ToList())
                _data.Remove(entity);
        }

        public override void RemoveRange(IEnumerable<TEntity> entities)
        {
            RemoveRange(entities.ToArray());
        }

        public override void UpdateRange(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            RemoveRange(entities);
            AddRange(entities);
        }

        public IList GetList()
        {
            return _data;
        }

        IList IListSource.GetList()
        {
            return _data;
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new FakeDbAsyncQueryProvider<TEntity>(_query.Provider); }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new FakeDbAsyncEnumerator<TEntity>(this.AsEnumerable().GetEnumerator());
        }

    }

    public class FakeDbAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        public FakeDbAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            var m = expression as MethodCallExpression;
            if (m != null)
            {
                var resultType = m.Method.ReturnType; // it should be IQueryable<T>
                var tElement = resultType.GetGenericArguments()[0];
                var queryType = typeof(FakeDbAsyncEnumerable<>).MakeGenericType(tElement);
                return (IQueryable) Activator.CreateInstance(queryType, expression);
            }
            return new FakeDbAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            var queryType = typeof(FakeDbAsyncEnumerable<>).MakeGenericType(typeof(TElement));
            return (IQueryable<TElement>) Activator.CreateInstance(queryType, expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = new CancellationToken())
        {
            return _inner.Execute<TResult>(expression);
        }
    }

    public class FakeDbAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public FakeDbAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        {
        }

        public FakeDbAsyncEnumerable(Expression expression)
            : base(expression)
        {
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            return new FakeDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IAsyncEnumerator<T> IAsyncEnumerable<T>.GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return GetAsyncEnumerator(cancellationToken);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.AsEnumerable().GetEnumerator();
        }
    }

    public class FakeDbAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public FakeDbAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public T Current
        {
            get { return _inner.Current; }
        }
        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(_inner.MoveNext());
        }

        public ValueTask DisposeAsync()
        {
            _inner.Dispose();
            return new ValueTask(Task.CompletedTask);
        }
    }

    #endregion

    #region POCO classes

    // ColumnNames
    /// <summary>
    /// This is to document the
    /// table with poor column name choices
    /// </summary>
    public class ColumnName
    {
        public int C36 { get; set; } // $ (Primary key)
        public int? C37 { get; set; } // %
        public int? C163 { get; set; } // £

        /// <summary>
        /// Multi
            ///        Line
            ///    Comment
        /// </summary>
        public int? C38Test36 { get; set; } // &test$
        public int? Abc4792 { get; set; } // abc/\
        public int? Joe46Bloggs { get; set; } // joe.bloggs
        public int? SnakeCase { get; set; } // snake-case
        public string DefaultTest { get; set; } // default_test (length: 20)
        public DateTime SomeDate { get; set; } // someDate
        public string Obs { get; set; } // Obs (length: 20)
        public string Slash1 { get; set; } // Slash1 (length: 20)
        public string Slash2 { get; set; } // Slash2 (length: 20)
        public string Slash3 { get; set; } // Slash3 (length: 20)
        public int? @Static { get; set; } // static
        public int? @Readonly { get; set; } // readonly
        public int? C123Hi { get; set; } // 123Hi
        public float? Afloat { get; set; } // afloat
        public double? Adouble { get; set; } // adouble
        public decimal? Adecimal { get; set; } // adecimal

        public ColumnName()
        {
            DefaultTest = "";
            SomeDate = DateTime.Now;
            Obs = "[{\"k\":\"en\",\"v\":\"\"},{\"k\":\"pt\",\"v\":\"\"}]";
            Slash1 = @"\";
            Slash2 = @"\\";
            Slash3 = @"\\\";
            Afloat = 1.23f;
            Adouble = 999.0;
        }
    }

    // The table 'NoPrimaryKeys' is not usable by entity framework because it
    // does not have a primary key. It is listed here for completeness.
    // NoPrimaryKeys
    public class NoPrimaryKey
    {
        public int? Id { get; set; } // Id
        public string Description { get; set; } // Description (length: 10)
    }

    // Boo
    public class Stafford_Boo
    {
        public int Id { get; set; } // id (Primary key)
        public string Name { get; set; } // name (length: 10)

        // Reverse navigation

        /// <summary>
        /// Parent (One-to-One) Stafford_Boo pointed by [Foo].[id] (FK_Foo_Boo)
        /// </summary>
        public virtual Stafford_Foo Stafford_Foo { get; set; } // Foo.FK_Foo_Boo
    }

    // ComputedColumns
    public class Stafford_ComputedColumn
    {
        public int Id { get; set; } // Id (Primary key)
        public string MyColumn { get; set; } // MyColumn (length: 10)
        public string MyComputedColumn { get; private set; } // MyComputedColumn (length: 10)
    }

    // Foo
    public class Stafford_Foo
    {
        public int Id { get; set; } // id (Primary key)
        public string Name { get; set; } // name (length: 10)

        // Foreign keys

        /// <summary>
        /// Parent Stafford_Boo pointed by [Foo].([Id]) (FK_Foo_Boo)
        /// </summary>
        public virtual Stafford_Boo Stafford_Boo { get; set; } // FK_Foo_Boo
    }

    // Child
    public class Synonyms_Child
    {
        public int ChildId { get; set; } // ChildId (Primary key)
        public int ParentId { get; set; } // ParentId
        public string ChildName { get; set; } // ChildName (length: 100)

        // Foreign keys

        /// <summary>
        /// Parent Synonyms_Parent pointed by [Child].([ParentId]) (FK_Child_Parent)
        /// </summary>
        public virtual Synonyms_Parent Synonyms_Parent { get; set; } // FK_Child_Parent
    }

    // Parent
    public class Synonyms_Parent
    {
        public int ParentId { get; set; } // ParentId (Primary key)
        public string ParentName { get; set; } // ParentName (length: 100)

        // Reverse navigation

        /// <summary>
        /// Child Synonyms_Children where [Child].[ParentId] point to this entity (FK_Child_Parent)
        /// </summary>
        public virtual ICollection<Synonyms_Child> Synonyms_Children { get; set; } // Child.FK_Child_Parent

        public Synonyms_Parent()
        {
            Synonyms_Children = new List<Synonyms_Child>();
        }
    }

    // UserInfo
    public class UserInfo
    {
        public int Id { get; set; } // Id (Primary key)

        // Reverse navigation

        /// <summary>
        /// Child UserInfoAttributes where [UserInfoAttributes].[PrimaryId] point to this entity (FK_UserInfoAttributes_PrimaryUserInfo)
        /// </summary>
        public virtual ICollection<UserInfoAttribute> UserInfoAttributes_PrimaryId { get; set; } // UserInfoAttributes.FK_UserInfoAttributes_PrimaryUserInfo

        /// <summary>
        /// Child UserInfoAttributes where [UserInfoAttributes].[SecondaryId] point to this entity (FK_UserInfoAttributes_SecondaryUserInfo)
        /// </summary>
        public virtual ICollection<UserInfoAttribute> UserInfoAttributes_SecondaryId { get; set; } // UserInfoAttributes.FK_UserInfoAttributes_SecondaryUserInfo

        public UserInfo()
        {
            UserInfoAttributes_PrimaryId = new List<UserInfoAttribute>();
            UserInfoAttributes_SecondaryId = new List<UserInfoAttribute>();
        }
    }

    // UserInfoAttributes
    public class UserInfoAttribute
    {
        public int Id { get; set; } // Id (Primary key)
        public int PrimaryId { get; set; } // PrimaryId
        public int SecondaryId { get; set; } // SecondaryId

        // Foreign keys

        /// <summary>
        /// Parent UserInfo pointed by [UserInfoAttributes].([PrimaryId]) (FK_UserInfoAttributes_PrimaryUserInfo)
        /// </summary>
        public virtual UserInfo Primary { get; set; } // FK_UserInfoAttributes_PrimaryUserInfo

        /// <summary>
        /// Parent UserInfo pointed by [UserInfoAttributes].([SecondaryId]) (FK_UserInfoAttributes_SecondaryUserInfo)
        /// </summary>
        public virtual UserInfo Secondary { get; set; } // FK_UserInfoAttributes_SecondaryUserInfo
    }


    #endregion

    #region POCO Configuration

    // ColumnNames
    public class ColumnNameConfiguration : IEntityTypeConfiguration<ColumnName>
    {
        public void Configure(EntityTypeBuilder<ColumnName> builder)
        {
            builder.ToTable("ColumnNames", "dbo");
            builder.HasKey(x => x.C36).HasName("PK_ColumnNames").IsClustered();

            builder.Property(x => x.C36).HasColumnName(@"$").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.C37).HasColumnName(@"%").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.C163).HasColumnName(@"£").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.C38Test36).HasColumnName(@"&test$").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Abc4792).HasColumnName(@"abc/\").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Joe46Bloggs).HasColumnName(@"joe.bloggs").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.SnakeCase).HasColumnName(@"snake-case").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.DefaultTest).HasColumnName(@"default_test").HasColumnType("varchar(20)").IsRequired().IsUnicode(false).HasMaxLength(20);
            builder.Property(x => x.SomeDate).HasColumnName(@"someDate").HasColumnType("datetime2").IsRequired();
            builder.Property(x => x.Obs).HasColumnName(@"Obs").HasColumnType("varchar(20)").IsRequired(false).IsUnicode(false).HasMaxLength(20);
            builder.Property(x => x.Slash1).HasColumnName(@"Slash1").HasColumnType("varchar(20)").IsRequired(false).IsUnicode(false).HasMaxLength(20);
            builder.Property(x => x.Slash2).HasColumnName(@"Slash2").HasColumnType("varchar(20)").IsRequired(false).IsUnicode(false).HasMaxLength(20);
            builder.Property(x => x.Slash3).HasColumnName(@"Slash3").HasColumnType("varchar(20)").IsRequired(false).IsUnicode(false).HasMaxLength(20);
            builder.Property(x => x.@Static).HasColumnName(@"static").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.@Readonly).HasColumnName(@"readonly").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.C123Hi).HasColumnName(@"123Hi").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Afloat).HasColumnName(@"afloat").HasColumnType("real").IsRequired(false);
            builder.Property(x => x.Adouble).HasColumnName(@"adouble").HasColumnType("float").IsRequired(false);
            builder.Property(x => x.Adecimal).HasColumnName(@"adecimal").HasColumnType("decimal(19,4)").IsRequired(false);
        }
    }

    // Boo
    public class Stafford_BooConfiguration : IEntityTypeConfiguration<Stafford_Boo>
    {
        public void Configure(EntityTypeBuilder<Stafford_Boo> builder)
        {
            builder.ToTable("Boo", "Stafford");
            builder.HasKey(x => x.Id).HasName("PK_Boo").IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Name).HasColumnName(@"name").HasColumnType("nchar(10)").IsRequired().IsFixedLength().HasMaxLength(10);
        }
    }

    // ComputedColumns
    public class Stafford_ComputedColumnConfiguration : IEntityTypeConfiguration<Stafford_ComputedColumn>
    {
        public void Configure(EntityTypeBuilder<Stafford_ComputedColumn> builder)
        {
            builder.ToTable("ComputedColumns", "Stafford");
            builder.HasKey(x => x.Id).HasName("PK_Stafford_ComputedColumns").IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.MyColumn).HasColumnName(@"MyColumn").HasColumnType("varchar(10)").IsRequired().IsUnicode(false).HasMaxLength(10);
            builder.Property(x => x.MyComputedColumn).HasColumnName(@"MyComputedColumn").HasColumnType("varchar(10)").IsRequired().IsUnicode(false).HasMaxLength(10).ValueGeneratedOnAddOrUpdate();
        }
    }

    // Foo
    public class Stafford_FooConfiguration : IEntityTypeConfiguration<Stafford_Foo>
    {
        public void Configure(EntityTypeBuilder<Stafford_Foo> builder)
        {
            builder.ToTable("Foo", "Stafford");
            builder.HasKey(x => x.Id).HasName("PK_Foo").IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"id").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Name).HasColumnName(@"name").HasColumnType("nchar(10)").IsRequired().IsFixedLength().HasMaxLength(10);

            // Foreign keys
            builder.HasOne(a => a.Stafford_Boo).WithOne(b => b.Stafford_Foo).HasForeignKey<Stafford_Foo>(c => c.Id).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Foo_Boo");
        }
    }

    // Child
    public class Synonyms_ChildConfiguration : IEntityTypeConfiguration<Synonyms_Child>
    {
        public void Configure(EntityTypeBuilder<Synonyms_Child> builder)
        {
            builder.ToTable("Child", "Synonyms");
            builder.HasKey(x => x.ChildId).HasName("PK_Child").IsClustered();

            builder.Property(x => x.ChildId).HasColumnName(@"ChildId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.ParentId).HasColumnName(@"ParentId").HasColumnType("int").IsRequired();
            builder.Property(x => x.ChildName).HasColumnName(@"ChildName").HasColumnType("varchar(100)").IsRequired(false).IsUnicode(false).HasMaxLength(100);

            // Foreign keys
            builder.HasOne(a => a.Synonyms_Parent).WithMany(b => b.Synonyms_Children).HasForeignKey(c => c.ParentId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Child_Parent");
        }
    }

    // Parent
    public class Synonyms_ParentConfiguration : IEntityTypeConfiguration<Synonyms_Parent>
    {
        public void Configure(EntityTypeBuilder<Synonyms_Parent> builder)
        {
            builder.ToTable("Parent", "Synonyms");
            builder.HasKey(x => x.ParentId).HasName("PK_Parent").IsClustered();

            builder.Property(x => x.ParentId).HasColumnName(@"ParentId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.ParentName).HasColumnName(@"ParentName").HasColumnType("varchar(100)").IsRequired().IsUnicode(false).HasMaxLength(100);
        }
    }

    // UserInfo
    public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.ToTable("UserInfo", "dbo");
            builder.HasKey(x => x.Id).HasName("PK_UserInfo").IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
        }
    }

    // UserInfoAttributes
    public class UserInfoAttributeConfiguration : IEntityTypeConfiguration<UserInfoAttribute>
    {
        public void Configure(EntityTypeBuilder<UserInfoAttribute> builder)
        {
            builder.ToTable("UserInfoAttributes", "dbo");
            builder.HasKey(x => x.Id).HasName("PK_UserInfoAttributes").IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.PrimaryId).HasColumnName(@"PrimaryId").HasColumnType("int").IsRequired();
            builder.Property(x => x.SecondaryId).HasColumnName(@"SecondaryId").HasColumnType("int").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Primary).WithMany(b => b.UserInfoAttributes_PrimaryId).HasForeignKey(c => c.PrimaryId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserInfoAttributes_PrimaryUserInfo");
            builder.HasOne(a => a.Secondary).WithMany(b => b.UserInfoAttributes_SecondaryId).HasForeignKey(c => c.SecondaryId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserInfoAttributes_SecondaryUserInfo");
        }
    }


    #endregion

    #region Enumerations

    public enum CarOptions
    {
        SunRoof = 0x01,
        Spoiler = 0x02,
        FogLights = 0x04,
        TintedWindows = 0x08,
    }

    public enum DaysOfWeek
    {
        Sun = 0,
        Mon = 1,
        Tue = 2,
        Wed = 3,
        Thu = 4,
        Fri = 6,
        Sat = 7,
    }


    #endregion

    #region Stored procedure return models

    public class CsvToIntReturnModel
    {
        public int? IntValue { get; set; }
    }

    public class Synonyms_SimpleStoredProcReturnModel
    {
        public string ReturnValue { get; set; }
    }


    #endregion

}
// </auto-generated>
