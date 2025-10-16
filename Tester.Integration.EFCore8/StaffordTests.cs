using Generator.Tests.Common;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace Tester.Integration.EFCore8
{
    [TestFixture]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SqlServer)]
    public class StaffordTests
    {
        private TestDatabaseStandard.Stafford_Foo _fooStd = null!;

        [SetUp]
        public void SetUp()
        {
            using var std = new TestDatabaseStandard.TestDbContext();
            std.Database.ExecuteSqlRaw("DELETE FROM Stafford.Foo; DELETE FROM Stafford.Boo;");
            _fooStd = new TestDatabaseStandard.Stafford_Foo { Name = "Foo", Stafford_Boo = new TestDatabaseStandard.Stafford_Boo { Name = "Boo" } };
            std.Stafford_Foos.Add(_fooStd);
            std.SaveChanges();
        }

        [Test]
        public void NormalNavigation_Standard()
        {
            Console.WriteLine(_fooStd.Id);
            using var db = new TestDatabaseStandard.TestDbContext();
            var foo = db.Stafford_Foos.Include(x => x.Stafford_Boo).First(f => f.Id == _fooStd.Id);
            Assert.IsNotNull(foo);
            Assert.IsNotNull(foo.Stafford_Boo);
        }

        [Test]
        public void ReverseNavigation_Standard()
        {
            Console.WriteLine(_fooStd.Stafford_Boo.Id);
            using var db = new TestDatabaseStandard.TestDbContext();
            var boo = db.Stafford_Boos.Include(x => x.Stafford_Foo).First(b => b.Id == _fooStd.Stafford_Boo.Id);
            Assert.IsNotNull(boo);
            Assert.IsNotNull(boo.Stafford_Foo);
        }

        /*[Test]
        public void Validation_WhenEntityHasComputedColumn_ShouldValidate_Standard()
        {
            using var db = new TestDatabaseStandard.TestDbContext();
            var entity = new TestDatabaseStandard.Stafford_ComputedColumn { MyColumn = "something" };
            db.Stafford_ComputedColumns.Add(entity);
            var validationErrors = db.GetValidationErrors()
                .SelectMany(p => p.ValidationErrors)
                .Select(p => $"{p.PropertyName} - ${p.ErrorMessage})");
            Assert.IsEmpty(validationErrors);
        }*/
    }
}
