CREATE TABLE [dbo].[BlahBlahLink_v2] (
    [BlahID]  INT NOT NULL,
    [BlahID2] INT NOT NULL,
    [dummy1]  INT NULL,
    [dummy2]  INT NOT NULL,
    [hello]   INT NOT NULL,
    CONSTRAINT [PK_BlahBlahLinkv2_ro] PRIMARY KEY CLUSTERED ([BlahID] ASC, [BlahID2] ASC),
    CONSTRAINT [FK_BlahBlahLinkv2_Blah_ro] FOREIGN KEY ([BlahID]) REFERENCES [dbo].[Blah] ([BlahID]) ON DELETE CASCADE,
    CONSTRAINT [FK_BlahBlahLinkv2_Blah_ro2] FOREIGN KEY ([BlahID2]) REFERENCES [dbo].[Blah] ([BlahID])
);

