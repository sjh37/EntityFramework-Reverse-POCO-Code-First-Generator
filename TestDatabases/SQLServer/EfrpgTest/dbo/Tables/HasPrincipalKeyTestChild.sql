CREATE TABLE [dbo].[HasPrincipalKeyTestChild] (
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [A]  INT NOT NULL,
    [B]  INT NOT NULL,
    [C]  INT NULL,
    [D]  INT NULL,
    CONSTRAINT [PK_HasPrincipalKeyTestChild] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HasPrincipalKey_AB] FOREIGN KEY ([A], [B]) REFERENCES [dbo].[HasPrincipalKeyTestParent] ([AA], [BB]),
    CONSTRAINT [FK_HasPrincipalKey_AC] FOREIGN KEY ([A], [C]) REFERENCES [dbo].[HasPrincipalKeyTestParent] ([AA], [CC]),
    CONSTRAINT [FK_HasPrincipalKey_CD] FOREIGN KEY ([C], [D]) REFERENCES [dbo].[HasPrincipalKeyTestParent] ([CC], [DD])
);

