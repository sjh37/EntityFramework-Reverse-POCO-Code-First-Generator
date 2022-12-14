-- This database will contain all the horrible edge cases this generator has to cope with
/*
CREATE DATABASE EfrpgTest CONTAINMENT = NONE
    ON PRIMARY
           (
               NAME = N'EfrpgTest',
               FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\EfrpgTest.mdf',
               SIZE = 8192KB,
               MAXSIZE = UNLIMITED,
               FILEGROWTH = 10%
           ),
       FILEGROUP EfrpgTest_mod CONTAINS MEMORY_OPTIMIZED_DATA DEFAULT
           (
               NAME = N'EfrpgTest_mod',
               FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\EfrpgTest_mod',
               MAXSIZE = UNLIMITED
           )
    LOG ON
        (
            NAME = N'EfrpgTest_log',
            FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\EfrpgTest.ldf',
            SIZE = 2816KB,
            FILEGROWTH = 10%
        );
GO

ALTER DATABASE [EfrpgTest] SET COMPATIBILITY_LEVEL = 140
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
BEGIN
    EXEC EfrpgTest.dbo.sp_fulltext_database @action = 'enable';
END;
GO

ALTER DATABASE [EfrpgTest] SET ANSI_NULL_DEFAULT OFF 
ALTER DATABASE [EfrpgTest] SET ANSI_NULLS OFF 
ALTER DATABASE [EfrpgTest] SET ANSI_PADDING ON 
ALTER DATABASE [EfrpgTest] SET ANSI_WARNINGS OFF 
ALTER DATABASE [EfrpgTest] SET ARITHABORT OFF 
ALTER DATABASE [EfrpgTest] SET AUTO_CLOSE OFF 
ALTER DATABASE [EfrpgTest] SET AUTO_SHRINK ON 
ALTER DATABASE [EfrpgTest] SET AUTO_UPDATE_STATISTICS ON 
ALTER DATABASE [EfrpgTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
ALTER DATABASE [EfrpgTest] SET CURSOR_DEFAULT  GLOBAL 
ALTER DATABASE [EfrpgTest] SET CONCAT_NULL_YIELDS_NULL OFF 
ALTER DATABASE [EfrpgTest] SET NUMERIC_ROUNDABORT OFF 
ALTER DATABASE [EfrpgTest] SET QUOTED_IDENTIFIER OFF 
ALTER DATABASE [EfrpgTest] SET RECURSIVE_TRIGGERS OFF 
ALTER DATABASE [EfrpgTest] SET  DISABLE_BROKER 
ALTER DATABASE [EfrpgTest] SET AUTO_UPDATE_STATISTICS_ASYNC ON 
ALTER DATABASE [EfrpgTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
ALTER DATABASE [EfrpgTest] SET TRUSTWORTHY OFF 
ALTER DATABASE [EfrpgTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
ALTER DATABASE [EfrpgTest] SET PARAMETERIZATION SIMPLE 
ALTER DATABASE [EfrpgTest] SET READ_COMMITTED_SNAPSHOT OFF 
ALTER DATABASE [EfrpgTest] SET HONOR_BROKER_PRIORITY OFF 
ALTER DATABASE [EfrpgTest] SET RECOVERY SIMPLE 
ALTER DATABASE [EfrpgTest] SET  MULTI_USER 
ALTER DATABASE [EfrpgTest] SET PAGE_VERIFY CHECKSUM  
ALTER DATABASE [EfrpgTest] SET DB_CHAINING OFF 
ALTER DATABASE [EfrpgTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
ALTER DATABASE [EfrpgTest] SET TARGET_RECOVERY_TIME = 60 SECONDS 
ALTER DATABASE [EfrpgTest] SET DELAYED_DURABILITY = DISABLED 
ALTER DATABASE [EfrpgTest] SET QUERY_STORE = OFF
ALTER DATABASE [EfrpgTest] SET READ_WRITE 
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
ALTER DATABASE [EfrpgTest_Synonyms] SET DISABLE_BROKER
ALTER DATABASE [EfrpgTest_Synonyms] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
ALTER DATABASE [EfrpgTest_Synonyms] SET DATE_CORRELATION_OPTIMIZATION OFF
ALTER DATABASE [EfrpgTest_Synonyms] SET PARAMETERIZATION SIMPLE
ALTER DATABASE [EfrpgTest_Synonyms] SET READ_WRITE
ALTER DATABASE [EfrpgTest_Synonyms] SET RECOVERY SIMPLE
ALTER DATABASE [EfrpgTest_Synonyms] SET MULTI_USER
ALTER DATABASE [EfrpgTest_Synonyms] SET PAGE_VERIFY CHECKSUM
GO
USE [EfrpgTest_Synonyms]
GO
IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE is_default=1 AND name = N'PRIMARY')
	ALTER DATABASE [EfrpgTest_Synonyms] MODIFY FILEGROUP [PRIMARY] DEFAULT
GO
*/

/*
CREATE DATABASE [EfrpgTest_Settings] ON PRIMARY
       ( NAME = N'EfrpgTest_Settings',     FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\EfrpgTest_Settings.mdf' , SIZE = 5Mb , FILEGROWTH = 1024KB )
LOG ON ( NAME = N'EfrpgTest_Settings_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\EfrpgTest_Settings.ldf' , SIZE = 1024KB , FILEGROWTH = 10%);
GO
ALTER DATABASE [EfrpgTest_Settings] SET COMPATIBILITY_LEVEL = 100
ALTER DATABASE [EfrpgTest_Settings] SET ANSI_NULL_DEFAULT OFF
ALTER DATABASE [EfrpgTest_Settings] SET ANSI_NULLS OFF
ALTER DATABASE [EfrpgTest_Settings] SET ANSI_PADDING OFF
ALTER DATABASE [EfrpgTest_Settings] SET ANSI_WARNINGS OFF
ALTER DATABASE [EfrpgTest_Settings] SET ARITHABORT OFF
ALTER DATABASE [EfrpgTest_Settings] SET AUTO_CLOSE OFF
ALTER DATABASE [EfrpgTest_Settings] SET AUTO_CREATE_STATISTICS ON
ALTER DATABASE [EfrpgTest_Settings] SET AUTO_SHRINK OFF
ALTER DATABASE [EfrpgTest_Settings] SET AUTO_UPDATE_STATISTICS ON
ALTER DATABASE [EfrpgTest_Settings] SET CURSOR_CLOSE_ON_COMMIT OFF
ALTER DATABASE [EfrpgTest_Settings] SET CURSOR_DEFAULT  GLOBAL
ALTER DATABASE [EfrpgTest_Settings] SET CONCAT_NULL_YIELDS_NULL OFF
ALTER DATABASE [EfrpgTest_Settings] SET NUMERIC_ROUNDABORT OFF
ALTER DATABASE [EfrpgTest_Settings] SET QUOTED_IDENTIFIER OFF
ALTER DATABASE [EfrpgTest_Settings] SET RECURSIVE_TRIGGERS OFF
ALTER DATABASE [EfrpgTest_Settings] SET DISABLE_BROKER
ALTER DATABASE [EfrpgTest_Settings] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
ALTER DATABASE [EfrpgTest_Settings] SET DATE_CORRELATION_OPTIMIZATION OFF
ALTER DATABASE [EfrpgTest_Settings] SET PARAMETERIZATION SIMPLE
ALTER DATABASE [EfrpgTest_Settings] SET READ_WRITE
ALTER DATABASE [EfrpgTest_Settings] SET RECOVERY SIMPLE
ALTER DATABASE [EfrpgTest_Settings] SET MULTI_USER
ALTER DATABASE [EfrpgTest_Settings] SET PAGE_VERIFY CHECKSUM
GO
USE [EfrpgTest_Settings]
GO
IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE is_default=1 AND name = N'PRIMARY')
	ALTER DATABASE [EfrpgTest_Settings] MODIFY FILEGROUP [PRIMARY] DEFAULT
GO
*/

USE [EfrpgTest_Settings]
GO

-- This will create the tables necessary for multi-context generation
IF SCHEMA_ID(N'MultiContext') IS NULL
    EXEC (N'CREATE SCHEMA [MultiContext];');
GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

-- You can add extra fields to this table. All columns will be read in and stored in a Dictionary<string,object>() for you to access and process.
CREATE TABLE MultiContext.Context
(
    Id           INT           NOT NULL IDENTITY(1, 1),
    Name         NVARCHAR(255) NOT NULL,
    Namespace    NVARCHAR(128) NULL,
    Description  NVARCHAR(255) NULL,
    BaseSchema   NVARCHAR(255) NULL,    -- Default to use if not specified for an object
    TemplatePath NVARCHAR(500) NULL,
    Filename     NVARCHAR(128) NULL,    -- If Filename == NULL, then use Name, else use Filename as the name of the file
    CONSTRAINT PK_Context PRIMARY KEY CLUSTERED (Id)
);
GO

/* Create enumeration from database table
public enum Name
{
    NameField = ValueField,
    etc
} */
-- You can add extra fields to this table. All columns will be read in and stored in a Dictionary<string,object>() for you to access and process.
CREATE TABLE MultiContext.Enumeration
(
    Id         INT           NOT NULL IDENTITY(1, 1),
    Name       NVARCHAR(255) NOT NULL,  -- Enum to generate. e.g. "DaysOfWeek" would result in "public enum DaysOfWeek {...}"
    [Table]    NVARCHAR(255) NOT NULL,  -- Database table containing enum values. e.g. "DaysOfWeek"
    NameField  NVARCHAR(255) NOT NULL,  -- Column containing the name for the enum. e.g. "TypeName"
    ValueField NVARCHAR(255) NOT NULL,  -- Column containing the values for the enum. e.g. "TypeId"
    ContextId  INT           NOT NULL,
    CONSTRAINT PK_Enumeration PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_Enumeration_Context_ContextId FOREIGN KEY (ContextId) REFERENCES MultiContext.Context (Id) ON DELETE NO ACTION
);
GO

-- You can add extra fields to this table. All columns will be read in and stored in a Dictionary<string,object>() for you to access and process.
CREATE TABLE MultiContext.[Function]
(
    Id        INT           NOT NULL IDENTITY(1, 1),
    Name      NVARCHAR(255) NOT NULL,
    DbName    NVARCHAR(255) NULL,   -- [optional] Name of function in database. Specify only if the db function name is different from the "Name" property.
    ContextId INT           NOT NULL,
    CONSTRAINT PK_Function PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_Function_Context_ContextId FOREIGN KEY (ContextId) REFERENCES MultiContext.Context (Id) ON DELETE NO ACTION
);
GO

-- You can add extra fields to this table. All columns will be read in and stored in a Dictionary<string,object>() for you to access and process.
CREATE TABLE MultiContext.StoredProcedure
(
    Id          INT           NOT NULL IDENTITY(1, 1),
    Name        NVARCHAR(255) NOT NULL,
    DbName      NVARCHAR(255) NULL, -- [optional] Name of stored proc in database. Specify only if the db stored proc name is different from the "Name" property.
    ReturnModel NVARCHAR(255) NULL, -- [optional] Specify a return model for stored proc
    ContextId   INT           NOT NULL,
    CONSTRAINT PK_StoredProcedure PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_StoredProcedure_Context_ContextId FOREIGN KEY (ContextId) REFERENCES MultiContext.Context (Id) ON DELETE NO ACTION
);
GO

-- You can add extra fields to this table. All columns will be read in and stored in a Dictionary<string,object>() for you to access and process.
CREATE TABLE MultiContext.[Table]
(
    Id            INT           NOT NULL IDENTITY(1, 1),
    Name          NVARCHAR(255) NOT NULL,
    Description   NVARCHAR(255) NULL,   -- [optional] Comment added to table class
    PluralName    NVARCHAR(255) NULL,   -- [optional] Override auto-plural name
    DbName        NVARCHAR(255) NULL,   -- [optional] Name of table in database. Specify only if the db table name is different from the "Name" property.
    ContextId     INT           NOT NULL,
    Attributes    NVARCHAR(500) NULL,   -- [optional] Use a tilda ~ delimited list of attributes to add to this table property. e.g. [CustomSecurity(Security.ReadOnly)]~[AnotherAttribute]~[Etc]
                                        --            The tilda ~ delimiter used in Attributes can be changed if you set Settings.MultiContextAttributeDelimiter = '~'; to something else.
    DbSetModifier NVARCHAR(128) NULL,   -- [optional] Will override setting of table.DbSetModifier. Default is "public".

    CONSTRAINT PK_Table PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_Table_Context_ContextId FOREIGN KEY (ContextId) REFERENCES MultiContext.Context (Id) ON DELETE NO ACTION
);
GO

