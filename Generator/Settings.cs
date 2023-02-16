using System;
using System.Collections.Generic;
using System.Linq;
using Efrpg.Filtering;
using Efrpg.Generators;
using Efrpg.LanguageMapping;
using Efrpg.Templates;

namespace Efrpg
{
    public static class Settings
    {
        // Main settings **********************************************************************************************************************
        // The following entries are the only required settings.
        public static DatabaseType DatabaseType                         = DatabaseType.SqlServer; // SqlServer, SqlCe, PostgreSQL. Coming next: MySql, Oracle
        public static TemplateType TemplateType                         = TemplateType.EfCore7; // EfCore7, EfCore6, EfCore5, EfCore3, EfCore2, Ef6, FileBasedCore2-7. FileBased specify folder using Settings.TemplateFolder
        public static GeneratorType GeneratorType                       = GeneratorType.EfCore; // EfCore, Ef6, Custom. Custom edit GeneratorCustom class to provide your own implementation
        public static ForeignKeyNamingStrategy ForeignKeyNamingStrategy = ForeignKeyNamingStrategy.Legacy; // Please use Legacy for now, Latest (not yet ready)
        public static bool UseMappingTables                             = false; // Can only be set to true for EF6. If true, mapping will be used and no mapping tables will be generated. If false, all tables will be generated.
        public static FileManagerType FileManagerType                   = FileManagerType.EfCore; // .NET Core project = EfCore; .NET 4.x project = VisualStudio; No output (testing only) = Null
        public static string ConnectionString                           = ""; // This is used by the generator to reverse engineer your database
        public static string ConnectionStringName                       = "MyDbContext"; // ConnectionString key as specified in your app.config/web.config/appsettings.json
        public static string DbContextName                              = "MyDbContext"; // Class name for the DbContext to be generated.
        public static bool GenerateSeparateFiles                        = false;
        public static string Namespace                                  = typeof(Settings).Namespace; // Override the default namespace here. Example: Namespace = "CustomNamespace";
        public static string TemplateFolder                             = ""; // Only used if Settings.TemplateType = TemplateType.FileBased. Specify folder name where the mustache folders can be found. Please read https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/wiki/Custom-file-based-templates
        public static bool AddUnitTestingDbContext                      = true; // Will add a FakeDbContext and FakeDbSet for easy unit testing

        // Elements to generate ***************************************************************************************************************
        // Add the elements that should be generated when the template is executed.
        // Multiple projects can now be used that separate the different concerns.
        public static Elements ElementsToGenerate = Elements.Poco | Elements.Context | Elements.Interface | Elements.PocoConfiguration | Elements.Enum;

        // Generate files in sub-folders ******************************************************************************************************
        // Only activated if Settings.FileManagerType = FileManagerType.EfCore && Settings.GenerateSeparateFiles = true
        public static string ContextFolder           = ""; // Sub-folder you would like your DbContext to be added to.              e.g. @"Data"
        public static string InterfaceFolder         = ""; // Sub-folder you would like your Interface to be added to.              e.g. @"Data\Interface"
        public static string PocoFolder              = ""; // Sub-folder you would like your Poco's to be added to.                 e.g. @"Data\Entities"
        public static string PocoConfigurationFolder = ""; // Sub-folder you would like your Configuration mappings to be added to. e.g. @"Data\Configuration"


        public static int    CommandTimeout                         = 600; // SQL Command timeout in seconds. 600 is 10 minutes, 0 will wait indefinitely. Some databases can be slow retrieving schema information.
        public static string DbContextInterfaceBaseClasses          = "IDisposable"; // Specify what the base classes are for your database context interface
        public static string DbContextBaseClass                     = "DbContext"; // Specify what the base class is for your DbContext. For ASP.NET Identity use "IdentityDbContext<ApplicationUser>";
        public static OnConfiguration OnConfiguration               = OnConfiguration.ConnectionString; // EFCore only. Determines the code generated within DbContext.OnConfiguration(). Please read https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/wiki/Settings.OnConfiguration
        public static bool   AddParameterlessConstructorToDbContext = true; // If true, then DbContext will have a default (parameter-less) constructor which automatically passes in the connection string name, if false then no parameter-less constructor will be created.
        public static string ConfigurationClassName                 = "Configuration"; // Configuration, Mapping, Map, etc. This is appended to the Poco class name to configure the mappings.
        public static string DatabaseReaderPlugin                   = ""; // Eg, "c:\\Path\\YourDatabaseReader.dll,Full.Name.Of.Class.Including.Namespace". See #501. This will allow you to specify a pluggable provider for reading your database.

