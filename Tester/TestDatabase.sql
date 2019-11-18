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
ALTER DATABASE [EfrpgTest_Settings] SET  DISABLE_BROKER
ALTER DATABASE [EfrpgTest_Settings] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
ALTER DATABASE [EfrpgTest_Settings] SET DATE_CORRELATION_OPTIMIZATION OFF
ALTER DATABASE [EfrpgTest_Settings] SET PARAMETERIZATION SIMPLE
ALTER DATABASE [EfrpgTest_Settings] SET  READ_WRITE
ALTER DATABASE [EfrpgTest_Settings] SET RECOVERY SIMPLE
ALTER DATABASE [EfrpgTest_Settings] SET  MULTI_USER
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

-- You can add extra fields to this table. All columns will be read in and stored in a Dictionary<string,object>() for you to access and process.
CREATE TABLE MultiContext.Context
(
    Id           INT           NOT NULL IDENTITY(1, 1),
    Name         NVARCHAR(128) NOT NULL,
    Namespace    NVARCHAR(128) NULL,
    Description  NVARCHAR(128) NULL,
    BaseSchema   NVARCHAR(128) NULL,    -- Default to use if not specified for an object
    TemplatePath NVARCHAR(500) NULL,
    Filename     NVARCHAR(128) NULL, -- If Filename == NULL, then use Name, else use Filename as the name of the file
    CONSTRAINT PK_Context PRIMARY KEY CLUSTERED (Id)
);
GO

ALTER TABLE MultiContext.Context DROP COLUMN BaseDatabase

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
    Name       NVARCHAR(128) NOT NULL,  -- Enum to generate. e.g. "DaysOfWeek" would result in "public enum DaysOfWeek {...}"
    [Table]    NVARCHAR(128) NOT NULL,  -- Database table containing enum values. e.g. "DaysOfWeek"
    NameField  NVARCHAR(128) NOT NULL,  -- Column containing the name for the enum. e.g. "TypeName"
    ValueField NVARCHAR(128) NOT NULL,  -- Column containing the values for the enum. e.g. "TypeId"
    ContextId  INT           NOT NULL,
    CONSTRAINT PK_Enumeration PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_Enumeration_Context_ContextId FOREIGN KEY (ContextId) REFERENCES MultiContext.Context (Id) ON DELETE NO ACTION
);
GO

-- You can add extra fields to this table. All columns will be read in and stored in a Dictionary<string,object>() for you to access and process.
CREATE TABLE MultiContext.[Function]
(
    Id        INT           NOT NULL IDENTITY(1, 1),
    Name      NVARCHAR(128) NOT NULL,
    DbName    NVARCHAR(128) NULL,   -- [optional] Name of function in database. Specify only if the db function name is different from the "Name" property.
    ContextId INT           NOT NULL,
    CONSTRAINT PK_Function PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_Function_Context_ContextId FOREIGN KEY (ContextId) REFERENCES MultiContext.Context (Id) ON DELETE NO ACTION
);
GO

-- You can add extra fields to this table. All columns will be read in and stored in a Dictionary<string,object>() for you to access and process.
CREATE TABLE MultiContext.StoredProcedure
(
    Id          INT           NOT NULL IDENTITY(1, 1),
    Name        NVARCHAR(128) NOT NULL,
    DbName      NVARCHAR(128) NULL, -- [optional] Name of stored proc in database. Specify only if the db stored proc name is different from the "Name" property.
    ReturnModel NVARCHAR(128) NULL, -- [optional] Specify a return model for stored proc
    ContextId   INT           NOT NULL,
    CONSTRAINT PK_StoredProcedure PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_StoredProcedure_Context_ContextId FOREIGN KEY (ContextId) REFERENCES MultiContext.Context (Id) ON DELETE NO ACTION
);
GO

-- You can add extra fields to this table. All columns will be read in and stored in a Dictionary<string,object>() for you to access and process.
CREATE TABLE MultiContext.[Table]
(
    Id            INT           NOT NULL IDENTITY(1, 1),
    Name          NVARCHAR(128) NOT NULL,
    Description   NVARCHAR(128) NULL,    -- [optional] Comment added to table class
    PluralName    NVARCHAR(128) NULL,    -- [optional] Override auto-plural name
    DbName        NVARCHAR(128) NULL,    -- [optional] Name of table in database. Specify only if the db table name is different from the "Name" property.
    ContextId     INT           NOT NULL,
    Attributes    NVARCHAR(500) NULL,    -- [optional] Use a tilda ~ delimited list of attributes to add to this table property. e.g. [CustomSecurity(Security.ReadOnly)]~[AnotherAttribute]~[Etc]
                                         --            The tilda ~ delimiter used in Attributes can be changed if you set Settings.MultiContextAttributeDelimiter = '~'; to something else.
    DbSetModifier NVARCHAR(128) NULL,    -- [optional] Will override setting of table.DbSetModifier. Default is "public".

    CONSTRAINT PK_Table PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_Table_Context_ContextId FOREIGN KEY (ContextId) REFERENCES MultiContext.Context (Id) ON DELETE NO ACTION
);
GO

