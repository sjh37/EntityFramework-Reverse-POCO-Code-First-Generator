CREATE TABLE [dbo].[table with duplicate column names] (
    [id]       INT IDENTITY (1, 1) NOT NULL,
    [user_id]  INT NOT NULL,
    [UserId]   INT NOT NULL,
    [User Id]  INT NOT NULL,
    [User  Id] INT NOT NULL,
    [user__id] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

