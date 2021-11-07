IF SCHEMA_ID(N'Alpha') IS NULL EXEC(N'CREATE SCHEMA [Alpha];');
GO


IF SCHEMA_ID(N'App') IS NULL EXEC(N'CREATE SCHEMA [App];');
GO


IF SCHEMA_ID(N'Beta') IS NULL EXEC(N'CREATE SCHEMA [Beta];');
GO


IF SCHEMA_ID(N'FFRS') IS NULL EXEC(N'CREATE SCHEMA [FFRS];');
GO


IF SCHEMA_ID(N'Issue47') IS NULL EXEC(N'CREATE SCHEMA [Issue47];');
GO


IF SCHEMA_ID(N'Kate') IS NULL EXEC(N'CREATE SCHEMA [Kate];');
GO


IF SCHEMA_ID(N'OneEightSix') IS NULL EXEC(N'CREATE SCHEMA [OneEightSix];');
GO


CREATE SEQUENCE [dbo].[CountBy1] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [dbo].[CountByBigInt] START WITH 22 INCREMENT BY 234 MINVALUE 1 MAXVALUE 9876543 CYCLE;
GO


CREATE SEQUENCE [dbo].[CountByDecimal] AS decimal START WITH 593 INCREMENT BY 82 MINVALUE 5 MAXVALUE 777777 NO CYCLE;
GO


CREATE SEQUENCE [dbo].[CountByNumeric] AS decimal START WITH 789 INCREMENT BY 987 MINVALUE 345 MAXVALUE 999999999999999999 NO CYCLE;
GO


CREATE SEQUENCE [dbo].[CountBySmallInt] AS smallint START WITH 44 INCREMENT BY 456 NO MINVALUE NO MAXVALUE CYCLE;
GO


CREATE SEQUENCE [dbo].[CountByTinyInt] AS tinyint START WITH 33 INCREMENT BY 3 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE TABLE [DboProcDataFromFfrsAndDboReturnModel] (
    [Id] int NOT NULL,
    [PrimaryColourId] int NOT NULL,
    [CarMake] nvarchar(max) NULL,
    [CVName] nvarchar(max) NULL
);
GO


CREATE TABLE [DboProcDataFromFfrsReturnModel] (
    [BatchUID] uniqueidentifier NOT NULL,
    [CVID] int NOT NULL,
    [CVName] nvarchar(max) NULL
);
GO


CREATE TABLE [DsOpeProcReturnModel] (
    [ID] int NOT NULL,
    [Selected] bit NULL
);
GO


CREATE TABLE [FFRS_CvDataReturnModel] (
    [BatchUID] uniqueidentifier NOT NULL,
    [CVID] int NOT NULL,
    [CVName] nvarchar(max) NULL
);
GO


CREATE TABLE [FFRS_DataFromDboAndFfrsReturnModel] (
    [Id] int NOT NULL,
    [PrimaryColourId] int NOT NULL,
    [CarMake] nvarchar(max) NULL,
    [CVName] nvarchar(max) NULL
);
GO


CREATE TABLE [FFRS_DataFromDboReturnModel] (
    [Id] int NOT NULL,
    [PrimaryColourId] int NOT NULL,
    [CarMake] nvarchar(max) NULL
);
GO


CREATE TABLE [GetScreamAndShoutReturnModel] (
    [id] int NOT NULL,
    [KoeffVed] decimal(18,2) NULL
);
GO


CREATE TABLE [Kate_HelloReturnModel] (
    [static] int NULL,
    [readonly] int NULL
);
GO


CREATE TABLE [SpatialTypesNoParamsReturnModel] (
    [Dollar] int NOT NULL,
    [someDate] datetime2 NOT NULL,
    [GeographyType] geography NULL,
    [GeometryType] geometry NULL
);
GO


CREATE TABLE [StpNoParamsTestReturnModel] (
    [codeObjectNo] int NOT NULL,
    [applicationNo] int NULL
);
GO


CREATE TABLE [StpNullableParamsTestReturnModel] (
    [codeObjectNo] int NOT NULL,
    [applicationNo] int NULL
);
GO


CREATE TABLE [StpTestReturnModel] (
    [codeObjectNo] int NOT NULL,
    [applicationNo] int NULL,
    [type] int NOT NULL,
    [eName] nvarchar(max) NULL,
    [aName] nvarchar(max) NULL,
    [description] nvarchar(max) NULL,
    [codeName] nvarchar(max) NULL,
    [note] nvarchar(max) NULL,
    [isObject] bit NOT NULL,
    [versionNumber] varbinary(max) NULL
);
GO


CREATE TABLE [StpTestUnderscoreTestReturnModel] (
    [code_object_no] int NOT NULL,
    [application_no] int NULL
);
GO


CREATE TABLE [TestReturnStringReturnModel] (
    [error] nvarchar(max) NULL
);
GO


CREATE TABLE [XmlDataV1ReturnModel] (
    [Column1] datetime2 NULL,
    [Column2] nvarchar(max) NULL
);
GO


CREATE TABLE [Alpha].[workflow] (
    [Id] int NOT NULL IDENTITY,
    [Description] varchar(10) NULL,
    CONSTRAINT [PK__workflow__3214EC072804087E] PRIMARY KEY CLUSTERED ([Id])
);
GO


CREATE TABLE [App].[UserFacilityServiceRole] (
    [userId] int NOT NULL,
    [appId] int NOT NULL,
    [fsrId] int NOT NULL,
    CONSTRAINT [PK_UserFacilityServiceRole] PRIMARY KEY ([userId], [appId], [fsrId])
);
GO


CREATE TABLE [Beta].[workflow] (
    [Id] int NOT NULL IDENTITY,
    [Description] varchar(10) NULL,
    CONSTRAINT [PK__workflow__3214EC07906CBEB9] PRIMARY KEY CLUSTERED ([Id])
);
GO


CREATE TABLE [dbo].[AAREF] (
    [C1] int NOT NULL,
    [C2] int NOT NULL,
    [CreatedUTC] datetime2 NOT NULL,
    CONSTRAINT [PK_AREF] PRIMARY KEY CLUSTERED ([C1], [C2])
);
GO


