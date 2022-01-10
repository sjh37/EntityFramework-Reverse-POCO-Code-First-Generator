CREATE TABLE [dbo].[DateTimeDefaultTest] (
    [Id]          INT                NOT NULL,
    [CreatedDate] DATETIMEOFFSET (7) CONSTRAINT [DF_DateTimeDefaultTest_CreatedDate] DEFAULT (getdate()) NULL
);

