CREATE TABLE [dbo].[CODE_PARAM_MeetingTopicDetailSource] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Code]        NVARCHAR (5)  NOT NULL,
    [Label]       NVARCHAR (50) NULL,
    [LabelENG]    NVARCHAR (50) NULL,
    [LabelESP]    NVARCHAR (50) NULL,
    [LabelFRA]    NVARCHAR (50) NULL,
    [DateCreated] DATETIME      NOT NULL,
    [DateChanged] DATETIME      NULL,
    CONSTRAINT [PK_CODE_PARAM_MeetingTopicDetailSource] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UK_CODE_PARAM_MeetingTopicDetailSource] UNIQUE NONCLUSTERED ([Code] ASC)
);

