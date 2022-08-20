using Generator.Tests.Common;
using NUnit.Framework;

namespace Tester.Integration.Ef6
{
    [TestFixture]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SqlServer)]
    public class EfrpgTestTests
    {
        private EfrpgTestDbContext _db;

        [SetUp]
        public void SetUp()
        {
            _db = new EfrpgTestDbContext("Data Source=(local);Initial Catalog=Efrpgtest;Integrated Security=True");
        }

        [Test]
        // https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/issues/538
        public void OneToOneInsert()
        {
            Assert.IsNotNull(_db);
            var boo = new Stafford_Boo { Name = "Hello" };
            var foo = new Stafford_Foo { Name = "World", Stafford_Boo = boo };
            _db.Stafford_Boos.Add(boo);
            _db.Stafford_Foos.Add(foo);
            var result = _db.SaveChanges();
            Assert.AreEqual(2, result);
            
            _db.Stafford_Boos.Remove(boo);
            _db.Stafford_Foos.Remove(foo);
            result = _db.SaveChanges();
            Assert.AreEqual(2, result);
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