using System.Linq;
using NUnit.Framework;

namespace Tester
{
    [TestFixture]
    public class StaffordTests
    {
        private TestDatabaseStandard.Stafford_Foo _fooStd;

        [SetUp]
        public void SetUp()
        {
            var std = new TestDatabaseStandard.TestDbContext();
            std.Database.ExecuteSqlCommand("DELETE FROM Stafford.Foo; DELETE FROM Stafford.Boo;");
            _fooStd = std.Stafford_Foos.Add(new TestDatabaseStandard.Stafford_Foo { Name = "Foo", Stafford_Boo = new TestDatabaseStandard.Stafford_Boo { Name = "Boo" } });
            std.SaveChanges();
        }

        [Test]
        public void NormalNavigation_Standard()
        {
            var db = new TestDatabaseStandard.TestDbContext();
            var foo = db.Stafford_Foos.First(f => f.Id == _fooStd.Id);
            Assert.IsNotNull(foo.Stafford_Boo);
        }

        [Test]
        public void ReverseNavigation_Standard()
        {
            var db = new TestDatabaseStandard.TestDbContext();
            var boo = db.Stafford_Boos.First(b => b.Id == _fooStd.Stafford_Boo.Id);
            Assert.IsNotNull(boo.Stafford_Foo);
        }

        [Test]
        public void Validation_WhenEntityHasComputedColumn_ShouldValidate_Standard()
        {
            var db = new TestDatabaseStandard.TestDbContext();
            var entity = new TestDatabaseStandard.Stafford_ComputedColumn { MyColumn = "something" };
            db.Stafford_ComputedColumns.Add(entity);
            var validationErrors = db.GetValidationErrors()
                .SelectMany(p => p.ValidationErrors)
                .Select(p => $"{p.PropertyName} - ${p.ErrorMessage})");
            Assert.IsEmpty(validationErrors);
        }
    }
}
