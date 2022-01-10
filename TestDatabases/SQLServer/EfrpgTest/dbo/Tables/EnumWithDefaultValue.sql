CREATE TABLE [dbo].[EnumWithDefaultValue] (
    [Id]       INT IDENTITY (1, 1) NOT NULL,
    [SomeEnum] INT DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

