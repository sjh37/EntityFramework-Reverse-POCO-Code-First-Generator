-- This database will contain all the horrible edge cases this generator has to cope with

/*CREATE DATABASE [EfrpgTest] ON PRIMARY
       ( NAME = N'EfrpgTest',     FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\EfrpgTest.mdf' , SIZE = 5Mb , FILEGROWTH = 1024KB )
LOG ON ( NAME = N'EfrpgTest_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\EfrpgTest.ldf' , SIZE = 1024KB , FILEGROWTH = 10%);
GO
ALTER DATABASE [EfrpgTest] SET COMPATIBILITY_LEVEL = 100
ALTER DATABASE [EfrpgTest] SET ANSI_NULL_DEFAULT OFF
ALTER DATABASE [EfrpgTest] SET ANSI_NULLS OFF
ALTER DATABASE [EfrpgTest] SET ANSI_PADDING OFF
ALTER DATABASE [EfrpgTest] SET ANSI_WARNINGS OFF
ALTER DATABASE [EfrpgTest] SET ARITHABORT OFF
ALTER DATABASE [EfrpgTest] SET AUTO_CLOSE OFF
ALTER DATABASE [EfrpgTest] SET AUTO_CREATE_STATISTICS ON
ALTER DATABASE [EfrpgTest] SET AUTO_SHRINK OFF
ALTER DATABASE [EfrpgTest] SET AUTO_UPDATE_STATISTICS ON
ALTER DATABASE [EfrpgTest] SET CURSOR_CLOSE_ON_COMMIT OFF
ALTER DATABASE [EfrpgTest] SET CURSOR_DEFAULT  GLOBAL
ALTER DATABASE [EfrpgTest] SET CONCAT_NULL_YIELDS_NULL OFF
ALTER DATABASE [EfrpgTest] SET NUMERIC_ROUNDABORT OFF
ALTER DATABASE [EfrpgTest] SET QUOTED_IDENTIFIER OFF
ALTER DATABASE [EfrpgTest] SET RECURSIVE_TRIGGERS OFF
ALTER DATABASE [EfrpgTest] SET  DISABLE_BROKER
ALTER DATABASE [EfrpgTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
ALTER DATABASE [EfrpgTest] SET DATE_CORRELATION_OPTIMIZATION OFF
ALTER DATABASE [EfrpgTest] SET PARAMETERIZATION SIMPLE
ALTER DATABASE [EfrpgTest] SET  READ_WRITE
ALTER DATABASE [EfrpgTest] SET RECOVERY SIMPLE
ALTER DATABASE [EfrpgTest] SET  MULTI_USER
ALTER DATABASE [EfrpgTest] SET PAGE_VERIFY CHECKSUM
GO
USE [EfrpgTest]
GO
IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE is_default=1 AND name = N'PRIMARY')
	ALTER DATABASE [EfrpgTest] MODIFY FILEGROUP [PRIMARY] DEFAULT
GO
*/

