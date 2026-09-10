namespace Efrpg.Gui
{
    /// <summary>One column of a table or view as the efrpg tool describes it, enough to pick enum fields from.</summary>
    public sealed class DatabaseColumn
    {
        public DatabaseColumn(string name, string typeName, bool isPrimaryKey, int ordinal)
        {
            Name         = name ?? string.Empty;
            TypeName     = (typeName ?? string.Empty).ToLowerInvariant();
            IsPrimaryKey = isPrimaryKey;
            Ordinal      = ordinal;
        }

        public string Name { get; }

        /// <summary>The database type name, lower case: int, nvarchar, varchar2, text and so on.</summary>
        public string TypeName { get; }

        public bool IsPrimaryKey { get; }

        public int Ordinal { get; }

        /// <summary>True for the integer types an enum's value can be read from.</summary>
        public bool IsIntegral =>
            TypeName == "int" || TypeName == "integer" || TypeName == "smallint" || TypeName == "tinyint" ||
            TypeName == "bigint" || TypeName == "int2" || TypeName == "int4" || TypeName == "int8" ||
            TypeName == "number" || TypeName == "mediumint";

        /// <summary>True for the text types an enum's member name can be read from.</summary>
        public bool IsText =>
            TypeName.Contains("char") || TypeName == "text" || TypeName == "string" || TypeName == "clob";

        public override string ToString()
        {
            return Name;
        }
    }
}
