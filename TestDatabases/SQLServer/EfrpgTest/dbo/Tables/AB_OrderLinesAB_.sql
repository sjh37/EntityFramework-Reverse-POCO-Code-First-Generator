CREATE TABLE [dbo].[AB_OrderLinesAB_] (
    [ID]      INT          IDENTITY (1, 1) NOT NULL,
    [OrderID] INT          NOT NULL,
    [sku]     VARCHAR (15) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [AB_OrderLinesAB_FK] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[AB_OrdersAB_] ([ID])
);