CREATE TABLE [dbo].[AB_OrdersAB_] (
    [ID] int NOT NULL IDENTITY,
    [added] datetime NOT NULL,
    CONSTRAINT [PK__AB_Order__3214EC277606C4E4] PRIMARY KEY CLUSTERED ([ID])
);
GO


CREATE TABLE [dbo].[AppUser] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    CONSTRAINT [PK__AppUser__3214EC074EE50760] PRIMARY KEY CLUSTERED ([Id])
);
GO


CREATE TABLE [dbo].[BatchTest] (
    [code] nvarchar(8) NOT NULL,
    CONSTRAINT [PK__BatchTes__357D4CF8F557E292] PRIMARY KEY CLUSTERED ([code])
);
GO


CREATE TABLE [dbo].[BITFIDDLERALLCAPS] (
    [Id] int NOT NULL IDENTITY,
    CONSTRAINT [PK__BITFIDDL__3214EC073CF127E7] PRIMARY KEY CLUSTERED ([Id])
);
GO


CREATE TABLE [dbo].[BitFiddlerCATEGORIES] (
    [Id] int NOT NULL IDENTITY,
    CONSTRAINT [PK__BitFiddl__3214EC07A08CA919] PRIMARY KEY CLUSTERED ([Id])
);
GO


CREATE TABLE [dbo].[BitFiddlerCURRENCIES] (
    [Id] int NOT NULL IDENTITY,
    CONSTRAINT [PK__BitFiddl__3214EC07D56DB345] PRIMARY KEY CLUSTERED ([Id])
);
GO


CREATE TABLE [dbo].[Blah] (
    [BlahID] int NOT NULL IDENTITY,
    CONSTRAINT [PK_Blah] PRIMARY KEY CLUSTERED ([BlahID])
);
GO


CREATE TABLE [dbo].[Blarg] (
    [BlargID] int NOT NULL IDENTITY,
    CONSTRAINT [PK_Blarg] PRIMARY KEY CLUSTERED ([BlargID])
);
GO


CREATE TABLE [dbo].[BringTheAction] (
    [$] int NOT NULL,
    [%] int NULL,
    [£] int NULL,
    [&fred$] int NULL,
    [abc/\] int NULL,
    [joe.bloggs] int NULL,
    [simon-hughes] int NULL,
    [description] varchar(20) NOT NULL,
    [someDate] datetime2 NOT NULL,
    [Obs] varchar(50) NULL,
    [Obs1] varchar(50) NULL,
    [Obs2] varchar(50) NULL,
    [Obs3] varchar(50) NULL,
    [static] int NULL,
    [readonly] int NULL,
    [123Hi] int NULL,
    [areal] real NULL,
    [afloat] float NULL,
    [afloat8] real NULL,
    [afloat20] real NULL,
    [afloat24] real NULL,
    [afloat53] float NULL,
    [adecimal] decimal(18,0) NULL,
    [adecimal_19_4] decimal(19,4) NULL,
    [adecimal_10_3] decimal(10,3) NULL,
    [anumeric] numeric(18,0) NULL,
    [anumeric_5_2] numeric(5,2) NULL,
    [anumeric_11_3] numeric(11,3) NULL,
    [amoney] money NULL,
    [asmallmoney] smallmoney NULL,
    [brandon] int NULL,
    CONSTRAINT [PK__BringThe__3BD01849891AAF49] PRIMARY KEY CLUSTERED ([$])
);
GO


CREATE TABLE [dbo].[Burak2] (
    [id] bigint NOT NULL IDENTITY,
    [num] bigint NOT NULL,
    CONSTRAINT [PK_Burak2] PRIMARY KEY CLUSTERED ([id]),
    CONSTRAINT [AK_Burak2_id_num] UNIQUE ([id], [num])
);
GO


CREATE TABLE [dbo].[CalculatedColumnNotNull] (
    [ID] int NOT NULL IDENTITY,
    [Type] tinyint NOT NULL,
    [IsCalendar] bit NOT NULL,
    [IsUtilization] bit NOT NULL,
    CONSTRAINT [PK_CalculatedColumnNotNull] PRIMARY KEY ([ID])
);
GO


CREATE TABLE [dbo].[ClientCreationState] (
    [id] uniqueidentifier NOT NULL,
    [WebhookSetup] bit NOT NULL,
    [AuthSetup] bit NOT NULL,
    [AssignedCarrier] bit NOT NULL,
    CONSTRAINT [PK__ClientCr__3213E83F95DD2206] PRIMARY KEY CLUSTERED ([id])
);
GO


CREATE TABLE [dbo].[CMS_File] (
    [FileId] int NOT NULL IDENTITY,
    [FileName] nvarchar(100) NOT NULL,
    [FileDescription] varchar(500) NOT NULL,
    [FileIdentifier] varchar(100) NOT NULL,
    [ValidStartDate] datetime NULL,
    [ValidEndDate] datetime NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_CMS_Form] PRIMARY KEY CLUSTERED ([FileId])
);
GO


CREATE TABLE [dbo].[CMS_Tag] (
    [TagId] int NOT NULL IDENTITY,
    [TagName] varchar(100) NOT NULL,
    CONSTRAINT [PK_CMS_Tag] PRIMARY KEY CLUSTERED ([TagId])
);
GO


CREATE TABLE [dbo].[CODE_PARAM_MeetingTopicDetailSource] (
    [ID] int NOT NULL IDENTITY,
    [Code] nvarchar(5) NOT NULL,
    [Label] nvarchar(50) NULL,
    [LabelENG] nvarchar(50) NULL,
    [LabelESP] nvarchar(50) NULL,
    [LabelFRA] nvarchar(50) NULL,
    [DateCreated] datetime NOT NULL,
    [DateChanged] datetime NULL,
    CONSTRAINT [PK_CODE_PARAM_MeetingTopicDetailSource] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [AK_CODE_PARAM_MeetingTopicDetailSource_Code] UNIQUE ([Code])
);
GO


