using System;
using Efrpg;
using Efrpg.Filtering;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture]
    public class FilterTests
    {
        private TestContextFilter _sut;

        [OneTimeSetUp]
        public void SetUp()
        {
            FilterSettings.Reset();
            FilterSettings.AddDefaults();
            FilterSettings.CheckSettings();
            _sut = new TestContextFilter();
        }

        [Test]
        // Period check
        [TestCase("a.b", FilterType.Schema, true)]
        [TestCase("a.b", FilterType.Table, true)]
        [TestCase("a.b", FilterType.Column, false)] // Period allowed
        [TestCase("a.b", FilterType.StoredProcedure, true)]

        // Schema Regex
        [TestCase("dbo",     FilterType.Schema, false)]
        [TestCase("events",  FilterType.Schema, false)]
        [TestCase("dbox",    FilterType.Schema, true)]
        [TestCase("event",   FilterType.Schema, true)]
        [TestCase("eventsx", FilterType.Schema, true)]
        [TestCase("ab", FilterType.Schema, true)]

        // Table regex
        [TestCase("Customerbilling", FilterType.Table, true)]
        [TestCase("CustomerBilling", FilterType.Table, true)]
        [TestCase("CustomerBillingTable", FilterType.Table, true)]
        [TestCase("TestBilling", FilterType.Table, true)]
        [TestCase("Test Billing", FilterType.Table, true)]
        [TestCase("Customer", FilterType.Table, false)]
        [TestCase("Test Customer", FilterType.Table, true)]
        [TestCase("data_properties", FilterType.Table, true)]
        [TestCase("Customer_data_properties", FilterType.Table, false)]
        [TestCase("Customer_FR_table", FilterType.Table, true)]
        [TestCase("Customer_FR_table_data_properties", FilterType.Table, true)]
        [TestCase("ab", FilterType.Table, true)]

        // Column
        [TestCase("ab", FilterType.Column, false)]

        // Stored procedure
        [TestCase("ab", FilterType.StoredProcedure, false)]
        public void IsTypeExcluded(string name, FilterType filterType, bool expectedExclusion)
        {
            var item = CreateType(name, filterType);
            var isExcluded = _sut.IsExcluded(item);
            Assert.AreEqual(expectedExclusion, isExcluded);
        }

        private EntityName CreateType(string name, FilterType filterType)
        {
            switch (filterType)
            {
                case FilterType.Schema:
                    return new Schema(name);
                case FilterType.Table:
                    return new Table(null, new Schema("dbo"), name, false);
                case FilterType.Column:
                    return new Column { DbName = name };
                case FilterType.StoredProcedure:
                    return new StoredProcedure { Schema = new Schema("dbo"), DbName = name };
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterType), filterType, null);
            }
        }
    }
}