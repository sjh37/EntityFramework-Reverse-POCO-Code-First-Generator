namespace Tester.Integration.EfCore2
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using Generator.Tests.Common;

    [TestFixture]
    [Category(Constants.CI)]
    public class FakeContextTests
    {
        private FakeMyDbContext _fakeContext;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _fakeContext = new FakeMyDbContext { Categories = { new Category { CategoryId = 123, CategoryName = "Flowers" } } };
        }

        [Test]
        public void FirstOrDefault()
        {
            var result = _fakeContext.Categories.FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.AreEqual(123, result.CategoryId);
        }

        [Test]
        public async Task FirstOrDefaultAsync()
        {
            var result = await _fakeContext.Categories.FirstOrDefaultAsync();
            Assert.IsNotNull(result);
            Assert.AreEqual(123, result.CategoryId);
        }

        [Test]
        public void QueryFirstOrDefault()
        {
            var query = from w in _fakeContext.Categories select new { QueriedId = w.CategoryId };
            var result = query.FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.AreEqual(123, result.QueriedId);
        }
    }
}