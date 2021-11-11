using Efrpg.Filtering;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture]
    [Category(Constants.CI)]
    public class MultiContextNameNormalisationTests
    {
        [Test]
        [TestCase(null, "dbo")]
        [TestCase("", "dbo")]
        [TestCase("DBO", "dbo")]
        [TestCase("  ", "dbo")]
        [TestCase("hello", "hello")]
        [TestCase("hello  ", "hello")]
        [TestCase("  hello  ", "hello")]
        [TestCase("Hello", "hello")]
        public void Schema(string defaultSchema, string expected)
        {
            // Arrange
            var sut = new MultiContextNameNormalisation(defaultSchema);

            // Act
            var result = sut.DefaultSchema;

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(null, "", null)]
        [TestCase(null, "dbo.", "")]
        [TestCase(null, "hello.", "")]
        [TestCase(null, "database.hello.", "")]
        [TestCase("dbo", "", null)]
        [TestCase("dbo", "dbo.", "")]
        [TestCase("dbo", "hello.", "")]
        [TestCase("dbo", "database.hello.", "")]
        [TestCase(null, "Fred", "dbo.fred")]
        [TestCase(null, " Fred ", "dbo.fred")]
        [TestCase("", "fred", "dbo.fred")]
        [TestCase("DBO", "fred", "dbo.fred")]
        [TestCase(" ", "fred", "dbo.fred")]
        [TestCase(" ", " fred ", "dbo.fred")]
        [TestCase(" ", " hello.fred ", "hello.fred")]
        [TestCase("dbo", " hello.fred ", "hello.fred")]
        [TestCase(" dbo ", " hello.fred ", "hello.fred")]
        [TestCase(" dbo ", " Hello.Fred ", "hello.fred")]
        [TestCase(" dbo ", " database.hello.fred ", "hello.fred")]
        [TestCase(" dbo ", " Database.Dbo.fred ", "dbo.fred")]
        public void Normalise(string defaultSchema, string name, string expected)
        {
            // Arrange
            var sut = new MultiContextNameNormalisation(defaultSchema);

            // Act
            var result = sut.Normalise(name);

            // Assert
            if (expected == null)
                Assert.IsNull(result);
            else
                Assert.AreEqual(expected, result.ToString());
        }

        [Test]
        [TestCase("", "", null)]
        [TestCase("", null, null)]
        [TestCase("fred", null, "dbo.fred")]
        [TestCase("fred", "abc.def", "abc.fred")]
        [TestCase("Fred", "ABC.DEF", "abc.fred")]
        public void NormaliseWithDbName(string name, string dbName, string expected)
        {
            // Arrange
            var sut = new MultiContextNameNormalisation("dbo");

            // Act
            var result = sut.Normalise(name, dbName);

            // Assert
            if (expected == null)
                Assert.IsNull(result);
            else
                Assert.AreEqual(expected, result.ToString());
        }
    }
}