CREATE TABLE [dbo].[HasPrincipalKeyTestParent] (
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [AA] INT NOT NULL,
    [BB] INT NOT NULL,
    [CC] INT NULL,
    [DD] INT NULL,
    CONSTRAINT [PK_HasPrincipalKeyTestParent] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_HasPrincipalKeyTestParent_AB] UNIQUE NONCLUSTERED ([AA] ASC, [BB] ASC),
    CONSTRAINT [UQ_HasPrincipalKeyTestParent_AC] UNIQUE NONCLUSTERED ([AA] ASC, [CC] ASC),
    CONSTRAINT [UQ_HasPrincipalKeyTestParent_CD] UNIQUE NONCLUSTERED ([CC] ASC, [DD] ASC)
);

