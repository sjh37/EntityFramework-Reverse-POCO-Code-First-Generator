CREATE TABLE [dbo].[TableA] (
    [TableAId]   INT          IDENTITY (1, 1) NOT NULL,
    [TableADesc] VARCHAR (20) NULL,
    CONSTRAINT [TableA_pkey] PRIMARY KEY CLUSTERED ([TableAId] ASC)
);

