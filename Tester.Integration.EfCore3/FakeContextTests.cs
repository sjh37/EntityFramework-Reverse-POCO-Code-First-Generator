namespace Tester.Integration.EfCore3
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
            _fakeContext = new FakeMyDbContext { AppUsers = { new AppUser { Id = 123, Name = "Simon" } } };
        }

        [Test]
        public void FirstOrDefault()
        {
            var result = _fakeContext.AppUsers.FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.AreEqual(123, result.Id);
        }

        [Test]
        public async Task FirstOrDefaultAsync()
        {
            var result = await _fakeContext.AppUsers.FirstOrDefaultAsync();
            Assert.IsNotNull(result);
            Assert.AreEqual(123, result.Id);
        }

        [Test]
        public void QueryFirstOrDefault()
        {
            var query = from w in _fakeContext.AppUsers select new { QueriedId = w.Id };
            var result = query.FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.AreEqual(123, result.QueriedId);
        }
    }
}