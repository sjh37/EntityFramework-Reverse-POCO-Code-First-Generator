CREATE TABLE [MultiContext].[Context] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (255) NOT NULL,
    [Description]  NVARCHAR (255) NULL,
    [BaseSchema]   NVARCHAR (255) NULL,
    [TemplatePath] NVARCHAR (500) NULL,
    [Namespace]    NVARCHAR (128) NULL,
    [Filename]     NVARCHAR (128) NULL,
    CONSTRAINT [PK_Context] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Context_Name]
    ON [MultiContext].[Context]([Name] ASC);

