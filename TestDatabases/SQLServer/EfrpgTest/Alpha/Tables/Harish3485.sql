CREATE TABLE [Alpha].[Harish3485] (
    [id]        INT IDENTITY (1, 1) NOT NULL,
    [harish_id] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Harish] FOREIGN KEY ([harish_id]) REFERENCES [FkTest].[SmallDecimalTestAttribute] ([FkID])
);

