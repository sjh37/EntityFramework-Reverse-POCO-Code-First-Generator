
CREATE VIEW WVN.v_Articles
AS
    WITH TabRecursive AS
    (
        SELECT  ItemP.PK_Article,
                ItemP.FK_Factory,
                ItemP.FK_ArticleLevel,
                ItemP.FK_ParentArticle,
                ItemP.Code,
                CONVERT(NVARCHAR(100), ItemP.Code) AS FullCode
        FROM    WVN.Articles ItemP
        WHERE   ItemP.FK_ParentArticle IS NULL
        UNION ALL
        SELECT  Item.PK_Article,
                Item.FK_Factory,
                Item.FK_ArticleLevel,
                Item.FK_ParentArticle,
                Item.Code,
                CONVERT(NVARCHAR(100), CONCAT(Parent.FullCode, '/', Item.Code)) AS FullCode
        FROM    WVN.Articles Item
                INNER JOIN TabRecursive Parent
                    ON Parent.PK_Article = Item.FK_ParentArticle
    )
    SELECT  T.PK_Article,
            T.FK_Factory,
            T.FK_ArticleLevel,
            T.FK_ParentArticle,
            T.Code,
            T.FullCode
    FROM    TabRecursive T;
