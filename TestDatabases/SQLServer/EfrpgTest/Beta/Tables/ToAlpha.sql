CREATE TABLE [Beta].[ToAlpha] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    [AlphaId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [BetaToAlpha_AlphaWorkflow] FOREIGN KEY ([AlphaId]) REFERENCES [Alpha].[workflow] ([Id])
);

