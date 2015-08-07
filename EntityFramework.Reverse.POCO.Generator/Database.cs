﻿

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Configuration file:     "EntityFramework.Reverse.POCO.Generator\App.config"
//     Connection String Name: "MyDbContext"
//     Connection String:      "Data Source=(local);Initial Catalog=northwind;Integrated Security=True;Application Name=EntityFramework Reverse POCO Generator"

// Database Edition: Developer Edition (64-bit)
// Database Engine Edition: Enterprise

// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
// TargetFrameworkVersion = 4.51
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Entity.ModelConfiguration;
using System.Threading;
using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

namespace EntityFramework_Reverse_POCO_Generator
{
    // ************************************************************************
    // Unit of work
    public interface IMyDbContext : IDisposable
    {
        DbSet<AlphabeticalListOfProduct> AlphabeticalListOfProducts { get; set; } // Alphabetical list of products
        DbSet<Category> Categories { get; set; } // Categories
        DbSet<CategorySalesFor1997> CategorySalesFor1997 { get; set; } // Category Sales for 1997
        DbSet<CurrentProductList> CurrentProductLists { get; set; } // Current Product List
        DbSet<Customer> Customers { get; set; } // Customers
        DbSet<CustomerAndSuppliersByCity> CustomerAndSuppliersByCities { get; set; } // Customer and Suppliers by City
        DbSet<CustomerDemographic> CustomerDemographics { get; set; } // CustomerDemographics
        DbSet<Employee> Employees { get; set; } // Employees
        DbSet<Invoice> Invoices { get; set; } // Invoices
        DbSet<Order> Orders { get; set; } // Orders
        DbSet<OrderDetail> OrderDetails { get; set; } // Order Details
        DbSet<OrderDetailsExtended> OrderDetailsExtendeds { get; set; } // Order Details Extended
        DbSet<OrdersQry> OrdersQries { get; set; } // Orders Qry
        DbSet<OrderSubtotal> OrderSubtotals { get; set; } // Order Subtotals
        DbSet<Product> Products { get; set; } // Products
        DbSet<ProductsAboveAveragePrice> ProductsAboveAveragePrices { get; set; } // Products Above Average Price
        DbSet<ProductSalesFor1997> ProductSalesFor1997 { get; set; } // Product Sales for 1997
        DbSet<ProductsByCategory> ProductsByCategories { get; set; } // Products by Category
        DbSet<Region> Regions { get; set; } // Region
        DbSet<SalesByCategory> SalesByCategories { get; set; } // Sales by Category
        DbSet<SalesTotalsByAmount> SalesTotalsByAmounts { get; set; } // Sales Totals by Amount
        DbSet<Shipper> Shippers { get; set; } // Shippers
        DbSet<SummaryOfSalesByQuarter> SummaryOfSalesByQuarters { get; set; } // Summary of Sales by Quarter
        DbSet<SummaryOfSalesByYear> SummaryOfSalesByYears { get; set; } // Summary of Sales by Year
        DbSet<Supplier> Suppliers { get; set; } // Suppliers
        DbSet<Sysdiagram> Sysdiagrams { get; set; } // sysdiagrams
        DbSet<Territory> Territories { get; set; } // Territories

        int SaveChanges();
        System.Threading.Tasks.Task<int> SaveChangesAsync();
        System.Threading.Tasks.Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
        // Stored Procedures
        List<CustOrderHistReturnModel> CustOrderHist(string customerId, out int procResult);
        List<CustOrdersDetailReturnModel> CustOrdersDetail(int? orderId, out int procResult);
        List<CustOrdersOrdersReturnModel> CustOrdersOrders(string customerId, out int procResult);
        List<EmployeeSalesByCountryReturnModel> EmployeeSalesByCountry(DateTime? beginningDate, DateTime? endingDate, out int procResult);
        List<SalesByYearReturnModel> SalesByYear(DateTime? beginningDate, DateTime? endingDate, out int procResult);
        List<SalesByCategoryReturnModel> SalesByCategory(string categoryName, string ordYear, out int procResult);
        List<TenMostExpensiveProductsReturnModel> TenMostExpensiveProducts( out int procResult);
    }

    // ************************************************************************
    // Database context
    public class MyDbContext : DbContext, IMyDbContext
    {
        public DbSet<AlphabeticalListOfProduct> AlphabeticalListOfProducts { get; set; } // Alphabetical list of products
        public DbSet<Category> Categories { get; set; } // Categories
        public DbSet<CategorySalesFor1997> CategorySalesFor1997 { get; set; } // Category Sales for 1997
        public DbSet<CurrentProductList> CurrentProductLists { get; set; } // Current Product List
        public DbSet<Customer> Customers { get; set; } // Customers
        public DbSet<CustomerAndSuppliersByCity> CustomerAndSuppliersByCities { get; set; } // Customer and Suppliers by City
        public DbSet<CustomerDemographic> CustomerDemographics { get; set; } // CustomerDemographics
        public DbSet<Employee> Employees { get; set; } // Employees
        public DbSet<Invoice> Invoices { get; set; } // Invoices
        public DbSet<Order> Orders { get; set; } // Orders
        public DbSet<OrderDetail> OrderDetails { get; set; } // Order Details
        public DbSet<OrderDetailsExtended> OrderDetailsExtendeds { get; set; } // Order Details Extended
        public DbSet<OrdersQry> OrdersQries { get; set; } // Orders Qry
        public DbSet<OrderSubtotal> OrderSubtotals { get; set; } // Order Subtotals
        public DbSet<Product> Products { get; set; } // Products
        public DbSet<ProductsAboveAveragePrice> ProductsAboveAveragePrices { get; set; } // Products Above Average Price
        public DbSet<ProductSalesFor1997> ProductSalesFor1997 { get; set; } // Product Sales for 1997
        public DbSet<ProductsByCategory> ProductsByCategories { get; set; } // Products by Category
        public DbSet<Region> Regions { get; set; } // Region
        public DbSet<SalesByCategory> SalesByCategories { get; set; } // Sales by Category
        public DbSet<SalesTotalsByAmount> SalesTotalsByAmounts { get; set; } // Sales Totals by Amount
        public DbSet<Shipper> Shippers { get; set; } // Shippers
        public DbSet<SummaryOfSalesByQuarter> SummaryOfSalesByQuarters { get; set; } // Summary of Sales by Quarter
        public DbSet<SummaryOfSalesByYear> SummaryOfSalesByYears { get; set; } // Summary of Sales by Year
        public DbSet<Supplier> Suppliers { get; set; } // Suppliers
        public DbSet<Sysdiagram> Sysdiagrams { get; set; } // sysdiagrams
        public DbSet<Territory> Territories { get; set; } // Territories
        
