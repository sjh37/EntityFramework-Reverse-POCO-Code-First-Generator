namespace Tester.Integration.EFCore6
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Generator.Tests.Common;
    using NUnit.Framework;
    using V6Fred;

    [TestFixture]
    [Category(Constants.CI)]
    public class FakeDbSetTests
    {
        private FakeDbSet<A> _dbSet = null!;
        private List<A> _list = null!;

        [SetUp]
        public void SetUp()
        {
            _dbSet = new FakeDbSet<A>("AId");

            _list = new List<A>
            {
                new() { AId = 1, C1 = 2, C2 = 3 },
                new() { AId = 4, C1 = 5, C2 = 6 }
            };
        }

        [Test]
        [TestCase(1, 2, 3)]
        [TestCase(4, 5, 6)]
        public void Find(int id, int c1, int c2)
        {
            var result = _dbSet.Find(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.AId);
            Assert.AreEqual(c1, result.C1);
            Assert.AreEqual(c2, result.C2);
        }

        [Test]
        [TestCase(1, 2, 3)]
        [TestCase(4, 5, 6)]
        public async Task FindAsync_CancellationToken(int id, int c1, int c2)
        {
            var result = await _dbSet.FindAsync(id, new CancellationToken());

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.AId);
            Assert.AreEqual(c1, result.C1);
            Assert.AreEqual(c2, result.C2);
        }
        
        [Test]
        [TestCase(1, 2, 3)]
        [TestCase(4, 5, 6)]
        public async Task FindAsync(int id, int c1, int c2)
        {
            var result = await _dbSet.FindAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.AId);
            Assert.AreEqual(c1, result.C1);
            Assert.AreEqual(c2, result.C2);
        }

        [Test]
        public void AddRange()
        {
            _dbSet.AddRange(_list);
            Assert.AreEqual(2, _dbSet.Count());
        }

        [Test]
        public async Task AddRangeAsync()
        {
            await _dbSet.AddRangeAsync(_list);
            Assert.AreEqual(2, _dbSet.Count());
        }
    }
}