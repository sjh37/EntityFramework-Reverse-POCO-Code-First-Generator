using System;
using System.Data.Entity;
using EntityFramework_Reverse_POCO_Generator;

namespace Tester.UnitTest
{
    public class FakeDbContext : IMyDbContext
    {
        public IDbSet<AlphabeticalListOfProduct>    AlphabeticalListOfProducts   { get; set; } // Alphabetical list of products
        public IDbSet<Category>                     Categories                   { get; set; } // Categories
        public IDbSet<CategorySalesFor1997>         CategorySalesFor1997         { get; set; } // Category Sales for 1997
        public IDbSet<CurrentProductList>           CurrentProductLists          { get; set; } // Current Product List
        public IDbSet<Customer>                     Customers                    { get; set; } // Customers
        public IDbSet<CustomerAndSuppliersByCity>   CustomerAndSuppliersByCities { get; set; } // Customer and Suppliers by City
        public IDbSet<CustomerDemographic>          CustomerDemographics         { get; set; } // CustomerDemographics
        public IDbSet<Employee>                     Employees                    { get; set; } // Employees
        public IDbSet<Invoice>                      Invoices                     { get; set; } // Invoices
        public IDbSet<Order>                        Orders                       { get; set; } // Orders
        public IDbSet<OrderDetail>                  OrderDetails                 { get; set; } // Order Details
        public IDbSet<OrderDetailsExtended>         OrderDetailsExtendeds        { get; set; } // Order Details Extended
        public IDbSet<OrdersQry>                    OrdersQries                  { get; set; } // Orders Qry
        public IDbSet<OrderSubtotal>                OrderSubtotals               { get; set; } // Order Subtotals
        public IDbSet<Product>                      Products                     { get; set; } // Products
        public IDbSet<ProductsAboveAveragePrice>    ProductsAboveAveragePrices   { get; set; } // Products Above Average Price
        public IDbSet<ProductSalesFor1997>          ProductSalesFor1997          { get; set; } // Product Sales for 1997
        public IDbSet<ProductsByCategory>           ProductsByCategories         { get; set; } // Products by Category
        public IDbSet<Region>                       Regions                      { get; set; } // Region
        public IDbSet<SalesByCategory>              SalesByCategories            { get; set; } // Sales by Category
        public IDbSet<SalesTotalsByAmount>          SalesTotalsByAmounts         { get; set; } // Sales Totals by Amount
        public IDbSet<Shipper>                      Shippers                     { get; set; } // Shippers
        public IDbSet<SummaryOfSalesByQuarter>      SummaryOfSalesByQuarters     { get; set; } // Summary of Sales by Quarter
        public IDbSet<SummaryOfSalesByYear>         SummaryOfSalesByYears        { get; set; } // Summary of Sales by Year
        public IDbSet<Supplier>                     Suppliers                    { get; set; } // Suppliers
        public IDbSet<Territory>                    Territories                  { get; set; } // Territories

        public FakeDbContext()
        {
            AlphabeticalListOfProducts   = new FakeDbSet<AlphabeticalListOfProduct>();
            Categories                   = new FakeDbSet<Category>();
            CategorySalesFor1997         = new FakeDbSet<CategorySalesFor1997>();
            CurrentProductLists          = new FakeDbSet<CurrentProductList>();
            CustomerAndSuppliersByCities = new FakeDbSet<CustomerAndSuppliersByCity>();
            CustomerDemographics         = new FakeDbSet<CustomerDemographic>();
            Customers                    = new FakeDbSet<Customer>();
            Employees                    = new FakeDbSet<Employee>();
            Invoices                     = new FakeDbSet<Invoice>();
            OrderDetails                 = new FakeDbSet<OrderDetail>();
            OrderDetailsExtendeds        = new FakeDbSet<OrderDetailsExtended>();
            Orders                       = new FakeDbSet<Order>();
            OrdersQries                  = new FakeDbSet<OrdersQry>();
            OrderSubtotals               = new FakeDbSet<OrderSubtotal>();
            Products                     = new FakeDbSet<Product>();
            ProductsAboveAveragePrices   = new FakeDbSet<ProductsAboveAveragePrice>();
            ProductSalesFor1997          = new FakeDbSet<ProductSalesFor1997>();
            ProductsByCategories         = new FakeDbSet<ProductsByCategory>();
            Regions                      = new FakeDbSet<Region>();
            SalesByCategories            = new FakeDbSet<SalesByCategory>();
            SalesTotalsByAmounts         = new FakeDbSet<SalesTotalsByAmount>();
            Shippers                     = new FakeDbSet<Shipper>();
            SummaryOfSalesByQuarters     = new FakeDbSet<SummaryOfSalesByQuarter>();
            SummaryOfSalesByYears        = new FakeDbSet<SummaryOfSalesByYear>();
            Suppliers                    = new FakeDbSet<Supplier>();
            Territories                  = new FakeDbSet<Territory>();
        }

        public int SaveChanges()
        {
            return 0;
        }

        public void Dispose()
        {
            throw new NotImplementedException(); 
        }
    }
}