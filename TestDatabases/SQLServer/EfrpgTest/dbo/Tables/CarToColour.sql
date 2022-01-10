CREATE TABLE [dbo].[CarToColour] (
    [CarId]    INT NOT NULL,
    [ColourId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([CarId] ASC, [ColourId] ASC),
    CONSTRAINT [CarToColour_CarId] FOREIGN KEY ([CarId]) REFERENCES [dbo].[Car] ([Id]),
    CONSTRAINT [CarToColour_ColourId] FOREIGN KEY ([ColourId]) REFERENCES [dbo].[Colour] ([Id])
);

