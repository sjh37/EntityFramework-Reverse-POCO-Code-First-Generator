using System;

namespace Efrpg.Gui
{
    /// <summary>
    ///     The four on/off choices the connection dialog offers beside the connection details: how the output is
    ///     laid out and whether a fake context is generated for tests.
    /// </summary>
    /// <remarks>
    ///     Each is a single-line boolean in the shipped Database.tt. One the file does not express that way - absent,
    ///     commented out, or set to an expression - reads as its default and is left alone when written, the same
    ///     rule <see cref="TemplateConfiguration" /> applies to everything else.
    /// </remarks>
    public sealed class TemplateOptions
    {
        public TemplateOptions(bool generateSeparateFiles, bool useFileScopedNamespaces, bool addUnitTestingDbContext,
            bool fakeDbContextInDebugOnlyMode)
        {
            GenerateSeparateFiles        = generateSeparateFiles;
            UseFileScopedNamespaces      = useFileScopedNamespaces;
            AddUnitTestingDbContext      = addUnitTestingDbContext;
            FakeDbContextInDebugOnlyMode = fakeDbContextInDebugOnlyMode;
        }

        /// <summary>What the shipped Database.tt says.</summary>
        public static TemplateOptions Default { get; } = new TemplateOptions(false, false, true, false);

        /// <summary>One file per class in sub-folders, rather than everything in the one .cs beside the .tt.</summary>
        public bool GenerateSeparateFiles { get; }

        /// <summary>C# 10 <c>namespace X;</c> rather than a namespace block.</summary>
        public bool UseFileScopedNamespaces { get; }

        /// <summary>Generate a FakeDbContext and FakeDbSet for unit tests.</summary>
        public bool AddUnitTestingDbContext { get; }

        /// <summary>Wrap the fake classes in <c>#if DEBUG</c> so Release builds leave them out.</summary>
        public bool FakeDbContextInDebugOnlyMode { get; }

        public static TemplateOptions ReadFrom(TemplateSettingsFile settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            return new TemplateOptions(
                Read(settings, "GenerateSeparateFiles",        Default.GenerateSeparateFiles),
                Read(settings, "UseFileScopedNamespaces",      Default.UseFileScopedNamespaces),
                Read(settings, "AddUnitTestingDbContext",      Default.AddUnitTestingDbContext),
                Read(settings, "FakeDbContextInDebugOnlyMode", Default.FakeDbContextInDebugOnlyMode));
        }

        /// <summary>Writes each option over its single-line assignment; lines not in that shape are left alone.</summary>
        public void ApplyTo(TemplateSettingsFile settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            Write(settings, "GenerateSeparateFiles",        GenerateSeparateFiles);
            Write(settings, "UseFileScopedNamespaces",      UseFileScopedNamespaces);
            Write(settings, "AddUnitTestingDbContext",      AddUnitTestingDbContext);
            Write(settings, "FakeDbContextInDebugOnlyMode", FakeDbContextInDebugOnlyMode);
        }

        private static bool Read(TemplateSettingsFile settings, string name, bool fallback)
        {
            var expression = settings.GetExpression(name);
            if (expression == "true")
                return true;
            if (expression == "false")
                return false;

            return fallback;
        }

        private static void Write(TemplateSettingsFile settings, string name, bool value)
        {
            // Only a line that already holds a plain true or false is rewritten, so an expression a user put there
            // stays theirs.
            var current = settings.GetExpression(name);
            if (current != "true" && current != "false")
                return;

            settings.TrySetExpression(name, value ? "true" : "false");
        }
    }
}
