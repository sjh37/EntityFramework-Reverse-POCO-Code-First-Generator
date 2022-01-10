CREATE TABLE [MultiContext].[Table] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (255) NOT NULL,
    [Description]   NVARCHAR (255) NULL,
    [PluralName]    NVARCHAR (255) NULL,
    [DbName]        NVARCHAR (255) NULL,
    [ContextId]     INT            NOT NULL,
    [Attributes]    NVARCHAR (500) NULL,
    [DbSetModifier] NVARCHAR (128) NULL,
    CONSTRAINT [PK_Table] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Table_Context_ContextId] FOREIGN KEY ([ContextId]) REFERENCES [MultiContext].[Context] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Table_ContextId]
    ON [MultiContext].[Table]([ContextId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Table_Name]
    ON [MultiContext].[Table]([ContextId] ASC, [Name] ASC);

