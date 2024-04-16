using System.Collections.Generic;
using Efrpg;
using Efrpg.Pluralization;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture]
    [Category(Constants.CI)]
    public class PluralisationTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Inflector.IgnoreWordsThatEndWith = new List<string> { "Status", "To", "Data" };
            Inflector.PluralisationService = new EnglishPluralizationService();
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
        [TestCase("SalesByCategory", "SalesByCategories")]
        public void MakePlural(string word, string expected)
        {
            // Act
            var result = Inflector.MakePlural(word);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
