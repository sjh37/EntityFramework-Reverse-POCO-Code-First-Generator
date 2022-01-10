CREATE TABLE [dbo].[ClientCreationState] (
    [id]              UNIQUEIDENTIFIER NOT NULL,
    [WebhookSetup]    BIT              NOT NULL,
    [AuthSetup]       BIT              NOT NULL,
    [AssignedCarrier] BIT              NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

