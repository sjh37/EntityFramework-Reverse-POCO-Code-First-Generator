using System;

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
            string dbContextName)
        {
            Database         = database ?? throw new ArgumentNullException(nameof(database));
            Template         = template ?? throw new ArgumentNullException(nameof(template));
            ConnectionString = connectionString ?? string.Empty;
            DbContextName    = dbContextName ?? string.Empty;
        }

        public DatabaseTarget Database { get; }

        public TemplateTarget Template { get; }

        public string ConnectionString { get; }

        public string DbContextName { get; }

        /// <summary>What a brand-new template should open on: the shipped defaults, with a name derived from the file.</summary>
        public static TemplateConfiguration ForNewTemplate(string dbContextName)
        {
            return new TemplateConfiguration(DatabaseTarget.Default, TemplateTarget.Default,
                DatabaseTarget.Default.ConnectionString, dbContextName);
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
                settings.GetString("DbContextName") ?? fallbackDbContextName);
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

            return settings.Text;
        }
    }
}
