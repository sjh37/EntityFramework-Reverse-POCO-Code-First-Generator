using System.Linq;

using NUnit.Framework;

namespace Tester
{
    [TestFixture]
    public class SynonymTests
    {
        [Test]
        public void SynonymTable_CanBeQueried()
        {
            var db = new TestSynonymsDatabase.TestDbContext();

            var parent = db.ParentSynonyms.First(p => p.ParentId == 1);

            Assert.IsNotNull(parent);
        }
    }
}
