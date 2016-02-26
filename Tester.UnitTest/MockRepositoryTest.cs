using System.Linq;
using EntityFramework_Reverse_POCO_Generator;
using NUnit.Framework;

namespace Tester.UnitTest
{
    [TestFixture]
    public class MockRepositoryTest
    {
        private IRepository<Customer> Customers { get; set; }

        [SetUp]
        public void Setup()
        {
            // Arrange
            Customers = new MockRepository<Customer>();
            Customers.InsertOnSubmit(new Customer { CustomerId = "1", CompanyName = "abc" });
            Customers.InsertOnSubmit(new Customer { CustomerId = "2", CompanyName = "def" });
        }

        [Test]
        public void InsertTest()
        {
            // Act
            Customers.SubmitChanges();
            
            // Assert
            Assert.AreEqual(2, Customers.All.Count());
            Assert.AreEqual("abc", Customers.All.First().CompanyName);
            Assert.AreEqual("def", Customers.All.Last().CompanyName);
        }

        [Test]
        public void DeleteTest()
        {
            // Act
            Customers.DeleteOnSubmit(Customers.All.First());
            Customers.SubmitChanges();

            // Assert
            Assert.AreEqual(1, Customers.All.Count());
            Assert.AreEqual("def", Customers.All.First().CompanyName);
        }
    }
}