-- You can add extra fields to this table. All columns will be read in and stored in a Dictionary<string,object>() for you to access and process.
CREATE TABLE MultiContext.[Column]
(
    Id               INT           NOT NULL IDENTITY(1, 1),
    Name             NVARCHAR(255) NOT NULL,
    DbName           NVARCHAR(255) NULL,    -- [optional] Name of column in database. Specify only if the db column name is different from the "Name" property.
    IsPrimaryKey     BIT           NULL,    -- [optional] Useful for views as views don't have primary keys.
    OverrideModifier BIT           NULL,    -- [optional] Adds "override" modifier.
    EnumType         NVARCHAR(255) NULL,    -- [optional] Use enum type instead of data type
    TableId          INT           NOT NULL,
    Attributes       NVARCHAR(500) NULL,    -- [optional] Use a tilda ~ delimited list of attributes to add to a poco property. e.g. [CustomSecurity(Security.ReadOnly)]~[Required]
                                            --            The tilda ~ delimiter used in Attributes can be changed if you set Settings.MultiContextAttributeDelimiter = '~'; to something else.
    PropertyType     NVARCHAR(128) NULL,    -- [optional] Will override setting of column.PropertyType
    IsNullable       BIT           NULL,    -- [optional] Will override setting of column.IsNullable

    CONSTRAINT PK_Column PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_Column_Table_TableId FOREIGN KEY (TableId) REFERENCES MultiContext.[Table] (Id) ON DELETE NO ACTION
);
GO

-- Existing foreign keys will be read and used as normal from the source database, however you can specify extra foreign keys here.
-- Define extra navigation relationships, such as views, since views don’t have relationships.
-- Specify names as defined in the database, not how they will be named in C#
CREATE TABLE MultiContext.ForeignKey
(
    Id                  INT           NOT NULL IDENTITY(1, 1),
    ContextId           INT           NOT NULL,
    ConstraintName      NVARCHAR(128) NOT NULL, -- The contstraint name appears in the comments of the foreign key
    ParentName          NVARCHAR(128) NULL,     -- [optional] Name of the parent foreign key property. If NULL it will be generated.
    ChildName           NVARCHAR(128) NULL,     -- [optional] Name of the child foreign key property. If NULL it will be generated.
    PkSchema            NVARCHAR(128) NULL,     -- [optional] Will default to MultiContext.Context.BaseSchema
    PkTableName         NVARCHAR(128) NOT NULL,
    PkColumn            NVARCHAR(128) NOT NULL,
    FkSchema            NVARCHAR(128) NULL,     -- [optional] Will default to MultiContext.Context.BaseSchema
    FkTableName         NVARCHAR(128) NOT NULL,
    FkColumn            NVARCHAR(128) NOT NULL,
    Ordinal             INT           NOT NULL, -- Order of this item
    CascadeOnDelete     BIT           NOT NULL, -- If false will add .WillCascadeOnDelete(false)
    IsNotEnforced       BIT           NOT NULL, -- If not enforced, it means foreign key is optional. .HasOptional(...) or .HasRequired(...)
    HasUniqueConstraint BIT           NOT NULL, -- True if this FK points to columns that have a unique constraint against them
    CONSTRAINT PK_ForeignKey PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_ForeignKey_Context_ContextId FOREIGN KEY (ContextId) REFERENCES MultiContext.Context (Id) ON DELETE NO ACTION
);
GO

CREATE NONCLUSTERED INDEX IX_Table_ContextId           ON MultiContext.[Table]         (ContextId);
CREATE NONCLUSTERED INDEX IX_Column_TableId            ON MultiContext.[Column]        (TableId);
CREATE NONCLUSTERED INDEX IX_Enumeration_ContextId     ON MultiContext.Enumeration     (ContextId);
CREATE NONCLUSTERED INDEX IX_Function_ContextId        ON MultiContext.[Function]      (ContextId);
CREATE NONCLUSTERED INDEX IX_StoredProcedure_ContextId ON MultiContext.StoredProcedure (ContextId);
CREATE NONCLUSTERED INDEX IX_ForeignKey_ContextId      ON MultiContext.ForeignKey      (ContextId);
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_Context_Name         ON MultiContext.Context         ([Name]);
CREATE UNIQUE NONCLUSTERED INDEX UX_Enumeration_Name     ON MultiContext.Enumeration     (ContextId, [Name]);
CREATE UNIQUE NONCLUSTERED INDEX UX_Function_Name        ON MultiContext.[Function]      (ContextId, [Name]);
CREATE UNIQUE NONCLUSTERED INDEX UX_StoredProcedure_Name ON MultiContext.StoredProcedure (ContextId, [Name]);
CREATE UNIQUE NONCLUSTERED INDEX UX_Table_Name           ON MultiContext.[Table]         (ContextId, [Name]);
CREATE UNIQUE NONCLUSTERED INDEX UX_Column_Name          ON MultiContext.[Column]        (TableId, [Name]);
GO

/* If you need to reset the data
TRUNCATE TABLE MultiContext.ForeignKey
TRUNCATE TABLE MultiContext.Enumeration
TRUNCATE TABLE MultiContext.[Column]
TRUNCATE TABLE MultiContext.StoredProcedure
TRUNCATE TABLE MultiContext.[Function]
DELETE FROM MultiContext.[Table]
DELETE FROM MultiContext.Context
*/

-- Example for inserting data
INSERT INTO MultiContext.Context ([Name], [Description], BaseSchema, [Namespace])
VALUES (N'EnumerationDbContext',N'This module is used to extract the enumerations', 'dbo', NULL),
       (N'AppleDbContext', N'Testing apples',  NULL, NULL),
       (N'BananaDbContext', N'Testing bananas', N'dbo', NULL),
	   (N'CherryDbContext', N'Testing cherries', N'dbo', 'Cherry'),
	   (N'DamsonDbContext', N'Testing Damson plums', NULL, 'Plum');
GO
UPDATE MultiContext.Context SET filename='CherryDatabaseContext' WHERE Name='CherryDbContext'
--UPDATE MultiContext.Context SET TemplatePath='C:\path_to_templates\' WHERE [Name]='DamsonDbContext'
GO

DECLARE @id INT;
SELECT @id = Id FROM MultiContext.Context WHERE [Name]='EnumerationDbContext';
INSERT INTO MultiContext.Enumeration ([Name], [Table], NameField, ValueField, ContextId)
VALUES (N'DaysOfWeek',N'EnumTest.DaysOfWeek',N'TypeName',N'TypeId', @id),
       (N'CarOptions',N'EnumsWithStringAsValue',N'enum_name',N'value', @id);
GO
DECLARE @id INT;
SELECT @id = Id FROM MultiContext.Context WHERE [Name]='AppleDbContext';
INSERT INTO MultiContext.[Table] ([Name], [Description], PluralName, DbName, ContextId)
VALUES (N'Boo', NULL, NULL, N'Stafford.Boo', @id),
       (N'Foo', NULL, NULL, N'Stafford.Foo', @id);
INSERT INTO MultiContext.[Column] (Name, DbName, IsPrimaryKey, OverrideModifier, EnumType, TableId)
VALUES (N'id', NULL, NULL, 0, NULL,   (SELECT id FROM MultiContext.[Table] WHERE ContextId=@id AND Name=N'Boo')),
       (N'Name', NULL, NULL, 0, NULL, (SELECT id FROM MultiContext.[Table] WHERE ContextId=@id AND Name=N'Boo')),
	   (N'id', NULL, NULL, 0, NULL,   (SELECT id FROM MultiContext.[Table] WHERE ContextId=@id AND Name=N'Foo'));
GO
DECLARE @id INT;
SELECT @id = Id FROM MultiContext.Context WHERE [Name]=N'BananaDbContext';
INSERT INTO MultiContext.[Table] ([Name], [Description], PluralName, DbName, ContextId)
VALUES (N'ComputedColumns', NULL, NULL, N'Stafford.ComputedColumns', @id);
INSERT INTO MultiContext.[Column] (Name, DbName, IsPrimaryKey, OverrideModifier, EnumType, TableId)
VALUES (N'id', NULL, NULL, 0, NULL,       (SELECT id FROM MultiContext.[Table] WHERE ContextId=@id AND Name=N'ComputedColumns')),
       (N'MyColumn', NULL, NULL, 0, NULL, (SELECT id FROM MultiContext.[Table] WHERE ContextId=@id AND Name=N'ComputedColumns'));
GO
DECLARE @id INT;
SELECT @id = Id FROM MultiContext.Context WHERE [Name]='CherryDbContext';
INSERT INTO MultiContext.[Table] ([Name], [Description], PluralName, DbName, ContextId)
VALUES (N'ColumnNameAndTypes', NULL, NULL, NULL, @id);
INSERT INTO MultiContext.[Column] (Name, DbName, IsPrimaryKey, OverrideModifier, EnumType, TableId)
VALUES (N'Dollar', N'$' ,NULL,0, NULL,          (SELECT id FROM MultiContext.[Table] WHERE ContextId=@id AND Name=N'ColumnNameAndTypes')),
       (N'Pound', N'[£]',NULL,0, NULL,          (SELECT id FROM MultiContext.[Table] WHERE ContextId=@id AND Name=N'ColumnNameAndTypes')),
	   (N'StaticField', N'static',NULL,0, NULL, (SELECT id FROM MultiContext.[Table] WHERE ContextId=@id AND Name=N'ColumnNameAndTypes')),
	   (N'Day', N'readonly',NULL,0, NULL,       (SELECT id FROM MultiContext.[Table] WHERE ContextId=@id AND Name=N'ColumnNameAndTypes'));
GO
DECLARE @id INT;
SELECT @id = Id FROM MultiContext.Context WHERE [Name]='DamsonDbContext';
INSERT INTO MultiContext.[Table] ([Name], [Description], PluralName, DbName, ContextId)
VALUES (N'NoPrimaryKeys', NULL, NULL, NULL, @id),
       (N'Parent', NULL, NULL, N'[Synonyms].[Parent]', @id);
INSERT INTO MultiContext.[Column] (Name, DbName, IsPrimaryKey, OverrideModifier, EnumType, TableId)
VALUES (N'Description', NULL ,1,0, NULL,           (SELECT id FROM MultiContext.[Table] WHERE ContextId=@id AND Name=N'NoPrimaryKeys')),
       (N'ParentId', NULL,NULL,0, NULL,	           (SELECT id FROM MultiContext.[Table] WHERE ContextId=@id AND Name=N'Parent')),
	   (N'ParentName', N'ParentName',NULL,0, NULL, (SELECT id FROM MultiContext.[Table] WHERE ContextId=@id AND Name=N'Parent'));
INSERT INTO MultiContext.ForeignKey (ContextId, ConstraintName, ParentName, ChildName, PkSchema, PkTableName, PkColumn, FkSchema, FkTableName, FkColumn, Ordinal, CascadeOnDelete, IsNotEnforced, HasUniqueConstraint)
VALUES (@id, N'CustomNameForForeignKey', N'ParentFkName', N'ChildFkName', N'dbo', N'NoPrimaryKeys', N'Description', N'Synonyms', N'Parent', N'ParentName', 1, 0, 0, 0);
GO

UPDATE MultiContext.[Column] SET Attributes=N'[ExampleForTesting("abc")]~[CustomRequired]' WHERE Name=N'Dollar'
UPDATE MultiContext.[Column] SET Attributes=N'[ExampleForTesting("def")]~[CustomSecurity(SecurityEnum.Readonly)]' WHERE Name=N'Pound'
GO

-- Test to make sure all optional fields are read in and stored in dictionary
ALTER TABLE MultiContext.[Column] ADD Test VARCHAR(10) NULL;
GO
ALTER TABLE MultiContext.[Column] ADD DummyInt int NULL;
GO
ALTER TABLE MultiContext.[Column] ADD date_of_birth DATETIME NULL;
GO
UPDATE MultiContext.[Column] SET Test = N'Hello', DummyInt = 1234, date_of_birth = '20 June 2019' WHERE Name=N'Dollar'
UPDATE MultiContext.[Column] SET Test = N'World', DummyInt = 5678, date_of_birth = '21 June 2019' WHERE Name=N'Pound'
GO

/* To see what settings you have
SELECT * FROM MultiContext.Context;
SELECT * FROM MultiContext.[Table];
SELECT * FROM MultiContext.[Column];
SELECT * FROM MultiContext.StoredProcedure;
SELECT * FROM MultiContext.[Function];
SELECT * FROM MultiContext.Enumeration;
SELECT * FROM MultiContext.ForeignKey;
*/

USE [EfrpgTest]
GO

-- Enumeration generation tests
CREATE SCHEMA EnumTest
GO
-- Enum inside schema
CREATE TABLE EnumTest.DaysOfWeek
(
	TypeName VARCHAR(50) NOT NULL,
	TypeId INT NOT NULL,
    CONSTRAINT PK_EnumTest_DaysOfWeek PRIMARY KEY (TypeId ASC)
);
GO
INSERT INTO EnumTest.DaysOfWeek (TypeName, TypeId)
VALUES ('Sun', 0), ('Mon', 1), ('Tue', 2), ('Wed', 3), ('Thu', 4), ('Fri', 6), ('Sat', 7);
GO
-- Enum in default schema
CREATE TABLE dbo.EnumsWithStringAsValue
(
	enum_name VARCHAR(50) NOT NULL,
	[value] VARCHAR(10) NOT NULL
);
GO
INSERT INTO EnumsWithStringAsValue
VALUES ('SunRoof','0x01'), ('Spoiler', '0x02'), ('FogLights', '0x04'), ('TintedWindows', '0x08')
GO
CREATE TABLE EnumTest.OpenDays
(
    Id INT IDENTITY(1, 1) NOT NULL,
    EnumId INT NOT NULL,
    CONSTRAINT PK_OpenDays PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT Fk_OpenDays_EnumId
        FOREIGN KEY (EnumId)
        REFERENCES EnumTest.DaysOfWeek (TypeId)
);
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
    Id INT NOT NULL IDENTITY(1,1),
	MyColumn varchar(10) NOT NULL,
	MyComputedColumn AS MyColumn,
    CONSTRAINT PK_Stafford_ComputedColumns PRIMARY KEY CLUSTERED (id ASC)
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