        public static string EntityClassesModifiers        = "public"; // "public partial";
        public static string ConfigurationClassesModifiers = "public"; // "public partial";
        public static string DbContextClassModifiers       = "public"; // "public partial";
        public static string DbContextInterfaceModifiers   = "public"; // "public partial";
        public static string ResultClassModifiers          = "public"; // "public partial";

        public static bool UsePascalCase                            = true; // This will rename the generated C# tables & properties to use PascalCase. If false table & property names will be left alone.
        public static bool UsePascalCaseForEnumMembers              = true; // This will rename the generated Enum Members to use PascalCase. If false Enum members will be left alone.
        public static bool UseDataAnnotations                       = false; // If true, will add data annotations to the poco classes.
        public static bool UsePropertyInitialisers                  = false; // Removes POCO constructor and instead uses C# 6 property initialisers to set defaults
        public static bool UseLazyLoading                           = true; // Marks all navigation properties as virtual or not, to support or disable EF Lazy Loading feature
        public static bool UseInheritedBaseInterfaceFunctions       = false; // If true, the main DBContext interface functions will come from the DBContextInterfaceBaseClasses and not generated. If false, the functions will be generated.
        public static CommentsStyle IncludeComments                 = CommentsStyle.AtEndOfField; // Adds comments to the generated code
        public static CommentsStyle IncludeExtendedPropertyComments = CommentsStyle.InSummaryBlock; // Adds extended properties as comments to the generated code
        public static bool DisableGeographyTypes                    = false; // Turns off use of System.Data.Entity.Spatial.DbGeography and System.Data.Entity.Spatial.DbGeometry as OData doesn't support entities with geometry/geography types.
        public static string CollectionInterfaceType                = "ICollection"; //  = "System.Collections.Generic.List"; // Determines the declaration type of collections for the Navigation Properties. ICollection is used if not set.
        public static string CollectionType                         = "List"; // Determines the type of collection for the Navigation Properties. "ObservableCollection" for example. Add "System.Collections.ObjectModel" to AdditionalNamespaces if setting the CollectionType = "ObservableCollection".
        public static bool NullableShortHand                        = true; // true => T?, false => Nullable<T>
        public static bool AddIDbContextFactory                     = true; // Will add a default IDbContextFactory<DbContextName> implementation for easy dependency injection
        public static bool IncludeQueryTraceOn9481Flag              = false; // If SqlServer 2014 appears frozen / take a long time when this file is saved, try setting this to true (you will also need elevated privileges).
        public static bool UsePrivateSetterForComputedColumns       = true; // If the columns is computed, use a private setter.
        public static bool IncludeGeneratorVersionInCode            = false; // If true, will include the version number of the generator in the generated code (Settings.ShowLicenseInfo must also be true).
        public static bool TrimCharFields                           = false; // EF Core option only. EF Core option only. If true, will TrimEnd() 'char' fields when read from the database.
        public static List<string> AdditionalNamespaces             = new List<string>(); // To include extra namespaces, include them here. i.e. "Microsoft.AspNet.Identity.EntityFramework"
        public static List<string> AdditionalContextInterfaceItems  = new List<string>(); //  example: "void SetAutoDetectChangesEnabled(bool flag);"
        public static List<string> AdditionalFileHeaderText         = new List<string>(); // This will put additional lines verbatim at the top of each file under the comments, 1 line per entry
        public static List<string> AdditionalFileFooterText         = new List<string>(); // This will put additional lines verbatim at the end of each file above the // </auto-generated>, 1 line per entry

        // Language choices
        public static GenerationLanguage GenerationLanguage = GenerationLanguage.CSharp;
        public static string FileExtension                  = ".cs";

        // Code suppression *******************************************************************************
        public static bool UseRegions                       = true;  // If false, suppresses the use of #region
        public static bool UseNamespace                     = true;  // If false, suppresses the writing of a namespace
        public static bool UsePragma                        = false;  // If false, suppresses the writing of #pragma
        public static bool AllowNullStrings                 = false; // If true, will allow string? properties and will add '#nullable enable' to the top of each file
        public static bool UseResharper                     = false; // If true, will add a list of 'ReSharper disable' comments to the top of each file
        public static bool ShowLicenseInfo                  = false; // If true, will add the licence info comment to the top of each file
        public static bool IncludeConnectionSettingComments = false; // Add comments describing connection settings used to generate file
        public static bool IncludeCodeGeneratedAttribute    = false; // If true, will include the [GeneratedCode] attribute before classes, false to remove it.
        public static bool IncludeColumnsWithDefaults       = true;  // If true, will set properties to the default value from the database.ro

