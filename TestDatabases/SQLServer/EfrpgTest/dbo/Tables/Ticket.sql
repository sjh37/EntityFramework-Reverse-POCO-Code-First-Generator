CREATE TABLE [dbo].[Ticket] (
    [Id]           BIGINT IDENTITY (1, 1) NOT NULL,
    [CreatedById]  BIGINT NOT NULL,
    [ModifiedById] BIGINT NULL,
    CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Ticket_AppUser] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[AppUser] ([Id]),
    CONSTRAINT [FK_Ticket_AppUser1] FOREIGN KEY ([ModifiedById]) REFERENCES [dbo].[AppUser] ([Id])
);

