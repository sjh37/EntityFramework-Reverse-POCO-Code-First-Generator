using System;
using System.Collections.Generic;
using Efrpg;
using Efrpg.FileManagement;
using Efrpg.Generators;
using Efrpg.Readers;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    using Efrpg.Templates;

    [TestFixture]
    [Category(Constants.CI)]
    public class IndexModelBuilderTests
    {
        private Table _orderDetails;

        [OneTimeSetUp]
        public void SetUp()
        {
            _orderDetails = new Table(null, new Schema("dbo"), "order details", false)
            {
                NameHumanCase = "OrderDetails",
                Indexes = new List<RawIndex>
                {
                    new RawIndex("dbo", "Order Details", "PK_Order_Details",      1, "OrderID",   2, true,  true,  false, true),
                    new RawIndex("dbo", "Order Details", "OrderID",               1, "OrderID",   1, false, false, false, false),
                    new RawIndex("dbo", "Order Details", "OrdersOrder_Details",   1, "OrderID",   1, false, false, false, false),
                    new RawIndex("dbo", "Order Details", "PK_Order_Details",      2, "ProductID", 2, true,  true,  false, true),
                    new RawIndex("dbo", "Order Details", "ProductID",             1, "ProductID", 1, false, false, false, false),
                    new RawIndex("dbo", "Order Details", "ProductsOrder_Details", 1, "ProductID", 1, false, false, false, false),
                    new RawIndex("dbo", "Order Details", "PK_test",               1, "xxx",       1, true,  true,  false, true)
                }
            };

            var orderId = new Column
            {
                ParentTable = _orderDetails,
                DbName = "OrderID",
                NameHumanCase = "OrderID",
                Config = "test",
                Indexes = new List<RawIndex>
                {
                    _orderDetails.Indexes[0],// new RawIndex("dbo", "Order Details", "PK_Order_Details",    1, "OrderID", 2, true,  true,  false, true),
                    _orderDetails.Indexes[1],// new RawIndex("dbo", "Order Details", "OrderID",             1, "OrderID", 1, false, false, false, false),
                    _orderDetails.Indexes[2],// new RawIndex("dbo", "Order Details", "OrdersOrder_Details", 1, "OrderID", 1, false, false, false, false)
                }
            };
            _orderDetails.Columns.Add(orderId);

            var productId = new Column
            {
                ParentTable = _orderDetails,
                DbName = "ProductID",
                NameHumanCase = "ProductID",
                Config = "test",
                IsNullable = true,
                Indexes = new List<RawIndex>
                {
                    _orderDetails.Indexes[3],//new RawIndex("dbo", "Order Details", "PK_Order_Details",      2, "ProductID", 2, true,  true,  false, true),
                    _orderDetails.Indexes[4],//new RawIndex("dbo", "Order Details", "ProductID",             1, "ProductID", 1, false, false, false, false),
                    _orderDetails.Indexes[5] //new RawIndex("dbo", "Order Details", "ProductsOrder_Details", 1, "ProductID", 1, false, false, false, false)
                }
            };
            _orderDetails.Columns.Add(productId);

            var test = new Column
            {
                ParentTable = _orderDetails,
                DbName = "xxx",
                NameHumanCase = "test",
                IsNullable = true,
                Config = "test",
                Indexes = new List<RawIndex>
                {
                    _orderDetails.Indexes[6] //new RawIndex("dbo", "Order Details", "PK_test", 1, "xxx", 1, true, true, false, true)
                }
            };
            _orderDetails.Columns.Add(test);
        }

        [Test]
        public void Ef6ModelBuilder()
        {
            // Arrange
            var fileManagement = new FileManagementService(new GeneratedTextTransformation());
            var sut = new GeneratorEf6(fileManagement, typeof(NullFileManager));

            // Act
            var list = new List<string>();
            foreach (var column in _orderDetails.Columns)
            {
                var s = sut.IndexModelBuilder(column);
                if(!string.IsNullOrWhiteSpace(s))
                    list.Add(s);
                Console.WriteLine(s);
            }

            // Assert
            Assert.AreEqual(2, list.Count);

            Assert.AreEqual(@"modelBuilder.Entity<OrderDetails>()
            .Property(e => e.OrderID)
            .HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new[]
                {
                    new IndexAttribute(""OrderID"", 1),
                    new IndexAttribute(""OrdersOrder_Details"", 1)
                }));", list[0].Trim());

            Assert.AreEqual(@"modelBuilder.Entity<OrderDetails>()
            .Property(e => e.ProductID)
            .HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new[]
                {
                    new IndexAttribute(""ProductID"", 1),
                    new IndexAttribute(""ProductsOrder_Details"", 1)
                }));", list[1].Trim());
        }

        [Test]
        [TestCase(TemplateType.Ef6,     "HasName")]
        [TestCase(TemplateType.EfCore6, "HasDatabaseName")]
        [TestCase(TemplateType.EfCore7, "HasDatabaseName")]
        [TestCase(TemplateType.EfCore8, "HasDatabaseName")]
        public void EfCoreModelBuilder(TemplateType templateType, string hasName)
        {
            // Arrange
            var fileManagement = new FileManagementService(new GeneratedTextTransformation());
            var sut = new GeneratorEfCore(fileManagement, typeof(NullFileManager));
            Settings.TemplateType = templateType;

            // Act
            var list = sut.IndexModelBuilder(_orderDetails);
            foreach (var str in list)
                Console.WriteLine(str);

            // Assert
            Assert.AreEqual(4, list.Count);

            Assert.AreEqual($@"builder.HasIndex(x => x.OrderID).{hasName}(""OrderID"");",                 list[0]);
            Assert.AreEqual($@"builder.HasIndex(x => x.OrderID).{hasName}(""OrdersOrder_Details"");",     list[1]);
            Assert.AreEqual($@"builder.HasIndex(x => x.ProductID).{hasName}(""ProductID"");",             list[2]);
            Assert.AreEqual($@"builder.HasIndex(x => x.ProductID).{hasName}(""ProductsOrder_Details"");", list[3]);
        }
    }
}