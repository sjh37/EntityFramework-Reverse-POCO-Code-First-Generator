CREATE TABLE [dbo].[ForeignKeyIsNotEnforced] (
    [id]             INT IDENTITY (1, 1) NOT NULL,
    [null_value]     INT NULL,
    [not_null_value] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [UQ_ForeignKeyIsNotEnforced_not_null_value] UNIQUE NONCLUSTERED ([not_null_value] ASC),
    CONSTRAINT [UQ_ForeignKeyIsNotEnforced_null_value] UNIQUE NONCLUSTERED ([null_value] ASC)
);

