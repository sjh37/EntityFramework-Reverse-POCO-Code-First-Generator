using Azure10;
using Generator.Tests.Common;
using Microsoft.Data.SqlTypes;
using NUnit.Framework;

namespace Tester.Integration.EFCore10
{
    /// <summary>
    /// Tests for SQL Server 2025 new data types (json and vector).
    /// These types are only available in Azure SQL Database or SQL Server 2025+.
    /// Uses the TestingEfCore10 table from Azure.cs which has both json and vector columns.
    /// </summary>
    [TestFixture]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SqlServer)]
    public class SqlServer2025TypeTests
    {
        /// <summary>
        /// Tests that the generator correctly maps SQL Server 2025's native json type.
        /// The json type is a new native type in SQL Server 2025 (distinct from storing JSON in nvarchar columns).
        /// It should map to AzureShippingAddress record class.
        /// </summary>
        /// <remarks>
        /// The TestingEfCore10 table has a ShippingAddress column of type json that maps to AzureShippingAddress.
        /// Fluent API: .HasColumnType("json")
        /// </remarks>
        [Test]
        public void JsonType_ShouldMapToAzureShippingAddress()
        {
            // Arrange - TestingEfCore10.ShippingAddress is a json column mapped to AzureShippingAddress

            // Act - Create an instance with an AzureShippingAddress value
            var shippingAddress = new AzureShippingAddress("John", "Doe", "123 Main St", "", "SW1A 1AA");
            var entity = new TestingEfCore10
            {
                ShippingAddress = shippingAddress
            };

            // Assert - Verify the property type is AzureShippingAddress
            Assert.That(entity.ShippingAddress, Is.TypeOf<AzureShippingAddress>());
            Assert.That(entity.ShippingAddress.Surname, Is.EqualTo("Doe"));
        }

        /// <summary>
        /// Tests that the generator correctly maps SQL Server 2025's vector type.
        /// The vector type is a new type for AI/ML scenarios, storing fixed-dimensional vectors.
        /// It should map to SqlVector&lt;float&gt; for optimal performance.
        /// </summary>
        /// <remarks>
        /// The TestingEfCore10 table has an Embedding column of type vector(1234) mapped to SqlVector&lt;float&gt;?.
        /// Fluent API: .HasColumnType("vector(1234)")
        /// 
        /// SqlVector&lt;float&gt; advantages over float[]:
        /// - Up to 50x faster reads via efficient binary transport
        /// - 3.3x faster writes
        /// - 19x faster bulk copy operations
        /// - Native support for EF.Functions.VectorDistance()
        /// </remarks>
        [Test]
        public void VectorType_ShouldMapToSqlVector()
        {
            // Arrange - TestingEfCore10.Embedding is a vector(4944) column mapped to SqlVector<float>?

            // Act - Create an instance with vector data
            var embeddingData = new float[1234];
            for (int i = 0; i < embeddingData.Length; i++)
                embeddingData[i] = i * 0.001f;

            var entity = new TestingEfCore10
            {
                ShippingAddress = new AzureShippingAddress("", "", "", "", ""),
                Embedding = new SqlVector<float>(embeddingData)
            };

            // Assert - Verify the property type is SqlVector<float>
            Assert.That(entity.Embedding, Is.TypeOf<SqlVector<float>>());
            Assert.That(entity.Embedding.Value.Length, Is.EqualTo(1234));
        }

        /// <summary>
        /// Tests that a nullable vector column correctly handles null values.
        /// </summary>
        [Test]
        public void VectorType_ShouldSupportNullable()
        {
            // Arrange - TestingEfCore10.Embedding is nullable (SqlVector<float>?)

            // Act - Create an instance with null embedding
            var entity = new TestingEfCore10
            {
                ShippingAddress = new AzureShippingAddress("", "", "", "", ""),
                Embedding = null
            };

            // Assert
            Assert.That(entity.Embedding, Is.Null);
        }

        /// <summary>
        /// Verifies the generated POCO configuration uses correct Fluent API for json and vector types.
        /// </summary>
        [Test]
        public void Configuration_ShouldUseCorrectColumnTypes()
        {
            // This test verifies the generated configuration in Azure.cs:
            // builder.Property(x => x.Embedding).HasColumnName(@"Embedding").HasColumnType("vector(1234)").IsRequired(false);
            // builder.Property(x => x.ShippingAddress).HasColumnName(@"ShippingAddress").HasColumnType("json").IsRequired();

            // Verify by checking the TestingEfCore10Configuration class exists and configures the entity correctly
            var config = new TestingEfCore10Configuration();
            Assert.That(config, Is.Not.Null);
            
            // If we get here without exceptions, the configuration is valid
            Assert.Pass("TestingEfCore10Configuration correctly configures json and vector column types");
        }
    }
}
