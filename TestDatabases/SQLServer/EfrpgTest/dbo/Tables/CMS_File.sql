CREATE TABLE [dbo].[CMS_File] (
    [FileId]          INT            IDENTITY (1, 1) NOT NULL,
    [FileName]        NVARCHAR (100) NOT NULL,
    [FileDescription] VARCHAR (500)  NOT NULL,
    [FileIdentifier]  VARCHAR (100)  NOT NULL,
    [ValidStartDate]  DATETIME       NULL,
    [ValidEndDate]    DATETIME       NULL,
    [IsActive]        BIT            NOT NULL,
    CONSTRAINT [PK_CMS_Form] PRIMARY KEY CLUSTERED ([FileId] ASC)
);