CREATE TABLE [dbo].[CodeObject] (
    [codeObjectNo] int NOT NULL,
    [applicationNo] int NULL,
    [type] int NOT NULL,
    [eName] nvarchar(250) NOT NULL,
    [aName] nvarchar(250) NULL,
    [description] nvarchar(250) NULL,
    [codeName] nvarchar(250) NULL,
    [note] nvarchar(250) NULL,
    [isObject] bit NOT NULL,
    [versionNumber] timestamp NULL,
    CONSTRAINT [aaaaaObject_PK] PRIMARY KEY ([codeObjectNo])
);
GO


CREATE TABLE [dbo].[Colour] (
    [Id] int NOT NULL,
    [Name] varchar(255) NOT NULL,
    CONSTRAINT [PK__Colour__3214EC07379EAA64] PRIMARY KEY CLUSTERED ([Id])
);
GO


CREATE TABLE [dbo].[CompanyGroup] (
    [cogID] int NOT NULL IDENTITY,
    [cogCompanyGroupName] nvarchar(50) NULL,
    [cogValidFrom] datetime NULL,
    [cogValidTo] datetime NULL,
    CONSTRAINT [PK_CompanyGroup] PRIMARY KEY CLUSTERED ([cogID])
);
GO


CREATE TABLE [dbo].[Country] (
    [CountryID] int NOT NULL IDENTITY,
    [Code] varchar(12) NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([CountryID])
);
GO


CREATE TABLE [dbo].[DateTimeDefaultTest] (
    [Id] int NOT NULL,
    [CreatedDate] datetimeoffset NULL,
    CONSTRAINT [PK_DateTimeDefaultTest] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [dbo].[DefaultCheckForNull] (
    [Id] int NOT NULL IDENTITY,
    [DescUppercase] varchar(5) NULL,
    [DescLowercase] varchar(5) NULL,
    [DescMixedCase] varchar(5) NULL,
    [DescBrackets] varchar(5) NULL,
    [X1] varchar(255) NULL,
    CONSTRAINT [PK__DefaultC__3214EC0723541C72] PRIMARY KEY CLUSTERED ([Id])
);
GO


CREATE TABLE [dbo].[DSOpe] (
    [ID] int NOT NULL,
    [decimal_default] decimal(15,2) NOT NULL,
    [MyGuid] uniqueidentifier NOT NULL,
    [default] varchar(10) NULL,
    [MyGuidBadDefault] uniqueidentifier NULL,
    CONSTRAINT [PK__DSOpe__3214EC27150C693D] PRIMARY KEY CLUSTERED ([ID])
);
GO


CREATE TABLE [dbo].[EnumWithDefaultValue] (
    [Id] int NOT NULL IDENTITY,
    [SomeEnum] int NOT NULL,
    CONSTRAINT [PK__EnumWith__3214EC07A2120E43] PRIMARY KEY CLUSTERED ([Id])
);
GO


CREATE TABLE [dbo].[EventProcessor] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(200) NOT NULL,
    [Description] varchar(512) NULL,
    [EndpointAddress] varchar(512) NULL,
    [Enabled] bit NOT NULL,
    CONSTRAINT [PK_EventProcessor] PRIMARY KEY CLUSTERED ([Id])
);
GO


CREATE TABLE [dbo].[FinancialInstitutionOffice] (
    [FinancialInstitutionCode] uniqueidentifier NOT NULL,
    [Code] uniqueidentifier NOT NULL,
    [OfficeName] nvarchar(200) NULL,
    CONSTRAINT [PK_FinancialInstitutionOffice] PRIMARY KEY ([FinancialInstitutionCode])
);
GO


CREATE TABLE [dbo].[ForeignKeyIsNotEnforced] (
    [id] int NOT NULL IDENTITY,
    [null_value] int NULL,
    [not_null_value] int NOT NULL,
    CONSTRAINT [PK__ForeignK__3213E83F7A9FD04F] PRIMARY KEY CLUSTERED ([id]),
    CONSTRAINT [AK_ForeignKeyIsNotEnforced_not_null_value] UNIQUE ([not_null_value])
);
GO


CREATE TABLE [dbo].[HasPrincipalKeyTestParent] (
    [Id] int NOT NULL IDENTITY,
    [A] int NOT NULL,
    [B] int NOT NULL,
    [C] int NULL,
    [D] int NULL,
    CONSTRAINT [PK__HasPrinc__3214EC07C4D24B68] PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [AK_HasPrincipalKeyTestParent_A_B] UNIQUE ([A], [B])
);
GO


CREATE TABLE [dbo].[header] (
    [ID] int NOT NULL,
    [anotherID] int NOT NULL,
    [added] datetime NOT NULL,
    CONSTRAINT [PK__header__FAB049E7232129AF] PRIMARY KEY CLUSTERED ([ID], [anotherID])
);
GO


CREATE TABLE [dbo].[hierarchy_test] (
    [ID] int NOT NULL IDENTITY,
    [hid] hierarchyid NOT NULL,
    CONSTRAINT [PK__hierarch__3214EC27A00D9B63] PRIMARY KEY CLUSTERED ([ID])
);
GO


CREATE TABLE [dbo].[MultipleKeys] (
    [UserId] int NOT NULL,
    [FavouriteColourId] int NOT NULL,
    [BestHolidayTypeId] int NOT NULL,
    [BankId] int NOT NULL,
    [CarId] int NOT NULL,
    CONSTRAINT [PK_MultipleKeys] PRIMARY KEY CLUSTERED ([UserId], [FavouriteColourId], [BestHolidayTypeId])
);
GO


CREATE TABLE [dbo].[PeriodTestTable] (
    [id] int NOT NULL,
    [joe.bloggs] int NULL,
    CONSTRAINT [PK__PeriodTe__3213E83F590BD2DE] PRIMARY KEY CLUSTERED ([id])
);
GO


CREATE TABLE [dbo].[Person] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(50) NOT NULL,
    CONSTRAINT [PK__Person__3214EC07424FC866] PRIMARY KEY CLUSTERED ([Id])
);
GO


CREATE TABLE [dbo].[pk_ordinal_test] (
    [C1] int NOT NULL,
    [C3] int NOT NULL,
    [C2] int NOT NULL,
    CONSTRAINT [PK__pk_ordin__1135D3B4AF17DB21] PRIMARY KEY CLUSTERED ([C3], [C1])
);
GO


