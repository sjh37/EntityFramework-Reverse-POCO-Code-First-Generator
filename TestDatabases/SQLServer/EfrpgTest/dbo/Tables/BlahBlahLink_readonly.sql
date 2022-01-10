CREATE TABLE [dbo].[BlahBlahLink_readonly] (
    [BlahID]     INT        NOT NULL,
    [BlahID2]    INT        NOT NULL,
    [RowVersion] ROWVERSION NULL,
    [id]         INT        IDENTITY (1, 1) NOT NULL,
    [id2]        AS         ([id]+(100)),
    CONSTRAINT [PK_BlahBlahLink_ro] PRIMARY KEY CLUSTERED ([BlahID] ASC, [BlahID2] ASC),
    CONSTRAINT [FK_BlahBlahLink_Blah_ro] FOREIGN KEY ([BlahID]) REFERENCES [dbo].[Blah] ([BlahID]),
    CONSTRAINT [FK_BlahBlahLink_Blah_ro2] FOREIGN KEY ([BlahID2]) REFERENCES [dbo].[Blah] ([BlahID])
);

