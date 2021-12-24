namespace Tester.Integration.EfCore2
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
        private FakeDbSet<AppUser> _dbSet;
        private List<AppUser> _list;

        [SetUp]
        public void SetUp()
        {
            _dbSet = new FakeDbSet<AppUser>();

            _list = new List<AppUser>
            {
                new AppUser { Id = 1, Name = "Simon" },
                new AppUser { Id = 2, Name = "Fred" },
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