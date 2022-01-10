CREATE TABLE [dbo].[tblOrders] (
    [ID]    INT      IDENTITY (1, 1) NOT NULL,
    [added] DATETIME DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

