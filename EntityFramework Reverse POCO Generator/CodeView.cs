using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     A read-only view of a piece of C# with Visual Studio's own syntax colouring, for showing a callback's
    ///     body inside a dialog.
    /// </summary>
    /// <remarks>
    ///     This is the editor Visual Studio itself uses, hosted as a control: the buffer carries the C# content type,
    ///     so the classifier colours keywords, strings and comments and the view follows the user's theme and font.
    ///     The buffer belongs to no project, so identifiers get no semantic colour - a fair trade for needing no
    ///     third-party highlighter. Anything failing to come up falls back to a plain monospace box, because a
    ///     dialog that cannot open is worse than one without colour.
    ///
    ///     A text view holds native resources until it is closed, so the owner disposes each one when the row it
    ///     sits in goes away.
    /// </remarks>
    internal sealed class CodeView : IDisposable
    {
        private readonly IWpfTextView _view;

        private CodeView(FrameworkElement element, IWpfTextView view)
        {
            Element = element;
            _view   = view;
        }

        public FrameworkElement Element { get; }

        public static CodeView Create(string code)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var text  = code ?? string.Empty;
            var lines = text.Split('\n').Length;

            try
            {
                var componentModel = (IComponentModel) ServiceProvider.GlobalProvider.GetService(typeof(SComponentModel));
                var editorFactory  = componentModel.GetService<ITextEditorFactoryService>();
                var bufferFactory  = componentModel.GetService<ITextBufferFactoryService>();
                var contentTypes   = componentModel.GetService<IContentTypeRegistryService>();

                // Our own content type, not "CSharp": Roslyn colours only the buffers in its workspaces, and this
                // one is in none, so the real C# content type gives the chrome and no colour at all. The preview
                // type carries a lexical classifier of ours instead - see CSharpPreviewClassifier.
                var contentType = contentTypes.GetContentType(CSharpPreviewContentType.Name) ?? contentTypes.GetContentType("text");
                var buffer      = bufferFactory.CreateTextBuffer(text, contentType);

                // Interactive so the text can be selected and copied; not Editable or Document, so nothing offers
                // to change it or hangs a file health bar off it.
                var roles = editorFactory.CreateTextViewRoleSet(PredefinedTextViewRoles.Interactive);
                var view  = editorFactory.CreateTextView(buffer, roles);

                view.Options.SetOptionValue(DefaultTextViewOptions.ViewProhibitUserInputId, true);
                view.Options.SetOptionValue(DefaultTextViewOptions.WordWrapStyleId, WordWrapStyles.None);
                view.Options.SetOptionValue(DefaultTextViewHostOptions.LineNumberMarginId, false);
                view.Options.SetOptionValue(DefaultTextViewHostOptions.HorizontalScrollBarId, true);
                view.Options.SetOptionValue(DefaultTextViewHostOptions.GlyphMarginId, false);
                view.Options.SetOptionValue(DefaultTextViewHostOptions.SelectionMarginId, false);
                view.Options.SetOptionValue(DefaultTextViewHostOptions.ZoomControlId, false);

                var host    = editorFactory.CreateTextViewHost(view, false);
                var control = host.HostControl;

                // Tall enough for the whole statement up to a screenful, then the view's own scrollbar takes over.
                control.Height = Math.Min(lines, 22) * 17 + 10;
                control.Margin = new Thickness(0, 4, 0, 4);

                return new CodeView(control, view);
            }
            catch (Exception ex)
            {
                // The reason goes on the first line rather than into a log nobody opens: a plain box that says
                // why it is plain is the only way this failure ever gets reported.
                var box = new TextBox
                {
                    Text = "// Syntax colouring unavailable: " + ex.GetType().Name + " - " + ex.Message.Replace("\r\n", " ").Replace("\n", " ") + Environment.NewLine + text,
                    IsReadOnly = true,
                    FontFamily = new FontFamily("Consolas"),
                    TextWrapping = TextWrapping.Wrap,
                    AcceptsReturn = true,
                    VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                    MaxHeight = 22 * 17 + 10,
                    Margin = new Thickness(0, 4, 0, 4),
                    Padding = new Thickness(6, 4, 6, 4)
                };

                return new CodeView(box, null);
            }
        }

        public void Dispose()
        {
            if (_view != null && !_view.IsClosed)
                _view.Close();
        }
    }
}
