using System;
using System.Collections.Generic;
using System.Linq;
using EntityFramework_Reverse_POCO_Generator;
using NUnit.Framework;
using Tester.BusinessLogic;

namespace Tester
{
    [TestFixture]
    public class CustomersRepositoryTests
    {
        private MyDbContext _db;
        private Dictionary<string, string> _dictionary;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _dictionary = new Dictionary<string, string>
            {
                {"ALFKI", "Alfreds Futterkiste"},
                {"ANATR", "Ana Trujillo Emparedados y helados"},
                {"ANTON", "Antonio Moreno Taquería"},
                {"AROUT", "Around the Horn"},
                {"BERGS", "Berglunds snabbköp"},
                {"BLAUS", "Blauer See Delikatessen"},
                {"BLONP", "Blondesddsl père et fils"},
                {"BOLID", "Bólido Comidas preparadas"},
                {"BONAP", "Bon app'"},
                {"BOTTM", "Bottom-Dollar Markets"}
            };
        }

        [SetUp]
        public void SetUp()
        {
            _db = new MyDbContext();
        }

        [Test]
        public void use_EF_directly()
        {
            // Arrange

            // Act
            var data = _db.Customers.Take(10).OrderBy(x => x.CustomerId);

            // Assert
            Assert.AreEqual(_dictionary.Count, data.Count());
            foreach (var customer in data)
            {
                Assert.IsTrue(_dictionary.ContainsKey(customer.CustomerId));
                Assert.AreEqual(_dictionary[customer.CustomerId], customer.CompanyName);
            }
        }

        [Test]
        public void use_EF_via_repository()
        {
            // Arrange
            ICustomersRepository customersRepository = new CustomersRepository(_db);

            // Act
            var data = customersRepository.GetTop10();

            // Assert
            Assert.AreEqual(_dictionary.Count, data.Count());
            foreach (var customer in data)
            {
                Assert.IsTrue(_dictionary.ContainsKey(customer.CustomerId));
                Assert.AreEqual(_dictionary[customer.CustomerId], customer.CompanyName);
            }
        }
    }
}
