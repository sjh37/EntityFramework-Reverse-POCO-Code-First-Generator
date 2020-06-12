
// <auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tester.Integration.EfCore3.Single_context_many_files
{
    public interface IEfCoreDbContext : IDisposable
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
}
// </auto-generated>
