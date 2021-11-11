using System.Collections.Generic;
using Efrpg.Filtering;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture]
    [Category(Constants.CI)]
    public class MultiContextSettingsTests
    {
        [Test]
        public void IncludeTests()
        {
            var s = new MultiContextSettings
            {
                Tables           = new List<MultiContextTableSettings>(),
                StoredProcedures = new List<MultiContextStoredProcedureSettings>(),
                Functions        = new List<MultiContextFunctionSettings>(),
                Enumerations     = new List<EnumerationSettings>()
            };
            Assert.AreEqual(false, s.IncludeViews());
            Assert.AreEqual(false, s.IncludeStoredProcedures());
            Assert.AreEqual(false, s.IncludeFunctions());

            s.Tables.Add(null);
            Assert.AreEqual(true, s.IncludeViews());
            Assert.AreEqual(false, s.IncludeStoredProcedures());
            Assert.AreEqual(false, s.IncludeFunctions());

            s.Tables.RemoveAll(x => true);
            s.StoredProcedures.Add(null);
            Assert.AreEqual(false, s.IncludeViews());
            Assert.AreEqual(true, s.IncludeStoredProcedures());
            Assert.AreEqual(false, s.IncludeFunctions());

            s.StoredProcedures.RemoveAll(x => true);
            s.Functions.Add(null);
            Assert.AreEqual(false, s.IncludeViews());
            Assert.AreEqual(false, s.IncludeStoredProcedures());
            Assert.AreEqual(true, s.IncludeFunctions());
        }
    }
}