using System.Reflection;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Tester.Integration.Ef6
{
    [TestFixture]
    [Category(Constants.CI)]
    public class EfrpgContextTests
    {
        private EfrpgTestDbContext _db;

        [SetUp]
        public void SetUp()
        {
            _db = new EfrpgTestDbContext("Data Source=(local);Initial Catalog=Efrpgtest;Integrated Security=True;Encrypt=false;TrustServerCertificate=true");
        }

        // https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/issues/621
        [Test]
        [TestCase("StpTest", false)]
        [TestCase("StpTestUnderscoreTest", true)]
        [TestCase("ColourPivot", true)]
        public void StoredProcedureAsyncExists(string method, bool expected)
        {
            Assert.IsNotNull(_db);

            var methodInfo = _db.GetType().GetMethod($"{method}Async", BindingFlags.Instance | BindingFlags.Public);
            Assert.AreEqual(expected, methodInfo != null);
        }
    }
}