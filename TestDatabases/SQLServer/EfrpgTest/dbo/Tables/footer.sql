CREATE TABLE [dbo].[footer] (
    [ID]      INT      IDENTITY (1, 1) NOT NULL,
    [otherID] INT      NOT NULL,
    [added]   DATETIME DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [fooderFK] FOREIGN KEY ([ID], [otherID]) REFERENCES [dbo].[header] ([ID], [anotherID])
);

