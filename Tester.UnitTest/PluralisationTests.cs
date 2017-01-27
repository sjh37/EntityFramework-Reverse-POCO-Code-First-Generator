using System.Data.Entity.Infrastructure.Pluralization;
using NUnit.Framework;

namespace Tester.UnitTest
{
    [TestFixture]
    public class PluralisationTests
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            Inflector.PluralizationService = new EnglishPluralizationService();
        }

        [Test]
        [TestCase("Issues", "Issue")]
        [TestCase("Schema_Issues", "Schema_Issue")]
        public void MakeSingular(string word, string expected)
        {
            // Act
            var result = Inflector.MakeSingular(word);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase("Issue", "Issues")]
        [TestCase("Schema_Issue", "Schema_Issues")]
        public void MakePlural(string word, string expected)
        {
            // Act
            var result = Inflector.MakePlural(word);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
