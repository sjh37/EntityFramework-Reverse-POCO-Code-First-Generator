﻿// <auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using NpgsqlTypes;
using System;
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

namespace Efrpg.PostgreSQL
{
    #region Database context interface

    public interface IMyDbContext : IDisposable
    {
        DbSet<Allcolumntype> Allcolumntypes { get; set; } // allcolumntypes

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
        DatabaseFacade Database { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        string ToString();

        EntityEntry Add(object entity);
        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
        Task AddRangeAsync(params object[] entities);
        Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default);
        ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
        ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default);
        void AddRange(IEnumerable<object> entities);
        void AddRange(params object[] entities);

        EntityEntry Attach(object entity);
        EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;
        void AttachRange(IEnumerable<object> entities);
        void AttachRange(params object[] entities);

        EntityEntry Entry(object entity);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        TEntity Find<TEntity>(params object[] keyValues) where TEntity : class;
        ValueTask<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : class;
        ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class;
        ValueTask<object> FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken);
        ValueTask<object> FindAsync(Type entityType, params object[] keyValues);
        object Find(Type entityType, params object[] keyValues);

        EntityEntry Remove(object entity);
        EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;
        void RemoveRange(IEnumerable<object> entities);
        void RemoveRange(params object[] entities);

        EntityEntry Update(object entity);
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
        void UpdateRange(IEnumerable<object> entities);
        void UpdateRange(params object[] entities);

        IQueryable<TResult> FromExpression<TResult> (Expression<Func<IQueryable<TResult>>> expression);
    }

    #endregion

    #region Database context

    public class MyDbContext : DbContext, IMyDbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Allcolumntype> Allcolumntypes { get; set; } // allcolumntypes

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(@"Server=127.0.0.1;Port=5432;Database=EfrpgTest;User Id=testuser;Password=testtesttest;");
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        public bool IsSqlParameterNull(NpgsqlParameter param)
        {
            var sqlValue = param.NpgsqlValue;
            var nullableValue = sqlValue as INullable;
            if (nullableValue != null)
                return nullableValue.IsNull;
            return (sqlValue == null || sqlValue == DBNull.Value);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AllcolumntypeConfiguration());
        }

    }

    #endregion

    #region Database context factory

    public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            return new MyDbContext();
        }
    }

    #endregion

    #region Fake Database context

    public class FakeMyDbContext : IMyDbContext
    {
        public DbSet<Allcolumntype> Allcolumntypes { get; set; } // allcolumntypes

        public FakeMyDbContext()
        {
            _database = new FakeDatabaseFacade(new MyDbContext());

            Allcolumntypes = new FakeDbSet<Allcolumntype>("Bigint");

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

        public virtual EntityEntry Add(object entity)
        {
            throw new NotImplementedException();
        }

        public virtual EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual Task AddRangeAsync(params object[] entities)
        {
            throw new NotImplementedException();
        }

        public virtual async Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public virtual async ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public virtual async ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public virtual void AddRange(IEnumerable<object> entities)
        {
            throw new NotImplementedException();
        }

        public virtual void AddRange(params object[] entities)
        {
            throw new NotImplementedException();
        }

        public virtual EntityEntry Attach(object entity)
        {
            throw new NotImplementedException();
        }

        public virtual EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual void AttachRange(IEnumerable<object> entities)
        {
            throw new NotImplementedException();
        }

        public virtual void AttachRange(params object[] entities)
        {
            throw new NotImplementedException();
        }

        public virtual EntityEntry Entry(object entity)
        {
            throw new NotImplementedException();
        }

        public virtual EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Find<TEntity>(params object[] keyValues) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual ValueTask<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual ValueTask<object> FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual ValueTask<object> FindAsync(Type entityType, params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public virtual object Find(Type entityType, params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public virtual EntityEntry Remove(object entity)
        {
            throw new NotImplementedException();
        }

        public virtual EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual void RemoveRange(IEnumerable<object> entities)
        {
            throw new NotImplementedException();
        }

        public virtual void RemoveRange(params object[] entities)
        {
            throw new NotImplementedException();
        }

        public virtual EntityEntry Update(object entity)
        {
            throw new NotImplementedException();
        }

        public virtual EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateRange(IEnumerable<object> entities)
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateRange(params object[] entities)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<TResult> FromExpression<TResult> (Expression<Func<IQueryable<TResult>>> expression)
        {
            throw new NotImplementedException();
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
    public class FakeDbSet<TEntity> :
        DbSet<TEntity>,
        IQueryable<TEntity>,
        IAsyncEnumerable<TEntity>,
        IListSource,
        IResettableService
        where TEntity : class
    {
        private readonly PropertyInfo[] _primaryKeys;
        private ObservableCollection<TEntity> _data;
        private IQueryable _query;
        public override IEntityType EntityType { get; }

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

        public override EntityEntry<TEntity> Add(TEntity entity)
        {
            _data.Add(entity);
            return null;
        }

        public override ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return new ValueTask<EntityEntry<TEntity>>(Task<EntityEntry<TEntity>>.Factory.StartNew(() => Add(entity), cancellationToken));
        }

        public override void AddRange(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            foreach (var entity in entities)
                _data.Add(entity);
        }

        public override void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            foreach (var entity in entities)
                _data.Add(entity);
        }

        public override Task AddRangeAsync(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return Task.Factory.StartNew(() => AddRange(entities));
        }

        public override Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return Task.Factory.StartNew(() => AddRange(entities), cancellationToken);
        }

        public override EntityEntry<TEntity> Attach(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            return Add(entity);
        }

        public override void AttachRange(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            AddRange(entities);
        }

        public override void AttachRange(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            AddRange(entities);
        }

        public override EntityEntry<TEntity> Remove(TEntity entity)
        {
            _data.Remove(entity);
            return null;
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

        public override EntityEntry<TEntity> Update(TEntity entity)
        {
            _data.Remove(entity);
            _data.Add(entity);
            return null;
        }

        public override void UpdateRange(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            RemoveRange(entities);
            AddRange(entities);
        }

        public override void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            var array = entities.ToArray();        RemoveRange(array);
            AddRange(array);
        }

        bool IListSource.ContainsListCollection => true;

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
            get { return new FakeDbAsyncQueryProvider<TEntity>(_data); }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public override IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new FakeDbAsyncEnumerator<TEntity>(this.AsEnumerable().GetEnumerator());
        }

        public void ResetState()
        {
            _data  = new ObservableCollection<TEntity>();
            _query = _data.AsQueryable();
        }

        public Task ResetStateAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Factory.StartNew(() => ResetState());
        }
    }

    public class FakeDbAsyncQueryProvider<TEntity> : FakeQueryProvider<TEntity>, IAsyncEnumerable<TEntity>, IAsyncQueryProvider
    {
        public FakeDbAsyncQueryProvider(Expression expression) : base(expression)
        {
        }

        public FakeDbAsyncQueryProvider(IEnumerable<TEntity> enumerable) : base(enumerable)
        {
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            var expectedResultType = typeof(TResult).GetGenericArguments()[0];
            var executionResult = typeof(IQueryProvider)
                .GetMethods()
                .First(method => method.Name == nameof(IQueryProvider.Execute) && method.IsGenericMethod)
                .MakeGenericMethod(expectedResultType)
                .Invoke(this, new object[] { expression });

            return (TResult) typeof(Task).GetMethod(nameof(Task.FromResult))
                ?.MakeGenericMethod(expectedResultType)
                .Invoke(null, new[] { executionResult });
        }

        public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new FakeDbAsyncEnumerator<TEntity>(this.AsEnumerable().GetEnumerator());
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

    public abstract class FakeQueryProvider<T> : IOrderedQueryable<T>, IQueryProvider
    {
        private IEnumerable<T> _enumerable;

        protected FakeQueryProvider(Expression expression)
        {
            Expression = expression;
        }

        protected FakeQueryProvider(IEnumerable<T> enumerable)
        {
            _enumerable = enumerable;
            Expression = enumerable.AsQueryable().Expression;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            if (expression is MethodCallExpression m)
            {
                var resultType = m.Method.ReturnType; // it should be IQueryable<T>
                var tElement = resultType.GetGenericArguments().First();
                return (IQueryable) CreateInstance(tElement, expression);
            }

            return CreateQuery<T>(expression);
        }

        public IQueryable<TEntity> CreateQuery<TEntity>(Expression expression)
        {
            return (IQueryable<TEntity>) CreateInstance(typeof(TEntity), expression);
        }

        private object CreateInstance(Type tElement, Expression expression)
        {
            var queryType = GetType().GetGenericTypeDefinition().MakeGenericType(tElement);
            return Activator.CreateInstance(queryType, expression);
        }

        public object Execute(Expression expression)
        {
            return CompileExpressionItem<object>(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return CompileExpressionItem<TResult>(expression);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            if (_enumerable == null) _enumerable = CompileExpressionItem<IEnumerable<T>>(Expression);
            return _enumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (_enumerable == null) _enumerable = CompileExpressionItem<IEnumerable<T>>(Expression);
            return _enumerable.GetEnumerator();
        }

        public Type ElementType => typeof(T);

        public Expression Expression { get; }

        public IQueryProvider Provider => this;

        private static TResult CompileExpressionItem<TResult>(Expression expression)
        {
            var visitor = new FakeExpressionVisitor();
            var body = visitor.Visit(expression);
            var f = Expression.Lambda<Func<TResult>>(body ?? throw new InvalidOperationException(string.Format("{0} is null", nameof(body))), (IEnumerable<ParameterExpression>) null);
            return f.Compile()();
        }
    }

    public class FakeExpressionVisitor : ExpressionVisitor
    {
    }

    public class FakeDatabaseFacade : DatabaseFacade
    {
        public FakeDatabaseFacade(DbContext context) : base(context)
        {
        }

        public override bool EnsureCreated()
        {
            return true;
        }

        public override Task<bool> EnsureCreatedAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.FromResult(EnsureCreated());
        }

        public override bool EnsureDeleted()
        {
            return true;
        }

        public override Task<bool> EnsureDeletedAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.FromResult(EnsureDeleted());
        }

        public override bool CanConnect()
        {
            return true;
        }

        public override Task<bool> CanConnectAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.FromResult(CanConnect());
        }

        public override IDbContextTransaction BeginTransaction()
        {
            return new FakeDbContextTransaction();
        }

        public override Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.FromResult(BeginTransaction());
        }

        public override void CommitTransaction()
        {
        }

        public override Task CommitTransactionAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.CompletedTask;
        }

        public override void RollbackTransaction()
        {
        }

        public override Task RollbackTransactionAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.CompletedTask;
        }

        public override IExecutionStrategy CreateExecutionStrategy()
        {
            return null;
        }

        public override string ToString()
        {
            return string.Empty;
        }
    }

    public class FakeDbContextTransaction : IDbContextTransaction
    {
        public Guid TransactionId => Guid.NewGuid();
        public void Commit() { }
        public void Rollback() { }
        public Task CommitAsync(CancellationToken cancellationToken = new CancellationToken()) => Task.CompletedTask;
        public Task RollbackAsync(CancellationToken cancellationToken = new CancellationToken()) => Task.CompletedTask;
        public void Dispose() { }
        public ValueTask DisposeAsync() => default;
    }

    #endregion

    #region POCO classes

    // allcolumntypes
    public class Allcolumntype
    {
        public long Bigint { get; set; } // bigint (Primary key)
        public BitArray? Bit1 { get; set; } // bit_1 (length: 1)
        public BitArray? Bit8 { get; set; } // bit_8 (length: 8)
        public bool? Boolean { get; set; } // boolean
        public NpgsqlBox? Box { get; set; } // box
        public byte[] Bytea { get; set; } // bytea
        public string @Char { get; set; } // char (length: 1)
        public string Character { get; set; } // character (length: 1)
        public string CharacterVarying { get; set; } // character_varying
        public uint? Cid { get; set; } // cid
        public NpgsqlInet? Cidr { get; set; } // cidr
        public NpgsqlCircle? Circle { get; set; } // circle
        public DateTime? Date { get; set; } // date
        public double? DoublePrecision { get; set; } // double_precision
        public NpgsqlInet? Inet { get; set; } // inet
        public int? Integer { get; set; } // integer
        public TimeSpan? Interval { get; set; } // interval
        public string Json { get; set; } // json
        public string Jsonb { get; set; } // jsonb
        public NpgsqlLine? Line { get; set; } // line
        public NpgsqlLSeg? Lseg { get; set; } // lseg
        public decimal? Money { get; set; } // money
        public string Name { get; set; } // name
        public decimal? Numeric { get; set; } // numeric
        public uint? Oid { get; set; } // oid
        public string Oidvector { get; set; } // oidvector
        public NpgsqlPath? Path { get; set; } // path
        public NpgsqlPoint? Point { get; set; } // point
        public NpgsqlPolygon? Polygon { get; set; } // polygon
        public float? Real { get; set; } // real
        public short? Smallint { get; set; } // smallint
        public string Text { get; set; } // text
        public DateTimeOffset? TimeWithTimeZone { get; set; } // time_with_time_zone
        public TimeSpan? TimeWithoutTimeZone { get; set; } // time_without_time_zone
        public DateTime? TimestampWithTimeZone { get; set; } // timestamp_with_time_zone
        public DateTime? TimestampWithoutTimeZone { get; set; } // timestamp_without_time_zone
        public Guid? Uuid { get; set; } // uuid
        public uint? Xid { get; set; } // xid
        public string Xml { get; set; } // xml
    }


    #endregion

    #region POCO Configuration

    // allcolumntypes
    public class AllcolumntypeConfiguration : IEntityTypeConfiguration<Allcolumntype>
    {
        public void Configure(EntityTypeBuilder<Allcolumntype> builder)
        {
            builder.ToTable("allcolumntypes", "public");
            builder.HasKey(x => x.Bigint).HasName("pk_allcolumntypes");

            builder.Property(x => x.Bigint).HasColumnName(@"bigint").HasColumnType("bigint").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Bit1).HasColumnName(@"bit_1").HasColumnType("bit(1)").IsRequired(false).HasMaxLength(1);
            builder.Property(x => x.Bit8).HasColumnName(@"bit_8").HasColumnType("bit(8)").IsRequired(false).HasMaxLength(8);
            builder.Property(x => x.Boolean).HasColumnName(@"boolean").HasColumnType("boolean").IsRequired(false);
            builder.Property(x => x.Box).HasColumnName(@"box").HasColumnType("box").IsRequired(false);
            builder.Property(x => x.Bytea).HasColumnName(@"bytea").HasColumnType("bytea").IsRequired(false);
            builder.Property(x => x.@Char).HasColumnName(@"char").HasColumnType("character(1)").IsRequired(false).HasMaxLength(1);
            builder.Property(x => x.Character).HasColumnName(@"character").HasColumnType("character(1)").IsRequired(false).HasMaxLength(1);
            builder.Property(x => x.CharacterVarying).HasColumnName(@"character_varying").HasColumnType("character varying").IsRequired(false);
            builder.Property(x => x.Cid).HasColumnName(@"cid").HasColumnType("cid").IsRequired(false);
            builder.Property(x => x.Cidr).HasColumnName(@"cidr").HasColumnType("cidr").IsRequired(false);
            builder.Property(x => x.Circle).HasColumnName(@"circle").HasColumnType("circle").IsRequired(false);
            builder.Property(x => x.Date).HasColumnName(@"date").HasColumnType("date").IsRequired(false);
            builder.Property(x => x.DoublePrecision).HasColumnName(@"double_precision").HasColumnType("double precision").IsRequired(false);
            builder.Property(x => x.Inet).HasColumnName(@"inet").HasColumnType("inet").IsRequired(false);
            builder.Property(x => x.Integer).HasColumnName(@"integer").HasColumnType("integer").IsRequired(false);
            builder.Property(x => x.Interval).HasColumnName(@"interval").HasColumnType("interval").IsRequired(false);
            builder.Property(x => x.Json).HasColumnName(@"json").HasColumnType("json").IsRequired(false);
            builder.Property(x => x.Jsonb).HasColumnName(@"jsonb").HasColumnType("jsonb").IsRequired(false);
            builder.Property(x => x.Line).HasColumnName(@"line").HasColumnType("line").IsRequired(false);
            builder.Property(x => x.Lseg).HasColumnName(@"lseg").HasColumnType("lseg").IsRequired(false);
            builder.Property(x => x.Money).HasColumnName(@"money").HasColumnType("money").IsRequired(false);
            builder.Property(x => x.Name).HasColumnName(@"name").HasColumnType("name").IsRequired(false);
            builder.Property(x => x.Numeric).HasColumnName(@"numeric").HasColumnType("numeric").IsRequired(false);
            builder.Property(x => x.Oid).HasColumnName(@"oid").HasColumnType("oid").IsRequired(false);
            builder.Property(x => x.Oidvector).HasColumnName(@"oidvector").HasColumnType("array").IsRequired(false);
            builder.Property(x => x.Path).HasColumnName(@"path").HasColumnType("path").IsRequired(false);
            builder.Property(x => x.Point).HasColumnName(@"point").HasColumnType("point").IsRequired(false);
            builder.Property(x => x.Polygon).HasColumnName(@"polygon").HasColumnType("polygon").IsRequired(false);
            builder.Property(x => x.Real).HasColumnName(@"real").HasColumnType("real").IsRequired(false);
            builder.Property(x => x.Smallint).HasColumnName(@"smallint").HasColumnType("smallint").IsRequired(false);
            builder.Property(x => x.Text).HasColumnName(@"text").HasColumnType("text").IsRequired(false).IsUnicode(false);
            builder.Property(x => x.TimeWithTimeZone).HasColumnName(@"time_with_time_zone").HasColumnType("time with time zone").IsRequired(false);
            builder.Property(x => x.TimeWithoutTimeZone).HasColumnName(@"time_without_time_zone").HasColumnType("time without time zone").IsRequired(false);
            builder.Property(x => x.TimestampWithTimeZone).HasColumnName(@"timestamp_with_time_zone").HasColumnType("timestamp with time zone").IsRequired(false);
            builder.Property(x => x.TimestampWithoutTimeZone).HasColumnName(@"timestamp_without_time_zone").HasColumnType("timestamp without time zone").IsRequired(false);
            builder.Property(x => x.Uuid).HasColumnName(@"uuid").HasColumnType("uuid").IsRequired(false);
            builder.Property(x => x.Xid).HasColumnName(@"xid").HasColumnType("xid").IsRequired(false);
            builder.Property(x => x.Xml).HasColumnName(@"xml").HasColumnType("xml").IsRequired(false);
        }
    }


    #endregion

}
// </auto-generated>
