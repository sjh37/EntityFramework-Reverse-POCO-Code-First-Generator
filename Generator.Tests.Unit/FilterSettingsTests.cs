using Efrpg.Filtering;
using Efrpg.Readers;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture]
    [Category(Constants.CI)]
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
    
    [TestFixture]
    [Category(Constants.CI)]
    public class MinMaxValueCacheTests
    {
        [Test]
        [TestCase("sbyte",   "-128")]
        [TestCase("byte",    "0")]
        [TestCase("short",   "-32768")]
        [TestCase("ushort",  "0")]
        [TestCase("int",     "-2147483648")]
        [TestCase("uint",    "0")]
        [TestCase("long",    "-9223372036854775808")]
        [TestCase("ulong",   "0")]
        [TestCase("float",   "-3.402823E+38")]
        [TestCase("double",  "-1.79769313486232E+308")]
        [TestCase("decimal", "-79228162514264337593543950335")]
        public void GetMinValue(string type, string expected)
        {
            Assert.AreEqual(expected, MinMaxValueCache.GetMinValue(type));
        }
        
        [Test]
        [TestCase("sbyte",   "127")]
        [TestCase("byte",    "255")]
        [TestCase("short",   "32767")]
        [TestCase("ushort",  "65535")]
        [TestCase("int",     "2147483647")]
        [TestCase("uint",    "4294967295")]
        [TestCase("long",    "9223372036854775807")]
        [TestCase("ulong",   "18446744073709551615")]
        [TestCase("float",   "3.402823E+38")]
        [TestCase("double",  "1.79769313486232E+308")]
        [TestCase("decimal", "79228162514264337593543950335")]
        public void GetMaxValue(string type, string expected)
        {
            Assert.AreEqual(expected, MinMaxValueCache.GetMaxValue(type));
        }
    }
}