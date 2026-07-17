using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Generator.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;
using Tester.Integration.EFCore8.Northwind;

namespace Tester.Integration.EFCore8
{
    /// <summary>
    ///     The relational Database operations (ExecuteSqlRaw, SqlQueryRaw, Migrate, ...) are extension methods on
    ///     DatabaseFacade, so they are not virtual and FakeDatabaseFacade cannot override them. They reach the database
    ///     through IDatabaseFacadeDependenciesAccessor.Dependencies or IInfrastructure&lt;IServiceProvider&gt;.Instance,
    ///     both of which FakeDatabaseFacade re-implements to throw, and the DbContext underneath is an inert shim with
    ///     no provider. Without all three, these calls run against the real database the connection string points at.
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

        [TearDown]
        public void TearDown()
        {
            _fake.Dispose();
        }

        // ---------------------------------------------------------------- Nothing reaches a real database

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

        // The third route: IsRelational() and GetService<T>() do not go through Dependencies at all. They ask
        // IDatabaseFacadeDependenciesAccessor.Context for its DbContext and then for that context's service provider.
        // Before the shim, that Context was the real, connection-string-bound DbContext.
        [Test]
        public void TheWrappedDbContextIsNotHandedOut()
        {
            Assert.Throws<NotSupportedException>(() => _ = ((IDatabaseFacadeDependenciesAccessor) _fake.Database).Context);
        }

        // The last line of defence. Even if some future EF Core route slips past all three guards, the DbContext it
        // would reach has no provider and no connection string, so it cannot execute anything.
        [Test]
        public void TheInertShimHasNoProviderConfigured()
        {
            using (var shim = new FakeDbContextShim())
            {
                Assert.IsNotInstanceOf<NorthwindDbContext>(shim);
                // No provider means EF Core itself refuses to do anything, rather than quietly connecting.
                Assert.Throws<InvalidOperationException>(() => _ = shim.Model);
            }
        }

        // IsRelational() and Get/SetCommandTimeout() issue no SQL themselves, but are only reachable through routes
        // that must be blocked. Returning a value would mean implementing all of IDatabaseFacadeDependencies, whose
        // members are EF Core internals that shift between releases and would break this generated file on every EF
        // Core upgrade. This test pins the trade-off so it stays a deliberate decision rather than an accident.
        [Test]
        public void IsRelationalThrowsAndSaysWhy()
        {
            var ex = Assert.Throws<NotSupportedException>(() => _fake.Database.IsRelational());

            Assert.That(ex!.Message, Does.Contain("IsRelational()"));
            Assert.That(ex.Message, Does.Contain("Fake DbContext"));
        }

        // ---------------------------------------------------------------- Things that should still work

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

        // GetType().Name rather than a hardcoded literal, so a user's derived context reports its own name.
        [Test]
        public void ToStringReportsTheRuntimeTypeName()
        {
            Assert.AreEqual("FakeNorthwindDbContext", _fake.ToString());

            using (var derived = new ExtraSetContext())
            {
                Assert.AreEqual("ExtraSetContext", derived.ToString());
            }
        }

        // The end-to-end shape of a business-logic unit test: seed the fake with good and edge-case rows, run the
        // logic through LINQ (sync and async), Find by primary key, a transaction and SaveChangesAsync - no database
        // anywhere.
        [Test]
        public async Task SeededFakeSupportsTheFullBusinessLogicWorkflow()
        {
            _fake.Customers.AddRange(
                new Customer { CustomerId = "ALFKI", CompanyName = "Alfreds Futterkiste" },
                new Customer { CustomerId = "ANATR", CompanyName = "Ana Trujillo" },
                new Customer { CustomerId = "ZZZZZ", CompanyName = "" }); // edge case: empty name

            using (var transaction = await _fake.Database.BeginTransactionAsync())
            {
                var found = _fake.Customers.Find("ALFKI");
                Assert.IsNotNull(found);

                var ordered = await _fake.Customers
                    .Where(x => x.CompanyName != "")
                    .OrderBy(x => x.CustomerId)
                    .ToListAsync();
                Assert.AreEqual(2, ordered.Count);
                Assert.AreEqual("ALFKI", ordered[0].CustomerId);

                var missing = await _fake.Customers.FirstOrDefaultAsync(x => x.CustomerId == "NOPE");
                Assert.IsNull(missing);

                _fake.Customers.Remove(found!);
                await _fake.SaveChangesAsync(default);
                await transaction.CommitAsync();
            }

            Assert.AreEqual(2, _fake.Customers.Count());
            Assert.AreEqual(1, _fake.SaveChangesCount);
            Assert.IsNull(_fake.Database.CurrentTransaction);
        }

