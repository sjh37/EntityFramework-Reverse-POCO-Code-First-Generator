CREATE TABLE [dbo].[CMS_FileTag] (
    [FileId] INT NOT NULL,
    [TagId]  INT NOT NULL,
    CONSTRAINT [FK_CMS_FileTag_CMS_File] FOREIGN KEY ([FileId]) REFERENCES [dbo].[CMS_File] ([FileId]),
    CONSTRAINT [FK_CMS_FileTag_CMS_Tag] FOREIGN KEY ([TagId]) REFERENCES [dbo].[CMS_Tag] ([TagId])
);

