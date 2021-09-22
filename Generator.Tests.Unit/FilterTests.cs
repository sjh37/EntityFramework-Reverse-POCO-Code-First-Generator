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
        [TestCase("ab",      FilterType.Schema, true)]

        // Table regex
        [TestCase("dbo.Customerbilling",                   FilterType.Table, true)]
        [TestCase("dbo.CustomerBilling",                   FilterType.Table, true)]
        [TestCase("dbo.CustomerBillingTable",              FilterType.Table, true)]
        [TestCase("dbo.TestBilling",                       FilterType.Table, true)]
        [TestCase("dbo.Test Billing",                      FilterType.Table, true)]
        [TestCase("dbo.Customer",                          FilterType.Table, false)]
        [TestCase("reports.Customer",                      FilterType.Table, true)]
        [TestCase("dbo.Test Customer",                     FilterType.Table, true)]
        [TestCase("reports.Test Customer",                 FilterType.Table, true)]
        [TestCase("dbo.data_properties",                   FilterType.Table, true)]
        [TestCase("dbo.Customer_data_properties",          FilterType.Table, false)]
        [TestCase("dbo.Customer_FR_table",                 FilterType.Table, true)]
        [TestCase("dbo.Customer_FR_table_data_properties", FilterType.Table, true)]
        [TestCase("dbo.Order",                             FilterType.Table, false)]
        [TestCase("events.OrderThing",                     FilterType.Table, false)]
        [TestCase("reports.OrderReport",                   FilterType.Table, true)]
        [TestCase("dbo.OrderItem",                         FilterType.Table, false)]
        [TestCase("dbo.ab",                                FilterType.Table, true)]

        // Column
        [TestCase("ab", FilterType.Column, false)]

        // Stored procedure
        [TestCase("ab", FilterType.StoredProcedure, false)]

        // Enum
        [TestCase("Enum.PriceType", FilterType.EnumTable, false)]
        [TestCase("Enum.ProductType", FilterType.EnumTable, true)]
        public void IsTypeExcluded(string name, FilterType filterType, bool expectedExclusion)
        {
            var item = CreateType(name, filterType);
            var isExcluded = _sut.IsExcluded(item);
            Assert.AreEqual(expectedExclusion, isExcluded);
        }

        private static EntityName CreateType(string name, FilterType filterType)
        {
            string[] split;
            switch (filterType)
            {
                case FilterType.Schema:
                    return new Schema(name);
                case FilterType.Table:
                    split = name.Split('.');
                    return new Table(null, new Schema(split[0]), split[1], false);
                case FilterType.Column:
                    return new Column { DbName = name };
                case FilterType.StoredProcedure:
                    return new StoredProcedure { Schema = new Schema("dbo"), DbName = name };
                case FilterType.EnumSchema:
                    return new EnumSchemaSource(name);
                case FilterType.EnumTable:
                    split = name.Split('.');
                    return new EnumTableSource(new EnumSchemaSource(split[0]), split[1]);
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterType), filterType, null);
            }
        }
    }
}