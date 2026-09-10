namespace Efrpg.Gui
{
    /// <summary>
    ///     What happened when the GUI asked the efrpg tool to read a database.
    /// </summary>
    /// <remarks>
    ///     A failure is a result, not an exception, because almost every failure here is the user's own connection
    ///     string being wrong - a typo in the server name, a database that does not exist, a password that has
    ///     expired. That is information to show them, and the message the database itself produced is nearly always
    ///     more useful than anything this code could say instead.
    /// </remarks>
    public sealed class SchemaReadResult
    {
        private SchemaReadResult(DatabaseSchema schema, string error)
        {
            Schema = schema;
            Error  = error;
        }

        /// <summary>The schema when <see cref="Succeeded"/>, otherwise null.</summary>
        public DatabaseSchema Schema { get; }

        /// <summary>What went wrong, as the tool or the database reported it. Null on success.</summary>
        public string Error { get; }

        public bool Succeeded => Schema != null;

        public static SchemaReadResult Success(DatabaseSchema schema)
        {
            return new SchemaReadResult(schema, null);
        }

        public static SchemaReadResult Failure(string error)
        {
            return new SchemaReadResult(null, string.IsNullOrWhiteSpace(error)
                ? "The efrpg tool returned no output and gave no reason."
                : error.Trim());
        }
    }
}
