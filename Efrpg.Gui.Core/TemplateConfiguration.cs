using System;
using System.Text.RegularExpressions;

namespace Efrpg.Gui
{
    /// <summary>
    ///     The set of answers the connection dialog collects, and the rules for reading them out of a .tt and
    ///     writing them back into one.
    /// </summary>
    /// <remarks>
    ///     Kept here rather than in the dialog so it can be unit tested. The dialog lives in the VSIX, which needs a
    ///     running Visual Studio to exercise at all, and the rule that matters most - TemplateType and GeneratorType
    ///     being written as a pair - is exactly the kind of thing that must not be discovered by a user.
    /// </remarks>
    public sealed class TemplateConfiguration
    {
        public TemplateConfiguration(DatabaseTarget database, TemplateTarget template, string connectionString,
            string dbContextName, string namespaceName)
        {
            Database         = database ?? throw new ArgumentNullException(nameof(database));
            Template         = template ?? throw new ArgumentNullException(nameof(template));
            ConnectionString = connectionString ?? string.Empty;
            DbContextName    = dbContextName ?? string.Empty;
            Namespace        = (namespaceName ?? string.Empty).Trim();
        }

        public DatabaseTarget Database { get; }

        public TemplateTarget Template { get; }

        public string ConnectionString { get; }

        public string DbContextName { get; }

        /// <summary>
        ///     The namespace for the generated code, or empty to keep the template's <c>DefaultNamespace</c>, which
        ///     resolves at generation time to the namespace of the project the .tt sits in.
        /// </summary>
        public string Namespace { get; }

        /// <summary>What the shipped template holds in Settings.Namespace, and what empty means here.</summary>
        public const string DefaultNamespaceExpression = "DefaultNamespace";

        private static readonly Regex NamespacePattern =
            new Regex(@"^[A-Za-z_]\w*(\.[A-Za-z_]\w*)*$");

        /// <summary>
        ///     True when the namespace can be written into the .tt as a string literal without producing code that
        ///     does not compile. An empty namespace is valid and means "leave it as DefaultNamespace".
        /// </summary>
        public bool HasValidNamespace => Namespace.Length == 0 || NamespacePattern.IsMatch(Namespace);

        /// <summary>What a brand-new template should open on: the shipped defaults, with a name derived from the file.</summary>
        public static TemplateConfiguration ForNewTemplate(string dbContextName)
        {
            return new TemplateConfiguration(DatabaseTarget.Default, TemplateTarget.Default,
                DatabaseTarget.Default.ConnectionString, dbContextName, string.Empty);
        }

        /// <summary>
        ///     What an existing .tt already says.
        /// </summary>
        /// <remarks>
        ///     Anything unreadable falls back to the default rather than failing: a user who has replaced a setting
        ///     with an expression still deserves a working dialog for the other fields, and <see cref="ApplyTo"/>
        ///     will refuse to overwrite the expression anyway.
        /// </remarks>
        public static TemplateConfiguration ReadFrom(TemplateSettingsFile settings, string fallbackDbContextName)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            var database = DatabaseTarget.Find(settings.GetEnum("DatabaseType")) ?? DatabaseTarget.Default;
            var template = TemplateTarget.Find(settings.GetEnum("TemplateType")) ?? TemplateTarget.Default;

            return new TemplateConfiguration(
                database,
                template,
                settings.GetString("ConnectionString") ?? database.ConnectionString,
                settings.GetString("DbContextName") ?? fallbackDbContextName,
                ReadNamespace(settings));
        }

        /// <summary>
        ///     Settings.Namespace ships as the bare identifier <c>DefaultNamespace</c> and becomes a quoted string
        ///     once somebody overrides it, so both shapes have to be understood. Anything else - a concatenation, a
        ///     call - reads back as empty, which leaves <see cref="ApplyTo"/> declining to touch it.
        /// </summary>
        private static string ReadNamespace(TemplateSettingsFile settings)
        {
            var literal = settings.GetString("Namespace");

            return literal ?? string.Empty;
        }

        /// <summary>
        ///     Writes every answer into the settings file and returns the new text. Settings the file does not
        ///     express as a plain single-line assignment are left exactly as they are.
        /// </summary>
        public string ApplyTo(TemplateSettingsFile settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            settings.TrySetString("ConnectionString", ConnectionString);
            settings.TrySetEnum("DatabaseType", Database.Name);

            // GeneratorType is written alongside TemplateType, never on its own. The generator keeps the two
            // independent, so an Ef6 template left with the default EfCore generator produces code that does not
            // compile - and the user would meet that as a build error a long way from the dialog that caused it.
            settings.TrySetEnum("TemplateType", Template.Name);
            settings.TrySetEnum("GeneratorType", Template.GeneratorTypeName);

            if (DbContextName.Length > 0)
            {
                settings.TrySetString("DbContextName", DbContextName);
                settings.TrySetString("ConnectionStringName", DbContextName);
            }

            WriteNamespace(settings);

            return settings.Text;
        }

        /// <summary>
        ///     Writes Settings.Namespace, switching the right-hand side between a quoted string and the bare
        ///     <c>DefaultNamespace</c> identifier rather than only replacing a literal.
        /// </summary>
        /// <remarks>
        ///     An invalid namespace is left alone rather than written. What goes here becomes C# in the .tt, and a
        ///     user who has replaced the setting with an expression of their own gets to keep it.
        /// </remarks>
        private void WriteNamespace(TemplateSettingsFile settings)
        {
            if (!HasValidNamespace)
                return;

            settings.TrySetExpression("Namespace",
                Namespace.Length == 0 ? DefaultNamespaceExpression : "\"" + Namespace + "\"");
        }
    }
}
