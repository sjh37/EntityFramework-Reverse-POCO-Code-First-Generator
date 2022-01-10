CREATE TABLE [dbo].[CodeObject] (
    [codeObjectNo]  INT            CONSTRAINT [DF__Object__objectNo__7E6CC920] DEFAULT ((0)) NOT NULL,
    [applicationNo] INT            NULL,
    [type]          INT            NOT NULL,
    [eName]         NVARCHAR (250) NOT NULL,
    [aName]         NVARCHAR (250) NULL,
    [description]   NVARCHAR (250) NULL,
    [codeName]      NVARCHAR (250) NULL,
    [note]          NVARCHAR (250) NULL,
    [isObject]      BIT            CONSTRAINT [DF__Object__isObject__7F60ED59] DEFAULT ((0)) NOT NULL,
    [versionNumber] ROWVERSION     NULL,
    CONSTRAINT [aaaaaObject_PK] PRIMARY KEY NONCLUSTERED ([codeObjectNo] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'This is a test', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CodeObject';