IF SCHEMA_ID(N'CustomSchema') IS NULL
    EXEC (N'CREATE SCHEMA [CustomSchema];');
GO

CREATE FUNCTION [CustomSchema].[CsvToIntWithSchema]
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

CREATE FUNCTION udfNetSale (@quantity INT, @list_price DEC(10, 2), @discount DEC(4, 2))
RETURNS DEC(10, 2)
AS
BEGIN
    RETURN @quantity * @list_price * (1 - @discount);
END;
GO

-- DROP TABLE ColumnNameAndTypes;
CREATE TABLE ColumnNameAndTypes
(
	[$] INT NOT null,
	[%] INT null,
	[£] INT null,
	[&fred$] INT null,
    [abc/\] INT null,
    [joe.bloggs] INT null, -- Contains a period
    [simon-hughes] INT null, -- Snake cased
	[description] varchar(20) NOT NULL DEFAULT (space((0))),
    someDate DATETIME2(7) NOT NULL DEFAULT (GETDATE()),
	[Obs] VARCHAR(50) NULL CONSTRAINT [DF__PlanStudies_Obs] DEFAULT ('[{"k":"en","v":""},{"k":"pt","v":""}]'), -- #281 Default values must be escaped on entity classes
	[Obs1] VARCHAR(50) NULL CONSTRAINT [DF__PlanStudies_Obs1] DEFAULT ('\'), -- #281
	[Obs2] VARCHAR(50) NULL CONSTRAINT [DF__PlanStudies_Obs2] DEFAULT ('\\'), -- #281
	[Obs3] VARCHAR(50) NULL CONSTRAINT [DF__PlanStudies_Obs3] DEFAULT ('\\\'), -- #281
	[static] INT NULL, -- #279 Illegal C#
	[readonly] INT NULL, -- #279 Illegal C#
	[123Hi] INT NULL, -- #279 Illegal C#
	[areal] REAL NULL DEFAULT (1.23), -- #283 need default as 1.23f
	[afloat] FLOAT NULL DEFAULT (999.),
	[afloat8] FLOAT(8) NULL,
	[afloat20] FLOAT(20) NULL,
	[afloat24] FLOAT(24) NULL, -- same as real. real = float(24)
	[afloat53] FLOAT(53) NULL,
	[adecimal] DECIMAL NULL,
	[adecimal_19_4] DECIMAL(19, 4) NULL,
	[adecimal_10_3] DECIMAL(10, 3) NULL,
	[anumeric] NUMERIC NULL,
	[anumeric_5_2] NUMERIC(5, 2) NULL,
	[anumeric_11_3] NUMERIC(11, 3) NULL,
	[amoney] MONEY NULL,
	[asmallmoney] SMALLMONEY NULL,
	[brandon] INT NULL, -- Brandon Lilly. IsFixedLength not acceptable for CHAR fixed length columns.
	GeographyType GEOGRAPHY NULL DEFAULT(CONVERT(GEOGRAPHY,'POINT (0 0)')),
	GeometryType GEOMETRY NULL DEFAULT(GEOMETRY::STGeomFromText('LINESTRING (100 100, 20 180, 180 180)', 0)),
    CONSTRAINT PK_ColumnNameAndTypes PRIMARY KEY CLUSTERED ([$])
);
GO
EXEC sys.sp_addextendedproperty
	@name = N'MS_Description',   
	@value = N'This is to document the bring the action table',
	@level0type = N'SCHEMA', @level0name = 'dbo',  
	@level1type = N'TABLE',  @level1name = 'ColumnNameAndTypes';
GO
INSERT INTO ColumnNameAndTypes ([$]) VALUES (1);
GO
EXEC sys.sp_addextendedproperty
    @name = N'MS_Description_v2',
    @value = N'This is to document the


    table with poor column name choices',
    @level0type = N'SCHEMA', @level0name = 'dbo',
    @level1type = N'TABLE',  @level1name = 'ColumnNameAndTypes';
GO

CREATE OR ALTER PROCEDURE ColumnNameAndTypesProc
AS
BEGIN
    SELECT someDate,
           Obs,
           [static],
           [readonly],
           areal,
           afloat,
           afloat8,
           afloat20,
           afloat24,
           afloat53,
           adecimal,
           adecimal_19_4,
           adecimal_10_3,
           anumeric,
           anumeric_5_2,
           anumeric_11_3,
           amoney,
           asmallmoney,
           GeographyType,
           GeometryType
    FROM ColumnNameAndTypes;
END;
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
GO

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
GO

CREATE PROCEDURE [Synonyms].[SimpleStoredProc]
	@InputInt int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT 'Return' AS ReturnValue
END
GO

-- Create synonyms pointing to main test dabase
USE [EfrpgTest_Synonyms]
GO

CREATE SCHEMA Synonyms
GO
CREATE SCHEMA [CustomSchema]
GO

CREATE SYNONYM [Synonyms].[Parent] FOR [EfrpgTest].[Synonyms].[Parent]
CREATE SYNONYM [Synonyms].[Child] FOR [EfrpgTest].[Synonyms].[Child]
CREATE SYNONYM [Synonyms].[SimpleStoredProc] FOR [EfrpgTest].[Synonyms].[SimpleStoredProc]
CREATE SYNONYM [CustomSchema].[CsvToIntWithSchema] FOR [EfrpgTest].[CustomSchema].[CsvToIntWithSchema]
GO

-- Create table with multiple FK's
CREATE TABLE dbo.UserInfo
(
    Id INT IDENTITY(1, 1) NOT NULL,
	Forename VARCHAR(20) NULL
    CONSTRAINT PK_UserInfo PRIMARY KEY CLUSTERED ([Id] ASC),
)
CREATE TABLE dbo.UserInfoAttributes
(
    Id INT IDENTITY(1, 1) NOT NULL,
    CONSTRAINT PK_UserInfoAttributes PRIMARY KEY CLUSTERED ([Id] ASC),
    PrimaryId INT NOT NULL,
    SecondaryId INT NOT NULL
)
GO
ALTER TABLE dbo.UserInfoAttributes WITH CHECK ADD CONSTRAINT FK_UserInfoAttributes_PrimaryUserInfo FOREIGN KEY(PrimaryId) REFERENCES dbo.UserInfo (id);
ALTER TABLE dbo.UserInfoAttributes WITH CHECK ADD CONSTRAINT FK_UserInfoAttributes_SecondaryUserInfo FOREIGN KEY(SecondaryId) REFERENCES dbo.UserInfo (id);
ALTER TABLE dbo.UserInfoAttributes CHECK CONSTRAINT FK_UserInfoAttributes_PrimaryUserInfo;
ALTER TABLE dbo.UserInfoAttributes CHECK CONSTRAINT FK_UserInfoAttributes_SecondaryUserInfo;
GO

USE EfrpgTest
GO

-- DROP TABLE BatchTest
CREATE TABLE BatchTest
(
	code NVARCHAR(8) NOT NULL,
	CONSTRAINT PK_BatchTest PRIMARY KEY CLUSTERED (code)
);
GO

-- Table contains a period
-- DROP TABLE [Period.Table]
CREATE TABLE [Period.Table]
(
	id INT NOT NULL,
	[joe.bloggs] INT null, -- Column contains a period
	CONSTRAINT PK_Period_Table PRIMARY KEY CLUSTERED (Id)
);
GO
-- Table contains a period
-- DROP TABLE [PeriodTestTable]
CREATE TABLE [PeriodTestTable]
(
	id INT NOT NULL,
	[joe.bloggs] INT null, -- Column contains a period
	CONSTRAINT PK_PeriodTestTable PRIMARY KEY (id)
);
GO

-- DROP TABLE SmallDecimalTest
CREATE TABLE SmallDecimalTest
(
	id INT NOT NULL,
	KoeffVed DECIMAL(4,4) NULL DEFAULT (0.5),
	CONSTRAINT PK_SmallDecimalTest PRIMARY KEY (id)
);
GO

/****** Object:  Default [d_t_address_type_domain]    Script Date: 22/07/2015 14:28:05 ******/
CREATE DEFAULT [dbo].[d_t_address_type_domain] 
AS
'A'
GO

-- DROP TABLE PropertyTypesToAdd
CREATE TABLE PropertyTypesToAdd
(
	id INT NOT NULL,
	dt_default DATETIME2 NULL,
	dt7 DATETIME2(7) NULL,
    defaultCheck varchar(10) NULL,
	CONSTRAINT PK_PropertyTypesToAdd PRIMARY KEY (id)
)
GO
sp_bindefault '[dbo].[d_t_address_type_domain]', 'PropertyTypesToAdd.defaultCheck';
GO

CREATE SCHEMA FkTest
GO
-- DROP TABLE FkTest.SmallDecimalTestAttribute
CREATE TABLE FkTest.SmallDecimalTestAttribute
(
	FkID INT NOT NULL,
	[description] varchar(20) NOT NULL,
    CONSTRAINT PK_FkTest_SmallDecimalTestAttribute PRIMARY KEY (FkID)
)
GO

ALTER TABLE FkTest.SmallDecimalTestAttribute ADD CONSTRAINT KateFK FOREIGN KEY (FkID) REFERENCES dbo.SmallDecimalTest (id)
-- ALTER TABLE FkTest.SmallDecimalTestAttribute drop CONSTRAINT KateFK;
GO
-- DROP PROC FkTest.Hello;
CREATE PROC FkTest.Hello AS
    SELECT  [static],[readonly] -- #279 Contains static and readonly, which are illegal in C#
    FROM    ColumnNameAndTypes
GO
CREATE VIEW SmallDecimalTestView
AS
	SELECT  FkID,
	        description FROM FkTest.SmallDecimalTestAttribute
GO

CREATE VIEW [view.with.multiple.periods]
AS
	SELECT  FkID,
	        description FROM FkTest.SmallDecimalTestAttribute
GO
-- DROP TABLE [table.with.multiple.periods]
CREATE TABLE [table.with.multiple.periods]
(
    id INT NOT NULL,
    [description] varchar(20) NOT NULL,
	CONSTRAINT PK_table_with_multiple_periods PRIMARY KEY (id)
)
GO

--DROP TABLE HasPrincipalKeyTestChild; DROP TABLE HasPrincipalKeyTestParent
CREATE TABLE HasPrincipalKeyTestParent
(
    Id INT IDENTITY(1,1) NOT NULL,
	AA INT NOT NULL,
	BB INT NOT NULL,
	CC INT NULL,
	DD INT NULL,
	CONSTRAINT PK_HasPrincipalKeyTestParent PRIMARY KEY (Id)
);
GO
CREATE TABLE HasPrincipalKeyTestChild
(
    Id INT IDENTITY(1,1) NOT NULL,
	A INT NOT NULL,
	B INT NOT NULL,
	C INT NULL,
	D INT NULL,
	CONSTRAINT PK_HasPrincipalKeyTestChild PRIMARY KEY (Id)
);
GO
ALTER TABLE HasPrincipalKeyTestParent ADD CONSTRAINT [UQ_HasPrincipalKeyTestParent_AB] UNIQUE NONCLUSTERED (AA, BB);
ALTER TABLE HasPrincipalKeyTestParent ADD CONSTRAINT [UQ_HasPrincipalKeyTestParent_CD] UNIQUE NONCLUSTERED (CC, DD);
ALTER TABLE HasPrincipalKeyTestParent ADD CONSTRAINT [UQ_HasPrincipalKeyTestParent_AC] UNIQUE NONCLUSTERED (AA, CC);
GO
ALTER TABLE HasPrincipalKeyTestChild WITH CHECK ADD CONSTRAINT [FK_HasPrincipalKey_AB] FOREIGN KEY(A, B) REFERENCES HasPrincipalKeyTestParent (AA, BB);
ALTER TABLE HasPrincipalKeyTestChild WITH CHECK ADD CONSTRAINT [FK_HasPrincipalKey_CD] FOREIGN KEY(C, D) REFERENCES HasPrincipalKeyTestParent (CC, DD);
ALTER TABLE HasPrincipalKeyTestChild WITH CHECK ADD CONSTRAINT [FK_HasPrincipalKey_AC] FOREIGN KEY(A, C) REFERENCES HasPrincipalKeyTestParent (AA, CC);
GO


CREATE SCHEMA FFRS
GO
CREATE TABLE [FFRS].[CV](
	[BatchUID] [uniqueidentifier] NOT NULL,
	[CVID] [int] NOT NULL,
	[CVName] [nvarchar](200) NULL
) ON [PRIMARY]
GO

CREATE PROC FFRS.cv_data(@maxId INT)
AS
SELECT BatchUID,
       CVID,
       CVName FROM FFRS.CV WHERE CVID < @maxId
GO

CREATE PROC FFRS.data_from_dbo
AS
SELECT Id, PrimaryColourId, CarMake FROM Car
GO

CREATE PROC FFRS.data_from_dbo_and_ffrs
AS
SELECT Id, PrimaryColourId, CarMake, CVName FROM Car JOIN FFRS.CV ON Id = CVID
GO

CREATE PROC dbo.dbo_proc_data_from_ffrs(@maxId INT)
AS
SELECT BatchUID,
       CVID,
       CVName FROM FFRS.CV WHERE CVID < @maxId
GO

CREATE PROC dbo.dbo_proc_data_from_ffrs_and_dbo
AS
SELECT Id, PrimaryColourId, CarMake, CVName FROM Car JOIN FFRS.CV ON Id = CVID
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

SELECT * FROM FFRS.CsvToInt2('123,456','')
GO

SELECT dbo.udfNetSale(10, 100, 0.1) AS [Value];
GO



-- #183 Return Model for functions returning nullable
CREATE FUNCTION [dbo].[182_test1]
(
	@test INT
)
RETURNS TABLE AS RETURN
(
	SELECT Id, ISNULL([Description], '') AS [Description]
	FROM NoPrimaryKeys
)
GO
CREATE PROCEDURE [dbo].[182_test2]
	@Flag INT
AS
BEGIN
	SET NOCOUNT ON;

	IF @Flag = 1
	BEGIN
		SELECT Id, [Description] AS DescriptionFlag1
		FROM NoPrimaryKeys
	END

	SELECT Id, ISNULL([Description], '') AS [DescriptionNotNull]
	FROM NoPrimaryKeys

	SELECT Id, [Description]
	FROM NoPrimaryKeys
END
GO



-- #711 Calculated Column Not Null
CREATE TABLE CalculatedColumnNotNull
(
    ID            INT     IDENTITY(1, 1) NOT NULL,
    [Type]        TINYINT NOT NULL,
    IsCalendar    AS CONVERT(BIT, CASE WHEN [Type] BETWEEN 0 AND  7 THEN 1 ELSE 0 END) PERSISTED NOT NULL,
    IsUtilization AS CONVERT(BIT, CASE WHEN [Type] BETWEEN 8 AND 10 THEN 1 ELSE 0 END) PERSISTED NOT NULL,
    CONSTRAINT PK_CalculatedColumnNotNull PRIMARY KEY NONCLUSTERED (ID ASC)
);
GO

CREATE OR ALTER TRIGGER CalculatedColumnAudit ON CalculatedColumnNotNull FOR INSERT NOT FOR REPLICATION AS
BEGIN
    PRINT GETDATE();
END
GO

CREATE OR ALTER TRIGGER CalculatedColumnAuditUpdate ON CalculatedColumnNotNull FOR UPDATE NOT FOR REPLICATION AS
BEGIN
    PRINT GETUTCDATE();
END
GO


-- Stored procedures resolving to the same name
-- DROP PROC resolve_to_same_name
/*CREATE PROC resolve_to_same_name
AS
BEGIN
    SELECT name, type, type_desc FROM sys.objects
END
GO
-- DROP PROC Resolve_ToSameName
CREATE PROC Resolve_ToSameName
AS
BEGIN
    SELECT name, type, type_desc FROM sys.objects
END
GO*/



-- #670 Table name issue
CREATE TABLE Task
(
    TaskId  BIGINT NOT NULL
    CONSTRAINT PK_Task PRIMARY KEY CLUSTERED (TaskId ASC)
);
GO



-- #614
CREATE TABLE Burak2
(
    id  BIGINT IDENTITY(1, 1) NOT NULL,
    num BIGINT NOT NULL,
    CONSTRAINT PK_Burak2 PRIMARY KEY CLUSTERED (id ASC),
    CONSTRAINT U_Burak2 UNIQUE NONCLUSTERED (id ASC, num ASC)
);
GO
CREATE TABLE Burak1
(
    id   BIGINT IDENTITY(1, 1) NOT NULL,
    id_t BIGINT NOT NULL,
    num  BIGINT NOT NULL,
    CONSTRAINT PK_Burak1 PRIMARY KEY CLUSTERED (id ASC)
);
GO
ALTER TABLE Burak1 WITH CHECK ADD CONSTRAINT FK_Burak_Test1 FOREIGN KEY (id_t, num) REFERENCES Burak2 (id, num);
ALTER TABLE Burak1 WITH CHECK ADD CONSTRAINT FK_Burak_Test2 FOREIGN KEY (id,   num) REFERENCES Burak2 (id, num);
ALTER TABLE Burak1 CHECK CONSTRAINT FK_Burak_Test1;
ALTER TABLE Burak1 CHECK CONSTRAINT FK_Burak_Test1;
GO


-- #714 Self-referencing tables. Thanks to afust003
-- DROP TABLE TableA
CREATE TABLE TableA
(
	TableAId INT IDENTITY(1, 1) NOT NULL,
	TableADesc VARCHAR(20) NULL,
	CONSTRAINT TableA_pkey PRIMARY KEY (TableAId ASC)
);
GO
-- DROP TABLE TableB
CREATE TABLE TableB
(
	TableBId INT IDENTITY(1, 1) NOT NULL,
	TableAId INT NOT NULL,
	ParentTableAId INT NULL,
	TableBDesc VARCHAR(20) NULL,
	CONSTRAINT TableB_pkey PRIMARY KEY (TableBId, TableAId),
	CONSTRAINT FK_TableA_CompositeKey_Req FOREIGN KEY (TableAId) REFERENCES TableA (TableAId),
	CONSTRAINT ParentTableB_Hierarchy FOREIGN KEY (TableAId, TableBId) REFERENCES TableB
);
GO
CREATE INDEX fki_ParentTableA_FK_Constraint ON TableB (TableAId);
GO



-- #453 datetimeoffset type SQL default function getdate() does not translate
CREATE TABLE [dbo].[DateTimeDefaultTest](
	[Id] [int] NOT NULL,
	[CreatedDate] [datetimeoffset](7) NULL
)
GO
ALTER TABLE [dbo].[DateTimeDefaultTest] ADD  CONSTRAINT [DF_DateTimeDefaultTest_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO



-- #329 Async SPROCs with output parameters. Thanks to ymerej.
CREATE PROCEDURE dbo.InsertRecord
    @Data VARCHAR(256),
    @InsertedId INT OUT
AS
BEGIN
    INSERT INTO TableA (TableADesc) VALUES (@Data)

    SET @InsertedId = SCOPE_IDENTITY();
END;
GO
-- drop PROCEDURE dbo.InsertRecordTwo
CREATE PROCEDURE dbo.InsertRecordTwo
    @Data VARCHAR(256),
    @InsertedId INT OUT,
    @AnotherInsertedId INT OUT
AS
BEGIN
    INSERT INTO TableA (TableADesc) VALUES (@Data)
    SET @InsertedId = SCOPE_IDENTITY();
    SET @AnotherInsertedId = @InsertedId + 1;
END;
GO
CREATE PROCEDURE dbo.InsertRecordThree
    @Data VARCHAR(256),
    @InsertedId INT OUT,
	@SomeId INT,
    @AnotherInsertedId INT OUT
AS
BEGIN
    INSERT INTO TableA (TableADesc) VALUES (@Data)
    SET @InsertedId = SCOPE_IDENTITY();
    SET @AnotherInsertedId = @InsertedId + 1;
END;
GO



-- #364 Generate foreign keys: FK against a unique index
CREATE TABLE dbo.CODE_MeetingTopicDetails
(
    id              INT           IDENTITY(1, 1) NOT NULL,
    id_reuniao      INT           NOT NULL,
    ord_trab        INT           NULL,
    assunto         NVARCHAR(250) NULL,
    desenvolvimento NVARCHAR(MAX) NULL,
    origem          NVARCHAR(5)   NULL,
    id_origem       INT           NULL,
    Estado          INT           NULL,
    CompanyID       INT           NOT NULL,
    DateCreated     DATETIME      NOT NULL,
    DateChanged     DATETIME      NULL,
    CONSTRAINT PK_CODE_MeetingTopicDetails PRIMARY KEY CLUSTERED (id ASC)
);
GO
CREATE TABLE dbo.CODE_PARAM_MeetingTopicDetailSource
(
    ID          INT          IDENTITY(1, 1) NOT NULL,
    Code        NVARCHAR(5)  NOT NULL,
    Label       NVARCHAR(50) NULL,
    LabelENG    NVARCHAR(50) NULL,
    LabelESP    NVARCHAR(50) NULL,
    LabelFRA    NVARCHAR(50) NULL,
    DateCreated DATETIME     NOT NULL,
    DateChanged DATETIME     NULL,
    CONSTRAINT PK_CODE_PARAM_MeetingTopicDetailSource PRIMARY KEY CLUSTERED (ID ASC),
    CONSTRAINT UK_CODE_PARAM_MeetingTopicDetailSource UNIQUE NONCLUSTERED (Code ASC)
);
GO
ALTER TABLE [dbo].[CODE_MeetingTopicDetails] WITH CHECK ADD CONSTRAINT [FK_CODE_MeetingTopicDetails_CODE_PARAM_MeetingTopicDetailSource]
     FOREIGN KEY([origem]) REFERENCES [dbo].[CODE_PARAM_MeetingTopicDetailSource] ([Code])
GO
ALTER TABLE [dbo].[CODE_MeetingTopicDetails] CHECK CONSTRAINT [FK_CODE_MeetingTopicDetails_CODE_PARAM_MeetingTopicDetailSource]
GO



-- From Unders0n. Case #161
CREATE TABLE [dbo].[Бренды товара](
	[Код бренда] [int] IDENTITY(1,1) NOT NULL,
	[Наименование бренда] [varchar](50) NOT NULL,
	[Логотип_бренда] [image] NULL,
	[Логотип_бренда_вертикальный] [image] NULL,
    CONSTRAINT [PK_Бренды] PRIMARY KEY CLUSTERED ( [Код бренда] ASC )
)
GO


-- #571 Stored procs returning spatial types not being mapped correctly
-- DROP PROCEDURE dbo.SpatialTypesWithParams
-- DROP PROCEDURE dbo.SpatialTypesNoParams
CREATE PROCEDURE dbo.SpatialTypesWithParams (@geometry GEOMETRY, @geography GEOGRAPHY)
AS
    SELECT  [$] AS Dollar, GeographyType, GeometryType FROM ColumnNameAndTypes;
GO
CREATE PROCEDURE dbo.SpatialTypesNoParams
AS
    SELECT  [$] AS Dollar, someDate, GeographyType, GeometryType FROM ColumnNameAndTypes;
GO


-- From 0v3rCl0ck. Case 180
CREATE SCHEMA Alpha;
GO
CREATE SCHEMA Beta;
GO
CREATE SCHEMA Omega;
GO
CREATE PROCEDURE Alpha.Overclock (@Parameter DATETIME) AS BEGIN RETURN 0 END;
GO
CREATE PROCEDURE Beta.Overclock (@Parameter DATETIME) AS BEGIN RETURN 0 END;
GO
CREATE PROCEDURE Omega.Overclock (@Parameter DATETIME) AS BEGIN RETURN 0 END;
GO
CREATE TABLE Alpha.Test
(
    Id INT NULL,
    ExclusionTest INT NULL  -- Try and exclude this one column, and not the others below
)
CREATE TABLE Beta.Test
(
    Id INT NULL,
    ExclusionTest INT NULL
)
CREATE TABLE Omega.Test
(
    Id INT NULL,
    ExclusionTest INT NULL
)
CREATE TABLE dbo.Test
(
    Id INT NULL,
    ExclusionTest INT NULL
)
GO


-- Harish3485. Duplicate foreign key names "in different schemas" cause problems
-- Running transformation: Failed to read database schema - Object reference not set to an instance of an object.
--DROP TABLE Beta.Harish3485
--DROP TABLE Alpha.Harish3485
CREATE TABLE Alpha.Harish3485 ( id int NOT NULL IDENTITY(1,1), harish_id INT NOT NULL )
GO
CREATE TABLE Beta.Harish3485 ( id int NOT NULL IDENTITY(1,1), another_id INT NOT NULL )
GO
ALTER TABLE [Alpha].[Harish3485] ADD CONSTRAINT [PK_Alpha_Harish3485] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY];
ALTER TABLE [Beta].[Harish3485] ADD CONSTRAINT [PK_Beta_Harish3485] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY];
GO
--                                                     vvvvvvvvvvv same name                                              vvvvvv different column names
ALTER TABLE Alpha.Harish3485 WITH CHECK ADD CONSTRAINT [FK_Harish] FOREIGN KEY(harish_id) REFERENCES FkTest.SmallDecimalTestAttribute (FkID)
ALTER TABLE Beta.Harish3485  WITH CHECK ADD CONSTRAINT [FK_Harish] FOREIGN KEY(another_id) REFERENCES  PropertyTypesToAdd (id)
GO




-- #309 Two tables having one property with the same name and their relationship is related to the same table. Model generation issue. Thanks to fxvits.
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Country](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](12) NULL,
CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)) ON [PRIMARY] 
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
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Attendee] WITH CHECK ADD  CONSTRAINT [FK_Attendee_PhoneCountry] FOREIGN KEY([PhoneCountryID]) REFERENCES [dbo].[Country] ([CountryID])
ALTER TABLE [dbo].[User309]  WITH CHECK ADD  CONSTRAINT [FK_User309_PhoneCountry]  FOREIGN KEY([PhoneCountryID]) REFERENCES [dbo].[Country] ([CountryID])
ALTER TABLE [dbo].[Attendee] CHECK CONSTRAINT [FK_Attendee_PhoneCountry]
ALTER TABLE [dbo].[User309]  CHECK CONSTRAINT [FK_User309_PhoneCountry]
GO


