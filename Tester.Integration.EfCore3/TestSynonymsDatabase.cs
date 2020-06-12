﻿// <auto-generated>

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query.Internal;
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

namespace TestSynonymsDatabase
{
    #region Database context interface

    public interface ITestDbContext : IDisposable
    {
        DbSet<Child> Children { get; set; } // Child
        DbSet<Parent> Parents { get; set; } // Parent

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
        DatabaseFacade Database { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        string ToString();

        // Stored Procedures
        List<SimpleStoredProcReturnModel> SimpleStoredProc(int? inputInt);
        List<SimpleStoredProcReturnModel> SimpleStoredProc(int? inputInt, out int procResult);
        Task<List<SimpleStoredProcReturnModel>> SimpleStoredProcAsync(int? inputInt);


        // Table Valued Functions
        IQueryable<CsvToIntReturnModel> CsvToInt(string array, string array2); // dbo.CsvToInt
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

        public DbSet<Child> Children { get; set; } // Child
        public DbSet<Parent> Parents { get; set; } // Parent

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(local);Initial Catalog=EfrpgTest_Synonyms;Integrated Security=True");
            }
        }

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

            modelBuilder.ApplyConfiguration(new ChildConfiguration());
            modelBuilder.ApplyConfiguration(new ParentConfiguration());

            modelBuilder.Entity<SimpleStoredProcReturnModel>().HasNoKey();

            // Table Valued Functions
            modelBuilder.Entity<CsvToIntReturnModel>().HasNoKey();
        }


        // Stored Procedures
        public List<SimpleStoredProcReturnModel> SimpleStoredProc(int? inputInt)
        {
            int procResult;
            return SimpleStoredProc(inputInt, out procResult);
        }

        public List<SimpleStoredProcReturnModel> SimpleStoredProc(int? inputInt, out int procResult)
        {
            var inputIntParam = new SqlParameter { ParameterName = "@InputInt", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = inputInt.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!inputInt.HasValue)
                inputIntParam.Value = DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            const string sqlCommand = "EXEC @procResult = [Synonyms].[SimpleStoredProc] @InputInt";
            var procResultData = Set<SimpleStoredProcReturnModel>()
                .FromSqlRaw(sqlCommand, inputIntParam, procResultParam)
                .ToList();

            procResult = (int) procResultParam.Value;
            return procResultData;
        }

        public async Task<List<SimpleStoredProcReturnModel>> SimpleStoredProcAsync(int? inputInt)
        {
            var inputIntParam = new SqlParameter { ParameterName = "@InputInt", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = inputInt.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!inputInt.HasValue)
                inputIntParam.Value = DBNull.Value;

            const string sqlCommand = "EXEC [Synonyms].[SimpleStoredProc] @InputInt";
            var procResultData = await Set<SimpleStoredProcReturnModel>()
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
        public DbSet<Child> Children { get; set; } // Child
        public DbSet<Parent> Parents { get; set; } // Parent

        public FakeTestDbContext()
        {
            _database = null;

            Children = new FakeDbSet<Child>("ChildId");
            Parents = new FakeDbSet<Parent>("ParentId");

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

        public DbSet<SimpleStoredProcReturnModel> SimpleStoredProcReturnModel { get; set; }
        public List<SimpleStoredProcReturnModel> SimpleStoredProc(int? inputInt)
        {
            int procResult;
            return SimpleStoredProc(inputInt, out procResult);
        }

        public List<SimpleStoredProcReturnModel> SimpleStoredProc(int? inputInt, out int procResult)
        {
            procResult = 0;
            return new List<SimpleStoredProcReturnModel>();
        }

        public Task<List<SimpleStoredProcReturnModel>> SimpleStoredProcAsync(int? inputInt)
        {
            int procResult;
            return Task.FromResult(SimpleStoredProc(inputInt, out procResult));
        }

        // Table Valued Functions

        // dbo.CsvToInt
        public IQueryable<CsvToIntReturnModel> CsvToInt(string array, string array2)
        {
            return new List<CsvToIntReturnModel>().AsQueryable();
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

    // Child
    public class Child
    {
        public int ChildId { get; set; } // ChildId (Primary key)
        public int ParentId { get; set; } // ParentId
        public string ChildName { get; set; } // ChildName (length: 100)

        // Foreign keys

        /// <summary>
        /// Parent Parent pointed by [Child].([ParentId]) (FK_Child_Parent)
        /// </summary>
        public virtual Parent Parent { get; set; } // FK_Child_Parent
    }

    // Parent
    public class Parent
    {
        public int ParentId { get; set; } // ParentId (Primary key)
        public string ParentName { get; set; } // ParentName (length: 100)

        // Reverse navigation

        /// <summary>
        /// Child Children where [Child].[ParentId] point to this entity (FK_Child_Parent)
        /// </summary>
        public virtual ICollection<Child> Children { get; set; } // Child.FK_Child_Parent

        public Parent()
        {
            Children = new List<Child>();
        }
    }


    #endregion

    #region POCO Configuration

    // Child
    public class ChildConfiguration : IEntityTypeConfiguration<Child>
    {
        public void Configure(EntityTypeBuilder<Child> builder)
        {
            builder.ToTable("Child", "Synonyms");
            builder.HasKey(x => x.ChildId);

            builder.Property(x => x.ChildId).HasColumnName(@"ChildId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.ParentId).HasColumnName(@"ParentId").HasColumnType("int").IsRequired();
            builder.Property(x => x.ChildName).HasColumnName(@"ChildName").HasColumnType("varchar(100)").IsRequired(false).IsUnicode(false).HasMaxLength(100);

            // Foreign keys
            builder.HasOne(a => a.Parent).WithMany(b => b.Children).HasForeignKey(c => c.ParentId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Child_Parent");
        }
    }

    // Parent
    public class ParentConfiguration : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder.ToTable("Parent", "Synonyms");
            builder.HasKey(x => x.ParentId);

            builder.Property(x => x.ParentId).HasColumnName(@"ParentId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.ParentName).HasColumnName(@"ParentName").HasColumnType("varchar(100)").IsRequired().IsUnicode(false).HasMaxLength(100);
        }
    }


    #endregion

    #region Stored procedure return models

    public class CsvToIntReturnModel
    {
        public int? IntValue { get; set; }
    }

    public class SimpleStoredProcReturnModel
    {
        public string ReturnValue { get; set; }
    }


    #endregion

}
// </auto-generated>
