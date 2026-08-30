using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Efrpg;
using Efrpg.FileManagement;
using Efrpg.Filtering;
using Efrpg.Generators;
using Efrpg.LanguageMapping;
using Efrpg.Pluralization;
using Efrpg.Readers;
using Efrpg.Templates;

namespace Generator.Tests.Unit.DocSamples
{
    /// <summary>
    ///     Runs the generator over <c>DocSampleSchema.xml</c> so the wiki can show real generated code instead of
    ///     code somebody typed from memory.
    /// </summary>
    /// <remarks>
    ///     Every example on every Settings.* wiki page comes through here. The point is that a snippet on the wiki
    ///     is output the generator actually produced, and that <see cref="WikiSnippetDriftTests"/> can re-run it
    ///     later and notice when it stops being true. Prose written by reading the templates drifts from the
    ///     templates; this cannot.
    ///     Single-file mode only: with GenerateSingleDbContext and no separate files, everything lands in
    ///     GeneratedTextTransformation.FileData and nothing touches the disk. Settings.Root is still pointed at a
    ///     temporary folder because a few code paths read it.
    /// </remarks>
    public static class DocSampleRunner
    {
        /// <summary>
        ///     Generates once with <paramref name="configure"/> applied on top of the shipped Database.tt defaults.
        /// </summary>
        /// <param name="configure">
        ///     Sets the one setting the sample is about. Everything else is left at its shipped default so the
        ///     difference between two runs is attributable to that setting alone.
        /// </param>
        public static string Generate(Action configure)
        {
            ApplyDatabaseTtDefaults();
            configure?.Invoke();

            var outer = new GeneratedTextTransformation();
            var fileManagement = new FileManagementService(outer);
            var generator = GeneratorFactory.Create(LoadSchema(), fileManagement, null);

            if (generator == null || !generator.InitialisationOk)
                throw new InvalidOperationException("The generator would not initialise for a doc sample.");

            generator.ReadDatabase();
            generator.GenerateCode();

            // Deliberately no fileManagement.Process(true): that writes an audit file and the generated .cs to
            // Settings.Root. In single-file mode the whole output is already in FileData.
            return Normalise(outer.FileData.ToString());
        }

        /// <summary>
        ///     Generates twice and returns both outputs, so a caller can show a before and an after.
        /// </summary>
        public static DocSamplePair GeneratePair(Action before, Action after)
        {
            return new DocSamplePair(Generate(before), Generate(after));
        }

        private static EfrpgResult LoadSchema()
        {
            return EfrpgResultXmlReader.Read(File.ReadAllText(SchemaPath()));
        }

        public static string SchemaPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DocSamples", "DocSampleSchema.xml");
        }

