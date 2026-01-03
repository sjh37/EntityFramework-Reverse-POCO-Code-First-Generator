using EntityFramework_Reverse_POCO_Generator;
using Generator.Tests.Common;
using NUnit.Framework;
using Tester.BusinessLogic;

namespace Tester.Integration.EFCore10
{
    [TestFixture]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SqlServer)]
    public class CustomersRepositoryTests
    {
        private MyDbContext _db = null!;
        private Dictionary<string, string> _dictionary = null!;

        [OneTimeSetUp]
        public void OneTimeSetUp()
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

        [OneTimeTearDown]
        public void OneTimeTearDown()
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
            //_configuration = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json", false, false)
            //    .Build();

            //_db = new MyDbContext(_configuration);
            _db = new MyDbContext();
        }

        [Test]
        public void UseEfDirectly()
        {
            // Arrange

            // Act
            var data = _db.Customers.Take(10).OrderBy(x => x.CustomerId).ToList();

            // Assert
            AssertCustomerData(data);
        }

        [Test]
        public void UseEfViaRepository()
        {
            // Arrange
            var customersRepository = new CustomersRepository(_db);

            // Act
            var data = customersRepository.GetTop10().ToList();

            // Assert
            AssertCustomerData(data);
        }

        private void AssertCustomerData(List<EntityFramework_Reverse_POCO_Generator.Customer> data)
        {
            Assert.AreEqual(_dictionary.Count, data.Count);
            foreach (var customer in data)
            {
                Assert.IsTrue(_dictionary.ContainsKey(customer.CustomerId));
                Assert.AreEqual(_dictionary[customer.CustomerId], customer.CompanyName);
            }
        }

        [Test]
        public void InsertAndDeleteTestRecordSuccessfullyViaFindById()
        {
            // Arrange
            var db2 = new MyDbContext();
            var db3 = new MyDbContext();
            var customersRepository1 = new CustomersRepository(_db);
            var customersRepository2 = new CustomersRepository(db2);
            var customersRepository3 = new CustomersRepository(db3);
            var customer = new EntityFramework_Reverse_POCO_Generator.Customer
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
        public void InsertAndDeleteTestRecordSuccessfullyViaFind()
        {
            // Arrange
            var db2 = new MyDbContext();
            var db3 = new MyDbContext();
            var customersRepository1 = new CustomersRepository(_db);
            var customersRepository2 = new CustomersRepository(db2);
            var customersRepository3 = new CustomersRepository(db3);
            var customer = new EntityFramework_Reverse_POCO_Generator.Customer
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

