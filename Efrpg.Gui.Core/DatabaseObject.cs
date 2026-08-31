using System;

namespace Efrpg.Gui
{
    /// <summary>
    ///     One table, view, stored procedure or function, named the way the user will recognise it.
    /// </summary>
    public sealed class DatabaseObject : IComparable<DatabaseObject>
    {
        public DatabaseObject(string schema, string name, DatabaseObjectKind kind)
        {
            Schema = schema ?? string.Empty;
            Name   = name ?? string.Empty;
            Kind   = kind;
        }

        public string Schema { get; }

        public string Name { get; }

        public DatabaseObjectKind Kind { get; }

        /// <summary>
        ///     Schema-qualified, except where there is no schema - SQLite has none, and qualifying with an empty
        ///     string would show the user a leading dot.
        /// </summary>
        public string FullName => Schema.Length == 0 ? Name : Schema + "." + Name;

        public int CompareTo(DatabaseObject other)
        {
            if (other == null)
                return 1;

            var bySchema = string.Compare(Schema, other.Schema, StringComparison.OrdinalIgnoreCase);

            return bySchema != 0 ? bySchema : string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
