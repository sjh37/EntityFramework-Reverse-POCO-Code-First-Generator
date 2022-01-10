CREATE TABLE [dbo].[ForeignKeyIsNotEnforcedItem] (
    [id]             INT IDENTITY (1, 1) NOT NULL,
    [null_value]     INT NULL,
    [not_null_value] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_ForeignKeyIsNotEnforcedItem_notnull_notnull] FOREIGN KEY ([not_null_value]) REFERENCES [dbo].[ForeignKeyIsNotEnforced] ([not_null_value]),
    CONSTRAINT [FK_ForeignKeyIsNotEnforcedItem_notnull_null] FOREIGN KEY ([not_null_value]) REFERENCES [dbo].[ForeignKeyIsNotEnforced] ([null_value]),
    CONSTRAINT [FK_ForeignKeyIsNotEnforcedItem_null_notnull] FOREIGN KEY ([null_value]) REFERENCES [dbo].[ForeignKeyIsNotEnforced] ([not_null_value]),
    CONSTRAINT [FK_ForeignKeyIsNotEnforcedItem_null_null] FOREIGN KEY ([null_value]) REFERENCES [dbo].[ForeignKeyIsNotEnforced] ([null_value]),
    CONSTRAINT [UQ_ForeignKeyIsNotEnforcedItem_not_null_value] UNIQUE NONCLUSTERED ([not_null_value] ASC),
    CONSTRAINT [UQ_ForeignKeyIsNotEnforcedItem_null_value] UNIQUE NONCLUSTERED ([null_value] ASC)
);

