CREATE TABLE [dbo].[DefaultCheckForNull] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [DescUppercase] VARCHAR (5)   DEFAULT (NULL) NULL,
    [DescLowercase] VARCHAR (5)   DEFAULT (NULL) NULL,
    [DescMixedCase] VARCHAR (5)   DEFAULT (NULL) NULL,
    [DescBrackets]  VARCHAR (5)   DEFAULT (NULL) NULL,
    [X1]            VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

