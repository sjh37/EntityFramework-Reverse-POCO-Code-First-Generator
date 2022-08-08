using Generator.Tests.Common;
using NUnit.Framework;

namespace Tester.Integration.Ef6
{
    [TestFixture]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SqlServer)]
    public class StoredProcedureTests
    {
        private EfrpgTestDbContext _db;

        [SetUp]
        public void SetUp()
        {
            _db = new EfrpgTestDbContext();
        }
        
        [Test]
        // https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/issues/769
        public void StoredProcedureOutParametersWithMultipleResultSets()
        {
            Assert.IsNotNull(_db);
            var result1 = _db.CheckIfApplicationIsComplete(1, out var isApplicationComplete);
            todo;
        }
    }
}