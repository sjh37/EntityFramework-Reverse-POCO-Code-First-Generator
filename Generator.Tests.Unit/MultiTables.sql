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

/* To see what settings you have
SELECT * FROM MultiContext.Context;
SELECT * FROM MultiContext.[Table];
SELECT * FROM MultiContext.[Column];
SELECT * FROM MultiContext.StoredProcedure;
SELECT * FROM MultiContext.[Function];
SELECT * FROM MultiContext.Enumeration;
SELECT * FROM MultiContext.ForeignKey;
*/