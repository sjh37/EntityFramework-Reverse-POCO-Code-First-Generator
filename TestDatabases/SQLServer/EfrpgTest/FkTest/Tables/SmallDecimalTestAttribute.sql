CREATE TABLE [FkTest].[SmallDecimalTestAttribute] (
    [FkID]        INT          NOT NULL,
    [description] VARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([FkID] ASC),
    CONSTRAINT [KateFK] FOREIGN KEY ([FkID]) REFERENCES [dbo].[SmallDecimalTest] ([id])
);

