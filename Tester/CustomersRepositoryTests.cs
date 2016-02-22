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
                { "ALFKI", "Alfreds Futterkiste" },
                { "ANATR", "Ana Trujillo Emparedados y helados" },
                { "ANTON", "Antonio Moreno Taquería" },
                { "AROUT", "Around the Horn" },
                { "BERGS", "Berglunds snabbköp" },
                { "BLAUS", "Blauer See Delikatessen" },
                { "BLONP", "Blondesddsl père et fils" },
                { "BOLID", "Bólido Comidas preparadas" },
                { "BONAP", "Bon app'" },
                { "BOTTM", "Bottom-Dollar Markets"}
            };
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            var customer = _db.Customers.FirstOrDefault(x => x.CustomerId == "TEST.");
            if (customer == null)
                return;
            _db.Customers.Remove(customer);
            _db.SaveChanges();
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
            var data = _db.Customers.Take(10).OrderBy(x => x.CustomerId).ToList();

            // Assert
            AssertCustomerData(data);
        }

        [Test]
        public void use_EF_via_repository()
        {
            // Arrange
            var customersRepository = new CustomersRepository(_db);

            // Act
            var data = customersRepository.GetTop10().ToList();

            // Assert
            AssertCustomerData(data);
        }

        [Test]
        public async void use_EF_via_repository_async()
        {
            // Arrange
            var customersRepository = new CustomersRepository(_db);

            // Act
            var data = await customersRepository.GetTop10Async();

            // Assert
            AssertCustomerData(data);
        }

        public void AssertCustomerData(List<Customer> data)
        {
            Assert.AreEqual(_dictionary.Count, data.Count);
            foreach (var customer in data)
            {
                Assert.IsTrue(_dictionary.ContainsKey(customer.CustomerId));
                Assert.AreEqual(_dictionary[customer.CustomerId], customer.CompanyName);
            }
        }

        [Test]
        public void Insert_and_delete_TEST_record_succesfully_via_FindById()
        {
            // Arrange
            var db2 = new MyDbContext();
            var db3 = new MyDbContext();
            var customersRepository1 = new CustomersRepository(_db);
            var customersRepository2 = new CustomersRepository(db2);
            var customersRepository3 = new CustomersRepository(db3);
            var customer = new Customer
            {
                CustomerId = "TEST.",
                CompanyName = "Integration testing"
            };

            // Act
            customersRepository1.AddCustomer(customer);
            var customer2 = customersRepository2.FindById(customer.CustomerId);
            customersRepository2.DeleteCustomer(customer2);
            var customer3 = customersRepository3.FindById(customer.CustomerId); // Should not be found

            // Assert
            Assert.IsNotNull(customer2);
            Assert.AreEqual(customer.CustomerId, customer2.CustomerId);
            Assert.AreEqual(customer.CompanyName, customer2.CompanyName);
            Assert.IsNull(customer3);
        }

        [Test]
        public void Insert_and_delete_TEST_record_succesfully_via_Find()
        {
            // Arrange
            var db2 = new MyDbContext();
            var db3 = new MyDbContext();
            var customersRepository1 = new CustomersRepository(_db);
            var customersRepository2 = new CustomersRepository(db2);
            var customersRepository3 = new CustomersRepository(db3);
            var customer = new Customer
            {
                CustomerId = "TEST.",
                CompanyName = "Integration testing"
            };

            // Act
            customersRepository1.AddCustomer(customer);
            var customer2 = customersRepository2.Find(customer.CustomerId);
            customersRepository2.DeleteCustomer(customer2);
            var customer3 = customersRepository3.Find(customer.CustomerId); // Should not be found

            // Assert
            Assert.IsNotNull(customer2);
            Assert.AreEqual(customer.CustomerId, customer2.CustomerId);
            Assert.AreEqual(customer.CompanyName, customer2.CompanyName);
            Assert.IsNull(customer3);
        }
    }
}
