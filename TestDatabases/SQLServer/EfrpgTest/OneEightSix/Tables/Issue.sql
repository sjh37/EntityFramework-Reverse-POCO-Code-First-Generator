CREATE TABLE [OneEightSix].[Issue] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Title]             NVARCHAR (100) NOT NULL,
    [Content]           NVARCHAR (MAX) NULL,
    [ConsentDocumentId] INT            NULL,
    CONSTRAINT [PK_Issue] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Issue_UploadedFileConsentDocument] FOREIGN KEY ([ConsentDocumentId]) REFERENCES [OneEightSix].[UploadedFile] ([Id])
);

