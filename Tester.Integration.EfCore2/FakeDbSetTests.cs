namespace Tester.Integration.EfCore2
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using NUnit.Framework;

    [TestFixture]
    [Category(Constants.CI)]
    public class FakeDbSetTests
    {
        private FakeDbSet<Category> _dbSet;
        private List<Category> _list;

        [SetUp]
        public void SetUp()
        {
            _dbSet = new FakeDbSet<Category>();

            _list = new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Flowers" },
                new Category { CategoryId = 2, CategoryName = "Cars" },
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