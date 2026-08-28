namespace BuildTT.SettingsMetadata
{
    /// <summary>
    ///     One <c>Settings.*</c> assignment as it was found in a source file, before it is matched against the
    ///     reflected type.
    /// </summary>
    public class SettingSource
    {
        /// <summary>
        ///     The member name, without the "Settings." prefix.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The right-hand side exactly as written, or null when the statement spans more than one line.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        ///     The text of the trailing // comment, or null when the line has none.
        /// </summary>
        public string Help { get; set; }

        /// <summary>
        ///     The most recent "// Something ******" banner above this line, or null if there was none.
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        ///     One-based line number of the assignment, used to keep the emitted order the same as the file's.
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        ///     True when the assignment itself is commented out, as the optional settings in Database.tt are.
        /// </summary>
        public bool CommentedOut { get; set; }

        /// <summary>
        ///     True when the statement spans more than one line, in which case <see cref="DefaultValue" /> is null.
        /// </summary>
        public bool MultiLine { get; set; }
    }
}
