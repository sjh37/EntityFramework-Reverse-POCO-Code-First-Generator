using Efrpg.Templates;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture]
    [Category(Constants.CI)]
    public class WriteToOuterTests
    {
        [Test]
        [TestCase(true,  1, false, TemplateType.Ef6,     true,  "VisualStudioFileManager", "")] // 2
        [TestCase(true,  1, false, TemplateType.Ef6,     true,  "NullFileManager",         ".SqlCE")] // 3
        [TestCase(true,  1, false, TemplateType.EfCore2, true,  "CustomFileManager",       ".SqlCE")] // 4
        [TestCase(true,  1, false, TemplateType.EfCore3, true,  "CustomFileManager",       ".SqlCE")] // 4
        [TestCase(false, 1, true,  TemplateType.Ef6,     true,  "VisualStudioFileManager", "")] // 5
        //[TestCase(false, 1, true,  TemplateType.Ef6,    false, "VisualStudioFileManager", "EnumerationDbContext")] // 6
        [TestCase(false, 1, true,  TemplateType.EfCore2, true,  "CustomFileManager",       ".SqlCE")] // 7
        [TestCase(false, 1, true,  TemplateType.EfCore3, true,  "CustomFileManager",       ".SqlCE")] // 7
        //[TestCase(true,  1, false, TemplateType.Ef6,    false, "NullFileManager",         "EnumerationDbContext")] // 8
        // 9
        [TestCase(false, 5, false, TemplateType.Ef6,     false, "VisualStudioFileManager", "EnumerationDbContext")] // 10
        // 11
        [TestCase(false, 5, true,  TemplateType.Ef6,     false, "VisualStudioFileManager", "EnumerationDbContext")] // 12
        public void Reset(bool expected, int filterCount, bool generateSeparateFiles, TemplateType type, bool generateSingleDbContext, string fileManagerType, string key)
        {
            var result = ShouldWriteToOuter(filterCount, generateSeparateFiles, type, generateSingleDbContext, fileManagerType, key);
            Assert.AreEqual(expected, result);
        }

        private bool ShouldWriteToOuter(int filterCount, bool generateSeparateFiles, TemplateType type, bool generateSingleDbContext, string fileManagerType, string key)
        {
            return generateSingleDbContext && !generateSeparateFiles;
        }
    }
}