CREATE TABLE [dbo].[PropertyTypesToAdd] (
    [id] int NOT NULL,
    [dt_default] datetime2 NULL,
    [dt7] datetime2 NULL,
    [defaultCheck] varchar(10) NULL,
    CONSTRAINT [PK__Property__3213E83FCECB1764] PRIMARY KEY CLUSTERED ([id])
);
GO


CREATE TABLE [dbo].[RebelGalaxyBroadsides] (
    [Damage] int NOT NULL,
    [Cost] int NOT NULL,
    [WeaponName] varchar(30) NULL,
    CONSTRAINT [PK_RebelGalaxyBroadsides] PRIMARY KEY ([Damage], [Cost])
);
GO


CREATE TABLE [dbo].[RebelGalaxyShips] (
    [Broadsides] int NOT NULL,
    [Turrets] int NOT NULL,
    [Cost] int NOT NULL,
    [ShipName] varchar(30) NULL,
    CONSTRAINT [PK_RebelGalaxyShips] PRIMARY KEY ([Broadsides], [Turrets], [Cost])
);
GO


CREATE TABLE [dbo].[RebelGalaxyWeapons] (
    [Damage] int NOT NULL,
    [Cost] int NOT NULL,
    [WeaponName] varchar(30) NULL,
    CONSTRAINT [PK_RebelGalaxyWeapons] PRIMARY KEY ([Damage], [Cost])
);
GO


CREATE TABLE [dbo].[ScreamAndShout] (
    [id] int NOT NULL,
    [KoeffVed] decimal(4,4) NULL,
    CONSTRAINT [PK__ScreamAn__3213E83F55C3020B] PRIMARY KEY CLUSTERED ([id])
);
GO


CREATE TABLE [dbo].[SequenceTest] (
    [Id] int NOT NULL DEFAULT (NEXT VALUE FOR [dbo].[CountBy1]),
    [CntByBigInt] bigint NOT NULL DEFAULT (NEXT VALUE FOR [dbo].[CountByBigInt]),
    [CntByTinyInt] tinyint NOT NULL DEFAULT (NEXT VALUE FOR [dbo].[CountByTinyInt]),
    [CntBySmallInt] smallint NOT NULL DEFAULT (NEXT VALUE FOR [dbo].[CountBySmallInt]),
    [CntByDecimal] decimal(18,0) NOT NULL DEFAULT (NEXT VALUE FOR [dbo].[CountByDecimal]),
    [CntByNumeric] numeric(18,0) NOT NULL DEFAULT (NEXT VALUE FOR [dbo].[CountByNumeric]),
    CONSTRAINT [PK__Sequence__3214EC072BABC879] PRIMARY KEY CLUSTERED ([Id])
);
GO


CREATE TABLE [dbo].[StockPrediction] (
    [id] int NOT NULL IDENTITY,
    [name] varchar(50) NOT NULL,
    [date] datetime NOT NULL,
    [open] decimal(10,4) NOT NULL,
    [high] decimal(10,4) NOT NULL,
    [low] decimal(10,4) NOT NULL,
    [close] decimal(10,4) NOT NULL,
    [adj_close] decimal(10,4) NOT NULL,
    [volume] int NOT NULL,
    [buy] bit NULL,
    CONSTRAINT [PK__StockPre__3213E83FFBA52754] PRIMARY KEY CLUSTERED ([id])
);
GO


CREATE TABLE [dbo].[table with duplicate column names] (
    [id] int NOT NULL IDENTITY,
    [user_id] int NOT NULL,
    [UserId] int NOT NULL,
    [User Id] int NOT NULL,
    [User  Id] int NOT NULL,
    [user__id] int NOT NULL,
    CONSTRAINT [PK__table wi__3213E83F7E62FED9] PRIMARY KEY CLUSTERED ([id])
);
GO


CREATE TABLE [dbo].[table with space] (
    [id] int NOT NULL,
    CONSTRAINT [PK__table wi__3213E83F6330D784] PRIMARY KEY CLUSTERED ([id])
);
GO


CREATE TABLE [dbo].[table with space and in columns] (
    [id value] int NOT NULL,
    CONSTRAINT [PK__table wi__92CF061C44B1B06D] PRIMARY KEY CLUSTERED ([id value])
);
GO


CREATE TABLE [dbo].[TableA] (
    [TableAId] int NOT NULL IDENTITY,
    [TableADesc] varchar(20) NULL,
    CONSTRAINT [TableA_pkey] PRIMARY KEY CLUSTERED ([TableAId])
);
GO


CREATE TABLE [dbo].[TableWithSpaceInColumnOnly] (
    [id value] int NOT NULL,
    CONSTRAINT [PK__TableWit__92CF061C3D75F56A] PRIMARY KEY CLUSTERED ([id value])
);
GO


CREATE TABLE [dbo].[TadeuszSobol] (
    [Id] int NOT NULL IDENTITY,
    [Description] varchar(max) NULL,
    [Notes] nvarchar(max) NULL,
    [Name] varchar(10) NULL,
    CONSTRAINT [PK__TadeuszS__3214EC07437DCB5A] PRIMARY KEY CLUSTERED ([Id])
);
GO


CREATE TABLE [dbo].[Task] (
    [TaskId] bigint NOT NULL,
    CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED ([TaskId])
);
GO


CREATE TABLE [dbo].[tblOrderErrors] (
    [ID] int NOT NULL IDENTITY,
    [error] varchar(50) NULL,
    CONSTRAINT [PK__tblOrder__3214EC27BAF60861] PRIMARY KEY CLUSTERED ([ID])
);
GO


CREATE TABLE [dbo].[tblOrderErrorsAB_] (
    [ID] int NOT NULL IDENTITY,
    [error] varchar(50) NULL,
    CONSTRAINT [PK__tblOrder__3214EC2777E38424] PRIMARY KEY CLUSTERED ([ID])
);
GO


CREATE TABLE [dbo].[tblOrders] (
    [ID] int NOT NULL IDENTITY,
    [added] datetime NOT NULL,
    CONSTRAINT [PK__tblOrder__3214EC27DE21AFD8] PRIMARY KEY CLUSTERED ([ID])
);
GO


