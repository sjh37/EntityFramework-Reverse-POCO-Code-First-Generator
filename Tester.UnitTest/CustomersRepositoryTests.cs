using System.Linq;
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

            _context.Customers.Attach(new Customer
            {
                CustomerId = "1",
                CompanyName = "abc"
            });
            _context.Customers.Attach(new Customer
            {
                CustomerId = "2",
                CompanyName = "def"
            });

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
        public void GetByID()
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
    }
}