        // Create enumerations from database tables
        // List the enumeration tables you want read and generated for
        // Also look at the AddEnum callback below to add your own during reverse generation of tables.
        public static List<EnumerationSettings> Enumerations = new List<EnumerationSettings>
        {
            // Example
            /*new EnumerationSettings
            {
                Name       = "DaysOfWeek",          // Enum to generate. e.g. "DaysOfWeek" would result in "public enum DaysOfWeek {...}"
                Table      = "EnumTest.DaysOfWeek", // Database table containing enum values. e.g. "DaysOfWeek"
                NameField  = "TypeName",            // Column containing the name for the enum. e.g. "TypeName"
                ValueField = "TypeId"               // Column containing the values for the enum. e.g. "TypeId"
            },
            new EnumerationSettings
            {
                Name       = "CarOptions",
                Table      = "car_options",
                NameField  = "enum_name",
                ValueField = "value"
            }*/
            // Code will be generated as:
            // public enum Name
            // {
            //     NameField = ValueField,
            //     etc
            // }
        };

        // Use the following list to add use of HiLo sequences for identity columns
        public static List<HiLoSequence> HiLoSequences = new List<HiLoSequence>
        {
            // Example
            // For column dbo.match_table_name.match_column_name this will use .UseHiLo("sequence_name", "sequence_schema") instead of .UseSqlServerIdentityColumn()
            /*new HiLoSequence
            {
                Schema         = "dbo",
                Table          = "match_table_name",
                Column         = "match_column_name",
                SequenceName   = "sequence_name",
                SequenceSchema = "sequence_schema"
            },
            new HiLoSequence
            {
                Schema         = "dbo",
                Table          = "Employees",
                Column         = "EmployeeID",
                SequenceName   = "EmployeeSequence",
                SequenceSchema = "dbo"
            }*/
        };

        // If you need to serialise your entities with the JsonSerializer from Newtonsoft, this would serialise
        // all properties including the Reverse Navigation and Foreign Keys. The simplest way to exclude them is
        // to use the data annotation [JsonIgnore] on reverse navigation and foreign keys.
        // For more control, take a look at ForeignKeyAnnotationsProcessing() further down
        public static string[] AdditionalReverseNavigationsDataAnnotations = new string[]
        {
            // "JsonIgnore" // Also add "Newtonsoft.Json" to the AdditionalNamespaces array above
        };
        public static string[] AdditionalForeignKeysDataAnnotations = new string[]
        {
            // "JsonIgnore" // Also add "Newtonsoft.Json" to the AdditionalNamespaces array above
        };


        // Reference other namespaces *********************************************************************************************************
        // Use these namespaces to specify where the different elements now live. These may even be in different assemblies.
        // NOTE: These are only used if ElementsToGenerate is not set to generate all the elements.
        // Please note the following does not create the files in these locations, it only adds a using statement to say where they are.
        // The way to generate files in other folders is to either specify sub-folders above, or add the "EntityFramework Reverse POCO Code First Generator"
        // into each of the folders you require, then set your <database>.tt files to only generate the relevant section(s) you need by setting:
        //      ElementsToGenerate = Elements.Poco; in your Entity folder,
        //      ElementsToGenerate = Elements.Context | Elements.Interface; in your Context folder,
        //      ElementsToGenerate = Elements.PocoConfiguration; in your Configuration folder.
        // You also need to set the following to the namespace where they now live:
        public static string ContextNamespace           = ""; // "YourProject.Data";
        public static string InterfaceNamespace         = ""; // "YourProject.Data";
        public static string PocoNamespace              = ""; // "YourProject.Data.Entities";
        public static string PocoConfigurationNamespace = ""; // "YourProject.Data.Configuration";


        // Schema *****************************************************************************************************************************
        // If there are multiple schemas, then the table name is prefixed with the schema, except for dbo.
        // Ie. dbo.hello will be Hello.
        //     abc.hello will be Abc_Hello.
        public static bool   PrependSchemaName = true; // Control if the schema name is prepended to the table name
        public static string DefaultSchema     = null; // Set via DatabaseReader.DefaultSchema()

        // Table Suffix ***********************************************************************************************************************
        // Appends the suffix to the generated classes names
        // Ie. If TableSuffix is "Dto" then Order will be OrderDto
        //     If TableSuffix is "Entity" then Order will be OrderEntity
        public static string TableSuffix = null;

