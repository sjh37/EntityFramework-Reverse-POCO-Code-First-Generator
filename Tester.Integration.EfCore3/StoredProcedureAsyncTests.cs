using EntityFramework_Reverse_POCO_Generator;
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
            //var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            //optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=Northwind;Integrated Security=True");
            //_db = new MyDbContext(optionsBuilder.Options);



            //var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //var configuration = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json", false, true)
            //    .AddJsonFile($"appsettings.{environment}.json", true, true)
            //    .Build();
            //_db = new MyDbContext(configuration);

            _db = new MyDbContext();
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