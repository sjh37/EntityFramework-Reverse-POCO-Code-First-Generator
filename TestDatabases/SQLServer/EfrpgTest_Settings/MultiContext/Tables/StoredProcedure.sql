CREATE TABLE [MultiContext].[StoredProcedure] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (255) NOT NULL,
    [DbName]      NVARCHAR (255) NULL,
    [ReturnModel] NVARCHAR (255) NULL,
    [ContextId]   INT            NOT NULL,
    CONSTRAINT [PK_StoredProcedure] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StoredProcedure_Context_ContextId] FOREIGN KEY ([ContextId]) REFERENCES [MultiContext].[Context] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_StoredProcedure_ContextId]
    ON [MultiContext].[StoredProcedure]([ContextId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_StoredProcedure_Name]
    ON [MultiContext].[StoredProcedure]([ContextId] ASC, [Name] ASC);

