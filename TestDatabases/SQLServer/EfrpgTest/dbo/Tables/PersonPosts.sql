CREATE TABLE [dbo].[PersonPosts] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Title]     VARCHAR (20)  NOT NULL,
    [Body]      VARCHAR (100) NOT NULL,
    [CreatedBy] INT           NOT NULL,
    [UpdatedBy] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonPosts_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Person] ([Id]),
    CONSTRAINT [FK_PersonPosts_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[Person] ([Id])
);

