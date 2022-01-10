CREATE TABLE [WVN].[Articles] (
    [PK_Article]       INT              IDENTITY (1, 1) NOT NULL,
    [FK_Factory]       UNIQUEIDENTIFIER NOT NULL,
    [FK_ArticleLevel]  INT              NOT NULL,
    [FK_ParentArticle] INT              NULL,
    [Code]             NVARCHAR (20)    NOT NULL,
    CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED ([PK_Article] ASC),
    CONSTRAINT [UK_Articles] UNIQUE NONCLUSTERED ([FK_Factory] ASC, [FK_ArticleLevel] ASC, [Code] ASC)
);

