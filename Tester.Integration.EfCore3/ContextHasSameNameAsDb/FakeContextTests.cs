namespace Tester.Integration.EfCore3.ContextHasSameNameAsDb
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    public class FakeContextTests
    {
        private FakeFred _fakeContext;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _fakeContext = new FakeFred { Cars = { new Car { Id = 123, CarMake = "Red" } } };
        }
        
        [Test]
        public void FirstOrDefault()
        {
            var result = _fakeContext.Cars.FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.AreEqual(123, result.Id);
        }
        
        [Test]
        public async Task FirstOrDefaultAsync()
        {
            var result = await _fakeContext.Cars.FirstOrDefaultAsync();
            Assert.IsNotNull(result);
            Assert.AreEqual(123, result.Id);
        }
        
        [Test]
        public void QueryFirstOrDefault()
        {
            var query = from w in _fakeContext.Cars select new { QueriedId = w.Id };
            var result = query.FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.AreEqual(123, result.QueriedId);
        }
        
        [Test]
        public async Task QueryFirstOrDefaultAsync()
        {
            var query = from w in _fakeContext.Cars select new { QueriedId = w.Id };
            var result = await query.FirstOrDefaultAsync();
            Assert.IsNotNull(result);
            Assert.AreEqual(123, result.QueriedId);
        }
    }
}