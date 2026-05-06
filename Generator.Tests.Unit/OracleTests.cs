using System.Collections.Generic;
using Efrpg.LanguageMapping;
using Efrpg.Readers;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture]
    [Category(Constants.CI)]
    public class OracleToCSharpTests
    {
        private OracleToCSharp _mapping;

        [SetUp]
        public void SetUp()
        {
            _mapping = new OracleToCSharp();
        }

        [Test]
        [TestCase("varchar2",                      "string")]
        [TestCase("nvarchar2",                     "string")]
        [TestCase("char",                          "string")]
        [TestCase("nchar",                         "string")]
        [TestCase("clob",                          "string")]
        [TestCase("nclob",                         "string")]
        [TestCase("long",                          "string")]   // Oracle LONG = deprecated character type
        [TestCase("rowid",                         "string")]
        [TestCase("urowid",                        "string")]
        [TestCase("xmltype",                       "string")]
        [TestCase("varchar",                       "string")]
        [TestCase("interval year to month",        "string")]   // no .NET equivalent
        public void StringTypes(string oracleType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(oracleType, out var actual), $"Type '{oracleType}' not found in mapping");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("number",           "decimal")]
        [TestCase("decimal",          "decimal")]
        [TestCase("numeric",          "decimal")]
        public void DecimalTypes(string oracleType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(oracleType, out var actual), $"Type '{oracleType}' not found in mapping");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("binary_double",    "double")]   // IEEE 754 double
        [TestCase("float",            "double")]   // ANSI FLOAT = binary double
        [TestCase("double precision", "double")]   // alias for BINARY_DOUBLE
        public void DoubleTypes(string oracleType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(oracleType, out var actual), $"Type '{oracleType}' not found in mapping");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("binary_float",     "float")]   // IEEE 754 single precision
        [TestCase("real",             "float")]   // ANSI REAL = single precision
        public void FloatTypes(string oracleType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(oracleType, out var actual), $"Type '{oracleType}' not found in mapping");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("int",              "long")]   // Oracle INT = NUMBER(38)
        [TestCase("integer",          "long")]   // Oracle INTEGER = NUMBER(38)
        [TestCase("smallint",         "long")]   // Oracle SMALLINT = NUMBER(38)
        public void LongIntegerTypes(string oracleType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(oracleType, out var actual), $"Type '{oracleType}' not found in mapping");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("binary_integer",   "int")]   // PL/SQL 32-bit integer
        [TestCase("pls_integer",      "int")]   // PL/SQL 32-bit integer
        public void IntegerTypes(string oracleType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(oracleType, out var actual), $"Type '{oracleType}' not found in mapping");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("blob",             "byte[]")]
        [TestCase("raw",              "byte[]")]
        [TestCase("long raw",         "byte[]")]
        [TestCase("bfile",            "byte[]")]
        public void BinaryTypes(string oracleType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(oracleType, out var actual), $"Type '{oracleType}' not found in mapping");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("date",             "DateTime")]
        [TestCase("timestamp",        "DateTime")]
        public void DateTimeTypes(string oracleType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(oracleType, out var actual), $"Type '{oracleType}' not found in mapping");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("timestamp with time zone",       "DateTimeOffset")]
        [TestCase("timestamp with local time zone", "DateTimeOffset")]
        public void DateTimeOffsetTypes(string oracleType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(oracleType, out var actual), $"Type '{oracleType}' not found in mapping");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("interval day to second", "TimeSpan")]
        public void TimeSpanTypes(string oracleType, string expected)
        {
            var map = _mapping.GetMapping();
            Assert.IsTrue(map.TryGetValue(oracleType, out var actual), $"Type '{oracleType}' not found in mapping");
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
        public void SpatialTypes_ContainsSdoGeometry()
        {
            CollectionAssert.Contains(_mapping.SpatialTypes(), "sdo_geometry");
        }

        [Test]
        public void PrecisionTypes_ContainsExpectedTypes()
        {
            var types = _mapping.PrecisionTypes();
            CollectionAssert.Contains(types, "float");
            CollectionAssert.Contains(types, "timestamp");
            CollectionAssert.Contains(types, "timestamp with time zone");
            CollectionAssert.Contains(types, "timestamp with local time zone");
        }

        [Test]
        public void PrecisionAndScaleTypes_ContainsNumber()
        {
            var types = _mapping.PrecisionAndScaleTypes();
            CollectionAssert.Contains(types, "number");
            CollectionAssert.Contains(types, "decimal");
            CollectionAssert.Contains(types, "numeric");
        }
    }

    [TestFixture]
    [Category(Constants.CI)]
    public class OracleDatabaseReaderSqlTests
    {
        // These tests verify that all SQL-returning methods produce non-empty strings,
        // catching any accidental regressions that leave queries as string.Empty.

        private OracleDatabaseReader _reader;

        [SetUp]
        public void SetUp()
        {
            _reader = new OracleDatabaseReader(new FakeDbProviderFactory(), new OracleToCSharp());
        }

        [Test] public void TableSQL_IsNotEmpty()            => AssertSqlNotEmpty("TableSQL");
        [Test] public void ForeignKeySQL_IsNotEmpty()        => AssertSqlNotEmpty("ForeignKeySQL");
        [Test] public void ExtendedPropertySQL_IsNotEmpty()  => AssertSqlNotEmpty("ExtendedPropertySQL");
        [Test] public void IndexSQL_IsNotEmpty()             => AssertSqlNotEmpty("IndexSQL");
        [Test] public void StoredProcedureSQL_IsNotEmpty()   => AssertSqlNotEmpty("StoredProcedureSQL");
        [Test] public void TriggerSQL_IsNotEmpty()           => AssertSqlNotEmpty("TriggerSQL");
        [Test] public void SequenceSQL_IsNotEmpty()          => AssertSqlNotEmpty("SequenceSQL");
        [Test] public void ReadDatabaseEditionSQL_IsNotEmpty() => AssertSqlNotEmpty("ReadDatabaseEditionSQL");

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
        public void EnumSQL_WithGroupField_IsNotEmpty()
        {
            var sql = InvokeEnumSQL("MySchema.MyTable", "Name", "Value", "Group");
            Assert.IsNotEmpty(sql);
            StringAssert.Contains("NameField", sql);
            StringAssert.Contains("ValueField", sql);
            StringAssert.Contains("GroupField", sql);
        }

        [Test]
        public void EnumSQL_WithoutGroupField_IsNotEmpty()
        {
            var sql = InvokeEnumSQL("MyTable", "Name", "Value", null);
            Assert.IsNotEmpty(sql);
            StringAssert.Contains("NameField", sql);
            StringAssert.Contains("ValueField", sql);
            StringAssert.DoesNotContain("GroupField", sql);
        }

        [Test]
        public void EnumSQL_SchemaQualifiedTable_IncludesBothParts()
        {
            var sql = InvokeEnumSQL("MYSCHEMA.MYTABLE", "Name", "Value", null);
            StringAssert.Contains("\"MYSCHEMA\"", sql);
            StringAssert.Contains("\"MYTABLE\"", sql);
        }

        private void AssertSqlNotEmpty(string methodName)
        {
            var method = typeof(OracleDatabaseReader).GetMethod(
                methodName,
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            Assert.IsNotNull(method, $"Method {methodName} not found");
            var sql = (string) method.Invoke(_reader, null);
            Assert.IsNotEmpty(sql, $"{methodName} returned an empty string");
        }

        private string InvokeEnumSQL(string table, string nameField, string valueField, string groupField)
        {
            var method = typeof(OracleDatabaseReader).GetMethod(
                "EnumSQL",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            Assert.IsNotNull(method);
            return (string) method.Invoke(_reader, new object[] { table, nameField, valueField, groupField });
        }
    }
}
