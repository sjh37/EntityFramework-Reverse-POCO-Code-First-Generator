using Efrpg;
using Efrpg.FileManagement;
using Efrpg.Filtering;
using Efrpg.Generators;
using Efrpg.Templates;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    // Issue #411 - Decimal type is missing precision and scale in generated EF configuration.
    // Verifies that decimal/numeric columns include precision and scale in:
    //   - HasColumnType("decimal(P,S)") and HasPrecision(P,S) for EF Core fluent API
    //   - [Precision(P, S)] attribute for EF Core data annotations
    //   - HasPrecision(P,S) for EF6 fluent API
    [TestFixture, NonParallelizable]
    [Category(Constants.CI)]
    public class DecimalPrecisionTests
    {
        [SetUp]
        public void SetUp()
        {
            FilterSettings.Reset();
            FilterSettings.AddDefaults();
            FilterSettings.CheckSettings();
            Settings.UseDataAnnotations = false;
        }

        private static Column CreateDecimalColumn(string sqlType, int precision, int scale, bool isNullable)
        {
            var table = new Table(null, new Schema("dbo"), "Order", false) { NameHumanCase = "Order" };
            var column = new Column
            {
                DbName          = "GrossAmount",
                NameHumanCase   = "GrossAmount",
                SqlPropertyType = sqlType,
                PropertyType    = "decimal",
                Precision       = precision,
                Scale           = scale,
                IsNullable      = isNullable,
                IsUnicode       = true, // decimal is not a char/varchar type
                ParentTable     = table,
            };
            table.Columns.Add(column);
            return column;
        }

        [Test]
        [TestCase(18, 4)]
        [TestCase(9, 6)]
        public void DecimalColumn_FluentApi_GeneratesPrecisionInColumnTypeAndHasPrecision(int precision, int scale)
        {
            // Arrange
            Settings.TemplateType       = TemplateType.EfCore10;
            Settings.GeneratorType      = GeneratorType.EfCore;
            Settings.UseDataAnnotations = false;

            var fileManagement = new FileManagementService(new GeneratedTextTransformation());
            var generator      = new GeneratorEfCore(fileManagement, typeof(NullFileManager));
            generator.Init(new FakeDatabaseReader(), string.Empty);

            var column = CreateDecimalColumn("decimal", precision, scale, isNullable: false);

            // Act
            generator.SetupEntityAndConfig(column);

            // Assert: precision and scale appear in both HasColumnType and HasPrecision
            Assert.IsNotNull(column.Config, "Config should not be null");
            StringAssert.Contains($"HasColumnType(\"decimal({precision},{scale})\")", column.Config);
            StringAssert.Contains($"HasPrecision({precision},{scale})", column.Config);
        }

        [Test]
        [TestCase(18, 4)]
        [TestCase(9, 6)]
        public void DecimalColumn_DataAnnotations_GeneratesPrecisionAttributeAndColumnType(int precision, int scale)
        {
            // Arrange
            Settings.TemplateType       = TemplateType.EfCore10;
            Settings.GeneratorType      = GeneratorType.EfCore;
            Settings.UseDataAnnotations = true;

            var fileManagement = new FileManagementService(new GeneratedTextTransformation());
            var generator      = new GeneratorEfCore(fileManagement, typeof(NullFileManager));
            generator.Init(new FakeDatabaseReader(), string.Empty);

            var column = CreateDecimalColumn("decimal", precision, scale, isNullable: false);

            // Act
            generator.SetupEntityAndConfig(column);

            // Assert: [Precision] on the property; HasColumnType in fluent config; HasPrecision not in config
            CollectionAssert.Contains(column.Attributes, $"[Precision({precision}, {scale})]");
            StringAssert.Contains($"HasColumnType(\"decimal({precision},{scale})\")", column.Config);
            StringAssert.DoesNotContain("HasPrecision", column.Config);
        }

        [Test]
        [TestCase(18, 4)]
        [TestCase(9, 6)]
        public void DecimalColumn_Ef6_GeneratesHasPrecision(int precision, int scale)
        {
            // Arrange
            Settings.TemplateType       = TemplateType.Ef6;
            Settings.GeneratorType      = GeneratorType.Ef6;
            Settings.UseDataAnnotations = false;

            var fileManagement = new FileManagementService(new GeneratedTextTransformation());
            var generator      = new GeneratorEf6(fileManagement, typeof(NullFileManager));
            generator.Init(new FakeDatabaseReader(), string.Empty);

            var column = CreateDecimalColumn("decimal", precision, scale, isNullable: false);

            // Act
            generator.SetupEntityAndConfig(column);

            // Assert
            Assert.IsNotNull(column.Config, "Config should not be null");
            StringAssert.Contains($"HasPrecision({precision},{scale})", column.Config);
        }

        [Test]
        [TestCase("decimal", 18, 4)]
        [TestCase("numeric", 10, 3)]
        [TestCase("decimal", 19, 4)]
        [TestCase("numeric",  5, 2)]
        public void PrecisionAndScaleType_IncludesPrecisionInConfig(string sqlType, int precision, int scale)
        {
            // Arrange
            Settings.TemplateType       = TemplateType.EfCore10;
            Settings.GeneratorType      = GeneratorType.EfCore;
            Settings.UseDataAnnotations = false;

            var fileManagement = new FileManagementService(new GeneratedTextTransformation());
            var generator      = new GeneratorEfCore(fileManagement, typeof(NullFileManager));
            generator.Init(new FakeDatabaseReader(), string.Empty);

            var column = CreateDecimalColumn(sqlType, precision, scale, isNullable: true);

            // Act
            generator.SetupEntityAndConfig(column);

            // Assert
            Assert.IsNotNull(column.Config);
            StringAssert.Contains($"HasColumnType(\"{sqlType}({precision},{scale})\")", column.Config);
            StringAssert.Contains($"HasPrecision({precision},{scale})", column.Config);
        }

        [Test]
        public void DecimalColumn_WithNoPrecisionOrScale_OmitsPrecisionFromConfig()
        {
            // Arrange: decimal with Precision=0 and Scale=0 - precision not specified in DB schema
            Settings.TemplateType       = TemplateType.EfCore10;
            Settings.GeneratorType      = GeneratorType.EfCore;
            Settings.UseDataAnnotations = false;

            var fileManagement = new FileManagementService(new GeneratedTextTransformation());
            var generator      = new GeneratorEfCore(fileManagement, typeof(NullFileManager));
            generator.Init(new FakeDatabaseReader(), string.Empty);

            var column = CreateDecimalColumn("decimal", 0, 0, isNullable: true);

            // Act
            generator.SetupEntityAndConfig(column);

            // Assert: type without precision appended, no HasPrecision
            Assert.IsNotNull(column.Config);
            StringAssert.Contains("HasColumnType(\"decimal\")", column.Config);
            StringAssert.DoesNotContain("HasPrecision", column.Config);
        }
    }
}
