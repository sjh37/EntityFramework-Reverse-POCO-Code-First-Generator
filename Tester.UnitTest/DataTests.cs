using System;
using EntityFramework_Reverse_POCO_Generator;
using NUnit.Framework;
using Tester.BusinessLogic;

namespace Tester.UnitTest
{
    [TestFixture]
    public class AspnetApplicationRepositoryTests
    {
        private IAspnetApplicationRepository _applicationRepository;
        private IMyDbContext _context;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _context = new FakeDbContext();

            _context.AspnetApplications.Attach(new AspnetApplications
            {
                ApplicationId = new Guid(),
                ApplicationName = "app1",
                Description = "description",
                LoweredApplicationName = "moq testing"
            });
            _context.AspnetApplications.Attach(new AspnetApplications
            {
                ApplicationId = new Guid(),
                ApplicationName = "app2",
                Description = "another description",
                LoweredApplicationName = "moq testing"
            });

            _applicationRepository = new AspnetApplicationRepository(_context);
        }

        [Test]
        public void GetCount()
        {
            // Act
            int count = _applicationRepository.GetCount();
            
            // Assert
            Assert.AreEqual(2, count);
        }

        [Test]
        public void GetApplication()
        {
            // Act
            var app1 = _applicationRepository.GetApplication("app1");
            var app2 = _applicationRepository.GetApplication("app2");
            var app3 = _applicationRepository.GetApplication("app3");

            // Assert
            Assert.IsNotNull(app1);
            Assert.IsNotNull(app2);
            Assert.IsNull(app3);

            Assert.AreEqual("app1", app1.ApplicationName);
            Assert.AreEqual("app2", app2.ApplicationName);
        }

        
        [Test]
        public void UpdateApplicationName()
        {
            // Act
            bool updated = _applicationRepository.UpdateApplicationName("app2", "fred");
            var count = _applicationRepository.GetCount();
            var data = _applicationRepository.GetApplication("fred");

            // Assert
            Assert.AreEqual(true, updated);
            Assert.AreEqual(2, count);
            Assert.IsNotNull(data);
            Assert.AreEqual("fred", data.ApplicationName);
            Assert.AreEqual("another description", data.Description);
        }
    }
}