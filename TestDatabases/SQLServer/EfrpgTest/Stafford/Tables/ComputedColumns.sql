CREATE TABLE [Stafford].[ComputedColumns] (
    [Id]               INT          IDENTITY (1, 1) NOT NULL,
    [MyColumn]         VARCHAR (10) NOT NULL,
    [MyComputedColumn] AS           ([MyColumn]),
    CONSTRAINT [PK_Stafford_ComputedColumns] PRIMARY KEY CLUSTERED ([Id] ASC)
);