        // AddRelationship is a helper function that creates ForeignKey objects and adds them to the foreignKeys list
        public static Action<IDbContextFilter, Generator, List<ForeignKey>, Tables> AddExtraForeignKeys = delegate (IDbContextFilter filter, Generator gen, List<ForeignKey> foreignKeys, Tables tablesAndViews)
        {
            // Northwind example:

            // [Orders] (Table) to [Invoices] (View) is one-to-many using Orders.OrderID = Invoices.OrderID
            // gen.AddRelationship(filter, foreignKeys, tablesAndViews, "orders_to_invoices", Settings.DefaultSchema, "Orders", "OrderID", "dbo", "Invoices", "OrderID", null, null, true);

            // [Orders] (Table) to [Orders Qry] (View) is one-to-zeroOrOne ( [Orders].OrderID = [Orders Qry].OrderID )
            // gen.AddRelationship(filter, foreignKeys, tablesAndViews, "orders_to_ordersQry", Settings.DefaultSchema, "Orders", "OrderID", "dbo", "Orders Qry", "OrderID", "ParentFkName", "ChildFkName", true);

            // [Order Details] (Table) to [Invoices] (View) is one-to-zeroOrOne - but uses a composite-key: ( [Order Details].OrderID,ProductID = [Invoices].OrderID,ProductID )
            // gen.AddRelationship(filter, foreignKeys, tablesAndViews, "orderDetails_to_invoices", Settings.DefaultSchema, "Order Details", new[] { "OrderID", "ProductID" }, "dbo", "Invoices", new[] { "OrderID", "ProductID" }, null, null, true);
        };

        // StoredProcedure return types *******************************************************************************************************
        // Override generation of return models for stored procedures that return entities.
        // If a stored procedure returns an entity, add it to the list below.
        // This will suppress the generation of the return model, and instead return the entity.
        // Example:                       Proc name      Return this entity type instead
        //StoredProcedureReturnTypes.Add("SalesByYear", "SummaryOfSalesByYear");
        public static Dictionary<string, string> StoredProcedureReturnTypes = new Dictionary<string, string>();


        // Renaming ***********************************************************************************************************************
        // Table renaming (single context generation only) ************************************************************************************
        // Use the following function to rename tables such as tblOrders to Orders, Shipments_AB to Shipments, etc.
        // Example:
        public static Func<string, string, bool, string> TableRename = delegate (string name, string schema, bool isView)
        {
            // Example
            //if (name.StartsWith("tbl"))
            //    name = name.Remove(0, 3);
            //name = name.Replace("_AB", "");

            //if(isView)
            //    name = name + "View";

            // If you turn pascal casing off (UsePascalCase = false), and use the pluralisation service, and some of your
            // tables names are all UPPERCASE, some words ending in IES such as CATEGORIES get singularised as CATEGORy.
            // Therefore you can make them lowercase by using the following
            //return Inflector.MakeLowerIfAllCaps(name);

            // If you are using the pluralisation service and you want to rename a table, make sure you rename the table to the plural form.
            // For example, if the table is called Treez (with a z), and your pluralisation entry is
            //     new CustomPluralizationEntry("Tree", "Trees")
            // Use this TableRename function to rename Treez to the plural (not singular) form, Trees:
            //if (name == "Treez") return "Trees";

            return name;
        };

        // Use the following function if you need to apply additional modifications to a table
        // Called just before UpdateColumn
        public static Action<Table> UpdateTable = delegate (Table table)
        {
            // Add an extra comment
            //if (table.NameHumanCase.Equals("SomeTable", StringComparison.InvariantCultureIgnoreCase))
            //    table.AdditionalComment = "Example comment";

            // To add a base class to a table
            //if (table.NameHumanCase == "User")
            //    table.BaseClasses = " : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>";
            //if (table.NameHumanCase == "LogData" || table.NameHumanCase == "ReturnData" || table.NameHumanCase == "DataBlob")            
            //    table.BaseClasses = " : ReportData";

            // To add attributes
            //table.Attributes.Add("[Serializable]");
        };

