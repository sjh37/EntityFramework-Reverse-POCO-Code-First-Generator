CREATE TABLE [Synonyms].[Child] (
    [ChildId]   INT           NOT NULL,
    [ParentId]  INT           NOT NULL,
    [ChildName] VARCHAR (100) NULL,
    CONSTRAINT [PK_Child] PRIMARY KEY CLUSTERED ([ChildId] ASC),
    CONSTRAINT [FK_Child_Parent] FOREIGN KEY ([ParentId]) REFERENCES [Synonyms].[Parent] ([ParentId])
);

