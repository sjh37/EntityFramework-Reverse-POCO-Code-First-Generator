-- The schema every Settings.* wiki example is generated from.
--
-- This file is what the wiki pages show the reader. DocSampleSchema.xml is the same schema in the shape
-- the efrpg tool would hand over, so the examples can be generated without a database. Keep the two in
-- step; DocSampleSchemaTests asserts the parts a page relies on are present in the XML.
--
-- Nothing runs this script. It exists to be read.

CREATE SCHEMA sales;
GO

CREATE TABLE dbo.Category
(
    CategoryId   int          NOT NULL IDENTITY(1, 1),
    CategoryName nvarchar(50) NOT NULL,
    CONSTRAINT PK_Category PRIMARY KEY (CategoryId)
);

CREATE TABLE dbo.Product
(
    ProductId    int            NOT NULL IDENTITY(1, 1),
    ProductName  nvarchar(100)  NOT NULL,
    UnitPrice    decimal(18, 2) NOT NULL CONSTRAINT DF_Product_UnitPrice DEFAULT ((0)),
    Notes        nvarchar(max)  NULL,
    CategoryId   int            NOT NULL,
    DisplayLabel AS (CONVERT(nvarchar(150), ProductName + ' (' + CONVERT(varchar(20), UnitPrice) + ')')),
    CONSTRAINT PK_Product PRIMARY KEY (ProductId),
    CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryId) REFERENCES dbo.Category (CategoryId)
);

CREATE TABLE sales.[Order]
(
    OrderId     int          NOT NULL IDENTITY(1, 1),
    OrderDate   datetime2(7) NOT NULL,
    CustomerRef nvarchar(40) NULL,
    CONSTRAINT PK_Order PRIMARY KEY (OrderId)
);
