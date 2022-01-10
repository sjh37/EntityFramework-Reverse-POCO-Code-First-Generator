CREATE TABLE [dbo].[TableB] (
    [TableBId]       INT          IDENTITY (1, 1) NOT NULL,
    [TableAId]       INT          NOT NULL,
    [ParentTableAId] INT          NULL,
    [TableBDesc]     VARCHAR (20) NULL,
    CONSTRAINT [TableB_pkey] PRIMARY KEY CLUSTERED ([TableBId] ASC, [TableAId] ASC),
    CONSTRAINT [FK_TableA_CompositeKey_Req] FOREIGN KEY ([TableAId]) REFERENCES [dbo].[TableA] ([TableAId]),
    CONSTRAINT [ParentTableB_Hierarchy] FOREIGN KEY ([TableAId], [TableBId]) REFERENCES [dbo].[TableB] ([TableBId], [TableAId])
);


GO
CREATE NONCLUSTERED INDEX [fki_ParentTableA_FK_Constraint]
    ON [dbo].[TableB]([TableAId] ASC);