        // Use the following function if you need to apply additional modifications to a column
        // eg. normalise names etc.
        public static Action<Column, Table, List<EnumDefinition>> UpdateColumn = delegate (Column column, Table table, List<EnumDefinition> enumDefinitions)
        {
            // Rename column
            //if (column.IsPrimaryKey && column.NameHumanCase == "PkId")
            //    column.NameHumanCase = "Id";

            // .IsConcurrencyToken() must be manually configured. However .IsRowVersion() can be automatically detected.
            //if (table.NameHumanCase.Equals("SomeTable", StringComparison.InvariantCultureIgnoreCase) && column.NameHumanCase.Equals("SomeColumn", StringComparison.InvariantCultureIgnoreCase))
            //    column.IsConcurrencyToken = true;

            // Remove table name from primary key
            //if (column.IsPrimaryKey && column.NameHumanCase.Equals(table.NameHumanCase + "Id", StringComparison.InvariantCultureIgnoreCase))
            //    column.NameHumanCase = "Id";

            // Remove column from poco class as it will be inherited from a base class
            //if (column.IsPrimaryKey &&
            //    (
            //        table.NameHumanCase.Equals("LogData", StringComparison.InvariantCultureIgnoreCase) ||
            //        table.NameHumanCase.Equals("ReturnData", StringComparison.InvariantCultureIgnoreCase) ||
            //        table.NameHumanCase.Equals("DataBlob", StringComparison.InvariantCultureIgnoreCase)
            //    ))
            //    column.ExistsInBaseClass = true; // If true, does not generate the property for this column as it will exist in a base class

            // Use the extended properties to perform tasks to column
            //if (column.ExtendedProperty == "HIDE")
            //    column.Hidden = true; // Hidden means the generator does not generate any code for this column at all.

            // Apply the "override" access modifier to a specific column.
            //if (column.NameHumanCase == "id")
            //    column.OverrideModifier = true;
            // This will create: public override long id { get; set; }

            // Perform Enum property type replacement
            var enumDefinition = enumDefinitions?.FirstOrDefault(e =>
                (e.Schema.Equals(table.Schema.DbName, StringComparison.InvariantCultureIgnoreCase)) &&
                (e.Table == "*" || e.Table.Equals(table.DbName, StringComparison.InvariantCultureIgnoreCase) || e.Table.Equals(table.NameHumanCase, StringComparison.InvariantCultureIgnoreCase)) &&
                (e.Column.Equals(column.DbName, StringComparison.InvariantCultureIgnoreCase) || e.Column.Equals(column.NameHumanCase, StringComparison.InvariantCultureIgnoreCase)));

            if (enumDefinition != null)
            {
                column.PropertyType = enumDefinition.EnumType;
                if (!string.IsNullOrEmpty(column.Default))
                    column.Default = "(" + enumDefinition.EnumType + ") " + column.Default;
            }
        };

        // Configures the key property to either use IDENTITY or HILO database feature to generate values for new entities.
        public static Func<Column, string> ColumnIdentity = delegate (Column c)
        {
            if(!IsEfCore3Plus())
                return ".UseSqlServerIdentityColumn()";

            // At this point we are using EFCore 3, 5+ which supports HiLo sequences.
            // Example of using a HiLo sequence using this function.
            /*if (c.ParentTable.NameHumanCase.Equals("SomeTable",  StringComparison.InvariantCultureIgnoreCase) &&
                c.NameHumanCase            .Equals("SomeColumn", StringComparison.InvariantCultureIgnoreCase))
            {
                return ".UseHiLo(\"your_sequence_name\",\"your_sequence_schema\")";
            }*/

            var hiLoSequence = HiLoSequences?.FirstOrDefault(x =>
                x.Schema.Equals(c.ParentTable.Schema.DbName, StringComparison.InvariantCultureIgnoreCase) &&
                (x.Table == "*" || x.Table.Equals(c.ParentTable.DbName, StringComparison.InvariantCultureIgnoreCase)));

            if (hiLoSequence != null)
                return string.Format(".UseHiLo(\"{0}\", \"{1}\")", hiLoSequence.SequenceName, hiLoSequence.SequenceSchema);

            return ".UseIdentityColumn()";
        };

        // In order to use this function, Settings.ElementsToGenerate must contain both Elements.Poco and Elements.Enum;
        public static Action<Table> AddEnum = delegate (Table table)
        {
            /*if (table.HasPrimaryKey && table.PrimaryKeys.Count() == 1 && table.Columns.Any(x => x.PropertyType == "string"))
            {
                // Example: choosing tables with certain naming conventions for enums. Please use your own conventions.
                if (table.NameHumanCase.StartsWith("Enum", StringComparison.InvariantCultureIgnoreCase) ||
                    table.NameHumanCase.EndsWith  ("Enum", StringComparison.InvariantCultureIgnoreCase))
                {
                    try
                    {
                        Enumerations.Add(new EnumerationSettings
                        {
                            Name       = table.NameHumanCase.Replace("Enum","").Replace("Enum","") + "Enum",
                            Table      = table.Schema.DbName + "." + table.DbName,
                            NameField  = table.Columns.First(x => x.PropertyType == "string").DbName, // Or specify your own
                            ValueField = table.PrimaryKeys.Single().DbName // Or specify your own
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
        public static Action<Enumeration> UpdateEnum = delegate (Enumeration enumeration)
        {
            //enumeration.EnumAttributes.Add("[DataContract]");
        };

        // Use the following function if you need to apply additional modifications to a enum member
        public static Action<EnumerationMember> UpdateEnumMember = delegate (EnumerationMember enumerationMember)
        {
            //enumerationMember.Attributes.Add("[EnumMember]");
            //enumerationMember.Attributes.Add("[SomeAttribute(\"" + enumerationMember.AllValues["SomeName"] + " \")]");
        };

        // Writes any boilerplate stuff inside the POCO class body
        public static Func<Table, string> WriteInsideClassBody = delegate (Table t)
        {
            // Example:
            //return "    // " + t.NameHumanCase + Environment.NewLine;

            // Do nothing by default
            return string.Empty;
        };

        // Using Views *****************************************************************************************************************
        // SQL Server does not support the declaration of primary-keys in VIEWs. Entity Framework's EDMX designer (and this generator)
        // assume that all non-null columns in a VIEW are primary-key columns, this will be incorrect for most non-trivial applications.
        // This callback will be invoked for each VIEW found in the database. Use it to declare which columns participate in that VIEW's
        // primary-key by setting 'IsPrimaryKey = true'.
        // If no columns are marked with 'IsPrimaryKey = true' then this T4 template defaults to marking all non-NULL columns as primary key columns.
        // To set-up Foreign-Key relationships between VIEWs and Tables (or even other VIEWs) use the 'AddForeignKeys' callback below.
        public static Action<Table> ViewProcessing = delegate (Table view)
        {
            // Below is example code for the Northwind database that configures the 'VIEW [Orders Qry]' and 'VIEW [Invoices]'
            /*switch (view.DbName)
            {
                case "Orders Qry":
                    // VIEW [Orders Qry] uniquely identifies rows with the 'OrderID' column:
                    view.Columns.Single(col => col.DbName == "OrderID").IsPrimaryKey = true;
                    break;

                case "Invoices":
                    // VIEW [Invoices] has a composite primary key (OrderID+ProductID), so both columns must be marked as a Primary Key:
                    foreach (var col in view.Columns.Where(c => c.DbName == "OrderID" || c.DbName == "ProductID"))
                        col.IsPrimaryKey = true;
                    break;
            }*/
        };