        static MyDbContext()
        {
            System.Data.Entity.Database.SetInitializer<MyDbContext>(null);
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AlphabeticalListOfProductConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new CategorySalesFor1997Configuration());
            modelBuilder.Configurations.Add(new CurrentProductListConfiguration());
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new CustomerAndSuppliersByCityConfiguration());
            modelBuilder.Configurations.Add(new CustomerDemographicConfiguration());
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new InvoiceConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailsExtendedConfiguration());
            modelBuilder.Configurations.Add(new OrdersQryConfiguration());
            modelBuilder.Configurations.Add(new OrderSubtotalConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new ProductsAboveAveragePriceConfiguration());
            modelBuilder.Configurations.Add(new ProductSalesFor1997Configuration());
            modelBuilder.Configurations.Add(new ProductsByCategoryConfiguration());
            modelBuilder.Configurations.Add(new RegionConfiguration());
            modelBuilder.Configurations.Add(new SalesByCategoryConfiguration());
            modelBuilder.Configurations.Add(new SalesTotalsByAmountConfiguration());
            modelBuilder.Configurations.Add(new ShipperConfiguration());
            modelBuilder.Configurations.Add(new SummaryOfSalesByQuarterConfiguration());
            modelBuilder.Configurations.Add(new SummaryOfSalesByYearConfiguration());
            modelBuilder.Configurations.Add(new SupplierConfiguration());
            modelBuilder.Configurations.Add(new SysdiagramConfiguration());
            modelBuilder.Configurations.Add(new TerritoryConfiguration());
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new AlphabeticalListOfProductConfiguration(schema));
            modelBuilder.Configurations.Add(new CategoryConfiguration(schema));
            modelBuilder.Configurations.Add(new CategorySalesFor1997Configuration(schema));
            modelBuilder.Configurations.Add(new CurrentProductListConfiguration(schema));
            modelBuilder.Configurations.Add(new CustomerConfiguration(schema));
            modelBuilder.Configurations.Add(new CustomerAndSuppliersByCityConfiguration(schema));
            modelBuilder.Configurations.Add(new CustomerDemographicConfiguration(schema));
            modelBuilder.Configurations.Add(new EmployeeConfiguration(schema));
            modelBuilder.Configurations.Add(new InvoiceConfiguration(schema));
            modelBuilder.Configurations.Add(new OrderConfiguration(schema));
            modelBuilder.Configurations.Add(new OrderDetailConfiguration(schema));
            modelBuilder.Configurations.Add(new OrderDetailsExtendedConfiguration(schema));
            modelBuilder.Configurations.Add(new OrdersQryConfiguration(schema));
            modelBuilder.Configurations.Add(new OrderSubtotalConfiguration(schema));
            modelBuilder.Configurations.Add(new ProductConfiguration(schema));
            modelBuilder.Configurations.Add(new ProductsAboveAveragePriceConfiguration(schema));
            modelBuilder.Configurations.Add(new ProductSalesFor1997Configuration(schema));
            modelBuilder.Configurations.Add(new ProductsByCategoryConfiguration(schema));
            modelBuilder.Configurations.Add(new RegionConfiguration(schema));
            modelBuilder.Configurations.Add(new SalesByCategoryConfiguration(schema));
            modelBuilder.Configurations.Add(new SalesTotalsByAmountConfiguration(schema));
            modelBuilder.Configurations.Add(new ShipperConfiguration(schema));
            modelBuilder.Configurations.Add(new SummaryOfSalesByQuarterConfiguration(schema));
            modelBuilder.Configurations.Add(new SummaryOfSalesByYearConfiguration(schema));
            modelBuilder.Configurations.Add(new SupplierConfiguration(schema));
            modelBuilder.Configurations.Add(new SysdiagramConfiguration(schema));
            modelBuilder.Configurations.Add(new TerritoryConfiguration(schema));
            return modelBuilder;
        }
        
        // Stored Procedures
        public List<CustOrderHistReturnModel> CustOrderHist(string customerId)
        {
            int procResult;
            return CustOrderHist(customerId, out procResult);
        }

        public List<CustOrderHistReturnModel> CustOrderHist(string customerId, out int procResult)
        {
            var customerIdParam = new SqlParameter { ParameterName = "@CustomerID", SqlDbType = SqlDbType.NChar, Direction = ParameterDirection.Input, Value = customerId, Size = 5 };
            if (!(customerIdParam.Value is ValueType) && customerIdParam.Value == null)
                customerIdParam.Value = DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
 
            var procResultData = Database.SqlQuery<CustOrderHistReturnModel>("EXEC @procResult = [dbo].[CustOrderHist] @CustomerID", customerIdParam, procResultParam).ToList();
 
            procResult = (int) procResultParam.Value;
            return procResultData;
        }

        public List<CustOrdersDetailReturnModel> CustOrdersDetail(int? orderId)
        {
            int procResult;
            return CustOrdersDetail(orderId, out procResult);
        }

        public List<CustOrdersDetailReturnModel> CustOrdersDetail(int? orderId, out int procResult)
        {
            var orderIdParam = new SqlParameter { ParameterName = "@OrderID", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = orderId.GetValueOrDefault() };
            if (!(orderIdParam.Value is ValueType) && orderIdParam.Value == null)
                orderIdParam.Value = DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
 
            var procResultData = Database.SqlQuery<CustOrdersDetailReturnModel>("EXEC @procResult = [dbo].[CustOrdersDetail] @OrderID", orderIdParam, procResultParam).ToList();
 
            procResult = (int) procResultParam.Value;
            return procResultData;
        }

        public List<CustOrdersOrdersReturnModel> CustOrdersOrders(string customerId)
        {
            int procResult;
            return CustOrdersOrders(customerId, out procResult);
        }

        public List<CustOrdersOrdersReturnModel> CustOrdersOrders(string customerId, out int procResult)
        {
            var customerIdParam = new SqlParameter { ParameterName = "@CustomerID", SqlDbType = SqlDbType.NChar, Direction = ParameterDirection.Input, Value = customerId, Size = 5 };
            if (!(customerIdParam.Value is ValueType) && customerIdParam.Value == null)
                customerIdParam.Value = DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
 
            var procResultData = Database.SqlQuery<CustOrdersOrdersReturnModel>("EXEC @procResult = [dbo].[CustOrdersOrders] @CustomerID", customerIdParam, procResultParam).ToList();
 
            procResult = (int) procResultParam.Value;
            return procResultData;
        }

        public List<EmployeeSalesByCountryReturnModel> EmployeeSalesByCountry(DateTime? beginningDate, DateTime? endingDate)
        {
            int procResult;
            return EmployeeSalesByCountry(beginningDate, endingDate, out procResult);
        }

        public List<EmployeeSalesByCountryReturnModel> EmployeeSalesByCountry(DateTime? beginningDate, DateTime? endingDate, out int procResult)
        {
            var beginningDateParam = new SqlParameter { ParameterName = "@Beginning_Date", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = beginningDate.GetValueOrDefault() };
            if (!(beginningDateParam.Value is ValueType) && beginningDateParam.Value == null)
                beginningDateParam.Value = DBNull.Value;

            var endingDateParam = new SqlParameter { ParameterName = "@Ending_Date", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = endingDate.GetValueOrDefault() };
            if (!(endingDateParam.Value is ValueType) && endingDateParam.Value == null)
                endingDateParam.Value = DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
 
            var procResultData = Database.SqlQuery<EmployeeSalesByCountryReturnModel>("EXEC @procResult = [dbo].[Employee Sales by Country] @Beginning_Date, @Ending_Date", beginningDateParam, endingDateParam, procResultParam).ToList();
 
            procResult = (int) procResultParam.Value;
            return procResultData;
        }

        public List<SalesByYearReturnModel> SalesByYear(DateTime? beginningDate, DateTime? endingDate)
        {
            int procResult;
            return SalesByYear(beginningDate, endingDate, out procResult);
        }

        public List<SalesByYearReturnModel> SalesByYear(DateTime? beginningDate, DateTime? endingDate, out int procResult)
        {
            var beginningDateParam = new SqlParameter { ParameterName = "@Beginning_Date", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = beginningDate.GetValueOrDefault() };
            if (!(beginningDateParam.Value is ValueType) && beginningDateParam.Value == null)
                beginningDateParam.Value = DBNull.Value;

            var endingDateParam = new SqlParameter { ParameterName = "@Ending_Date", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = endingDate.GetValueOrDefault() };
            if (!(endingDateParam.Value is ValueType) && endingDateParam.Value == null)
                endingDateParam.Value = DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
 
            var procResultData = Database.SqlQuery<SalesByYearReturnModel>("EXEC @procResult = [dbo].[Sales by Year] @Beginning_Date, @Ending_Date", beginningDateParam, endingDateParam, procResultParam).ToList();
 
            procResult = (int) procResultParam.Value;
            return procResultData;
        }

        public List<SalesByCategoryReturnModel> SalesByCategory(string categoryName, string ordYear)
        {
            int procResult;
            return SalesByCategory(categoryName, ordYear, out procResult);
        }

        public List<SalesByCategoryReturnModel> SalesByCategory(string categoryName, string ordYear, out int procResult)
        {
            var categoryNameParam = new SqlParameter { ParameterName = "@CategoryName", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = categoryName, Size = 15 };
            if (!(categoryNameParam.Value is ValueType) && categoryNameParam.Value == null)
                categoryNameParam.Value = DBNull.Value;

            var ordYearParam = new SqlParameter { ParameterName = "@OrdYear", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = ordYear, Size = 4 };
            if (!(ordYearParam.Value is ValueType) && ordYearParam.Value == null)
                ordYearParam.Value = DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
 
            var procResultData = Database.SqlQuery<SalesByCategoryReturnModel>("EXEC @procResult = [dbo].[SalesByCategory] @CategoryName, @OrdYear", categoryNameParam, ordYearParam, procResultParam).ToList();
 
            procResult = (int) procResultParam.Value;
            return procResultData;
        }

        public List<TenMostExpensiveProductsReturnModel> TenMostExpensiveProducts()
        {
            int procResult;
            return TenMostExpensiveProducts(out procResult);
        }

        public List<TenMostExpensiveProductsReturnModel> TenMostExpensiveProducts( out int procResult)
        {
            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
 
            var procResultData = Database.SqlQuery<TenMostExpensiveProductsReturnModel>("EXEC @procResult = [dbo].[Ten Most Expensive Products] ", procResultParam).ToList();
 
            procResult = (int) procResultParam.Value;
            return procResultData;
        }

    }

    // ************************************************************************
    // Fake Database context
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.14.3.0")]
    public class FakeMyDbContext : IMyDbContext
    {
        public DbSet<AlphabeticalListOfProduct> AlphabeticalListOfProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategorySalesFor1997> CategorySalesFor1997 { get; set; }
        public DbSet<CurrentProductList> CurrentProductLists { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAndSuppliersByCity> CustomerAndSuppliersByCities { get; set; }
        public DbSet<CustomerDemographic> CustomerDemographics { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderDetailsExtended> OrderDetailsExtendeds { get; set; }
        public DbSet<OrdersQry> OrdersQries { get; set; }
        public DbSet<OrderSubtotal> OrderSubtotals { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductsAboveAveragePrice> ProductsAboveAveragePrices { get; set; }
        public DbSet<ProductSalesFor1997> ProductSalesFor1997 { get; set; }
        public DbSet<ProductsByCategory> ProductsByCategories { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<SalesByCategory> SalesByCategories { get; set; }
        public DbSet<SalesTotalsByAmount> SalesTotalsByAmounts { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<SummaryOfSalesByQuarter> SummaryOfSalesByQuarters { get; set; }
        public DbSet<SummaryOfSalesByYear> SummaryOfSalesByYears { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Sysdiagram> Sysdiagrams { get; set; }
        public DbSet<Territory> Territories { get; set; }

        public FakeMyDbContext()
        {
            AlphabeticalListOfProducts = new FakeDbSet<AlphabeticalListOfProduct>();
            Categories = new FakeDbSet<Category>();
            CategorySalesFor1997 = new FakeDbSet<CategorySalesFor1997>();
            CurrentProductLists = new FakeDbSet<CurrentProductList>();
            Customers = new FakeDbSet<Customer>();
            CustomerAndSuppliersByCities = new FakeDbSet<CustomerAndSuppliersByCity>();
            CustomerDemographics = new FakeDbSet<CustomerDemographic>();
            Employees = new FakeDbSet<Employee>();
            Invoices = new FakeDbSet<Invoice>();
            Orders = new FakeDbSet<Order>();
            OrderDetails = new FakeDbSet<OrderDetail>();
            OrderDetailsExtendeds = new FakeDbSet<OrderDetailsExtended>();
            OrdersQries = new FakeDbSet<OrdersQry>();
            OrderSubtotals = new FakeDbSet<OrderSubtotal>();
            Products = new FakeDbSet<Product>();
            ProductsAboveAveragePrices = new FakeDbSet<ProductsAboveAveragePrice>();
            ProductSalesFor1997 = new FakeDbSet<ProductSalesFor1997>();
            ProductsByCategories = new FakeDbSet<ProductsByCategory>();
            Regions = new FakeDbSet<Region>();
            SalesByCategories = new FakeDbSet<SalesByCategory>();
            SalesTotalsByAmounts = new FakeDbSet<SalesTotalsByAmount>();
            Shippers = new FakeDbSet<Shipper>();
            SummaryOfSalesByQuarters = new FakeDbSet<SummaryOfSalesByQuarter>();
            SummaryOfSalesByYears = new FakeDbSet<SummaryOfSalesByYear>();
            Suppliers = new FakeDbSet<Supplier>();
            Sysdiagrams = new FakeDbSet<Sysdiagram>();
            Territories = new FakeDbSet<Territory>();
        }
        
        public int SaveChangesCount { get; private set; } 
        public int SaveChanges()
        {
            ++SaveChangesCount;
            return 1;
        }

        public System.Threading.Tasks.Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
        
        // Stored Procedures
        public List<CustOrderHistReturnModel> CustOrderHist(string customerId)
        {
            int procResult;
            return CustOrderHist(customerId, out procResult);
        }

        public List<CustOrderHistReturnModel> CustOrderHist(string customerId, out int procResult)
        {
 
            procResult = 0;
            return new List<CustOrderHistReturnModel>();
        }

        public List<CustOrdersDetailReturnModel> CustOrdersDetail(int? orderId)
        {
            int procResult;
            return CustOrdersDetail(orderId, out procResult);
        }

        public List<CustOrdersDetailReturnModel> CustOrdersDetail(int? orderId, out int procResult)
        {
 
            procResult = 0;
            return new List<CustOrdersDetailReturnModel>();
        }

        public List<CustOrdersOrdersReturnModel> CustOrdersOrders(string customerId)
        {
            int procResult;
            return CustOrdersOrders(customerId, out procResult);
        }

        public List<CustOrdersOrdersReturnModel> CustOrdersOrders(string customerId, out int procResult)
        {
 
            procResult = 0;
            return new List<CustOrdersOrdersReturnModel>();
        }

        public List<EmployeeSalesByCountryReturnModel> EmployeeSalesByCountry(DateTime? beginningDate, DateTime? endingDate)
        {
            int procResult;
            return EmployeeSalesByCountry(beginningDate, endingDate, out procResult);
        }

        public List<EmployeeSalesByCountryReturnModel> EmployeeSalesByCountry(DateTime? beginningDate, DateTime? endingDate, out int procResult)
        {
 
            procResult = 0;
            return new List<EmployeeSalesByCountryReturnModel>();
        }

        public List<SalesByYearReturnModel> SalesByYear(DateTime? beginningDate, DateTime? endingDate)
        {
            int procResult;
            return SalesByYear(beginningDate, endingDate, out procResult);
        }

        public List<SalesByYearReturnModel> SalesByYear(DateTime? beginningDate, DateTime? endingDate, out int procResult)
        {
 
            procResult = 0;
            return new List<SalesByYearReturnModel>();
        }

        public List<SalesByCategoryReturnModel> SalesByCategory(string categoryName, string ordYear)
        {
            int procResult;
            return SalesByCategory(categoryName, ordYear, out procResult);
        }

        public List<SalesByCategoryReturnModel> SalesByCategory(string categoryName, string ordYear, out int procResult)
        {
 
            procResult = 0;
            return new List<SalesByCategoryReturnModel>();
        }

        public List<TenMostExpensiveProductsReturnModel> TenMostExpensiveProducts()
        {
            int procResult;
            return TenMostExpensiveProducts(out procResult);
        }

        public List<TenMostExpensiveProductsReturnModel> TenMostExpensiveProducts( out int procResult)
        {
 
            procResult = 0;
            return new List<TenMostExpensiveProductsReturnModel>();
        }

    }

    // ************************************************************************
    // Fake DbSet
    // Implementing Find:
    //      The Find method is difficult to implement in a generic fashion. If
    //      you need to test code that makes use of the Find method it is
    //      easiest to create a test DbSet for each of the entity types that
    //      need to support find. You can then write logic to find that
    //      particular type of entity, as shown below:
    //      public class FakeBlogDbSet : FakeDbSet<Blog>
    //      {
    //          public override Blog Find(params object[] keyValues)
    //          {
    //              var id = (int) keyValues.Single();
    //              return this.SingleOrDefault(b => b.BlogId == id);
    //          }
    //      }
    //      Read more about it here: https://msdn.microsoft.com/en-us/data/dn314431.aspx
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.14.3.0")]
    public class FakeDbSet<TEntity> : DbSet<TEntity>, IQueryable, IEnumerable<TEntity>, IDbAsyncEnumerable<TEntity> 
        where TEntity : class 
    { 
        private readonly ObservableCollection<TEntity> _data;
        private readonly IQueryable _query;
 
        public FakeDbSet() 
        { 
            _data = new ObservableCollection<TEntity>(); 
            _query = _data.AsQueryable(); 
        }

        public override IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            var items = entities.ToList();
            foreach (var entity in items)
            {
                _data.Add(entity);
            }
            return items;
        }
        
        public override TEntity Add(TEntity item) 
        {
            if (item == null) throw new ArgumentNullException("item");
            _data.Add(item); 
            return item; 
        } 
 
        public override TEntity Remove(TEntity item) 
        {
            if (item == null) throw new ArgumentNullException("item");
            _data.Remove(item); 
            return item; 
        } 
 
        public override TEntity Attach(TEntity item) 
        {
            if (item == null) throw new ArgumentNullException("item");
            _data.Add(item); 
            return item; 
        } 
 
        public override TEntity Create() 
        { 
            return Activator.CreateInstance<TEntity>(); 
        } 
 
        public override TDerivedEntity Create<TDerivedEntity>() 
        { 
            return Activator.CreateInstance<TDerivedEntity>(); 
        } 
 
        public override ObservableCollection<TEntity> Local 
        { 
            get { return _data; } 
        } 
 
        Type IQueryable.ElementType 
        { 
            get { return _query.ElementType; } 
        } 
 
        Expression IQueryable.Expression 
        { 
            get { return _query.Expression; } 
        } 
 
        IQueryProvider IQueryable.Provider 
        { 
            get { return new FakeDbAsyncQueryProvider<TEntity>(_query.Provider); } 
        } 
 
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() 
        { 
            return _data.GetEnumerator(); 
        } 
 
        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator() 
        { 
            return _data.GetEnumerator(); 
        } 
 
        IDbAsyncEnumerator<TEntity> IDbAsyncEnumerable<TEntity>.GetAsyncEnumerator() 
        { 
            return new FakeDbAsyncEnumerator<TEntity>(_data.GetEnumerator()); 
        }
    } 
 
    internal class FakeDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider 
    { 
        private readonly IQueryProvider _inner; 
 
        internal FakeDbAsyncQueryProvider(IQueryProvider inner) 
        { 
            _inner = inner; 
        } 
 
        public IQueryable CreateQuery(Expression expression) 
        { 
            return new FakeDbAsyncEnumerable<TEntity>(expression); 
        } 
 
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) 
        { 
            return new FakeDbAsyncEnumerable<TElement>(expression); 
        } 
 
        public object Execute(Expression expression) 
        { 
            return _inner.Execute(expression); 
        } 
 
        public TResult Execute<TResult>(Expression expression) 
        { 
            return _inner.Execute<TResult>(expression); 
        } 
 
        public System.Threading.Tasks.Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken) 
        { 
            return System.Threading.Tasks.Task.FromResult(Execute(expression)); 
        } 
 
        public System.Threading.Tasks.Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken) 
        { 
            return System.Threading.Tasks.Task.FromResult(Execute<TResult>(expression)); 
        } 
    } 
 
    internal class FakeDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T> 
    { 
        public FakeDbAsyncEnumerable(IEnumerable<T> enumerable) 
            : base(enumerable) 
        { } 
 
        public FakeDbAsyncEnumerable(Expression expression) 
            : base(expression) 
        { } 
 
        public IDbAsyncEnumerator<T> GetAsyncEnumerator() 
        { 
            return new FakeDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator()); 
        } 
 
        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator() 
        { 
            return GetAsyncEnumerator(); 
        } 
 
        IQueryProvider IQueryable.Provider 
        { 
            get { return new FakeDbAsyncQueryProvider<T>(this); } 
        } 
    } 
 
    internal class FakeDbAsyncEnumerator<T> : IDbAsyncEnumerator<T> 
    { 
        private readonly IEnumerator<T> _inner; 
 
        public FakeDbAsyncEnumerator(IEnumerator<T> inner) 
        { 
            _inner = inner; 
        } 
 
        public void Dispose() 
        { 
            _inner.Dispose(); 
        } 
 
        public System.Threading.Tasks.Task<bool> MoveNextAsync(CancellationToken cancellationToken) 
        { 
            return System.Threading.Tasks.Task.FromResult(_inner.MoveNext()); 
        } 
 
        public T Current 
        { 
            get { return _inner.Current; } 
        } 
 
        object IDbAsyncEnumerator.Current 
        { 
            get { return Current; } 
        } 
    }

    // ************************************************************************
    // POCO classes

    // Alphabetical list of products
    public class AlphabeticalListOfProduct
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
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.14.3.0")]
    public class Category
    {
        public int CategoryId { get; set; } // CategoryID (Primary key)
        public string CategoryName { get; set; } // CategoryName
        public string Description { get; set; } // Description
        public byte[] Picture { get; set; } // Picture

        // Reverse navigation
        public virtual ICollection<Product> Products { get; set; } // Products.FK_Products_Categories
        
        public Category()
        {
            Products = new List<Product>();
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

    // Customers
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.14.3.0")]
    public class Customer
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
        public virtual ICollection<CustomerDemographic> CustomerDemographics { get; set; } // Many to many mapping
        public virtual ICollection<Order> Orders { get; set; } // Orders.FK_Orders_Customers
        
        public Customer()
        {
            Orders = new List<Order>();
            CustomerDemographics = new List<CustomerDemographic>();
        }
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
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.14.3.0")]
    public class CustomerDemographic
    {
        public string CustomerTypeId { get; set; } // CustomerTypeID (Primary key)
        public string CustomerDesc { get; set; } // CustomerDesc

        // Reverse navigation
        public virtual ICollection<Customer> Customers { get; set; } // Many to many mapping
        
        public CustomerDemographic()
        {
            Customers = new List<Customer>();
        }
    }

    // Employees
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.14.3.0")]
    public class Employee
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
        public virtual ICollection<Employee> Employees { get; set; } // Employees.FK_Employees_Employees
        public virtual ICollection<Order> Orders { get; set; } // Orders.FK_Orders_Employees
        public virtual ICollection<Territory> Territories { get; set; } // Many to many mapping

        // Foreign keys
        public virtual Employee Employee_ReportsTo { get; set; } // FK_Employees_Employees
        
        public Employee()
        {
            Employees = new List<Employee>();
            Orders = new List<Order>();
            Territories = new List<Territory>();
        }
    }

    // Invoices
    public class Invoice
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

    // Orders
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.14.3.0")]
    public class Order
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
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } // Many to many mapping

        // Foreign keys
        public virtual Customer Customer { get; set; } // FK_Orders_Customers
        public virtual Employee Employee { get; set; } // FK_Orders_Employees
        public virtual Shipper Shipper { get; set; } // FK_Orders_Shippers
        
        public Order()
        {
            Freight = 0m;
            OrderDetails = new List<OrderDetail>();
        }
    }

    // Order Details
    public class OrderDetail
    {
        public int OrderId { get; set; } // OrderID (Primary key)
        public int ProductId { get; set; } // ProductID (Primary key)
        public decimal UnitPrice { get; set; } // UnitPrice
        public short Quantity { get; set; } // Quantity
        public float Discount { get; set; } // Discount

        // Foreign keys
        public virtual Order Order { get; set; } // FK_Order_Details_Orders
        public virtual Product Product { get; set; } // FK_Order_Details_Products
        
        public OrderDetail()
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
    public class OrderSubtotal
    {
        public int OrderId { get; set; } // OrderID
        public decimal? Subtotal { get; set; } // Subtotal
    }

    // Products
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.14.3.0")]
    public class Product
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
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } // Many to many mapping

        // Foreign keys
        public virtual Category Category { get; set; } // FK_Products_Categories
        public virtual Supplier Supplier { get; set; } // FK_Products_Suppliers
        
        public Product()
        {
            UnitPrice = 0m;
            UnitsInStock = 0;
            UnitsOnOrder = 0;
            ReorderLevel = 0;
            Discontinued = false;
            OrderDetails = new List<OrderDetail>();
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
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.14.3.0")]
    public class Region
    {
        public int RegionId { get; set; } // RegionID (Primary key)
        public string RegionDescription { get; set; } // RegionDescription

        // Reverse navigation
        public virtual ICollection<Territory> Territories { get; set; } // Territories.FK_Territories_Region
        
        public Region()
        {
            Territories = new List<Territory>();
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
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.14.3.0")]
    public class Shipper
    {
        public int ShipperId { get; set; } // ShipperID (Primary key)
        public string CompanyName { get; set; } // CompanyName
        public string Phone { get; set; } // Phone

        // Reverse navigation
        public virtual ICollection<Order> Orders { get; set; } // Orders.FK_Orders_Shippers
        
        public Shipper()
        {
            Orders = new List<Order>();
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
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.14.3.0")]
    public class Supplier
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
        public virtual ICollection<Product> Products { get; set; } // Products.FK_Products_Suppliers
        
        public Supplier()
        {
            Products = new List<Product>();
        }
    }

    // sysdiagrams
    public class Sysdiagram
    {
        public string Name { get; set; } // name
        public int PrincipalId { get; set; } // principal_id
        public int DiagramId { get; set; } // diagram_id (Primary key)
        public int? Version { get; set; } // version
        public byte[] Definition { get; set; } // definition
    }

    // Territories
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.14.3.0")]
    public class Territory
    {
        public string TerritoryId { get; set; } // TerritoryID (Primary key)
        public string TerritoryDescription { get; set; } // TerritoryDescription
        public int RegionId { get; set; } // RegionID

        // Reverse navigation
        public virtual ICollection<Employee> Employees { get; set; } // Many to many mapping

        // Foreign keys
        public virtual Region Region { get; set; } // FK_Territories_Region
        
        public Territory()
        {
            Employees = new List<Employee>();
        }
    }


    // ************************************************************************
    // POCO Configuration

    // Alphabetical list of products
    internal class AlphabeticalListOfProductConfiguration : EntityTypeConfiguration<AlphabeticalListOfProduct>
    {
        public AlphabeticalListOfProductConfiguration()
            : this("dbo")
        {
        }
 
        public AlphabeticalListOfProductConfiguration(string schema)
        {
            ToTable(schema + ".Alphabetical list of products");
            HasKey(x => new { x.ProductId, x.ProductName, x.Discontinued, x.CategoryName });

            Property(x => x.ProductId).HasColumnName("ProductID").IsRequired().HasColumnType("int");
            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
            Property(x => x.SupplierId).HasColumnName("SupplierID").IsOptional().HasColumnType("int");
            Property(x => x.CategoryId).HasColumnName("CategoryID").IsOptional().HasColumnType("int");
            Property(x => x.QuantityPerUnit).HasColumnName("QuantityPerUnit").IsOptional().HasColumnType("nvarchar").HasMaxLength(20);
            Property(x => x.UnitPrice).HasColumnName("UnitPrice").IsOptional().HasColumnType("money").HasPrecision(19,4);
            Property(x => x.UnitsInStock).HasColumnName("UnitsInStock").IsOptional().HasColumnType("smallint");
            Property(x => x.UnitsOnOrder).HasColumnName("UnitsOnOrder").IsOptional().HasColumnType("smallint");
            Property(x => x.ReorderLevel).HasColumnName("ReorderLevel").IsOptional().HasColumnType("smallint");
            Property(x => x.Discontinued).HasColumnName("Discontinued").IsRequired().HasColumnType("bit");
            Property(x => x.CategoryName).HasColumnName("CategoryName").IsRequired().HasColumnType("nvarchar").HasMaxLength(15);
        }
    }

    // Categories
    internal class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
            : this("dbo")
        {
        }
 
        public CategoryConfiguration(string schema)
        {
            ToTable(schema + ".Categories");
            HasKey(x => x.CategoryId);

            Property(x => x.CategoryId).HasColumnName("CategoryID").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CategoryName).HasColumnName("CategoryName").IsRequired().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasColumnType("ntext").IsMaxLength();
            Property(x => x.Picture).HasColumnName("Picture").IsOptional().HasColumnType("image").HasMaxLength(2147483647);
        }
    }

    // Category Sales for 1997
    internal class CategorySalesFor1997Configuration : EntityTypeConfiguration<CategorySalesFor1997>
    {
        public CategorySalesFor1997Configuration()
            : this("dbo")
        {
        }
 
        public CategorySalesFor1997Configuration(string schema)
        {
            ToTable(schema + ".Category Sales for 1997");
            HasKey(x => x.CategoryName);

            Property(x => x.CategoryName).HasColumnName("CategoryName").IsRequired().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.CategorySales).HasColumnName("CategorySales").IsOptional().HasColumnType("money").HasPrecision(19,4);
        }
    }

    // Current Product List
    internal class CurrentProductListConfiguration : EntityTypeConfiguration<CurrentProductList>
    {
        public CurrentProductListConfiguration()
            : this("dbo")
        {
        }
 
        public CurrentProductListConfiguration(string schema)
        {
            ToTable(schema + ".Current Product List");
            HasKey(x => new { x.ProductId, x.ProductName });

            Property(x => x.ProductId).HasColumnName("ProductID").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
        }
    }

    // Customers
    internal class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
            : this("dbo")
        {
        }
 
        public CustomerConfiguration(string schema)
        {
            ToTable(schema + ".Customers");
            HasKey(x => x.CustomerId);

            Property(x => x.CustomerId).HasColumnName("CustomerID").IsRequired().IsFixedLength().HasColumnType("nchar").HasMaxLength(5).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
            Property(x => x.ContactName).HasColumnName("ContactName").IsOptional().HasColumnType("nvarchar").HasMaxLength(30);
            Property(x => x.ContactTitle).HasColumnName("ContactTitle").IsOptional().HasColumnType("nvarchar").HasMaxLength(30);
            Property(x => x.Address).HasColumnName("Address").IsOptional().HasColumnType("nvarchar").HasMaxLength(60);
            Property(x => x.City).HasColumnName("City").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.Region).HasColumnName("Region").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.PostalCode).HasColumnName("PostalCode").IsOptional().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.Country).HasColumnName("Country").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.Phone).HasColumnName("Phone").IsOptional().HasColumnType("nvarchar").HasMaxLength(24);
            Property(x => x.Fax).HasColumnName("Fax").IsOptional().HasColumnType("nvarchar").HasMaxLength(24);
            HasMany(t => t.CustomerDemographics).WithMany(t => t.Customers).Map(m => 
            {
                m.ToTable("CustomerCustomerDemo", "dbo");
                m.MapLeftKey("CustomerID");
                m.MapRightKey("CustomerTypeID");
            });
        }
    }

    // Customer and Suppliers by City
    internal class CustomerAndSuppliersByCityConfiguration : EntityTypeConfiguration<CustomerAndSuppliersByCity>
    {
        public CustomerAndSuppliersByCityConfiguration()
            : this("dbo")
        {
        }
 
        public CustomerAndSuppliersByCityConfiguration(string schema)
        {
            ToTable(schema + ".Customer and Suppliers by City");
            HasKey(x => new { x.CompanyName, x.Relationship });

            Property(x => x.City).HasColumnName("City").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
            Property(x => x.ContactName).HasColumnName("ContactName").IsOptional().HasColumnType("nvarchar").HasMaxLength(30);
            Property(x => x.Relationship).HasColumnName("Relationship").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(9);
        }
    }

    // CustomerDemographics
    internal class CustomerDemographicConfiguration : EntityTypeConfiguration<CustomerDemographic>
    {
        public CustomerDemographicConfiguration()
            : this("dbo")
        {
        }
 
        public CustomerDemographicConfiguration(string schema)
        {
            ToTable(schema + ".CustomerDemographics");
            HasKey(x => x.CustomerTypeId);

            Property(x => x.CustomerTypeId).HasColumnName("CustomerTypeID").IsRequired().IsFixedLength().HasColumnType("nchar").HasMaxLength(10).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.CustomerDesc).HasColumnName("CustomerDesc").IsOptional().HasColumnType("ntext").IsMaxLength();
        }
    }

    // Employees
    internal class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
            : this("dbo")
        {
        }
 
        public EmployeeConfiguration(string schema)
        {
            ToTable(schema + ".Employees");
            HasKey(x => x.EmployeeId);

            Property(x => x.EmployeeId).HasColumnName("EmployeeID").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.LastName).HasColumnName("LastName").IsRequired().HasColumnType("nvarchar").HasMaxLength(20);
            Property(x => x.FirstName).HasColumnName("FirstName").IsRequired().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.Title).HasColumnName("Title").IsOptional().HasColumnType("nvarchar").HasMaxLength(30);
            Property(x => x.TitleOfCourtesy).HasColumnName("TitleOfCourtesy").IsOptional().HasColumnType("nvarchar").HasMaxLength(25);
            Property(x => x.BirthDate).HasColumnName("BirthDate").IsOptional().HasColumnType("datetime");
            Property(x => x.HireDate).HasColumnName("HireDate").IsOptional().HasColumnType("datetime");
            Property(x => x.Address).HasColumnName("Address").IsOptional().HasColumnType("nvarchar").HasMaxLength(60);
            Property(x => x.City).HasColumnName("City").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.Region).HasColumnName("Region").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.PostalCode).HasColumnName("PostalCode").IsOptional().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.Country).HasColumnName("Country").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.HomePhone).HasColumnName("HomePhone").IsOptional().HasColumnType("nvarchar").HasMaxLength(24);
            Property(x => x.Extension).HasColumnName("Extension").IsOptional().HasColumnType("nvarchar").HasMaxLength(4);
            Property(x => x.Photo).HasColumnName("Photo").IsOptional().HasColumnType("image").HasMaxLength(2147483647);
            Property(x => x.Notes).HasColumnName("Notes").IsOptional().HasColumnType("ntext").IsMaxLength();
            Property(x => x.ReportsTo).HasColumnName("ReportsTo").IsOptional().HasColumnType("int");
            Property(x => x.PhotoPath).HasColumnName("PhotoPath").IsOptional().HasColumnType("nvarchar").HasMaxLength(255);

            // Foreign keys
            HasOptional(a => a.Employee_ReportsTo).WithMany(b => b.Employees).HasForeignKey(c => c.ReportsTo); // FK_Employees_Employees
            HasMany(t => t.Territories).WithMany(t => t.Employees).Map(m => 
            {
                m.ToTable("EmployeeTerritories", "dbo");
                m.MapLeftKey("EmployeeID");
                m.MapRightKey("TerritoryID");
            });
        }
    }

    // Invoices
    internal class InvoiceConfiguration : EntityTypeConfiguration<Invoice>
    {
        public InvoiceConfiguration()
            : this("dbo")
        {
        }
 
        public InvoiceConfiguration(string schema)
        {
            ToTable(schema + ".Invoices");
            HasKey(x => new { x.CustomerName, x.Salesperson, x.OrderId, x.ShipperName, x.ProductId, x.ProductName, x.UnitPrice, x.Quantity, x.Discount });

            Property(x => x.ShipName).HasColumnName("ShipName").IsOptional().HasColumnType("nvarchar").HasMaxLength(40);
            Property(x => x.ShipAddress).HasColumnName("ShipAddress").IsOptional().HasColumnType("nvarchar").HasMaxLength(60);
            Property(x => x.ShipCity).HasColumnName("ShipCity").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.ShipRegion).HasColumnName("ShipRegion").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.ShipPostalCode).HasColumnName("ShipPostalCode").IsOptional().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.ShipCountry).HasColumnName("ShipCountry").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.CustomerId).HasColumnName("CustomerID").IsOptional().IsFixedLength().HasColumnType("nchar").HasMaxLength(5);
            Property(x => x.CustomerName).HasColumnName("CustomerName").IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
            Property(x => x.Address).HasColumnName("Address").IsOptional().HasColumnType("nvarchar").HasMaxLength(60);
            Property(x => x.City).HasColumnName("City").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.Region).HasColumnName("Region").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.PostalCode).HasColumnName("PostalCode").IsOptional().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.Country).HasColumnName("Country").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.Salesperson).HasColumnName("Salesperson").IsRequired().HasColumnType("nvarchar").HasMaxLength(31);
            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired().HasColumnType("int");
            Property(x => x.OrderDate).HasColumnName("OrderDate").IsOptional().HasColumnType("datetime");
            Property(x => x.RequiredDate).HasColumnName("RequiredDate").IsOptional().HasColumnType("datetime");
            Property(x => x.ShippedDate).HasColumnName("ShippedDate").IsOptional().HasColumnType("datetime");
            Property(x => x.ShipperName).HasColumnName("ShipperName").IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
            Property(x => x.ProductId).HasColumnName("ProductID").IsRequired().HasColumnType("int");
            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
            Property(x => x.UnitPrice).HasColumnName("UnitPrice").IsRequired().HasColumnType("money").HasPrecision(19,4);
            Property(x => x.Quantity).HasColumnName("Quantity").IsRequired().HasColumnType("smallint");
            Property(x => x.Discount).HasColumnName("Discount").IsRequired().HasColumnType("real");
            Property(x => x.ExtendedPrice).HasColumnName("ExtendedPrice").IsOptional().HasColumnType("money").HasPrecision(19,4);
            Property(x => x.Freight).HasColumnName("Freight").IsOptional().HasColumnType("money").HasPrecision(19,4);
        }
    }

    // Orders
    internal class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
            : this("dbo")
        {
        }
 
        public OrderConfiguration(string schema)
        {
            ToTable(schema + ".Orders");
            HasKey(x => x.OrderId);

            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CustomerId).HasColumnName("CustomerID").IsOptional().IsFixedLength().HasColumnType("nchar").HasMaxLength(5);
            Property(x => x.EmployeeId).HasColumnName("EmployeeID").IsOptional().HasColumnType("int");
            Property(x => x.OrderDate).HasColumnName("OrderDate").IsOptional().HasColumnType("datetime");
            Property(x => x.RequiredDate).HasColumnName("RequiredDate").IsOptional().HasColumnType("datetime");
            Property(x => x.ShippedDate).HasColumnName("ShippedDate").IsOptional().HasColumnType("datetime");
            Property(x => x.ShipVia).HasColumnName("ShipVia").IsOptional().HasColumnType("int");
            Property(x => x.Freight).HasColumnName("Freight").IsOptional().HasColumnType("money").HasPrecision(19,4);
            Property(x => x.ShipName).HasColumnName("ShipName").IsOptional().HasColumnType("nvarchar").HasMaxLength(40);
            Property(x => x.ShipAddress).HasColumnName("ShipAddress").IsOptional().HasColumnType("nvarchar").HasMaxLength(60);
            Property(x => x.ShipCity).HasColumnName("ShipCity").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.ShipRegion).HasColumnName("ShipRegion").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.ShipPostalCode).HasColumnName("ShipPostalCode").IsOptional().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.ShipCountry).HasColumnName("ShipCountry").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);

            // Foreign keys
            HasOptional(a => a.Customer).WithMany(b => b.Orders).HasForeignKey(c => c.CustomerId); // FK_Orders_Customers
            HasOptional(a => a.Employee).WithMany(b => b.Orders).HasForeignKey(c => c.EmployeeId); // FK_Orders_Employees
            HasOptional(a => a.Shipper).WithMany(b => b.Orders).HasForeignKey(c => c.ShipVia); // FK_Orders_Shippers
        }
    }

    // Order Details
    internal class OrderDetailConfiguration : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailConfiguration()
            : this("dbo")
        {
        }
 
        public OrderDetailConfiguration(string schema)
        {
            ToTable(schema + ".Order Details");
            HasKey(x => new { x.OrderId, x.ProductId });

            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ProductId).HasColumnName("ProductID").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.UnitPrice).HasColumnName("UnitPrice").IsRequired().HasColumnType("money").HasPrecision(19,4);
            Property(x => x.Quantity).HasColumnName("Quantity").IsRequired().HasColumnType("smallint");
            Property(x => x.Discount).HasColumnName("Discount").IsRequired().HasColumnType("real");

            // Foreign keys
            HasRequired(a => a.Order).WithMany(b => b.OrderDetails).HasForeignKey(c => c.OrderId); // FK_Order_Details_Orders
            HasRequired(a => a.Product).WithMany(b => b.OrderDetails).HasForeignKey(c => c.ProductId); // FK_Order_Details_Products
        }
    }

    // Order Details Extended
    internal class OrderDetailsExtendedConfiguration : EntityTypeConfiguration<OrderDetailsExtended>
    {
        public OrderDetailsExtendedConfiguration()
            : this("dbo")
        {
        }
 
        public OrderDetailsExtendedConfiguration(string schema)
        {
            ToTable(schema + ".Order Details Extended");
            HasKey(x => new { x.OrderId, x.ProductId, x.ProductName, x.UnitPrice, x.Quantity, x.Discount });

            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired().HasColumnType("int");
            Property(x => x.ProductId).HasColumnName("ProductID").IsRequired().HasColumnType("int");
            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
            Property(x => x.UnitPrice).HasColumnName("UnitPrice").IsRequired().HasColumnType("money").HasPrecision(19,4);
            Property(x => x.Quantity).HasColumnName("Quantity").IsRequired().HasColumnType("smallint");
            Property(x => x.Discount).HasColumnName("Discount").IsRequired().HasColumnType("real");
            Property(x => x.ExtendedPrice).HasColumnName("ExtendedPrice").IsOptional().HasColumnType("money").HasPrecision(19,4);
        }
    }

    // Orders Qry
    internal class OrdersQryConfiguration : EntityTypeConfiguration<OrdersQry>
    {
        public OrdersQryConfiguration()
            : this("dbo")
        {
        }
 
        public OrdersQryConfiguration(string schema)
        {
            ToTable(schema + ".Orders Qry");
            HasKey(x => new { x.OrderId, x.CompanyName });

            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired().HasColumnType("int");
            Property(x => x.CustomerId).HasColumnName("CustomerID").IsOptional().IsFixedLength().HasColumnType("nchar").HasMaxLength(5);
            Property(x => x.EmployeeId).HasColumnName("EmployeeID").IsOptional().HasColumnType("int");
            Property(x => x.OrderDate).HasColumnName("OrderDate").IsOptional().HasColumnType("datetime");
            Property(x => x.RequiredDate).HasColumnName("RequiredDate").IsOptional().HasColumnType("datetime");
            Property(x => x.ShippedDate).HasColumnName("ShippedDate").IsOptional().HasColumnType("datetime");
            Property(x => x.ShipVia).HasColumnName("ShipVia").IsOptional().HasColumnType("int");
            Property(x => x.Freight).HasColumnName("Freight").IsOptional().HasColumnType("money").HasPrecision(19,4);
            Property(x => x.ShipName).HasColumnName("ShipName").IsOptional().HasColumnType("nvarchar").HasMaxLength(40);
            Property(x => x.ShipAddress).HasColumnName("ShipAddress").IsOptional().HasColumnType("nvarchar").HasMaxLength(60);
            Property(x => x.ShipCity).HasColumnName("ShipCity").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.ShipRegion).HasColumnName("ShipRegion").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.ShipPostalCode).HasColumnName("ShipPostalCode").IsOptional().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.ShipCountry).HasColumnName("ShipCountry").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
            Property(x => x.Address).HasColumnName("Address").IsOptional().HasColumnType("nvarchar").HasMaxLength(60);
            Property(x => x.City).HasColumnName("City").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.Region).HasColumnName("Region").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.PostalCode).HasColumnName("PostalCode").IsOptional().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.Country).HasColumnName("Country").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
        }
    }

    // Order Subtotals
    internal class OrderSubtotalConfiguration : EntityTypeConfiguration<OrderSubtotal>
    {
        public OrderSubtotalConfiguration()
            : this("dbo")
        {
        }
 
        public OrderSubtotalConfiguration(string schema)
        {
            ToTable(schema + ".Order Subtotals");
            HasKey(x => x.OrderId);

            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired().HasColumnType("int");
            Property(x => x.Subtotal).HasColumnName("Subtotal").IsOptional().HasColumnType("money").HasPrecision(19,4);
        }
    }

    // Products
    internal class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
            : this("dbo")
        {
        }
 
        public ProductConfiguration(string schema)
        {
            ToTable(schema + ".Products");
            HasKey(x => x.ProductId);

            Property(x => x.ProductId).HasColumnName("ProductID").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
            Property(x => x.SupplierId).HasColumnName("SupplierID").IsOptional().HasColumnType("int");
            Property(x => x.CategoryId).HasColumnName("CategoryID").IsOptional().HasColumnType("int");
            Property(x => x.QuantityPerUnit).HasColumnName("QuantityPerUnit").IsOptional().HasColumnType("nvarchar").HasMaxLength(20);
            Property(x => x.UnitPrice).HasColumnName("UnitPrice").IsOptional().HasColumnType("money").HasPrecision(19,4);
            Property(x => x.UnitsInStock).HasColumnName("UnitsInStock").IsOptional().HasColumnType("smallint");
            Property(x => x.UnitsOnOrder).HasColumnName("UnitsOnOrder").IsOptional().HasColumnType("smallint");
            Property(x => x.ReorderLevel).HasColumnName("ReorderLevel").IsOptional().HasColumnType("smallint");
            Property(x => x.Discontinued).HasColumnName("Discontinued").IsRequired().HasColumnType("bit");

            // Foreign keys
            HasOptional(a => a.Category).WithMany(b => b.Products).HasForeignKey(c => c.CategoryId); // FK_Products_Categories
            HasOptional(a => a.Supplier).WithMany(b => b.Products).HasForeignKey(c => c.SupplierId); // FK_Products_Suppliers
        }
    }

    // Products Above Average Price
    internal class ProductsAboveAveragePriceConfiguration : EntityTypeConfiguration<ProductsAboveAveragePrice>
    {
        public ProductsAboveAveragePriceConfiguration()
            : this("dbo")
        {
        }
 
        public ProductsAboveAveragePriceConfiguration(string schema)
        {
            ToTable(schema + ".Products Above Average Price");
            HasKey(x => x.ProductName);

            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
            Property(x => x.UnitPrice).HasColumnName("UnitPrice").IsOptional().HasColumnType("money").HasPrecision(19,4);
        }
    }

    // Product Sales for 1997
    internal class ProductSalesFor1997Configuration : EntityTypeConfiguration<ProductSalesFor1997>
    {
        public ProductSalesFor1997Configuration()
            : this("dbo")
        {
        }
 
        public ProductSalesFor1997Configuration(string schema)
        {
            ToTable(schema + ".Product Sales for 1997");
            HasKey(x => new { x.CategoryName, x.ProductName });

            Property(x => x.CategoryName).HasColumnName("CategoryName").IsRequired().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
            Property(x => x.ProductSales).HasColumnName("ProductSales").IsOptional().HasColumnType("money").HasPrecision(19,4);
        }
    }

    // Products by Category
    internal class ProductsByCategoryConfiguration : EntityTypeConfiguration<ProductsByCategory>
    {
        public ProductsByCategoryConfiguration()
            : this("dbo")
        {
        }
 
        public ProductsByCategoryConfiguration(string schema)
        {
            ToTable(schema + ".Products by Category");
            HasKey(x => new { x.CategoryName, x.ProductName, x.Discontinued });

            Property(x => x.CategoryName).HasColumnName("CategoryName").IsRequired().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
            Property(x => x.QuantityPerUnit).HasColumnName("QuantityPerUnit").IsOptional().HasColumnType("nvarchar").HasMaxLength(20);
            Property(x => x.UnitsInStock).HasColumnName("UnitsInStock").IsOptional().HasColumnType("smallint");
            Property(x => x.Discontinued).HasColumnName("Discontinued").IsRequired().HasColumnType("bit");
        }
    }

    // Region
    internal class RegionConfiguration : EntityTypeConfiguration<Region>
    {
        public RegionConfiguration()
            : this("dbo")
        {
        }
 
        public RegionConfiguration(string schema)
        {
            ToTable(schema + ".Region");
            HasKey(x => x.RegionId);

            Property(x => x.RegionId).HasColumnName("RegionID").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.RegionDescription).HasColumnName("RegionDescription").IsRequired().IsFixedLength().HasColumnType("nchar").HasMaxLength(50);
        }
    }

    // Sales by Category
    internal class SalesByCategoryConfiguration : EntityTypeConfiguration<SalesByCategory>
    {
        public SalesByCategoryConfiguration()
            : this("dbo")
        {
        }
 
        public SalesByCategoryConfiguration(string schema)
        {
            ToTable(schema + ".Sales by Category");
            HasKey(x => new { x.CategoryId, x.CategoryName, x.ProductName });

            Property(x => x.CategoryId).HasColumnName("CategoryID").IsRequired().HasColumnType("int");
            Property(x => x.CategoryName).HasColumnName("CategoryName").IsRequired().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
            Property(x => x.ProductSales).HasColumnName("ProductSales").IsOptional().HasColumnType("money").HasPrecision(19,4);
        }
    }

    // Sales Totals by Amount
    internal class SalesTotalsByAmountConfiguration : EntityTypeConfiguration<SalesTotalsByAmount>
    {
        public SalesTotalsByAmountConfiguration()
            : this("dbo")
        {
        }
 
        public SalesTotalsByAmountConfiguration(string schema)
        {
            ToTable(schema + ".Sales Totals by Amount");
            HasKey(x => new { x.OrderId, x.CompanyName });

            Property(x => x.SaleAmount).HasColumnName("SaleAmount").IsOptional().HasColumnType("money").HasPrecision(19,4);
            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired().HasColumnType("int");
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
            Property(x => x.ShippedDate).HasColumnName("ShippedDate").IsOptional().HasColumnType("datetime");
        }
    }

    // Shippers
    internal class ShipperConfiguration : EntityTypeConfiguration<Shipper>
    {
        public ShipperConfiguration()
            : this("dbo")
        {
        }
 
        public ShipperConfiguration(string schema)
        {
            ToTable(schema + ".Shippers");
            HasKey(x => x.ShipperId);

            Property(x => x.ShipperId).HasColumnName("ShipperID").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
            Property(x => x.Phone).HasColumnName("Phone").IsOptional().HasColumnType("nvarchar").HasMaxLength(24);
        }
    }

    // Summary of Sales by Quarter
    internal class SummaryOfSalesByQuarterConfiguration : EntityTypeConfiguration<SummaryOfSalesByQuarter>
    {
        public SummaryOfSalesByQuarterConfiguration()
            : this("dbo")
        {
        }
 
        public SummaryOfSalesByQuarterConfiguration(string schema)
        {
            ToTable(schema + ".Summary of Sales by Quarter");
            HasKey(x => x.OrderId);

            Property(x => x.ShippedDate).HasColumnName("ShippedDate").IsOptional().HasColumnType("datetime");
            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired().HasColumnType("int");
            Property(x => x.Subtotal).HasColumnName("Subtotal").IsOptional().HasColumnType("money").HasPrecision(19,4);
        }
    }

    // Summary of Sales by Year
    internal class SummaryOfSalesByYearConfiguration : EntityTypeConfiguration<SummaryOfSalesByYear>
    {
        public SummaryOfSalesByYearConfiguration()
            : this("dbo")
        {
        }
 
        public SummaryOfSalesByYearConfiguration(string schema)
        {
            ToTable(schema + ".Summary of Sales by Year");
            HasKey(x => x.OrderId);

            Property(x => x.ShippedDate).HasColumnName("ShippedDate").IsOptional().HasColumnType("datetime");
            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired().HasColumnType("int");
            Property(x => x.Subtotal).HasColumnName("Subtotal").IsOptional().HasColumnType("money").HasPrecision(19,4);
        }
    }

    // Suppliers
    internal class SupplierConfiguration : EntityTypeConfiguration<Supplier>
    {
        public SupplierConfiguration()
            : this("dbo")
        {
        }
 
        public SupplierConfiguration(string schema)
        {
            ToTable(schema + ".Suppliers");
            HasKey(x => x.SupplierId);

            Property(x => x.SupplierId).HasColumnName("SupplierID").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
            Property(x => x.ContactName).HasColumnName("ContactName").IsOptional().HasColumnType("nvarchar").HasMaxLength(30);
            Property(x => x.ContactTitle).HasColumnName("ContactTitle").IsOptional().HasColumnType("nvarchar").HasMaxLength(30);
            Property(x => x.Address).HasColumnName("Address").IsOptional().HasColumnType("nvarchar").HasMaxLength(60);
            Property(x => x.City).HasColumnName("City").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.Region).HasColumnName("Region").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.PostalCode).HasColumnName("PostalCode").IsOptional().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.Country).HasColumnName("Country").IsOptional().HasColumnType("nvarchar").HasMaxLength(15);
            Property(x => x.Phone).HasColumnName("Phone").IsOptional().HasColumnType("nvarchar").HasMaxLength(24);
            Property(x => x.Fax).HasColumnName("Fax").IsOptional().HasColumnType("nvarchar").HasMaxLength(24);
            Property(x => x.HomePage).HasColumnName("HomePage").IsOptional().HasColumnType("ntext").IsMaxLength();
        }
    }

    // sysdiagrams
    internal class SysdiagramConfiguration : EntityTypeConfiguration<Sysdiagram>
    {
        public SysdiagramConfiguration()
            : this("dbo")
        {
        }
 
        public SysdiagramConfiguration(string schema)
        {
            ToTable(schema + ".sysdiagrams");
            HasKey(x => x.DiagramId);

            Property(x => x.Name).HasColumnName("name").IsRequired().HasColumnType("nvarchar").HasMaxLength(128);
            Property(x => x.PrincipalId).HasColumnName("principal_id").IsRequired().HasColumnType("int");
            Property(x => x.DiagramId).HasColumnName("diagram_id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Version).HasColumnName("version").IsOptional().HasColumnType("int");
            Property(x => x.Definition).HasColumnName("definition").IsOptional().HasColumnType("varbinary");
        }
    }

    // Territories
    internal class TerritoryConfiguration : EntityTypeConfiguration<Territory>
    {
        public TerritoryConfiguration()
            : this("dbo")
        {
        }
 
        public TerritoryConfiguration(string schema)
        {
            ToTable(schema + ".Territories");
            HasKey(x => x.TerritoryId);

            Property(x => x.TerritoryId).HasColumnName("TerritoryID").IsRequired().HasColumnType("nvarchar").HasMaxLength(20).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TerritoryDescription).HasColumnName("TerritoryDescription").IsRequired().IsFixedLength().HasColumnType("nchar").HasMaxLength(50);
            Property(x => x.RegionId).HasColumnName("RegionID").IsRequired().HasColumnType("int");

            // Foreign keys
            HasRequired(a => a.Region).WithMany(b => b.Territories).HasForeignKey(c => c.RegionId); // FK_Territories_Region
        }
    }


    // ************************************************************************
    // Stored procedure return models

    public class CustOrderHistReturnModel
    {
        public String ProductName { get; set; }
        public Int32? Total { get; set; }
    }

    public class CustOrdersDetailReturnModel
    {
        public String ProductName { get; set; }
        public Decimal? UnitPrice { get; set; }
        public Int16 Quantity { get; set; }
        public Int32? Discount { get; set; }
        public Decimal? ExtendedPrice { get; set; }
    }

    public class CustOrdersOrdersReturnModel
    {
        public Int32 OrderID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
    }

    public class EmployeeSalesByCountryReturnModel
    {
        public String Country { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public DateTime? ShippedDate { get; set; }
        public Int32 OrderID { get; set; }
        public Decimal? SaleAmount { get; set; }
    }

    public class SalesByYearReturnModel
    {
        public DateTime? ShippedDate { get; set; }
        public Int32 OrderID { get; set; }
        public Decimal? Subtotal { get; set; }
        public String Year { get; set; }
    }

    public class SalesByCategoryReturnModel
    {
        public String ProductName { get; set; }
        public Decimal? TotalPurchase { get; set; }
    }

    public class TenMostExpensiveProductsReturnModel
    {
        public String TenMostExpensiveProducts { get; set; }
        public Decimal? UnitPrice { get; set; }
    }

}

