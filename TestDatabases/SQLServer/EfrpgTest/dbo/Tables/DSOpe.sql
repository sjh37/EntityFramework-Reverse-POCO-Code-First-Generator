CREATE TABLE [dbo].[DSOpe] (
    [ID]               INT              NOT NULL,
    [decimal_default]  DECIMAL (15, 2)  CONSTRAINT [DF_DSOpe_MaxRabat] DEFAULT ((99.99)) NOT NULL,
    [MyGuid]           UNIQUEIDENTIFIER DEFAULT ('9B7E1F67-5A81-4277-BC7D-06A3262A5C70') NOT NULL,
    [default]          VARCHAR (10)     NULL,
    [MyGuidBadDefault] UNIQUEIDENTIFIER CONSTRAINT [DF_MyGuidBadDefaul] DEFAULT (NULL) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

