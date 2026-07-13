using Generator.Tests.Common;
using NUnit.Framework;

namespace Tester.Integration.EFCore8
{
    // Issue #885 - dbo.sp_NullableStringRepro returns CAST(NULL AS varchar(50)) AS SomeText. In a consumer project
    // with <Nullable>enable</Nullable>, EF Core 8+ infers result-set column nullability from the C# annotation of
    // the return model property. Before the fix the generated single-file output re-enabled the project's nullable
    // context after the DbContext block ('#nullable restore'), so the plain 'string SomeText' was inferred as
    // required and materialisation threw SqlNullValueException. These tests call the stored procedure for real and
    // must materialise the NULL instead of throwing.
    [TestFixture]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SqlServer)]
    public class NullableStringReproTests
    {
        [Test]
        [Description("Issue #885 - default settings: return model is nullable-oblivious ('#nullable disable'), so EF Core reads the NULL")]
        public void SpNullableStringRepro_ObliviousModel_MaterialisesNullInsteadOfThrowing()
        {
            using var db = new TestDatabaseStandard.TestDbContext();

            var rows = db.SpNullableStringRepro();

            Assert.AreEqual(1, rows.Count);
            Assert.IsNull(rows[0].SomeText);
        }

        [Test]
        [Description("Issue #885 - AllowNullStrings: return model property is string?, so EF Core reads the NULL")]
        public void SpNullableStringRepro_AllowNullStringsModel_MaterialisesNullIntoNullableString()
        {
            using var db = new V8EfrpgTest.V8EfrpgTestDbContext();

            var rows = db.SpNullableStringRepro();

            Assert.AreEqual(1, rows.Count);
            Assert.IsNull(rows[0].SomeText);
        }
    }
}