-- You can add extra fields to this table. All columns will be read in and stored in a Dictionary<string,object>() for you to access and process.
CREATE TABLE MultiContext.[Column]
(
    Id               INT           NOT NULL IDENTITY(1, 1),
    Name             NVARCHAR(128) NOT NULL,
    DbName           NVARCHAR(128) NULL,    -- [optional] Name of column in database. Specify only if the db column name is different from the "Name" property.
    IsPrimaryKey     BIT           NULL,    -- [optional] Useful for views as views don't have primary keys.
    OverrideModifier BIT           NULL,    -- [optional] Adds "override" modifier.
    EnumType         NVARCHAR(128) NULL,    -- [optional] Use enum type instead of data type
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

/*
-- If you need to reset the data
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
	   (N'CherryDbContext', N'Testing cherries', N'dbo', "Cherry"),
	   (N'DamsonDbContext', N'Testing Damson plums', NULL, "Plum");
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
VALUES (N'ColumnNames', NULL, NULL, NULL, @id);
INSERT INTO MultiContext.[Column] (Name, DbName, IsPrimaryKey, OverrideModifier, EnumType, TableId)
VALUES (N'Dollar', N'$' ,NULL,0, NULL,          (SELECT id FROM MultiContext.[Table] WHERE ContextId=@id AND Name=N'ColumnNames')),
       (N'Pound', N'[£]',NULL,0, NULL,          (SELECT id FROM MultiContext.[Table] WHERE ContextId=@id AND Name=N'ColumnNames')),
	   (N'StaticField', N'static',NULL,0, NULL, (SELECT id FROM MultiContext.[Table] WHERE ContextId=@id AND Name=N'ColumnNames')),
	   (N'Day', N'readonly',NULL,0, NULL,       (SELECT id FROM MultiContext.[Table] WHERE ContextId=@id AND Name=N'ColumnNames'));
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
INSERT INTO MultiContext.ForeignKey (ContextId, ConstraintName, ParentName, ChildName, PkSchema, PkTableName, PkColumn, FkSchema, FkTableName, FkColumn, Ordinal, CascadeOnDelete, IsNotEnforced)
VALUES (@id, N'CustomNameForForeignKey', N'ParentFkName', N'ChildFkName', N'dbo', N'NoPrimaryKeys', N'Description', N'Synonyms', N'Parent', N'ParentName', 1, 0, 0);
GO

UPDATE MultiContext.[Column] SET Attributes=N'[ExampleForTesting("abc")]~[CustomRequired]' WHERE Name=N'Dollar'
UPDATE MultiContext.[Column] SET Attributes=N'[ExampleForTesting("def")]~[CustomSecurity(SecurityEnum.Readonly)]' WHERE Name=N'Pound'
GO

-- Test to make sure all optional fields are read in and stored in dictionary
ALTER TABLE MultiContext.[Column] ADD Test VARCHAR(10) NULL;
ALTER TABLE MultiContext.[Column] ADD DummyInt int NULL;
ALTER TABLE MultiContext.[Column] ADD date_of_birth DATETIME NULL;
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

SELECT Id,
       ContextId,
       ConstraintName,
       PkSchema,
       PkTableName,
       PkColumn,
       FkSchema,
       FkTableName,
       FkColumn,
       Ordinal,
       CascadeOnDelete,
       IsNotEnforced FROM MultiContext.ForeignKey;


USE [EfrpgTest]
GO

-- Enumeration generation tests
CREATE SCHEMA EnumTest
GO
-- Enum inside schema
CREATE TABLE EnumTest.DaysOfWeek
(
	TypeName VARCHAR(50) NOT NULL,
	TypeId INT NOT NULL
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

CREATE FUNCTION udfNetSale (@quantity INT, @list_price DEC(10, 2), @discount DEC(4, 2))
RETURNS DEC(10, 2)
AS
BEGIN
    RETURN @quantity * @list_price * (1 - @discount);
END;
GO

CREATE TABLE ColumnNames
(
    [$] INT NOT null,
    CONSTRAINT PK_ColumnNames PRIMARY KEY CLUSTERED ([$] ASC),

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
    [adouble] FLOAT NULL DEFAULT (999.), -- #284 need default as 999.0
    [adecimal] DECIMAL(19, 4) NULL
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

CREATE SYNONYM [Synonyms].[Parent] FOR [EfrpgTest].[Synonyms].[Parent]
CREATE SYNONYM [Synonyms].[Child] FOR [EfrpgTest].[Synonyms].[Child]
CREATE SYNONYM [Synonyms].[SimpleStoredProc] FOR [EfrpgTest].[Synonyms].[SimpleStoredProc]
GO

-- Create table with multiple FK's
CREATE TABLE dbo.UserInfo
(
    Id INT IDENTITY(1, 1) NOT NULL,
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