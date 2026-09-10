using System;
using System.Collections.Generic;
using System.Linq;

namespace Efrpg.Gui
{
    /// <summary>
    ///     One entry in the wizard's template dropdown: which TemplateType to write, and what to call it.
    /// </summary>
    /// <remarks>
    ///     TemplateType alone decides which generator runs - Ef6 is the only non-EF Core template - so this is the
    ///     one setting the dropdown writes. Ordered by what a new user most likely wants rather than by the enum, and
    ///     identified by member name for the same reason as <see cref="DatabaseTarget"/>: TemplateType is net48 and
    ///     this assembly is netstandard2.0. <c>TemplateTargetTests</c> checks the list against
    ///     settings-metadata.v4.json.
    /// </remarks>
    public sealed class TemplateTarget
    {
        private TemplateTarget(string name, string displayName)
        {
            Name        = name;
            DisplayName = displayName;
        }

        /// <summary>The TemplateType enum member name, written into the .tt verbatim.</summary>
        public string Name { get; }

        /// <summary>What the dropdown shows.</summary>
        public string DisplayName { get; }

        /// <summary>Newest first, then EF6.</summary>
        public static IReadOnlyList<TemplateTarget> All { get; } = new[]
        {
            new TemplateTarget("EfCore10", "EF Core 10"),
            new TemplateTarget("EfCore9",  "EF Core 9"),
            new TemplateTarget("EfCore8",  "EF Core 8"),
            new TemplateTarget("Ef6",      "Entity Framework 6")
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
