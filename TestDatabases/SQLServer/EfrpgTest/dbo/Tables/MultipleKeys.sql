CREATE TABLE [dbo].[MultipleKeys] (
    [UserId]            INT NOT NULL,
    [FavouriteColourId] INT NOT NULL,
    [BestHolidayTypeId] INT NOT NULL,
    [BankId]            INT NOT NULL,
    [CarId]             INT NOT NULL,
    CONSTRAINT [PK_MultipleKeys] PRIMARY KEY CLUSTERED ([UserId] ASC, [FavouriteColourId] ASC, [BestHolidayTypeId] ASC),
    CONSTRAINT [UC_MultipleKeys_FavouriteColour] UNIQUE NONCLUSTERED ([FavouriteColourId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MultipleKeys_Holiday_Bank]
    ON [dbo].[MultipleKeys]([BestHolidayTypeId] ASC, [BankId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MultipleKeys_BestHolidayType]
    ON [dbo].[MultipleKeys]([BestHolidayTypeId] ASC);

