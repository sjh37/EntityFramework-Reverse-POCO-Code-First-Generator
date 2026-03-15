using EntityFramework_Reverse_POCO_Generator;
using Generator.Tests.Common;
using NUnit.Framework;
using V8EfrpgTest;

namespace Tester.Integration.EFCore8
{
    [TestFixture]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SqlServer)]
    public class StoredProcedureTests
    {
        [SetUp]
        public void SetUp()
        {
            _northwind = new MyDbContext();
        }

        private MyDbContext _northwind = null!;

        [Test]
        [TestCase("ALFKI")]
        [TestCase("FAMIA")]
        [TestCase("WOLZA")]
        public void CustOrderHist(string customerId)
        {
            Assert.IsNotNull(_northwind);
            var data = _northwind.CustOrderHist(customerId);
            var asyncData = _northwind.CustOrderHistAsync(customerId).Result;

            Assert.IsNotNull(data);
            Assert.IsNotNull(asyncData);
            Assert.AreEqual(data.Count, asyncData.Count);

            for (var n = 0; n < data.Count; ++n)
            {
                var dataItem = data[n];
                var asyncItem = asyncData[n];

                Assert.AreEqual(dataItem.ProductName, asyncItem.ProductName);
                Assert.AreEqual(dataItem.Total, asyncItem.Total);
            }
        }

        [Test]
        [Description("Issue #721 - SP returning columns whose names contain spaces should map correctly")]
        public void SpacedColumnStoredProcedure_ColumnsAreMappedCorrectly()
        {
            // Arrange
            using var db = new V8EfrpgTestDbContext();

            // Act - [dbo].[stp test space test] returns [code object no] and [application no]
            var data = db.StpTestSpaceTest(0, 0);

            // Assert - verify the spaced column names were mapped to valid C# properties
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Count > 0, "Expected rows from CodeObject table");
            Assert.IsTrue(data[0].CodeObjectNo > 0, "CodeObjectNo (mapped from 'code object no') should be populated");
            Assert.IsNotNull(data[0].ApplicationNo, "ApplicationNo (mapped from 'application no') should be populated");
        }
    }
}