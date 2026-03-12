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
                ReturnModels = new List<List<DataColumn>>(),
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
            Assert.AreEqual($"public string {expected} {{ get; set; }} = null!;", result);
        }

        [TestCase(false, false, false, "DateTime? A, out DateTime? B, DateTime? C = null, DateTime? D = null")]
        [TestCase(false, true,  false, "DateTime? A, out DateTime? B, DateTime? C, DateTime? D")]
        [TestCase(true,  false, false, "DateTime? A, out DateTime? B, DateTime? C = null, DateTime? D = null")]
        [TestCase(true,  true,  false, "DateTime? A, out DateTime? B, DateTime? C, DateTime? D")]
        [TestCase(false, false, true, "DateTime? A, out DateTime? B, DateTime? C = null, DateTime? D = null")]
        [TestCase(false, true,  true, "DateTime? A, out DateTime? B, DateTime? C, DateTime? D")]
        [TestCase(true,  false, true, "DateTime? A, out DateTime? B, DateTime? C, DateTime? D, out int procResult")]
        [TestCase(true,  true,  true, "DateTime? A, out DateTime? B, DateTime? C, DateTime? D, out int procResult")]
        public void WriteStoredProcFunctionParams_HasTailNullable(bool includeProcResult, bool forInterface, bool hasReturnModel, string expected)
        {
            // Arrange
            if (hasReturnModel)
                _sut.ReturnModels = new List<List<DataColumn>> { new List<DataColumn> { new DataColumn("test")} };

            // Act
            var result = _sut.WriteStoredProcFunctionParams(includeProcResult, forInterface);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestCase(false, false, false, "DateTime? A, out DateTime? B, DateTime? C, out DateTime? D")]
        [TestCase(false, true,  false, "DateTime? A, out DateTime? B, DateTime? C, out DateTime? D")]
        [TestCase(true,  false, false, "DateTime? A, out DateTime? B, DateTime? C, out DateTime? D")]
        [TestCase(true,  true,  false, "DateTime? A, out DateTime? B, DateTime? C, out DateTime? D")]
        [TestCase(false, false, true, "DateTime? A, out DateTime? B, DateTime? C, out DateTime? D")]
        [TestCase(false, true,  true, "DateTime? A, out DateTime? B, DateTime? C, out DateTime? D")]
        [TestCase(true,  false, true, "DateTime? A, out DateTime? B, DateTime? C, out DateTime? D, out int procResult")]
        [TestCase(true,  true,  true, "DateTime? A, out DateTime? B, DateTime? C, out DateTime? D, out int procResult")]
        public void WriteStoredProcFunctionParams_NoTailNullable(bool includeProcResult, bool forInterface, bool hasReturnModel, string expected)
        {
            // Arrange - Set last to be an 'out' parameter.
            _sut.Parameters.Single(x => x.NameHumanCase == "D").Mode = StoredProcedureParameterMode.InOut;
            
            if (hasReturnModel)
                _sut.ReturnModels = new List<List<DataColumn>> { new List<DataColumn> { new DataColumn("test")} };

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
            var resultStringFf = _sut.WriteStoredProcFunctionParams(false, false);
            var resultStringTf = _sut.WriteStoredProcFunctionParams(true, false); // End up being false due to having no ReturnModels

            // Assert
            Assert.IsNotNull(resultStringFf);
            Assert.IsNotNull(resultStringTf);
            Assert.AreEqual(resultStringFf, resultStringTf);
            Assert.AreEqual("DateTime? foo, out int? firstOutParam, DateTime? bar, out int? secondOutParam, DateTime? baz = null", resultStringTf);
        }

        [Test]
        public void ParameterWithNotNullableType()
        {
            // Arrange
            _sut.Parameters = new List<StoredProcedureParameter>
            {
                new StoredProcedureParameter
                {
                    Mode = StoredProcedureParameterMode.In,
                    NameHumanCase = "a",
                    PropertyType = "int",
                    Ordinal = 1
                },
                new StoredProcedureParameter
                {
                    Mode = StoredProcedureParameterMode.In,
                    NameHumanCase = "type",
                    PropertyType = "DataTable",
                    Ordinal = 3
                },
                new StoredProcedureParameter
                {
                    Mode = StoredProcedureParameterMode.In,
                    NameHumanCase = "b",
                    PropertyType = "int",
                    Ordinal = 5
                }
            };

            // Act
            var result = _sut.WriteStoredProcFunctionParams(false, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("int? a, DataTable type, int? b = null", result);
        }

        [Test]
        [Description("Issue #859 - with NRT enabled: string params with NULL default become string? = null")]
        public void WriteStoredProcFunctionParams_WithDbDefaults_StringNullDefault_NrtEnabled()
        {
            Settings.AllowNullStrings = true;
            Settings.NullableReverseNavigationProperties = false;
            try
            {
                _sut.Parameters = new List<StoredProcedureParameter>
                {
                    new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "int",    NameHumanCase = "userId",          Ordinal = 1 },
                    new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "string", NameHumanCase = "clientId",         Ordinal = 2 },
                    new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "string", NameHumanCase = "tokenProvider",    Ordinal = 3, HasDefault = true, DefaultValue = "'FCV'" },
                    new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "string", NameHumanCase = "referringUrl",     Ordinal = 4, HasDefault = true, DefaultValue = null },
                    new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "string", NameHumanCase = "callbackUrl",      Ordinal = 5, HasDefault = true, DefaultValue = null },
                };

                var result = _sut.WriteStoredProcFunctionParams(false, false);

                Assert.AreEqual(
                    "int? userId, string clientId, string tokenProvider = \"FCV\", string? referringUrl = null, string? callbackUrl = null",
                    result);
            }
            finally
            {
                Settings.AllowNullStrings = false;
                Settings.NullableReverseNavigationProperties = true;
            }
        }

        [Test]
        [Description("Issue #859 - with NRT disabled (EF6): string params with NULL default become string = null (no ?)")]
        public void WriteStoredProcFunctionParams_WithDbDefaults_StringNullDefault_NrtDisabled()
        {
            Settings.AllowNullStrings = false;
            Settings.NullableReverseNavigationProperties = false;
            try
            {
                _sut.Parameters = new List<StoredProcedureParameter>
                {
                    new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "int",    NameHumanCase = "userId",          Ordinal = 1 },
                    new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "string", NameHumanCase = "clientId",         Ordinal = 2 },
                    new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "string", NameHumanCase = "tokenProvider",    Ordinal = 3, HasDefault = true, DefaultValue = "'FCV'" },
                    new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "string", NameHumanCase = "referringUrl",     Ordinal = 4, HasDefault = true, DefaultValue = null },
                    new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "string", NameHumanCase = "callbackUrl",      Ordinal = 5, HasDefault = true, DefaultValue = null },
                };

                var result = _sut.WriteStoredProcFunctionParams(false, false);

                // No ? on string — valid C# 7.3 (EF6 / .NET Framework)
                Assert.AreEqual(
                    "int? userId, string clientId, string tokenProvider = \"FCV\", string referringUrl = null, string callbackUrl = null",
                    result);
            }
            finally
            {
                Settings.AllowNullStrings = false;
                Settings.NullableReverseNavigationProperties = true;
            }
        }

        [Test]
        [Description("Issue #859 - int param with numeric default, decimal param with decimal default")]
        public void WriteStoredProcFunctionParams_WithDbDefaults_NumericDefaults()
        {
            _sut.Parameters = new List<StoredProcedureParameter>
            {
                new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "int",     NameHumanCase = "userId",       Ordinal = 1, HasDefault = true, DefaultValue = "12" },
                new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "int",     NameHumanCase = "userIdNull",   Ordinal = 2, HasDefault = true, DefaultValue = null },
                new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "string",  NameHumanCase = "clientName",   Ordinal = 3, HasDefault = true, DefaultValue = "'Hello'" },
                new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "string",  NameHumanCase = "clientDesc",   Ordinal = 4, HasDefault = true, DefaultValue = "'World'" },
                new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "decimal", NameHumanCase = "decimalValue", Ordinal = 5, HasDefault = true, DefaultValue = "1.234" },
                new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "float",   NameHumanCase = "realValue",    Ordinal = 6, HasDefault = true, DefaultValue = "9.876" },
                new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "double",  NameHumanCase = "floatValue",   Ordinal = 7, HasDefault = true, DefaultValue = "6.54" },
            };

            var result = _sut.WriteStoredProcFunctionParams(false, false);

            Assert.AreEqual(
                "int? userId = 12, int? userIdNull = null, string clientName = \"Hello\", string clientDesc = \"World\", decimal? decimalValue = 1.234m, float? realValue = 9.876f, double? floatValue = 6.54",
                result);
        }

        [Test]
        [Description("Issue #859 - DB defaults break the tail: only contiguous trailing defaults become optional")]
        public void WriteStoredProcFunctionParams_WithDbDefaults_OnlyTailBecomesOptional()
        {
            // @Required INT (no default), @Optional1 INT = 5, @Required2 INT (no default), @Optional2 INT = 10
            // Only @Optional2 is a contiguous tail default, so only it becomes optional.
            _sut.Parameters = new List<StoredProcedureParameter>
            {
                new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "int", NameHumanCase = "required",  Ordinal = 1 },
                new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "int", NameHumanCase = "optional1", Ordinal = 2, HasDefault = true, DefaultValue = "5" },
                new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "int", NameHumanCase = "required2", Ordinal = 3 },
                new StoredProcedureParameter { Mode = StoredProcedureParameterMode.In, PropertyType = "int", NameHumanCase = "optional2", Ordinal = 4, HasDefault = true, DefaultValue = "10" },
            };

            var result = _sut.WriteStoredProcFunctionParams(false, false);

            Assert.AreEqual("int? required, int? optional1, int? required2, int? optional2 = 10", result);
        }

        [Test]
        [Description("Issue #859 - ExtractSqlServerParamDefault correctly extracts various default types")]
        public void ExtractSqlServerParamDefault_VariousDefaults()
        {
            const string definition = @"
CREATE OR ALTER PROCEDURE dbo.TestProc
    @UserId INT = 12,
    @ClientId NVARCHAR(50),
    @TokenProvider NVARCHAR(50) = 'FCV',
    @NullableStr NVARCHAR(MAX) = NULL,
    @DecimalVal DECIMAL(18, 4) = 1.234,
    @FloatVal FLOAT = 6.54
AS BEGIN SELECT 1 END";

            Assert.AreEqual("12",      Efrpg.Readers.DatabaseReader.ExtractSqlServerParamDefault(definition, "@UserId"));
            Assert.IsNull(             Efrpg.Readers.DatabaseReader.ExtractSqlServerParamDefault(definition, "@ClientId"));
            Assert.AreEqual("'FCV'",   Efrpg.Readers.DatabaseReader.ExtractSqlServerParamDefault(definition, "@TokenProvider"));
            Assert.AreEqual("NULL",    Efrpg.Readers.DatabaseReader.ExtractSqlServerParamDefault(definition, "@NullableStr"));
            Assert.AreEqual("1.234",   Efrpg.Readers.DatabaseReader.ExtractSqlServerParamDefault(definition, "@DecimalVal"));
            Assert.AreEqual("6.54",    Efrpg.Readers.DatabaseReader.ExtractSqlServerParamDefault(definition, "@FloatVal"));
        }

        [Test]
        [Description("Issue #859 - NormaliseParamDefault strips type casts and outer parens")]
        public void NormaliseParamDefault_StripsTypeCastsAndParens()
        {
            Assert.IsNull(  Efrpg.Readers.DatabaseReader.NormaliseParamDefault("NULL"));
            Assert.IsNull(  Efrpg.Readers.DatabaseReader.NormaliseParamDefault("NULL::integer"));
            Assert.IsNull(  Efrpg.Readers.DatabaseReader.NormaliseParamDefault("(NULL)"));
            Assert.AreEqual("'FCV'",  Efrpg.Readers.DatabaseReader.NormaliseParamDefault("'FCV'::character varying"));
            Assert.AreEqual("'FCV'",  Efrpg.Readers.DatabaseReader.NormaliseParamDefault("'FCV'"));
            Assert.AreEqual("12",     Efrpg.Readers.DatabaseReader.NormaliseParamDefault("12"));
            Assert.AreEqual("1.234",  Efrpg.Readers.DatabaseReader.NormaliseParamDefault("1.234"));
        }

        private List<StoredProcedureParameter> GetParams()
        {
            return _sut.Parameters.Where(x => x.Mode != StoredProcedureParameterMode.Out).OrderBy(x => x.Ordinal).ToList();
        }
    }
}