using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture]
    [Category(Constants.CI)]
    public class WriteToOuterTests
    {
        [Test]
        [TestCase(false, false, false)]
        [TestCase(false, true, false)]
        [TestCase(false, true, true)]
        [TestCase(true, false, true)]
        public void Reset(bool expected, bool generateSeparateFiles, bool generateSingleDbContext)
        {
            var result = ShouldWriteToOuter(generateSeparateFiles, generateSingleDbContext);
            Assert.AreEqual(expected, result);
        }

        private bool ShouldWriteToOuter(bool generateSeparateFiles, bool generateSingleDbContext)
        {
            return generateSingleDbContext && !generateSeparateFiles;
        }
    }
}