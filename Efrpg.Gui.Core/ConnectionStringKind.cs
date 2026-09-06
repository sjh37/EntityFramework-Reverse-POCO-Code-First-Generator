namespace Efrpg.Gui
{
    /// <summary>
    ///     Where a template's connection string comes from, as far as the GUI can tell from the one line that sets it.
    /// </summary>
    public enum ConnectionStringKind
    {
        /// <summary>A string literal. The dialog can show it, edit it and write it back.</summary>
        Literal,

        /// <summary>
        ///     <c>Environment.GetEnvironmentVariable("NAME")</c>, with or without a target. Read-only in the dialog,
        ///     but the value can be fetched here because the T4 runs in this same process as this same user.
        /// </summary>
        EnvironmentVariable,

        /// <summary><c>File.ReadAllText("path")</c> with a literal path. Read-only, and the file can be read here.</summary>
        File,

        /// <summary>Any other code. Read-only, and the GUI does not know its value.</summary>
        Expression,

        /// <summary>The template has no live <c>Settings.ConnectionString</c> line at all.</summary>
        Missing
    }
}