/*
CREATE DATABASE [EfrpgTest_Synonyms] ON PRIMARY
       ( NAME = N'EfrpgTest_Synonyms',     FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\EfrpgTest_Synonyms.mdf' , SIZE = 5Mb , FILEGROWTH = 1024KB )
LOG ON ( NAME = N'EfrpgTest_Synonyms_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\EfrpgTest_Synonyms.ldf' , SIZE = 1024KB , FILEGROWTH = 10%);
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET COMPATIBILITY_LEVEL = 100
ALTER DATABASE [EfrpgTest_Synonyms] SET ANSI_NULL_DEFAULT OFF
ALTER DATABASE [EfrpgTest_Synonyms] SET ANSI_NULLS OFF
ALTER DATABASE [EfrpgTest_Synonyms] SET ANSI_PADDING OFF
ALTER DATABASE [EfrpgTest_Synonyms] SET ANSI_WARNINGS OFF
ALTER DATABASE [EfrpgTest_Synonyms] SET ARITHABORT OFF
ALTER DATABASE [EfrpgTest_Synonyms] SET AUTO_CLOSE OFF
ALTER DATABASE [EfrpgTest_Synonyms] SET AUTO_CREATE_STATISTICS ON
ALTER DATABASE [EfrpgTest_Synonyms] SET AUTO_SHRINK OFF
ALTER DATABASE [EfrpgTest_Synonyms] SET AUTO_UPDATE_STATISTICS ON
ALTER DATABASE [EfrpgTest_Synonyms] SET CURSOR_CLOSE_ON_COMMIT OFF
ALTER DATABASE [EfrpgTest_Synonyms] SET CURSOR_DEFAULT  GLOBAL
ALTER DATABASE [EfrpgTest_Synonyms] SET CONCAT_NULL_YIELDS_NULL OFF
ALTER DATABASE [EfrpgTest_Synonyms] SET NUMERIC_ROUNDABORT OFF
ALTER DATABASE [EfrpgTest_Synonyms] SET QUOTED_IDENTIFIER OFF
ALTER DATABASE [EfrpgTest_Synonyms] SET RECURSIVE_TRIGGERS OFF
ALTER DATABASE [EfrpgTest_Synonyms] SET  DISABLE_BROKER
ALTER DATABASE [EfrpgTest_Synonyms] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
ALTER DATABASE [EfrpgTest_Synonyms] SET DATE_CORRELATION_OPTIMIZATION OFF
ALTER DATABASE [EfrpgTest_Synonyms] SET PARAMETERIZATION SIMPLE
ALTER DATABASE [EfrpgTest_Synonyms] SET  READ_WRITE
ALTER DATABASE [EfrpgTest_Synonyms] SET RECOVERY SIMPLE
ALTER DATABASE [EfrpgTest_Synonyms] SET  MULTI_USER
ALTER DATABASE [EfrpgTest_Synonyms] SET PAGE_VERIFY CHECKSUM
GO
USE [EfrpgTest_Synonyms]
GO
IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE is_default=1 AND name = N'PRIMARY')
	ALTER DATABASE [EfrpgTest_Synonyms] MODIFY FILEGROUP [PRIMARY] DEFAULT
GO
*/

USE [EfrpgTest]
GO




-- #321 Fix reverse navigation properties for One-To-One relationships
CREATE SCHEMA Stafford
GO
CREATE TABLE Stafford.Boo
(
	id INT IDENTITY(1, 1) NOT NULL,
	[name] NCHAR(10) NOT NULL,
    CONSTRAINT PK_Boo PRIMARY KEY CLUSTERED (id ASC)
);
CREATE TABLE Stafford.Foo
(
	id INT NOT NULL,
    [name] NCHAR(10) NOT NULL,
    CONSTRAINT PK_Foo PRIMARY KEY CLUSTERED (id ASC)
);
GO
ALTER TABLE Stafford.Foo WITH CHECK ADD CONSTRAINT FK_Foo_Boo FOREIGN KEY(id) REFERENCES Stafford.Boo (id);
GO
ALTER TABLE Stafford.Foo CHECK CONSTRAINT FK_Foo_Boo;
GO



CREATE TABLE Stafford.ComputedColumns
(
    Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	MyColumn varchar(10) NOT NULL,
	MyComputedColumn AS MyColumn
);
GO


CREATE FUNCTION [dbo].[CsvToInt]
(
	@array varchar(8000),
	@array2 varchar(8000) = ''
)
RETURNS @IntTable TABLE(IntValue int)
AS
BEGIN
	DECLARE @seperator char(1)
	SET @seperator = ','

	DECLARE @seperator_position int
	DECLARE @array_value varchar(8000)

	SET @array = @array + @seperator

	WHILE PATINDEX('%,%', @array) <> 0
	BEGIN
		SELECT @seperator_position = PATINDEX('%,%', @array)
		SELECT @array_value = LEFT(@array, @seperator_position - 1)

		INSERT @IntTable VALUES (CAST(@array_value AS int))

		SELECT @array = STUFF(@array, 1, @seperator_position, '')
		IF LEN(@array) = 0 AND LEN(@array2) > 0
		BEGIN
			SET @array = @array2 + ','
			SET @array2 = ''
		END
	END

	RETURN
