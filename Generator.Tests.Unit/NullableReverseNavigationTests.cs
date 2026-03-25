using System.Collections.Generic;
using Efrpg;
using Efrpg.Filtering;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture, NonParallelizable]
    [Category(Constants.CI)]
    public class NullableReverseNavigationTests
    {
        private IDbContextFilter _filter;
        private Table _parentTable;
        private Table _childTable;
        private List<ForeignKey> _foreignKeys;

        [SetUp]
        public void SetUp()
        {
            _filter      = new SingleContextFilter();
            _parentTable = new Table(_filter, new Schema("dbo"), "A", false) { NameHumanCase = "A" };
            _childTable  = new Table(_filter, new Schema("dbo"), "B", false) { NameHumanCase = "B" };
            _foreignKeys = new List<ForeignKey>
            {
                new ForeignKey("B", "dbo", "A", "dbo", "Id", "Id", "FK_B_A", "B", "A", 1, false, false, "", "", true)
            };
        }

        [TearDown]
        public void TearDown()
        {
            // Restore defaults so other tests are not affected
            Settings.NullableReverseNavigationProperties = false;
            Settings.AllowNullStrings = false;
        }

        // NeedsNullForgiving() -----------------------------------------------

        [Test]
        public void NeedsNullForgiving_WhenNullableReverseNavEnabled_ReturnsTrue()
        {
            Settings.AllowNullStrings = false;
            Settings.NullableReverseNavigationProperties = true;

            Assert.IsTrue(Settings.NeedsNullForgiving());
        }

        [Test]
        public void NeedsNullForgiving_WhenAllowNullStringsEnabled_ReturnsTrue()
        {
            Settings.AllowNullStrings = true;
            Settings.NullableReverseNavigationProperties = false;

            Assert.IsTrue(Settings.NeedsNullForgiving());
        }

        [Test]
        public void NeedsNullForgiving_WhenBothDisabled_ReturnsFalse()
        {
            Settings.AllowNullStrings = false;
            Settings.NullableReverseNavigationProperties = false;

            Assert.IsFalse(Settings.NeedsNullForgiving());
        }

        // AddReverseNavigation OneToOne -----------------------------------------

        [Test]
        public void OneToOne_WhenNullableReverseNavEnabled_ShouldAddNullableMarker()
        {
            Settings.NullableReverseNavigationProperties = true;
            Settings.AllowNullStrings = false;

            _parentTable.AddReverseNavigation(Relationship.OneToOne, _childTable, "B", "B.FK_B_A", _foreignKeys);

            var definition = _parentTable.ReverseNavigationProperty[0].Definition;
            StringAssert.Contains("B?", definition);
            StringAssert.DoesNotContain("= null!", definition);
        }

        [Test]
        public void OneToOne_WhenNullableReverseNavDisabled_AndNrtEnabled_ShouldAddNullForgiving()
        {
            Settings.NullableReverseNavigationProperties = false;
            Settings.AllowNullStrings = true;

            _parentTable.AddReverseNavigation(Relationship.OneToOne, _childTable, "B", "B.FK_B_A", _foreignKeys);

            var definition = _parentTable.ReverseNavigationProperty[0].Definition;
            StringAssert.DoesNotContain("B?", definition);
            StringAssert.Contains("= null!", definition);
        }

        [Test]
        public void OneToOne_WhenBothDisabled_ShouldAddNeither()
        {
            Settings.NullableReverseNavigationProperties = false;
            Settings.AllowNullStrings = false;

            _parentTable.AddReverseNavigation(Relationship.OneToOne, _childTable, "B", "B.FK_B_A", _foreignKeys);

            var definition = _parentTable.ReverseNavigationProperty[0].Definition;
            StringAssert.DoesNotContain("B?", definition);
            StringAssert.DoesNotContain("= null!", definition);
        }

        // String column nullable behaviour when NullableReverseNavEnabled but AllowNullStrings=false ------

        [Test]
        public void NullableStringColumn_WhenNullableReverseNavEnabled_AllowNullStringsFalse_ShouldNotAddNullableMarker()
        {
            // NullableReverseNavigationProperties should ONLY affect reverse navigation properties,
            // not regular string columns.
            Settings.NullableReverseNavigationProperties = true;
            Settings.AllowNullStrings = false;

            var col = new Column { PropertyType = "string", IsNullable = true };
            Assert.IsFalse(col.IsColumnNullable());
        }

        [Test]
        public void NullableStringColumn_WhenAllowNullStringsEnabled_ShouldAddNullableMarker()
        {
            Settings.AllowNullStrings = true;
            Settings.NullableReverseNavigationProperties = false;

            var col = new Column { PropertyType = "string", IsNullable = true };
            Assert.IsTrue(col.IsColumnNullable());
        }

        // JSON-mapped column nullable behaviour ---------------------------------

        [Test]
        public void JsonMappedColumn_WhenBothNrtSettingsFalse_ShouldNotAddNullableMarker()
        {
            // Even though the DB column is nullable, a JSON-mapped class type should not get '?'
            // when NRT is disabled.
            Settings.AllowNullStrings = false;
            Settings.NullableReverseNavigationProperties = false;

            var col = new Column { PropertyType = "MyCustomClass", IsNullable = true, IsJsonMapped = true };
            Assert.IsFalse(col.IsColumnNullable());
        }

        [Test]
        public void JsonMappedColumn_WhenAllowNullStringsEnabled_ShouldAddNullableMarker()
        {
            Settings.AllowNullStrings = true;
            Settings.NullableReverseNavigationProperties = false;

            var col = new Column { PropertyType = "MyCustomClass", IsNullable = true, IsJsonMapped = true };
            Assert.IsTrue(col.IsColumnNullable());
        }

        [Test]
        public void JsonMappedColumn_WhenOnlyNullableReverseNavEnabled_ShouldNotAddNullableMarker()
        {
            // NullableReverseNavigationProperties should not make JSON-mapped types nullable.
            Settings.AllowNullStrings = false;
            Settings.NullableReverseNavigationProperties = true;

            var col = new Column { PropertyType = "MyCustomClass", IsNullable = true, IsJsonMapped = true };
            Assert.IsFalse(col.IsColumnNullable());
        }

        [Test]
        public void NonJsonColumn_CustomType_WhenBothNrtSettingsFalse_ShouldAddNullableMarker()
        {
            // Non-JSON custom types (e.g. enums resolved to a C# enum) remain unchanged:
            // they are not reference types so nullable wrapping is appropriate.
            Settings.AllowNullStrings = false;
            Settings.NullableReverseNavigationProperties = false;

            var col = new Column { PropertyType = "MyEnum", IsNullable = true, IsJsonMapped = false };
            Assert.IsTrue(col.IsColumnNullable());
        }

        // AddReverseNavigation ManyToOne (collection) ---------------------------

        [Test]
        public void ManyToOne_ShouldAlwaysGenerateCollection_RegardlessOfNrtSettings()
        {
            Settings.NullableReverseNavigationProperties = true;
            Settings.AllowNullStrings = true;

            _parentTable.AddReverseNavigation(Relationship.ManyToOne, _childTable, "Bs", "B.FK_B_A", _foreignKeys);

            var definition = _parentTable.ReverseNavigationProperty[0].Definition;
            StringAssert.Contains("ICollection<B>", definition);
            StringAssert.DoesNotContain("?", definition);
        }
    }
}
