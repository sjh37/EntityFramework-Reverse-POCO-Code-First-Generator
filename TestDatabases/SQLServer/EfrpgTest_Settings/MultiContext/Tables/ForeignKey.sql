CREATE TABLE [MultiContext].[ForeignKey] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [ContextId]           INT            NOT NULL,
    [ConstraintName]      NVARCHAR (128) NOT NULL,
    [ParentName]          NVARCHAR (128) NULL,
    [ChildName]           NVARCHAR (128) NULL,
    [PkSchema]            NVARCHAR (128) NULL,
    [PkTableName]         NVARCHAR (128) NOT NULL,
    [PkColumn]            NVARCHAR (128) NOT NULL,
    [FkSchema]            NVARCHAR (128) NULL,
    [FkTableName]         NVARCHAR (128) NOT NULL,
    [FkColumn]            NVARCHAR (128) NOT NULL,
    [Ordinal]             INT            NOT NULL,
    [CascadeOnDelete]     BIT            NOT NULL,
    [IsNotEnforced]       BIT            NOT NULL,
    [HasUniqueConstraint] BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ForeignKey] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ForeignKey_Context_ContextId] FOREIGN KEY ([ContextId]) REFERENCES [MultiContext].[Context] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ForeignKey_ContextId]
    ON [MultiContext].[ForeignKey]([ContextId] ASC);

