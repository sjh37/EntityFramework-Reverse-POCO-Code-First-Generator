CREATE TABLE [dbo].[EventProcessorEventFilter] (
    [Id]               INT IDENTITY (1, 1) NOT NULL,
    [EventProcessorId] INT NOT NULL,
    [WantedEventId]    INT NOT NULL,
    CONSTRAINT [PK_EventProcessorEventFilter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EventProcessorEventFilter__EventProcessor] FOREIGN KEY ([EventProcessorId]) REFERENCES [dbo].[EventProcessor] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_EventProcessorEventFilter]
    ON [dbo].[EventProcessorEventFilter]([EventProcessorId] ASC, [WantedEventId] ASC);

