using System;
using System.Linq;
using NUnit.Framework;
using Tester.Single_context_many_files;

namespace Tester
{
    [TestFixture]
    public class ScalarValuedFunctionTests
    {
        private Ef6DbContext _db;

        [SetUp]
        public void SetUp()
        {
            _db = new Ef6DbContext();
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