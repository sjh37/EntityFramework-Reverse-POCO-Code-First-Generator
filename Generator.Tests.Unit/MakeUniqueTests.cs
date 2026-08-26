using System.Collections.Generic;
using Efrpg;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    /// <summary>
    ///     Two stored procedures can clean up to the same C# name - <c>resolve_to_same_name</c> and
    ///     <c>Resolve_ToSameName</c> both become <c>ResolveToSameName</c> - which generated two methods with
    ///     identical signatures and code that would not compile. Tables and columns already disambiguated with a
    ///     numeric suffix; this is the shared helper that now does it for routines too.
    /// </summary>
    [TestFixture]
    [Category(Constants.CI)]
    public class MakeUniqueTests
    {
        [Test]
        public void MakeUnique_NameNotTaken_IsUnchanged()
        {
            var result = NamingHelper.MakeUnique("ResolveToSameName", new List<string> { "SomethingElse" });

            Assert.That(result, Is.EqualTo("ResolveToSameName"));
        }

        [Test]
        public void MakeUnique_NameTaken_GetsTheFirstFreeSuffix()
        {
            var result = NamingHelper.MakeUnique("ResolveToSameName", new List<string> { "ResolveToSameName" });

            Assert.That(result, Is.EqualTo("ResolveToSameName1"));
        }

        [Test]
        public void MakeUnique_SuffixAlsoTaken_KeepsCounting()
        {
            var taken = new List<string> { "ResolveToSameName", "ResolveToSameName1", "ResolveToSameName2" };

            var result = NamingHelper.MakeUnique("ResolveToSameName", taken);

            Assert.That(result, Is.EqualTo("ResolveToSameName3"));
        }

        [Test]
        public void MakeUnique_DiffersOnlyByCase_IsLeftAlone()
        {
            // C# identifiers are case sensitive, so these are legal as two separate members.
            var result = NamingHelper.MakeUnique("Foo", new List<string> { "foo" });

            Assert.That(result, Is.EqualTo("Foo"));
        }

        [Test]
        public void MakeUnique_NothingTaken_IsUnchanged()
        {
            var result = NamingHelper.MakeUnique("Foo", new List<string>());

            Assert.That(result, Is.EqualTo("Foo"));
        }
    }
}
