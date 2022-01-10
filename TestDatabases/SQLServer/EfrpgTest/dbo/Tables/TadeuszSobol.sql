CREATE TABLE [dbo].[TadeuszSobol] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (MAX)  NULL,
    [Notes]       NVARCHAR (MAX) NULL,
    [Name]        VARCHAR (10)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

