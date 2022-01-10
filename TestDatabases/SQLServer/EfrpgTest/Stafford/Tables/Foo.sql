CREATE TABLE [Stafford].[Foo] (
    [id]   INT        NOT NULL,
    [name] NCHAR (10) NOT NULL,
    CONSTRAINT [PK_Foo] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Foo_Boo] FOREIGN KEY ([id]) REFERENCES [Stafford].[Boo] ([id])
);

