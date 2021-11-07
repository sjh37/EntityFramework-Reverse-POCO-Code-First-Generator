using EntityFramework_Reverse_POCO_Generator;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Tester.BusinessLogic;

namespace Tester.Integration.EfCore3
{
    [TestFixture]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SqlServer)]
    public class CustomersRepositoryTests
    {
        private MyDbContext SUT;
        private Dictionary<string, string> _dictionary;

        [OneTimeSetUp]
        public void BeforeAll()
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
            var customer = SUT.Customers.FirstOrDefault(x => x.CustomerId == "TEST.");
            if (customer == null)
                return;
            SUT.Customers.Remove(customer);
            SUT.SaveChanges();
        }

        [SetUp]
        public void SetUp()
        {
            //_configuration = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json", false, false)
            //    .Build();

            //SUT = new MyDbContext(_configuration);
            //SUT = new MyDbContext();

            SUT = ConfigurationExtensions.CreateMyDbContext();
        }

        

        [Test]
        public void UseEfDirectly()
        {
            // Arrange

            // Act
            var data = SUT.Customers.Take(10).OrderBy(x => x.CustomerId).ToList();

            // Assert
            AssertCustomerData(data);
        }

        [Test]
        public void UseEfViaRepository()
        {
            // Arrange
            var customersRepository = new CustomersRepository(SUT);

            // Act
            var data = customersRepository.GetTop10().ToList();

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
        public void InsertAndDeleteTestRecordSuccessfullyViaFindById()
        {
            // Arrange
            var db2 = ConfigurationExtensions.CreateMyDbContext();
            var db3 = ConfigurationExtensions.CreateMyDbContext();
            var customersRepository1 = new CustomersRepository(SUT);
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
        [Category(Constants.Integration)]
        public void InsertAndDeleteTestRecordSuccessfullyViaFind()
        {
            // Arrange
            var db2 = ConfigurationExtensions.CreateMyDbContext();
            var db3 = ConfigurationExtensions.CreateMyDbContext();

            var customersRepository1 = new CustomersRepository(SUT);
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

        #region manually run test
        [Test]
        [Ignore("a bug with decimal sequence (see note)")]
        public void GenerateFredDb()
        {

            /*NOTE:
                There seem to be a bug in generation for decimal sequence
                It generates (18,2) and cause the execution to fail
                CREATE SEQUENCE [dbo].[CountByDecimal] AS decimal(18,2) START WITH 593 INCREMENT BY 82 MINVALUE 5 MAXVALUE 777777 NO CYCLE;

               Should be generated as decimal or decimal(18,0)
               CREATE SEQUENCE [dbo].[CountByDecimal] AS decimal START WITH 593 INCREMENT BY 82 MINVALUE 5 MAXVALUE 777777 NO CYCLE;

            */
            var fredDb = ConfigurationExtensions.CreateFredDbContext();
            fredDb.Database.EnsureCreated();

            var sql = fredDb.Database.GenerateCreateScript();
            Console.WriteLine(sql);
        }
        #endregion
    }
}
