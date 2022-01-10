CREATE TABLE [MultiContext].[Function] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (255) NOT NULL,
    [DbName]    NVARCHAR (255) NULL,
    [ContextId] INT            NOT NULL,
    CONSTRAINT [PK_Function] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Function_Context_ContextId] FOREIGN KEY ([ContextId]) REFERENCES [MultiContext].[Context] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Function_ContextId]
    ON [MultiContext].[Function]([ContextId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Function_Name]
    ON [MultiContext].[Function]([ContextId] ASC, [Name] ASC);

