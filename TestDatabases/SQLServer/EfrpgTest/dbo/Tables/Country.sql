CREATE TABLE [dbo].[Country] (
    [CountryID] INT          IDENTITY (1, 1) NOT NULL,
    [Code]      VARCHAR (12) NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([CountryID] ASC)
);

