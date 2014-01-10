

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Configuration file:     "EntityFramework.Reverse.POCO.Generator\App.config"
//     Connection String Name: "MyDbContext"
//     Connection String:      "Data Source=(local);Initial Catalog=northwind;Integrated Security=True;Application Name=EntityFramework Reverse POCO Generator"

// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace EntityFramework_Reverse_POCO_Generator
{
    // ************************************************************************
    // Unit of work
    public interface IMyDbContext : IDisposable
    {
        IDbSet<AlphabeticalListOfProducts> AlphabeticalListOfProducts { get; set; } // Alphabetical list of products
        IDbSet<Categories> Categories { get; set; } // Categories
        IDbSet<CategorySalesFor1997> CategorySalesFor1997 { get; set; } // Category Sales for 1997
        IDbSet<CurrentProductList> CurrentProductList { get; set; } // Current Product List
        IDbSet<CustomerAndSuppliersByCity> CustomerAndSuppliersByCity { get; set; } // Customer and Suppliers by City
        IDbSet<CustomerDemographics> CustomerDemographics { get; set; } // CustomerDemographics
        IDbSet<Customers> Customers { get; set; } // Customers
        IDbSet<Employees> Employees { get; set; } // Employees
        IDbSet<Invoices> Invoices { get; set; } // Invoices
        IDbSet<OrderDetails> OrderDetails { get; set; } // Order Details
        IDbSet<OrderDetailsExtended> OrderDetailsExtended { get; set; } // Order Details Extended
        IDbSet<Orders> Orders { get; set; } // Orders
        IDbSet<OrdersQry> OrdersQry { get; set; } // Orders Qry
        IDbSet<OrderSubtotals> OrderSubtotals { get; set; } // Order Subtotals
        IDbSet<Products> Products { get; set; } // Products
        IDbSet<ProductsAboveAveragePrice> ProductsAboveAveragePrice { get; set; } // Products Above Average Price
        IDbSet<ProductSalesFor1997> ProductSalesFor1997 { get; set; } // Product Sales for 1997
        IDbSet<ProductsByCategory> ProductsByCategory { get; set; } // Products by Category
        IDbSet<Region> Region { get; set; } // Region
        IDbSet<SalesByCategory> SalesByCategory { get; set; } // Sales by Category
        IDbSet<SalesTotalsByAmount> SalesTotalsByAmount { get; set; } // Sales Totals by Amount
        IDbSet<Shippers> Shippers { get; set; } // Shippers
        IDbSet<SummaryOfSalesByQuarter> SummaryOfSalesByQuarter { get; set; } // Summary of Sales by Quarter
        IDbSet<SummaryOfSalesByYear> SummaryOfSalesByYear { get; set; } // Summary of Sales by Year
        IDbSet<Suppliers> Suppliers { get; set; } // Suppliers
        IDbSet<Territories> Territories { get; set; } // Territories

        int SaveChanges();
    }

    // ************************************************************************
    // Database context
    public class MyDbContext : DbContext, IMyDbContext
    {
        public IDbSet<AlphabeticalListOfProducts> AlphabeticalListOfProducts { get; set; } // Alphabetical list of products
        public IDbSet<Categories> Categories { get; set; } // Categories
        public IDbSet<CategorySalesFor1997> CategorySalesFor1997 { get; set; } // Category Sales for 1997
        public IDbSet<CurrentProductList> CurrentProductList { get; set; } // Current Product List
        public IDbSet<CustomerAndSuppliersByCity> CustomerAndSuppliersByCity { get; set; } // Customer and Suppliers by City
        public IDbSet<CustomerDemographics> CustomerDemographics { get; set; } // CustomerDemographics
        public IDbSet<Customers> Customers { get; set; } // Customers
        public IDbSet<Employees> Employees { get; set; } // Employees
        public IDbSet<Invoices> Invoices { get; set; } // Invoices
        public IDbSet<OrderDetails> OrderDetails { get; set; } // Order Details
        public IDbSet<OrderDetailsExtended> OrderDetailsExtended { get; set; } // Order Details Extended
        public IDbSet<Orders> Orders { get; set; } // Orders
        public IDbSet<OrdersQry> OrdersQry { get; set; } // Orders Qry
        public IDbSet<OrderSubtotals> OrderSubtotals { get; set; } // Order Subtotals
        public IDbSet<Products> Products { get; set; } // Products
        public IDbSet<ProductsAboveAveragePrice> ProductsAboveAveragePrice { get; set; } // Products Above Average Price
        public IDbSet<ProductSalesFor1997> ProductSalesFor1997 { get; set; } // Product Sales for 1997
        public IDbSet<ProductsByCategory> ProductsByCategory { get; set; } // Products by Category
        public IDbSet<Region> Region { get; set; } // Region
        public IDbSet<SalesByCategory> SalesByCategory { get; set; } // Sales by Category
        public IDbSet<SalesTotalsByAmount> SalesTotalsByAmount { get; set; } // Sales Totals by Amount
        public IDbSet<Shippers> Shippers { get; set; } // Shippers
        public IDbSet<SummaryOfSalesByQuarter> SummaryOfSalesByQuarter { get; set; } // Summary of Sales by Quarter
        public IDbSet<SummaryOfSalesByYear> SummaryOfSalesByYear { get; set; } // Summary of Sales by Year
        public IDbSet<Suppliers> Suppliers { get; set; } // Suppliers
        public IDbSet<Territories> Territories { get; set; } // Territories

        static MyDbContext()
        {
            Database.SetInitializer<MyDbContext>(null);
        }

        public MyDbContext()
            : base("Name=MyDbContext")
        {
        }

        public MyDbContext(string connectionString) : base(connectionString)
        {
        }

        public MyDbContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AlphabeticalListOfProductsConfiguration());
            modelBuilder.Configurations.Add(new CategoriesConfiguration());
            modelBuilder.Configurations.Add(new CategorySalesFor1997Configuration());
            modelBuilder.Configurations.Add(new CurrentProductListConfiguration());
            modelBuilder.Configurations.Add(new CustomerAndSuppliersByCityConfiguration());
            modelBuilder.Configurations.Add(new CustomerDemographicsConfiguration());
            modelBuilder.Configurations.Add(new CustomersConfiguration());
            modelBuilder.Configurations.Add(new EmployeesConfiguration());
            modelBuilder.Configurations.Add(new InvoicesConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailsConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailsExtendedConfiguration());
            modelBuilder.Configurations.Add(new OrdersConfiguration());
            modelBuilder.Configurations.Add(new OrdersQryConfiguration());
            modelBuilder.Configurations.Add(new OrderSubtotalsConfiguration());
            modelBuilder.Configurations.Add(new ProductsConfiguration());
            modelBuilder.Configurations.Add(new ProductsAboveAveragePriceConfiguration());
            modelBuilder.Configurations.Add(new ProductSalesFor1997Configuration());
            modelBuilder.Configurations.Add(new ProductsByCategoryConfiguration());
            modelBuilder.Configurations.Add(new RegionConfiguration());
            modelBuilder.Configurations.Add(new SalesByCategoryConfiguration());
            modelBuilder.Configurations.Add(new SalesTotalsByAmountConfiguration());
            modelBuilder.Configurations.Add(new ShippersConfiguration());
            modelBuilder.Configurations.Add(new SummaryOfSalesByQuarterConfiguration());
            modelBuilder.Configurations.Add(new SummaryOfSalesByYearConfiguration());
            modelBuilder.Configurations.Add(new SuppliersConfiguration());
            modelBuilder.Configurations.Add(new TerritoriesConfiguration());
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new AlphabeticalListOfProductsConfiguration(schema));
            modelBuilder.Configurations.Add(new CategoriesConfiguration(schema));
            modelBuilder.Configurations.Add(new CategorySalesFor1997Configuration(schema));
            modelBuilder.Configurations.Add(new CurrentProductListConfiguration(schema));
            modelBuilder.Configurations.Add(new CustomerAndSuppliersByCityConfiguration(schema));
            modelBuilder.Configurations.Add(new CustomerDemographicsConfiguration(schema));
            modelBuilder.Configurations.Add(new CustomersConfiguration(schema));
            modelBuilder.Configurations.Add(new EmployeesConfiguration(schema));
            modelBuilder.Configurations.Add(new InvoicesConfiguration(schema));
            modelBuilder.Configurations.Add(new OrderDetailsConfiguration(schema));
            modelBuilder.Configurations.Add(new OrderDetailsExtendedConfiguration(schema));
            modelBuilder.Configurations.Add(new OrdersConfiguration(schema));
            modelBuilder.Configurations.Add(new OrdersQryConfiguration(schema));
            modelBuilder.Configurations.Add(new OrderSubtotalsConfiguration(schema));
            modelBuilder.Configurations.Add(new ProductsConfiguration(schema));
            modelBuilder.Configurations.Add(new ProductsAboveAveragePriceConfiguration(schema));
            modelBuilder.Configurations.Add(new ProductSalesFor1997Configuration(schema));
            modelBuilder.Configurations.Add(new ProductsByCategoryConfiguration(schema));
            modelBuilder.Configurations.Add(new RegionConfiguration(schema));
            modelBuilder.Configurations.Add(new SalesByCategoryConfiguration(schema));
            modelBuilder.Configurations.Add(new SalesTotalsByAmountConfiguration(schema));
            modelBuilder.Configurations.Add(new ShippersConfiguration(schema));
            modelBuilder.Configurations.Add(new SummaryOfSalesByQuarterConfiguration(schema));
            modelBuilder.Configurations.Add(new SummaryOfSalesByYearConfiguration(schema));
            modelBuilder.Configurations.Add(new SuppliersConfiguration(schema));
            modelBuilder.Configurations.Add(new TerritoriesConfiguration(schema));
            return modelBuilder;
        }
    }

    // ************************************************************************
    // POCO classes

    // Alphabetical list of products
    public class AlphabeticalListOfProducts
    {
        public int ProductId { get; set; } // ProductID
        public string ProductName { get; set; } // ProductName
        public int? SupplierId { get; set; } // SupplierID
        public int? CategoryId { get; set; } // CategoryID
        public string QuantityPerUnit { get; set; } // QuantityPerUnit
        public decimal? UnitPrice { get; set; } // UnitPrice
        public short? UnitsInStock { get; set; } // UnitsInStock
        public short? UnitsOnOrder { get; set; } // UnitsOnOrder
        public short? ReorderLevel { get; set; } // ReorderLevel
        public bool Discontinued { get; set; } // Discontinued
        public string CategoryName { get; set; } // CategoryName
    }

    // Categories
    public class Categories
    {
        public int CategoryId { get; set; } // CategoryID (Primary key)
        public string CategoryName { get; set; } // CategoryName
        public string Description { get; set; } // Description
        public byte[] Picture { get; set; } // Picture

        // Reverse navigation
        public virtual ICollection<Products> Products { get; set; } // Products.FK_Products_Categories

        public Categories()
        {
            Products = new List<Products>();
        }
    }

    // Category Sales for 1997
    public class CategorySalesFor1997
    {
        public string CategoryName { get; set; } // CategoryName
        public decimal? CategorySales { get; set; } // CategorySales
    }

    // Current Product List
    public class CurrentProductList
    {
        public int ProductId { get; set; } // ProductID
        public string ProductName { get; set; } // ProductName
    }

    // Customer and Suppliers by City
    public class CustomerAndSuppliersByCity
    {
        public string City { get; set; } // City
        public string CompanyName { get; set; } // CompanyName
        public string ContactName { get; set; } // ContactName
        public string Relationship { get; set; } // Relationship
    }

    // CustomerDemographics
    public class CustomerDemographics
    {
        public string CustomerTypeId { get; set; } // CustomerTypeID (Primary key)
        public string CustomerDesc { get; set; } // CustomerDesc

        // Reverse navigation
        public virtual ICollection<Customers> Customers { get; set; } // Many to many mapping

        public CustomerDemographics()
        {
            Customers = new List<Customers>();
        }
    }

    // Customers
    public class Customers
    {
        public string CustomerId { get; set; } // CustomerID (Primary key)
        public string CompanyName { get; set; } // CompanyName
        public string ContactName { get; set; } // ContactName
        public string ContactTitle { get; set; } // ContactTitle
        public string Address { get; set; } // Address
        public string City { get; set; } // City
        public string Region { get; set; } // Region
        public string PostalCode { get; set; } // PostalCode
        public string Country { get; set; } // Country
        public string Phone { get; set; } // Phone
        public string Fax { get; set; } // Fax

        // Reverse navigation
        public virtual ICollection<CustomerDemographics> CustomerDemographics { get; set; } // Many to many mapping
        public virtual ICollection<Orders> Orders { get; set; } // Orders.FK_Orders_Customers

        public Customers()
        {
            Orders = new List<Orders>();
            CustomerDemographics = new List<CustomerDemographics>();
        }
    }

    // Employees
    public class Employees
    {
        public int EmployeeId { get; set; } // EmployeeID (Primary key)
        public string LastName { get; set; } // LastName
        public string FirstName { get; set; } // FirstName
        public string Title { get; set; } // Title
        public string TitleOfCourtesy { get; set; } // TitleOfCourtesy
        public DateTime? BirthDate { get; set; } // BirthDate
        public DateTime? HireDate { get; set; } // HireDate
        public string Address { get; set; } // Address
        public string City { get; set; } // City
        public string Region { get; set; } // Region
        public string PostalCode { get; set; } // PostalCode
        public string Country { get; set; } // Country
        public string HomePhone { get; set; } // HomePhone
        public string Extension { get; set; } // Extension
        public byte[] Photo { get; set; } // Photo
        public string Notes { get; set; } // Notes
        public int? ReportsTo { get; set; } // ReportsTo
        public string PhotoPath { get; set; } // PhotoPath

        // Reverse navigation
        public virtual ICollection<Employees> Employees2 { get; set; } // Employees.FK_Employees_Employees
        public virtual ICollection<Orders> Orders { get; set; } // Orders.FK_Orders_Employees
        public virtual ICollection<Territories> Territories { get; set; } // Many to many mapping

        // Foreign keys
        public virtual Employees Employees_ReportsTo { get; set; } //  FK_Employees_Employees

        public Employees()
        {
            Employees2 = new List<Employees>();
            Orders = new List<Orders>();
            Territories = new List<Territories>();
        }
    }

    // Invoices
    public class Invoices
    {
        public string ShipName { get; set; } // ShipName
        public string ShipAddress { get; set; } // ShipAddress
        public string ShipCity { get; set; } // ShipCity
        public string ShipRegion { get; set; } // ShipRegion
        public string ShipPostalCode { get; set; } // ShipPostalCode
        public string ShipCountry { get; set; } // ShipCountry
        public string CustomerId { get; set; } // CustomerID
        public string CustomerName { get; set; } // CustomerName
        public string Address { get; set; } // Address
        public string City { get; set; } // City
        public string Region { get; set; } // Region
        public string PostalCode { get; set; } // PostalCode
        public string Country { get; set; } // Country
        public string Salesperson { get; set; } // Salesperson
        public int OrderId { get; set; } // OrderID
        public DateTime? OrderDate { get; set; } // OrderDate
        public DateTime? RequiredDate { get; set; } // RequiredDate
        public DateTime? ShippedDate { get; set; } // ShippedDate
        public string ShipperName { get; set; } // ShipperName
        public int ProductId { get; set; } // ProductID
        public string ProductName { get; set; } // ProductName
        public decimal UnitPrice { get; set; } // UnitPrice
        public short Quantity { get; set; } // Quantity
        public float Discount { get; set; } // Discount
        public decimal? ExtendedPrice { get; set; } // ExtendedPrice
        public decimal? Freight { get; set; } // Freight
    }

    // Order Details
    public class OrderDetails
    {
        public int OrderId { get; set; } // OrderID (Primary key)
        public int ProductId { get; set; } // ProductID (Primary key)
        public decimal UnitPrice { get; set; } // UnitPrice
        public short Quantity { get; set; } // Quantity
        public float Discount { get; set; } // Discount

        // Foreign keys
        public virtual Orders Orders { get; set; } //  FK_Order_Details_Orders
        public virtual Products Products { get; set; } //  FK_Order_Details_Products

        public OrderDetails()
        {
            UnitPrice = 0m;
            Quantity = 1;
            Discount = 0;
        }
    }

    // Order Details Extended
    public class OrderDetailsExtended
    {
        public int OrderId { get; set; } // OrderID
        public int ProductId { get; set; } // ProductID
        public string ProductName { get; set; } // ProductName
        public decimal UnitPrice { get; set; } // UnitPrice
        public short Quantity { get; set; } // Quantity
        public float Discount { get; set; } // Discount
        public decimal? ExtendedPrice { get; set; } // ExtendedPrice
    }

    // Orders
    public class Orders
    {
        public int OrderId { get; set; } // OrderID (Primary key)
        public string CustomerId { get; set; } // CustomerID
        public int? EmployeeId { get; set; } // EmployeeID
        public DateTime? OrderDate { get; set; } // OrderDate
        public DateTime? RequiredDate { get; set; } // RequiredDate
        public DateTime? ShippedDate { get; set; } // ShippedDate
        public int? ShipVia { get; set; } // ShipVia
        public decimal? Freight { get; set; } // Freight
        public string ShipName { get; set; } // ShipName
        public string ShipAddress { get; set; } // ShipAddress
        public string ShipCity { get; set; } // ShipCity
        public string ShipRegion { get; set; } // ShipRegion
        public string ShipPostalCode { get; set; } // ShipPostalCode
        public string ShipCountry { get; set; } // ShipCountry

        // Reverse navigation
        public virtual ICollection<OrderDetails> OrderDetails { get; set; } // Many to many mapping

        // Foreign keys
        public virtual Customers Customers { get; set; } //  FK_Orders_Customers
        public virtual Employees Employees { get; set; } //  FK_Orders_Employees
        public virtual Shippers Shippers { get; set; } //  FK_Orders_Shippers

        public Orders()
        {
            Freight = 0m;
            OrderDetails = new List<OrderDetails>();
        }
    }

    // Orders Qry
    public class OrdersQry
    {
        public int OrderId { get; set; } // OrderID
        public string CustomerId { get; set; } // CustomerID
        public int? EmployeeId { get; set; } // EmployeeID
        public DateTime? OrderDate { get; set; } // OrderDate
        public DateTime? RequiredDate { get; set; } // RequiredDate
        public DateTime? ShippedDate { get; set; } // ShippedDate
        public int? ShipVia { get; set; } // ShipVia
        public decimal? Freight { get; set; } // Freight
        public string ShipName { get; set; } // ShipName
        public string ShipAddress { get; set; } // ShipAddress
        public string ShipCity { get; set; } // ShipCity
        public string ShipRegion { get; set; } // ShipRegion
        public string ShipPostalCode { get; set; } // ShipPostalCode
        public string ShipCountry { get; set; } // ShipCountry
        public string CompanyName { get; set; } // CompanyName
        public string Address { get; set; } // Address
        public string City { get; set; } // City
        public string Region { get; set; } // Region
        public string PostalCode { get; set; } // PostalCode
        public string Country { get; set; } // Country
    }

    // Order Subtotals
    public class OrderSubtotals
    {
        public int OrderId { get; set; } // OrderID
        public decimal? Subtotal { get; set; } // Subtotal
    }

    // Products
    public class Products
    {
        public int ProductId { get; set; } // ProductID (Primary key)
        public string ProductName { get; set; } // ProductName
        public int? SupplierId { get; set; } // SupplierID
        public int? CategoryId { get; set; } // CategoryID
        public string QuantityPerUnit { get; set; } // QuantityPerUnit
        public decimal? UnitPrice { get; set; } // UnitPrice
        public short? UnitsInStock { get; set; } // UnitsInStock
        public short? UnitsOnOrder { get; set; } // UnitsOnOrder
        public short? ReorderLevel { get; set; } // ReorderLevel
        public bool Discontinued { get; set; } // Discontinued

        // Reverse navigation
        public virtual ICollection<OrderDetails> OrderDetails { get; set; } // Many to many mapping

        // Foreign keys
        public virtual Categories Categories { get; set; } //  FK_Products_Categories
        public virtual Suppliers Suppliers { get; set; } //  FK_Products_Suppliers

        public Products()
        {
            UnitPrice = 0m;
            UnitsInStock = 0;
            UnitsOnOrder = 0;
            ReorderLevel = 0;
            Discontinued = false;
            OrderDetails = new List<OrderDetails>();
        }
    }

    // Products Above Average Price
    public class ProductsAboveAveragePrice
    {
        public string ProductName { get; set; } // ProductName
        public decimal? UnitPrice { get; set; } // UnitPrice
    }

    // Product Sales for 1997
    public class ProductSalesFor1997
    {
        public string CategoryName { get; set; } // CategoryName
        public string ProductName { get; set; } // ProductName
        public decimal? ProductSales { get; set; } // ProductSales
    }

    // Products by Category
    public class ProductsByCategory
    {
        public string CategoryName { get; set; } // CategoryName
        public string ProductName { get; set; } // ProductName
        public string QuantityPerUnit { get; set; } // QuantityPerUnit
        public short? UnitsInStock { get; set; } // UnitsInStock
        public bool Discontinued { get; set; } // Discontinued
    }

    // Region
    public class Region
    {
        public int RegionId { get; set; } // RegionID (Primary key)
        public string RegionDescription { get; set; } // RegionDescription

        // Reverse navigation
        public virtual ICollection<Territories> Territories { get; set; } // Territories.FK_Territories_Region

        public Region()
        {
            Territories = new List<Territories>();
        }
    }

    // Sales by Category
    public class SalesByCategory
    {
        public int CategoryId { get; set; } // CategoryID
        public string CategoryName { get; set; } // CategoryName
        public string ProductName { get; set; } // ProductName
        public decimal? ProductSales { get; set; } // ProductSales
    }

    // Sales Totals by Amount
    public class SalesTotalsByAmount
    {
        public decimal? SaleAmount { get; set; } // SaleAmount
        public int OrderId { get; set; } // OrderID
        public string CompanyName { get; set; } // CompanyName
        public DateTime? ShippedDate { get; set; } // ShippedDate
    }

    // Shippers
    public class Shippers
    {
        public int ShipperId { get; set; } // ShipperID (Primary key)
        public string CompanyName { get; set; } // CompanyName
        public string Phone { get; set; } // Phone

        // Reverse navigation
        public virtual ICollection<Orders> Orders { get; set; } // Orders.FK_Orders_Shippers

        public Shippers()
        {
            Orders = new List<Orders>();
        }
    }

    // Summary of Sales by Quarter
    public class SummaryOfSalesByQuarter
    {
        public DateTime? ShippedDate { get; set; } // ShippedDate
        public int OrderId { get; set; } // OrderID
        public decimal? Subtotal { get; set; } // Subtotal
    }

    // Summary of Sales by Year
    public class SummaryOfSalesByYear
    {
        public DateTime? ShippedDate { get; set; } // ShippedDate
        public int OrderId { get; set; } // OrderID
        public decimal? Subtotal { get; set; } // Subtotal
    }

    // Suppliers
    public class Suppliers
    {
        public int SupplierId { get; set; } // SupplierID (Primary key)
        public string CompanyName { get; set; } // CompanyName
        public string ContactName { get; set; } // ContactName
        public string ContactTitle { get; set; } // ContactTitle
        public string Address { get; set; } // Address
        public string City { get; set; } // City
        public string Region { get; set; } // Region
        public string PostalCode { get; set; } // PostalCode
        public string Country { get; set; } // Country
        public string Phone { get; set; } // Phone
        public string Fax { get; set; } // Fax
        public string HomePage { get; set; } // HomePage

        // Reverse navigation
        public virtual ICollection<Products> Products { get; set; } // Products.FK_Products_Suppliers

        public Suppliers()
        {
            Products = new List<Products>();
        }
    }

    // Territories
    public class Territories
    {
        public string TerritoryId { get; set; } // TerritoryID (Primary key)
        public string TerritoryDescription { get; set; } // TerritoryDescription
        public int RegionId { get; set; } // RegionID

        // Reverse navigation
        public virtual ICollection<Employees> Employees { get; set; } // Many to many mapping

        // Foreign keys
        public virtual Region Region { get; set; } //  FK_Territories_Region

        public Territories()
        {
            Employees = new List<Employees>();
        }
    }


    // ************************************************************************
    // POCO Configuration

    // Alphabetical list of products
    internal class AlphabeticalListOfProductsConfiguration : EntityTypeConfiguration<AlphabeticalListOfProducts>
    {
        public AlphabeticalListOfProductsConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Alphabetical list of products");
            HasKey(x => new { x.ProductId, x.ProductName, x.Discontinued, x.CategoryName });

            Property(x => x.ProductId).HasColumnName("ProductID").IsRequired();
            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasMaxLength(40);
            Property(x => x.SupplierId).HasColumnName("SupplierID").IsOptional();
            Property(x => x.CategoryId).HasColumnName("CategoryID").IsOptional();
            Property(x => x.QuantityPerUnit).HasColumnName("QuantityPerUnit").IsOptional().HasMaxLength(20);
            Property(x => x.UnitPrice).HasColumnName("UnitPrice").IsOptional().HasPrecision(19,4);
            Property(x => x.UnitsInStock).HasColumnName("UnitsInStock").IsOptional();
            Property(x => x.UnitsOnOrder).HasColumnName("UnitsOnOrder").IsOptional();
            Property(x => x.ReorderLevel).HasColumnName("ReorderLevel").IsOptional();
            Property(x => x.Discontinued).HasColumnName("Discontinued").IsRequired();
            Property(x => x.CategoryName).HasColumnName("CategoryName").IsRequired().HasMaxLength(15);
        }
    }

    // Categories
    internal class CategoriesConfiguration : EntityTypeConfiguration<Categories>
    {
        public CategoriesConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Categories");
            HasKey(x => x.CategoryId);

            Property(x => x.CategoryId).HasColumnName("CategoryID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CategoryName).HasColumnName("CategoryName").IsRequired().HasMaxLength(15);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(1073741823);
            Property(x => x.Picture).HasColumnName("Picture").IsOptional().HasMaxLength(2147483647);
        }
    }

    // Category Sales for 1997
    internal class CategorySalesFor1997Configuration : EntityTypeConfiguration<CategorySalesFor1997>
    {
        public CategorySalesFor1997Configuration(string schema = "dbo")
        {
            ToTable(schema + ".Category Sales for 1997");
            HasKey(x => x.CategoryName);

            Property(x => x.CategoryName).HasColumnName("CategoryName").IsRequired().HasMaxLength(15);
            Property(x => x.CategorySales).HasColumnName("CategorySales").IsOptional().HasPrecision(19,4);
        }
    }

    // Current Product List
    internal class CurrentProductListConfiguration : EntityTypeConfiguration<CurrentProductList>
    {
        public CurrentProductListConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Current Product List");
            HasKey(x => new { x.ProductId, x.ProductName });

            Property(x => x.ProductId).HasColumnName("ProductID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasMaxLength(40);
        }
    }

    // Customer and Suppliers by City
    internal class CustomerAndSuppliersByCityConfiguration : EntityTypeConfiguration<CustomerAndSuppliersByCity>
    {
        public CustomerAndSuppliersByCityConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Customer and Suppliers by City");
            HasKey(x => new { x.CompanyName, x.Relationship });

            Property(x => x.City).HasColumnName("City").IsOptional().HasMaxLength(15);
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasMaxLength(40);
            Property(x => x.ContactName).HasColumnName("ContactName").IsOptional().HasMaxLength(30);
            Property(x => x.Relationship).HasColumnName("Relationship").IsRequired().HasMaxLength(9);
        }
    }

    // CustomerDemographics
    internal class CustomerDemographicsConfiguration : EntityTypeConfiguration<CustomerDemographics>
    {
        public CustomerDemographicsConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".CustomerDemographics");
            HasKey(x => x.CustomerTypeId);

            Property(x => x.CustomerTypeId).HasColumnName("CustomerTypeID").IsRequired().HasMaxLength(10);
            Property(x => x.CustomerDesc).HasColumnName("CustomerDesc").IsOptional().HasMaxLength(1073741823);
        }
    }

    // Customers
    internal class CustomersConfiguration : EntityTypeConfiguration<Customers>
    {
        public CustomersConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Customers");
            HasKey(x => x.CustomerId);

            Property(x => x.CustomerId).HasColumnName("CustomerID").IsRequired().HasMaxLength(5);
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasMaxLength(40);
            Property(x => x.ContactName).HasColumnName("ContactName").IsOptional().HasMaxLength(30);
            Property(x => x.ContactTitle).HasColumnName("ContactTitle").IsOptional().HasMaxLength(30);
            Property(x => x.Address).HasColumnName("Address").IsOptional().HasMaxLength(60);
            Property(x => x.City).HasColumnName("City").IsOptional().HasMaxLength(15);
            Property(x => x.Region).HasColumnName("Region").IsOptional().HasMaxLength(15);
            Property(x => x.PostalCode).HasColumnName("PostalCode").IsOptional().HasMaxLength(10);
            Property(x => x.Country).HasColumnName("Country").IsOptional().HasMaxLength(15);
            Property(x => x.Phone).HasColumnName("Phone").IsOptional().HasMaxLength(24);
            Property(x => x.Fax).HasColumnName("Fax").IsOptional().HasMaxLength(24);
            HasMany(t => t.CustomerDemographics).WithMany(t => t.Customers).Map(m => 
            {
                m.ToTable("CustomerCustomerDemo");
                m.MapLeftKey("CustomerID");
                m.MapRightKey("CustomerTypeID");
            });
            HasMany(t => t.CustomerDemographics).WithMany(t => t.Customers).Map(m => 
            {
                m.ToTable("CustomerCustomerDemo");
                m.MapLeftKey("CustomerID");
                m.MapRightKey("CustomerTypeID");
            });
        }
    }

    // Employees
    internal class EmployeesConfiguration : EntityTypeConfiguration<Employees>
    {
        public EmployeesConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Employees");
            HasKey(x => x.EmployeeId);

            Property(x => x.EmployeeId).HasColumnName("EmployeeID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(20);
            Property(x => x.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(10);
            Property(x => x.Title).HasColumnName("Title").IsOptional().HasMaxLength(30);
            Property(x => x.TitleOfCourtesy).HasColumnName("TitleOfCourtesy").IsOptional().HasMaxLength(25);
            Property(x => x.BirthDate).HasColumnName("BirthDate").IsOptional();
            Property(x => x.HireDate).HasColumnName("HireDate").IsOptional();
            Property(x => x.Address).HasColumnName("Address").IsOptional().HasMaxLength(60);
            Property(x => x.City).HasColumnName("City").IsOptional().HasMaxLength(15);
            Property(x => x.Region).HasColumnName("Region").IsOptional().HasMaxLength(15);
            Property(x => x.PostalCode).HasColumnName("PostalCode").IsOptional().HasMaxLength(10);
            Property(x => x.Country).HasColumnName("Country").IsOptional().HasMaxLength(15);
            Property(x => x.HomePhone).HasColumnName("HomePhone").IsOptional().HasMaxLength(24);
            Property(x => x.Extension).HasColumnName("Extension").IsOptional().HasMaxLength(4);
            Property(x => x.Photo).HasColumnName("Photo").IsOptional().HasMaxLength(2147483647);
            Property(x => x.Notes).HasColumnName("Notes").IsOptional().HasMaxLength(1073741823);
            Property(x => x.ReportsTo).HasColumnName("ReportsTo").IsOptional();
            Property(x => x.PhotoPath).HasColumnName("PhotoPath").IsOptional().HasMaxLength(255);

            // Foreign keys
            HasOptional(a => a.Employees_ReportsTo).WithMany(b => b.Employees2).HasForeignKey(c => c.ReportsTo); // FK_Employees_Employees
            HasMany(t => t.Territories).WithMany(t => t.Employees).Map(m => 
            {
                m.ToTable("EmployeeTerritories");
                m.MapLeftKey("EmployeeID");
                m.MapRightKey("TerritoryID");
            });
            HasMany(t => t.Territories).WithMany(t => t.Employees).Map(m => 
            {
                m.ToTable("EmployeeTerritories");
                m.MapLeftKey("EmployeeID");
                m.MapRightKey("TerritoryID");
            });
        }
    }

    // Invoices
    internal class InvoicesConfiguration : EntityTypeConfiguration<Invoices>
    {
        public InvoicesConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Invoices");
            HasKey(x => new { x.CustomerName, x.Salesperson, x.OrderId, x.ShipperName, x.ProductId, x.ProductName, x.UnitPrice, x.Quantity, x.Discount });

            Property(x => x.ShipName).HasColumnName("ShipName").IsOptional().HasMaxLength(40);
            Property(x => x.ShipAddress).HasColumnName("ShipAddress").IsOptional().HasMaxLength(60);
            Property(x => x.ShipCity).HasColumnName("ShipCity").IsOptional().HasMaxLength(15);
            Property(x => x.ShipRegion).HasColumnName("ShipRegion").IsOptional().HasMaxLength(15);
            Property(x => x.ShipPostalCode).HasColumnName("ShipPostalCode").IsOptional().HasMaxLength(10);
            Property(x => x.ShipCountry).HasColumnName("ShipCountry").IsOptional().HasMaxLength(15);
            Property(x => x.CustomerId).HasColumnName("CustomerID").IsOptional().HasMaxLength(5);
            Property(x => x.CustomerName).HasColumnName("CustomerName").IsRequired().HasMaxLength(40);
            Property(x => x.Address).HasColumnName("Address").IsOptional().HasMaxLength(60);
            Property(x => x.City).HasColumnName("City").IsOptional().HasMaxLength(15);
            Property(x => x.Region).HasColumnName("Region").IsOptional().HasMaxLength(15);
            Property(x => x.PostalCode).HasColumnName("PostalCode").IsOptional().HasMaxLength(10);
            Property(x => x.Country).HasColumnName("Country").IsOptional().HasMaxLength(15);
            Property(x => x.Salesperson).HasColumnName("Salesperson").IsRequired().HasMaxLength(31);
            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired();
            Property(x => x.OrderDate).HasColumnName("OrderDate").IsOptional();
            Property(x => x.RequiredDate).HasColumnName("RequiredDate").IsOptional();
            Property(x => x.ShippedDate).HasColumnName("ShippedDate").IsOptional();
            Property(x => x.ShipperName).HasColumnName("ShipperName").IsRequired().HasMaxLength(40);
            Property(x => x.ProductId).HasColumnName("ProductID").IsRequired();
            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasMaxLength(40);
            Property(x => x.UnitPrice).HasColumnName("UnitPrice").IsRequired().HasPrecision(19,4);
            Property(x => x.Quantity).HasColumnName("Quantity").IsRequired();
            Property(x => x.Discount).HasColumnName("Discount").IsRequired();
            Property(x => x.ExtendedPrice).HasColumnName("ExtendedPrice").IsOptional().HasPrecision(19,4);
            Property(x => x.Freight).HasColumnName("Freight").IsOptional().HasPrecision(19,4);
        }
    }

    // Order Details
    internal class OrderDetailsConfiguration : EntityTypeConfiguration<OrderDetails>
    {
        public OrderDetailsConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Order Details");
            HasKey(x => new { x.OrderId, x.ProductId });

            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ProductId).HasColumnName("ProductID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.UnitPrice).HasColumnName("UnitPrice").IsRequired().HasPrecision(19,4);
            Property(x => x.Quantity).HasColumnName("Quantity").IsRequired();
            Property(x => x.Discount).HasColumnName("Discount").IsRequired();

            // Foreign keys
            HasRequired(a => a.Orders).WithMany(b => b.OrderDetails).HasForeignKey(c => c.OrderId); // FK_Order_Details_Orders
            HasRequired(a => a.Products).WithMany(b => b.OrderDetails).HasForeignKey(c => c.ProductId); // FK_Order_Details_Products
        }
    }

    // Order Details Extended
    internal class OrderDetailsExtendedConfiguration : EntityTypeConfiguration<OrderDetailsExtended>
    {
        public OrderDetailsExtendedConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Order Details Extended");
            HasKey(x => new { x.OrderId, x.ProductId, x.ProductName, x.UnitPrice, x.Quantity, x.Discount });

            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired();
            Property(x => x.ProductId).HasColumnName("ProductID").IsRequired();
            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasMaxLength(40);
            Property(x => x.UnitPrice).HasColumnName("UnitPrice").IsRequired().HasPrecision(19,4);
            Property(x => x.Quantity).HasColumnName("Quantity").IsRequired();
            Property(x => x.Discount).HasColumnName("Discount").IsRequired();
            Property(x => x.ExtendedPrice).HasColumnName("ExtendedPrice").IsOptional().HasPrecision(19,4);
        }
    }

    // Orders
    internal class OrdersConfiguration : EntityTypeConfiguration<Orders>
    {
        public OrdersConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Orders");
            HasKey(x => x.OrderId);

            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CustomerId).HasColumnName("CustomerID").IsOptional().HasMaxLength(5);
            Property(x => x.EmployeeId).HasColumnName("EmployeeID").IsOptional();
            Property(x => x.OrderDate).HasColumnName("OrderDate").IsOptional();
            Property(x => x.RequiredDate).HasColumnName("RequiredDate").IsOptional();
            Property(x => x.ShippedDate).HasColumnName("ShippedDate").IsOptional();
            Property(x => x.ShipVia).HasColumnName("ShipVia").IsOptional();
            Property(x => x.Freight).HasColumnName("Freight").IsOptional().HasPrecision(19,4);
            Property(x => x.ShipName).HasColumnName("ShipName").IsOptional().HasMaxLength(40);
            Property(x => x.ShipAddress).HasColumnName("ShipAddress").IsOptional().HasMaxLength(60);
            Property(x => x.ShipCity).HasColumnName("ShipCity").IsOptional().HasMaxLength(15);
            Property(x => x.ShipRegion).HasColumnName("ShipRegion").IsOptional().HasMaxLength(15);
            Property(x => x.ShipPostalCode).HasColumnName("ShipPostalCode").IsOptional().HasMaxLength(10);
            Property(x => x.ShipCountry).HasColumnName("ShipCountry").IsOptional().HasMaxLength(15);

            // Foreign keys
            HasOptional(a => a.Customers).WithMany(b => b.Orders).HasForeignKey(c => c.CustomerId); // FK_Orders_Customers
            HasOptional(a => a.Employees).WithMany(b => b.Orders).HasForeignKey(c => c.EmployeeId); // FK_Orders_Employees
            HasOptional(a => a.Shippers).WithMany(b => b.Orders).HasForeignKey(c => c.ShipVia); // FK_Orders_Shippers
        }
    }

    // Orders Qry
    internal class OrdersQryConfiguration : EntityTypeConfiguration<OrdersQry>
    {
        public OrdersQryConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Orders Qry");
            HasKey(x => new { x.OrderId, x.CompanyName });

            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired();
            Property(x => x.CustomerId).HasColumnName("CustomerID").IsOptional().HasMaxLength(5);
            Property(x => x.EmployeeId).HasColumnName("EmployeeID").IsOptional();
            Property(x => x.OrderDate).HasColumnName("OrderDate").IsOptional();
            Property(x => x.RequiredDate).HasColumnName("RequiredDate").IsOptional();
            Property(x => x.ShippedDate).HasColumnName("ShippedDate").IsOptional();
            Property(x => x.ShipVia).HasColumnName("ShipVia").IsOptional();
            Property(x => x.Freight).HasColumnName("Freight").IsOptional().HasPrecision(19,4);
            Property(x => x.ShipName).HasColumnName("ShipName").IsOptional().HasMaxLength(40);
            Property(x => x.ShipAddress).HasColumnName("ShipAddress").IsOptional().HasMaxLength(60);
            Property(x => x.ShipCity).HasColumnName("ShipCity").IsOptional().HasMaxLength(15);
            Property(x => x.ShipRegion).HasColumnName("ShipRegion").IsOptional().HasMaxLength(15);
            Property(x => x.ShipPostalCode).HasColumnName("ShipPostalCode").IsOptional().HasMaxLength(10);
            Property(x => x.ShipCountry).HasColumnName("ShipCountry").IsOptional().HasMaxLength(15);
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasMaxLength(40);
            Property(x => x.Address).HasColumnName("Address").IsOptional().HasMaxLength(60);
            Property(x => x.City).HasColumnName("City").IsOptional().HasMaxLength(15);
            Property(x => x.Region).HasColumnName("Region").IsOptional().HasMaxLength(15);
            Property(x => x.PostalCode).HasColumnName("PostalCode").IsOptional().HasMaxLength(10);
            Property(x => x.Country).HasColumnName("Country").IsOptional().HasMaxLength(15);
        }
    }

    // Order Subtotals
    internal class OrderSubtotalsConfiguration : EntityTypeConfiguration<OrderSubtotals>
    {
        public OrderSubtotalsConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Order Subtotals");
            HasKey(x => x.OrderId);

            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired();
            Property(x => x.Subtotal).HasColumnName("Subtotal").IsOptional().HasPrecision(19,4);
        }
    }

    // Products
    internal class ProductsConfiguration : EntityTypeConfiguration<Products>
    {
        public ProductsConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Products");
            HasKey(x => x.ProductId);

            Property(x => x.ProductId).HasColumnName("ProductID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasMaxLength(40);
            Property(x => x.SupplierId).HasColumnName("SupplierID").IsOptional();
            Property(x => x.CategoryId).HasColumnName("CategoryID").IsOptional();
            Property(x => x.QuantityPerUnit).HasColumnName("QuantityPerUnit").IsOptional().HasMaxLength(20);
            Property(x => x.UnitPrice).HasColumnName("UnitPrice").IsOptional().HasPrecision(19,4);
            Property(x => x.UnitsInStock).HasColumnName("UnitsInStock").IsOptional();
            Property(x => x.UnitsOnOrder).HasColumnName("UnitsOnOrder").IsOptional();
            Property(x => x.ReorderLevel).HasColumnName("ReorderLevel").IsOptional();
            Property(x => x.Discontinued).HasColumnName("Discontinued").IsRequired();

            // Foreign keys
            HasOptional(a => a.Suppliers).WithMany(b => b.Products).HasForeignKey(c => c.SupplierId); // FK_Products_Suppliers
            HasOptional(a => a.Categories).WithMany(b => b.Products).HasForeignKey(c => c.CategoryId); // FK_Products_Categories
        }
    }

    // Products Above Average Price
    internal class ProductsAboveAveragePriceConfiguration : EntityTypeConfiguration<ProductsAboveAveragePrice>
    {
        public ProductsAboveAveragePriceConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Products Above Average Price");
            HasKey(x => x.ProductName);

            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasMaxLength(40);
            Property(x => x.UnitPrice).HasColumnName("UnitPrice").IsOptional().HasPrecision(19,4);
        }
    }

    // Product Sales for 1997
    internal class ProductSalesFor1997Configuration : EntityTypeConfiguration<ProductSalesFor1997>
    {
        public ProductSalesFor1997Configuration(string schema = "dbo")
        {
            ToTable(schema + ".Product Sales for 1997");
            HasKey(x => new { x.CategoryName, x.ProductName });

            Property(x => x.CategoryName).HasColumnName("CategoryName").IsRequired().HasMaxLength(15);
            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasMaxLength(40);
            Property(x => x.ProductSales).HasColumnName("ProductSales").IsOptional().HasPrecision(19,4);
        }
    }

    // Products by Category
    internal class ProductsByCategoryConfiguration : EntityTypeConfiguration<ProductsByCategory>
    {
        public ProductsByCategoryConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Products by Category");
            HasKey(x => new { x.CategoryName, x.ProductName, x.Discontinued });

            Property(x => x.CategoryName).HasColumnName("CategoryName").IsRequired().HasMaxLength(15);
            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasMaxLength(40);
            Property(x => x.QuantityPerUnit).HasColumnName("QuantityPerUnit").IsOptional().HasMaxLength(20);
            Property(x => x.UnitsInStock).HasColumnName("UnitsInStock").IsOptional();
            Property(x => x.Discontinued).HasColumnName("Discontinued").IsRequired();
        }
    }

    // Region
    internal class RegionConfiguration : EntityTypeConfiguration<Region>
    {
        public RegionConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Region");
            HasKey(x => x.RegionId);

            Property(x => x.RegionId).HasColumnName("RegionID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.RegionDescription).HasColumnName("RegionDescription").IsRequired().HasMaxLength(50);
        }
    }

    // Sales by Category
    internal class SalesByCategoryConfiguration : EntityTypeConfiguration<SalesByCategory>
    {
        public SalesByCategoryConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Sales by Category");
            HasKey(x => new { x.CategoryId, x.CategoryName, x.ProductName });

            Property(x => x.CategoryId).HasColumnName("CategoryID").IsRequired();
            Property(x => x.CategoryName).HasColumnName("CategoryName").IsRequired().HasMaxLength(15);
            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasMaxLength(40);
            Property(x => x.ProductSales).HasColumnName("ProductSales").IsOptional().HasPrecision(19,4);
        }
    }

    // Sales Totals by Amount
    internal class SalesTotalsByAmountConfiguration : EntityTypeConfiguration<SalesTotalsByAmount>
    {
        public SalesTotalsByAmountConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Sales Totals by Amount");
            HasKey(x => new { x.OrderId, x.CompanyName });

            Property(x => x.SaleAmount).HasColumnName("SaleAmount").IsOptional().HasPrecision(19,4);
            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired();
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasMaxLength(40);
            Property(x => x.ShippedDate).HasColumnName("ShippedDate").IsOptional();
        }
    }

    // Shippers
    internal class ShippersConfiguration : EntityTypeConfiguration<Shippers>
    {
        public ShippersConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Shippers");
            HasKey(x => x.ShipperId);

            Property(x => x.ShipperId).HasColumnName("ShipperID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasMaxLength(40);
            Property(x => x.Phone).HasColumnName("Phone").IsOptional().HasMaxLength(24);
        }
    }

    // Summary of Sales by Quarter
    internal class SummaryOfSalesByQuarterConfiguration : EntityTypeConfiguration<SummaryOfSalesByQuarter>
    {
        public SummaryOfSalesByQuarterConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Summary of Sales by Quarter");
            HasKey(x => x.OrderId);

            Property(x => x.ShippedDate).HasColumnName("ShippedDate").IsOptional();
            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired();
            Property(x => x.Subtotal).HasColumnName("Subtotal").IsOptional().HasPrecision(19,4);
        }
    }

    // Summary of Sales by Year
    internal class SummaryOfSalesByYearConfiguration : EntityTypeConfiguration<SummaryOfSalesByYear>
    {
        public SummaryOfSalesByYearConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Summary of Sales by Year");
            HasKey(x => x.OrderId);

            Property(x => x.ShippedDate).HasColumnName("ShippedDate").IsOptional();
            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired();
            Property(x => x.Subtotal).HasColumnName("Subtotal").IsOptional().HasPrecision(19,4);
        }
    }

    // Suppliers
    internal class SuppliersConfiguration : EntityTypeConfiguration<Suppliers>
    {
        public SuppliersConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Suppliers");
            HasKey(x => x.SupplierId);

            Property(x => x.SupplierId).HasColumnName("SupplierID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasMaxLength(40);
            Property(x => x.ContactName).HasColumnName("ContactName").IsOptional().HasMaxLength(30);
            Property(x => x.ContactTitle).HasColumnName("ContactTitle").IsOptional().HasMaxLength(30);
            Property(x => x.Address).HasColumnName("Address").IsOptional().HasMaxLength(60);
            Property(x => x.City).HasColumnName("City").IsOptional().HasMaxLength(15);
            Property(x => x.Region).HasColumnName("Region").IsOptional().HasMaxLength(15);
            Property(x => x.PostalCode).HasColumnName("PostalCode").IsOptional().HasMaxLength(10);
            Property(x => x.Country).HasColumnName("Country").IsOptional().HasMaxLength(15);
            Property(x => x.Phone).HasColumnName("Phone").IsOptional().HasMaxLength(24);
            Property(x => x.Fax).HasColumnName("Fax").IsOptional().HasMaxLength(24);
            Property(x => x.HomePage).HasColumnName("HomePage").IsOptional().HasMaxLength(1073741823);
        }
    }

    // Territories
    internal class TerritoriesConfiguration : EntityTypeConfiguration<Territories>
    {
        public TerritoriesConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Territories");
            HasKey(x => x.TerritoryId);

            Property(x => x.TerritoryId).HasColumnName("TerritoryID").IsRequired().HasMaxLength(20);
            Property(x => x.TerritoryDescription).HasColumnName("TerritoryDescription").IsRequired().HasMaxLength(50);
            Property(x => x.RegionId).HasColumnName("RegionID").IsRequired();

            // Foreign keys
            HasRequired(a => a.Region).WithMany(b => b.Territories).HasForeignKey(c => c.RegionId); // FK_Territories_Region
        }
    }

}

