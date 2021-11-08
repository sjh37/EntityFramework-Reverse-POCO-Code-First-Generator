namespace Tester.Integration.Ef6
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using NUnit.Framework;

    [TestFixture]
    [Category(Constants.CI)]
    public class FakeContextTests
    {
        private FakeMyDbContext SUT;

        [SetUp]
        public void BeforeEach()
        {
            SUT = new FakeMyDbContext { Categories = { new Category { CategoryId = 123, CategoryName = "Flowers" } } };
        }

        [Test]
        public void FirstOrDefault()
        {
            var result = SUT.Categories.FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.AreEqual(123, result.CategoryId);
        }

        [Test]
        public async Task FirstOrDefaultAsync()
        {
            var result = await SUT.Categories.FirstOrDefaultAsync();
            Assert.IsNotNull(result);
            Assert.AreEqual(123, result.CategoryId);
        }

        [Test]
        public void QueryFirstOrDefault()
        {
            var query = from w in SUT.Categories select new { QueriedId = w.CategoryId };
            var result = query.FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.AreEqual(123, result.QueriedId);
        }

        [Test]
        public async Task QueryFirstOrDefaultAsync()
        {
            var query = from w in SUT.Categories select new { QueriedId = w.CategoryId };
            var result = await query.FirstOrDefaultAsync();
            Assert.IsNotNull(result);
            Assert.AreEqual(123, result.QueriedId);
        }
    }
}