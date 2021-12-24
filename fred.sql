USE [master]
GO
/****** Object:  Database [fred]    Script Date: 15/11/21 18:11:02 ******/
CREATE DATABASE [fred]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'fred', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\fred.mdf' , SIZE = 7168KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'fred_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\fred_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [fred] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [fred].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [fred] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [fred] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [fred] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [fred] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [fred] SET ARITHABORT OFF 
GO
ALTER DATABASE [fred] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [fred] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [fred] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [fred] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [fred] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [fred] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [fred] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [fred] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [fred] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [fred] SET  DISABLE_BROKER 
GO
ALTER DATABASE [fred] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [fred] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [fred] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [fred] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [fred] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [fred] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [fred] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [fred] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [fred] SET  MULTI_USER 
GO
ALTER DATABASE [fred] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [fred] SET DB_CHAINING OFF 
GO
ALTER DATABASE [fred] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [fred] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [fred] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'fred', N'ON'
GO
ALTER DATABASE [fred] SET QUERY_STORE = OFF
GO
USE [fred]
GO
/****** Object:  Schema [Alpha]    Script Date: 15/11/21 18:11:03 ******/
CREATE SCHEMA [Alpha]
GO
/****** Object:  Schema [App]    Script Date: 15/11/21 18:11:03 ******/
CREATE SCHEMA [App]
GO
/****** Object:  Schema [Beta]    Script Date: 15/11/21 18:11:03 ******/
CREATE SCHEMA [Beta]
GO
/****** Object:  Schema [dcg]    Script Date: 15/11/21 18:11:03 ******/
CREATE SCHEMA [dcg]
GO
/****** Object:  Schema [FFRS]    Script Date: 15/11/21 18:11:03 ******/
CREATE SCHEMA [FFRS]
GO
/****** Object:  Schema [Issue47]    Script Date: 15/11/21 18:11:03 ******/
CREATE SCHEMA [Issue47]
GO
/****** Object:  Schema [Kate]    Script Date: 15/11/21 18:11:03 ******/
CREATE SCHEMA [Kate]
GO
/****** Object:  Schema [Omega]    Script Date: 15/11/21 18:11:03 ******/
CREATE SCHEMA [Omega]
GO
/****** Object:  Schema [OneEightSix]    Script Date: 15/11/21 18:11:03 ******/
CREATE SCHEMA [OneEightSix]
GO
/****** Object:  Schema [RichardGriffiths]    Script Date: 15/11/21 18:11:03 ******/
CREATE SCHEMA [RichardGriffiths]
GO
/****** Object:  Default [d_t_address_type_domain]    Script Date: 15/11/21 18:11:03 ******/
CREATE DEFAULT [dbo].[d_t_address_type_domain] 
AS
'A'
GO
USE [fred]
GO
/****** Object:  Sequence [dbo].[CountBy1]    Script Date: 15/11/21 18:11:03 ******/
CREATE SEQUENCE [dbo].[CountBy1] 
 AS [int]
 START WITH 1
 INCREMENT BY 1
 MINVALUE -2147483648
 MAXVALUE 2147483647
 CACHE 
GO
USE [fred]
GO
/****** Object:  Sequence [dbo].[CountByBigInt]    Script Date: 15/11/21 18:11:03 ******/
CREATE SEQUENCE [dbo].[CountByBigInt] 
 AS [bigint]
 START WITH 22
 INCREMENT BY 234
 MINVALUE 1
 MAXVALUE 9876543
 CYCLE 
 CACHE 
GO
USE [fred]
GO
/****** Object:  Sequence [dbo].[CountByDecimal]    Script Date: 15/11/21 18:11:03 ******/
CREATE SEQUENCE [dbo].[CountByDecimal] 
 AS [decimal](18, 0)
 START WITH 593
 INCREMENT BY 82
 MINVALUE 5
 MAXVALUE 777777
 CACHE  10 
GO
USE [fred]
GO
/****** Object:  Sequence [dbo].[CountByNumeric]    Script Date: 15/11/21 18:11:03 ******/
CREATE SEQUENCE [dbo].[CountByNumeric] 
 AS [numeric](18, 0)
 START WITH 789
 INCREMENT BY 987
 MINVALUE 345
 MAXVALUE 999999999999999999
 NO CACHE 
GO
USE [fred]
GO
/****** Object:  Sequence [dbo].[CountBySmallInt]    Script Date: 15/11/21 18:11:03 ******/
CREATE SEQUENCE [dbo].[CountBySmallInt] 
 AS [smallint]
 START WITH 44
 INCREMENT BY 456
 MINVALUE -32768
 MAXVALUE 32767
 CYCLE 
 CACHE 
GO
USE [fred]
GO
/****** Object:  Sequence [dbo].[CountByTinyInt]    Script Date: 15/11/21 18:11:03 ******/
CREATE SEQUENCE [dbo].[CountByTinyInt] 
 AS [tinyint]
 START WITH 33
 INCREMENT BY 3
 MINVALUE 0
 MAXVALUE 255
 CACHE 
