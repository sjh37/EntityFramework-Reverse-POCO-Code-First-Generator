using Efrpg;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    /// <summary>
    ///     PostgreSQL reports a column default with its type cast attached - <c>'Hello world'::character varying</c> -
    ///     which has to come off before the value is turned into C#, or it ends up inside the generated string literal.
    ///     T-SQL uses <c>::</c> for something else entirely (a static method call, as in <c>hierarchyid::GetRoot()</c>),
    ///     so the strip is PostgreSQL only and these tests pin both halves of that.
    /// </summary>
    [TestFixture]
    [Category(Constants.CI)]
    public class ColumnDefaultTests
    {
        private DatabaseType _originalDatabaseType;

        [SetUp]
        public void SetUp()
        {
            // Settings is static, so anything set here leaks into whichever test runs next unless it is put back.
            _originalDatabaseType = Settings.DatabaseType;
        }

        [TearDown]
        public void TearDown()
        {
            Settings.DatabaseType = _originalDatabaseType;
        }

        private static Column CleanUp(DatabaseType databaseType, string propertyType, string defaultValue)
        {
            Settings.DatabaseType = databaseType;

            var column = new Column
            {
                PropertyType = propertyType,
                Default      = defaultValue
            };

            column.CleanUpDefault();
            return column;
        }

        [Test]
        public void CleanUpDefault_PostgreSqlStringWithCast_StripsTheCast()
        {
            var column = CleanUp(DatabaseType.PostgreSQL, "string", "'Hello world'::character varying");

            Assert.That(column.Default, Is.EqualTo("\"Hello world\""));
        }

        [Test]
        public void CleanUpDefault_PostgreSqlNullWithCast_ClearsTheDefault()
        {
            var column = CleanUp(DatabaseType.PostgreSQL, "string", "NULL::character varying");

            Assert.That(column.Default, Is.Empty);
        }

        [Test]
        public void CleanUpDefault_PostgreSqlQuotedNullWithCast_KeepsTheLiteralString()
        {
            var column = CleanUp(DatabaseType.PostgreSQL, "string", "'NULL'::character varying");

            Assert.That(column.Default, Is.EqualTo("\"NULL\""));
        }

        [Test]
        public void CleanUpDefault_PostgreSqlArrayWithCast_StripsTheCastAndItsBrackets()
        {
            var column = CleanUp(DatabaseType.PostgreSQL, "string", "'{}'::text[]");

            Assert.That(column.Default, Is.EqualTo("\"{}\""));
        }

        [Test]
        public void CleanUpDefault_PostgreSqlCastOnAQuotedTypeName_StripsTheCast()
        {
            var column = CleanUp(DatabaseType.PostgreSQL, "string", "'sad'::public.\"Mood\"");

            Assert.That(column.Default, Is.EqualTo("\"sad\""));
        }

        [Test]
        public void CleanUpDefault_PostgreSqlDoubleColonInsideTheValue_IsNotTreatedAsACast()
        {
            var column = CleanUp(DatabaseType.PostgreSQL, "string", "'a::b'::text");

            Assert.That(column.Default, Is.EqualTo("\"a::b\""));
        }

        [Test]
        public void CleanUpDefault_PostgreSqlDefaultSql_KeepsTheCast()
        {
            var column = CleanUp(DatabaseType.PostgreSQL, "string", "'Hello world'::character varying");

            Assert.That(column.DefaultSql, Is.EqualTo("'Hello world'::character varying"));
        }

        [Test]
        public void CleanUpDefault_PostgreSqlNullWithCastOnAUnicodeColumn_DoesNotLoseItsLeadingN()
        {
            // IsUnicode is true for anything not spelled char/varchar/text, so PostgreSQL's
            // "character varying" qualifies. The N-prefix strip must not see NULL::character varying.
            Settings.DatabaseType = DatabaseType.PostgreSQL;
            var column = new Column { PropertyType = "string", IsUnicode = true, Default = "NULL::character varying" };

            column.CleanUpDefault();

            Assert.That(column.Default, Is.Empty);
        }

        [Test]
        public void CleanUpDefault_PostgreSqlGenRandomUuid_BecomesGuidNewGuid()
        {
            var column = CleanUp(DatabaseType.PostgreSQL, "Guid", "gen_random_uuid()");

            Assert.That(column.Default, Is.EqualTo("Guid.NewGuid()"));
        }

        [Test]
        public void CleanUpDefault_PostgreSqlUuidGenerateV4_BecomesGuidNewGuid()
        {
            var column = CleanUp(DatabaseType.PostgreSQL, "Guid", "uuid_generate_v4()");

            Assert.That(column.Default, Is.EqualTo("Guid.NewGuid()"));
        }

        [Test]
        public void CleanUpDefault_SqlServerStaticMethodCall_KeepsTheDoubleColon()
        {
            var column = CleanUp(DatabaseType.SqlServer, "string", "hierarchyid::GetRoot()");

            Assert.That(column.Default, Does.Contain("hierarchyid::GetRoot()"));
        }
    }
}