        /// <summary>
        ///     Resets every setting a sample might touch to the value the shipped Database.tt assigns.
        /// </summary>
        /// <remarks>
        ///     Settings is static and shared across the whole test run, so anything not reset here leaks from one
        ///     sample into the next and the snippets become order-dependent. The values here are the .tt values,
        ///     not the Settings.cs field initialisers - those differ for UseLazyLoading, DisableGeographyTypes,
        ///     UseResharper and the folder settings, and the .tt is what a user actually gets.
        /// </remarks>
        private static void ApplyDatabaseTtDefaults()
        {
            Settings.DatabaseType                 = DatabaseType.SqlServer;
            Settings.TemplateType                 = TemplateType.EfCore10;
            Settings.GeneratorType                = GeneratorType.EfCore;
            Settings.ConnectionString             = "Data Source=(local);Initial Catalog=DocSamples;Integrated Security=True";
            Settings.ConnectionStringActions      = "";
            Settings.ConnectionStringName         = "MyDbContext";
            Settings.DbContextName                = "MyDbContext";
            Settings.DbContextInterfaceName       = null;
            Settings.GenerateSeparateFiles        = false;
            Settings.Namespace                    = "MyApp.Data";
            Settings.UseFileScopedNamespaces      = false;
            Settings.AddUnitTestingDbContext      = false; // Off: the fake context doubles the length of every snippet
            Settings.FakeDbContextInDebugOnlyMode = false;

            Settings.ElementsToGenerate = Elements.Poco | Elements.Context | Elements.Interface | Elements.PocoConfiguration | Elements.Enum;

            Settings.ContextFolder             = "";
            Settings.InterfaceFolder           = "";
            Settings.PocoFolder                = "";
            Settings.PocoConfigurationFolder   = "";
            Settings.OwnedEntityFolder         = "";
            Settings.UseFolderNameInNamespace  = false;

            Settings.CommandTimeout                         = 600;
            Settings.DbContextInterfaceBaseClasses          = "IDisposable";
            Settings.DbContextBaseClass                     = "DbContext";
            Settings.OnConfiguration                        = OnConfiguration.ConnectionString;
            Settings.AddParameterlessConstructorToDbContext = true;
            Settings.ConfigurationClassName                 = "Configuration";
            Settings.UseMappingTables                       = false;

            Settings.EntityClassesModifiers        = "public";
            Settings.ConfigurationClassesModifiers = "public";
            Settings.DbContextClassModifiers       = "public";
            Settings.DbContextInterfaceModifiers   = "public";
            Settings.ResultClassModifiers          = "public";

            Settings.UsePascalCase                          = true;
            Settings.UsePascalCaseForEnumMembers            = true;
            Settings.UseDataAnnotations                     = false;
            Settings.UsePropertyInitialisers                = false;
            Settings.UseLazyLoading                         = false;
            Settings.UseInheritedBaseInterfaceFunctions     = false;
            Settings.IncludeComments                        = CommentsStyle.AtEndOfField;
            Settings.IncludeExtendedPropertyComments        = CommentsStyle.InSummaryBlock;
            Settings.DisableGeographyTypes                  = true;
            Settings.CollectionInterfaceType                = "ICollection";
            Settings.CollectionType                         = "List";
            Settings.NullableShortHand                      = true;
            Settings.AddIDbContextFactory                   = true;
            Settings.IncludeQueryTraceOn9481Flag            = false;
            Settings.UsePrivateSetterForComputedColumns     = true;
            Settings.IncludeGeneratorVersionInCode          = false;
            Settings.TrimCharFields                         = false;
            Settings.IncludeFieldNameConstants              = false;
            Settings.UsePropertiesForStoredProcResultSets   = false;
            Settings.MergeMultipleStoredProcModelsIfAllSame = true;
            Settings.AdditionalNamespaces                   = new List<string>();
            Settings.AdditionalContextInterfaceItems        = new List<string>();
            Settings.AdditionalFileHeaderText               = new List<string>();
            Settings.AdditionalFileFooterText               = new List<string>();
            Settings.OrderProperties                        = OrderProperties.Ordinal;

            Settings.GenerationLanguage = GenerationLanguage.CSharp;
            Settings.FileExtension      = ".cs";

            Settings.UseRegions                          = true;
            Settings.UseNamespace                        = true;
            Settings.UsePragma                           = false;
            Settings.AllowNullStrings                    = false;
            Settings.NullableReverseNavigationProperties = false;
            Settings.UseResharper                        = false; // The .tt ships true; the banner is pure noise in a snippet
            Settings.ShowLicenseInfo                     = false;
            Settings.IncludeConnectionSettingComments    = false;
            Settings.IncludeCodeGeneratedAttribute       = false;
            Settings.IncludeColumnsWithDefaults          = true;
            Settings.GenerateHasDefaultValueSql          = false;

            // Null, not empty: a non-empty list makes the generator re-invoke the efrpg tool to read enum rows,
            // which needs a database.
            Settings.Enumerations = null;
            Settings.HiLoSequences = new List<HiLoSequence>();

            Settings.AdditionalReverseNavigationsDataAnnotations = new string[0];
            Settings.AdditionalForeignKeysDataAnnotations        = new string[0];

            Settings.ContextNamespace           = "";
            Settings.InterfaceNamespace         = "";
            Settings.PocoNamespace              = "";
            Settings.PocoConfigurationNamespace = "";

            Settings.PrependSchemaName = true;
            Settings.TableSuffix       = null;

            Settings.GenerateSingleDbContext            = true;
            Settings.MultiContextSettingsConnectionString = "";
            Settings.MultiContextSettingsPlugin           = null;
            Settings.MultiContextAttributeDelimiter       = '~';

            Settings.StoredProcedureReturnTypes = new Dictionary<string, string>();

            ResetCallbacksToShippedBehaviour();

            Settings.Root = TempRoot();

            Inflector.IgnoreWordsThatEndWith = new List<string> { "Status", "To", "Data" };
            Inflector.PluralisationService   = new EnglishPluralizationService();

            FilterSettings.Reset();
            FilterSettings.AddDefaults();
            FilterSettings.CheckSettings();
        }

