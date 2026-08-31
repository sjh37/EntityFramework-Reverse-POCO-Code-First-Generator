using System;
using System.Collections.Generic;
using System.Linq;

namespace Efrpg.Gui
{
    /// <summary>
    ///     One entry in the wizard's database dropdown: the name written into the .tt, a label for the dropdown, and
    ///     the connection string to start the user off with.
    /// </summary>
    /// <remarks>
    ///     The connection strings are the reason this exists. Oracle, MySQL, PostgreSQL and SQL Server share no
    ///     keywords at all - Oracle wants <c>Data Source=host:port/service</c>, PostgreSQL wants
    ///     <c>Server=;Port=;Database=</c> - so a user who picks anything but SQL Server and is handed a SQL Server
    ///     connection string has to go and find the right shape somewhere else, which is exactly the problem this
    ///     wizard exists to remove. Choosing the database type first and being given a skeleton to fill in is the
    ///     whole point of the dropdown.
    ///
    ///     Identified by the enum member name rather than by <c>Efrpg.DatabaseType</c> itself, because that enum
    ///     lives in the net48 Generator project and this assembly is netstandard2.0. The name is what gets written
    ///     into the .tt in any case, so nothing is lost. <c>DatabaseTargetTests</c> checks the list against the enum
    ///     members recorded in settings-metadata.v4.json, so a database type added to the generator and not to this
    ///     list fails the build rather than quietly going missing from the dropdown.
    /// </remarks>
    public sealed class DatabaseTarget
    {
        private DatabaseTarget(string name, string displayName, string connectionString, string hint)
        {
            Name             = name;
            DisplayName      = displayName;
            ConnectionString = connectionString;
            Hint             = hint;
        }

        /// <summary>The DatabaseType enum member name, written into the .tt verbatim.</summary>
        public string Name { get; }

        /// <summary>What the dropdown shows. Spelled the way the vendor spells it, not the way the enum does.</summary>
        public string DisplayName { get; }

        /// <summary>
        ///     A connection string with <see cref="TemplateSettingWriter.Placeholder"/> wherever the user has to
        ///     supply something. Every placeholder must be replaced before the dialog will let them continue.
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>One line under the connection box saying what the placeholders stand for.</summary>
        public string Hint { get; }

        private const string Todo = TemplateSettingWriter.Placeholder;

        /// <summary>
        ///     In the order the dropdown shows them: SQL Server first because it is what most users of this
        ///     generator have, then the rest in the order the enum declares them.
        /// </summary>
        public static IReadOnlyList<DatabaseTarget> All { get; } = new[]
        {
            new DatabaseTarget("SqlServer", "SQL Server",
                "Data Source=(local);Initial Catalog=" + Todo + ";Integrated Security=True;MultipleActiveResultSets=True;Encrypt=false;TrustServerCertificate=true",
                "Replace " + Todo + @" with your database name. Use a server name, or .\INSTANCE, in place of (local) if the database is not on this machine."),

            new DatabaseTarget("SQLite", "SQLite",
                "Data Source=" + Todo + ".db",
                "Replace " + Todo + " with the path to your .db file, relative to the generated project or absolute."),

            new DatabaseTarget("PostgreSQL", "PostgreSQL",
                "Server=127.0.0.1;Port=5432;Database=" + Todo + ";User Id=postgres;Password=" + Todo + ";",
                "Replace both " + Todo + " markers: the database name, and the password for the user shown."),

            new DatabaseTarget("MySql", "MySQL / MariaDB",
                "Server=localhost;Port=3306;Database=" + Todo + ";User Id=root;Password=" + Todo + ";",
                "Replace both " + Todo + " markers: the database name, and the password for the user shown."),

            new DatabaseTarget("Oracle", "Oracle",
                "Data Source=localhost:1521/" + Todo + ";User Id=" + Todo + ";Password=" + Todo + ";",
                "Replace all three " + Todo + " markers: the service name, then the schema user and its password. The schema user is the one whose tables get generated.")
        };

        /// <summary>What the dialog opens on, and what the shipped Database.tt already says.</summary>
        public static DatabaseTarget Default => All[0];

        /// <summary>Returns the target with this enum member name, or null. Case sensitive, as the enum is.</summary>
        public static DatabaseTarget Find(string name)
        {
            return All.FirstOrDefault(t => string.Equals(t.Name, name, StringComparison.Ordinal));
        }

        /// <summary>
        ///     True when the text is still one of the untouched defaults, so replacing it loses nothing the user
        ///     typed. Switching database type swaps the connection string only while this holds.
        /// </summary>
        public static bool IsUntouchedDefault(string connectionString)
        {
            return All.Any(t => string.Equals(t.ConnectionString, connectionString, StringComparison.Ordinal));
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
