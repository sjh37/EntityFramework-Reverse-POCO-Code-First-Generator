using System.Collections.Generic;
using Efrpg.Filtering;
using Efrpg.Readers;

namespace Generator.Tests.Unit
{
    public class MultiContextSettingsPluginNorthwind : IMultiContextSettingsPlugin
    {
        public List<MultiContextSettings> ReadSettings()
        {
            var list = new List<MultiContextSettings>();

            var a = new MultiContextSettings
            {
                Name        = "NorthwindTest1",
                Description = "Unit test 1",
                BaseSchema  = "dbo",

                Tables           = new List<MultiContextTableSettings>(),
                StoredProcedures = new List<MultiContextStoredProcedureSettings>(),
                Functions        = new List<MultiContextFunctionSettings>(),
                Enumerations     = new List<EnumerationSettings>()
            };

            // Force a duplicate context Name, but have a different namespace
            var aCopy = new MultiContextSettings
            {
                Name        = "NorthwindTest1", Namespace = "Copy", Filename = "NorthwindTest1Copy",
                Description = "Unit test 1",
                BaseSchema  = "dbo",

                Tables           = new List<MultiContextTableSettings>(),
                StoredProcedures = new List<MultiContextStoredProcedureSettings>(),
                Functions        = new List<MultiContextFunctionSettings>(),
                Enumerations     = new List<EnumerationSettings>()
            };

            var b = new MultiContextSettings
            {
                Name        = "NorthwindTest2",
                Description = "Unit test 2",
                BaseSchema  = "dbo",

                Tables           = new List<MultiContextTableSettings>(),
                StoredProcedures = new List<MultiContextStoredProcedureSettings>(),
                Functions        = new List<MultiContextFunctionSettings>(),
                Enumerations     = new List<EnumerationSettings>()
            };

            // View
            a.Tables.Add(new MultiContextTableSettings
            {
                Name        = "AbcPluralTest",
                Description = "List of products used by Northwind",
                PluralName  = "AlphabeticalPluralTests",
                DbName      = "Alphabetical list of products",

                Columns = new List<MultiContextColumnSettings>
                {
                    new MultiContextColumnSettings { Name = "ProductKEY", DbName = "productid", IsPrimaryKey = true },
                    new MultiContextColumnSettings { Name = "ProductName" },
                    new MultiContextColumnSettings { Name = "Price", DbName = "UnitPrice" }
                }
            });

            a.Tables.Add(new MultiContextTableSettings
            {
                Name  = "Categories",

                Columns = new List<MultiContextColumnSettings>
                {
                    new MultiContextColumnSettings { Name = "CategoryID" },
                    new MultiContextColumnSettings { Name = "CategoryName" }
                }
            });

            a.Tables.Add(new MultiContextTableSettings
            {
                Name  = "Products",

                Columns = new List<MultiContextColumnSettings>
                {
                    new MultiContextColumnSettings { Name = "ProductId" },
                    new MultiContextColumnSettings { Name = "ProductName" }
                }
            });

            aCopy.Tables = a.Tables;

            b.Tables.Add(new MultiContextTableSettings
            {
                Name = "AlphabeticalListOfProducts",
                DbName = "Alphabetical list of products",

                Columns = new List<MultiContextColumnSettings>
                {
                    new MultiContextColumnSettings { Name = "ProductId", IsPrimaryKey = true },
                    new MultiContextColumnSettings { Name = "UnitPrice" },
                    new MultiContextColumnSettings { Name = "Discontinued" },
                }
            });
            b.Tables.Add(new MultiContextTableSettings
            {
                Name  = "QuarterlyOrder",
                DbName = "Quarterly Orders",

                Columns = new List<MultiContextColumnSettings>
                {
                    new MultiContextColumnSettings { Name = "CustomerId", IsPrimaryKey = true },
                    new MultiContextColumnSettings { Name = "City" }
                }
            });

            list.Add(a);
            list.Add(aCopy);
            list.Add(b);
            return list;
        }
    }
}