END
GO


CREATE TABLE ColumnNames
(
    [$] INT NOT null PRIMARY KEY,
    [%] INT null,
    [£] INT null,
    [&test$] INT null,
    [abc/\] INT null,
    [joe.bloggs] INT null,
    [snake-case] INT null,
    [default_test] varchar(20) NOT NULL DEFAULT (space((0))),
    someDate DATETIME2(7) NOT NULL DEFAULT (GETDATE()),
    [Obs] VARCHAR(20) NULL CONSTRAINT [DF_ColumnNamesTests_Obs] DEFAULT ('[{"k":"en","v":""},{"k":"pt","v":""}]'), -- #281 Default values must be escaped on entity classes
    [Slash1] VARCHAR(20) NULL CONSTRAINT [DF_ColumnNamesTests_Slash1] DEFAULT ('\'), -- #281
    [Slash2] VARCHAR(20) NULL CONSTRAINT [DF_ColumnNamesTests_Slash2] DEFAULT ('\\'), -- #281
    [Slash3] VARCHAR(20) NULL CONSTRAINT [DF_ColumnNamesTests_Slash3] DEFAULT ('\\\'), -- #281
    [static] INT NULL, -- #279 Illegal C#
    [readonly] INT NULL, -- #279 Illegal C#
    [123Hi] INT NULL, -- #279 Illegal C#
    [afloat] real NULL DEFAULT (1.23), -- #283 need default as 1.23f
    [adouble] FLOAT NULL DEFAULT (999.) -- #284 need default as 999.0
);
GO
EXEC sys.sp_addextendedproperty
    @name = N'MS_Description',
    @value = N'This is to document the


    table with poor column name choices',
    @level0type = N'SCHEMA', @level0name = 'dbo',
    @level1type = N'TABLE',  @level1name = 'ColumnNames';
GO
DECLARE @v sql_variant;
SET @v = N'Multi
       Line
   Comment';
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ColumnNames', N'COLUMN', N'&test$';
GO

-- This table is unusable by EF as all the columns are NULL.
-- We should see this table generated inside a comment, but with a comment that it is unusable
CREATE TABLE NoPrimaryKeys
(
    Id INT NULL,
    [Description] VARCHAR(10) NULL
)
GO

-- Create objects that will be linked to via synonyms from another database
CREATE SCHEMA Synonyms
GO

CREATE TABLE [Synonyms].[Parent](
	[ParentId] [int] NOT NULL,
	[ParentName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Parent] PRIMARY KEY CLUSTERED 
(
	[ParentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [Synonyms].[Parent] ([ParentId], [ParentName]) VALUES (1 ,'Parent 1')

CREATE TABLE [Synonyms].[Child](
	[ChildId] [int] NOT NULL,
	[ParentId] [int] NOT NULL,
	[ChildName] [varchar](100) NULL,
 CONSTRAINT [PK_Child] PRIMARY KEY CLUSTERED 
(
	[ChildId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Synonyms].[Child]  WITH CHECK ADD  CONSTRAINT [FK_Child_Parent] FOREIGN KEY([ParentId])
REFERENCES [Synonyms].[Parent] ([ParentId])
GO

ALTER TABLE [Synonyms].[Child] CHECK CONSTRAINT [FK_Child_Parent]
GO

INSERT INTO [Synonyms].[Child] ([ChildId],[ParentId],[ChildName]) VALUES (1, 1, 'Child 1')

-- Create synonyms pointing to main test dabase
USE [EfrpgTest_Synonyms]
GO

CREATE SYNONYM [dbo].[ParentSynonym] FOR [EfrpgTest].[Synonyms].[Parent]
CREATE SYNONYM [dbo].[ChildSynonym] FOR [EfrpgTest].[Synonyms].[Child]