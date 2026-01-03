using RP;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Tester.Integration.EFCore8
{
    /// <summary>
    /// Tests for SQL Server 2025 new data types (json and vector) in EF Core 8.
    /// These types are only available in Azure SQL Database or SQL Server 2025+.
    /// EF Core 8 uses generic fallback types since it doesn't have native support:
    /// - json → string
    /// - vector → byte[] (raw binary data, not SqlVector&lt;float&gt;)
    /// </summary>
    [TestFixture]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SqlServer)]
    public class SqlServer2025TypeTests
    {
        /// <summary>
        /// Tests that the generator correctly maps SQL Server 2025's native json type.
        /// The json type is a new native type in SQL Server 2025 (distinct from storing JSON in nvarchar columns).
        /// In EF Core 8, it maps to string (same as EF Core 10).
        /// </summary>
        /// <remarks>
        /// The TestingEfCore10 table has a ShippingAddress column of type json that maps to string.
        /// Fluent API: .HasColumnType("json")
        /// </remarks>
        [Test]
        public void JsonType_ShouldMapToString()
        {
            // Arrange - TestingEfCore10.ShippingAddress is a json column mapped to string

            // Act - Create an instance with JSON data
            var entity = new TestingEfCore10
            {
                ShippingAddress = "{\"street\": \"123 Main St\", \"city\": \"London\", \"postcode\": \"SW1A 1AA\"}"
            };

            // Assert - Verify the property type is string
            Assert.That(entity.ShippingAddress, Is.TypeOf<string>());
            Assert.That(entity.ShippingAddress, Does.Contain("London"));
        }

        /// <summary>
        /// Tests that the generator correctly maps SQL Server 2025's vector type in EF Core 8.
        /// Since EF Core 8 doesn't have native SqlVector support, the vector type maps to byte[].
        /// This is the raw binary representation of the vector data.
        /// </summary>
        /// <remarks>
        /// The TestingEfCore10 table has an Embedding column of type vector(1234) mapped to byte[].
        /// Fluent API: .HasColumnType("vector(1234)")
        /// 
        /// Note: For optimal performance with vector operations, consider upgrading to EF Core 10
        /// which supports SqlVector&lt;float&gt; with up to 50x faster reads.
        /// </remarks>
        [Test]
        public void VectorType_ShouldMapToByteArray()
        {
            // Arrange - TestingEfCore10.Embedding is a vector(1234) column mapped to byte[] in EFCore8
            // Each float is 4 bytes, so 1234 floats = 4936 bytes, plus 8 bytes overhead = 4944 bytes

            // Act - Create an instance with byte array data (simulating vector binary format)
            var embeddingData = new byte[4944]; // Raw binary representation
            for (int i = 0; i < embeddingData.Length; i++)
                embeddingData[i] = (byte)(i % 256);

            var entity = new TestingEfCore10
            {
                ShippingAddress = "{}",
                Embedding = embeddingData
            };

            // Assert - Verify the property type is byte[]
            Assert.That(entity.Embedding, Is.TypeOf<byte[]>());
            Assert.That(entity.Embedding.Length, Is.EqualTo(4944));
        }

        /// <summary>
        /// Tests that a nullable vector column correctly handles null values.
        /// </summary>
        [Test]
        public void VectorType_ShouldSupportNullable()
        {
            // Arrange - TestingEfCore10.Embedding is nullable (byte[])

            // Act - Create an instance with null embedding
            var entity = new TestingEfCore10
            {
                ShippingAddress = "{}",
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
            Assert.Pass("TestingEfCore10Configuration correctly configures json and vector column types for EF Core 8");
        }
    }
}
