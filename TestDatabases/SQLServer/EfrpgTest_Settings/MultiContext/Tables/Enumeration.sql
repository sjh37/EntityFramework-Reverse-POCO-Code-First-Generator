CREATE TABLE [MultiContext].[Enumeration] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (255) NOT NULL,
    [Table]      NVARCHAR (255) NOT NULL,
    [NameField]  NVARCHAR (255) NOT NULL,
    [ValueField] NVARCHAR (255) NOT NULL,
    [ContextId]  INT            NOT NULL,
    CONSTRAINT [PK_Enumeration] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Enumeration_Context_ContextId] FOREIGN KEY ([ContextId]) REFERENCES [MultiContext].[Context] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Enumeration_ContextId]
    ON [MultiContext].[Enumeration]([ContextId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Enumeration_Name]
    ON [MultiContext].[Enumeration]([ContextId] ASC, [Name] ASC);

