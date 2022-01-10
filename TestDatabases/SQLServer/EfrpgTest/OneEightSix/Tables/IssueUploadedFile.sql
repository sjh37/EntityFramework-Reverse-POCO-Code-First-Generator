CREATE TABLE [OneEightSix].[IssueUploadedFile] (
    [UploadedFileId] INT NOT NULL,
    [IssueId]        INT NOT NULL,
    CONSTRAINT [PK_IssueUploadedFile] PRIMARY KEY CLUSTERED ([UploadedFileId] ASC, [IssueId] ASC),
    CONSTRAINT [FK_IssueUploadedFile_Issue] FOREIGN KEY ([IssueId]) REFERENCES [OneEightSix].[Issue] ([Id]),
    CONSTRAINT [FK_IssueUploadedFile_UploadedFile] FOREIGN KEY ([UploadedFileId]) REFERENCES [OneEightSix].[UploadedFile] ([Id])
);

