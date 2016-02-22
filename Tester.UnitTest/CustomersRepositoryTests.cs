using System.Collections.Generic;
using System.Threading.Tasks;
using EntityFramework_Reverse_POCO_Generator;
using NUnit.Framework;
using Tester.BusinessLogic;

namespace Tester.UnitTest
{
    [TestFixture]
    public class CustomersRepositoryTests
    {
        private ICustomersRepository _customersRepository;
        private IMyDbContext _context;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _context = new FakeMyDbContext();

            var list = new List<Customer>
            {
                new Customer
                {
                    CustomerId = "1",
                    CompanyName = "abc"
                },
                new Customer
                {
                    CustomerId = "2",
                    CompanyName = "def"
                }
            };

            _context.Customers.AddRange(list);
            _customersRepository = new CustomersRepository(_context);
        }

        [Test]
        public void GetCount()
        {
            // Act
            int count = _customersRepository.Count();
            
            // Assert
            Assert.AreEqual(2, count);
        }

        [Test]
        public void FindById()
        {
            // Act
            var abc = _customersRepository.FindById("1");
            var def = _customersRepository.FindById("2");
            var ghi = _customersRepository.FindById("3");

            // Assert
            Assert.IsNotNull(abc);
            Assert.IsNotNull(def);
            Assert.IsNull(ghi);

            Assert.AreEqual("abc", abc.CompanyName);
            Assert.AreEqual("def", def.CompanyName);
        }

        [Test]
        public void Find()
        {
            // Act
            var abc = _customersRepository.Find("1");
            var def = _customersRepository.Find("2");
            var ghi = _customersRepository.Find("3");

            // Assert
            Assert.IsNotNull(abc);
            Assert.IsNotNull(def);
            Assert.IsNull(ghi);

            Assert.AreEqual("abc", abc.CompanyName);
            Assert.AreEqual("def", def.CompanyName);
        }
        
        [Test]
        public void FindById_ShouldFail()
        {
            // Act
            var result = _customersRepository.FindById("123");

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Find_ShouldFail()
        {
            // Act
            var result = _customersRepository.Find("123");

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetTop10Async()
        {
            // Act
            var customers = await _customersRepository.GetTop10Async();

            Assert.AreEqual(2, customers.Count);
            Assert.AreEqual("abc", customers[0].CompanyName);
            Assert.AreEqual("def", customers[1].CompanyName);
        } 
    }
}