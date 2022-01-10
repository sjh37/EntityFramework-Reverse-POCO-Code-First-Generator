CREATE TABLE [dbo].[tblOrderLines] (
    [ID]      INT          IDENTITY (1, 1) NOT NULL,
    [OrderID] INT          NOT NULL,
    [sku]     VARCHAR (15) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [tblOrdersFK] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[tblOrders] ([ID])
);

