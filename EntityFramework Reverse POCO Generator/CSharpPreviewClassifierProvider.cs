using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>Attaches <see cref="CSharpPreviewClassifier"/> to every buffer of the preview content type.</summary>
    [Export(typeof(IClassifierProvider))]
    [ContentType(CSharpPreviewContentType.Name)]
    internal sealed class CSharpPreviewClassifierProvider : IClassifierProvider
    {
        [Import]
        internal IClassificationTypeRegistryService ClassificationTypes { get; set; }

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            return buffer.Properties.GetOrCreateSingletonProperty(() => new CSharpPreviewClassifier(ClassificationTypes));
        }
    }
}
