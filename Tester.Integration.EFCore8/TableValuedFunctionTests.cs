using Generator.Tests.Common;
using NUnit.Framework;
using System.Linq;

namespace Tester.Integration.EFCore8
{
    [TestFixture]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SqlServer)]
    public class TableValuedFunctionTests
    {
        [Test]
        public void Standard_test()
        {
            // Arrange
            using var db = new TestDatabaseStandard.TestDbContext();

            // Act
            var data = db.CsvToInt("123,456", "").ToList();

            // Assert
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(123, data[0].IntValue);
            Assert.AreEqual(456, data[1].IntValue);
        }

        [Test]
        public void Standard_FunctionCanBeCalledWithinQueryThroughInterfaceReference()
        {
            // Arrange
            using var db = (TestDatabaseStandard.ITestDbContext) new TestDatabaseStandard.TestDbContext();

            // Act
            var data = db.CsvToInt("123,456", "").ToList();
            
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
            using var db = new TestDatabaseStandard.TestDbContext();

            // Act
            var data = db.CustomSchema_CsvToIntWithSchema("123,456", "").ToList();

            // Assert
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(123, data[0].IntValue);
            Assert.AreEqual(456, data[1].IntValue);
        }
    }
}