        // StoredProcedure renaming ************************************************************************************************************
        // Use the following function to rename stored procs such as sp_CreateOrderHistory to CreateOrderHistory, my_sp_shipments to Shipments, etc.
        public static Func<StoredProcedure, string> StoredProcedureRename = delegate (StoredProcedure sp)
        {
            // Example:
            //if (sp.NameHumanCase.StartsWith("sp_"))
            //    return sp.NameHumanCase.Remove(0, 3);
            //return sp.NameHumanCase.Replace("my_sp_", "");

            return sp.NameHumanCase; // Do nothing by default
        };

        // Use the following function to rename the return model automatically generated for stored procedure.
        // By default it's <proc_name>ReturnModel.
        public static Func<string, StoredProcedure, string> StoredProcedureReturnModelRename = delegate (string name, StoredProcedure sp)
        {
            // Example:
            //if (sp.NameHumanCase.Equals("ComputeValuesForDate", StringComparison.InvariantCultureIgnoreCase))
            //    return "ValueSet";
            //if (sp.NameHumanCase.Equals("SalesByYear", StringComparison.InvariantCultureIgnoreCase))
            //    return "SalesSet";

            return name; // Do nothing by default
        };

        // Mapping Table renaming *********************************************************************************************************************
        // By default, name of the properties created relate to the table the foreign key points to and not the mapping table.
        // Use the following function to rename the properties created by ManyToMany relationship tables especially if you have 2 relationships between the same tables.
        // Example:
        public static Func<string, string, string, string> MappingTableRename = delegate (string mappingTable, string tableName, string entityName)
        {
            // Example: If you have two mapping tables such as one being UserRequiredSkills snd one being UserOptionalSkills, this would change the name of one property
            //if (mappingTable == "UserRequiredSkills" && tableName == "User")
            //    return "RequiredSkills";

            // or if you want to give the same property name on both classes
            //if (mappingTable == "UserRequiredSkills")
            //    return "UserRequiredSkills";

            return entityName;
        };

        public static Func<string, ForeignKey, string, Relationship, short, string> ForeignKeyName = delegate (string tableName, ForeignKey foreignKey, string foreignKeyName, Relationship relationship, short attempt)
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
                    // Only called if foreign key name ends with "id"
                    // Use foreign key name without "id" at end of string
                    fkName = foreignKeyName.Remove(foreignKeyName.Length - 2, 2);
                    break;

                case 3:
                    // Use foreign key name only
                    fkName = foreignKeyName;
                    break;

                case 4:
                    // Use table name and foreign key name
                    fkName = tableName + "_" + foreignKeyName;
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

            // Apply custom foreign key renaming rules. Can be useful in applying pluralization.
            // For example:
            /*if (tableName == "Employee" && foreignKey.FkColumn == "ReportsTo")
                return "Manager";

            if (tableName == "Territories" && foreignKey.FkTableName == "EmployeeTerritories")
                return "Locations";

            if (tableName == "Employee" && foreignKey.FkTableName == "Orders" && foreignKey.FkColumn == "EmployeeID")
                return "ContactPerson";
            */