-- #312 One to one association missing relationship fluent API or data annotation. Thanks to Chris3773
--DROP TABLE Foo
--DROP TABLE Boo
/*CREATE TABLE [dbo].[Boo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Boo] PRIMARY KEY CLUSTERED ([id] ASC)
)
CREATE TABLE [dbo].[Foo](
	[id] [int] NOT NULL,
	[name] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Foo] PRIMARY KEY CLUSTERED ([id] ASC)
)
GO
ALTER TABLE [dbo].[Foo] WITH CHECK ADD CONSTRAINT [FK_Foo_Boo] FOREIGN KEY([id]) REFERENCES [dbo].[Boo] ([id])
ALTER TABLE [dbo].[Foo] CHECK CONSTRAINT [FK_Foo_Boo]
GO
*/


-- #363 Foreign keys with NOCHECK cause compile problems
-- 0xced Shouldn't we completely drop the IsNotEnforced setting and use only the nullable information?
-- If a foreign key constraint is not enforced, the generator produces a model which is not valid for Entity
-- Framework and throws. It can even generate code that does not even compile with a combination of HasOptional + WithOptional.
--drop table ForeignKeyIsNotEnforcedItem
--drop table ForeignKeyIsNotEnforced
CREATE TABLE ForeignKeyIsNotEnforced
(
    id int NOT NULL IDENTITY(1,1),
    null_value INT NULL, not_null_value INT NOT NULL,
    CONSTRAINT PK_ForeignKeyIsNotEnforced PRIMARY KEY (id)
);
GO
CREATE TABLE ForeignKeyIsNotEnforcedItem
(
    id int NOT NULL IDENTITY(1,1),
    null_value INT NULL, not_null_value INT NOT NULL,
    CONSTRAINT PK_ForeignKeyIsNotEnforcedItem PRIMARY KEY (id)
);
GO
ALTER TABLE ForeignKeyIsNotEnforced ADD CONSTRAINT [UQ_ForeignKeyIsNotEnforced_null_value] UNIQUE NONCLUSTERED (null_value);
ALTER TABLE ForeignKeyIsNotEnforced ADD CONSTRAINT [UQ_ForeignKeyIsNotEnforced_not_null_value] UNIQUE NONCLUSTERED (not_null_value);
ALTER TABLE ForeignKeyIsNotEnforcedItem ADD CONSTRAINT [UQ_ForeignKeyIsNotEnforcedItem_null_value] UNIQUE NONCLUSTERED (null_value);
ALTER TABLE ForeignKeyIsNotEnforcedItem ADD CONSTRAINT [UQ_ForeignKeyIsNotEnforcedItem_not_null_value] UNIQUE NONCLUSTERED (not_null_value);
GO
ALTER TABLE ForeignKeyIsNotEnforcedItem WITH NOCHECK ADD CONSTRAINT [FK_ForeignKeyIsNotEnforcedItem_null_null] FOREIGN KEY(null_value) REFERENCES ForeignKeyIsNotEnforced (null_value);
ALTER TABLE ForeignKeyIsNotEnforcedItem WITH NOCHECK ADD CONSTRAINT [FK_ForeignKeyIsNotEnforcedItem_null_notnull] FOREIGN KEY(null_value) REFERENCES ForeignKeyIsNotEnforced (not_null_value);
ALTER TABLE ForeignKeyIsNotEnforcedItem WITH NOCHECK ADD CONSTRAINT [FK_ForeignKeyIsNotEnforcedItem_notnull_null] FOREIGN KEY(not_null_value) REFERENCES ForeignKeyIsNotEnforced (null_value);
ALTER TABLE ForeignKeyIsNotEnforcedItem WITH NOCHECK ADD CONSTRAINT [FK_ForeignKeyIsNotEnforcedItem_notnull_notnull] FOREIGN KEY(not_null_value) REFERENCES ForeignKeyIsNotEnforced (not_null_value);
GO
-- DELETE from ForeignKeyIsNotEnforced
-- DELETE FROM ForeignKeyIsNotEnforcedItem
INSERT INTO ForeignKeyIsNotEnforced     (null_value, not_null_value) VALUES (222, 222)
INSERT INTO ForeignKeyIsNotEnforced     (null_value, not_null_value) VALUES (333, 333)
INSERT INTO ForeignKeyIsNotEnforced     (null_value, not_null_value) VALUES (NULL,444)
INSERT INTO ForeignKeyIsNotEnforcedItem (null_value, not_null_value) VALUES (NULL,222)
INSERT INTO ForeignKeyIsNotEnforcedItem (null_value, not_null_value) VALUES (333, 333)
-- INSERT INTO ForeignKeyIsNotEnforcedItem (null_value, not_null_value) VALUES (NULL, 444) -- Violation of UNIQUE KEY constraint 'UQ_ForeignKeyIsNotEnforcedItem_null_value'. Cannot insert duplicate key in object 'dbo.ForeignKeyIsNotEnforcedItem'. The duplicate key value is (<NULL>).
SELECT * FROM ForeignKeyIsNotEnforced
SELECT * FROM ForeignKeyIsNotEnforcedItem
GO

