using System;
using System.Data.Entity;
using EntityFramework_Reverse_POCO_Generator;

namespace Tester.UnitTest
{
    public class FakeDbContext : IMyDbContext
    {
        public IDbSet<AlphabeticalListOfProducts>   AlphabeticalListOfProducts { get; set; } // Alphabetical list of products
        public IDbSet<Categories>                   Categories                 { get; set; } // Categories
        public IDbSet<CategorySalesFor1997>         CategorySalesFor1997       { get; set; } // Category Sales for 1997
        public IDbSet<CurrentProductList>           CurrentProductList         { get; set; } // Current Product List
        public IDbSet<CustomerAndSuppliersByCity>   CustomerAndSuppliersByCity { get; set; } // Customer and Suppliers by City
        public IDbSet<CustomerDemographics>         CustomerDemographics       { get; set; } // CustomerDemographics
        public IDbSet<Customers>                    Customers                  { get; set; } // Customers
        public IDbSet<Employees>                    Employees                  { get; set; } // Employees
        public IDbSet<Invoices>                     Invoices                   { get; set; } // Invoices
        public IDbSet<OrderDetails>                 OrderDetails               { get; set; } // Order Details
        public IDbSet<OrderDetailsExtended>         OrderDetailsExtended       { get; set; } // Order Details Extended
        public IDbSet<Orders>                       Orders                     { get; set; } // Orders
        public IDbSet<OrdersQry>                    OrdersQry                  { get; set; } // Orders Qry
        public IDbSet<OrderSubtotals>               OrderSubtotals             { get; set; } // Order Subtotals
        public IDbSet<Products>                     Products                   { get; set; } // Products
        public IDbSet<ProductsAboveAveragePrice>    ProductsAboveAveragePrice  { get; set; } // Products Above Average Price
        public IDbSet<ProductSalesFor1997>          ProductSalesFor1997        { get; set; } // Product Sales for 1997
        public IDbSet<ProductsByCategory>           ProductsByCategory         { get; set; } // Products by Category
        public IDbSet<Region>                       Region                     { get; set; } // Region
        public IDbSet<SalesByCategory>              SalesByCategory            { get; set; } // Sales by Category
        public IDbSet<SalesTotalsByAmount>          SalesTotalsByAmount        { get; set; } // Sales Totals by Amount
        public IDbSet<Shippers>                     Shippers                   { get; set; } // Shippers
        public IDbSet<SummaryOfSalesByQuarter>      SummaryOfSalesByQuarter    { get; set; } // Summary of Sales by Quarter
        public IDbSet<SummaryOfSalesByYear>         SummaryOfSalesByYear       { get; set; } // Summary of Sales by Year
        public IDbSet<Suppliers>                    Suppliers                  { get; set; } // Suppliers
        public IDbSet<Territories>                  Territories                { get; set; } // Territories

        public FakeDbContext()
        {
            AlphabeticalListOfProducts = new FakeDbSet<AlphabeticalListOfProducts>();
            Categories                 = new FakeDbSet<Categories>();
            CategorySalesFor1997       = new FakeDbSet<CategorySalesFor1997>();
            CurrentProductList         = new FakeDbSet<CurrentProductList>();
            CustomerAndSuppliersByCity = new FakeDbSet<CustomerAndSuppliersByCity>();
            CustomerDemographics       = new FakeDbSet<CustomerDemographics>();
            Customers                  = new FakeDbSet<Customers>();
            Employees                  = new FakeDbSet<Employees>();
            Invoices                   = new FakeDbSet<Invoices>();
            OrderDetails               = new FakeDbSet<OrderDetails>();
            OrderDetailsExtended       = new FakeDbSet<OrderDetailsExtended>();
            Orders                     = new FakeDbSet<Orders>();
            OrdersQry                  = new FakeDbSet<OrdersQry>();
            OrderSubtotals             = new FakeDbSet<OrderSubtotals>();
            Products                   = new FakeDbSet<Products>();
            ProductsAboveAveragePrice  = new FakeDbSet<ProductsAboveAveragePrice>();
            ProductSalesFor1997        = new FakeDbSet<ProductSalesFor1997>();
            ProductsByCategory         = new FakeDbSet<ProductsByCategory>();
            Region                     = new FakeDbSet<Region>();
            SalesByCategory            = new FakeDbSet<SalesByCategory>();
            SalesTotalsByAmount        = new FakeDbSet<SalesTotalsByAmount>();
            Shippers                   = new FakeDbSet<Shippers>();
            SummaryOfSalesByQuarter    = new FakeDbSet<SummaryOfSalesByQuarter>();
            SummaryOfSalesByYear       = new FakeDbSet<SummaryOfSalesByYear>();
            Suppliers                  = new FakeDbSet<Suppliers>();
            Territories                = new FakeDbSet<Territories>();
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