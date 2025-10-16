﻿using Generator.Tests.Common;
using NUnit.Framework;
using System.Linq;

namespace Tester.Integration.EFCore8
{
    [TestFixture]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SqlServer)]
    public class SynonymTests
    {
        [Test]
        public void SynonymTable_CanBeQueried()
        {
            using var db = new TestSynonymsDatabase.TestDbContext();

            var parent = db.Parents.First(p => p.ParentId == 1);

            Assert.IsNotNull(parent);
        }

        [Test]
        public void SynonymTable_HasForeignKeyNavigationProperties()
        {
            using var db = new TestSynonymsDatabase.TestDbContext();

            var parent = db.Parents.First(p => p.ParentId == 1);
            var child = db.Children.First(p => p.ParentId == 1);

            Assert.IsNotNull(parent.Children);
            Assert.IsNotNull(child.Parent);
        }

        [Test]
        public void SynonymStoredProcedure_CanBeCalled()
        {
            using var db = new TestSynonymsDatabase.TestDbContext();

            var result = db.SimpleStoredProc(0);

            Assert.IsNotNull(result);
        }
    }
}