-- Case 32. From android8.
CREATE SCHEMA App
GO
CREATE TABLE App.UserFacilityServiceRole ( userId INT NOT NULL, appId INT NOT NULL, fsrId INT NOT NULL )
GO
CREATE PROCEDURE [App].[usp_CMTUserFSRUpdate]
    @userId INT,
    @fsrId INT,
    @ufsrId INT OUT
AS
	SET NOCOUNT ON
	DECLARE @appId INT
	SET @appId = 2
	INSERT  [UserFacilityServiceRole] SELECT  @userId, @appId, @fsrId
	SELECT  @ufsrId = @@IDENTITY
GO


-- from dyardyGIT
-- DROP TABLE Beta.workflow
-- DROP TABLE Beta.ToAlpha
-- DROP TABLE Alpha.workflow
CREATE TABLE Alpha.workflow
(
    [Id] INT NOT NULL IDENTITY(1, 1),
    [Description] VARCHAR(10) NULL,
	CONSTRAINT PK_alpha_workflow PRIMARY KEY (Id)
);
GO
CREATE TABLE Beta.workflow
(
    [Id] INT NOT NULL IDENTITY(1, 1),
    [Description] VARCHAR(10) NULL
	CONSTRAINT PK_beta_workflow PRIMARY KEY (Id)
);
GO
-- m woffenden - filter out schema, but FK's are not filtered out.
CREATE TABLE Beta.ToAlpha
(
    [Id] INT NOT NULL IDENTITY(1, 1),
    AlphaId INT NOT NULL,
	CONSTRAINT PK_beta_ToAlpha PRIMARY KEY (Id)
);
GO
ALTER TABLE Beta.ToAlpha ADD CONSTRAINT BetaToAlpha_AlphaWorkflow FOREIGN KEY (AlphaId) REFERENCES [Alpha].[Workflow] ([Id])
GO



-- #632 Can't map view with recursive join
CREATE SCHEMA WVN
GO
CREATE TABLE WVN.Articles
(
    PK_Article       INT              IDENTITY(1, 1) NOT NULL,
    FK_Factory       UNIQUEIDENTIFIER NOT NULL,
    FK_ArticleLevel  INT              NOT NULL,
    FK_ParentArticle INT              NULL,
    Code             NVARCHAR(20)     NOT NULL CONSTRAINT PK_Articles PRIMARY KEY CLUSTERED (PK_Article ASC),
    CONSTRAINT UK_Articles UNIQUE NONCLUSTERED (FK_Factory ASC, FK_ArticleLevel ASC, Code ASC) 
);
GO
CREATE VIEW WVN.v_Articles
AS
    WITH TabRecursive AS
    (
        SELECT  ItemP.PK_Article,
                ItemP.FK_Factory,
                ItemP.FK_ArticleLevel,
                ItemP.FK_ParentArticle,
                ItemP.Code,
                CONVERT(NVARCHAR(100), ItemP.Code) AS FullCode
        FROM    WVN.Articles ItemP
        WHERE   ItemP.FK_ParentArticle IS NULL
        UNION ALL
        SELECT  Item.PK_Article,
                Item.FK_Factory,
                Item.FK_ArticleLevel,
                Item.FK_ParentArticle,
                Item.Code,
                CONVERT(NVARCHAR(100), CONCAT(Parent.FullCode, '/', Item.Code)) AS FullCode
        FROM    WVN.Articles Item
                INNER JOIN TabRecursive Parent
                    ON Parent.PK_Article = Item.FK_ParentArticle
    )
    SELECT  T.PK_Article,
            T.FK_Factory,
            T.FK_ArticleLevel,
            T.FK_ParentArticle,
            T.Code,
            T.FullCode
    FROM    TabRecursive T;
GO




-- #93 TadeuszSobol
-- DROP TABLE TadeuszSobol
CREATE TABLE TadeuszSobol
( 
	[Id] int NOT NULL IDENTITY(1,1), 
	[Description] VARCHAR(MAX) NULL, 
	[Notes] NVARCHAR(MAX) NULL, 
	[Name] VARCHAR(10) NULL,
	CONSTRAINT PK_TadeuszSobol PRIMARY KEY (Id)
)
GO

-- #68 Support for Synonyms (TimSirmovics)
CREATE SYNONYM alpha_workflow_synonym FOR Alpha.workflow
GO
INSERT INTO alpha_workflow_synonym ([description]) VALUES('Hello')
GO
CREATE SYNONYM cross_database_synonym FOR EfrpgTest_Synonyms.dbo.UserInfo
GO
INSERT INTO cross_database_synonym (Forename) VALUES ('Ruprecht');
GO


-- #186 Incorrect pluralization of reverse navigation property name
CREATE SCHEMA OneEightSix
GO
CREATE TABLE OneEightSix.UploadedFile
(
	Id int identity(1, 1) not null,
	FullPath nvarchar(max) not null,
	constraint PK_UploadedFile primary key clustered(Id)
)
GO
create table OneEightSix.Issue
(
	Id int identity(1, 1) not null,
	Title nvarchar(100) not null,
	Content nvarchar(max) null,
	constraint PK_Issue primary key clustered(Id)
)
GO
create table OneEightSix.IssueUploadedFile
(
	UploadedFileId int not null,
	IssueId int not null,
	constraint PK_IssueUploadedFile primary key clustered(UploadedFileId, IssueId),
	constraint FK_IssueUploadedFile_UploadedFile foreign key (UploadedFileId) references OneEightSix.UploadedFile(Id),
	constraint FK_IssueUploadedFile_Issue  foreign key (IssueId)  references OneEightSix.Issue(Id),
)
GO
-- Reverse engineer above and put into araxis, then run this, and reverse engineer again and compare with Araxis
alter table OneEightSix.Issue add ConsentDocumentId int null
alter table OneEightSix.Issue add constraint FK_Issue_UploadedFileConsentDocument foreign key(ConsentDocumentId) references OneEightSix.UploadedFile(Id)
GO



-- #47 v2.19.3 issue with many to many generation
CREATE SCHEMA Issue47
GO
--DROP TABLE Issue47.UserRoles
--DROP TABLE Issue47.[Role]
--DROP TABLE Issue47.Users
CREATE TABLE Issue47.Users
(
    UserId INT NOT NULL IDENTITY(1, 1),
    Name VARCHAR(10) NULL,
	CONSTRAINT PK_Issue47_Users PRIMARY KEY (UserId)
);
GO
CREATE TABLE Issue47.[Role]
(
    RoleId INT NOT NULL IDENTITY(1, 1),
    [Role] VARCHAR(10) NULL,
	CONSTRAINT PK_Issue47_Role PRIMARY KEY (RoleId)
);
GO
CREATE TABLE Issue47.UserRoles
(
    UserRoleId INT NOT NULL IDENTITY(1, 1),
    UserId INT NOT NULL,
    RoleId INT NOT NULL,
	CONSTRAINT PK_Issue47_UserRoles PRIMARY KEY (UserRoleId)
);
GO
ALTER TABLE Issue47.UserRoles ADD CONSTRAINT Issue47_UserRoles_userid FOREIGN KEY (UserId) REFERENCES Issue47.Users (UserId)
ALTER TABLE Issue47.UserRoles ADD CONSTRAINT Issue47_UserRoles_roleid FOREIGN KEY (RoleId) REFERENCES Issue47.[Role] (RoleId)
GO