        /// <summary>
        ///     Restores the delegate settings to what the shipped Database.tt does, which for most of them is
        ///     "nothing at all".
        /// </summary>
        private static void ResetCallbacksToShippedBehaviour()
        {
            Settings.TableRename          = (name, schema, isView) => name;
            Settings.UpdateTable          = table => { };
            Settings.UpdateColumn         = (column, table, enumDefinitions, jsonColumnMappings) =>
            {
                Settings.ApplyJsonPropertyNameAttribute(column);
                Settings.ApplyJsonColumnMappings(column, table, jsonColumnMappings);
                Settings.ApplyDataAnnotations(column);
                Settings.ApplyEnumTypeReplacement(column, table, enumDefinitions);
            };
            Settings.AddEnum              = table => { };
            Settings.UpdateEnum           = enumeration => { };
            Settings.UpdateEnumMember     = member => { };
            Settings.WriteInsideClassBody = table => string.Empty;
            Settings.ViewProcessing       = view => { };

            Settings.AddEnumDefinitions      = definitions => { };
            Settings.AddJsonColumnMappings   = mappings => { };
            Settings.AddOwnedEntityMappings  = mappings => { };
            Settings.AddExtraForeignKeys     = (filter, gen, foreignKeys, tables) => { };

            Settings.StoredProcedureRename            = sp => sp.NameHumanCase;
            Settings.StoredProcedureReturnModelRename = (name, sp) => name;
            Settings.MappingTableRename               = (mappingTable, tableName, entityName) => entityName;
            Settings.ForeignKeyFilterFunc             = fk => fk;
            Settings.ForeignKeyAnnotationsProcessing  = (fkTable, pkTable, propName, fkPropName) => null;

            Settings.PrependSchemaNameForTable           = table => true;
            Settings.PrependSchemaNameForStoredProcedure = sp => true;

            Settings.ReadStoredProcReturnObjectException = (ex, sp) => { sp.Error = ex.Message; };
            Settings.ReadStoredProcReturnObjectCompleted = sp => { };
        }

        private static string TempRoot()
        {
            var path = Path.Combine(Path.GetTempPath(), "efrpg-doc-samples");
            Directory.CreateDirectory(path);
            return path;
        }

        /// <summary>
        ///     Removes the parts of the output that are not the generator's answer to the question the sample asks.
        /// </summary>
        /// <remarks>
        ///     The trial-licence banner is the important one. Whether it appears depends on whether the machine
        ///     running the tests has a ReversePOCO.txt, which would otherwise make every snippet differ between
        ///     the author's machine and CI.
        /// </remarks>
        private static string Normalise(string generated)
        {
            var lines = generated
                .Replace("\r\n", "\n")
                .Split('\n')
                .Where(line => !IsLicenceNoise(line))
                .ToList();

            while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[0]))
                lines.RemoveAt(0);
            while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[lines.Count - 1]))
                lines.RemoveAt(lines.Count - 1);

            return string.Join("\n", lines);
        }

        private static bool IsLicenceNoise(string line)
        {
            var t = line.Trim();
            return t.StartsWith("// ****")
                || t.StartsWith("// This is not a commercial licence")
                || t.StartsWith("// Licence file")
                || t.StartsWith("// Your licence file")
                || t.StartsWith("// Please obtain your licence")
                || t.StartsWith("// Defaulting to Trial version");
        }
    }

    /// <summary>
    ///     A before and an after, ready to be diffed or written out.
    /// </summary>
    public class DocSamplePair
    {
        public string Before { get; private set; }
        public string After { get; private set; }

        public DocSamplePair(string before, string after)
        {
            Before = before;
            After = after;
        }
    }
}
