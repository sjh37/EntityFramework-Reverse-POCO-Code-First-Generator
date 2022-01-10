CREATE TABLE [dbo].[Car] (
    [Id]                        INT           NOT NULL,
    [PrimaryColourId]           INT           NOT NULL,
    [CarMake]                   VARCHAR (255) NOT NULL,
    [computed_column]           AS            ([PrimaryColourId]*(10)),
    [computed_column_persisted] AS            ([PrimaryColourId]*(10)) PERSISTED NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CarPrimaryColourFK] FOREIGN KEY ([PrimaryColourId]) REFERENCES [dbo].[Colour] ([Id])
);

