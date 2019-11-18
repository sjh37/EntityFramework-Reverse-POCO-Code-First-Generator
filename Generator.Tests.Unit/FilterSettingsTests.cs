using Efrpg.Filtering;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture]
    public class FilterSettingsTests
    {
        [Test]
        public void Reset()
        {
            Assert.AreEqual(0, FilterSettings.SchemaFilters.Count);
            Assert.AreEqual(0, FilterSettings.TableFilters.Count);
            Assert.AreEqual(0, FilterSettings.ColumnFilters.Count);
            Assert.AreEqual(0, FilterSettings.StoredProcedureFilters.Count);

            FilterSettings.AddDefaults();
            Assert.AreNotEqual(0, FilterSettings.SchemaFilters.Count);
            Assert.AreNotEqual(0, FilterSettings.TableFilters.Count);
            Assert.AreNotEqual(0, FilterSettings.ColumnFilters.Count);
            Assert.AreNotEqual(0, FilterSettings.StoredProcedureFilters.Count);

            FilterSettings.Reset();
            Assert.AreEqual(0, FilterSettings.SchemaFilters.Count);
            Assert.AreEqual(0, FilterSettings.TableFilters.Count);
            Assert.AreEqual(0, FilterSettings.ColumnFilters.Count);
            Assert.AreEqual(0, FilterSettings.StoredProcedureFilters.Count);
        }
    }
}