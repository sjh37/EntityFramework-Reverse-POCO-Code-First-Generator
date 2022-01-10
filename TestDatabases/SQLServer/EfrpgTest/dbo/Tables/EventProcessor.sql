CREATE TABLE [dbo].[EventProcessor] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [Name]            VARCHAR (200) NOT NULL,
    [Description]     VARCHAR (512) NULL,
    [EndpointAddress] VARCHAR (512) NULL,
    [Enabled]         BIT           NOT NULL,
    CONSTRAINT [PK_EventProcessor] PRIMARY KEY CLUSTERED ([Id] ASC)
);

