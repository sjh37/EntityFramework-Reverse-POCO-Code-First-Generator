using EntityFramework_Reverse_POCO_Generator;
using Generator.Tests.Common;
using NUnit.Framework;

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
    }
}