namespace Tester.Integration.EfCore3
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using Generator.Tests.Common;

    [TestFixture]
    [Category(Constants.CI)]
    public class FakeDbSetTests
    {
        private FakeDbSet<A> _dbSet;
        private List<A> _list;

        [SetUp]
        public void SetUp()
        {
            _dbSet = new FakeDbSet<A>();

            _list = new List<A>
            {
                new A { AId = 1, C1 = 2, C2 = 3 },
                new A { AId = 4, C1 = 5, C2 = 6 }
            };
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