using System.Collections.Generic;
using Efrpg;
using Efrpg.Filtering;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture]
    [Category(Constants.CI)]
    public class MultiContextFilterTests
    {
        private MultiContextFilter SUT;

        [SetUp]
        public void BeforeEach()
        {
            var settings = new MultiContextSettings
            {
                Name             = "a",
                Description      = "b",
                BaseSchema       = "c",
                Tables           = new List<MultiContextTableSettings>(),
                StoredProcedures = new List<MultiContextStoredProcedureSettings>(),
                Functions        = new List<MultiContextFunctionSettings>(),
                Enumerations     = new List<EnumerationSettings>()
            };

            settings.Tables.Add(new MultiContextTableSettings
            {
                Name        = "view1",
                Description = "Hello",
                PluralName  = "viewOnes",
                DbName      = "view_1",

                Columns = new List<MultiContextColumnSettings>
                {
                    new() { Name = "null",  DbName = null, IsPrimaryKey = null,  EnumType = null,       OverrideModifier = null  },
                    new() { Name = "blank", DbName = "",   IsPrimaryKey = false, EnumType = "",         OverrideModifier = false },
                    new() { Name = "d",     DbName = "dd", IsPrimaryKey = true,  EnumType = "someEnum", OverrideModifier = true  },
                    new() { Name = "e",     IsPrimaryKey = true },
                    new() { Name = "f",     IsPrimaryKey = false }
                }
            });

            settings.Tables.Add(new MultiContextTableSettings
            {
                Name        = "view2",
                Description = null,
                PluralName  = null,
                DbName      = null,

                Columns = new List<MultiContextColumnSettings>
                {
                    new()  { Name = "null2",  DbName = null,  IsPrimaryKey = null,  EnumType = null,        OverrideModifier = null  },
                    new()  { Name = "blank2", DbName = "",    IsPrimaryKey = false, EnumType = "",          OverrideModifier = false },
                    new() { Name = "dd",     DbName = "ddd", IsPrimaryKey = true,  EnumType = "someEnum2", OverrideModifier = true  },
                    new() { Name = "ee",     IsPrimaryKey = true },
                    new() { Name = "ff",     IsPrimaryKey = false }
                }
            });

            settings.Tables.Add(new MultiContextTableSettings
            {
                Name = "schemaTest",
                DbName = "Accounting.schema_test",
                Columns = new List<MultiContextColumnSettings>
                {
                    new() { Name = "gg", DbName = null,  IsPrimaryKey = null,  EnumType = null,        OverrideModifier = null  },
                    new() { Name = "hh", DbName = "h_h", IsPrimaryKey = false, EnumType = "",          OverrideModifier = false }
                }
            });

            settings.Tables.Add(new MultiContextTableSettings
            {
                Name = "schemaTest2",
                DbName = "Events.schema_test2",
                Columns = new List<MultiContextColumnSettings>
                {
                    new() { Name = "ii",  DbName = null,  IsPrimaryKey = null,  EnumType = null,        OverrideModifier = null  },
                    new() { Name = "jj", DbName = "j_j",  IsPrimaryKey = false, EnumType = "",          OverrideModifier = false }
                }
            });

            settings.StoredProcedures.Add(new MultiContextStoredProcedureSettings
            {
                Name = "sp1",
                DbName = null,
                ReturnModel = null
            });

            settings.StoredProcedures.Add(new MultiContextStoredProcedureSettings
            {
                Name        = "sp2",
                DbName      = "SpSchema.sp2_test",
                ReturnModel = "sp2ReturnModelName"
            });

            settings.StoredProcedures.Add(new MultiContextStoredProcedureSettings
            {
                Name        = "sp3",
                DbName      = "sp3_test",
                ReturnModel = null
            });

            settings.Enumerations.Add(new EnumerationSettings
            {
                Name       = "RequestTypeEnum",
                Table      = "tblReqTypes",
                ValueField = "TypeID",
                NameField  = "TypeName"
            });

            settings.Enumerations.Add(new EnumerationSettings
            {
                Name       = "TestEnum",
                Table      = "unknown",
                ValueField = "",
                NameField  = ""
            });

            SUT = new MultiContextFilter(settings);
        }

        [TestCase("", true)]
        [TestCase("dbo", true)]
        [TestCase("DBO", true)]
        [TestCase("accounting", false)]
        [TestCase("events", false)]
        [TestCase("c", false)]
        [TestCase("C", false)]
        public void IsSchemaExcluded(string schema, bool expected)
        {
            // Arrange
            var item = new Schema(schema);
            
            // Act
            var result = SUT.IsExcluded(item);
            
            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]                                 // Correct: Schema   Name    NameHumanCase
        [TestCase("", "", "", true)]
        [TestCase("dbo", "x", "x", true)]
        [TestCase("dbo", "view1", "x", true)]           //          x
        [TestCase("c", "x", "x", true)]                 // x
        [TestCase("c", "view1", "x", false)]            // x        x
        [TestCase("c", "viewOnes", "x", false)]         // x        x
        [TestCase("c", "x", "viewOnes", false)]         // x                x
        [TestCase("c", "viewOnes", "viewOnes", false)]  // x        x       x
        [TestCase("", "viewOnes", "viewOnes", true)]    //          x       x
        [TestCase("x", "viewOnes", "viewOnes", true)]   //          x       x
        [TestCase("c", "x", "view1", false)]            // x
        [TestCase("c", "x", "view_1", false)]           // x                x
        [TestCase("c", "viewOnes ", "viewOnes", false)] // x                x
        [TestCase("c", "viewOnes ", "viewOnes ", true)] // x
        [TestCase("c", "viewOnes", "x", false)]         // x                x
        [TestCase("events", "viewOnes", null, true)]    // x
        [TestCase("accounting", "viewOnes", null, true)]// x
        public void IsTableExcluded(string schema, string name, string nameHumanCase, bool expected)
        {
            // Arrange
            var t = new Table(SUT, new Schema(schema), name, false) { NameHumanCase = nameHumanCase };
            
            // Act
            var result = SUT.IsExcluded(t);
            
            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase("", "", "", "", "", true)]
        [TestCase("c", "view1", "", "", "", true)]
        [TestCase("c", "view1", "x", "", "", true)]
        [TestCase("c", "view1", "x", "", "x", true)]
        [TestCase("c", "view1", "x", "x", "x", true)]
        [TestCase("c", "view1", "x", "x", "", true)]
        [TestCase("c", "view1", "viewOnes", "d", "x", false)]
        [TestCase("c", "view2", "x", "x", "dd", false)]
        [TestCase("c", "view2", "x", "x", "ddd", false)]
        [TestCase("c", "view2", "x", "dd", "ddd", false)]
        [TestCase("c", "x", "view_1", "d", "dd", false)]
        [TestCase("c", "x", "view_1", "d", "x", false)]
        [TestCase("c", "x", "viewOnes", "d", "dd", false)]
        [TestCase("c", "x", "viewOnes", "ee", "x", true)]
        [TestCase("c", "x", "viewOnes", "e", "x", false)]
        [TestCase("c", "x", "view2", "x", "dd", false)]
        [TestCase("c", "x", "view2", "dd", "x", false)]
        [TestCase("c", "x", "view2", "x", "ddd", false)]
        [TestCase("c", "view2", "x", "x", "dd", false)]
        [TestCase("events", "schemaTest2", "viewOnes", "e", "x", true)]
        [TestCase("events", "schemaTest2", "x", "e", "x", true)]
        [TestCase("events", "x", "schema_test2", "e", "x", true)]
        [TestCase("events", "x", "schema_test2", "ii", "x", false)]
        [TestCase("events", "x", "schema_test2", "x", "j_j", false)]
        [TestCase("x", "x", "viewOnes", "e", "x", true)]
        [TestCase("x", "view2", "viewOnes", "e", "x", true)]
        [TestCase("x", "view2", "viewOnes", "ee", "x", true)]
        [TestCase("x", "view2", "x", "ee", "x", true)]
        [TestCase("x", "view2", "x", "ee", "x", true)]
        public void IsColumnExcluded(string schema, string tableName, string tableNameHumanCase, string columnName, string columnNameHumanCase, bool expected)
        {
            // Arrange
            var t = new Table(SUT, new Schema(schema), tableName, false) { NameHumanCase = tableNameHumanCase };
            var c = new Column
            {
                ParentTable   = t,
                DbName          = columnName,
                NameHumanCase = columnNameHumanCase
            };

            // Act
            var result = SUT.IsExcluded(c);
            
            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]                                 // Correct: Schema   Name    NameHumanCase
        [TestCase("", "", "", true)]
        [TestCase("dbo", "x", "x", true)]
        [TestCase("dbo", "sp1", "x", true)]             //          x
        [TestCase("c", "x", "x", true)]                 // x
        [TestCase("c", "sp1", "x", false)]              // x        x
        [TestCase("c", "sp2", "x", true)]               //          x
        [TestCase("c", "sp3", "x", false)]              // x        x
        [TestCase("c", "x", "sp1", false)]              // x        x
        [TestCase("SpSchema", "sp2_test", "x", false)]  // x        x
        [TestCase("SpSchema", "sp2", "sp2_test", false)]// x        x       x
        [TestCase("SpSchema", "x", "sp2_test", false)]  // x                x
        [TestCase("accounting", "sp2_test", "x", true)] //          x
        [TestCase("events", "sp2_test", "x", true)]     //          x
        public void IsStoredProcedureExcluded(string schema, string name, string nameHumanCase, bool expected)
        {
            // Arrange
            var sp = new StoredProcedure { Schema = new Schema(schema), DbName = name, NameHumanCase = nameHumanCase, IsStoredProcedure = true };
            
            // Act
            var result = SUT.IsExcluded(sp);
            
            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(null, null, null)]
        [TestCase("", null, "")]
        [TestCase("", "", "")]
        [TestCase("x", "dbo", "x")]
        [TestCase("view_1", "dbo", "view_1")]
        [TestCase("view_1", "", "view_1")]
        [TestCase("view_1", null, "view_1")]
        [TestCase("view_1", "c", "view1")]
        [TestCase("view2", "c", "view2")]
        [TestCase("view2", "dbo", "view2")]
        public void TableRename(string name, string schema, string expected)
        {
            // Act
            var result1 = SUT.TableRename(name, schema, true);
            var result2 = SUT.TableRename(name, schema, false);

            // Assert
            Assert.AreEqual(expected, result1);
            Assert.AreEqual(expected, result2);
        }

        [Test]
        [TestCase("c", "view1", "null", null, false, "string", false, false, "string", false, null)]
        [TestCase("c", "view1", "null", null, true, "int", false, true, "int", false, null)]
        [TestCase("c", "view1", "null", null, false, "int", true, false, "int", true, null)]

        [TestCase("c", "view1", "d", null, false, "string", false, true, "someEnum", true, null)]
        [TestCase("c", "view1", "d", null, true, "int", false, true, "someEnum", true, null)]
        [TestCase("c", "view1", "d", null, false, "int", true, true, "someEnum", true, null)]

        [TestCase("c", "view1", "d", "test", false, "string", false, true, "someEnum", true, "(someEnum) test")]
        [TestCase("c", "view1", "d", "test", true, "int", false, true, "someEnum", true, "(someEnum) test")]
        [TestCase("c", "view1", "d", "test", false, "int", true, true, "someEnum", true, "(someEnum) test")]

        [TestCase("c", "view1", "e", null, false, "int", false, true, "int", false, null)]
        [TestCase("c", "view1", "e", null, true, "int", false, true, "int", false, null)]

        [TestCase("c", "view1", "f", null, false, "int", false, false, "int", false, null)]
        [TestCase("c", "view1", "f", null, true, "int", false, false, "int", false, null)]
        public void UpdateColumn(string schema, string tableName, string columnName, string columnDefault,
            bool isPrimaryKey, string propertyType, bool overrideModifier,
            bool expectedIsPrimaryKey, string expectedPropertyType, bool expectedOverrideModifier, string expectedDefault)
        {
            // Arrange
            var t = new Table(SUT, new Schema(schema), tableName, false) { NameHumanCase = tableName };
            var c = new Column
            {
                ParentTable      = t,
                DbName             = columnName,
                NameHumanCase    = columnName,
                IsPrimaryKey     = isPrimaryKey,
                PropertyType     = propertyType,
                OverrideModifier = overrideModifier,
                Default          = columnDefault
            };

            // Act
            SUT.UpdateColumn(c, t);

            // Assert
            Assert.AreEqual(expectedIsPrimaryKey,     c.IsPrimaryKey);
            Assert.AreEqual(expectedPropertyType,     c.PropertyType);
            Assert.AreEqual(expectedOverrideModifier, c.OverrideModifier);
            Assert.AreEqual(expectedDefault,          c.Default);
        }
    }
}