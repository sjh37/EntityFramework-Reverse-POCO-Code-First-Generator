namespace Generator.Tests.Unit
{
    using Efrpg;
    using Efrpg.FileManagement;
    using Efrpg.Generators;
    using Efrpg.Readers;
    using Common;
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture, NonParallelizable]
    [Category(Constants.CI)]
    public class ViewTests
    {
        private List<RawTable> _rawTables;
        private GeneratorCustom _sut;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var fileManagement = new FileManagementService(new GeneratedTextTransformation());
            var databaseReader = new FakeDatabaseReader();

            _rawTables = new List<RawTable>();
            _sut = new GeneratorCustom(fileManagement, typeof(NullFileManager));
            _sut.Init(databaseReader, string.Empty);
        }

        [Test]
        [TestCase("a.b.c")]
        [TestCase("a b c")]
        public void InvalidSchema(string schema)
        {
            _rawTables.Add(new RawTable(schema, "test", false, 0, "int", false, 1, 1, 1, false, false, false, 0, false, 1, true, false, null, 1, "name", "123"));

            _sut.LoadTables();
        }
        
        [Test]
        [TestCase("view.with.multiple.periods")]
        [TestCase("view with multiple spaces")]
        public void InvalidName(string name)
        {
            _rawTables.Add(new RawTable("dbo", name, false, 0, "int", false, 1, 1, 1, false, false, false, 0, false, 1, true, false, null, 1, "name", "123"));
            _sut.LoadTables();
        }

        // TODO add TestCases above for No primary key, with invalid schema & invalid name
    }
}