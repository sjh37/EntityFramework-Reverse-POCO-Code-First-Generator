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
        public IRepository<AspnetApplications> AspnetApplications { get; private set; }

        [SetUp]
        public void Setup()
        {
            // Arrange
            AspnetApplications = new MockRepository<AspnetApplications>();
            AspnetApplications.InsertOnSubmit(new AspnetApplications { ApplicationId = new Guid(), ApplicationName = "app1", Description = "description", LoweredApplicationName = "moq testing" });
            AspnetApplications.InsertOnSubmit(new AspnetApplications { ApplicationId = new Guid(), ApplicationName = "app2", Description = "another description", LoweredApplicationName = "moq testing" });
        }

        [Test]
        public void InsertTest()
        {
            // Act
            AspnetApplications.SubmitChanges();
            
            // Assert
            Assert.AreEqual(2, AspnetApplications.All.Count());
            Assert.AreEqual("app1", AspnetApplications.All.First().ApplicationName);
            Assert.AreEqual("app2", AspnetApplications.All.Last().ApplicationName);
        }

        [Test]
        public void DeleteTest()
        {
            // Act
            AspnetApplications.DeleteOnSubmit(AspnetApplications.All.First());
            AspnetApplications.SubmitChanges();

            // Assert
            Assert.AreEqual(1, AspnetApplications.All.Count());
            Assert.AreEqual("app2", AspnetApplications.All.First().ApplicationName);
        }
    }
}
