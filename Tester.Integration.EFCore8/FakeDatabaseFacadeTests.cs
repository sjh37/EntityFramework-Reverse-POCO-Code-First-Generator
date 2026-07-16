using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Generator.Tests.Common;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Tester.Integration.EFCore8.Northwind;

namespace Tester.Integration.EFCore8
{
    /// <summary>
    ///     The relational Database operations (ExecuteSqlRaw, SqlQueryRaw, Migrate, ...) are extension methods on
    ///     DatabaseFacade, so they are not virtual and FakeDatabaseFacade cannot override them. They reach the database
    ///     through IDatabaseFacadeDependenciesAccessor.Dependencies or IInfrastructure&lt;IServiceProvider&gt;.Instance,
    ///     both of which FakeDatabaseFacade re-implements to throw. Without that, these calls fall through to the real
    ///     DbContext the fake wraps and run against the real database the connection string points at.
    /// </summary>
    [TestFixture]
    [Category(Constants.CI)]
    public class FakeDatabaseFacadeTests
    {
        private FakeNorthwindDbContext _fake = null!;

        [SetUp]
        public void SetUp()
        {
            _fake = new FakeNorthwindDbContext();
        }

        [Test]
        public void ExecuteSqlRawDoesNotReachTheRealDatabase()
        {
            Assert.Throws<NotSupportedException>(() => _fake.Database.ExecuteSqlRaw("DELETE FROM Customers"));
        }

        [Test]
        public void ExecuteSqlInterpolatedDoesNotReachTheRealDatabase()
        {
            Assert.Throws<NotSupportedException>(() => _fake.Database.ExecuteSqlInterpolated($"DELETE FROM Customers"));
        }

        [Test]
        public void ExecuteSqlRawAsyncDoesNotReachTheRealDatabase()
        {
            Assert.ThrowsAsync<NotSupportedException>(() => _fake.Database.ExecuteSqlRawAsync("DELETE FROM Customers"));
        }

        [Test]
        public void SqlQueryRawDoesNotReachTheRealDatabase()
        {
            Assert.Throws<NotSupportedException>(() => _fake.Database.SqlQueryRaw<int>("SELECT 1").ToList());
        }

        [Test]
        public void GetDbConnectionDoesNotReachTheRealDatabase()
        {
            Assert.Throws<NotSupportedException>(() => _fake.Database.GetDbConnection());
        }

        [Test]
        public void OpenConnectionDoesNotReachTheRealDatabase()
        {
            Assert.Throws<NotSupportedException>(() => _fake.Database.OpenConnection());
        }

        [Test]
        public void BeginTransactionWithIsolationLevelDoesNotReachTheRealDatabase()
        {
            Assert.Throws<NotSupportedException>(() => _fake.Database.BeginTransaction(IsolationLevel.ReadCommitted));
        }

        // Migrate() and the rest of the migration family resolve via IInfrastructure<IServiceProvider>.Instance
        // rather than via Dependencies, so they need blocking separately.
        [Test]
        public void MigrateDoesNotReachTheRealDatabase()
        {
            Assert.Throws<NotSupportedException>(() => _fake.Database.Migrate());
        }

        [Test]
        public void GetPendingMigrationsDoesNotReachTheRealDatabase()
        {
            Assert.Throws<NotSupportedException>(() => _fake.Database.GetPendingMigrations().ToList());
        }

        [Test]
        public void GenerateCreateScriptDoesNotReachTheRealDatabase()
        {
            Assert.Throws<NotSupportedException>(() => _fake.Database.GenerateCreateScript());
        }

        [Test]
        public void CanConnectAndEnsureCreatedStillSucceed()
        {
            Assert.IsTrue(_fake.Database.CanConnect());
            Assert.IsTrue(_fake.Database.EnsureCreated());
            Assert.IsTrue(_fake.Database.EnsureDeleted());
        }

        [Test]
        public void ProviderNameIsReportedSoProviderChecksStillWork()
        {
            Assert.AreEqual("Microsoft.EntityFrameworkCore.SqlServer", _fake.Database.ProviderName);
            Assert.IsTrue(_fake.Database.IsSqlServer());
        }

        [Test]
        public void CurrentTransactionTracksBeginCommitAndDispose()
        {
            Assert.IsNull(_fake.Database.CurrentTransaction);

            var transaction = _fake.Database.BeginTransaction();
            Assert.IsNotNull(_fake.Database.CurrentTransaction);
            Assert.AreSame(transaction, _fake.Database.CurrentTransaction);

            _fake.Database.CommitTransaction();
            Assert.IsNull(_fake.Database.CurrentTransaction);
        }

        [Test]
        public void DisposingATransactionClearsCurrentTransaction()
        {
            using (_fake.Database.BeginTransaction())
            {
                Assert.IsNotNull(_fake.Database.CurrentTransaction);
            }

            Assert.IsNull(_fake.Database.CurrentTransaction);
        }

        [Test]
        public void TransactionIdIsStableAcrossReads()
        {
            var transaction = _fake.Database.BeginTransaction();
            Assert.AreEqual(transaction.TransactionId, transaction.TransactionId);
        }

        [Test]
        public void CreateExecutionStrategyRunsTheOperation()
        {
            var strategy = _fake.Database.CreateExecutionStrategy();
            Assert.IsNotNull(strategy);
            Assert.AreEqual(42, strategy.Execute(() => 42));
        }

        [Test]
        public async Task CreateExecutionStrategyRunsTheAsyncOperation()
        {
            var strategy = _fake.Database.CreateExecutionStrategy();
            var result = await strategy.ExecuteAsync(async () =>
            {
                await Task.Yield();
                return 42;
            });
            Assert.AreEqual(42, result);
        }

        [Test]
        public void SetReturnsTheSameInstanceAsTheGeneratedDbSetProperty()
        {
            var set = _fake.Set<Customer>();
            Assert.IsNotNull(set);

            set.Add(new Customer { CustomerId = "ALFKI" });
            Assert.AreEqual(1, _fake.Customers.Count());
        }

        [Test]
        public void SetForAnEntityNotInTheContextThrows()
        {
            Assert.Throws<InvalidOperationException>(() => _fake.Set<NotInThisContext>());
        }

        [Test]
        public void ToStringDoesNotThrow()
        {
            Assert.AreEqual("FakeNorthwindDbContext", _fake.ToString());
        }

        [Test]
        public async Task SaveChangesCountIncludesTheAsyncOverloads()
        {
            _fake.SaveChanges();
            await _fake.SaveChangesAsync(default);
            await _fake.SaveChangesAsync(true, default);

            Assert.AreEqual(3, _fake.SaveChangesCount);
        }

        private class NotInThisContext
        {
        }
    }
}
