CREATE TABLE [dbo].[Бренды товара] (
    [Код бренда]                  INT          IDENTITY (1, 1) NOT NULL,
    [Наименование бренда]         VARCHAR (50) NOT NULL,
    [Логотип_бренда]              IMAGE        NULL,
    [Логотип_бренда_вертикальный] IMAGE        NULL,
    CONSTRAINT [PK_Бренды] PRIMARY KEY CLUSTERED ([Код бренда] ASC)
);

