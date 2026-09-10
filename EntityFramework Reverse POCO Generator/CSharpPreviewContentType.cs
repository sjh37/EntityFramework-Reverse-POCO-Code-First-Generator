using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     A content type of our own for the read-only C# previews in the settings editor.
    /// </summary>
    /// <remarks>
    ///     Giving the preview buffer the real "CSharp" content type looks right and colours nothing: Roslyn only
    ///     classifies buffers that belong to one of its workspaces, and a buffer made for a dialog belongs to none.
    ///     A private content type based on "text" gets no language service at all, so the only classifier that
    ///     runs is <see cref="CSharpPreviewClassifier"/>, which is enough for keywords, strings, comments and
    ///     numbers and takes its colours from the user's Fonts and Colours settings.
    /// </remarks>
    internal static class CSharpPreviewContentType
    {
        public const string Name = "EfrpgCSharpPreview";

        [Export]
        [Name(Name)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition Definition;
    }
}
