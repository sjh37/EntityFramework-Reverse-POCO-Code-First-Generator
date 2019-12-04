using System;
using EntityFramework_Reverse_POCO_Generator;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Tester.Integration.EfCore3
{
    [TestFixture]
    public class StoredProcedureAsyncTests
    {
        private MyDbContext _db;

        [SetUp]
        public void SetUp()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, false)
                .Build();

            _db = new MyDbContext(configuration);
        }

        [Test]
        [TestCase("ALFKI")]
        [TestCase("FAMIA")]
        [TestCase("WOLZA")]
        public void CustOrderHist(string customerId)
        {
            var data = _db.CustOrderHist(customerId);
            var asyncData = _db.CustOrderHistAsync(customerId).Result;

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