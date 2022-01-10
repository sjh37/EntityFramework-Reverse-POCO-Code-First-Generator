CREATE TABLE [dbo].[Burak1] (
    [id]   BIGINT IDENTITY (1, 1) NOT NULL,
    [id_t] BIGINT NOT NULL,
    [num]  BIGINT NOT NULL,
    CONSTRAINT [PK_Burak1] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Burak_Test1] FOREIGN KEY ([id_t], [num]) REFERENCES [dbo].[Burak2] ([id], [num]),
    CONSTRAINT [FK_Burak_Test2] FOREIGN KEY ([id], [num]) REFERENCES [dbo].[Burak2] ([id], [num])
);

