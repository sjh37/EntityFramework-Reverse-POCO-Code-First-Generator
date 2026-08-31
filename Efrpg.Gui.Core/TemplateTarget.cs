using System;
using System.Collections.Generic;
using System.Linq;

namespace Efrpg.Gui
{
    /// <summary>
    ///     One entry in the wizard's template dropdown: which TemplateType to write, and the GeneratorType that has
    ///     to go with it.
    /// </summary>
    /// <remarks>
    ///     The pairing is the reason this is a type rather than a list of strings. Settings.TemplateType and
    ///     Settings.GeneratorType are independent in the generator - nothing derives one from the other - so a user
    ///     who picks Ef6 in a dropdown that writes only TemplateType ends up with EF6 templates driven by the EF Core
    ///     generator, which produces code that does not compile. The wizard therefore always writes both, and the
    ///     mapping lives here where it can be tested.
    ///
    ///     Ordered by what a new user most likely wants rather than by the enum, and identified by member name for
    ///     the same reason as <see cref="DatabaseTarget"/>: TemplateType is net48 and this assembly is
    ///     netstandard2.0. <c>TemplateTargetTests</c> checks the list against settings-metadata.v4.json.
    /// </remarks>
    public sealed class TemplateTarget
    {
        private TemplateTarget(string name, string displayName, string generatorTypeName, bool requiresTemplateFolder)
        {
            Name                   = name;
            DisplayName            = displayName;
            GeneratorTypeName      = generatorTypeName;
            RequiresTemplateFolder = requiresTemplateFolder;
        }

        /// <summary>The TemplateType enum member name, written into the .tt verbatim.</summary>
        public string Name { get; }

        /// <summary>What the dropdown shows.</summary>
        public string DisplayName { get; }

        /// <summary>The GeneratorType enum member name that must accompany it.</summary>
        public string GeneratorTypeName { get; }

        /// <summary>
        ///     True for the FileBased templates, which read mustache files from Settings.TemplateFolder. The wizard
        ///     does not set that folder, so it says so rather than leaving the user with a template that fails on
        ///     first save.
        /// </summary>
        public bool RequiresTemplateFolder { get; }

        private const string EfCore = "EfCore";
        private const string Ef6    = "Ef6";

        /// <summary>Newest first, then EF6, then the file-based variants, which are an advanced choice.</summary>
        public static IReadOnlyList<TemplateTarget> All { get; } = new[]
        {
            new TemplateTarget("EfCore10",        "EF Core 10",                   EfCore, false),
            new TemplateTarget("EfCore9",         "EF Core 9",                    EfCore, false),
            new TemplateTarget("EfCore8",         "EF Core 8",                    EfCore, false),
            new TemplateTarget("Ef6",             "Entity Framework 6",           Ef6,    false),
            new TemplateTarget("FileBasedCore10", "File based - EF Core 10",      EfCore, true),
            new TemplateTarget("FileBasedCore9",  "File based - EF Core 9",       EfCore, true),
            new TemplateTarget("FileBasedCore8",  "File based - EF Core 8",       EfCore, true),
            new TemplateTarget("FileBasedEf6",    "File based - EF 6",            Ef6,    true)
        };

        /// <summary>What the dialog opens on, and what the shipped Database.tt already says.</summary>
        public static TemplateTarget Default => All[0];

        /// <summary>Returns the target with this enum member name, or null. Case sensitive, as the enum is.</summary>
        public static TemplateTarget Find(string name)
        {
            return All.FirstOrDefault(t => string.Equals(t.Name, name, StringComparison.Ordinal));
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
