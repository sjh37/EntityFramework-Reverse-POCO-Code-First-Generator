using EntityFramework_Reverse_POCO_Generator.OneToOne;
using NUnit.Framework;
using System.Linq;

namespace Tester
{
    [TestFixture]
    public class ReverseOneToOneTests
    {
        private MyDbContext _db;
        private Foo _foo;

        [SetUp]
        public void SetUp()
        {
            _db = new MyDbContext();
            _foo = _db.Foos.Add(new Foo { Name = "Foo", Boo = new Boo { Name = "Boo" } });
            _db.SaveChanges();
        }


        [Test]
        public void NormalNavigation()
        {
            _db = new MyDbContext();
            var foo = _db.Foos.First(f => f.Id == _foo.Id);
            Assert.IsNotNull(foo.Boo);
        }

        [Test]
        public void ReverseNavigation()
        {
            _db = new MyDbContext();
            var boo = _db.Boos.First(b => b.Id == _foo.Boo.Id);
            Assert.IsNotNull(boo.Foo);
        }
    }
}
