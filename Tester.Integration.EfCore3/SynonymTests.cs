using NUnit.Framework;
using System.Linq;

namespace Tester.Integration.EfCore3
{
    [TestFixture]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SqlServer)]
    public class SynonymTests
    {
        private TestSynonymsDatabase.TestDbContext SUT;

        [SetUp]
        public void SetUp()
        {
            SUT = ConfigurationExtensions.CreateSynonymsDbContext();
        }

        [TearDown]
        public void TearDown()
        {
            SUT.Dispose();
        }

        [Test]
        public void SynonymTable_CanBeQueried()
        {

            var parent = SUT.Parents.First(p => p.ParentId == 1);

            Assert.IsNotNull(parent);
        }

        [Test]
        public void SynonymTable_HasForeignKeyNavigationProperties()
        {
            var parent = SUT.Parents.First(p => p.ParentId == 1);
            var child = SUT.Children.First(p => p.ParentId == 1);

            Assert.IsNotNull(parent.Children);
            Assert.IsNotNull(child.Parent);
        }

        [Test]
        public void SynonymStoredProcedure_CanBeCalled()
        {
            var result = SUT.SimpleStoredProc(0);

            Assert.IsNotNull(result);
        }
    }
}
