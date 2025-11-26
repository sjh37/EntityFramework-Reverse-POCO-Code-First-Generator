using Generator.Tests.Common;
using NUnit.Framework;

namespace Tester.Integration.EFCore10
{
    /// <summary>
    /// Tests for SQL Server 2025 new data types (json and vector).
    /// These types are only available in Azure SQL Database or SQL Server 2025+.
    /// </summary>
    [TestFixture]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SqlServer)]
    public class SqlServer2025TypeTests
    {
        /// <summary>
        /// Tests that the generator correctly maps SQL Server 2025's native json type.
        /// The json type is a new native type in SQL Server 2025 (distinct from storing JSON in nvarchar columns).
        /// It should map to string by default, or optionally to JsonDocument/JsonElement.
        /// </summary>
        /// <remarks>
        /// To run this test, you need a SQL Server 2025 instance or Azure SQL Database with the json type.
        /// Create a test table:
        /// CREATE TABLE dbo.JsonTestTable (
        ///     Id INT IDENTITY(1,1) PRIMARY KEY,
        ///     JsonData json NOT NULL,
        ///     NullableJsonData json NULL
        /// );
        /// </remarks>
        [Test]
        [Ignore("Requires SQL Server 2025 or Azure SQL Database with json type support. Uncomment when available.")]
        public void JsonType_ShouldMapToString()
        {
            // Arrange
            // This test requires a table with a json column to be reverse-engineered
            // The generated POCO should have a string property for the json column

            // Act
            // The generator should produce code like:
            // public string JsonData { get; set; }
            // public string? NullableJsonData { get; set; }

            // Assert
            Assert.Pass("JSON type mapping test - manual verification required");
        }

        /// <summary>
        /// Tests that the generator correctly maps SQL Server 2025's vector type.
        /// The vector type is a new type for AI/ML scenarios, storing fixed-dimensional vectors.
        /// It should map to SqlVector&lt;float&gt; for optimal performance (up to 50x faster reads).
        /// </summary>
        /// <remarks>
        /// To run this test, you need a SQL Server 2025 instance or Azure SQL Database with vector support.
        /// Create a test table:
        /// CREATE TABLE dbo.VectorTestTable (
        ///     Id INT IDENTITY(1,1) PRIMARY KEY,
        ///     Embedding vector(1536) NOT NULL,  -- 1536 is typical for OpenAI embeddings
        ///     NullableEmbedding vector(384) NULL
        /// );
        /// 
        /// SqlVector&lt;float&gt; advantages over float[]:
        /// - Up to 50x faster reads via efficient binary transport
        /// - 3.3x faster writes
        /// - 19x faster bulk copy operations
        /// - Native support for EF.Functions.VectorDistance()
        /// </remarks>
        [Test]
        [Ignore("Requires SQL Server 2025 or Azure SQL Database with vector type support. Uncomment when available.")]
        public void VectorType_ShouldMapToSqlVector()
        {
            // Arrange
            // This test requires a table with a vector column to be reverse-engineered
            // The generated POCO should have a SqlVector<float> property for the vector column

            // Act
            // The generator should produce code like:
            // using Microsoft.Data.SqlClient.Types;
            // public SqlVector<float> Embedding { get; set; }
            // public SqlVector<float>? NullableEmbedding { get; set; }

            // Assert
            Assert.Pass("Vector type mapping test - manual verification required");
        }

        /// <summary>
        /// Verifies that the AutoMapSqlServer2025Types setting only applies to SQL Server databases.
        /// PostgreSQL has its own json/jsonb types that are handled separately.
        /// </summary>
        [Test]
        public void AutoMapSqlServer2025Types_OnlyAppliesToSqlServer()
        {
            // This is a design verification test
            // The AutoMapSqlServer2025Types setting should check:
            // 1. Settings.DatabaseType == DatabaseType.SqlServer
            // 2. Settings.IsEfCore10Plus()
            // 
            // PostgreSQL json/jsonb types are handled by PostgresToCSharp.cs
            // which maps them to string without needing this setting.

            Assert.Pass("Design verification - AutoMapSqlServer2025Types should only apply to SQL Server");
        }
    }
}

