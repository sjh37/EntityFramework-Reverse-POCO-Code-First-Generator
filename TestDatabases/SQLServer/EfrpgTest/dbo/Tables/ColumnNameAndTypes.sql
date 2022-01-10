CREATE TABLE [dbo].[ColumnNameAndTypes] (
    [$]             INT               NOT NULL,
    [%]             INT               NULL,
    [£]             INT               NULL,
    [&fred$]        INT               NULL,
    [abc/\]         INT               NULL,
    [joe.bloggs]    INT               NULL,
    [simon-hughes]  INT               NULL,
    [description]   VARCHAR (20)      DEFAULT (space((0))) NOT NULL,
    [someDate]      DATETIME2 (7)     DEFAULT (getdate()) NOT NULL,
    [Obs]           VARCHAR (50)      CONSTRAINT [DF__PlanStudies_Obs] DEFAULT ('[{"k":"en","v":""},{"k":"pt","v":""}]') NULL,
    [Obs1]          VARCHAR (50)      CONSTRAINT [DF__PlanStudies_Obs1] DEFAULT ('\') NULL,
    [Obs2]          VARCHAR (50)      CONSTRAINT [DF__PlanStudies_Obs2] DEFAULT ('\\') NULL,
    [Obs3]          VARCHAR (50)      CONSTRAINT [DF__PlanStudies_Obs3] DEFAULT ('\\\') NULL,
    [static]        INT               NULL,
    [readonly]      INT               NULL,
    [123Hi]         INT               NULL,
    [areal]         REAL              DEFAULT ((1.23)) NULL,
    [afloat]        FLOAT (53)        DEFAULT ((999.)) NULL,
    [afloat8]       REAL              NULL,
    [afloat20]      REAL              NULL,
    [afloat24]      REAL              NULL,
    [afloat53]      FLOAT (53)        NULL,
    [adecimal]      DECIMAL (18)      NULL,
    [adecimal_19_4] DECIMAL (19, 4)   NULL,
    [adecimal_10_3] DECIMAL (10, 3)   NULL,
    [anumeric]      NUMERIC (18)      NULL,
    [anumeric_5_2]  NUMERIC (5, 2)    NULL,
    [anumeric_11_3] NUMERIC (11, 3)   NULL,
    [amoney]        MONEY             NULL,
    [asmallmoney]   SMALLMONEY        NULL,
    [brandon]       INT               NULL,
    [GeographyType] [sys].[geography] DEFAULT (CONVERT([geography],'POINT (0 0)',0)) NULL,
    [GeometryType]  [sys].[geometry]  DEFAULT ([GEOMETRY]::STGeomFromText('LINESTRING (100 100, 20 180, 180 180)',(0))) NULL,
    PRIMARY KEY CLUSTERED ([$] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'This is to document the bring the action table', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ColumnNameAndTypes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description_v2', @value = N'This is to document the


    table with poor column name choices', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ColumnNameAndTypes';