CREATE TABLE [dbo].[Token] (
    [Id] uniqueidentifier NOT NULL,
    [Enabled] bit NOT NULL,
    CONSTRAINT [PK__Token__3214EC0780725089] PRIMARY KEY CLUSTERED ([Id])
);
GO


CREATE TABLE [dbo].[User] (
    [ID] int NOT NULL IDENTITY,
    [ExternalUserID] varchar(50) NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED ([ID])
);
GO


CREATE TABLE [dbo].[Бренды товара] (
    [Код бренда] int NOT NULL IDENTITY,
    [Наименование бренда] varchar(50) NOT NULL,
    [Логотип_бренда] image(2147483647) NULL,
    [Логотип_бренда_вертикальный] image(2147483647) NULL,
    CONSTRAINT [PK_Бренды] PRIMARY KEY CLUSTERED ([Код бренда])
);
GO


CREATE TABLE [FFRS].[CV] (
    [BatchUID] uniqueidentifier NOT NULL,
    [CVID] int NOT NULL,
    [CVName] nvarchar(200) NULL,
    CONSTRAINT [PK_CV] PRIMARY KEY ([BatchUID], [CVID])
);
GO


CREATE TABLE [Issue47].[Role] (
    [RoleId] int NOT NULL IDENTITY,
    [Role] varchar(10) NULL,
    CONSTRAINT [PK__Role__8AFACE1A223A2B5D] PRIMARY KEY CLUSTERED ([RoleId])
);
GO


CREATE TABLE [Issue47].[Users] (
    [UserId] int NOT NULL IDENTITY,
    [Name] varchar(10) NULL,
    CONSTRAINT [PK__Users__1788CC4CFC98EC84] PRIMARY KEY CLUSTERED ([UserId])
);
GO


CREATE TABLE [OneEightSix].[UploadedFile] (
    [Id] int NOT NULL IDENTITY,
    [FullPath] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_UploadedFile] PRIMARY KEY CLUSTERED ([Id])
);
GO