-- #78 Default values for column mapped to enums fail
-- DROP TABLE EnumWithDefaultValue
CREATE TABLE EnumWithDefaultValue
(
    [Id] INT NOT NULL IDENTITY(1, 1),
    SomeEnum INT NOT NULL DEFAULT (1),
	CONSTRAINT PK_EnumWithDefaultValue PRIMARY KEY (Id)
);
GO

-- From mhwlng
-- user defined table types for stored procedures
CREATE TYPE UserDefinedTypeSample AS TABLE
(
    sensorid INT,
    utctimestamp DATETIME,
    value FLOAT
);
GO

CREATE PROCEDURE UserDefinedTypeSampleStoredProc (@a INT, @type UserDefinedTypeSample READONLY, @b INT)
AS
BEGIN
  -- todo
  RETURN (0)
END
GO



-- #173 ReturnModel is not generated for stored proc if xml.modify is used
CREATE procedure dbo.XmlDataV1
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
CREATE procedure dbo.XmlDataV2
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
-- EXEC dbo.XmlDataV1; EXEC dbo.XmlDataV2;
-- SET FMTONLY OFF; SET FMTONLY ON; exec dbo.XmlDataV1; SET FMTONLY OFF; SET FMTONLY OFF;
-- SET FMTONLY OFF; SET FMTONLY ON; exec dbo.XmlDataV2; SET FMTONLY OFF; SET FMTONLY OFF;


-- Case 156: Should get null instead of "NULL" for varchar DEFAULT NULL
-- DROP TABLE [DefaultCheckForNull]
CREATE TABLE [dbo].[DefaultCheckForNull]
(
    [Id] int NOT NULL IDENTITY(1,1),
    [DescUppercase] varchar(5) NULL DEFAULT NULL,
    [DescLowercase] varchar(5) NULL DEFAULT null,
    [DescMixedCase] varchar(5) NULL DEFAULT Null,
    [DescBrackets] varchar(5) NULL DEFAULT (((NULL))),
    [X1] varchar(255) NULL,
	CONSTRAINT PK_DefaultCheckForNull PRIMARY KEY (Id)
)
GO

-- From pinger. Reserved words in stored procs
CREATE PROC dbo.StupidStoredProcedureParams(@ReqType varchar(25),@Dept smallint,@Class smallint,@Item SMALLINT)
AS
BEGIN
  RETURN 0
END
GO
-- JasonMur Reserved words in stored procs
CREATE PROC dbo.StupidStoredProcedureParams2(@override varchar(25),@readonly smallint,@class smallint,@enum SMALLINT)
AS
BEGIN
  RETURN 0
END
GO


-- From Bitfiddler. tables with ALLCAPS that end in ies get messed up
--DROP TABLE BITFIDDLERALLCAPS
--DROP TABLE BitFiddlerCURRENCIES
--DROP TABLE BitFiddlerCATEGORIES
CREATE TABLE BitFiddlerCATEGORIES
(
    [Id] INT NOT NULL IDENTITY(1, 1),
	CONSTRAINT PK_BitFiddlerCATEGORIES PRIMARY KEY (Id)
);
GO
CREATE TABLE BitFiddlerCURRENCIES
(
    [Id] INT NOT NULL IDENTITY(1, 1),
	CONSTRAINT PK_BitFiddlerCURRENCIES PRIMARY KEY (Id)
);
GO
CREATE TABLE BITFIDDLERALLCAPS
(
    [Id] INT NOT NULL IDENTITY(1, 1),
	CONSTRAINT PK_BITFIDDLERALLCAPS PRIMARY KEY (Id)
);
GO


-- Mikael Johannesson
-- DROP TABLE Ticket
-- DROP TABLE AppUser
CREATE TABLE [dbo].[AppUser]
(
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	CONSTRAINT PK_AppUser PRIMARY KEY (Id)
)
GO
CREATE TABLE [dbo].[Ticket]
(
    [Id] BIGINT IDENTITY(1, 1) NOT NULL,
    [CreatedById] BIGINT NOT NULL,
    [ModifiedById] BIGINT NULL,
    CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Ticket_AppUser] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[AppUser] ([Id]),
    CONSTRAINT [FK_Ticket_AppUser1] FOREIGN KEY ([ModifiedById]) REFERENCES [dbo].[AppUser] ([Id])
);
GO
/*
When generating this the Ticket POCO will get navigators as
        public virtual AppUser AppUser_CreatedById { get; set; } // FK_Ticket_AppUser
        public virtual AppUser AppUser_ModifiedById { get; set; } // FK_Ticket_AppUser1

I want these to be named as
        public virtual AppUser CreatedBy { get; set; } // FK_Ticket_AppUser
        public virtual AppUser ModifiedBy { get; set; } // FK_Ticket_AppUser1
*/



-- From Silverfox
-- DROP TABLE Token
CREATE TABLE Token 
(
    Id UNIQUEIDENTIFIER NOT NULL DEFAULT (newsequentialid()) ROWGUIDCOL,
    Enabled bit NOT NULL,
	CONSTRAINT PK_Token PRIMARY KEY (Id)
)
GO
INSERT INTO Token (Enabled) VALUES(1)
GO
ALTER DATABASE [EfrpgTest] SET ANSI_PADDING ON;
GO
-- FROM David_Poco
--DROP TABLE CarToColour
--drop TABLE Car
--DROP TABLE Colour
CREATE TABLE Car
(
    Id INT NOT NULL,
    PrimaryColourId INT NOT NULL,
    CarMake VARCHAR(255) NOT NULL,
	CONSTRAINT PK_Car PRIMARY KEY (Id)
)
GO
SET ANSI_PADDING ON
GO
ALTER TABLE Car ADD 
    computed_column AS PrimaryColourId * 10,
    computed_column_persisted AS PrimaryColourId * 10 PERSISTED NOT NULL
GO
CREATE TABLE Colour
(
    Id INT NOT NULL,
    Name VARCHAR(255) NOT NULL,
	CONSTRAINT PK_Colour PRIMARY KEY (Id)
)
GO
CREATE TABLE CarToColour
(
    CarId INT NOT NULL,
    ColourId INT NOT NULL,
	CONSTRAINT PK_CarToColour PRIMARY KEY (CarId, ColourId)
)
GO
ALTER TABLE Car ADD CONSTRAINT CarPrimaryColourFK FOREIGN KEY (PrimaryColourId) REFERENCES Colour (Id);
ALTER TABLE CarToColour ADD CONSTRAINT CarToColour_CarId FOREIGN KEY (CarId) REFERENCES Car (Id);
ALTER TABLE CarToColour ADD CONSTRAINT CarToColour_ColourId FOREIGN KEY (ColourId) REFERENCES Colour (Id);
GO
INSERT INTO Colour (Id, Name) VALUES (1, 'Red'),(2,'Green'),(3,'Blue')
INSERT INTO Car (Id, PrimaryColourId, CarMake) VALUES (1, 1, 'Ford'),(2,3, 'Saab')
INSERT INTO CarToColour (CarId, ColourId) VALUES (1,2),(2,1),(2,2)
GO

CREATE PROCEDURE dbo.ColourPivot
AS
BEGIN
    SELECT pivot_table.Blue, pivot_table.Green, pivot_table.Red
    FROM
    (SELECT Id, Name FROM Colour) t
    PIVOT
    (
        MIN(Id)
        FOR Name IN (Red, Green, Blue)
    ) pivot_table;
END;
GO


-- drop table MultipleKeys
CREATE TABLE MultipleKeys
(
	UserId INT NOT NULL,
	FavouriteColourId INT NOT NULL CONSTRAINT UC_MultipleKeys_FavouriteColour UNIQUE,
	BestHolidayTypeId INT NOT NULL,
	BankId INT NOT NULL,
	CarId INT NOT NULL
)
GO
ALTER TABLE MultipleKeys ADD CONSTRAINT PK_MultipleKeys PRIMARY KEY CLUSTERED (UserId, FavouriteColourId, BestHolidayTypeId)
CREATE UNIQUE INDEX IX_MultipleKeys_Holiday_Bank ON MultipleKeys(BestHolidayTypeId, BankId)
CREATE INDEX IX_MultipleKeys_BestHolidayType ON MultipleKeys(BestHolidayTypeId)
GO


-- DROP TABLE DSOpe
CREATE TABLE DSOpe
(
	ID INT NOT NULL,
	[decimal_default] decimal(15, 2) NOT NULL,
	MyGuid UNIQUEIDENTIFIER NOT NULL DEFAULT ('9B7E1F67-5A81-4277-BC7D-06A3262A5C70'),
	[default] VARCHAR(10) NULL, -- reserved keyword
    [MyGuidBadDefault] UNIQUEIDENTIFIER CONSTRAINT [DF_MyGuidBadDefaul] DEFAULT (NULL) NULL,
	CONSTRAINT PK_DSOpe PRIMARY KEY (Id)
)
GO
ALTER TABLE DSOpe ADD CONSTRAINT [DF_DSOpe_MaxRabat] DEFAULT ((99.99)) FOR [decimal_default] 
GO

-- From George Lesser <george.lesser@harvesthcm.com>
CREATE PROCEDURE DSOpeProc
AS
BEGIN
    SET NOCOUNT ON;

    SELECT  ID,
            CONVERT(BIT, CASE WHEN [default] IS NOT NULL THEN 1 ELSE 0 END) AS Selected
    FROM    DSOpe;
END;
GO

--DROP TABLE tblOrderErrorsAB_
--DROP TABLE AB_OrderLinesAB_
--DROP TABLE AB_OrdersAB_
--DROP TABLE tblOrderErrors
--DROP TABLE tblOrderLines
--DROP TABLE tblOrders

CREATE TABLE tblOrders
(
    ID INT NOT NULL IDENTITY(1, 1),
    added DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT PK_tblOrders PRIMARY KEY (Id)
);
GO
CREATE TABLE tblOrderLines
(
    ID INT NOT NULL IDENTITY(1, 1),
    OrderID INT NOT NULL,
    sku VARCHAR(15) NULL,
	CONSTRAINT PK_tblOrderLines PRIMARY KEY (Id)
);
GO
CREATE TABLE tblOrderErrors
(
    ID INT NOT NULL IDENTITY(1, 1),
    error VARCHAR(50) NULL,
	CONSTRAINT PK_tblOrderErrors PRIMARY KEY (Id)
);
GO
CREATE TABLE AB_OrdersAB_
(
    ID INT NOT NULL IDENTITY(1, 1),
    added DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT PK_AB_OrdersAB PRIMARY KEY (Id)
);
GO
CREATE TABLE AB_OrderLinesAB_
(
    ID INT NOT NULL IDENTITY(1, 1),
    OrderID INT NOT NULL,
    sku VARCHAR(15) NULL,
	CONSTRAINT PK_AB_OrderLinesAB PRIMARY KEY (Id)
);
GO
CREATE TABLE tblOrderErrorsAB_
(
    ID INT NOT NULL IDENTITY(1, 1),
    error VARCHAR(50) NULL,
	CONSTRAINT PK_tblOrderErrorsAB PRIMARY KEY (Id)
);
GO
ALTER TABLE tblOrderLines ADD CONSTRAINT tblOrdersFK FOREIGN KEY (OrderID) REFERENCES tblOrders (id);
ALTER TABLE AB_OrderLinesAB_ ADD CONSTRAINT AB_OrderLinesAB_FK FOREIGN KEY (OrderID) REFERENCES AB_OrdersAB_ (id);
GO

-- Forign key with multiple fields
--DROP TABLE footer; DROP TABLE header;
CREATE TABLE header
(
    ID INT NOT NULL,
    anotherID INT NOT NULL,
    added DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT PK_header PRIMARY KEY (ID, anotherID)
);
GO
CREATE TABLE footer
(
    ID INT NOT NULL IDENTITY(1, 1),
    otherID INT NOT NULL,
    added DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT PK_footer PRIMARY KEY (Id)
);
GO
ALTER TABLE footer ADD CONSTRAINT fooderFK FOREIGN KEY (ID,otherID) REFERENCES header (ID,anotherID);
GO

INSERT INTO SmallDecimalTest (id, KoeffVed) VALUES (1, 0.1),(2, 0.2),(3, 0.3),(4, 0.4)
INSERT INTO PropertyTypesToAdd (id, dt_default, dt7) VALUES(1, GETDATE(), GETDATE()), (2, GETDATE(), GETDATE()), (3, GETDATE(), GETDATE())
INSERT INTO FkTest.SmallDecimalTestAttribute (FkID,[description]) VALUES  (1,'tattoo')
INSERT INTO [FFRS].[CV] ([BatchUID], [CVID], [CVName]) VALUES  (NEWID(),123, N'Hello world')
INSERT INTO DSOpe (ID,decimal_default,[default]) VALUES  (1,2,3)
GO

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'dcg')
	EXEC sys.sp_executesql N'CREATE SCHEMA [dcg]'
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dcg].[rov_ColumnDefinitions]'))
	DROP VIEW [dcg].[rov_ColumnDefinitions]
