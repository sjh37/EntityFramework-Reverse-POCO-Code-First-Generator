CREATE TABLE [dbo].[User_Document] (
    [ID]              INT IDENTITY (1, 1) NOT NULL,
    [UserID]          INT NOT NULL,
    [CreatedByUserID] INT NOT NULL,
    CONSTRAINT [PK_User_Document] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_User_Document_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_User_Document_User1] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[User] ([ID])
);

