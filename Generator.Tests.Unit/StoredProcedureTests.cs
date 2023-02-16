using System.Collections.Generic;
using Efrpg;
using NUnit.Framework;
using System.Data;
using System.Linq;
using Generator.Tests.Common;

namespace Generator.Tests.Unit
{
    [TestFixture]
    [Category(Constants.CI)]
    public class StoredProcedureTests
    {
        private StoredProcedure _sut;

        [SetUp]
        public void Init()
        {
            _sut = new StoredProcedure
            {
                Schema = new Schema("dbo"),
                DbName = "name",
                NameHumanCase = "some_sp",
                IsStoredProcedure = true,
                ReturnModels = new List<List<DataColumn>>
                {
                    new List<DataColumn> { new DataColumn { AllowDBNull = true } }
                },
                Parameters = new List<StoredProcedureParameter>
                {
                    new StoredProcedureParameter
                    {
                        Mode = StoredProcedureParameterMode.In,
                        PropertyType = "DateTime",
                        NameHumanCase = "A",
                        Ordinal = 1
                    },
                    new StoredProcedureParameter
                    {
                        Mode = StoredProcedureParameterMode.InOut,
                        PropertyType = "DateTime",
                        NameHumanCase = "B",
                        Ordinal = 2
                    },
                    new StoredProcedureParameter
                    {
                        Mode = StoredProcedureParameterMode.In,
                        PropertyType = "DateTime",
                        NameHumanCase = "C",
                        Ordinal = 3
                    },
                    new StoredProcedureParameter
                    {
                        Mode = StoredProcedureParameterMode.Out, // Ignored
                        PropertyType = "DateTime",
                        NameHumanCase = "X",
                        Ordinal = 4
                    },
                    new StoredProcedureParameter
                    {
                        Mode = StoredProcedureParameterMode.In,
                        PropertyType = "DateTime",
                        NameHumanCase = "D",
                        Ordinal = 5
                    }
                }
            };
        }

        [Description("Issue #286")]
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
            var result = _sut.WriteStoredProcReturnColumn(col);

            // Assert
            Assert.AreEqual($"public string {expected} {{ get; set; }}", result);
        }

        [TestCase(false, false, "DateTime? A, out DateTime? B, DateTime? C = null, DateTime? D = null")]
        [TestCase(false, true,  "DateTime? A, out DateTime? B, DateTime? C, DateTime? D")]
        [TestCase(true,  false, "DateTime? A, out DateTime? B, DateTime? C, DateTime? D, out int procResult")]
        [TestCase(true,  true,  "DateTime? A, out DateTime? B, DateTime? C, DateTime? D, out int procResult")]
        public void WriteStoredProcFunctionParams_HasTailNullable(bool includeProcResult, bool forInterface, string expected)
        {
            // Act
            var result = _sut.WriteStoredProcFunctionParams(includeProcResult, forInterface);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestCase(false, false, "DateTime? A, out DateTime? B, DateTime? C, out DateTime? D")]
        [TestCase(false, true,  "DateTime? A, out DateTime? B, DateTime? C, out DateTime? D")]
        [TestCase(true,  false, "DateTime? A, out DateTime? B, DateTime? C, out DateTime? D, out int procResult")]
        [TestCase(true,  true,  "DateTime? A, out DateTime? B, DateTime? C, out DateTime? D, out int procResult")]
        public void WriteStoredProcFunctionParams_NoTailNullable(bool includeProcResult, bool forInterface, string expected)
        {
            // Arrange - Set last to be an 'out' parameter.
            _sut.Parameters.Single(x => x.NameHumanCase == "D").Mode = StoredProcedureParameterMode.InOut;

            // Act
            var result = _sut.WriteStoredProcFunctionParams(includeProcResult, forInterface);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(false, false, false, 3)]
        [TestCase(false, true, false,  0)]
        [TestCase(true,  false, false, 0)]
        [TestCase(true,  true, false,  0)]
        [TestCase(false, false, true, 0)]
        [TestCase(false, true, true,  0)]
        [TestCase(true,  false, true, 0)]
        [TestCase(true,  true, true,  0)]
        public void WhichTailEndParametersCanBeNullable(bool includeProcResult, bool forInterface, bool noTailNullable, int expected)
        {
            if (noTailNullable)
            {
                // Arrange - Set last to be an 'out' parameter.
                _sut.Parameters.Single(x => x.NameHumanCase == "D").Mode = StoredProcedureParameterMode.InOut;
            }

            // Act
            var result = _sut.WhichTailEndParametersCanBeNullable(GetParams(), includeProcResult, forInterface);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void WhichTailEndParametersCanBeNullable_AllOutParameters()
        {
            foreach (var parameter in _sut.Parameters)
            {
                parameter.Mode = StoredProcedureParameterMode.InOut;
            }

            // Act
            var result = _sut.WhichTailEndParametersCanBeNullable(GetParams(), false, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void WhichTailEndParametersCanBeNullable_AllInParameters()
        {
            foreach (var parameter in _sut.Parameters)
            {
                parameter.Mode = StoredProcedureParameterMode.In;
            }

            // Act
            var result = _sut.WhichTailEndParametersCanBeNullable(GetParams(), false, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void WhichTailEndParametersCanBeNullable_AllInOutParameters()
        {
            foreach (var parameter in _sut.Parameters)
            {
                parameter.Mode = StoredProcedureParameterMode.InOut;
            }

            // Act
            var result = _sut.WhichTailEndParametersCanBeNullable(GetParams(), false, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void ThisHasMixedOutParameters()
        {
            // Arrange
            _sut.Parameters = new List<StoredProcedureParameter>
            {
                new StoredProcedureParameter
                {
                    Mode = StoredProcedureParameterMode.In,
                    NameHumanCase = "foo",
                    PropertyType = "DateTime",
                    Ordinal = 1
                },
                new StoredProcedureParameter
                {
                    Mode = StoredProcedureParameterMode.InOut,
                    NameHumanCase = "firstOutParam",
                    PropertyType = "int",
                    Ordinal = 2
                },
                new StoredProcedureParameter
                {
                    Mode = StoredProcedureParameterMode.In,
                    NameHumanCase = "bar",
                    PropertyType = "DateTime",
                    Ordinal = 3
                },
                new StoredProcedureParameter
                {
                    Mode = StoredProcedureParameterMode.InOut,
                    NameHumanCase = "secondOutParam",
                    PropertyType = "int",
                    Ordinal = 4
                },
                new StoredProcedureParameter
                {
                    Mode = StoredProcedureParameterMode.In,
                    NameHumanCase = "baz",
                    PropertyType = "DateTime",
                    Ordinal = 5
                }
            };

            // Act
            var resultString = _sut.WriteStoredProcFunctionParams(false, false);
            var resultTail   = _sut.WhichTailEndParametersCanBeNullable(GetParams(), false, false);

            // Assert
            Assert.IsNotNull(resultString);
            Assert.IsNotNull(resultTail);
            Assert.AreEqual(5, resultTail);
            Assert.AreEqual("DateTime? foo, out int? firstOutParam, DateTime? bar, out int? secondOutParam, DateTime? baz = null", resultString);
        }

        private List<StoredProcedureParameter> GetParams()
        {
            return _sut.Parameters.Where(x => x.Mode != StoredProcedureParameterMode.Out).OrderBy(x => x.Ordinal).ToList();
        }
    }
}