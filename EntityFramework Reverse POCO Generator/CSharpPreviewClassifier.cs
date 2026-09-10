using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     Lexical colouring for a C# snippet: comments, strings, characters, numbers and keywords, using the
    ///     standard classification types so the editor's theme supplies the colours.
    /// </summary>
    /// <remarks>
    ///     The whole snapshot is scanned on every request rather than the requested span, because a block comment
    ///     or verbatim string can start well before the span. Snippets are a few dozen lines, so this costs nothing.
    /// </remarks>
    internal sealed class CSharpPreviewClassifier : IClassifier
    {
        private static readonly HashSet<string> Keywords = new HashSet<string>(StringComparer.Ordinal)
        {
            "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const",
            "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern",
            "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface",
            "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override",
            "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short",
            "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof",
            "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "var", "virtual", "void", "volatile", "while",
            "yield", "async", "await", "nameof", "when", "where", "select", "from", "get", "set"
        };

        private readonly IClassificationType _comment;
        private readonly IClassificationType _string;
        private readonly IClassificationType _number;
        private readonly IClassificationType _keyword;

        public CSharpPreviewClassifier(IClassificationTypeRegistryService registry)
        {
            _comment = registry.GetClassificationType(PredefinedClassificationTypeNames.Comment);
            _string  = registry.GetClassificationType(PredefinedClassificationTypeNames.String);
            _number  = registry.GetClassificationType(PredefinedClassificationTypeNames.Number);
            _keyword = registry.GetClassificationType(PredefinedClassificationTypeNames.Keyword);
        }

        /// <summary>Never raised: the preview buffers are read-only, so a classification never changes.</summary>
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged
        {
            add { }
            remove { }
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            var snapshot = span.Snapshot;
            var text     = snapshot.GetText();
            var result   = new List<ClassificationSpan>();
            var i        = 0;

            while (i < text.Length)
            {
                var c    = text[i];
                var next = i + 1 < text.Length ? text[i + 1] : '\0';

                if (c == '/' && next == '/')
                {
                    var end = text.IndexOfAny(new[] { '\r', '\n' }, i);
                    i = Add(result, snapshot, span, i, end < 0 ? text.Length : end, _comment);
                    continue;
                }

                if (c == '/' && next == '*')
                {
                    var end = text.IndexOf("*/", i + 2, StringComparison.Ordinal);
                    i = Add(result, snapshot, span, i, end < 0 ? text.Length : end + 2, _comment);
                    continue;
                }

                if (c == '@' && next == '"')
                {
                    var j = i + 2;
                    while (j < text.Length)
                    {
                        if (text[j] == '"' && j + 1 < text.Length && text[j + 1] == '"') { j += 2; continue; }
                        if (text[j] == '"') { j++; break; }
                        j++;
                    }
                    i = Add(result, snapshot, span, i, j, _string);
                    continue;
                }

                if (c == '"' || c == '\'')
                {
                    var j = i + 1;
                    while (j < text.Length && text[j] != c && text[j] != '\n')
                        j += text[j] == '\\' ? 2 : 1;
                    i = Add(result, snapshot, span, i, Math.Min(j + 1, text.Length), _string);
                    continue;
                }

                if (char.IsDigit(c))
                {
                    var j = i;
                    while (j < text.Length && (char.IsLetterOrDigit(text[j]) || text[j] == '.' || text[j] == '_'))
                        j++;
                    i = Add(result, snapshot, span, i, j, _number);
                    continue;
                }

                if (char.IsLetter(c) || c == '_')
                {
                    var j = i;
                    while (j < text.Length && (char.IsLetterOrDigit(text[j]) || text[j] == '_'))
                        j++;

                    // A keyword used as a member name (x.string) is a name, not a keyword.
                    var afterDot = i > 0 && text[i - 1] == '.';
                    if (!afterDot && Keywords.Contains(text.Substring(i, j - i)))
                        Add(result, snapshot, span, i, j, _keyword);

                    i = j;
                    continue;
                }

                i++;
            }

            return result;
        }

        private static int Add(List<ClassificationSpan> result, ITextSnapshot snapshot, SnapshotSpan requested,
            int start, int end, IClassificationType type)
        {
            if (end > start)
            {
                var candidate = new SnapshotSpan(snapshot, start, end - start);
                if (candidate.IntersectsWith(requested))
                    result.Add(new ClassificationSpan(candidate, type));
            }

            return Math.Max(end, start + 1);
        }
    }
}
