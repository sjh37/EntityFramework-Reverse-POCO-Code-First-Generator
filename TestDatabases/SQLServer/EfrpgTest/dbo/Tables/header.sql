CREATE TABLE [dbo].[header] (
    [ID]        INT      NOT NULL,
    [anotherID] INT      NOT NULL,
    [added]     DATETIME DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC, [anotherID] ASC)
);

