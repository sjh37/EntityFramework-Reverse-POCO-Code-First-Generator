CREATE TABLE [dbo].[Attendee] (
    [AttendeeID]     BIGINT        NOT NULL,
    [Lastname]       NVARCHAR (50) NOT NULL,
    [Firstname]      NVARCHAR (50) NOT NULL,
    [PhoneCountryID] INT           NULL,
    CONSTRAINT [PK_Attendee] PRIMARY KEY CLUSTERED ([AttendeeID] ASC),
    CONSTRAINT [FK_Attendee_PhoneCountry] FOREIGN KEY ([PhoneCountryID]) REFERENCES [dbo].[Country] ([CountryID])
);

