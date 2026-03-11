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
            Settings.NullableReverseNavigationProperties = true;
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
