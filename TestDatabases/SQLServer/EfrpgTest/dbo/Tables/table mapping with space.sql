CREATE TABLE [dbo].[table mapping with space] (
    [id]       INT NOT NULL,
    [id value] INT NOT NULL,
    CONSTRAINT [map_with_space] PRIMARY KEY CLUSTERED ([id] ASC, [id value] ASC),
    CONSTRAINT [space1FK] FOREIGN KEY ([id]) REFERENCES [dbo].[table with space] ([id]),
    CONSTRAINT [space2FK] FOREIGN KEY ([id value]) REFERENCES [dbo].[table with space and in columns] ([id value])
);

