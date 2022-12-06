﻿using System.Linq;
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
            _db = new EfrpgTestDbContext("Data Source=(local);Initial Catalog=Efrpgtest;Integrated Security=True;Encrypt=false;TrustServerCertificate=true");
        }

        // https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/issues/538
        [Test]
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

        // https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/issues/769
        [Test]
        [TestCase(1, false, "Not complete")]
        [TestCase(20, true, "Complete")]
        public void StoredProcedureOutParametersWithMultipleResultSets(int applicationId, bool expected, string expectedResult)
        {
            Assert.IsNotNull(_db);
            var result = _db.CheckIfApplicationIsComplete(applicationId, out var isApplicationComplete);

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, isApplicationComplete);
            Assert.AreEqual("Application", result.ResultSet1.Single().Key);
            Assert.AreEqual(expectedResult, result.ResultSet1.Single().Value);
        }
    }
}