        // ---------------------------------------------------------------- Transactions

        [Test]
        public void CurrentTransactionTracksBeginAndCommitViaTheDatabase()
        {
            Assert.IsNull(_fake.Database.CurrentTransaction);

            var transaction = _fake.Database.BeginTransaction();
            Assert.IsNotNull(_fake.Database.CurrentTransaction);
            Assert.AreSame(transaction, _fake.Database.CurrentTransaction);

            _fake.Database.CommitTransaction();
            Assert.IsNull(_fake.Database.CurrentTransaction);
        }

        [Test]
        public void RollbackTransactionClearsCurrentTransaction()
        {
            _fake.Database.BeginTransaction();
            _fake.Database.RollbackTransaction();

            Assert.IsNull(_fake.Database.CurrentTransaction);
        }

        // Committing on the transaction object is the idiomatic EF pattern, and clears CurrentTransaction in real EF.
        [Test]
        public void CommittingOnTheTransactionItselfClearsCurrentTransaction()
        {
            var transaction = _fake.Database.BeginTransaction();
            transaction.Commit();

            Assert.IsNull(_fake.Database.CurrentTransaction);
        }

        [Test]
        public void RollingBackOnTheTransactionItselfClearsCurrentTransaction()
        {
            var transaction = _fake.Database.BeginTransaction();
            transaction.Rollback();

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
        public async Task BeginTransactionAsyncSetsCurrentTransaction()
        {
            var transaction = await _fake.Database.BeginTransactionAsync();

            Assert.AreSame(transaction, _fake.Database.CurrentTransaction);

            await _fake.Database.CommitTransactionAsync();
            Assert.IsNull(_fake.Database.CurrentTransaction);
        }

        [Test]
        public async Task CommittingAsyncOnTheTransactionItselfClearsCurrentTransaction()
        {
            var transaction = await _fake.Database.BeginTransactionAsync();
            await transaction.CommitAsync();

            Assert.IsNull(_fake.Database.CurrentTransaction);
        }

        [Test]
        public async Task DisposeAsyncOnATransactionClearsCurrentTransaction()
        {
            var transaction = await _fake.Database.BeginTransactionAsync();
            await transaction.DisposeAsync();

            Assert.IsNull(_fake.Database.CurrentTransaction);
        }

        // Real EF Core does not support nested transactions and throws rather than silently orphaning the first one.
        [Test]
        public void BeginningASecondTransactionWhileOneIsActiveThrows()
        {
            _fake.Database.BeginTransaction();

            var ex = Assert.Throws<InvalidOperationException>(() => _fake.Database.BeginTransaction());
            Assert.That(ex!.Message, Does.Contain("already in a transaction"));
        }

        // Disposing a transaction that has already been superseded must not clear the live one.
        [Test]
        public void DisposingAStaleTransactionDoesNotClearANewerOne()
        {
            var first = _fake.Database.BeginTransaction();
            _fake.Database.CommitTransaction();

            var second = _fake.Database.BeginTransaction();
            first.Dispose();

            Assert.IsNotNull(_fake.Database.CurrentTransaction);
            Assert.AreSame(second, _fake.Database.CurrentTransaction);
        }

        [Test]
        public void TransactionIdIsStableAcrossReads()
        {
            using (var transaction = _fake.Database.BeginTransaction())
            {
                Assert.AreEqual(transaction.TransactionId, transaction.TransactionId);
            }
        }

        [Test]
        public void SeparateTransactionsGetSeparateIds()
        {
            Guid first;
            using (var transaction = _fake.Database.BeginTransaction())
            {
                first = transaction.TransactionId;
            }

            using (var transaction = _fake.Database.BeginTransaction())
            {
                Assert.AreNotEqual(first, transaction.TransactionId);
            }
        }

        // ---------------------------------------------------------------- Set<TEntity>()

        [Test]
        public void SetReturnsTheSameInstanceAsTheGeneratedDbSetProperty()
        {
            var set = _fake.Set<Customer>();
            Assert.IsNotNull(set);
            Assert.AreSame(_fake.Customers, set);

            set.Add(new Customer { CustomerId = "ALFKI" });
            Assert.AreEqual(1, _fake.Customers.Count());
        }

        [Test]
        public void SetIsStableAcrossRepeatedCalls()
        {
            Assert.AreSame(_fake.Set<Customer>(), _fake.Set<Customer>());
        }

        // The property lookup is cached statically, so it must stay correct across separate instances.
        [Test]
        public void SetWorksOnASecondInstance()
        {
            using (var other = new FakeNorthwindDbContext())
            {
                Assert.AreSame(other.Customers, other.Set<Customer>());
            }
        }

        [Test]
        public void SetForAnEntityNotInTheContextThrows()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _fake.Set<NotInThisContext>());
            Assert.That(ex!.Message, Does.Contain("NotInThisContext"));
        }

        // The property cache is static, so it is shared with every class deriving from the generated fake. Keying it
        // on the entity type alone would let the miss below be reused for the derived context, which does have a
        // DbSet for that entity.
        [Test]
        public void SetIsCachedPerContextTypeAndNotJustPerEntityType()
        {
            Assert.Throws<InvalidOperationException>(() => _fake.Set<OnlyOnTheDerivedContext>());

            using (var derived = new ExtraSetContext())
            {
                Assert.AreSame(derived.Extras, derived.Set<OnlyOnTheDerivedContext>());
            }
        }

        // ---------------------------------------------------------------- SaveChanges

        [Test]
        public async Task SaveChangesCountIncludesTheAsyncOverloads()
        {
            _fake.SaveChanges();
            await _fake.SaveChangesAsync(default);
            await _fake.SaveChangesAsync(true, default);

            Assert.AreEqual(3, _fake.SaveChangesCount);
        }

        // SaveChangesAsync(bool, ct) must go through SaveChanges(bool), so overriding it is honoured on both paths.
        [Test]
        public async Task SaveChangesAsyncForwardsAcceptAllChangesOnSuccess()
        {
            using (var fake = new AcceptAllChangesRecordingContext())
            {
                await fake.SaveChangesAsync(false, default);
                Assert.AreEqual(false, fake.LastAcceptAllChangesOnSuccess);

                await fake.SaveChangesAsync(true, default);
                Assert.AreEqual(true, fake.LastAcceptAllChangesOnSuccess);
            }
        }

        [Test]
        public void SaveChangesAsyncHonoursACancelledToken()
        {
            using (var cts = new CancellationTokenSource())
            {
                cts.Cancel();

                Assert.ThrowsAsync<TaskCanceledException>(() => _fake.SaveChangesAsync(cts.Token));
                Assert.ThrowsAsync<TaskCanceledException>(() => _fake.SaveChangesAsync(true, cts.Token));
                Assert.AreEqual(0, _fake.SaveChangesCount);
            }
        }

        // Real EF honours the token on every async facade method, so business logic cancellation paths test the
        // same way against the fake as against the real context.
        [Test]
        public void FacadeAsyncMethodsHonourACancelledToken()
        {
            using (var cts = new CancellationTokenSource())
            {
                cts.Cancel();

                Assert.ThrowsAsync<TaskCanceledException>(() => _fake.Database.EnsureCreatedAsync(cts.Token));
                Assert.ThrowsAsync<TaskCanceledException>(() => _fake.Database.EnsureDeletedAsync(cts.Token));
                Assert.ThrowsAsync<TaskCanceledException>(() => _fake.Database.CanConnectAsync(cts.Token));
                Assert.ThrowsAsync<TaskCanceledException>(() => _fake.Database.BeginTransactionAsync(cts.Token));

                _fake.Database.BeginTransaction();
                Assert.ThrowsAsync<TaskCanceledException>(() => _fake.Database.CommitTransactionAsync(cts.Token));
                Assert.ThrowsAsync<TaskCanceledException>(() => _fake.Database.RollbackTransactionAsync(cts.Token));

                // The cancelled commit/rollback must not have cleared the still-live transaction.
                Assert.IsNotNull(_fake.Database.CurrentTransaction);
            }
        }

        private class AcceptAllChangesRecordingContext : FakeNorthwindDbContext
        {
            public bool? LastAcceptAllChangesOnSuccess { get; private set; }

            public override int SaveChanges(bool acceptAllChangesOnSuccess)
            {
                LastAcceptAllChangesOnSuccess = acceptAllChangesOnSuccess;
                return base.SaveChanges(acceptAllChangesOnSuccess);
            }
        }

        private class NotInThisContext
        {
        }

        public class OnlyOnTheDerivedContext
        {
        }

        public class ExtraSetContext : FakeNorthwindDbContext
        {
            public DbSet<OnlyOnTheDerivedContext> Extras { get; set; } = new FakeDbSet<OnlyOnTheDerivedContext>();
        }
    }
}