GO
SET ANSI_NULLS ON
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
CREATE PROCEDURE GetSmallDecimalTest(@maxId INT)
AS
BEGIN
	SET NOCOUNT ON;
	IF(@maxId IS NULL)
		SET @maxId = 999
	SELECT id, KoeffVed FROM SmallDecimalTest WHERE id <= @maxId
END
GO
CREATE PROCEDURE AddTwoValues(@a INT, @b INT)
AS
BEGIN
	SET NOCOUNT ON;
	RETURN @a + @b -- 0 indicates success, anything else is a failure
END
GO
CREATE PROCEDURE AddTwoValuesWithResult(@a INT, @b INT, @result INT OUTPUT, @result2 INT OUTPUT)
AS
BEGIN
	SET NOCOUNT ON;
	SET @result = @a + @b
	SET @result2 = @b - @a
END
GO
CREATE PROCEDURE MinTripSequenceStart(@minTripSequenceStartParam DATETIME2 OUTPUT)
AS
BEGIN
	SET NOCOUNT ON;
	SET @minTripSequenceStartParam = GETDATE()
END
GO
CREATE PROCEDURE MinTripSequenceStartNull(@minTripSequenceStartParam DATETIME2 OUTPUT)
AS
BEGIN
	SET NOCOUNT ON;
	SET @minTripSequenceStartParam = NULL
END
GO
CREATE PROCEDURE ConvertToString(@someValue INT, @someString VARCHAR(20) OUTPUT)
AS
BEGIN
	SET NOCOUNT ON;
	SET @someString = '*' + CAST(@someValue AS VARCHAR(20)) + '*'
END
GO

CREATE TABLE [dbo].[FinancialInstitutionOffice](
    [Code] [uniqueidentifier] NOT NULL,
    [FinancialInstitutionCode] [uniqueidentifier] NOT NULL,
    [OfficeName] [nvarchar](200) NULL
)
GO
ALTER TABLE [dbo].[FinancialInstitutionOffice] ADD CONSTRAINT [UniqueOfficeName_FinancialInstitutionOffice] UNIQUE NONCLUSTERED 
(
    [FinancialInstitutionCode] ASC,
    [OfficeName] ASC
)
GO

