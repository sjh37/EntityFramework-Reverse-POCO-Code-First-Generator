CREATE TABLE [dbo].[Token] (
    [Id]      UNIQUEIDENTIFIER DEFAULT (newsequentialid()) ROWGUIDCOL NOT NULL,
    [Enabled] BIT              NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

