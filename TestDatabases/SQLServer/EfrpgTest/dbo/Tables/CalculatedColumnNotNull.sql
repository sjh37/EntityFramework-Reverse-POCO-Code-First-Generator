CREATE TABLE [dbo].[CalculatedColumnNotNull] (
    [ID]            INT     IDENTITY (1, 1) NOT NULL,
    [Type]          TINYINT NOT NULL,
    [IsCalendar]    AS      (CONVERT([bit],case when [Type]>=(0) AND [Type]<=(7) then (1) else (0) end,0)) PERSISTED NOT NULL,
    [IsUtilization] AS      (CONVERT([bit],case when [Type]>=(8) AND [Type]<=(10) then (1) else (0) end,0)) PERSISTED NOT NULL,
    CONSTRAINT [PK_CalculatedColumnNotNull] PRIMARY KEY NONCLUSTERED ([ID] ASC)
);

