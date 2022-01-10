CREATE TABLE [Issue47].[UserRoles] (
    [UserRoleId] INT IDENTITY (1, 1) NOT NULL,
    [UserId]     INT NOT NULL,
    [RoleId]     INT NOT NULL,
    PRIMARY KEY CLUSTERED ([UserRoleId] ASC),
    CONSTRAINT [Issue47_UserRoles_roleid] FOREIGN KEY ([RoleId]) REFERENCES [Issue47].[Role] ([RoleId]),
    CONSTRAINT [Issue47_UserRoles_userid] FOREIGN KEY ([UserId]) REFERENCES [Issue47].[Users] ([UserId])
);

