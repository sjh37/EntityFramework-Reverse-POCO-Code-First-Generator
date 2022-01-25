using Efrpg.Filtering;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    using Efrpg;

    [TestFixture]
    [Category(Constants.CI)]
    public class MultiContextNameNormalisationTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Settings.DefaultSchema = "dbo";
        }

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
        [TestCase(null, "Test", "dbo.test")]
        [TestCase(null, " Test ", "dbo.test")]
        [TestCase("", "test", "dbo.test")]
        [TestCase("DBO", "test", "dbo.test")]
        [TestCase(" ", "test", "dbo.test")]
        [TestCase(" ", " test ", "dbo.test")]
        [TestCase(" ", " hello.test ", "hello.test")]
        [TestCase("dbo", " hello.test ", "hello.test")]
        [TestCase(" dbo ", " hello.test ", "hello.test")]
        [TestCase(" dbo ", " Hello.Test ", "hello.test")]
        [TestCase(" dbo ", " database.hello.test ", "hello.test")]
        [TestCase(" dbo ", " Database.Dbo.test ", "dbo.test")]
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
        [TestCase("test", null, "dbo.test")]
        [TestCase("test", "abc.def", "abc.test")]
        [TestCase("Test", "ABC.DEF", "abc.test")]
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