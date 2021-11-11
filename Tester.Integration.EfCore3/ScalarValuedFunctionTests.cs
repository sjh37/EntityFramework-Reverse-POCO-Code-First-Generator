using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Tester.Integration.EfCore3.Single_context_many_files;

namespace Tester.Integration.EfCore3
{
    [TestFixture]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SqlServer)]
    public class ScalarValuedFunctionTests
    {
        private EfCoreDbContext _db;

        [SetUp]
        public void SetUp()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, false)
                .Build();

            _db = new EfCoreDbContext(configuration);
        }

        [Test]
        public void DoNotCallDirectly()
        {
            Assert.Throws<Exception>(() => _db.UdfNetSale(10, 79.0m, 0.1m));
        }

        [Test]
        public void UseWithinQuery()
        {
            var result = _db.ColumnNames
                .Where(x => x.Adecimal != null)
                .Select(x => new
                {
                    Sale    = x.Adecimal.Value,
                    NetSale = _db.UdfNetSale(10, x.Adecimal.Value, 0.1m)
                })
                .ToList();

            Assert.IsNotNull(result);

            foreach (var row in result)
            {
                Console.WriteLine($"Sale = {row.Sale} Net Sale = {row.NetSale}");
            }
        }
    }
}