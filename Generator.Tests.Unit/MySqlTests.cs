using Efrpg;
using Efrpg.Filtering;
using Efrpg.LanguageMapping;
using Efrpg.Readers;
using Efrpg.Templates;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture]
    [Category(Constants.CI)]
    public class MySqlToCSharpTests
    {
        private MySqlToCSharp _mapping;

        [SetUp]
        public void SetUp()
        {
            FilterSettings.Reset();
            FilterSettings.AddDefaults();
            FilterSettings.CheckSettings();
            Settings.TemplateType = TemplateType.EfCore10;
            Settings.GeneratorType = GeneratorType.EfCore;
            Settings.DisableGeographyTypes = false;
            _mapping = new MySqlToCSharp();
        }

        [Test]
        [TestCase("varchar",           "string")]
        [TestCase("char",              "string")]
        [TestCase("nchar",             "string")]
        [TestCase("nvarchar",          "string")]
        [TestCase("text",              "string")]
        [TestCase("tinytext",          "string")]
        [TestCase("mediumtext",        "string")]
        [TestCase("longtext",          "string")]
        [TestCase("enum",              "string")]
        [TestCase("set",               "string")]
        [TestCase("character varying", "string")]
        [TestCase("national char",     "string")]
        [TestCase("national varchar",  "string")]
        public void StringTypes(string mysqlType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(mysqlType, out var actual), $"Type '{mysqlType}' not found");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("decimal",           "decimal")]
        [TestCase("numeric",           "decimal")]
        [TestCase("dec",               "decimal")]
        [TestCase("fixed",             "decimal")]
        [TestCase("serial",            "decimal")]
        [TestCase("bigint unsigned",   "decimal")]
        [TestCase("double unsigned",   "decimal")]
        [TestCase("float unsigned",    "decimal")]
        public void DecimalTypes(string mysqlType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(mysqlType, out var actual), $"Type '{mysqlType}' not found");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("double",            "double")]
        [TestCase("real",              "double")]
        [TestCase("float",             "double")]
        public void DoubleTypes(string mysqlType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(mysqlType, out var actual), $"Type '{mysqlType}' not found");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("bigint",            "long")]
        [TestCase("int unsigned",      "long")]
        [TestCase("integer unsigned",  "long")]
        public void LongTypes(string mysqlType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(mysqlType, out var actual), $"Type '{mysqlType}' not found");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("int",               "int")]
        [TestCase("integer",           "int")]
        [TestCase("mediumint",         "int")]
        [TestCase("smallint unsigned", "int")]
        public void IntTypes(string mysqlType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(mysqlType, out var actual), $"Type '{mysqlType}' not found");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("smallint",          "short")]
        [TestCase("year",              "short")]
        public void ShortTypes(string mysqlType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(mysqlType, out var actual), $"Type '{mysqlType}' not found");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("tinyint unsigned",  "byte")]
        public void ByteTypes(string mysqlType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(mysqlType, out var actual), $"Type '{mysqlType}' not found");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("tinyint",           "SByte")]
        public void SByteTypes(string mysqlType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(mysqlType, out var actual), $"Type '{mysqlType}' not found");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("bool",              "bool")]
        [TestCase("boolean",           "bool")]
        [TestCase("bit(1)",            "bool")]
        public void BoolTypes(string mysqlType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(mysqlType, out var actual), $"Type '{mysqlType}' not found");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("bit",               "long")]
        public void BitType(string mysqlType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(mysqlType, out var actual), $"Type '{mysqlType}' not found");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("blob",              "byte[]")]
        [TestCase("tinyblob",          "byte[]")]
        [TestCase("mediumblob",        "byte[]")]
        [TestCase("longblob",          "byte[]")]
        [TestCase("binary",            "byte[]")]
        [TestCase("varbinary",         "byte[]")]
        [TestCase("char byte",         "byte[]")]
        public void BinaryTypes(string mysqlType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(mysqlType, out var actual), $"Type '{mysqlType}' not found");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("date",              "DateTime")]
        [TestCase("datetime",          "DateTime")]
        [TestCase("timestamp",         "DateTime")]
        public void DateTimeTypes(string mysqlType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(mysqlType, out var actual), $"Type '{mysqlType}' not found");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("datetimeoffset",    "DateTimeOffset")]
        public void DateTimeOffsetTypes(string mysqlType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(mysqlType, out var actual), $"Type '{mysqlType}' not found");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("time",              "TimeSpan")]
        public void TimeSpanTypes(string mysqlType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(mysqlType, out var actual), $"Type '{mysqlType}' not found");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DefaultType_ReturnsString()
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(string.Empty, out var actual));
            Assert.AreEqual("string", actual);
        }

        [Test]
        public void SpatialTypes_ContainsExpectedTypes()
        {
            var types = _mapping.SpatialTypes();
            CollectionAssert.Contains(types, "geometry");
            CollectionAssert.Contains(types, "geography");
            CollectionAssert.Contains(types, "point");
            CollectionAssert.Contains(types, "linestring");
            CollectionAssert.Contains(types, "polygon");
        }

        [Test]
        public void PrecisionTypes_ContainsExpectedTypes()
        {
            var types = _mapping.PrecisionTypes();
            CollectionAssert.Contains(types, "float");
            CollectionAssert.Contains(types, "datetime");
            CollectionAssert.Contains(types, "time");
            CollectionAssert.Contains(types, "timestamp");
        }

        [Test]
        public void PrecisionAndScaleTypes_ContainsExpectedTypes()
        {
            var types = _mapping.PrecisionAndScaleTypes();
            CollectionAssert.Contains(types, "decimal");
            CollectionAssert.Contains(types, "numeric");
        }
    }

    [TestFixture]
    [Category(Constants.CI)]
    public class MySqlDatabaseReaderSqlTests
    {
        private MySqlDatabaseReader _reader;

        [SetUp]
        public void SetUp()
        {
            FilterSettings.Reset();
            FilterSettings.AddDefaults();
            FilterSettings.CheckSettings();
            _reader = new MySqlDatabaseReader(new FakeDbProviderFactory(), new MySqlToCSharp());
        }

        [Test] public void TableSQL_IsNotEmpty()             => AssertSqlNotEmpty("TableSQL");
        [Test] public void ForeignKeySQL_IsNotEmpty()         => AssertSqlNotEmpty("ForeignKeySQL");
        [Test] public void ExtendedPropertySQL_IsNotEmpty()   => AssertSqlNotEmpty("ExtendedPropertySQL");
        [Test] public void IndexSQL_IsNotEmpty()              => AssertSqlNotEmpty("IndexSQL");
        [Test] public void StoredProcedureSQL_IsNotEmpty()    => AssertSqlNotEmpty("StoredProcedureSQL");
        [Test] public void TriggerSQL_IsNotEmpty()            => AssertSqlNotEmpty("TriggerSQL");
        [Test] public void ReadDatabaseEditionSQL_IsNotEmpty() => AssertSqlNotEmpty("ReadDatabaseEditionSQL");

        [Test]
        public void SequenceSQL_IsEmpty_MySqlHasNoSequences()
        {
            var method = GetPrivateMethod("SequenceSQL");
            var sql = (string) method.Invoke(_reader, null);
            Assert.IsEmpty(sql, "MySQL has no sequence objects; SequenceSQL should return empty string");
        }

        [Test]
        public void CanReadStoredProcedures_ReturnsTrue()
        {
            Assert.IsTrue(_reader.CanReadStoredProcedures());
        }

        [Test]
        public void HasIdentityColumnSupport_ReturnsTrue()
        {
            Assert.IsTrue(_reader.HasIdentityColumnSupport());
        }

        [Test]
        public void IndexSQL_ContainsFilterDefinitionColumn()
        {
            var method = GetPrivateMethod("IndexSQL");
            var sql = (string) method.Invoke(_reader, null);
            StringAssert.Contains("FilterDefinition", sql);
            StringAssert.Contains("IncludedColumns", sql);
        }

        [Test]
        public void StoredProcedureSQL_ContainsParameterDefaultColumn()
        {
            var method = GetPrivateMethod("StoredProcedureSQL");
            var sql = (string) method.Invoke(_reader, null);
            StringAssert.Contains("PARAMETER_DEFAULT", sql);
        }

        [Test]
        public void EnumSQL_WithGroupField_IsNotEmpty()
        {
            var sql = InvokeEnumSQL("mydb.StatusCodes", "Name", "Value", "Group");
            Assert.IsNotEmpty(sql);
            StringAssert.Contains("NameField", sql);
            StringAssert.Contains("ValueField", sql);
            StringAssert.Contains("GroupField", sql);
        }

        [Test]
        public void EnumSQL_WithoutGroupField_OmitsGroupField()
        {
            var sql = InvokeEnumSQL("StatusCodes", "Name", "Value", null);
            Assert.IsNotEmpty(sql);
            StringAssert.Contains("NameField", sql);
            StringAssert.Contains("ValueField", sql);
            StringAssert.DoesNotContain("GroupField", sql);
        }

        [Test]
        public void EnumSQL_SchemaQualifiedTable_UsesBacktickQuoting()
        {
            var sql = InvokeEnumSQL("mydb.StatusCodes", "Name", "Value", null);
            StringAssert.Contains("`mydb`", sql);
            StringAssert.Contains("`StatusCodes`", sql);
        }

        [Test]
        public void EnumSQL_EmptyTable_ReturnsEmpty()
        {
            var sql = InvokeEnumSQL("", "Name", "Value", null);
            Assert.IsEmpty(sql);
        }

        private void AssertSqlNotEmpty(string methodName)
        {
            var method = GetPrivateMethod(methodName);
            var sql = (string) method.Invoke(_reader, null);
            Assert.IsNotEmpty(sql, $"{methodName} returned an empty string");
        }

        private System.Reflection.MethodInfo GetPrivateMethod(string name)
        {
            var method = typeof(MySqlDatabaseReader).GetMethod(
                name,
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Assert.IsNotNull(method, $"Method {name} not found");
            return method;
        }

        private string InvokeEnumSQL(string table, string nameField, string valueField, string groupField)
        {
            var method = GetPrivateMethod("EnumSQL");
            return (string) method.Invoke(_reader, new object[] { table, nameField, valueField, groupField });
        }
    }
}
