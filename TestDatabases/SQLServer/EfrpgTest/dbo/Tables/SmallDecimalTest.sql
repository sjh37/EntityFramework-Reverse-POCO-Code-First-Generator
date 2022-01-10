CREATE TABLE [dbo].[SmallDecimalTest] (
    [id]       INT            NOT NULL,
    [KoeffVed] DECIMAL (4, 4) DEFAULT ((0.5)) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

