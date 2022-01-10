CREATE TABLE [MultiContext].[Column] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (255) NOT NULL,
    [DbName]           NVARCHAR (255) NULL,
    [IsPrimaryKey]     BIT            NULL,
    [OverrideModifier] BIT            NULL,
    [EnumType]         NVARCHAR (255) NULL,
    [TableId]          INT            NOT NULL,
    [Attributes]       NVARCHAR (500) NULL,
    [Test]             VARCHAR (10)   NULL,
    [DummyInt]         INT            NULL,
    [date_of_birth]    DATETIME       NULL,
    [PropertyType]     NVARCHAR (128) NULL,
    [IsNullable]       BIT            NULL,
    CONSTRAINT [PK_Column] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Column_Table_TableId] FOREIGN KEY ([TableId]) REFERENCES [MultiContext].[Table] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Column_TableId]
    ON [MultiContext].[Column]([TableId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Column_Name]
    ON [MultiContext].[Column]([TableId] ASC, [Name] ASC);

