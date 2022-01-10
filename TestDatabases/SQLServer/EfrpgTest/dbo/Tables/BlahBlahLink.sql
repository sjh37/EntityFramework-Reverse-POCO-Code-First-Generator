CREATE TABLE [dbo].[BlahBlahLink] (
    [BlahID]  INT NOT NULL,
    [BlahID2] INT NOT NULL,
    CONSTRAINT [PK_BlahBlahLink] PRIMARY KEY CLUSTERED ([BlahID] ASC, [BlahID2] ASC),
    CONSTRAINT [FK_BlahBlahLink_Blah] FOREIGN KEY ([BlahID]) REFERENCES [dbo].[Blah] ([BlahID]),
    CONSTRAINT [FK_BlahBlahLink_Blah2] FOREIGN KEY ([BlahID2]) REFERENCES [dbo].[Blah] ([BlahID])
);

