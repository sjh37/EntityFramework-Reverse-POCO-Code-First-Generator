CREATE TABLE [dbo].[PropertyTypesToAdd] (
    [id]           INT           NOT NULL,
    [dt_default]   DATETIME2 (7) NULL,
    [dt7]          DATETIME2 (7) NULL,
    [defaultCheck] VARCHAR (10)  NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

