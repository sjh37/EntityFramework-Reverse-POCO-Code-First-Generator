CREATE TABLE [dbo].[tblOrderErrors] (
    [ID]    INT          IDENTITY (1, 1) NOT NULL,
    [error] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

