USE [master]
GO
CREATE DATABASE [EfrpgTest_Synonyms]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EfrpgTest_Synonyms', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\EfrpgTest_Synonyms.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EfrpgTest_Synonyms_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\EfrpgTest_Synonyms_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EfrpgTest_Synonyms].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET ARITHABORT OFF 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET AUTO_SHRINK ON 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET  MULTI_USER 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'EfrpgTest_Synonyms', N'ON'
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET QUERY_STORE = OFF
GO
USE [EfrpgTest_Synonyms]
GO
CREATE SCHEMA [CustomSchema]
GO
CREATE SCHEMA [Stafford]
GO
CREATE SCHEMA [Synonyms]
GO
CREATE SYNONYM [CustomSchema].[CsvToIntWithSchema] FOR [EfrpgTest].[CustomSchema].[CsvToIntWithSchema]
GO
CREATE SYNONYM [dbo].[CarWithDifferentSynonymName] FOR [EfrpgTest].[dbo].[Car]
GO
CREATE SYNONYM [Synonyms].[Child] FOR [EfrpgTest].[Synonyms].[Child]
GO
CREATE SYNONYM [Synonyms].[Parent] FOR [EfrpgTest].[Synonyms].[Parent]
GO
CREATE SYNONYM [Synonyms].[SimpleStoredProc] FOR [EfrpgTest].[Synonyms].[SimpleStoredProc]
GO
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Forename] [varchar](20) NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfoAttributes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PrimaryId] [int] NOT NULL,
	[SecondaryId] [int] NOT NULL,
 CONSTRAINT [PK_UserInfoAttributes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserInfo] ON 
GO
INSERT [dbo].[UserInfo] ([Id], [Forename]) VALUES (1, N'Ruprecht')
GO
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
GO
ALTER TABLE [dbo].[UserInfoAttributes]  WITH CHECK ADD  CONSTRAINT [FK_UserInfoAttributes_PrimaryUserInfo] FOREIGN KEY([PrimaryId])
REFERENCES [dbo].[UserInfo] ([Id])
GO
ALTER TABLE [dbo].[UserInfoAttributes] CHECK CONSTRAINT [FK_UserInfoAttributes_PrimaryUserInfo]
GO
ALTER TABLE [dbo].[UserInfoAttributes]  WITH CHECK ADD  CONSTRAINT [FK_UserInfoAttributes_SecondaryUserInfo] FOREIGN KEY([SecondaryId])
REFERENCES [dbo].[UserInfo] ([Id])
GO
ALTER TABLE [dbo].[UserInfoAttributes] CHECK CONSTRAINT [FK_UserInfoAttributes_SecondaryUserInfo]
GO
USE [master]
GO
ALTER DATABASE [EfrpgTest_Synonyms] SET  READ_WRITE 
GO
