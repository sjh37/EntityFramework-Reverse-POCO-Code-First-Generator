using NUnit.Framework;
using System.Linq;

namespace Tester.Integration.EfCore3
{
    [TestFixture]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SqlServer)]
    public class TableValuedFunctionTests
    {
        private TestDatabaseStandard.TestDbContext SUT;

        [SetUp]
        public void SetUp()
        {
            SUT = ConfigurationExtensions.CreateTestDbContext();
        }

        [TearDown]
        public void TearDown()
        {
            SUT.Dispose();
        }

        [Test]
        public void Standard_test()
        {
            // Arrange

            // Act
            var data = SUT.CsvToInt("123,456", "").ToList();

            // Assert
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(123, data[0].IntValue);
            Assert.AreEqual(456, data[1].IntValue);
        }

        [Test]
        public void Standard_FunctionCanBeCalledWithinQueryThroughInterfaceReference()
        {
            // Arrange
            var sut = (TestDatabaseStandard.ITestDbContext) SUT;

            // Act
            var data = sut.CsvToInt("123,456", "").ToList();
            
            /* EF6 test
             var data = (from f in db.Stafford_Foos
                        let i = db.CsvToInt("123,456", "")
                        select new { f, i })
                    .ToList()
                    .SelectMany(d => d.i)
                    .ToList();*/

            // Assert
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(123, data[0].IntValue);
            Assert.AreEqual(456, data[1].IntValue);
        }

        [Test]
        public void Standard_FunctionInNonDefaultSchemaCanBeCalled()
        {
            // Arrange

            // Act
            var data = SUT.CustomSchema_CsvToIntWithSchema("123,456", "").ToList();

            // Assert
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(123, data[0].IntValue);
            Assert.AreEqual(456, data[1].IntValue);
        }
    }
}