CREATE TABLE [dbo].[CodeObject](
    [codeObjectNo] [int] NOT NULL CONSTRAINT [DF__Object__objectNo__7E6CC920]  DEFAULT ((0)),
    [applicationNo] [int] NULL,
    [type] [int] NOT NULL,
    [eName] [nvarchar](250) NOT NULL,
    [aName] [nvarchar](250) NULL,
    [description] [nvarchar](250) NULL,
    [codeName] [nvarchar](250) NULL,
    [note] [nvarchar](250) NULL,
    [isObject] [bit] NOT NULL CONSTRAINT [DF__Object__isObject__7F60ED59]  DEFAULT ((0)),
    [versionNumber] [timestamp] NULL,
 CONSTRAINT [aaaaaObject_PK] PRIMARY KEY NONCLUSTERED 
(
    [codeObjectNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT  INTO CodeObject (codeObjectNo,applicationNo,type,eName,aName,description,codeName,note,isObject)
VALUES  (1,2,3,'fred','test','hello','some name','a note',0)
GO
EXEC sys.sp_addextendedproperty   
	@name = N'MS_Description',   
	@value = N'This is a test',
	@level0type = N'SCHEMA', @level0name = 'dbo',  
	@level1type = N'TABLE',  @level1name = 'CodeObject';
GO

CREATE TABLE Versioned
(
    Id INT NOT NULL IDENTITY(1, 1),
    [Version] ROWVERSION NOT NULL,
    Number INT NOT NULL,
	CONSTRAINT PK_Versioned PRIMARY KEY (Id)
);
GO
INSERT INTO Versioned (Number) VALUES (123);
INSERT INTO Versioned (Number) VALUES (456);
GO

CREATE TABLE VersionedNullable
(
    Id INT NOT NULL IDENTITY(1, 1),
    [Version] ROWVERSION NULL,
    Number INT NOT NULL,
	CONSTRAINT PK_VersionedNullable PRIMARY KEY (Id)
);
GO
INSERT INTO VersionedNullable (Number) VALUES (123);
INSERT INTO VersionedNullable (Number) VALUES (456);
GO

CREATE TABLE TimestampNotNull
(
    Id INT NOT NULL IDENTITY(1, 1),
    [Version] TIMESTAMP NOT NULL,
    Number INT NOT NULL,
	CONSTRAINT PK_TimestampNotNull PRIMARY KEY (Id)
);
GO
CREATE TABLE TimestampNullable
(
    Id INT NOT NULL IDENTITY(1, 1),
    [Version] TIMESTAMP NULL,
    Number INT NOT NULL,
	CONSTRAINT PK_TTimestampNullable PRIMARY KEY (Id)
);
GO

-- Table with sequences
CREATE SEQUENCE dbo.CountBy1 AS INT START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE dbo.CountByBigInt AS BIGINT START WITH 22 INCREMENT BY 234 MINVALUE 1 MAXVALUE 9876543 CYCLE;
CREATE SEQUENCE dbo.CountByTinyInt AS TINYINT START WITH 33 INCREMENT BY 3 NO MINVALUE NO CYCLE;
CREATE SEQUENCE dbo.CountBySmallInt AS SMALLINT START WITH 44 INCREMENT BY 456 NO MAXVALUE CYCLE;
CREATE SEQUENCE dbo.CountByDecimal AS DECIMAL START WITH 593 INCREMENT BY 82 MINVALUE 5 MAXVALUE 777777 CACHE 10;
CREATE SEQUENCE dbo.CountByNumeric AS NUMERIC START WITH 789 INCREMENT BY 987 MINVALUE 345 NO MAXVALUE NO CACHE;
-- DROP SEQUENCE dbo.CountBy1; DROP SEQUENCE dbo.CountByBigInt; DROP SEQUENCE dbo.CountByTinyInt; DROP SEQUENCE dbo.CountBySmallInt; DROP SEQUENCE dbo.CountByDecimal; DROP SEQUENCE dbo.CountByNumeric 
GO
--SELECT NEXT VALUE FOR dbo.CountBy1;
CREATE TABLE dbo.SequenceTest -- drop TABLE dbo.SequenceTest
(
    Id            INT         NOT NULL DEFAULT (NEXT VALUE FOR dbo.CountBy1),
    CntByBigInt   BIGINT      NOT NULL DEFAULT (NEXT VALUE FOR dbo.CountByBigInt),
    CntByTinyInt  TINYINT     NOT NULL DEFAULT (NEXT VALUE FOR dbo.CountByTinyInt),
    CntBySmallInt SMALLINT    NOT NULL DEFAULT (NEXT VALUE FOR dbo.CountBySmallInt),
    CntByDecimal  DECIMAL     NOT NULL DEFAULT (NEXT VALUE FOR dbo.CountByDecimal),
    CntByNumeric  NUMERIC     NOT NULL DEFAULT (NEXT VALUE FOR dbo.CountByNumeric),
    CONSTRAINT PK_SequenceTest PRIMARY KEY CLUSTERED (Id)
);
GO

--DROP VIEW AllColumnsNull
CREATE VIEW AllColumnsNull
AS
    SELECT  SUM(applicationNo) AS total,
            aName --ISNULL(aName, '') AS aName
    FROM    CodeObject
    GROUP BY aName
GO

CREATE PROCEDURE aSimpleExample
AS
BEGIN
    SET NOCOUNT ON;

    declare @test table (
        id int,
        [stuff] varchar(50)
    )
    insert into @test (id, [stuff]) values (1, 'some stuff'), (2, 'more stuff')

    select 1 as id, 'even more' as [stuff] into #test

    select * from @test
    select * from #test
	DROP TABLE #test
END
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

CREATE TABLE [dbo].[table with space](
    [id] [int] NOT NULL,
    CONSTRAINT PK_TableWithSpace PRIMARY KEY CLUSTERED ([id])
)
GO
CREATE TABLE [dbo].[table with space and in columns](
    [id value] [int] NOT NULL,
    CONSTRAINT PK_TableWithSpaceAndInColumns PRIMARY KEY CLUSTERED ([id value])
)
GO
CREATE TABLE [dbo].[TableWithSpaceInColumnOnly](
    [id value] [int] NOT NULL,
    CONSTRAINT PK_TableWithSpaceInColumnOnly PRIMARY KEY CLUSTERED ([id value])
)
GO
CREATE TABLE [dbo].[table mapping with space](
    [id] [int] NOT NULL,
    [id value] [int] NOT NULL,
    CONSTRAINT [PK_TableMappingWithSpace] PRIMARY KEY CLUSTERED ( [id],[id value] )
)
GO
ALTER TABLE [dbo].[table mapping with space] ADD CONSTRAINT space1FK FOREIGN KEY (id) REFERENCES [dbo].[table with space] (id);
ALTER TABLE [dbo].[table mapping with space] ADD CONSTRAINT space2FK FOREIGN KEY ([id value]) REFERENCES [dbo].[table with space and in columns] ([id value]);
GO
INSERT INTO [table with space] (id) VALUES  (1)
INSERT INTO [table with space and in columns] ([id value]) VALUES  (2)
INSERT INTO TableWithSpaceInColumnOnly ([id value]) VALUES  (3)
INSERT INTO [table mapping with space] (id, [id value]) VALUES  (1,2)
GO

CREATE PROCEDURE [dbo].[stp_test_underscore_test]
(
 @str_Date_FROM NVARCHAR(20),
 @str_date_to NVARCHAR(20)
)
AS
BEGIN
	SELECT  [codeObjectNo] AS code_object_no,
			[applicationNo] AS application_no
	FROM    [dbo].[CodeObject];
END;
GO

CREATE PROCEDURE [dbo].[stp test space test] (@a_val INT, @b_val INT)
AS
SELECT  [codeObjectNo] AS [code object no],
        [applicationNo] AS [application no]
FROM    [dbo].[CodeObject];
GO

CREATE PROCEDURE [dbo].[stp_nullable_params_test] (@a_val INT, @b_val int NULL)
AS
BEGIN
	SELECT  [codeObjectNo],[applicationNo]
	FROM    [dbo].[CodeObject];
END
GO
CREATE PROCEDURE [dbo].[stp_no_params_test]
AS
BEGIN
	SELECT  [codeObjectNo],[applicationNo]
	FROM    [dbo].[CodeObject];
END
GO
CREATE PROCEDURE [dbo].[stp_no_return_fields]
AS
    UPDATE [dbo].[CodeObject] SET type=4 WHERE codeObjectNo < 1000
GO
CREATE PROCEDURE [dbo].[stp_multiple_results]
AS
BEGIN
	SELECT codeObjectNo, applicationNo, [type], eName, aName, [description], codeName, note, isObject, versionNumber FROM [dbo].[CodeObject];
	SELECT Id, PrimaryColourId, CarMake, computed_column, computed_column_persisted FROM Car;
	SELECT Id,Name FROM Colour;
END;
GO
CREATE PROCEDURE [dbo].[stp_multiple_identical_results] (@someVar INT)
AS
	IF(@someVar > 5) BEGIN
		SELECT * FROM Colour;
	END ELSE BEGIN
		SELECT * FROM Colour;
	END
GO
CREATE PROCEDURE [dbo].[stp_multiple_results_with_params] (@first_val INT, @second_val int NULL)
AS
SELECT  [codeObjectNo],[applicationNo] FROM    [dbo].[CodeObject];
SELECT * FROM Colour;
GO
EXEC stp_multiple_results
GO

-- DROP PROC [dbo].[stp_multiple_multiple_results_with_params]
CREATE PROCEDURE [dbo].[stp_multiple_multiple_results_with_params] (@first_val INT, @second_val int NULL, @third_val INT)
AS
	SELECT  [codeObjectNo],[applicationNo] FROM    [dbo].[CodeObject];
	SELECT * FROM Colour;
	SELECT * FROM BatchTest;
	SELECT * FROM Burak1;
	SELECT * FROM Car;
	SELECT * FROM AB_OrderLinesAB_;
GO
DECLARE @procResult int
EXEC @procResult = [dbo].[stp_multiple_multiple_results_with_params] @first_val=1,@second_val=2,@third_val=3
PRINT @procResult
GO

-- DROP VIEW ComplexView
CREATE VIEW dbo.ComplexView
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

/*
SELECT * FROM ComplexView

EXEC [dbo].[stp_test_underscore_test] '',''
EXEC [dbo].[stp test space test] 0,0
EXEC [dbo].[stp_nullable_params_test] NULL,NULL
*/


DECLARE	@proc_result INT
EXEC	@proc_result = GetSmallDecimalTest @maxId = 3
SELECT	'proc_result' = @proc_result
GO
DECLARE	@proc_result int
EXEC @proc_result = AddTwoValues @a = 2, @b = 6
SELECT	@proc_result AS 'proc_result'
GO
DECLARE	@proc_result int, @result INT, @result2 INT
EXEC	@proc_result = AddTwoValuesWithResult @a = 3, @b = 7, @result = @result OUTPUT, @result2 = @result2 OUTPUT
SELECT	@result as '@result', @result2 AS '@result2'
SELECT	@proc_result AS 'proc_result'
GO
DECLARE	@proc_result INT
DECLARE @result VARCHAR(50)
EXEC @proc_result = ConvertToString @someValue = 56, @someString = @result OUTPUT
SELECT	@result as '@result'
SELECT	@proc_result AS 'proc_result'
GO

SELECT * FROM ColumnNameAndTypes
SELECT * FROM SmallDecimalTest
SELECT * FROM PropertyTypesToAdd
SELECT * FROM FkTest.SmallDecimalTestAttribute
SELECT * FROM DSOpe
SELECT * FROM [FFRS].[CV]
GO


-- From Naveen
CREATE TABLE [dbo].[CMS_File](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](100) NOT NULL,
	[FileDescription] [varchar](500) NOT NULL,
	[FileIdentifier] [varchar](100) NOT NULL,
	[ValidStartDate] [datetime] NULL,
	[ValidEndDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_CMS_File] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
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
CREATE TABLE [dbo].[CMS_FileTag](
	[FileId] [int] NOT NULL,
	[TagId] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CMS_FileTag] WITH NOCHECK ADD  CONSTRAINT [FK_CMS_FileTag_CMS_File] FOREIGN KEY([FileId]) REFERENCES [dbo].[CMS_File] ([FileId])
ALTER TABLE [dbo].[CMS_FileTag] CHECK CONSTRAINT [FK_CMS_FileTag_CMS_File]
ALTER TABLE [dbo].[CMS_FileTag] WITH NOCHECK ADD  CONSTRAINT [FK_CMS_FileTag_CMS_Tag] FOREIGN KEY([TagId]) REFERENCES [dbo].[CMS_Tag] ([TagId])
ALTER TABLE [dbo].[CMS_FileTag] CHECK CONSTRAINT [FK_CMS_FileTag_CMS_Tag]
GO

-- From 0v3rCl0ck
CREATE PROCEDURE [dbo].[proc_TestDecimalOutput]
    @PerfectNumber decimal(18,2) OUTPUT
AS
BEGIN
    SET @PerfectNumber = 10.35;
END
GO
CREATE PROCEDURE [dbo].[proc_TestDecimalOutputV2]
    @PerfectNumber decimal(12,8) OUTPUT
AS
BEGIN
    SET @PerfectNumber = 10.35;
END
GO
CREATE PROCEDURE [dbo].[proc_TestDecimalOutputV3Default]
    @PerfectNumber decimal OUTPUT
AS
BEGIN
    SET @PerfectNumber = 10.35;
END
GO



--- From WisdomGuidedByExperience
-- stored proc with nvarchar(max) parameter is missing size parameter
CREATE PROCEDURE [dbo].NvarcharTest @maxOutputParam NVARCHAR(MAX), @normalOutputParam NVARCHAR(20)
AS
BEGIN
    SET @maxOutputParam = 'hello'
    SET @normalOutputParam = 'world'
END
GO



-- From Leanard Lobel
CREATE TABLE hierarchy_test
(
    ID INT NOT NULL IDENTITY(1, 1),
    hid HIERARCHYID NOT NULL,
    CONSTRAINT PK_hierarchy_test PRIMARY KEY CLUSTERED (ID)
)
GO
INSERT INTO hierarchy_test (hid)
VALUES  ('/1/'),('/2/'),('/1/1/'),('/1/2/'),('/1/1/1/')
GO


-- My own testing
-- DROP TABLE ClientCreationState
CREATE TABLE ClientCreationState
(
	id UNIQUEIDENTIFIER NOT NULL,
	WebhookSetup BIT NOT NULL,
	AuthSetup BIT NOT NULL,
	AssignedCarrier BIT NOT NULL,
    CONSTRAINT PK_ClientCreationState PRIMARY KEY CLUSTERED (Id)
);
GO
-- SELECT * FROM ClientCreationState WHERE WebhookSetup=0 OR AuthSetup=0 OR AssignedCarrier=0
GO




-- From Raju_L
-- DROP PROC TestReturnString
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
EXEC [TestReturnString]
GO

-- #91 From igormono. FnWithCustomTableTypeReturnModel is not generated
CREATE TYPE [dbo].CustomTableType AS TABLE
(
    [id] [INT] NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
)
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



-- #595 from kinetic (Brian)
CREATE TABLE [User]
(
    ID             INT         IDENTITY(1, 1) NOT NULL,
    ExternalUserID VARCHAR(50) NULL,
    CONSTRAINT PK_User PRIMARY KEY CLUSTERED (ID ASC)
);
GO
CREATE TABLE User_Document
(
    ID              INT IDENTITY(1, 1) NOT NULL,
    UserID          INT NOT NULL,
    CreatedByUserID INT NOT NULL,
    CONSTRAINT PK_User_Document PRIMARY KEY CLUSTERED (ID ASC)
);
GO
ALTER TABLE User_Document WITH CHECK
ADD CONSTRAINT FK_User_Document_User FOREIGN KEY (UserID) REFERENCES [User] (ID) ON DELETE CASCADE;
GO
ALTER TABLE User_Document CHECK CONSTRAINT FK_User_Document_User;
GO
ALTER TABLE User_Document WITH CHECK
ADD CONSTRAINT FK_User_Document_User1 FOREIGN KEY (CreatedByUserID) REFERENCES [User] (ID);
GO
ALTER TABLE User_Document CHECK CONSTRAINT FK_User_Document_User1;
GO


-- From Jon Califf
CREATE TABLE dbo.Blah
(
 BlahID INT NOT NULL IDENTITY(1, 1),
 CONSTRAINT PK_Blah PRIMARY KEY CLUSTERED (BlahID)
)

CREATE TABLE dbo.Blarg
(
 BlargID INT NOT NULL IDENTITY(1, 1),
 CONSTRAINT PK_Blarg PRIMARY KEY CLUSTERED (BlargID)
)

CREATE TABLE dbo.BlahBlargLink
(
 BlahID INT NOT NULL,
 BlargID INT NOT NULL,
 CONSTRAINT [PK_BlahBlargLink] PRIMARY KEY CLUSTERED (BlahID, BlargID),
 CONSTRAINT FK_BlahBlargLink_Blah FOREIGN KEY (BlahID) REFERENCES Blah (BlahID),
 CONSTRAINT FK_BlahBlargLink_Blarg FOREIGN KEY (BlargID) REFERENCES dbo.Blarg (BlargID)
)

CREATE TABLE dbo.BlahBlahLink
(
 BlahID INT NOT NULL,
 BlahID2 INT NOT NULL,
 CONSTRAINT [PK_BlahBlahLink] PRIMARY KEY CLUSTERED (BlahID, BlahID2),
 CONSTRAINT FK_BlahBlahLink_Blah FOREIGN KEY (BlahID) REFERENCES dbo.Blah (BlahID),
 CONSTRAINT FK_BlahBlahLink_Blah2 FOREIGN KEY (BlahID2) REFERENCES Blah (BlahID)
)
GO

CREATE TABLE dbo.BlahBlahLink_readonly
(
 BlahID INT NOT NULL,
 BlahID2 INT NOT NULL,
 [RowVersion] TIMESTAMP NULL, -- readonly timestamp
 id INT NOT NULL IDENTITY(1, 1),  -- readonly identity
 id2 as (id+100) -- readonly computed
 CONSTRAINT [PK_BlahBlahLink_ro] PRIMARY KEY CLUSTERED (BlahID, BlahID2),
 CONSTRAINT FK_BlahBlahLink_Blah_ro FOREIGN KEY (BlahID) REFERENCES dbo.Blah (BlahID),
 CONSTRAINT FK_BlahBlahLink_Blah_ro2 FOREIGN KEY (BlahID2) REFERENCES Blah (BlahID)
)
GO
CREATE TABLE dbo.BlahBlahLink_v2
(
 BlahID INT NOT NULL,
 BlahID2 INT NOT NULL,
 dummy1 INT NULL,
 dummy2 INT NOT NULL,
 hello INT NOT NULL,
 CONSTRAINT [PK_BlahBlahLinkv2_ro] PRIMARY KEY CLUSTERED (BlahID, BlahID2),
 CONSTRAINT FK_BlahBlahLinkv2_Blah_ro FOREIGN KEY (BlahID) REFERENCES dbo.Blah (BlahID) ON DELETE CASCADE,
 CONSTRAINT FK_BlahBlahLinkv2_Blah_ro2 FOREIGN KEY (BlahID2) REFERENCES Blah (BlahID)
)
GO



-- Multiple foreign keys [InverseProperty] test
CREATE TABLE Person
(
    Id INT NOT NULL IDENTITY(1, 1),
    [Name] VARCHAR(50) NOT NULL,
    CONSTRAINT PK_Person PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE TABLE PersonPosts
(
    Id INT NOT NULL IDENTITY(1, 1),
    Title VARCHAR(20) NOT NULL,
    Body VARCHAR(100) NOT NULL,
    CreatedBy INT NOT NULL,
    UpdatedBy INT NOT NULL,
    CONSTRAINT PK_PersonPosts PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_PersonPosts_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Person (Id),
    CONSTRAINT FK_PersonPosts_UpdatedBy FOREIGN KEY (UpdatedBy) REFERENCES Person (Id)
)
GO





-- Case 142 - no fk created (This relationship has multiple composite keys) Relationship.DoNotUse
CREATE TABLE [dbo].[AAREF]
(
 [C1] [INT] NOT NULL,
 [C2] [INT] NOT NULL,
 [CreatedUTC] [DATETIME2](7) NOT NULL,
 CONSTRAINT [PK_AREF] PRIMARY KEY CLUSTERED ([C1] ASC, [C2] ASC)
)
GO
CREATE TABLE [dbo].[A]
(
 [AId] [INT] IDENTITY(1, 1) NOT NULL,
 [C1] [INT] NOT NULL,
 [C2] [INT] NOT NULL,
 CONSTRAINT [PK_A] PRIMARY KEY CLUSTERED ([AId] ASC)
)
GO
ALTER TABLE [dbo].[A] WITH CHECK ADD CONSTRAINT [FK_A_A] FOREIGN KEY([C1], [C2]) REFERENCES [dbo].[AAREF] ([C1], [C2])
GO


CREATE TABLE [table with duplicate column names]
(
 [id] [INT] IDENTITY(1, 1) NOT NULL,
 [user_id] [INT] NOT NULL,
 [UserId] [INT] NOT NULL,
 [User Id] [INT] NOT NULL,
 [User  Id] [INT] NOT NULL,
 [user__id] [INT] NOT NULL,
 CONSTRAINT PK_TableWithDuplicateColumnNames PRIMARY KEY CLUSTERED (Id)
)
GO


-- DROP TABLE pk_ordinal_test
CREATE TABLE pk_ordinal_test
(
    C1 INT NOT NULL,
    C2 INT NOT NULL,
    C3 INT NOT NULL,
    CONSTRAINT PK_pk_ordinal_test PRIMARY KEY (C3,C1)
);
GO
-- SELECT * FROM pk_ordinal_test



CREATE TABLE EventProcessor
(
	Id INT NOT NULL IDENTITY(1,1),
	[Name] VARCHAR(200) NOT NULL,
	[Description] VARCHAR(512) NULL,
	[EndpointAddress] VARCHAR(512) NULL,
	[Enabled] BIT NOT NULL,
	CONSTRAINT [PK_EventProcessor] PRIMARY KEY CLUSTERED (Id)
);
GO
CREATE TABLE EventProcessorEventFilter
(
	Id INT NOT NULL IDENTITY(1,1),
	EventProcessorId INT NOT NULL,
	WantedEventId INT NOT NULL,
	CONSTRAINT [PK_EventProcessorEventFilter] PRIMARY KEY CLUSTERED (Id)
);
GO
CREATE UNIQUE INDEX [IX_EventProcessorEventFilter] ON EventProcessorEventFilter (EventProcessorId, WantedEventId);
ALTER TABLE EventProcessorEventFilter ADD CONSTRAINT [FK_EventProcessorEventFilter__EventProcessor] FOREIGN KEY (EventProcessorId) REFERENCES EventProcessor (Id);
GO


-- #769 Using stored procedure OUT parameters with multiple result sets
CREATE PROCEDURE CheckIfApplicationIsComplete
    @ApplicationId INT, @IsApplicationComplete BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF (@ApplicationId < 10)
    BEGIN
        SET @IsApplicationComplete = 0;
        SELECT 'Application' [Key], 'Not complete' [Value];
    END
    ELSE
    BEGIN
        SET @IsApplicationComplete = 1;
        SELECT 'Application' [Key], 'Complete' [Value];
    END
END;
GO

-- #782 support for memory optimised tables
CREATE TABLE dbo.ThisIsMemoryOptimised
(
    Id INT NOT NULL IDENTITY(1, 1),
    Description VARCHAR(20) NOT NULL,
    CONSTRAINT PK_ThisIsMemoryOptimised PRIMARY KEY NONCLUSTERED (Id)
)
WITH (MEMORY_OPTIMIZED = ON, DURABILITY = SCHEMA_AND_DATA);
GO
