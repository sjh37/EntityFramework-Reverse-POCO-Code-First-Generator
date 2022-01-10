CREATE TABLE [dbo].[UserInfoAttributes] (
    [Id]          INT IDENTITY (1, 1) NOT NULL,
    [PrimaryId]   INT NOT NULL,
    [SecondaryId] INT NOT NULL,
    CONSTRAINT [PK_UserInfoAttributes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserInfoAttributes_PrimaryUserInfo] FOREIGN KEY ([PrimaryId]) REFERENCES [dbo].[UserInfo] ([Id]),
    CONSTRAINT [FK_UserInfoAttributes_SecondaryUserInfo] FOREIGN KEY ([SecondaryId]) REFERENCES [dbo].[UserInfo] ([Id])
);

