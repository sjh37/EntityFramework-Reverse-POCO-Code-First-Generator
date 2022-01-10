CREATE TABLE [Beta].[Harish3485] (
    [id]         INT IDENTITY (1, 1) NOT NULL,
    [another_id] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Harish] FOREIGN KEY ([another_id]) REFERENCES [dbo].[PropertyTypesToAdd] ([id])
);

