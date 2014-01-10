using System;
using EntityFramework_Reverse_POCO_Generator;
using NUnit.Framework;
using Tester.BusinessLogic;

namespace Tester.UnitTest
{
    [TestFixture]
    public class CustomersRepositoryTests
    {
        private ICurrentProductListRepository _currentProductListRepositoryRepository;
        private IMyDbContext _context;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _context = new FakeDbContext();

            _context.CurrentProductList.Attach(new CurrentProductList
            {
                ProductId = 1,
                ProductName = "abc"
            });
            _context.CurrentProductList.Attach(new CurrentProductList
            {
                ProductId = 2,
                ProductName = "def"
            });

            _currentProductListRepositoryRepository = new CurrentProductListRepositoryRepository(_context);
        }

        [Test]
        public void GetCount()
        {
            // Act
            int count = _currentProductListRepositoryRepository.GetCount();
            
            // Assert
            Assert.AreEqual(2, count);
        }

        [Test]
        public void GetProductByID()
        {
            // Act
            var abc = _currentProductListRepositoryRepository.GetProductByID(1);
            var def = _currentProductListRepositoryRepository.GetProductByID(2);
            var ghi = _currentProductListRepositoryRepository.GetProductByID(3);

            // Assert
            Assert.IsNotNull(abc);
            Assert.IsNotNull(def);
            Assert.IsNull(ghi);

            Assert.AreEqual("abc", abc.ProductName);
            Assert.AreEqual("def", def.ProductName);
        }

        
        [Test]
        public void UpdateProductName()
        {
            // Act
            bool updated = _currentProductListRepositoryRepository.UpdateProductName(2, "fred");
            var count = _currentProductListRepositoryRepository.GetCount();
            var data = _currentProductListRepositoryRepository.GetProductByID(2);

            // Assert
            Assert.AreEqual(true, updated);
            Assert.AreEqual(2, count);
            Assert.IsNotNull(data);
            Assert.AreEqual("fred", data.ProductName);
        }
    }
}