            // FK_TableName_FromThisToParentRelationshipName_FromParentToThisChildsRelationshipName
            // (e.g. FK_CustomerAddress_Customer_Addresses will extract navigation properties "address.Customer" and "customer.Addresses")
            // Feel free to use and change the following
            /*if (foreignKey.ConstraintName.StartsWith("FK_") && foreignKey.ConstraintName.Count(x => x == '_') == 3)
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
        public static Func<ForeignKey, ForeignKey> ForeignKeyFilterFunc = delegate (ForeignKey fk)
        {
            // Return null to exclude this foreign key, or set IncludeReverseNavigation = false
            // to include the foreign key but not generate reverse navigation properties.
            // Example, to exclude all foreign keys for the Categories table, use:
            //if (fk.PkTableName == "Categories")
            //    return null;

            // Example, to exclude reverse navigation properties for tables ending with Type, use:
            //if (fk.PkTableName.EndsWith("Type"))
            //    fk.IncludeReverseNavigation = false;

            // You can also change the access modifier of the foreign-key's navigation property:
            //if(fk.PkTableName == "Categories")
            //     fk.AccessModifier = "internal";

            return fk;
        };

        public static Func<Table, Table, string, string, string[]> ForeignKeyAnnotationsProcessing = delegate (Table fkTable, Table pkTable, string propName, string fkPropName)
        {
            // Example:
            // Each navigation property that is a reference to User are left intact
            //if(pkTable.NameHumanCase.Equals("User") && propName.Equals("User"))
            //    return null;

            // all the others are marked with this attribute
            //return new[] { "System.Runtime.Serialization.IgnoreDataMember" };

            return null;
        };

        // Column modification ****************************************************************************************************************
        // Use the following list to replace column byte types with Enums.
        // As long as the type can be mapped to your new type, all is well.
        public static Action<List<EnumDefinition>> AddEnumDefinitions = delegate (List<EnumDefinition> enumDefinitions)
        {
            // Examples:
            //enumDefinitions.Add(new EnumDefinition { Schema = Settings.DefaultSchema, Table = "match_table_name", Column = "match_column_name", EnumType = "name_of_enum" });

            // This will replace OrderHeader.OrderStatus type to be an OrderStatusType enum
            //enumDefinitions.Add(new EnumDefinition { Schema = Settings.DefaultSchema, Table = "OrderHeader", Column = "OrderStatus", EnumType = "OrderStatusType" }); 

            // This will replace any table *.OrderStatus type to be an OrderStatusType enum
            //enumDefinitions.Add(new EnumDefinition { Schema = Settings.DefaultSchema, Table = "*", Column = "OrderStatus", EnumType = "OrderStatusType" });
        };

        // Generate multiple db contexts in a single go ***************************************************************************************
        // Generating multiple contexts at a time requires you specifying which tables, and columns to generate for each context.
        // As this generator can now generate multiple db contexts in a single go, filtering is done a per db context, and no longer global.
        // If GenerateSingleDbContext = true (default), please modify SingleContextFilter, this is where your previous global settings should go.
        // If GenerateSingleDbContext = false, this will generate multiple db contexts. Please read https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/wiki/Generating-multiple-database-contexts-in-a-single-go
        public static bool GenerateSingleDbContext                = true;
        public static string MultiContextSettingsConnectionString = ""; // Leave empty to read data from same database in ConnectionString above. If settings are in another database, specify the connection string here.
        public static string MultiContextSettingsPlugin           = ""; // Only used for unit testing Generator project as you can't (yet) inherit from IMultiContextSettingsPlugin. "c:\\Path\\YourMultiDbSettingsReader.dll,Full.Name.Of.Class.Including.Namespace". This will allow you to specify a pluggable provider for reading your MultiContext settings.
        public static char MultiContextAttributeDelimiter         = '~'; // The delimiter used for splitting MultiContext attributes

        public static Action<Column, Table, Dictionary<string, object>> MultiContextAllFieldsColumnProcessing = delegate (Column column, Table table, Dictionary<string, object> allFields)
        {
            // Examples of how to use additional custom fields from the MultiContext.[Column] table
            // INT example
            /*if (allFields.ContainsKey("DummyInt"))
            {
                var o = allFields["DummyInt"];
                column.ExtendedProperty += string.Format(" DummyInt = {0}", (int) o);
            }*/

            // VARCHAR example
            /*if (allFields.ContainsKey("Test"))
            {
                var o = allFields["Test"];
                column.ExtendedProperty += string.Format(" Test = {0}", o.ToString());
            }*/

            // DATETIME example
            /*if (allFields.ContainsKey("date_of_birth"))
            {
                var o = allFields["date_of_birth"];
                var date = Convert.ToDateTime(o);
                column.ExtendedProperty += string.Format(" date_of_birth = {0}", date.ToLongDateString());
            }*/
        };