CREATE TABLE [Beta].[ToAlpha] (
    [Id] int NOT NULL IDENTITY,
    [AlphaId] int NOT NULL,
    CONSTRAINT [PK__ToAlpha__3214EC07B6C8B1E7] PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [BetaToAlpha_AlphaWorkflow] FOREIGN KEY ([AlphaId]) REFERENCES [Alpha].[workflow] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[A] (
    [AId] int NOT NULL IDENTITY,
    [C1] int NOT NULL,
    [C2] int NOT NULL,
    CONSTRAINT [PK_A] PRIMARY KEY CLUSTERED ([AId]),
    CONSTRAINT [FK_A_A] FOREIGN KEY ([C1], [C2]) REFERENCES [dbo].[AAREF] ([C1], [C2]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[AB_OrderLinesAB_] (
    [ID] int NOT NULL IDENTITY,
    [OrderID] int NOT NULL,
    [sku] varchar(15) NULL,
    CONSTRAINT [PK__AB_Order__3214EC2731B53C8A] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [AB_OrderLinesAB_FK] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[AB_OrdersAB_] ([ID]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[Ticket] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedById] bigint NOT NULL,
    [ModifiedById] bigint NULL,
    CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [FK_Ticket_AppUser] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[AppUser] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Ticket_AppUser1] FOREIGN KEY ([ModifiedById]) REFERENCES [dbo].[AppUser] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[BlahBlahLink] (
    [BlahID] int NOT NULL,
    [BlahID2] int NOT NULL,
    CONSTRAINT [PK_BlahBlahLink] PRIMARY KEY CLUSTERED ([BlahID], [BlahID2]),
    CONSTRAINT [FK_BlahBlahLink_Blah] FOREIGN KEY ([BlahID]) REFERENCES [dbo].[Blah] ([BlahID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_BlahBlahLink_Blah2] FOREIGN KEY ([BlahID2]) REFERENCES [dbo].[Blah] ([BlahID]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[BlahBlahLink_readonly] (
    [BlahID] int NOT NULL,
    [BlahID2] int NOT NULL,
    [RowVersion] timestamp NULL,
    [id] int NOT NULL IDENTITY,
    [id2] int NULL,
    CONSTRAINT [PK_BlahBlahLink_ro] PRIMARY KEY CLUSTERED ([BlahID], [BlahID2]),
    CONSTRAINT [FK_BlahBlahLink_Blah_ro] FOREIGN KEY ([BlahID]) REFERENCES [dbo].[Blah] ([BlahID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_BlahBlahLink_Blah_ro2] FOREIGN KEY ([BlahID2]) REFERENCES [dbo].[Blah] ([BlahID]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[BlahBlahLink_v2] (
    [BlahID] int NOT NULL,
    [BlahID2] int NOT NULL,
    [dummy1] int NULL,
    [dummy2] int NOT NULL,
    [hello] int NOT NULL,
    CONSTRAINT [PK_BlahBlahLinkv2_ro] PRIMARY KEY CLUSTERED ([BlahID], [BlahID2]),
    CONSTRAINT [FK_BlahBlahLinkv2_Blah_ro] FOREIGN KEY ([BlahID]) REFERENCES [dbo].[Blah] ([BlahID]) ON DELETE CASCADE,
    CONSTRAINT [FK_BlahBlahLinkv2_Blah_ro2] FOREIGN KEY ([BlahID2]) REFERENCES [dbo].[Blah] ([BlahID]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[BlahBlargLink] (
    [BlahID] int NOT NULL,
    [BlargID] int NOT NULL,
    CONSTRAINT [PK_BlahBlargLink] PRIMARY KEY CLUSTERED ([BlahID], [BlargID]),
    CONSTRAINT [FK_BlahBlargLink_Blah] FOREIGN KEY ([BlahID]) REFERENCES [dbo].[Blah] ([BlahID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_BlahBlargLink_Blarg] FOREIGN KEY ([BlargID]) REFERENCES [dbo].[Blarg] ([BlargID]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[Burak1] (
    [id] bigint NOT NULL IDENTITY,
    [id_t] bigint NOT NULL,
    [num] bigint NOT NULL,
    CONSTRAINT [PK_Burak1] PRIMARY KEY CLUSTERED ([id]),
    CONSTRAINT [FK_Burak_Test2] FOREIGN KEY ([id]) REFERENCES [dbo].[Burak2] ([id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Burak_Test1] FOREIGN KEY ([id_t], [num]) REFERENCES [dbo].[Burak2] ([id], [num]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[CMS_FileTag] (
    [FileId] int NOT NULL,
    [TagId] int NOT NULL,
    CONSTRAINT [PK_CMS_FileTag] PRIMARY KEY ([FileId], [TagId]),
    CONSTRAINT [FK_CMS_FileTag_CMS_File] FOREIGN KEY ([FileId]) REFERENCES [dbo].[CMS_File] ([FileId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CMS_FileTag_CMS_Tag] FOREIGN KEY ([TagId]) REFERENCES [dbo].[CMS_Tag] ([TagId]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[CODE_MeetingTopicDetails] (
    [id] int NOT NULL IDENTITY,
    [id_reuniao] int NOT NULL,
    [ord_trab] int NULL,
    [assunto] nvarchar(250) NULL,
    [desenvolvimento] nvarchar(max) NULL,
    [origem] nvarchar(5) NULL,
    [id_origem] int NULL,
    [Estado] int NULL,
    [CompanyID] int NOT NULL,
    [DateCreated] datetime NOT NULL,
    [DateChanged] datetime NULL,
    CONSTRAINT [PK_CODE_MeetingTopicDetails] PRIMARY KEY CLUSTERED ([id]),
    CONSTRAINT [FK_CODE_MeetingTopicDetails_CODE_PARAM_MeetingTopicDetailSource] FOREIGN KEY ([origem]) REFERENCES [dbo].[CODE_PARAM_MeetingTopicDetailSource] ([Code]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[Car] (
    [Id] int NOT NULL,
    [PrimaryColourId] int NOT NULL,
    [CarMake] varchar(255) NOT NULL,
    [computed_column] int NULL,
    [computed_column_persisted] int NOT NULL,
    CONSTRAINT [PK__Car__3214EC0702833709] PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [CarPrimaryColourFK] FOREIGN KEY ([PrimaryColourId]) REFERENCES [dbo].[Colour] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[Company] (
    [coID] int NOT NULL IDENTITY,
    [cocogID] int NULL,
    [cocuID] int NULL,
    [coName] nvarchar(255) NULL,
    [coShortName] nvarchar(50) NULL,
    [coHtmlName] nvarchar(1000) NULL,
    [coEmployerNo] nvarchar(50) NULL,
    [coValidFrom] datetime NULL,
    [coValidTo] datetime NULL,
    [coVatIdNo] nvarchar(50) NULL,
    [coURL] nvarchar(255) NULL,
    [coActive] bit NOT NULL,
    CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED ([coID]),
    CONSTRAINT [FK_Company_CompanyGroup] FOREIGN KEY ([cocogID]) REFERENCES [dbo].[CompanyGroup] ([cogID]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[Attendee] (
    [AttendeeID] bigint NOT NULL,
    [Lastname] nvarchar(50) NOT NULL,
    [Firstname] nvarchar(50) NOT NULL,
    [PhoneCountryID] int NULL,
    CONSTRAINT [PK_Attendee] PRIMARY KEY CLUSTERED ([AttendeeID]),
    CONSTRAINT [FK_Attendee_PhoneCountry] FOREIGN KEY ([PhoneCountryID]) REFERENCES [dbo].[Country] ([CountryID]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[User309] (
    [UserID] bigint NOT NULL,
    [Lastname] nvarchar(100) NOT NULL,
    [Firstname] nvarchar(100) NOT NULL,
    [PhoneCountryID] int NULL,
    CONSTRAINT [PK_User309] PRIMARY KEY CLUSTERED ([UserID]),
    CONSTRAINT [FK_User309_PhoneCountry] FOREIGN KEY ([PhoneCountryID]) REFERENCES [dbo].[Country] ([CountryID]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[EventProcessorEventFilter] (
    [Id] int NOT NULL IDENTITY,
    [EventProcessorId] int NOT NULL,
    [WantedEventId] int NOT NULL,
    CONSTRAINT [PK_EventProcessorEventFilter] PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [FK_EventProcessorEventFilter__EventProcessor] FOREIGN KEY ([EventProcessorId]) REFERENCES [dbo].[EventProcessor] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[ForeignKeyIsNotEnforcedItem] (
    [id] int NOT NULL IDENTITY,
    [null_value] int NULL,
    [not_null_value] int NOT NULL,
    CONSTRAINT [PK__ForeignK__3213E83FB7C5760B] PRIMARY KEY CLUSTERED ([id]),
    CONSTRAINT [FK_ForeignKeyIsNotEnforcedItem_notnull_notnull] FOREIGN KEY ([not_null_value]) REFERENCES [dbo].[ForeignKeyIsNotEnforced] ([not_null_value]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ForeignKeyIsNotEnforcedItem_null_notnull] FOREIGN KEY ([null_value]) REFERENCES [dbo].[ForeignKeyIsNotEnforced] ([not_null_value]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[HasPrincipalKeyTestChild] (
    [Id] int NOT NULL IDENTITY,
    [A] int NOT NULL,
    [B] int NOT NULL,
    [C] int NULL,
    [D] int NULL,
    CONSTRAINT [PK__HasPrinc__3214EC07061FCDAA] PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [FK_HasPrincipalKey_AB] FOREIGN KEY ([A], [B]) REFERENCES [dbo].[HasPrincipalKeyTestParent] ([A], [B]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[footer] (
    [ID] int NOT NULL IDENTITY,
    [otherID] int NOT NULL,
    [added] datetime NOT NULL,
    CONSTRAINT [PK__footer__3214EC272B429812] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [fooderFK] FOREIGN KEY ([ID], [otherID]) REFERENCES [dbo].[header] ([ID], [anotherID]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[PersonPosts] (
    [Id] int NOT NULL IDENTITY,
    [Title] varchar(20) NOT NULL,
    [Body] varchar(100) NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedBy] int NOT NULL,
    CONSTRAINT [PK__PersonPo__3214EC0714F40FA4] PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [FK_PersonPosts_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Person] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PersonPosts_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[Person] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [Beta].[Harish3485] (
    [id] int NOT NULL IDENTITY,
    [another_id] int NOT NULL,
    CONSTRAINT [PK__Harish34__3213E83F1B89ADF0] PRIMARY KEY CLUSTERED ([id]),
    CONSTRAINT [FK_Harish] FOREIGN KEY ([another_id]) REFERENCES [dbo].[PropertyTypesToAdd] ([id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [Kate].[ScreamAndShout] (
    [KateID] int NOT NULL,
    [description] varchar(20) NOT NULL,
    CONSTRAINT [PK__ScreamAn__8540EE1FFC54DDC8] PRIMARY KEY CLUSTERED ([KateID]),
    CONSTRAINT [KateFK] FOREIGN KEY ([KateID]) REFERENCES [dbo].[ScreamAndShout] ([id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[table mapping with space] (
    [id] int NOT NULL,
    [id value] int NOT NULL,
    CONSTRAINT [map_with_space] PRIMARY KEY CLUSTERED ([id], [id value]),
    CONSTRAINT [space1FK] FOREIGN KEY ([id]) REFERENCES [dbo].[table with space] ([id]) ON DELETE NO ACTION,
    CONSTRAINT [space2FK] FOREIGN KEY ([id value]) REFERENCES [dbo].[table with space and in columns] ([id value]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[TableB] (
    [TableBId] int NOT NULL IDENTITY,
    [TableAId] int NOT NULL,
    [ParentTableAId] int NULL,
    [TableBDesc] varchar(20) NULL,
    CONSTRAINT [TableB_pkey] PRIMARY KEY CLUSTERED ([TableBId], [TableAId]),
    CONSTRAINT [FK_TableA_CompositeKey_Req] FOREIGN KEY ([TableAId]) REFERENCES [dbo].[TableA] ([TableAId]) ON DELETE NO ACTION,
    CONSTRAINT [ParentTableB_Hierarchy] FOREIGN KEY ([TableAId], [TableBId]) REFERENCES [dbo].[TableB] ([TableBId], [TableAId]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[tblOrderLines] (
    [ID] int NOT NULL IDENTITY,
    [OrderID] int NOT NULL,
    [sku] varchar(15) NULL,
    CONSTRAINT [PK__tblOrder__3214EC2741C7C0E7] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [tblOrdersFK] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[tblOrders] ([ID]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[User_Document] (
    [ID] int NOT NULL IDENTITY,
    [UserID] int NOT NULL,
    [CreatedByUserID] int NOT NULL,
    CONSTRAINT [PK_User_Document] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_User_Document_User1] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_User_Document_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE
);
GO


CREATE TABLE [Issue47].[UserRoles] (
    [UserRoleId] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK__UserRole__3D978A3528CA58C0] PRIMARY KEY CLUSTERED ([UserRoleId]),
    CONSTRAINT [Issue47_UserRoles_roleid] FOREIGN KEY ([RoleId]) REFERENCES [Issue47].[Role] ([RoleId]) ON DELETE NO ACTION,
    CONSTRAINT [Issue47_UserRoles_userid] FOREIGN KEY ([UserId]) REFERENCES [Issue47].[Users] ([UserId]) ON DELETE NO ACTION
);
GO


CREATE TABLE [OneEightSix].[Issue] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(100) NOT NULL,
    [Content] nvarchar(max) NULL,
    [ConsentDocumentId] int NULL,
    CONSTRAINT [PK_Issue] PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [FK_Issue_UploadedFileConsentDocument] FOREIGN KEY ([ConsentDocumentId]) REFERENCES [OneEightSix].[UploadedFile] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [dbo].[CarToColour] (
    [CarId] int NOT NULL,
    [ColourId] int NOT NULL,
    CONSTRAINT [PK__CarToCol__8C02E66BFA508402] PRIMARY KEY CLUSTERED ([CarId], [ColourId]),
    CONSTRAINT [CarToColour_CarId] FOREIGN KEY ([CarId]) REFERENCES [dbo].[Car] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [CarToColour_ColourId] FOREIGN KEY ([ColourId]) REFERENCES [dbo].[Colour] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [Alpha].[Harish3485] (
    [id] int NOT NULL IDENTITY,
    [harish_id] int NOT NULL,
    CONSTRAINT [PK__Harish34__3213E83F6B711DC1] PRIMARY KEY CLUSTERED ([id]),
    CONSTRAINT [FK_Harish] FOREIGN KEY ([harish_id]) REFERENCES [Kate].[ScreamAndShout] ([KateID]) ON DELETE NO ACTION
);
GO


CREATE TABLE [OneEightSix].[IssueUploadedFile] (
    [UploadedFileId] int NOT NULL,
    [IssueId] int NOT NULL,
    CONSTRAINT [PK_IssueUploadedFile] PRIMARY KEY CLUSTERED ([UploadedFileId], [IssueId]),
    CONSTRAINT [FK_IssueUploadedFile_Issue] FOREIGN KEY ([IssueId]) REFERENCES [OneEightSix].[Issue] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_IssueUploadedFile_UploadedFile] FOREIGN KEY ([UploadedFileId]) REFERENCES [OneEightSix].[UploadedFile] ([Id]) ON DELETE NO ACTION
);
GO


CREATE INDEX [IX_Harish3485_harish_id] ON [Alpha].[Harish3485] ([harish_id]);
GO


CREATE INDEX [IX_Harish3485_another_id] ON [Beta].[Harish3485] ([another_id]);
GO


CREATE INDEX [IX_ToAlpha_AlphaId] ON [Beta].[ToAlpha] ([AlphaId]);
GO


CREATE INDEX [IX_A_C1_C2] ON [dbo].[A] ([C1], [C2]);
GO


CREATE INDEX [IX_AB_OrderLinesAB__OrderID] ON [dbo].[AB_OrderLinesAB_] ([OrderID]);
GO


CREATE INDEX [IX_Attendee_PhoneCountryID] ON [dbo].[Attendee] ([PhoneCountryID]);
GO


CREATE INDEX [IX_BlahBlahLink_BlahID2] ON [dbo].[BlahBlahLink] ([BlahID2]);
GO


CREATE INDEX [IX_BlahBlahLink_readonly_BlahID2] ON [dbo].[BlahBlahLink_readonly] ([BlahID2]);
GO


CREATE INDEX [IX_BlahBlahLink_v2_BlahID2] ON [dbo].[BlahBlahLink_v2] ([BlahID2]);
GO


CREATE INDEX [IX_BlahBlargLink_BlargID] ON [dbo].[BlahBlargLink] ([BlargID]);
GO


CREATE UNIQUE INDEX [IX_Burak1_id_t_num] ON [dbo].[Burak1] ([id_t], [num]);
GO


CREATE UNIQUE INDEX [U_Burak2] ON [dbo].[Burak2] ([id], [num]);
GO


CREATE INDEX [IX_Car_PrimaryColourId] ON [dbo].[Car] ([PrimaryColourId]);
GO


CREATE INDEX [IX_CarToColour_ColourId] ON [dbo].[CarToColour] ([ColourId]);
GO


CREATE INDEX [IX_CMS_FileTag_TagId] ON [dbo].[CMS_FileTag] ([TagId]);
GO


CREATE INDEX [IX_CODE_MeetingTopicDetails_origem] ON [dbo].[CODE_MeetingTopicDetails] ([origem]);
GO


CREATE UNIQUE INDEX [UK_CODE_PARAM_MeetingTopicDetailSource] ON [dbo].[CODE_PARAM_MeetingTopicDetailSource] ([Code]);
GO


CREATE INDEX [IX_Company_cocogID] ON [dbo].[Company] ([cocogID]);
GO


CREATE UNIQUE INDEX [IX_EventProcessorEventFilter] ON [dbo].[EventProcessorEventFilter] ([EventProcessorId], [WantedEventId]);
GO


CREATE UNIQUE INDEX [UniqueOfficeName_FinancialInstitutionOffice] ON [dbo].[FinancialInstitutionOffice] ([FinancialInstitutionCode], [OfficeName]) WHERE [OfficeName] IS NOT NULL;
GO


CREATE INDEX [IX_footer_ID_otherID] ON [dbo].[footer] ([ID], [otherID]);
GO


CREATE UNIQUE INDEX [UQ_ForeignKeyIsNotEnforced_not_null_value] ON [dbo].[ForeignKeyIsNotEnforced] ([not_null_value]);
GO


CREATE UNIQUE INDEX [UQ_ForeignKeyIsNotEnforced_null_value] ON [dbo].[ForeignKeyIsNotEnforced] ([null_value]) WHERE [null_value] IS NOT NULL;
GO


CREATE UNIQUE INDEX [UQ_ForeignKeyIsNotEnforcedItem_not_null_value] ON [dbo].[ForeignKeyIsNotEnforcedItem] ([not_null_value]);
GO


CREATE UNIQUE INDEX [UQ_ForeignKeyIsNotEnforcedItem_null_value] ON [dbo].[ForeignKeyIsNotEnforcedItem] ([null_value]) WHERE [null_value] IS NOT NULL;
GO


CREATE UNIQUE INDEX [IX_HasPrincipalKeyTestChild_A_B] ON [dbo].[HasPrincipalKeyTestChild] ([A], [B]);
GO


CREATE UNIQUE INDEX [UQ_HasPrincipalKeyTestParent_AB] ON [dbo].[HasPrincipalKeyTestParent] ([A], [B]);
GO


CREATE UNIQUE INDEX [UQ_HasPrincipalKeyTestParent_AC] ON [dbo].[HasPrincipalKeyTestParent] ([A], [C]) WHERE [C] IS NOT NULL;
GO


CREATE UNIQUE INDEX [UQ_HasPrincipalKeyTestParent_CD] ON [dbo].[HasPrincipalKeyTestParent] ([C], [D]) WHERE [C] IS NOT NULL AND [D] IS NOT NULL;
GO


CREATE INDEX [IX_MultipleKeys_BestHolidayType] ON [dbo].[MultipleKeys] ([BestHolidayTypeId]);
GO


CREATE UNIQUE INDEX [UC_MultipleKeys_FavouriteColour] ON [dbo].[MultipleKeys] ([FavouriteColourId]);
GO


CREATE UNIQUE INDEX [IX_MultipleKeys_Holiday_Bank] ON [dbo].[MultipleKeys] ([BestHolidayTypeId], [BankId]);
GO


CREATE INDEX [IX_PersonPosts_CreatedBy] ON [dbo].[PersonPosts] ([CreatedBy]);
GO


CREATE INDEX [IX_PersonPosts_UpdatedBy] ON [dbo].[PersonPosts] ([UpdatedBy]);
GO


CREATE INDEX [IX_table mapping with space_id value] ON [dbo].[table mapping with space] ([id value]);
GO


CREATE INDEX [fki_ParentTableA_FK_Constraint] ON [dbo].[TableB] ([TableAId]);
GO


CREATE UNIQUE INDEX [IX_TableB_TableAId_TableBId] ON [dbo].[TableB] ([TableAId], [TableBId]);
GO


CREATE INDEX [IX_tblOrderLines_OrderID] ON [dbo].[tblOrderLines] ([OrderID]);
GO


CREATE INDEX [IX_Ticket_CreatedById] ON [dbo].[Ticket] ([CreatedById]);
GO


CREATE INDEX [IX_Ticket_ModifiedById] ON [dbo].[Ticket] ([ModifiedById]);
GO


CREATE INDEX [IX_User_Document_CreatedByUserID] ON [dbo].[User_Document] ([CreatedByUserID]);
GO


CREATE INDEX [IX_User_Document_UserID] ON [dbo].[User_Document] ([UserID]);
GO


CREATE INDEX [IX_User309_PhoneCountryID] ON [dbo].[User309] ([PhoneCountryID]);
GO


CREATE INDEX [IX_UserRoles_RoleId] ON [Issue47].[UserRoles] ([RoleId]);
GO


CREATE INDEX [IX_UserRoles_UserId] ON [Issue47].[UserRoles] ([UserId]);
GO


CREATE INDEX [IX_Issue_ConsentDocumentId] ON [OneEightSix].[Issue] ([ConsentDocumentId]);
GO


CREATE INDEX [IX_IssueUploadedFile_IssueId] ON [OneEightSix].[IssueUploadedFile] ([IssueId]);
GO


