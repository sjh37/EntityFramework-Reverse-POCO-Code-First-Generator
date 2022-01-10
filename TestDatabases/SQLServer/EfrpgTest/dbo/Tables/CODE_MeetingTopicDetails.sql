CREATE TABLE [dbo].[CODE_MeetingTopicDetails] (
    [id]              INT            IDENTITY (1, 1) NOT NULL,
    [id_reuniao]      INT            NOT NULL,
    [ord_trab]        INT            NULL,
    [assunto]         NVARCHAR (250) NULL,
    [desenvolvimento] NVARCHAR (MAX) NULL,
    [origem]          NVARCHAR (5)   NULL,
    [id_origem]       INT            NULL,
    [Estado]          INT            NULL,
    [CompanyID]       INT            NOT NULL,
    [DateCreated]     DATETIME       NOT NULL,
    [DateChanged]     DATETIME       NULL,
    CONSTRAINT [PK_CODE_MeetingTopicDetails] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_CODE_MeetingTopicDetails_CODE_PARAM_MeetingTopicDetailSource] FOREIGN KEY ([origem]) REFERENCES [dbo].[CODE_PARAM_MeetingTopicDetailSource] ([Code])
);