GO
/****** Object:  UserDefinedTableType [dbo].[CustomTableType]    Script Date: 15/11/21 18:11:03 ******/
CREATE TYPE [dbo].[CustomTableType] AS TABLE(
	[id] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  UserDefinedTableType [dbo].[UserDefinedTypeSample]    Script Date: 15/11/21 18:11:03 ******/
CREATE TYPE [dbo].[UserDefinedTypeSample] AS TABLE(
	[sensorid] [int] NULL,
	[utctimestamp] [datetime] NULL,
	[value] [float] NULL
)
GO
/****** Object:  Synonym [dbo].[alpha_workflow_synonym]    Script Date: 15/11/21 18:11:03 ******/
CREATE SYNONYM [dbo].[alpha_workflow_synonym] FOR [Alpha].[workflow]
GO
/****** Object:  Synonym [dbo].[cross_database_synonym]    Script Date: 15/11/21 18:11:03 ******/
CREATE SYNONYM [dbo].[cross_database_synonym] FOR [efdemo].[dbo].[engineer_company]
GO
/****** Object:  UserDefinedFunction [dbo].[CsvToInt]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  UserDefinedFunction [dbo].[fnWithCustomTableType]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fnWithCustomTableType] (@cusType dbo.CustomTableType READONLY)
RETURNS @result TABLE ([Something] NVARCHAR(64))
AS
BEGIN
    INSERT INTO @result
    SELECT 'Something'
    RETURN;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[udfNetSale]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[udfNetSale] (@quantity INT, @list_price DEC(10, 2), @discount DEC(4, 2))
RETURNS DEC(10, 2)
AS
BEGIN
    RETURN @quantity * @list_price * (1 - @discount);
END;
GO
/****** Object:  UserDefinedFunction [FFRS].[CsvToInt2]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [FFRS].[CsvToInt2]
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
/****** Object:  Table [Kate].[ScreamAndShout]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Kate].[ScreamAndShout](
	[KateID] [int] NOT NULL,
	[description] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[KateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[view.with.multiple.periods]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view.with.multiple.periods]
AS
	SELECT  KateID,
	        description FROM Kate.ScreamAndShout
GO
/****** Object:  View [dbo].[KatieView]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[KatieView]
AS
	SELECT  KateID,
	        description FROM Kate.ScreamAndShout
GO
/****** Object:  Table [dbo].[CodeObject]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CodeObject](
	[codeObjectNo] [int] NOT NULL,
	[applicationNo] [int] NULL,
	[type] [int] NOT NULL,
	[eName] [nvarchar](250) NOT NULL,
	[aName] [nvarchar](250) NULL,
	[description] [nvarchar](250) NULL,
	[codeName] [nvarchar](250) NULL,
	[note] [nvarchar](250) NULL,
	[isObject] [bit] NOT NULL,
	[versionNumber] [timestamp] NULL,
 CONSTRAINT [aaaaaObject_PK] PRIMARY KEY NONCLUSTERED 
(
	[codeObjectNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[AllColumnsNull]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AllColumnsNull]
AS
    SELECT  SUM(applicationNo) AS total,
            aName --ISNULL(aName, '') AS aName
    FROM    CodeObject
    GROUP BY aName
GO
/****** Object:  View [dbo].[ComplexView]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ComplexView]
AS
	with cteLicenses as (
		select sum(case when sp.type = 0 and sp.aName <> 'test'    then cast(sp.applicationNo as int) else 0 end) as AccessWare
			 , sum(case when sp.type = 4                           then cast(sp.applicationNo as int) else 0 end) as AdvInventory
			 , sum(                                                     cast(sp.applicationNo as int))            as Test
		  from CodeObject sp where sp.isObject = 0
	)
		SELECT  ISNULL(LicenseType, '') AS LicenseType, [Count]
		FROM    cteLicenses UNPIVOT ( [Count] FOR LicenseType IN (AccessWare, Test) ) unpvt;
GO
/****** Object:  View [dbo].[view with space]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[view with space]
AS
SELECT  codeObjectNo,
        applicationNo,
        type,
        eName,
        aName,
        description,
        codeName,
        note,
        isObject,
        versionNumber
FROM    [dbo].[CodeObject];
GO
/****** Object:  UserDefinedFunction [dbo].[182_test1]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- #183 Return Model for functions returning nullable
CREATE FUNCTION [dbo].[182_test1]
(
	@test INT
)
RETURNS TABLE AS RETURN
(
	SELECT name, ISNULL(principal_id, 0) AS principal_id
	FROM sys.objects
)
GO
/****** Object:  View [dcg].[rov_ColumnDefinitions]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dcg].[rov_ColumnDefinitions]
AS
	SELECT
		isc.*,
		o.TYPE
	FROM
		INFORMATION_SCHEMA.COLUMNS AS isc
		INNER JOIN sys.objects AS o
			ON SCHEMA_NAME(o.SCHEMA_ID) = isc.TABLE_SCHEMA
			   AND o.NAME = isc.TABLE_NAME
	WHERE
		NOT isc.TABLE_SCHEMA IN ('dcg');
GO
/****** Object:  Table [Alpha].[Harish3485]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Alpha].[Harish3485](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[harish_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Alpha].[Test]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Alpha].[Test](
	[Id] [int] NULL,
	[ExclusionTest] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Alpha].[workflow]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Alpha].[workflow](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [App].[UserFacilityServiceRole]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [App].[UserFacilityServiceRole](
	[userId] [int] NOT NULL,
	[appId] [int] NOT NULL,
	[fsrId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Beta].[Harish3485]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Beta].[Harish3485](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[another_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Beta].[Test]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Beta].[Test](
	[Id] [int] NULL,
	[ExclusionTest] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Beta].[ToAlpha]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Beta].[ToAlpha](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AlphaId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Beta].[workflow]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Beta].[workflow](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[A]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[A](
	[AId] [int] IDENTITY(1,1) NOT NULL,
	[C1] [int] NOT NULL,
	[C2] [int] NOT NULL,
 CONSTRAINT [PK_A] PRIMARY KEY CLUSTERED 
(
	[AId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AAREF]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AAREF](
	[C1] [int] NOT NULL,
	[C2] [int] NOT NULL,
	[CreatedUTC] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_AREF] PRIMARY KEY CLUSTERED 
(
	[C1] ASC,
	[C2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AB_OrderLinesAB_]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AB_OrderLinesAB_](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[sku] [varchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AB_OrdersAB_]    Script Date: 15/11/21 18:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AB_OrdersAB_](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[added] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppUser]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUser](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attendee]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendee](
	[AttendeeID] [bigint] NOT NULL,
	[Lastname] [nvarchar](50) NOT NULL,
	[Firstname] [nvarchar](50) NOT NULL,
	[PhoneCountryID] [int] NULL,
 CONSTRAINT [PK_Attendee] PRIMARY KEY CLUSTERED 
(
	[AttendeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BatchTest]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchTest](
	[code] [nvarchar](8) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BITFIDDLERALLCAPS]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BITFIDDLERALLCAPS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BitFiddlerCATEGORIES]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BitFiddlerCATEGORIES](
	[Id] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BitFiddlerCURRENCIES]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BitFiddlerCURRENCIES](
	[Id] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blah]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blah](
	[BlahID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Blah] PRIMARY KEY CLUSTERED 
(
	[BlahID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlahBlahLink]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlahBlahLink](
	[BlahID] [int] NOT NULL,
	[BlahID2] [int] NOT NULL,
 CONSTRAINT [PK_BlahBlahLink] PRIMARY KEY CLUSTERED 
(
	[BlahID] ASC,
	[BlahID2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlahBlahLink_readonly]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlahBlahLink_readonly](
	[BlahID] [int] NOT NULL,
	[BlahID2] [int] NOT NULL,
	[RowVersion] [timestamp] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id2]  AS ([id]+(100)),
 CONSTRAINT [PK_BlahBlahLink_ro] PRIMARY KEY CLUSTERED 
(
	[BlahID] ASC,
	[BlahID2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlahBlahLink_v2]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlahBlahLink_v2](
	[BlahID] [int] NOT NULL,
	[BlahID2] [int] NOT NULL,
	[dummy1] [int] NULL,
	[dummy2] [int] NOT NULL,
	[hello] [int] NOT NULL,
 CONSTRAINT [PK_BlahBlahLinkv2_ro] PRIMARY KEY CLUSTERED 
(
	[BlahID] ASC,
	[BlahID2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlahBlargLink]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlahBlargLink](
	[BlahID] [int] NOT NULL,
	[BlargID] [int] NOT NULL,
 CONSTRAINT [PK_BlahBlargLink] PRIMARY KEY CLUSTERED 
(
	[BlahID] ASC,
	[BlargID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blarg]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blarg](
	[BlargID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Blarg] PRIMARY KEY CLUSTERED 
(
	[BlargID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BringTheAction]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BringTheAction](
	[$] [int] NOT NULL,
	[%] [int] NULL,
	[£] [int] NULL,
	[&fred$] [int] NULL,
	[abc/\] [int] NULL,
	[joe.bloggs] [int] NULL,
	[simon-hughes] [int] NULL,
	[description] [varchar](20) NOT NULL,
	[someDate] [datetime2](7) NOT NULL,
	[Obs] [varchar](50) NULL,
	[Obs1] [varchar](50) NULL,
	[Obs2] [varchar](50) NULL,
	[Obs3] [varchar](50) NULL,
	[static] [int] NULL,
	[readonly] [int] NULL,
	[123Hi] [int] NULL,
	[areal] [real] NULL,
	[afloat] [float] NULL,
	[afloat8] [real] NULL,
	[afloat20] [real] NULL,
	[afloat24] [real] NULL,
	[afloat53] [float] NULL,
	[adecimal] [decimal](18, 0) NULL,
	[adecimal_19_4] [decimal](19, 4) NULL,
	[adecimal_10_3] [decimal](10, 3) NULL,
	[anumeric] [numeric](18, 0) NULL,
	[anumeric_5_2] [numeric](5, 2) NULL,
	[anumeric_11_3] [numeric](11, 3) NULL,
	[amoney] [money] NULL,
	[asmallmoney] [smallmoney] NULL,
	[brandon] [int] NULL,
	[GeographyType] [geography] NULL,
	[GeometryType] [geometry] NULL,
PRIMARY KEY CLUSTERED 
(
	[$] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Burak1]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Burak1](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[id_t] [bigint] NOT NULL,
	[num] [bigint] NOT NULL,
 CONSTRAINT [PK_Burak1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Burak2]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Burak2](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[num] [bigint] NOT NULL,
 CONSTRAINT [PK_Burak2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [U_Burak2] UNIQUE NONCLUSTERED 
(
	[id] ASC,
	[num] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CalculatedColumnNotNull]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CalculatedColumnNotNull](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [tinyint] NOT NULL,
	[IsCalendar]  AS (CONVERT([bit],case when [Type]>=(0) AND [Type]<=(7) then (1) else (0) end,0)) PERSISTED NOT NULL,
	[IsUtilization]  AS (CONVERT([bit],case when [Type]>=(8) AND [Type]<=(10) then (1) else (0) end,0)) PERSISTED NOT NULL,
 CONSTRAINT [PK_CalculatedColumnNotNull] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Car]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car](
	[Id] [int] NOT NULL,
	[PrimaryColourId] [int] NOT NULL,
	[CarMake] [varchar](255) NOT NULL,
	[computed_column]  AS ([PrimaryColourId]*(10)),
	[computed_column_persisted]  AS ([PrimaryColourId]*(10)) PERSISTED NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarToColour]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarToColour](
	[CarId] [int] NOT NULL,
	[ColourId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CarId] ASC,
	[ColourId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientCreationState]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientCreationState](
	[id] [uniqueidentifier] NOT NULL,
	[WebhookSetup] [bit] NOT NULL,
	[AuthSetup] [bit] NOT NULL,
	[AssignedCarrier] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CMS_File]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CMS_File](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](100) NOT NULL,
	[FileDescription] [varchar](500) NOT NULL,
	[FileIdentifier] [varchar](100) NOT NULL,
	[ValidStartDate] [datetime] NULL,
	[ValidEndDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_CMS_Form] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CMS_FileTag]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CMS_FileTag](
	[FileId] [int] NOT NULL,
	[TagId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CMS_Tag]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CMS_Tag](
	[TagId] [int] IDENTITY(1,1) NOT NULL,
	[TagName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_CMS_Tag] PRIMARY KEY CLUSTERED 
(
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CODE_MeetingTopicDetails]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CODE_MeetingTopicDetails](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_reuniao] [int] NOT NULL,
	[ord_trab] [int] NULL,
	[assunto] [nvarchar](250) NULL,
	[desenvolvimento] [nvarchar](max) NULL,
	[origem] [nvarchar](5) NULL,
	[id_origem] [int] NULL,
	[Estado] [int] NULL,
	[CompanyID] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateChanged] [datetime] NULL,
 CONSTRAINT [PK_CODE_MeetingTopicDetails] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CODE_PARAM_MeetingTopicDetailSource]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CODE_PARAM_MeetingTopicDetailSource](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](5) NOT NULL,
	[Label] [nvarchar](50) NULL,
	[LabelENG] [nvarchar](50) NULL,
	[LabelESP] [nvarchar](50) NULL,
	[LabelFRA] [nvarchar](50) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateChanged] [datetime] NULL,
 CONSTRAINT [PK_CODE_PARAM_MeetingTopicDetailSource] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_CODE_PARAM_MeetingTopicDetailSource] UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colour]    Script Date: 15/11/21 18:11:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colour](
	[Id] [int] NOT NULL,
	[Name] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[coID] [int] IDENTITY(1,1) NOT NULL,
	[cocogID] [int] NULL,
	[cocuID] [int] NULL,
	[coName] [nvarchar](255) NULL,
	[coShortName] [nvarchar](50) NULL,
	[coHtmlName] [nvarchar](1000) NULL,
	[coEmployerNo] [nvarchar](50) NULL,
	[coValidFrom] [datetime] NULL,
	[coValidTo] [datetime] NULL,
	[coVatIdNo] [nvarchar](50) NULL,
	[coURL] [nvarchar](255) NULL,
	[coActive] [bit] NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[coID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyGroup]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyGroup](
	[cogID] [int] IDENTITY(1,1) NOT NULL,
	[cogCompanyGroupName] [nvarchar](50) NULL,
	[cogValidFrom] [datetime] NULL,
	[cogValidTo] [datetime] NULL,
 CONSTRAINT [PK_CompanyGroup] PRIMARY KEY CLUSTERED 
(
	[cogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](12) NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DateTimeDefaultTest]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DateTimeDefaultTest](
	[Id] [int] NOT NULL,
	[CreatedDate] [datetimeoffset](7) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DefaultCheckForNull]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DefaultCheckForNull](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DescUppercase] [varchar](5) NULL,
	[DescLowercase] [varchar](5) NULL,
	[DescMixedCase] [varchar](5) NULL,
	[DescBrackets] [varchar](5) NULL,
	[X1] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DSOpe]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DSOpe](
	[ID] [int] NOT NULL,
	[decimal_default] [decimal](15, 2) NOT NULL,
	[MyGuid] [uniqueidentifier] NOT NULL,
	[default] [varchar](10) NULL,
	[MyGuidBadDefault] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EnumWithDefaultValue]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EnumWithDefaultValue](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SomeEnum] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventProcessor]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventProcessor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Description] [varchar](512) NULL,
	[EndpointAddress] [varchar](512) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_EventProcessor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventProcessorEventFilter]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventProcessorEventFilter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventProcessorId] [int] NOT NULL,
	[WantedEventId] [int] NOT NULL,
 CONSTRAINT [PK_EventProcessorEventFilter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FinancialInstitutionOffice]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinancialInstitutionOffice](
	[Code] [uniqueidentifier] NOT NULL,
	[FinancialInstitutionCode] [uniqueidentifier] NOT NULL,
	[OfficeName] [nvarchar](200) NULL,
 CONSTRAINT [UniqueOfficeName_FinancialInstitutionOffice] UNIQUE NONCLUSTERED 
(
	[FinancialInstitutionCode] ASC,
	[OfficeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[footer]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[footer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[otherID] [int] NOT NULL,
	[added] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ForeignKeyIsNotEnforced]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ForeignKeyIsNotEnforced](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[null_value] [int] NULL,
	[not_null_value] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_ForeignKeyIsNotEnforced_not_null_value] UNIQUE NONCLUSTERED 
(
	[not_null_value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_ForeignKeyIsNotEnforced_null_value] UNIQUE NONCLUSTERED 
(
	[null_value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ForeignKeyIsNotEnforcedItem]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ForeignKeyIsNotEnforcedItem](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[null_value] [int] NULL,
	[not_null_value] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_ForeignKeyIsNotEnforcedItem_not_null_value] UNIQUE NONCLUSTERED 
(
	[not_null_value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_ForeignKeyIsNotEnforcedItem_null_value] UNIQUE NONCLUSTERED 
(
	[null_value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HasPrincipalKeyTestChild]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HasPrincipalKeyTestChild](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[A] [int] NOT NULL,
	[B] [int] NOT NULL,
	[C] [int] NULL,
	[D] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HasPrincipalKeyTestParent]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HasPrincipalKeyTestParent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[A] [int] NOT NULL,
	[B] [int] NOT NULL,
	[C] [int] NULL,
	[D] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_HasPrincipalKeyTestParent_AB] UNIQUE NONCLUSTERED 
(
	[A] ASC,
	[B] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_HasPrincipalKeyTestParent_AC] UNIQUE NONCLUSTERED 
(
	[A] ASC,
	[C] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_HasPrincipalKeyTestParent_CD] UNIQUE NONCLUSTERED 
(
	[C] ASC,
	[D] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[header]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[header](
	[ID] [int] NOT NULL,
	[anotherID] [int] NOT NULL,
	[added] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[anotherID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[hierarchy_test]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hierarchy_test](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[hid] [hierarchyid] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MultipleKeys]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MultipleKeys](
	[UserId] [int] NOT NULL,
	[FavouriteColourId] [int] NOT NULL,
	[BestHolidayTypeId] [int] NOT NULL,
	[BankId] [int] NOT NULL,
	[CarId] [int] NOT NULL,
 CONSTRAINT [PK_MultipleKeys] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[FavouriteColourId] ASC,
	[BestHolidayTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC_MultipleKeys_FavouriteColour] UNIQUE NONCLUSTERED 
(
	[FavouriteColourId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NFLX]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NFLX](
	[Date] [varchar](50) NULL,
	[Open] [varchar](50) NULL,
	[High] [varchar](50) NULL,
	[Low] [varchar](50) NULL,
	[Close] [varchar](50) NULL,
	[Adj Close] [varchar](50) NULL,
	[Volume] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoPrimaryKeys]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoPrimaryKeys](
	[Id] [int] NULL,
	[Description] [varchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Period.Table]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Period.Table](
	[id] [int] NOT NULL,
	[joe.bloggs] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PeriodTestTable]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PeriodTestTable](
	[id] [int] NOT NULL,
	[joe.bloggs] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonPosts]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonPosts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](20) NOT NULL,
	[Body] [varchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pk_ordinal_test]    Script Date: 15/11/21 18:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pk_ordinal_test](
	[C1] [int] NOT NULL,
	[C2] [int] NOT NULL,
	[C3] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[C3] ASC,
	[C1] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PPH]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PPH](
	[Date] [varchar](50) NULL,
	[Open] [varchar](50) NULL,
	[High] [varchar](50) NULL,
	[Low] [varchar](50) NULL,
	[Close] [varchar](50) NULL,
	[Adj Close] [varchar](50) NULL,
	[Volume] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PropertyTypesToAdd]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyTypesToAdd](
	[id] [int] NOT NULL,
	[dt_default] [datetime2](7) NULL,
	[dt7] [datetime2](7) NULL,
	[defaultCheck] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RebelGalaxyBroadsides]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RebelGalaxyBroadsides](
	[WeaponName] [varchar](30) NULL,
	[Damage] [int] NOT NULL,
	[Cost] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RebelGalaxyShips]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RebelGalaxyShips](
	[ShipName] [varchar](30) NULL,
	[Broadsides] [int] NOT NULL,
	[Turrets] [int] NOT NULL,
	[Cost] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RebelGalaxyWeapons]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RebelGalaxyWeapons](
	[WeaponName] [varchar](30) NULL,
	[Damage] [int] NOT NULL,
	[Cost] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScreamAndShout]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScreamAndShout](
	[id] [int] NOT NULL,
	[KoeffVed] [decimal](4, 4) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SequenceTest]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SequenceTest](
	[Id] [int] NOT NULL,
	[CntByBigInt] [bigint] NOT NULL,
	[CntByTinyInt] [tinyint] NOT NULL,
	[CntBySmallInt] [smallint] NOT NULL,
	[CntByDecimal] [decimal](18, 0) NOT NULL,
	[CntByNumeric] [numeric](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockPrediction]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockPrediction](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[date] [datetime] NOT NULL,
	[open] [decimal](10, 4) NOT NULL,
	[high] [decimal](10, 4) NOT NULL,
	[low] [decimal](10, 4) NOT NULL,
	[close] [decimal](10, 4) NOT NULL,
	[adj_close] [decimal](10, 4) NOT NULL,
	[volume] [int] NOT NULL,
	[buy] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[table mapping with space]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[table mapping with space](
	[id] [int] NOT NULL,
	[id value] [int] NOT NULL,
 CONSTRAINT [map_with_space] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[id value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[table with duplicate column names]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[table with duplicate column names](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[User Id] [int] NOT NULL,
	[User  Id] [int] NOT NULL,
	[user__id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[table with space]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[table with space](
	[id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[table with space and in columns]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[table with space and in columns](
	[id value] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[table.with.multiple.periods]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[table.with.multiple.periods](
	[id] [int] NOT NULL,
	[description] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TableA]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TableA](
	[TableAId] [int] IDENTITY(1,1) NOT NULL,
	[TableADesc] [varchar](20) NULL,
 CONSTRAINT [TableA_pkey] PRIMARY KEY CLUSTERED 
(
	[TableAId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TableB]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TableB](
	[TableBId] [int] IDENTITY(1,1) NOT NULL,
	[TableAId] [int] NOT NULL,
	[ParentTableAId] [int] NULL,
	[TableBDesc] [varchar](20) NULL,
 CONSTRAINT [TableB_pkey] PRIMARY KEY CLUSTERED 
(
	[TableBId] ASC,
	[TableAId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TableWithSpaceInColumnOnly]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TableWithSpaceInColumnOnly](
	[id value] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TadeuszSobol]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TadeuszSobol](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
	[Name] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[TaskId] [bigint] NOT NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOrderErrors]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrderErrors](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[error] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOrderErrorsAB_]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrderErrorsAB_](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[error] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOrderLines]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrderLines](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[sku] [varchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOrders]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[added] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Test]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
	[Id] [int] NULL,
	[ExclusionTest] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatedById] [bigint] NOT NULL,
	[ModifiedById] [bigint] NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Token]    Script Date: 15/11/21 18:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Token](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Enabled] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ExternalUserID] [varchar](50) NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Document]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Document](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
 CONSTRAINT [PK_User_Document] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User309]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User309](
	[UserID] [bigint] NOT NULL,
	[Lastname] [nvarchar](100) NOT NULL,
	[Firstname] [nvarchar](100) NOT NULL,
	[PhoneCountryID] [int] NULL,
 CONSTRAINT [PK_User309] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Бренды товара]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Бренды товара](
	[Код бренда] [int] IDENTITY(1,1) NOT NULL,
	[Наименование бренда] [varchar](50) NOT NULL,
	[Логотип_бренда] [image] NULL,
	[Логотип_бренда_вертикальный] [image] NULL,
 CONSTRAINT [PK_Бренды] PRIMARY KEY CLUSTERED 
(
	[Код бренда] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [FFRS].[CV]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [FFRS].[CV](
	[BatchUID] [uniqueidentifier] NOT NULL,
	[CVID] [int] NOT NULL,
	[CVName] [nvarchar](200) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Issue47].[Role]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Issue47].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Issue47].[UserRoles]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Issue47].[UserRoles](
	[UserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Issue47].[Users]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Issue47].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Omega].[Test]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Omega].[Test](
	[Id] [int] NULL,
	[ExclusionTest] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [OneEightSix].[Issue]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [OneEightSix].[Issue](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[ConsentDocumentId] [int] NULL,
 CONSTRAINT [PK_Issue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [OneEightSix].[IssueUploadedFile]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [OneEightSix].[IssueUploadedFile](
	[UploadedFileId] [int] NOT NULL,
	[IssueId] [int] NOT NULL,
 CONSTRAINT [PK_IssueUploadedFile] PRIMARY KEY CLUSTERED 
(
	[UploadedFileId] ASC,
	[IssueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [OneEightSix].[UploadedFile]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [OneEightSix].[UploadedFile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullPath] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_UploadedFile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_EventProcessorEventFilter]    Script Date: 15/11/21 18:11:07 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_EventProcessorEventFilter] ON [dbo].[EventProcessorEventFilter]
(
	[EventProcessorId] ASC,
	[WantedEventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MultipleKeys_BestHolidayType]    Script Date: 15/11/21 18:11:07 ******/
CREATE NONCLUSTERED INDEX [IX_MultipleKeys_BestHolidayType] ON [dbo].[MultipleKeys]
(
	[BestHolidayTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MultipleKeys_Holiday_Bank]    Script Date: 15/11/21 18:11:07 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_MultipleKeys_Holiday_Bank] ON [dbo].[MultipleKeys]
(
	[BestHolidayTypeId] ASC,
	[BankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [fki_ParentTableA_FK_Constraint]    Script Date: 15/11/21 18:11:07 ******/
CREATE NONCLUSTERED INDEX [fki_ParentTableA_FK_Constraint] ON [dbo].[TableB]
(
	[TableAId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AB_OrdersAB_] ADD  DEFAULT (getdate()) FOR [added]
GO
ALTER TABLE [dbo].[BringTheAction] ADD  DEFAULT (space((0))) FOR [description]
GO
ALTER TABLE [dbo].[BringTheAction] ADD  DEFAULT (getdate()) FOR [someDate]
GO
ALTER TABLE [dbo].[BringTheAction] ADD  CONSTRAINT [DF__PlanStudies_Obs]  DEFAULT ('[{"k":"en","v":""},{"k":"pt","v":""}]') FOR [Obs]
GO
ALTER TABLE [dbo].[BringTheAction] ADD  CONSTRAINT [DF__PlanStudies_Obs1]  DEFAULT ('\') FOR [Obs1]
GO
ALTER TABLE [dbo].[BringTheAction] ADD  CONSTRAINT [DF__PlanStudies_Obs2]  DEFAULT ('\\') FOR [Obs2]
GO
ALTER TABLE [dbo].[BringTheAction] ADD  CONSTRAINT [DF__PlanStudies_Obs3]  DEFAULT ('\\\') FOR [Obs3]
GO
ALTER TABLE [dbo].[BringTheAction] ADD  DEFAULT ((1.23)) FOR [areal]
GO
ALTER TABLE [dbo].[BringTheAction] ADD  DEFAULT ((999.)) FOR [afloat]
GO
ALTER TABLE [dbo].[BringTheAction] ADD  DEFAULT (CONVERT([geography],'POINT (0 0)',0)) FOR [GeographyType]
GO
ALTER TABLE [dbo].[BringTheAction] ADD  DEFAULT ([GEOMETRY]::STGeomFromText('LINESTRING (100 100, 20 180, 180 180)',(0))) FOR [GeometryType]
GO
ALTER TABLE [dbo].[CodeObject] ADD  CONSTRAINT [DF__Object__objectNo__7E6CC920]  DEFAULT ((0)) FOR [codeObjectNo]
GO
ALTER TABLE [dbo].[CodeObject] ADD  CONSTRAINT [DF__Object__isObject__7F60ED59]  DEFAULT ((0)) FOR [isObject]
GO
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DFCompanyCoActive]  DEFAULT ((0)) FOR [coActive]
GO
ALTER TABLE [dbo].[DateTimeDefaultTest] ADD  CONSTRAINT [DF_DateTimeDefaultTest_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[DefaultCheckForNull] ADD  DEFAULT (NULL) FOR [DescUppercase]
GO
ALTER TABLE [dbo].[DefaultCheckForNull] ADD  DEFAULT (NULL) FOR [DescLowercase]
GO
ALTER TABLE [dbo].[DefaultCheckForNull] ADD  DEFAULT (NULL) FOR [DescMixedCase]
GO
ALTER TABLE [dbo].[DefaultCheckForNull] ADD  DEFAULT (NULL) FOR [DescBrackets]
GO
ALTER TABLE [dbo].[DSOpe] ADD  CONSTRAINT [DF_DSOpe_MaxRabat]  DEFAULT ((99.99)) FOR [decimal_default]
GO
ALTER TABLE [dbo].[DSOpe] ADD  DEFAULT ('9B7E1F67-5A81-4277-BC7D-06A3262A5C70') FOR [MyGuid]
GO
ALTER TABLE [dbo].[DSOpe] ADD  CONSTRAINT [DF_MyGuidBadDefaul]  DEFAULT (NULL) FOR [MyGuidBadDefault]
GO
ALTER TABLE [dbo].[EnumWithDefaultValue] ADD  DEFAULT ((1)) FOR [SomeEnum]
GO
ALTER TABLE [dbo].[footer] ADD  DEFAULT (getdate()) FOR [added]
GO
ALTER TABLE [dbo].[header] ADD  DEFAULT (getdate()) FOR [added]
GO
ALTER TABLE [dbo].[ScreamAndShout] ADD  DEFAULT ((0.5)) FOR [KoeffVed]
GO
ALTER TABLE [dbo].[SequenceTest] ADD  DEFAULT (NEXT VALUE FOR [dbo].[CountBy1]) FOR [Id]
GO
ALTER TABLE [dbo].[SequenceTest] ADD  DEFAULT (NEXT VALUE FOR [dbo].[CountByBigInt]) FOR [CntByBigInt]
GO
ALTER TABLE [dbo].[SequenceTest] ADD  DEFAULT (NEXT VALUE FOR [dbo].[CountByTinyInt]) FOR [CntByTinyInt]
GO
ALTER TABLE [dbo].[SequenceTest] ADD  DEFAULT (NEXT VALUE FOR [dbo].[CountBySmallInt]) FOR [CntBySmallInt]
GO
ALTER TABLE [dbo].[SequenceTest] ADD  DEFAULT (NEXT VALUE FOR [dbo].[CountByDecimal]) FOR [CntByDecimal]
GO
ALTER TABLE [dbo].[SequenceTest] ADD  DEFAULT (NEXT VALUE FOR [dbo].[CountByNumeric]) FOR [CntByNumeric]
GO
ALTER TABLE [dbo].[tblOrders] ADD  DEFAULT (getdate()) FOR [added]
GO
ALTER TABLE [dbo].[Token] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Alpha].[Harish3485]  WITH CHECK ADD  CONSTRAINT [FK_Harish] FOREIGN KEY([harish_id])
REFERENCES [Kate].[ScreamAndShout] ([KateID])
GO
ALTER TABLE [Alpha].[Harish3485] CHECK CONSTRAINT [FK_Harish]
GO
ALTER TABLE [Beta].[Harish3485]  WITH CHECK ADD  CONSTRAINT [FK_Harish] FOREIGN KEY([another_id])
REFERENCES [dbo].[PropertyTypesToAdd] ([id])
GO
ALTER TABLE [Beta].[Harish3485] CHECK CONSTRAINT [FK_Harish]
GO
ALTER TABLE [Beta].[ToAlpha]  WITH CHECK ADD  CONSTRAINT [BetaToAlpha_AlphaWorkflow] FOREIGN KEY([AlphaId])
REFERENCES [Alpha].[workflow] ([Id])
GO
ALTER TABLE [Beta].[ToAlpha] CHECK CONSTRAINT [BetaToAlpha_AlphaWorkflow]
GO
ALTER TABLE [dbo].[A]  WITH CHECK ADD  CONSTRAINT [FK_A_A] FOREIGN KEY([C1], [C2])
REFERENCES [dbo].[AAREF] ([C1], [C2])
GO
ALTER TABLE [dbo].[A] CHECK CONSTRAINT [FK_A_A]
GO
ALTER TABLE [dbo].[AB_OrderLinesAB_]  WITH CHECK ADD  CONSTRAINT [AB_OrderLinesAB_FK] FOREIGN KEY([OrderID])
REFERENCES [dbo].[AB_OrdersAB_] ([ID])
GO
ALTER TABLE [dbo].[AB_OrderLinesAB_] CHECK CONSTRAINT [AB_OrderLinesAB_FK]
GO
ALTER TABLE [dbo].[Attendee]  WITH CHECK ADD  CONSTRAINT [FK_Attendee_PhoneCountry] FOREIGN KEY([PhoneCountryID])
REFERENCES [dbo].[Country] ([CountryID])
GO
ALTER TABLE [dbo].[Attendee] CHECK CONSTRAINT [FK_Attendee_PhoneCountry]
GO
ALTER TABLE [dbo].[BlahBlahLink]  WITH CHECK ADD  CONSTRAINT [FK_BlahBlahLink_Blah] FOREIGN KEY([BlahID])
REFERENCES [dbo].[Blah] ([BlahID])
GO
ALTER TABLE [dbo].[BlahBlahLink] CHECK CONSTRAINT [FK_BlahBlahLink_Blah]
GO
ALTER TABLE [dbo].[BlahBlahLink]  WITH CHECK ADD  CONSTRAINT [FK_BlahBlahLink_Blah2] FOREIGN KEY([BlahID2])
REFERENCES [dbo].[Blah] ([BlahID])
GO
ALTER TABLE [dbo].[BlahBlahLink] CHECK CONSTRAINT [FK_BlahBlahLink_Blah2]
GO
ALTER TABLE [dbo].[BlahBlahLink_readonly]  WITH CHECK ADD  CONSTRAINT [FK_BlahBlahLink_Blah_ro] FOREIGN KEY([BlahID])
REFERENCES [dbo].[Blah] ([BlahID])
GO
ALTER TABLE [dbo].[BlahBlahLink_readonly] CHECK CONSTRAINT [FK_BlahBlahLink_Blah_ro]
GO
ALTER TABLE [dbo].[BlahBlahLink_readonly]  WITH CHECK ADD  CONSTRAINT [FK_BlahBlahLink_Blah_ro2] FOREIGN KEY([BlahID2])
REFERENCES [dbo].[Blah] ([BlahID])
GO
ALTER TABLE [dbo].[BlahBlahLink_readonly] CHECK CONSTRAINT [FK_BlahBlahLink_Blah_ro2]
GO
ALTER TABLE [dbo].[BlahBlahLink_v2]  WITH CHECK ADD  CONSTRAINT [FK_BlahBlahLinkv2_Blah_ro] FOREIGN KEY([BlahID])
REFERENCES [dbo].[Blah] ([BlahID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BlahBlahLink_v2] CHECK CONSTRAINT [FK_BlahBlahLinkv2_Blah_ro]
GO
ALTER TABLE [dbo].[BlahBlahLink_v2]  WITH CHECK ADD  CONSTRAINT [FK_BlahBlahLinkv2_Blah_ro2] FOREIGN KEY([BlahID2])
REFERENCES [dbo].[Blah] ([BlahID])
GO
ALTER TABLE [dbo].[BlahBlahLink_v2] CHECK CONSTRAINT [FK_BlahBlahLinkv2_Blah_ro2]
GO
ALTER TABLE [dbo].[BlahBlargLink]  WITH CHECK ADD  CONSTRAINT [FK_BlahBlargLink_Blah] FOREIGN KEY([BlahID])
REFERENCES [dbo].[Blah] ([BlahID])
GO
ALTER TABLE [dbo].[BlahBlargLink] CHECK CONSTRAINT [FK_BlahBlargLink_Blah]
GO
ALTER TABLE [dbo].[BlahBlargLink]  WITH CHECK ADD  CONSTRAINT [FK_BlahBlargLink_Blarg] FOREIGN KEY([BlargID])
REFERENCES [dbo].[Blarg] ([BlargID])
GO
ALTER TABLE [dbo].[BlahBlargLink] CHECK CONSTRAINT [FK_BlahBlargLink_Blarg]
GO
ALTER TABLE [dbo].[Burak1]  WITH CHECK ADD  CONSTRAINT [FK_Burak_Test1] FOREIGN KEY([id_t], [num])
REFERENCES [dbo].[Burak2] ([id], [num])
GO
ALTER TABLE [dbo].[Burak1] CHECK CONSTRAINT [FK_Burak_Test1]
GO
ALTER TABLE [dbo].[Burak1]  WITH CHECK ADD  CONSTRAINT [FK_Burak_Test2] FOREIGN KEY([id], [num])
REFERENCES [dbo].[Burak2] ([id], [num])
GO
ALTER TABLE [dbo].[Burak1] CHECK CONSTRAINT [FK_Burak_Test2]
GO
ALTER TABLE [dbo].[Car]  WITH CHECK ADD  CONSTRAINT [CarPrimaryColourFK] FOREIGN KEY([PrimaryColourId])
REFERENCES [dbo].[Colour] ([Id])
GO
ALTER TABLE [dbo].[Car] CHECK CONSTRAINT [CarPrimaryColourFK]
GO
ALTER TABLE [dbo].[CarToColour]  WITH CHECK ADD  CONSTRAINT [CarToColour_CarId] FOREIGN KEY([CarId])
REFERENCES [dbo].[Car] ([Id])
GO
ALTER TABLE [dbo].[CarToColour] CHECK CONSTRAINT [CarToColour_CarId]
GO
ALTER TABLE [dbo].[CarToColour]  WITH CHECK ADD  CONSTRAINT [CarToColour_ColourId] FOREIGN KEY([ColourId])
REFERENCES [dbo].[Colour] ([Id])
GO
ALTER TABLE [dbo].[CarToColour] CHECK CONSTRAINT [CarToColour_ColourId]
GO
ALTER TABLE [dbo].[CMS_FileTag]  WITH NOCHECK ADD  CONSTRAINT [FK_CMS_FileTag_CMS_File] FOREIGN KEY([FileId])
REFERENCES [dbo].[CMS_File] ([FileId])
GO
ALTER TABLE [dbo].[CMS_FileTag] CHECK CONSTRAINT [FK_CMS_FileTag_CMS_File]
GO
ALTER TABLE [dbo].[CMS_FileTag]  WITH NOCHECK ADD  CONSTRAINT [FK_CMS_FileTag_CMS_Tag] FOREIGN KEY([TagId])
REFERENCES [dbo].[CMS_Tag] ([TagId])
GO
ALTER TABLE [dbo].[CMS_FileTag] CHECK CONSTRAINT [FK_CMS_FileTag_CMS_Tag]
GO
ALTER TABLE [dbo].[CODE_MeetingTopicDetails]  WITH CHECK ADD  CONSTRAINT [FK_CODE_MeetingTopicDetails_CODE_PARAM_MeetingTopicDetailSource] FOREIGN KEY([origem])
REFERENCES [dbo].[CODE_PARAM_MeetingTopicDetailSource] ([Code])
GO
ALTER TABLE [dbo].[CODE_MeetingTopicDetails] CHECK CONSTRAINT [FK_CODE_MeetingTopicDetails_CODE_PARAM_MeetingTopicDetailSource]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_CompanyGroup] FOREIGN KEY([cocogID])
REFERENCES [dbo].[CompanyGroup] ([cogID])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_CompanyGroup]
GO
ALTER TABLE [dbo].[EventProcessorEventFilter]  WITH CHECK ADD  CONSTRAINT [FK_EventProcessorEventFilter__EventProcessor] FOREIGN KEY([EventProcessorId])
REFERENCES [dbo].[EventProcessor] ([Id])
GO
ALTER TABLE [dbo].[EventProcessorEventFilter] CHECK CONSTRAINT [FK_EventProcessorEventFilter__EventProcessor]
GO
ALTER TABLE [dbo].[footer]  WITH CHECK ADD  CONSTRAINT [fooderFK] FOREIGN KEY([ID], [otherID])
REFERENCES [dbo].[header] ([ID], [anotherID])
GO
ALTER TABLE [dbo].[footer] CHECK CONSTRAINT [fooderFK]
GO
ALTER TABLE [dbo].[ForeignKeyIsNotEnforcedItem]  WITH NOCHECK ADD  CONSTRAINT [FK_ForeignKeyIsNotEnforcedItem_notnull_notnull] FOREIGN KEY([not_null_value])
REFERENCES [dbo].[ForeignKeyIsNotEnforced] ([not_null_value])
GO
ALTER TABLE [dbo].[ForeignKeyIsNotEnforcedItem] CHECK CONSTRAINT [FK_ForeignKeyIsNotEnforcedItem_notnull_notnull]
GO
ALTER TABLE [dbo].[ForeignKeyIsNotEnforcedItem]  WITH NOCHECK ADD  CONSTRAINT [FK_ForeignKeyIsNotEnforcedItem_notnull_null] FOREIGN KEY([not_null_value])
REFERENCES [dbo].[ForeignKeyIsNotEnforced] ([null_value])
GO
ALTER TABLE [dbo].[ForeignKeyIsNotEnforcedItem] CHECK CONSTRAINT [FK_ForeignKeyIsNotEnforcedItem_notnull_null]
GO
ALTER TABLE [dbo].[ForeignKeyIsNotEnforcedItem]  WITH NOCHECK ADD  CONSTRAINT [FK_ForeignKeyIsNotEnforcedItem_null_notnull] FOREIGN KEY([null_value])
REFERENCES [dbo].[ForeignKeyIsNotEnforced] ([not_null_value])
GO
ALTER TABLE [dbo].[ForeignKeyIsNotEnforcedItem] CHECK CONSTRAINT [FK_ForeignKeyIsNotEnforcedItem_null_notnull]
GO
ALTER TABLE [dbo].[ForeignKeyIsNotEnforcedItem]  WITH NOCHECK ADD  CONSTRAINT [FK_ForeignKeyIsNotEnforcedItem_null_null] FOREIGN KEY([null_value])
REFERENCES [dbo].[ForeignKeyIsNotEnforced] ([null_value])
GO
ALTER TABLE [dbo].[ForeignKeyIsNotEnforcedItem] CHECK CONSTRAINT [FK_ForeignKeyIsNotEnforcedItem_null_null]
GO
ALTER TABLE [dbo].[HasPrincipalKeyTestChild]  WITH CHECK ADD  CONSTRAINT [FK_HasPrincipalKey_AB] FOREIGN KEY([A], [B])
REFERENCES [dbo].[HasPrincipalKeyTestParent] ([A], [B])
GO
ALTER TABLE [dbo].[HasPrincipalKeyTestChild] CHECK CONSTRAINT [FK_HasPrincipalKey_AB]
GO
ALTER TABLE [dbo].[HasPrincipalKeyTestChild]  WITH CHECK ADD  CONSTRAINT [FK_HasPrincipalKey_AC] FOREIGN KEY([A], [C])
REFERENCES [dbo].[HasPrincipalKeyTestParent] ([A], [C])
GO
ALTER TABLE [dbo].[HasPrincipalKeyTestChild] CHECK CONSTRAINT [FK_HasPrincipalKey_AC]
GO
ALTER TABLE [dbo].[HasPrincipalKeyTestChild]  WITH CHECK ADD  CONSTRAINT [FK_HasPrincipalKey_CD] FOREIGN KEY([C], [D])
REFERENCES [dbo].[HasPrincipalKeyTestParent] ([C], [D])
GO
ALTER TABLE [dbo].[HasPrincipalKeyTestChild] CHECK CONSTRAINT [FK_HasPrincipalKey_CD]
GO
ALTER TABLE [dbo].[PersonPosts]  WITH CHECK ADD  CONSTRAINT [FK_PersonPosts_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[PersonPosts] CHECK CONSTRAINT [FK_PersonPosts_CreatedBy]
GO
ALTER TABLE [dbo].[PersonPosts]  WITH CHECK ADD  CONSTRAINT [FK_PersonPosts_UpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[PersonPosts] CHECK CONSTRAINT [FK_PersonPosts_UpdatedBy]
GO
ALTER TABLE [dbo].[table mapping with space]  WITH CHECK ADD  CONSTRAINT [space1FK] FOREIGN KEY([id])
REFERENCES [dbo].[table with space] ([id])
GO
ALTER TABLE [dbo].[table mapping with space] CHECK CONSTRAINT [space1FK]
GO
ALTER TABLE [dbo].[table mapping with space]  WITH CHECK ADD  CONSTRAINT [space2FK] FOREIGN KEY([id value])
REFERENCES [dbo].[table with space and in columns] ([id value])
GO
ALTER TABLE [dbo].[table mapping with space] CHECK CONSTRAINT [space2FK]
GO
ALTER TABLE [dbo].[TableB]  WITH CHECK ADD  CONSTRAINT [FK_TableA_CompositeKey_Req] FOREIGN KEY([TableAId])
REFERENCES [dbo].[TableA] ([TableAId])
GO
ALTER TABLE [dbo].[TableB] CHECK CONSTRAINT [FK_TableA_CompositeKey_Req]
GO
ALTER TABLE [dbo].[TableB]  WITH CHECK ADD  CONSTRAINT [ParentTableB_Hierarchy] FOREIGN KEY([TableAId], [TableBId])
REFERENCES [dbo].[TableB] ([TableBId], [TableAId])
GO
ALTER TABLE [dbo].[TableB] CHECK CONSTRAINT [ParentTableB_Hierarchy]
GO
ALTER TABLE [dbo].[tblOrderLines]  WITH CHECK ADD  CONSTRAINT [tblOrdersFK] FOREIGN KEY([OrderID])
REFERENCES [dbo].[tblOrders] ([ID])
GO
ALTER TABLE [dbo].[tblOrderLines] CHECK CONSTRAINT [tblOrdersFK]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_AppUser] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[AppUser] ([Id])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_AppUser]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_AppUser1] FOREIGN KEY([ModifiedById])
REFERENCES [dbo].[AppUser] ([Id])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_AppUser1]
GO
ALTER TABLE [dbo].[User_Document]  WITH CHECK ADD  CONSTRAINT [FK_User_Document_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User_Document] CHECK CONSTRAINT [FK_User_Document_User]
GO
ALTER TABLE [dbo].[User_Document]  WITH CHECK ADD  CONSTRAINT [FK_User_Document_User1] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[User_Document] CHECK CONSTRAINT [FK_User_Document_User1]
GO
ALTER TABLE [dbo].[User309]  WITH CHECK ADD  CONSTRAINT [FK_User309_PhoneCountry] FOREIGN KEY([PhoneCountryID])
REFERENCES [dbo].[Country] ([CountryID])
GO
ALTER TABLE [dbo].[User309] CHECK CONSTRAINT [FK_User309_PhoneCountry]
GO
ALTER TABLE [Issue47].[UserRoles]  WITH CHECK ADD  CONSTRAINT [Issue47_UserRoles_roleid] FOREIGN KEY([RoleId])
REFERENCES [Issue47].[Role] ([RoleId])
GO
ALTER TABLE [Issue47].[UserRoles] CHECK CONSTRAINT [Issue47_UserRoles_roleid]
GO
ALTER TABLE [Issue47].[UserRoles]  WITH CHECK ADD  CONSTRAINT [Issue47_UserRoles_userid] FOREIGN KEY([UserId])
REFERENCES [Issue47].[Users] ([UserId])
GO
ALTER TABLE [Issue47].[UserRoles] CHECK CONSTRAINT [Issue47_UserRoles_userid]
GO
ALTER TABLE [Kate].[ScreamAndShout]  WITH CHECK ADD  CONSTRAINT [KateFK] FOREIGN KEY([KateID])
REFERENCES [dbo].[ScreamAndShout] ([id])
GO
ALTER TABLE [Kate].[ScreamAndShout] CHECK CONSTRAINT [KateFK]
GO
ALTER TABLE [OneEightSix].[Issue]  WITH CHECK ADD  CONSTRAINT [FK_Issue_UploadedFileConsentDocument] FOREIGN KEY([ConsentDocumentId])
REFERENCES [OneEightSix].[UploadedFile] ([Id])
GO
ALTER TABLE [OneEightSix].[Issue] CHECK CONSTRAINT [FK_Issue_UploadedFileConsentDocument]
GO
ALTER TABLE [OneEightSix].[IssueUploadedFile]  WITH CHECK ADD  CONSTRAINT [FK_IssueUploadedFile_Issue] FOREIGN KEY([IssueId])
REFERENCES [OneEightSix].[Issue] ([Id])
GO
ALTER TABLE [OneEightSix].[IssueUploadedFile] CHECK CONSTRAINT [FK_IssueUploadedFile_Issue]
GO
ALTER TABLE [OneEightSix].[IssueUploadedFile]  WITH CHECK ADD  CONSTRAINT [FK_IssueUploadedFile_UploadedFile] FOREIGN KEY([UploadedFileId])
REFERENCES [OneEightSix].[UploadedFile] ([Id])
GO
ALTER TABLE [OneEightSix].[IssueUploadedFile] CHECK CONSTRAINT [FK_IssueUploadedFile_UploadedFile]
GO
/****** Object:  StoredProcedure [Alpha].[Overclock]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Alpha].[Overclock] (@Parameter DATETIME) AS BEGIN RETURN 0 END;
GO
/****** Object:  StoredProcedure [App].[usp_CMTUserFSRUpdate]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [App].[usp_CMTUserFSRUpdate]
    @userId INT,
    @fsrId INT,
    @ufsrId INT OUT
AS
SET NOCOUNT ON
DECLARE @appId INT
SET @appId = 2

INSERT  [UserFacilityServiceRole]
        SELECT  @userId, @appId, @fsrId

SELECT  @ufsrId = @@IDENTITY
GO
/****** Object:  StoredProcedure [Beta].[Overclock]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Beta].[Overclock] (@Parameter DATETIME) AS BEGIN RETURN 0 END;
GO
/****** Object:  StoredProcedure [dbo].[182_test2]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[182_test2]
	@Flag INT
AS
BEGIN
	SET NOCOUNT ON;

	IF @Flag = 1
	BEGIN
		SELECT name, type, type_desc
		FROM sys.objects
	END

	SELECT name, ISNULL(principal_id, 0) AS principal_id
	FROM sys.objects

	SELECT name, object_id
	FROM sys.objects
END
GO
/****** Object:  StoredProcedure [dbo].[AddTwoValues]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddTwoValues](@a INT, @b INT)
AS
BEGIN
	SET NOCOUNT ON;
	RETURN @a + @b
END
GO
/****** Object:  StoredProcedure [dbo].[AddTwoValuesWithResult]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddTwoValuesWithResult](@a INT, @b INT, @result INT OUTPUT, @result2 INT OUTPUT)
AS
BEGIN
	SET NOCOUNT ON;
	SET @result = @a + @b
	SET @result2 = @b - @a
END
GO
/****** Object:  StoredProcedure [dbo].[aSimpleExample]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[aSimpleExample]
AS
BEGIN
    SET NOCOUNT ON;

    declare @test table (
        id int,
        [stuff] varchar(50)
    )
    insert into @test (id, [stuff]) values (1, 'some stuff'), (2, 'more stuff')

    select 1 as id, 'even moar' as [stuff] into #test

    select * from @test
    select * from #test
	DROP TABLE #test
END
GO
/****** Object:  StoredProcedure [dbo].[ConvertToString]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConvertToString](@someValue INT, @someString VARCHAR(20) OUTPUT)
AS
BEGIN
	SET NOCOUNT ON;
	SET @someString = '*' + CAST(@someValue AS VARCHAR(20)) + '*'
END
GO
/****** Object:  StoredProcedure [dbo].[dbo_proc_data_from_ffrs]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[dbo_proc_data_from_ffrs](@maxId INT)
AS
SELECT BatchUID,
       CVID,
       CVName FROM FFRS.CV WHERE CVID < @maxId
GO
/****** Object:  StoredProcedure [dbo].[dbo_proc_data_from_ffrs_and_dbo]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[dbo_proc_data_from_ffrs_and_dbo]
AS
SELECT Id, PrimaryColourId, CarMake, CVName FROM Car JOIN FFRS.CV ON Id = CVID
GO
/****** Object:  StoredProcedure [dbo].[DeleteItemById]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteItemById]
    @BinaryId   INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Car WHERE Id = @BinaryId;
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteOldestBinary]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteOldestBinary]
    @UserId   INT = 0,
    @ItemType INT = 0
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @OldestBinaryId INT;
	SET @OldestBinaryId = 
	(
		SELECT TOP (1)  b.BinaryId
		FROM    dbo.Binaries b
		WHERE   b.UserId = @UserId
				AND b.ItemType = @ItemType
		ORDER BY b.CreatedUtc
	);

	EXEC dbo.DeleteItemById @BinaryId = @OldestBinaryId;    -- int
END;
GO
/****** Object:  StoredProcedure [dbo].[DSOpeProc]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- From George Lesser <george.lesser@harvesthcm.com>
CREATE PROCEDURE [dbo].[DSOpeProc]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT  ID,
            CONVERT(BIT, CASE WHEN [default] IS NOT NULL THEN 1 ELSE 0 END) AS Selected
    FROM    DSOpe;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetScreamAndShout]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetScreamAndShout](@maxId INT)
AS
BEGIN
	SET NOCOUNT ON;
	IF(@maxId IS NULL)
		SET @maxId = 999
	SELECT id, KoeffVed FROM ScreamAndShout WHERE id <= @maxId
END
GO
/****** Object:  StoredProcedure [dbo].[InsertRecord]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertRecord]
    @Data         VARCHAR(256)
  , @InsertedId   INT OUT
AS
    BEGIN
        INSERT INTO DataTable
        VALUES (@Data)

        SET @InsertedId = SCOPE_IDENTITY();
    END;
GO
/****** Object:  StoredProcedure [dbo].[InsertRecordThree]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertRecordThree]
    @Data VARCHAR(256),
    @InsertedId INT OUT,
	@SomeId INT,
    @AnotherInsertedId INT OUT
AS
BEGIN
    INSERT  INTO DataTable
    VALUES  (@Data)
    SET @InsertedId = SCOPE_IDENTITY();
    SET @AnotherInsertedId = @InsertedId + 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertRecordTwo]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertRecordTwo]
    @Data VARCHAR(256),
    @InsertedId INT OUT,
    @AnotherInsertedId INT OUT
AS
BEGIN
    INSERT  INTO DataTable
    VALUES  (@Data)
    SET @InsertedId = SCOPE_IDENTITY();
    SET @AnotherInsertedId = @InsertedId + 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[MinTripSequenceStart]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MinTripSequenceStart](@minTripSequenceStartParam DATETIME2 OUTPUT)
AS
BEGIN
	SET NOCOUNT ON;
	SET @minTripSequenceStartParam = GETDATE()
END
GO
/****** Object:  StoredProcedure [dbo].[MinTripSequenceStartNull]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MinTripSequenceStartNull](@minTripSequenceStartParam DATETIME2 OUTPUT)
AS
BEGIN
	SET NOCOUNT ON;
	SET @minTripSequenceStartParam = NULL
END
GO
/****** Object:  StoredProcedure [dbo].[NvarcharTest]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--- From WisdomGuidedByExperience
-- stored proc with nvarchar(max) parameter is missing size parameter
CREATE PROCEDURE [dbo].[NvarcharTest] @maxOutputParam NVARCHAR(MAX), @normalOutputParam NVARCHAR(20)
AS
BEGIN
    SET @maxOutputParam = 'hello'
    SET @normalOutputParam = 'world'
END
GO
/****** Object:  StoredProcedure [dbo].[proc_TestDecimalOutput]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_TestDecimalOutput]
    @PerfectNumber decimal(18,2) OUTPUT
AS
BEGIN
    SET @PerfectNumber = 10.35;
END
GO
/****** Object:  StoredProcedure [dbo].[proc_TestDecimalOutputV2]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_TestDecimalOutputV2]
    @PerfectNumber decimal(12,8) OUTPUT
AS
BEGIN
    SET @PerfectNumber = 10.35;
END
GO
/****** Object:  StoredProcedure [dbo].[proc_TestDecimalOutputV3Default]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_TestDecimalOutputV3Default]
    @PerfectNumber decimal OUTPUT
AS
BEGIN
    SET @PerfectNumber = 10.35;
END
GO
/****** Object:  StoredProcedure [dbo].[SpatialTypesNoParams]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SpatialTypesNoParams]
AS
    SELECT  [$] AS Dollar, someDate, GeographyType, GeometryType FROM BringTheAction;
GO
/****** Object:  StoredProcedure [dbo].[SpatialTypesWithParams]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SpatialTypesWithParams] (@geometry GEOMETRY, @geography GEOGRAPHY)
AS
    SELECT  [$] AS Dollar, GeographyType, GeometryType FROM BringTheAction;
GO
/****** Object:  StoredProcedure [dbo].[stp test space test]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[stp test space test]
(
 @a_val int,
 @b_val int
)
 AS
 
 SELECT [codeObjectNo] AS [code object no],
        [applicationNo] AS [application no]
 FROM   [dbo].[CodeObject];
GO
/****** Object:  StoredProcedure [dbo].[stp_multiple_identical_results]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stp_multiple_identical_results] (@someVar INT)
AS
	IF(@someVar > 5) BEGIN
		SELECT * FROM Colour;
	END ELSE BEGIN
		SELECT * FROM Colour;
	END
GO
/****** Object:  StoredProcedure [dbo].[stp_multiple_multiple_results_with_params]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stp_multiple_multiple_results_with_params] (@first_val INT, @second_val int NULL, @third_val INT)
AS
SELECT  [codeObjectNo],[applicationNo] FROM    [dbo].[CodeObject];
SELECT * FROM Colour;
SELECT * FROM Blah;
SELECT * FROM Company;
SELECT * FROM CompanyGroup;
SELECT * FROM Colour;
GO
/****** Object:  StoredProcedure [dbo].[stp_multiple_results]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stp_multiple_results]
AS
SELECT  [codeObjectNo],[applicationNo]
FROM    [dbo].[CodeObject];
SELECT * FROM CompanyGroup;
SELECT * FROM Colour;
GO
/****** Object:  StoredProcedure [dbo].[stp_multiple_results_with_params]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stp_multiple_results_with_params] (@first_val INT, @second_val int NULL)
AS
SELECT  [codeObjectNo],[applicationNo]
FROM    [dbo].[CodeObject];
SELECT * FROM Colour;
GO
/****** Object:  StoredProcedure [dbo].[stp_no_params_test]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stp_no_params_test]
AS
SELECT  [codeObjectNo],[applicationNo]
FROM    [dbo].[CodeObject];
GO
/****** Object:  StoredProcedure [dbo].[stp_no_return_fields]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stp_no_return_fields]
AS
UPDATE [dbo].[CodeObject]
SET type=4
GO
/****** Object:  StoredProcedure [dbo].[stp_nullable_params_test]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stp_nullable_params_test] (@a_val INT, @b_val int NULL)
AS
SELECT  [codeObjectNo],[applicationNo]
FROM    [dbo].[CodeObject];
GO
/****** Object:  StoredProcedure [dbo].[stp_test]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[stp_test]
(
 @strDateFROM NVARCHAR(20),
 @strDateTo NVARCHAR(20),
 @retBool BIT OUTPUT
)
 AS
 DECLARE @intError INT
 SET @intError = 0
 SET @retBool = 0

 SELECT [codeObjectNo],
        [applicationNo],
        [type],
        [eName],
        [aName],
        [description],
        [codeName],
        [note],
        [isObject],
        [versionNumber]
 FROM   [dbo].[CodeObject];
GO
/****** Object:  StoredProcedure [dbo].[stp_test_underscore_test]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 CREATE PROCEDURE [dbo].[stp_test_underscore_test]
(
 @str_Date_FROM NVARCHAR(20),
 @str_date_to NVARCHAR(20)
)
 AS
 
 SELECT [codeObjectNo] AS code_object_no,
        [applicationNo] AS application_no
 FROM   [dbo].[CodeObject];
GO
/****** Object:  StoredProcedure [dbo].[StupidStoredProcedureParams]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[StupidStoredProcedureParams](@ReqType varchar(25),@Dept smallint,@Class smallint,@Item SMALLINT)
AS
BEGIN
  RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[StupidStoredProcedureParams2]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[StupidStoredProcedureParams2](@override varchar(25),@readonly smallint,@class smallint,@enum SMALLINT)
AS
BEGIN
  RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[TestReturnString]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TestReturnString]
AS
BEGIN
    SET NOCOUNT ON
    SET XACT_ABORT ON
    DECLARE @error VARCHAR(100)
    SET @error = ''
    IF(1+1 = 2) BEGIN
        SET @error = 'test'
    END
    SELECT @error AS error
END
GO
/****** Object:  StoredProcedure [dbo].[UserDefinedTypeSampleStoredProc]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserDefinedTypeSampleStoredProc] (@a INT, @type UserDefinedTypeSample READONLY, @b INT)
AS
BEGIN
  -- todo
  RETURN (0)
END
GO
/****** Object:  StoredProcedure [dbo].[XmlDataV1]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- #173 ReturnModel is not generated for stored proc if xml.modify is used
CREATE procedure [dbo].[XmlDataV1]
as
	declare @temp table ([SomeXML] [xml])
	insert into @temp values('<root></root>')

	declare @someXml xml	
	select top 1 @someXml = [SomeXML] from @temp

	--explicit assignment. when this is line commented out, ReversePoco generates incorrect output type (int) for this stored proc
	set @someXml = '<root></root>'; 

	set @someXml.modify('insert <new>node</new> into (/root)[1]');

	SELECT getdate(), @someXml	
GO
/****** Object:  StoredProcedure [dbo].[XmlDataV2]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[XmlDataV2]
as
	declare @temp table ([SomeXML] [xml])
	insert into @temp values('<root></root>')

	declare @someXml xml	
	select top 1 @someXml = [SomeXML] from @temp

	--explicit assignment. when this is line commented out, ReversePoco generates incorrect output type (int) for this stored proc
	--set @someXml = '<root></root>'; 

	set @someXml.modify('insert <new>node</new> into (/root)[1]');

	SELECT getdate(), @someXml	
GO
/****** Object:  StoredProcedure [FFRS].[cv_data]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [FFRS].[cv_data](@maxId INT)
AS
SELECT BatchUID,
       CVID,
       CVName FROM FFRS.CV WHERE CVID < @maxId
GO
/****** Object:  StoredProcedure [FFRS].[data_from_dbo]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [FFRS].[data_from_dbo]
AS
SELECT Id, PrimaryColourId, CarMake FROM Car
GO
/****** Object:  StoredProcedure [FFRS].[data_from_dbo_and_ffrs]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [FFRS].[data_from_dbo_and_ffrs]
AS
SELECT Id, PrimaryColourId, CarMake, CVName FROM Car
JOIN FFRS.CV ON Id = CVID
GO
/****** Object:  StoredProcedure [Kate].[Hello]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [Kate].[Hello] AS
    SELECT  [static],[readonly] -- #279 Contains static and readonly, which are illegal in C#
    FROM    BringTheAction
GO
/****** Object:  StoredProcedure [Omega].[Overclock]    Script Date: 15/11/21 18:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Omega].[Overclock] (@Parameter DATETIME) AS BEGIN RETURN 0 END;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'This is to document the bring the action table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BringTheAction'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The Type is used to describe what type of CodeObject this is. See the enum BusinessLogic.CodeObjectType.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CodeObject', @level2type=N'COLUMN',@level2name=N'type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'this is a test' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CodeObject'
GO
USE [master]
GO
ALTER DATABASE [fred] SET  READ_WRITE 
GO
