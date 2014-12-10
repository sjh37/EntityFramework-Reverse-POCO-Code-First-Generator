using System;
using System.Linq;
using EntityFramework_Reverse_POCO_Generator;
using NUnit.Framework;
using Tester.Repository;

namespace Tester.UnitTest
{
    [TestFixture]
    public class MockRepositoryTest
    {
        private IRepository<CurrentProductList> CurrentProductList { get; set; }

        [SetUp]
        public void Setup()
        {
            // Arrange
            CurrentProductList = new MockRepository<CurrentProductList>();
            CurrentProductList.InsertOnSubmit(new CurrentProductList { ProductId = 1, ProductName = "abc"});
            CurrentProductList.InsertOnSubmit(new CurrentProductList { ProductId = 2, ProductName = "def"});
        }

        [Test]
        public void InsertTest()
        {
            // Act
            CurrentProductList.SubmitChanges();
            
            // Assert
            Assert.AreEqual(2, CurrentProductList.All.Count());
            Assert.AreEqual("abc", CurrentProductList.All.First().ProductName);
            Assert.AreEqual("def", CurrentProductList.All.Last().ProductName);
        }

        [Test]
        public void DeleteTest()
        {
            // Act
            CurrentProductList.DeleteOnSubmit(CurrentProductList.All.First());
            CurrentProductList.SubmitChanges();

            // Assert
            Assert.AreEqual(1, CurrentProductList.All.Count());
            Assert.AreEqual("def", CurrentProductList.All.First().ProductName);
        }
    }
}
