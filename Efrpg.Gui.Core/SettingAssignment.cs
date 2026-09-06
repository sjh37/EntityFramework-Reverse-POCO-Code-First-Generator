namespace Efrpg.Gui
{
    /// <summary>
    ///     One <c>Settings.Something = ...;</c> found in a particular .tt, and exactly where its value sits.
    /// </summary>
    /// <remarks>
    ///     The span is the whole point. Writing back replaces those characters and nothing else, so the alignment,
    ///     the trailing comment, the line endings and every other line survive untouched. Anything that re-renders
    ///     the file instead - a syntax tree, a serialiser, a regenerated template - eats the comments a paying
    ///     customer wrote, which is the one failure this feature cannot have.
    /// </remarks>
    public sealed class SettingAssignment
    {
        public SettingAssignment(string name, string valueText, int valueStart, int lineNumber, int endLineNumber,
            bool isCommentedOut, int commentMarkerStart, int commentMarkerLength, bool spansMultipleLines, string indent)
        {
            Indent              = indent ?? string.Empty;
            Name                = name;
            ValueText           = valueText ?? string.Empty;
            ValueStart          = valueStart;
            LineNumber          = lineNumber;
            EndLineNumber       = endLineNumber;
            IsCommentedOut      = isCommentedOut;
            CommentMarkerStart  = commentMarkerStart;
            CommentMarkerLength = commentMarkerLength;
            SpansMultipleLines  = spansMultipleLines;
        }

        public string Name { get; }

        /// <summary>
        ///     The whitespace before the line's first character. Deeper than the block's usual indentation means
        ///     the assignment sits inside an if or a loop, which is not a place to add a neighbour.
        /// </summary>
        public string Indent { get; }

        /// <summary>The right-hand side exactly as written, without the semicolon.</summary>
        public string ValueText { get; }

        /// <summary>Index into the template text where <see cref="ValueText"/> begins.</summary>
        public int ValueStart { get; }

        public int ValueLength => ValueText.Length;

        /// <summary>One-based, for showing the user where a setting they cannot edit actually lives.</summary>
        public int LineNumber { get; }

        /// <summary>
        ///     One-based line holding the terminating semicolon. The same as <see cref="LineNumber"/> unless the
        ///     assignment spans several lines. A new line added after this assignment goes after this one.
        /// </summary>
        public int EndLineNumber { get; }

        /// <summary>
        ///     The line is commented out, so the generator never sees it. Shown, because a user looking for a
        ///     setting needs to be told it is switched off rather than left to conclude it does not exist.
        /// </summary>
        public bool IsCommentedOut { get; }

        /// <summary>
        ///     Index into the template text of the <c>//</c> marker and the whitespace after it, when
        ///     <see cref="IsCommentedOut"/>. Removing exactly that span switches the line on without moving its
        ///     alignment relative to the lines around it. Minus one otherwise.
        /// </summary>
        public int CommentMarkerStart { get; }

        public int CommentMarkerLength { get; }

        /// <summary>
        ///     A lambda or an object initialiser running past the end of its first line. Never rewritten from a
        ///     form: the value is code, and the form has nothing to put back.
        /// </summary>
        public bool SpansMultipleLines { get; }

        public override string ToString()
        {
            return "Settings." + Name + " = " + ValueText;
        }
    }
}
