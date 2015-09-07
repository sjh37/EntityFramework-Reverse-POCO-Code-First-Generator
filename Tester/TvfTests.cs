using System.Linq;
using EntityFramework_Reverse_POCO_Generator;
using NUnit.Framework;

namespace Tester
{
    [TestFixture]
    public class TvfTests
    {
        private MyDbContext _db;

        [SetUp]
        public void SetUp()
        {
            _db = new MyDbContext();
        }

        [Test]
        public void tvf_dbo_schema_test()
        {
            // Arrange

            // Act
            var data = _db.CsvToInt("123,456", "").ToList();

            // Assert
            Assert.AreEqual(2, data.Count());
            Assert.AreEqual(123, data[0].IntValue);
            Assert.AreEqual(456, data[1].IntValue);
        }

        [Test]
        public void tvf_ffrs_schema_test()
        {
            // Arrange

            // Act
            var data = _db.FFRS_CsvToInt2("23,45", "").ToList();

            // Assert
            Assert.AreEqual(2, data.Count());
            Assert.AreEqual(23, data[0].IntValue);
            Assert.AreEqual(45, data[1].IntValue);
        }
    }
}
