CREATE TABLE [dbo].[User309] (
    [UserID]         BIGINT         NOT NULL,
    [Lastname]       NVARCHAR (100) NOT NULL,
    [Firstname]      NVARCHAR (100) NOT NULL,
    [PhoneCountryID] INT            NULL,
    CONSTRAINT [PK_User309] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_User309_PhoneCountry] FOREIGN KEY ([PhoneCountryID]) REFERENCES [dbo].[Country] ([CountryID])
);

