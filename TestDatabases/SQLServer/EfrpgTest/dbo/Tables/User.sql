CREATE TABLE [dbo].[User] (
    [ID]             INT          IDENTITY (1, 1) NOT NULL,
    [ExternalUserID] VARCHAR (50) NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED ([ID] ASC)
);

