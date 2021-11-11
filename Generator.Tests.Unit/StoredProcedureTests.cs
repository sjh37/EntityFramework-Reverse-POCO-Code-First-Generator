using Efrpg;
using NUnit.Framework;
using System.Data;
using Generator.Tests.Common;

namespace Generator.Tests.Unit
{
    [TestFixture]
    [Category(Constants.CI)]
    public class StoredProcedureTests
    {
        public StoredProcedure Sut { get; private set; }

        [SetUp]
        public void Init()
        {
            Sut = new StoredProcedure { Schema = new Schema("dbo"), DbName = "name", NameHumanCase = "some_sp", IsStoredProcedure = true };

        }

        [Description("Issue #286")]
        [Test]
        [TestCase("JSON_F52E2B61-18A1-11d1-B105-00805F49916B", "JSON_Value")]
        [TestCase("XML_F52E2B61-18A1-11d1-B105-00805F49916B", "XML_Value")]
        public void ColumnNameForXmlOrJsonReturnType(string exampleServerGenerated, string expected)
        {
            // Arrange

            var col = new DataColumn
            {
                DataType = typeof(string),
                ColumnName = exampleServerGenerated,
            };

            // Act
            var result = Sut.WriteStoredProcReturnColumn(col);

            // Assert
            Assert.AreEqual($"public string {expected} {{ get; set; }}", result);
        }
    }
}