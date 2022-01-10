CREATE TABLE [dbo].[SequenceTest] (
    [Id]            INT          DEFAULT (NEXT VALUE FOR [dbo].[CountBy1]) NOT NULL,
    [CntByBigInt]   BIGINT       DEFAULT (NEXT VALUE FOR [dbo].[CountByBigInt]) NOT NULL,
    [CntByTinyInt]  TINYINT      DEFAULT (NEXT VALUE FOR [dbo].[CountByTinyInt]) NOT NULL,
    [CntBySmallInt] SMALLINT     DEFAULT (NEXT VALUE FOR [dbo].[CountBySmallInt]) NOT NULL,
    [CntByDecimal]  DECIMAL (18) DEFAULT (NEXT VALUE FOR [dbo].[CountByDecimal]) NOT NULL,
    [CntByNumeric]  NUMERIC (18) DEFAULT (NEXT VALUE FOR [dbo].[CountByNumeric]) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

