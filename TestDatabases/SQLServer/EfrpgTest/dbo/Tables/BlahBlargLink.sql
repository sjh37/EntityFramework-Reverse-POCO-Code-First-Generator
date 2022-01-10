CREATE TABLE [dbo].[BlahBlargLink] (
    [BlahID]  INT NOT NULL,
    [BlargID] INT NOT NULL,
    CONSTRAINT [PK_BlahBlargLink] PRIMARY KEY CLUSTERED ([BlahID] ASC, [BlargID] ASC),
    CONSTRAINT [FK_BlahBlargLink_Blah] FOREIGN KEY ([BlahID]) REFERENCES [dbo].[Blah] ([BlahID]),
    CONSTRAINT [FK_BlahBlargLink_Blarg] FOREIGN KEY ([BlargID]) REFERENCES [dbo].[Blarg] ([BlargID])
);