        public static Action<Table, Dictionary<string, object>> MultiContextAllFieldsTableProcessing = delegate (Table table, Dictionary<string, object> allFields)
        {
            // Examples of how to use additional custom fields from the MultiContext.[Table] table
            // VARCHAR example
            /*if (allFields.ContainsKey("Notes"))
            {
                var o = allFields["Notes"];
                if (string.IsNullOrEmpty(table.AdditionalComment))
                    table.AdditionalComment = string.Empty;

                table.AdditionalComment += string.Format(" Test = {0}", o.ToString());
            }*/
        };

        public static Action<StoredProcedure, Dictionary<string, object>> MultiContextAllFieldsStoredProcedureProcessing = delegate (StoredProcedure sp, Dictionary<string, object> allFields)
        {
            // Examples of how to use additional custom fields from the MultiContext.[Table] table
            // VARCHAR example
            /*if (allFields.ContainsKey("CustomRename"))
            {
                var o = allFields["CustomRename"];
                sp.NameHumanCase = o.ToString();
            }*/
        };

        public static Action<StoredProcedure, Dictionary<string, object>> MultiContextAllFieldsFunctionProcessing = delegate (StoredProcedure sp, Dictionary<string, object> allFields)
        {
            // Examples of how to use additional custom fields from the MultiContext.[Table] table
            // VARCHAR example
            /*if (allFields.ContainsKey("CustomRename"))
            {
                var o = allFields["CustomRename"];
                sp.NameHumanCase = o.ToString();
            }*/
        };

        // Helper functions ***************************************************************************************************************
        public static bool DbContextClassIsPartial()
        {
            return DbContextClassModifiers != null && DbContextClassModifiers.Contains("partial");
        }

        public static bool EntityClassesArePartial()
        {
            return EntityClassesModifiers != null && EntityClassesModifiers.Contains("partial");
        }

        public static bool ConfigurationClassesArePartial()
        {
            return ConfigurationClassesModifiers != null && ConfigurationClassesModifiers.Contains("partial");
        }

        private static string _dbContextInterfaceName;
        public static string DbContextInterfaceName
        {
            get { return _dbContextInterfaceName ?? ("I" + DbContextName); }
            set { _dbContextInterfaceName = value; }
        }

        private static bool _explicitDefaultConstructorArgument;
        private static string _defaultConstructorArgument;
        public static string DefaultConstructorArgument
        {
            // = null; // Defaults to "Name=" + ConnectionStringName, use null in order not to call the base constructor
            get { return _explicitDefaultConstructorArgument ? _defaultConstructorArgument : string.Format('"' + "Name={0}" + '"', ConnectionStringName); }
            set { _explicitDefaultConstructorArgument = true; _defaultConstructorArgument = value; }
        }

        // Don't forget to take a look at SingleContextFilter and FilterSettings classes!
        // That's it, nothing else to configure ***********************************************************************************************

        public static bool IsEf6()     => TemplateType == TemplateType.Ef6;
        public static bool IsEfCore3Plus() => EfCoreVersion() >= 3;
        public static bool IsEfCore5Plus() => EfCoreVersion() >= 5;
        public static bool IsEfCore6Plus() => EfCoreVersion() >= 6;
        public static bool IsEfCore7Plus() => EfCoreVersion() >= 7;
        public static int EfCoreVersion()
        {
            switch (TemplateType)
            {
                case TemplateType.EfCore2:
                case TemplateType.FileBasedCore2:
                    return 2;

                case TemplateType.EfCore3:
                case TemplateType.FileBasedCore3:
                    return 3;
                
                case TemplateType.EfCore5:
                case TemplateType.FileBasedCore5:
                    return 5;
                
                case TemplateType.EfCore6:
                case TemplateType.FileBasedCore6:
                    return 6;
                
                case TemplateType.EfCore7:
                case TemplateType.FileBasedCore7:
                    return 7;
                
                case TemplateType.Ef6:
                case TemplateType.FileBasedEf6:
                default:
                    return 0;
            }
        }

        public static string DatabaseProvider()
        {
            switch (DatabaseType)
            {
                case DatabaseType.PostgreSQL:
                    return "UseNpgsql";

                case DatabaseType.MySql:
                    return "UseMySql";

                case DatabaseType.Oracle:
                    return "UseOracle";

                default:
                    return "UseSqlServer";
            }
        }
        
        public static string SqlParameter()
        {
            switch (DatabaseType)
            {
                case DatabaseType.PostgreSQL:
                    return "NpgsqlParameter";

                case DatabaseType.MySql:
                    return "MySqlParameter";

                case DatabaseType.Oracle:
                    return "OracleParameter";

                default:
                    return "SqlParameter";
            }
        }

        public static string Root;
        public static string TemplateFile;
        public static int FilterCount;
    }
}