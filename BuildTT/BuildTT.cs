﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BuildTT.Application;
using BuildTT.Infrastructure;

namespace BuildTT
{
    public static class BuildTT
    {
        private static string _version;

        public static void Create(string generatorRoot, string ttRoot, string version)
        {
            _version = version;
            UpdateEfrpgVersion(Path.Combine(generatorRoot, "EfrpgVersion.cs"));
            CreateTT(generatorRoot, ttRoot);
            CreateCoreTTInclude(generatorRoot, ttRoot);
        }

        private static void CreateTT(string generatorRoot, string ttRoot)
        {
            const string footer = @"    // Settings ***************************************************************************************************************************
    // Only the most popular settings are listed below.
    // Either override Settings.* here, or edit the Settings, FilterSettings and SingleContextFilter classes located at the top of EF.Reverse.POCO.v3.ttinclude
    
    // For help on the various Types below, please read https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/wiki/Common-Settings.*Types-explained
    // The following entries are the only required settings.
    Settings.DatabaseType            = DatabaseType.SqlServer; // SqlServer, SqlCe, SQLite, PostgreSQL. Coming next: MySql, Oracle
    Settings.TemplateType            = TemplateType.EfCore9; // EfCore9, EfCore8, Ef6, FileBasedCore8-9. FileBased specify folder using Settings.TemplateFolder
    Settings.GeneratorType           = GeneratorType.EfCore; // EfCore, Ef6, Custom. Custom edit GeneratorCustom class to provide your own implementation

    Settings.FileManagerType         = FileManagerType.EfCore; // .NET Core project = EfCore; .NET 4.x project = VisualStudio; No output (testing only) = Null
    Settings.ConnectionString        = ""Data Source=(local);Initial Catalog=**TODO**;Integrated Security=True;MultipleActiveResultSets=True;Encrypt=false;TrustServerCertificate=true""; // This is used by the generator to reverse engineer your database
    Settings.ConnectionStringName    = ""MyDbContext""; // ConnectionString key as specified in your app.config/web.config/appsettings.json. Not used by the generator, but is placed into the generated DbContext constructor.
    Settings.DbContextName           = ""MyDbContext""; // Class name for the DbContext to be generated.
    //Settings.DbContextInterfaceName= ""IMyDbContext""; // Defaults to ""I"" + DbContextName or set string empty to not implement any interface.
    Settings.GenerateSeparateFiles   = false;
    Settings.Namespace               = DefaultNamespace; // Override the default namespace here. Please use double quotes, example: ""Accounts.Billing""
    Settings.TemplateFolder          = Path.Combine(Settings.Root, ""Templates""); // Only used if Settings.TemplateType = TemplateType.FileBased. Specify folder name where the mustache folders can be found. Please read https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/wiki/Custom-file-based-templates
    Settings.AddUnitTestingDbContext = true; // Will add a FakeDbContext and FakeDbSet for easy unit testing. Read https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/wiki/FakeDbContext


    // Filtering **************************************************************************************************************************
    // Filtering can now be done via one or more Regex's and one or more functions.
    // Gone are the days of a single do-it-all regex, you can now split them up into many smaller Regex's.
    // You can have as many as you like, and mix and match them.
    // These settings are only used by the single context filter SingleContextFilter (Settings.GenerateSingleDbContext = true)
    // For further information please visit https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/wiki/Filtering
    // For multi-context filtering (Settings.GenerateSingleDbContext = false), please read https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/wiki/Generating-multiple-database-contexts-in-a-single-go
    // Single-context filtering is done via FilterSettings and SingleContextFilter classes.
    // Override the filters here, or edit directly the FilterSettings and SingleContextFilter classes located at the top of EF.Reverse.POCO.v3.ttinclude
    FilterSettings.Reset();
    FilterSettings.AddDefaults();

    // Examples:
    //FilterSettings.IncludeViews                 = true;
    //FilterSettings.IncludeSynonyms              = false;
    //FilterSettings.IncludeStoredProcedures      = true;
    //FilterSettings.IncludeTableValuedFunctions  = false; // If true, for EF6 install the ""EntityFramework.CodeFirstStoreFunctions"" NuGet Package.
    //FilterSettings.IncludeScalarValuedFunctions = false;

    // Examples:
    //FilterSettings.SchemaFilters.Add(new RegexExcludeFilter(""[Ff]inance.*"")); // This excludes the Finance schema
    //FilterSettings.SchemaFilters.Add(new RegexIncludeFilter(""dbo.*"")); // This includes only dbo schema
    //FilterSettings.TableFilters.Add(new RegexExcludeFilter("".*[Bb]illing.*"")); // This excludes all tables with 'billing' anywhere in the name
    //FilterSettings.TableFilters.Add(new RegexIncludeFilter(""^[Cc]ustomer.*"")); // This includes any remaining tables with names beginning with 'customer'
    FilterSettings.TableFilters.Add(new RegexExcludeFilter(""AspNet.*"")); // This excludes all tables starting with 'AspNet'
    FilterSettings.TableFilters.Add(new RegexExcludeFilter(""__EFMigrationsHistory"")); // This excludes a table called '__EFMigrationsHistory'
    //FilterSettings.ColumnFilters.Add(new RegexExcludeFilter(""[Cc]reated[Aa]t.*"")); // This excludes all columns starting with 'CreatedAt' e.g CreatedAtUtc
    //FilterSettings.ColumnFilters.Add(new RegexIncludeFilter("".*"")); // Rarely used as it would only include columns with names listed here
    //FilterSettings.StoredProcedureFilters.Add(new RegexExcludeFilter(""Calc"")); // This excludes all stored procedures 'Calc' in the name
    //FilterSettings.StoredProcedureFilters.Add(new RegexIncludeFilter(""Pricing.*"")); // This includes all stored procedures starting with 'Pricing'
    

    // Elements to generate ***************************************************************************************************************
    // Add the elements that should be generated when the template is executed.
    // Multiple projects can be used that separate the different concerns.
    Settings.ElementsToGenerate = Elements.Poco | Elements.Context | Elements.Interface | Elements.PocoConfiguration | Elements.Enum;


    // Generate files in sub-folders ******************************************************************************************************
    if (Settings.GenerateSeparateFiles && Settings.FileManagerType == FileManagerType.EfCore)
    {
        Settings.ContextFolder           = @"""";              // Sub-folder you would like your DbContext to be added to.              e.g. @""Data""
        Settings.InterfaceFolder         = @""Interface"";     // Sub-folder you would like your Interface to be added to.              e.g. @""Data\Interface""
        Settings.PocoFolder              = @""Entities"";      // Sub-folder you would like your Poco's to be added to.                 e.g. @""Data\Entities""
        Settings.PocoConfigurationFolder = @""Configuration""; // Sub-folder you would like your Configuration mappings to be added to. e.g. @""Data\Configuration""
    }

    // Other settings *********************************************************************************************************************
    Settings.CommandTimeout                         = 600; // SQL Command timeout in seconds. 600 is 10 minutes, 0 will wait indefinitely. Some databases can be slow retrieving schema information.
    Settings.DbContextInterfaceBaseClasses          = ""IDisposable""; // Specify what the base classes are for your database context interface
    Settings.DbContextBaseClass                     = ""DbContext""; // Specify what the base class is for your DbContext. For ASP.NET Identity use ""IdentityDbContext<ApplicationUser>"";
    Settings.OnConfiguration                        = OnConfiguration.ConnectionString; // EFCore only. Determines the code generated within DbContext.OnConfiguration(). Please read https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/wiki/Settings.OnConfiguration
    Settings.AddParameterlessConstructorToDbContext = true; // EF6 only. If true, then DbContext will have a default (parameter-less) constructor which automatically passes in the connection string name, if false then no parameter-less constructor will be created.
    Settings.ConfigurationClassName                 = ""Configuration""; // Configuration, Mapping, Map, etc. This is appended to the Poco class name to configure the mappings.
    Settings.DatabaseReaderPlugin                   = """"; // Eg, ""c:\\Path\\YourDatabaseReader.dll,Full.Name.Of.Class.Including.Namespace"". See #501. This will allow you to specify a pluggable provider for reading your database.
    Settings.UseMappingTables                       = false; // Must be false for TemplateType.EfCore2-4. If true, mapping will be used, and no mapping tables will be generated. If false, all tables will be generated.

    Settings.EntityClassesModifiers        = ""public""; // ""public partial"";
    Settings.ConfigurationClassesModifiers = ""public""; // ""public partial"";
    Settings.DbContextClassModifiers       = ""public""; // ""public partial"";
    Settings.DbContextInterfaceModifiers   = ""public""; // ""public partial"";
    Settings.ResultClassModifiers          = ""public""; // ""public partial"";

    Settings.UsePascalCase                          = true; // This will rename the generated C# tables & properties to use PascalCase. If false table & property names will be left alone.
    Settings.UseDataAnnotations                     = false; // If true, will add data annotations to the poco classes.
    Settings.UsePropertyInitialisers                = false; // Removes POCO constructor and instead uses C# 6 property initialisers to set defaults
    Settings.UseLazyLoading                         = false; // Marks all navigation properties as virtual or not, to support or disable EF Lazy Loading feature
    Settings.UseInheritedBaseInterfaceFunctions     = false; // If true, the main DBContext interface functions will come from the DBContextInterfaceBaseClasses and not generated. If false, the functions will be generated.
    Settings.IncludeComments                        = CommentsStyle.AtEndOfField; // Adds comments to the generated code
    Settings.IncludeExtendedPropertyComments        = CommentsStyle.InSummaryBlock; // Adds extended properties as comments to the generated code
    Settings.DisableGeographyTypes                  = true; // Turns off use of spatial types: Geography, Geometry. More info: https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/wiki/Spatial-Types
    Settings.CollectionInterfaceType                = ""ICollection""; //  = ""System.Collections.Generic.List""; // Determines the declaration type of collections for the Navigation Properties. ICollection is used if not set.
    Settings.CollectionType                         = ""List""; // Determines the type of collection for the Navigation Properties. ""ObservableCollection"" for example. Add ""System.Collections.ObjectModel"" to AdditionalNamespaces if setting the CollectionType = ""ObservableCollection"".
    Settings.NullableShortHand                      = true; // true => T?, false => Nullable<T>
    Settings.AddIDbContextFactory                   = true; // Will add a default IDbContextFactory<DbContextName> implementation for easy dependency injection
    Settings.IncludeQueryTraceOn9481Flag            = false; // If SqlServer 2014 appears frozen / take a long time when this file is saved, try setting this to true (you will also need elevated privileges).
    Settings.UsePrivateSetterForComputedColumns     = true; // If the columns is computed, use a private setter.
    Settings.IncludeGeneratorVersionInCode          = false; // If true, will include the version number of the generator in the generated code (Settings.ShowLicenseInfo must also be true).
    Settings.TrimCharFields                         = false; // EF Core option only. If true, will TrimEnd() 'char' fields when read from the database.
    Settings.IncludeFieldNameConstants              = false; // Will include public const string {{NameHumanCase}}Field = ""{{NameHumanCase}}""; in the generated POCO class. It allows you to use a constant instead of magic strings.
    Settings.UsePropertiesForStoredProcResultSets   = false; // Stored procedure result set return models are rendered as fields (false) or properties (true).
    Settings.MergeMultipleStoredProcModelsIfAllSame = true; // Some stored procedures are reported as having multiple result sets when in fact there is only one. Set this to true to merge identical result sets.
    Settings.AdditionalNamespaces                   = new List<string>(); // To include extra namespaces, include them here. i.e. new List<string> { ""Microsoft.AspNetCore.Identity.EntityFrameworkCore"", ""System.ComponentModel.DataAnnotations"" };
    Settings.AdditionalContextInterfaceItems        = new List<string>(); // example: new List<string> { ""void SetAutoDetectChangesEnabled(bool flag);"" };
    Settings.AdditionalFileHeaderText               = new List<string>(); // This will put additional lines verbatim at the top of each file under the comments, 1 line per entry
    Settings.AdditionalFileFooterText               = new List<string>(); // This will put additional lines verbatim at the end of each file above the // </auto-generated>, 1 line per entry

    // Language choices
    Settings.GenerationLanguage = GenerationLanguage.CSharp;
    Settings.FileExtension      = "".cs"";

    // Code suppression *******************************************************************************
    Settings.UseRegions                       = true;  // If false, suppresses the use of #region
    Settings.UseNamespace                     = true;  // If false, suppresses the writing of a namespace
    Settings.UsePragma                        = false; // If false, suppresses the writing of #pragma
    Settings.AllowNullStrings                 = false; // If true, will allow string? properties and will add '#nullable enable' to the top of each file
    Settings.UseResharper                     = true;  // If true, will add a list of 'ReSharper disable' comments to the top of each file
    Settings.ShowLicenseInfo                  = false; // If true, will add the licence info comment to the top of each file
    Settings.IncludeConnectionSettingComments = false; // Add comments describing connection settings used to generate file
    Settings.IncludeCodeGeneratedAttribute    = false; // If true, will include the [GeneratedCode] attribute before classes, false to remove it.
    Settings.IncludeColumnsWithDefaults       = true;  // If true, will set properties to the default value from the database.

    // Enumerations ***********************************************************************************************************************
    // Create enumerations from database tables
    // List the enumeration tables you want read and generated for
    // Also look at the AddEnum callback below to add your own during reverse generation of tables.
    Settings.Enumerations = new List<EnumerationSettings>
    {
        // Example
        /*new EnumerationSettings
        {
            Name       = ""DaysOfWeek"",          // Enum to generate. e.g. ""DaysOfWeek"" would result in ""public enum DaysOfWeek {...}"" if the GroupField is set to a value then {GroupField} must be used in this name. e.g. ""DaysOfWeek{GroupField}""
            Table      = ""EnumTest.DaysOfWeek"", // Database table containing enum values. e.g. ""DaysOfWeek""
            NameField  = ""TypeName"",            // Column containing the name for the enum. e.g. ""TypeName""
            ValueField = ""TypeId"",              // Column containing the values for the enum. e.g. ""TypeId""
            GroupField = string.Empty           // [optional] Column containing the group name for the enum. This is used if multiple Enums are in the same table. if this is populated, use {GroupField} in the Name property. e.g. ""{GroupField}Enum""
        },
        new EnumerationSettings
        {
            Name       = ""CarOptions"",
            Table      = ""car_options"",
            NameField  = ""enum_name"",
            ValueField = ""value""
        }*/
        // Code will be generated as:
        // public enum Name
        // {
        //     NameField = ValueField,
        //     etc
        // }
    };

    // Use the following list to add use of HiLo sequences for identity columns
    Settings.HiLoSequences = new List<HiLoSequence>
    {
        // Example
        // For column dbo.match_table_name.match_column_name this will use .UseHiLo(""sequence_name"", ""sequence_schema"") instead of .UseSqlServerIdentityColumn()
        /*new HiLoSequence
        {
            Schema         = ""dbo"",
            Table          = ""match_table_name"", // Use * to match all tables
            SequenceName   = ""sequence_name"",
            SequenceSchema = ""sequence_schema""
        },
        new HiLoSequence
        {
            Schema         = ""dbo"",
            Table          = ""Employees"",
            SequenceName   = ""EmployeeSequence"",
            SequenceSchema = ""dbo""
        }*/
    };

    // Column modification ****************************************************************************************************************
    // Use the following list to replace column byte types with Enums.
    // As long as the type can be mapped to your new type, all is well.
    Settings.AddEnumDefinitions = delegate(List<EnumDefinition> enumDefinitions)
    {
        // Examples:
        //enumDefinitions.Add(new EnumDefinition { Schema = Settings.DefaultSchema, Table = ""match_table_name"", Column = ""match_column_name"", EnumType = ""name_of_enum"" });

        // This will replace OrderHeader.OrderStatus type to be an OrderStatusType enum
        //enumDefinitions.Add(new EnumDefinition { Schema = Settings.DefaultSchema, Table = ""OrderHeader"", Column = ""OrderStatus"", EnumType = ""OrderStatusType"" }); 

        // This will replace any table *.OrderStatus type to be an OrderStatusType enum
        //enumDefinitions.Add(new EnumDefinition { Schema = Settings.DefaultSchema, Table = ""*"", Column = ""OrderStatus"", EnumType = ""OrderStatusType"" });
    };

    // StoredProcedure return types *******************************************************************************************************
    // Override generation of return models for stored procedures that return entities.
    // If a stored procedure returns an entity, add it to the list below.
    // This will suppress the generation of the return model, and instead return the entity.
    // Example:                                 Proc name      Return this entity type instead
    // Settings.StoredProcedureReturnTypes.Add(""SalesByYear"", ""SummaryOfSalesByYear"");

    // Additional attributes on on reverse navigation and foreign key properties **********************************************************
    // If you need to serialise your entities with the JsonSerializer from Newtonsoft, this would serialise
    // all properties including the Reverse Navigation and Foreign Keys. The simplest way to exclude them is
    // to use the data annotation [JsonIgnore] on reverse navigation and foreign keys.
    // For more control, take a look at ForeignKeyAnnotationsProcessing() further down
    Settings.AdditionalReverseNavigationsDataAnnotations = new string[]
    {
        // ""JsonIgnore"" // Also add ""Newtonsoft.Json"" to the AdditionalNamespaces array above
    };

    Settings.AdditionalForeignKeysDataAnnotations = new string[]
    {
        // ""JsonIgnore"" // Also add ""Newtonsoft.Json"" to the AdditionalNamespaces array above
    };

 
    // Reference other namespaces *********************************************************************************************************
    // Use these namespaces to specify where the different elements now live. These may even be in different assemblies.
    // NOTE: These are only used if ElementsToGenerate is not set to generate all the elements.
    // Please note the following does not create the files in these locations, it only adds a using statement to say where they are.
    // The way to generate files in other folders is to either specify sub-folders above (See Settings.PocoFolder),
    // or add the ""EntityFramework Reverse POCO Code First Generator"" into each of the folders you require, then
    // set your <database>.tt files to only generate the relevant section(s) you need by setting:
    // Then set the .tt to only generate the relevant section you need by setting:
    //      ElementsToGenerate = Elements.Poco; in your Entity folder,
    //      ElementsToGenerate = Elements.Context | Elements.Interface; in your Context folder,
    //      ElementsToGenerate = Elements.PocoConfiguration; in your Configuration folder.
    // You also need to set the following to the namespace where they now live:
    Settings.ContextNamespace           = """"; // ""YourProject.Data"";
    Settings.InterfaceNamespace         = """"; // ""YourProject.Data"";
    Settings.PocoNamespace              = """"; // ""YourProject.Data.Entities"";
    Settings.PocoConfigurationNamespace = """"; // ""YourProject.Data.Configuration"";


    // Schema *****************************************************************************************************************************
    // If there are multiple schemas, then the table name is prefixed with the schema, except for dbo.
    // Ie. dbo.hello will be Hello.
    //     abc.hello will be Abc_Hello.
    Settings.PrependSchemaName = true; // Control if the schema name is prepended to the table name

    // Table Suffix ***********************************************************************************************************************
    // Appends the suffix to the generated classes names
    // Ie. If TableSuffix is ""Dto"" then Order will be OrderDto
    //     If TableSuffix is ""Entity"" then Order will be OrderEntity
    Settings.TableSuffix = null;

    // Call-backs *************************************************************************************************************************

    // AddRelationship is a helper function that creates ForeignKey objects and adds them to the foreignKeys list
    Settings.AddExtraForeignKeys = delegate(IDbContextFilter filter, Generator gen, List<ForeignKey> foreignKeys, Tables tablesAndViews) 
    {
        // Northwind example:
            
        // [Orders] (Table) to [Invoices] (View) is one-to-many using Orders.OrderID = Invoices.OrderID
        // gen.AddRelationship(filter, foreignKeys, tablesAndViews, ""orders_to_invoices"", Settings.DefaultSchema, ""Orders"", ""OrderID"", ""dbo"", ""Invoices"", ""OrderID"", null, null, true);
            
        // [Orders] (Table) to [Orders Qry] (View) is one-to-zeroOrOne ( [Orders].OrderID = [Orders Qry].OrderID )
        // gen.AddRelationship(filter, foreignKeys, tablesAndViews, ""orders_to_ordersQry"", Settings.DefaultSchema, ""Orders"", ""OrderID"", ""dbo"", ""Orders Qry"", ""OrderID"", ""ParentFkName"", ""ChildFkName"", true);
            
        // [Order Details] (Table) to [Invoices] (View) is one-to-zeroOrOne - but uses a composite-key: ( [Order Details].OrderID,ProductID = [Invoices].OrderID,ProductID )
        // gen.AddRelationship(filter, foreignKeys, tablesAndViews, ""orderDetails_to_invoices"", Settings.DefaultSchema, ""Order Details"", new[] { ""OrderID"", ""ProductID"" }, ""dbo"", ""Invoices"", new[] { ""OrderID"", ""ProductID"" }, null, null, true);
    };

    // Table renaming (single context generation only) ************************************************************************************
    // Use the following function to rename tables such as tblOrders to Orders, Shipments_AB to Shipments, etc.
    Settings.TableRename = delegate(string name, string schema, bool isView)
    {
        // Example
        //if (name.StartsWith(""tbl""))
        //    name = name.Remove(0, 3);
        //name = name.Replace(""_AB"", """");

        //if(isView)
        //    name = name + ""View"";

        // If you turn pascal casing off (UsePascalCase = false), and use the pluralisation service, and some of your
        // tables names are all UPPERCASE, some words ending in IES such as CATEGORIES get singularised as CATEGORy.
        // Therefore you can make them lowercase by using the following
        //return Inflector.MakeLowerIfAllCaps(name);

        // If you are using the pluralisation service and you want to rename a table, make sure you rename the table to the plural form.
        // For example, if the table is called Treez (with a z), and your pluralisation entry is
        //     new CustomPluralizationEntry(""Tree"", ""Trees"")
        // Use this TableRename function to rename Treez to the plural (not singular) form, Trees:
        //if (name == ""Treez"") return ""Trees"";

        return name;
    };

    // Use the following function if you need to apply additional modifications to a table
    // Called just before UpdateColumn
    Settings.UpdateTable = delegate(Table table)
    {
        // Add an extra comment
        //if (table.NameHumanCase.Equals(""SomeTable"", StringComparison.InvariantCultureIgnoreCase))
        //    table.AdditionalComment = ""Example comment"";

        // To add a base class to a table based on any of these column names. Also see Settings.UpdateColumn below.
        //var tracking  = new List<string> { ""createdby"", ""createdon"", ""modifiedby"", ""modifiedon"" };
        //var replicate = new List<string> { ""uniqueid"", ""hrid""};
        //if (table.Columns.Any(x => tracking.Contains(x.NameHumanCase)))
        //    table.BaseClasses = "" : TrackingEntity"";
        //if (table.Columns.Any(x => replicate.Contains(x.NameHumanCase)))
        //    table.BaseClasses = "" : ReplicateEntity"";

        // To add a base class to a table based on ALL of these column names
        //var lookupTitle = new List<string>  { ""ID"", ""Title"", ""Description"" };
	    //if (lookupTitle.All(x => table.Columns.Any(c => c.NameHumanCase == x)))
        //{
        //    if(string.IsNullOrEmpty(table.BaseClasses))
		//        table.BaseClasses = "" : ILookupTitle"";
	    //    else
		//        table.BaseClasses += "", ILookupTitle"";
        //}

        // Add a base class to a table based on its name
        //var quickEdit = new List<string>  { ""Categories"", ""Customers"", ""Employees"", ""Orders"" };
        //if(quickEdit.Any(x => table.NameHumanCase == x))
        //{
        //    if(string.IsNullOrEmpty(table.BaseClasses))
		//        table.BaseClasses = "" : IQuickEdit"";
	    //    else
		//        table.BaseClasses += "", IQuickEdit"";
        //}

        // To add attributes
        //table.Attributes.Add(""[Serializable]"");
    };

    // Use the following function if you need to apply additional modifications to a column
    // eg. normalise names etc.
    Settings.UpdateColumn = delegate(Column column, Table table, List<EnumDefinition> enumDefinitions)
    {
        // Rename column
        //if (column.IsPrimaryKey && column.NameHumanCase == ""PkId"")
        //    column.NameHumanCase = ""Id"";

        // .IsConcurrencyToken() must be manually configured. However .IsRowVersion() can be automatically detected.
        //if (table.NameHumanCase.Equals(""SomeTable"", StringComparison.InvariantCultureIgnoreCase) && column.NameHumanCase.Equals(""SomeColumn"", StringComparison.InvariantCultureIgnoreCase))
        //    column.IsConcurrencyToken = true;

        // Remove table name from primary key
        //if (column.IsPrimaryKey && column.NameHumanCase.Equals(table.NameHumanCase + ""Id"", StringComparison.InvariantCultureIgnoreCase))
        //    column.NameHumanCase = ""Id"";

        // Remove column from poco class as it will be inherited from a base class. Also see Settings.UpdateTable above.
        //var tracking  = new List<string> { ""createdby"", ""createdon"", ""modifiedby"", ""modifiedon"" };
        //var replicate = new List<string> { ""uniqueid"", ""hrid""};
        //if (tracking .Contains(column.NameHumanCase.ToLower()) ||
        //    replicate.Contains(column.NameHumanCase.ToLower()))
        //{
        //    column.ExistsInBaseClass = true; // If true, does not generate the property for this column as it will exist in a base class
        //}

        // Use the extended properties to perform tasks to column
        //if (column.ExtendedProperty == ""HIDE"")
        //    column.Hidden = true; // Hidden means the generator does not generate any code for this column at all.

        // Apply the ""override"" access modifier to a specific column.
        //if (column.NameHumanCase == ""id"")
        //    column.OverrideModifier = true;
        // This will create: public override long id { get; set; }

        if (Settings.UseDataAnnotations)
        {
             if (column.IsPrimaryKey)
                column.Attributes.Add(string.Format(""[Key, Column(Order = {0})]"", column.Ordinal));

            if (column.IsMaxLength) 
                column.Attributes.Add(""[MaxLength]"");

            if (column.IsRowVersion)
                column.Attributes.Add(""[Timestamp, ConcurrencyCheck]"");

            if (!column.IsMaxLength && column.MaxLength > 0)
            { 
                var doNotSpecifySize = (Settings.DatabaseType == DatabaseType.SqlCe && column.MaxLength > 4000);
                column.Attributes.Add(doNotSpecifySize ? ""[MaxLength]"" : string.Format(""[MaxLength({0})]"", column.MaxLength));
                if (column.PropertyType.Equals(""string"", StringComparison.InvariantCultureIgnoreCase))
                    column.Attributes.Add(string.Format(""[StringLength({0})]"", column.MaxLength));
            }

            if (!column.IsNullable && !column.IsComputed)
            {
                if (column.PropertyType.Equals(""string"", StringComparison.InvariantCultureIgnoreCase) && column.AllowEmptyStrings)
                    column.Attributes.Add(""[Required(AllowEmptyStrings = true)]"");
                else
                    column.Attributes.Add(""[Required]"");
            }

            column.Attributes.Add(string.Format(""[Display(Name = \""{0}\"")]"", column.DisplayName));
        }

        // Perform Enum property type replacement
        if (enumDefinitions != null)
        {
            var enumDefinition = enumDefinitions.FirstOrDefault(e =>
                (e.Schema.Equals(table.Schema.DbName, StringComparison.InvariantCultureIgnoreCase)) && 
                (e.Table == ""*"" || e.Table.Equals(table.DbName, StringComparison.InvariantCultureIgnoreCase) || e.Table.Equals(table.NameHumanCase, StringComparison.InvariantCultureIgnoreCase)) &&
                (e.Column.Equals(column.DbName, StringComparison.InvariantCultureIgnoreCase) || e.Column.Equals(column.NameHumanCase, StringComparison.InvariantCultureIgnoreCase)));

            if (enumDefinition != null)
            {
                column.PropertyType = enumDefinition.EnumType;
                if (!string.IsNullOrEmpty(column.Default))
                    column.Default = ""("" + enumDefinition.EnumType + "") "" + column.Default;
            }
        }
    };

    // In order to use this function, Settings.ElementsToGenerate must contain both Elements.Poco and Elements.Enum;
    Settings.AddEnum = delegate (Table table)
    {
        /*if (table.HasPrimaryKey && table.PrimaryKeys.Count() == 1 && table.Columns.Any(x => x.PropertyType == ""string""))
        {
            // Example: choosing tables with certain naming conventions for enums. Please use your own conventions.
            if (table.NameHumanCase.StartsWith(""Enum"", StringComparison.InvariantCultureIgnoreCase) ||
                table.NameHumanCase.EndsWith(""Enum"", StringComparison.InvariantCultureIgnoreCase))
            {
                try
                {
                    Settings.Enumerations.Add(new EnumerationSettings
                    {
                        Name       = table.NameHumanCase.Replace(""Enum"","""").Replace(""Enum"","""") + ""Enum"",
                        Table      = table.Schema.DbName + ""."" + table.DbName,
                        NameField  = table.Columns.First(x => x.PropertyType == ""string"").DbName, // Or specify your own
                        ValueField = table.PrimaryKeys.Single().DbName, // Or specify your own
                        GroupField = string.Empty // Or specify your own
                    });

                    // This will cause this table to not be reverse-engineered.
                    // This means it was only required to generate an enum and can now be removed.
                    table.RemoveTable = true; // Remove this line if you want to keep it in your dbContext.
                }
                catch
                {
                    // Swallow exception
                }
            }
        }*/
    };

    // Use the following function if you need to apply additional modifications to a enum
    // Called just before UpdateEnumMember
    Settings.UpdateEnum = delegate (Enumeration enumeration)
    {
        //enumeration.EnumAttributes.Add(""[DataContract]"");
    };

    // Use the following function if you need to apply additional modifications to a enum member
    Settings.UpdateEnumMember = delegate (EnumerationMember enumerationMember)
    {
        //enumerationMember.Attributes.Add(""[EnumMember]"");
        //enumerationMember.Attributes.Add(""[SomeAttribute(\"""" + enumerationMember.AllValues[""SomeName""] + "" \"")]"");
    };


    // Writes any boilerplate stuff inside the POCO class body
    Settings.WriteInsideClassBody = delegate(Table t)
    {
        // Example:
        //return ""    // "" + t.NameHumanCase + Environment.NewLine;

        // Do nothing by default
        return string.Empty;
    };

    // Using Views *****************************************************************************************************************
    // SQL Server does not support the declaration of primary-keys in VIEWs. Entity Framework's EDMX designer (and this generator)
    // assume that all non-null columns in a VIEW are primary-key columns, this will be incorrect for most non-trivial applications.
    // This callback will be invoked for each VIEW found in the database. Use it to declare which columns participate in that VIEW's
    // primary-key by setting 'IsPrimaryKey = true'.
    // If no columns are marked with 'IsPrimaryKey = true' then this T4 template defaults to marking all non-NULL columns as primary key columns.
    Settings.ViewProcessing = delegate(Table view)
    {
        // Below is example code for the Northwind database that configures the 'VIEW [Orders Qry]' and 'VIEW [Invoices]'
        /*switch (view.DbName)
        {
            case ""Orders Qry"":
                // VIEW [Orders Qry] uniquely identifies rows with the 'OrderID' column:
                view.Columns.Single(col => col.DbName == ""OrderID"").IsPrimaryKey = true;
                break;

            case ""Invoices"":
                // VIEW [Invoices] has a composite primary key (OrderID+ProductID), so both columns must be marked as a Primary Key:
                foreach (var col in view.Columns.Where(c => c.DbName == ""OrderID"" || c.DbName == ""ProductID""))
                    col.IsPrimaryKey = true;
                break;
        }*/
    };

    // StoredProcedure renaming ************************************************************************************************************
    // Use the following function to rename stored procs such as sp_CreateOrderHistory to CreateOrderHistory, my_sp_shipments to Shipments, etc.
    Settings.StoredProcedureRename = delegate(StoredProcedure sp)
    {
        // Example:
        //if (sp.NameHumanCase.StartsWith(""sp_""))
        //    return sp.NameHumanCase.Remove(0, 3);
        //return sp.NameHumanCase.Replace(""my_sp_"", """");

        return sp.NameHumanCase; // Do nothing by default
    };

    // Use the following function to rename the return model automatically generated for stored procedure.
    // By default it's <proc_name>ReturnModel.
    Settings.StoredProcedureReturnModelRename = delegate(string name, StoredProcedure sp)
    {
        // Example:
        //if (sp.NameHumanCase.Equals(""ComputeValuesForDate"", StringComparison.InvariantCultureIgnoreCase))
        //    return ""ValueSet"";
        //if (sp.NameHumanCase.Equals(""SalesByYear"", StringComparison.InvariantCultureIgnoreCase))
        //    return ""SalesSet"";

        return name; // Do nothing by default
    };

    // Mapping Table renaming *********************************************************************************************************************
    // By default, name of the properties created relate to the table the foreign key points to and not the mapping table.
    // Use the following function to rename the properties created by ManyToMany relationship tables especially if you have 2 relationships between the same tables.
    // Example:
    Settings.MappingTableRename = delegate(string mappingTable, string tableName, string entityName)
    {
        // Example: If you have two mapping tables such as one being UserRequiredSkills snd one being UserOptionalSkills, this would change the name of one property
        //if (mappingTable == ""UserRequiredSkills"" && tableName == ""User"")
        //    return ""RequiredSkills"";

        // or if you want to give the same property name on both classes
        //if (mappingTable == ""UserRequiredSkills"")
        //    return ""UserRequiredSkills"";

        return entityName;
    };

    Settings.ForeignKeyName = delegate(string tableName, ForeignKey foreignKey, string foreignKeyName, Relationship relationship, short attempt)
    {
        string fkName;

        // 5 Attempts to correctly name the foreign key
        switch (attempt)
        {
            case 1:
                // Try without appending foreign key name
                fkName = tableName;
                break;

            case 2:
                // Only called if foreign key name ends with ""id""
                // Use foreign key name without ""id"" at end of string
                fkName = foreignKeyName.Remove(foreignKeyName.Length - 2, 2);
                break;

            case 3:
                // Use foreign key name only
                fkName = foreignKeyName;
                break;

            case 4:
                // Use table name and foreign key name
                fkName = tableName + ""_"" + foreignKeyName;
                break;

            case 5:
                // Used in for loop 1 to 99 to append a number to the end
                fkName = tableName;
                break;

            default:
                // Give up
                fkName = tableName;
                break;
        }

        if (!Settings.UsePascalCase)
            fkName = DatabaseReader.CleanUp(fkName);

        // Apply custom foreign key renaming rules. Can be useful in applying pluralization.
        // For example:
        /*if (tableName == ""Employee"" && foreignKey.FkColumn == ""ReportsTo"")
            return ""Manager"";

        if (tableName == ""Territories"" && foreignKey.FkTableName == ""EmployeeTerritories"")
            return ""Locations"";

        if (tableName == ""Employee"" && foreignKey.FkTableName == ""Orders"" && foreignKey.FkColumn == ""EmployeeID"")
            return ""ContactPerson"";
        */

        // FK_TableName_FromThisToParentRelationshipName_FromParentToThisChildsRelationshipName
        // (e.g. FK_CustomerAddress_Customer_Addresses will extract navigation properties ""address.Customer"" and ""customer.Addresses"")
        // Feel free to use and change the following
        /*if (foreignKey.ConstraintName.StartsWith(""FK_"") && foreignKey.ConstraintName.Count(x => x == '_') == 3)
        {
            var parts = foreignKey.ConstraintName.Split('_');
            if (!string.IsNullOrWhiteSpace(parts[2]) && !string.IsNullOrWhiteSpace(parts[3]) && parts[1] == foreignKey.FkTableName)
            {
                if (relationship == Relationship.OneToMany)
                    fkName = parts[3];
                else if (relationship == Relationship.ManyToOne)
                    fkName = parts[2];
            }
        }*/

        return fkName;
    };

    // This foreign key filter used in addition to SingleContextFilter.ForeignKeyFilter()
    // Return null to exclude this foreign key
    Settings.ForeignKeyFilterFunc = delegate(ForeignKey fk)
    {
        // Return null to exclude this foreign key, or set IncludeReverseNavigation = false
        // to include the foreign key but not generate reverse navigation properties.
        // Example, to exclude all foreign keys for the Categories table, use:
        //if (fk.PkTableName == ""Categories"")
        //    return null;

        // Example, to exclude reverse navigation properties for tables ending with Type, use:
        //if (fk.PkTableName.EndsWith(""Type""))
        //    fk.IncludeReverseNavigation = false;

        // You can also change the access modifier of the foreign-key's navigation property:
        //if(fk.PkTableName == ""Categories"")
        //     fk.AccessModifier = ""internal"";

        return fk;
    };

    Settings.ForeignKeyAnnotationsProcessing = delegate(Table fkTable, Table pkTable, string propName, string fkPropName)
    {
        // Example:
        // Each navigation property that is a reference to User are left intact
        //if(pkTable.NameHumanCase.Equals(""User"") && propName.Equals(""User""))
        //    return null;

        // all the others are marked with this attribute
        //return new[] { ""System.Runtime.Serialization.IgnoreDataMember"" };

        return null;
    };


    // Generate multiple db contexts in a single go ***************************************************************************************
    // Generating multiple contexts at a time requires you specifying which tables, and columns to generate for each context.
    // As this generator can now generate multiple db contexts in a single go, filtering is done a per db context, and no longer global.
    // If GenerateSingleDbContext = true (default), please modify SingleContextFilter, this is where your previous global settings should go.
    // If GenerateSingleDbContext = false, this will generate multiple db contexts. Please read https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/wiki/Generating-multiple-database-contexts-in-a-single-go
    Settings.GenerateSingleDbContext              = true; // Set this to false to generate multiple db contexts.
    Settings.MultiContextSettingsConnectionString = """"; // Leave empty to read data from same database in ConnectionString above. If settings are in another database, specify the connection string here.
    Settings.MultiContextSettingsPlugin           = """"; // Only used for unit testing Generator project as you can't (yet) inherit from IMultiContextSettingsPlugin. ""c:\\Path\\YourMultiDbSettingsReader.dll,Full.Name.Of.Class.Including.Namespace"". This will allow you to specify a pluggable provider for reading your MultiContext settings.
    Settings.MultiContextAttributeDelimiter       = '~'; // The delimiter used for splitting MultiContext attributes

    Settings.MultiContextAllFieldsColumnProcessing = delegate (Column column, Table table, Dictionary<string, object> allFields)
    {
        // Examples of how to use additional custom fields from the MultiContext.[Column] table
        // INT example
        /*if (allFields.ContainsKey(""DummyInt""))
        {
            var o = allFields[""DummyInt""];
            column.ExtendedProperty += string.Format("" DummyInt = {0}"", (int) o);
        }*/

        // VARCHAR example
        /*if (allFields.ContainsKey(""Test""))
        {
            var o = allFields[""Test""];
            column.ExtendedProperty += string.Format("" Test = {0}"", o.ToString());
        }*/

        // DATETIME example
        /*if (allFields.ContainsKey(""date_of_birth""))
        {
            var o = allFields[""date_of_birth""];
            var date = Convert.ToDateTime(o);
            column.ExtendedProperty += string.Format("" date_of_birth = {0}"", date.ToLongDateString());
        }*/
    };

    Settings.MultiContextAllFieldsTableProcessing = delegate (Table table, Dictionary<string, object> allFields)
    {
        // Examples of how to use additional custom fields from the MultiContext.[Table] table
        // VARCHAR example
        /*if (allFields.ContainsKey(""Notes""))
        {
            var o = allFields[""Notes""];
            if (string.IsNullOrEmpty(table.AdditionalComment))
                table.AdditionalComment = string.Empty;

            table.AdditionalComment += string.Format("" Test = {0}"", o.ToString());
        }*/
    };

    Settings.MultiContextAllFieldsStoredProcedureProcessing = delegate (StoredProcedure sp, Dictionary<string, object> allFields)
    {
        // Examples of how to use additional custom fields from the MultiContext.[Table] table
        // VARCHAR example
        /*if (allFields.ContainsKey(""CustomRename""))
        {
            var o = allFields[""CustomRename""];
            sp.NameHumanCase = o.ToString();
        }*/
    };

    Settings.MultiContextAllFieldsFunctionProcessing = delegate (StoredProcedure sp, Dictionary<string, object> allFields)
    {
        // Examples of how to use additional custom fields from the MultiContext.[Table] table
        // VARCHAR example
        /*if (allFields.ContainsKey(""CustomRename""))
        {
            var o = allFields[""CustomRename""];
            sp.NameHumanCase = o.ToString();
        }*/
    };


    // Don't forget to take a look at SingleContextFilter and FilterSettings classes!
    // That's it, nothing else to configure ***********************************************************************************************

    FilterSettings.CheckSettings();

    // How to add custom pluralisation entries
    /*var customPluralisationEntries = new List<CustomPluralizationEntry>
    {
        new CustomPluralizationEntry(""Tree"", ""Trees""),
        new CustomPluralizationEntry(""Order"", ""Orders"")
    };
    Inflector.PluralisationService = new EnglishPluralizationService(customPluralisationEntries);*/
    Inflector.IgnoreWordsThatEndWith = new List<string> { ""Status"", ""To"", ""Data"" };
    Inflector.PluralisationService = new EnglishPluralizationService(); // To disable pluralisation, set this to null

    var outer = (GeneratedTextTransformation) this;

    // Show where the machine.config file is
    // outer.WriteLine(""// "" + System.Runtime.InteropServices.RuntimeEnvironment.SystemConfigurationFile);

    var fileManagement = new FileManagementService(outer);
    var generator = GeneratorFactory.Create(fileManagement, FileManagerFactory.GetFileManagerType());
    if (generator != null && generator.InitialisationOk)
    {
        generator.ReadDatabase();
        generator.GenerateCode();
    }
    fileManagement.Process(true);#>";

            using (var tt = File.CreateText(Path.Combine(ttRoot, "Database.tt")))
            {
                var settings = File.ReadAllText(Path.Combine(generatorRoot, "Settings.cs")).Trim();

                tt.WriteLine("<#@ include file=\"EF.Reverse.POCO.v3.ttinclude\" #>");
                tt.WriteLine("<#");
                tt.WriteLine("    // v" + _version);
                tt.WriteLine("    // Please make changes to the settings below.");
                tt.WriteLine("    // All you have to do is save this file, and the output file(s) are generated. Compiling does not regenerate the file(s).");
                tt.WriteLine("    // A course for the older v2 generator is available on Pluralsight at https://www.pluralsight.com/courses/code-first-entity-framework-legacy-databases");
                tt.WriteLine("");

                settings = settings.Remove(0, settings.IndexOf("    public static class")); // Remove text before class declaration
                settings = settings.Remove(settings.Length - 1, 1); // Remove closing namespace brace
                //tt.WriteLine(settings);

                tt.WriteLine(footer);
            }
        }

        private static void UpdateEfrpgVersion(string filename)
        {
            var body = $@"namespace Efrpg
{{
    public static class EfrpgVersion
    {{
        public static string Version()
        {{
            return ""v{_version}"";
        }}
    }}
}}";
            // C:\S\Source (open source)\EntityFramework-Reverse-POCO-Code-Generator\Generator\EfrpgVersion.cs
            File.WriteAllText(filename, body, Encoding.UTF8);
        }

        private static void CreateCoreTTInclude(string generatorRoot, string ttRoot)
        {
            string header1 = @"<#
// Copyright (C) Simon Hughes 2012
" + "// v" + _version + @"
// If you want to submit a pull request, please modify the Generator C# project as this file
// is automatically constructed from the C# Generator project during the build process.
#>
<#@ template debug=""true"" hostspecific=""true"" language=""C#"" #>
<#@ include file=""EF6.Utility.CS.ttinclude""#><#@ assembly name=""System.Configuration"" #>
<#@ assembly name=""System.Windows.Forms"" #>
<#@ import namespace=""System.Data.Entity.Infrastructure.Pluralization"" #>";

            const string header2 = @"<#@ import namespace=""EnvDTE"" #>
<#@ import namespace=""Microsoft.VisualStudio.TextTemplating"" #>
<#@ output extension="".cs"" encoding=""utf-8"" #>
<#
        // WriteLine(""// T4 framework version = "" + AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName);
        var DefaultNamespace = new CodeGenerationTools(this).VsNamespaceSuggestion() ?? ""DebugMode"";
        Settings.Root = Host.ResolvePath(string.Empty);
        Settings.TemplateFile = Path.GetFileNameWithoutExtension(DynamicTextTransformation.Create(this).Host.TemplateFile);
        // System.Diagnostics.Debugger.Launch();
#><#+";

            const string footer = @"
    public static void ArgumentNotNull<T>(T arg, string name) where T : class
    {
        if (arg == null)
        {
            throw new ArgumentNullException(name);
        }
    }
#>";
            var fileReaderStrategy = new FileReaderStrategy();
            string[] ignoreFolders = { "\\bin", "\\obj" };
            string[] ignoreFiles =
            {
                "AssemblyInfo.cs",
                "EntityFrameworkTemplateFileManager.cs",
                "GeneratedTextTransformation.cs",

                "GlobalSuppressions.cs", // Resharper
            };
            var filesToListFirst = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Settings.cs"),
                new KeyValuePair<int, string>(2, "FilterSettings.cs"),
                new KeyValuePair<int, string>(3, "SingleContextFilter.cs")
            };
            var files = Directory
                .GetFiles(generatorRoot, "*.cs", SearchOption.AllDirectories)
                .OrderBy(x => x)
                .ToList();

            var filesToListFirstReaders = new List<KeyValuePair<int, IFileReader>>();
            var remainingFileReaders = new List<IFileReader>();

            foreach (var file in files)
            {
                var skip      = false;
                var listFirst = false;
                var path      = Path.GetDirectoryName(file);
                var filename  = Path.GetFileName(file);

                foreach (var ignore in ignoreFolders)
                {
                    if (path.Contains(ignore))
                        skip = true;
                }
                if (skip)
                    continue;

                foreach (var ignore in ignoreFiles)
                {
                    if (filename == ignore)
                        skip = true;
                }
                if (skip)
                    continue;

                var order = 0;
                foreach (var fileToListFirst in filesToListFirst)
                {
                    if (filename == fileToListFirst.Value)
                    {
                        listFirst = true;
                        order = fileToListFirst.Key;
                    }
                }

                var fileReader = new FileReader(fileReaderStrategy);
                if (fileReader.ReadFile(file))
                {
                    if (listFirst)
                        filesToListFirstReaders.Add(new KeyValuePair<int, IFileReader>(order, fileReader));
                    else
                        remainingFileReaders.Add(fileReader);
                }
            }

            var fileReaders = new List<IFileReader>();
            fileReaders.AddRange(filesToListFirstReaders.OrderBy(x => x.Key).Select(x => x.Value));
            fileReaders.AddRange(remainingFileReaders);

            using (var tt = File.CreateText(Path.Combine(ttRoot, "EF.Reverse.POCO.v3.ttinclude")))
            {
                var writerStrategy = new TTWriterStrategy();
                var writer = new FileWriter(tt, writerStrategy, fileReaders);

                tt.WriteLine(header1);
                writer.WriteUsings();
                tt.WriteLine(header2);
                writer.WriteCode();
                tt.WriteLine(footer);
            }
        }
    }
}