// <auto-generated>

using Generator.Tests.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Tester.Integration.EFCore8.Multi_context_single_filesAppleDbContext
{
    #region Database context interface

    public interface IAppleDbContext : IDisposable
    {
        DbSet<Stafford_Boo> Stafford_Boos { get; set; } // Boo
        DbSet<Stafford_Foo> Stafford_Foos { get; set; } // Foo

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

    public class AppleDbContext : DbContext, IAppleDbContext
    {
        public AppleDbContext()
        {
        }

        public AppleDbContext(DbContextOptions<AppleDbContext> options)
            : base(options)
        {
        }

        public DbSet<Stafford_Boo> Stafford_Boos { get; set; } // Boo
        public DbSet<Stafford_Foo> Stafford_Foos { get; set; } // Foo

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

            modelBuilder.ApplyConfiguration(new Stafford_BooConfiguration());
            modelBuilder.ApplyConfiguration(new Stafford_FooConfiguration());
        }

    }

    #endregion

    #region Database context factory

    public class AppleDbContextFactory : IDesignTimeDbContextFactory<AppleDbContext>
    {
        public AppleDbContext CreateDbContext(string[] args)
        {
            return new AppleDbContext();
        }
    }

    #endregion

    #region POCO classes

    // Boo
    [CustomSecurity(SecurityEnum.Readonly)]
    public class Stafford_Boo
    {
        public int id { get; set; } // id (Primary key)
        public string Name { get; set; } // name (length: 10)

        // Reverse navigation

        /// <summary>
        /// Parent (One-to-One) Stafford_Boo pointed by [Foo].[id] (FK_Foo_Boo)
        /// </summary>
        public virtual Stafford_Foo Stafford_Foo { get; set; } // Foo.FK_Foo_Boo
    }

    // Foo
    public class Stafford_Foo
    {
        public int id { get; set; } // id (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent Stafford_Boo pointed by [Foo].([id]) (FK_Foo_Boo)
        /// </summary>
        public virtual Stafford_Boo Stafford_Boo { get; set; } // FK_Foo_Boo
    }


    #endregion

    #region POCO Configuration

    // Boo
    public class Stafford_BooConfiguration : IEntityTypeConfiguration<Stafford_Boo>
    {
        public void Configure(EntityTypeBuilder<Stafford_Boo> builder)
        {
            builder.ToTable("Boo", "Stafford");
            builder.HasKey(x => x.id).HasName("PK_Boo").IsClustered();

            builder.Property(x => x.id).HasColumnName(@"id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Name).HasColumnName(@"name").HasColumnType("nchar(10)").IsRequired().IsFixedLength().HasMaxLength(10);
        }
    }

    // Foo
    public class Stafford_FooConfiguration : IEntityTypeConfiguration<Stafford_Foo>
    {
        public void Configure(EntityTypeBuilder<Stafford_Foo> builder)
        {
            builder.ToTable("Foo", "Stafford");
            builder.HasKey(x => x.id).HasName("PK_Foo").IsClustered();

            builder.Property(x => x.id).HasColumnName(@"id").HasColumnType("int").IsRequired().ValueGeneratedNever();

            // Foreign keys
            builder.HasOne(a => a.Stafford_Boo).WithOne(b => b.Stafford_Foo).HasForeignKey<Stafford_Foo>(c => c.id).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Foo_Boo");
        }
    }


    #endregion

}
// </auto-generated>
