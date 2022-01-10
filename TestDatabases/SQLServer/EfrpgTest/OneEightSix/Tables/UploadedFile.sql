CREATE TABLE [OneEightSix].[UploadedFile] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [FullPath] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_UploadedFile] PRIMARY KEY CLUSTERED ([Id] ASC)
);

