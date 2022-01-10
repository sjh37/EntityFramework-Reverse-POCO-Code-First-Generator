CREATE TABLE [dbo].[AAREF] (
    [C1]         INT           NOT NULL,
    [C2]         INT           NOT NULL,
    [CreatedUTC] DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_AREF] PRIMARY KEY CLUSTERED ([C1] ASC, [C2] ASC)
);

