using System;
using System.Collections.Generic;
using System.Linq;

namespace Efrpg.Gui
{
    /// <summary>
    ///     What the generator knows about one setting: its type, its help text, its default, and - for an enum -
    ///     the members it accepts.
    /// </summary>
    /// <remarks>
    ///     Read from settings-metadata.v4.json, which BuildTT emits by reflecting over <c>Efrpg.Settings</c> and
    ///     reading the trailing <c>//</c> comments in the generated Database.tt. That is the whole reason Phase 0
    ///     came first: the editor needs to know what a setting *is*, and the only place that cannot go stale is the
    ///     generator's own source.
    /// </remarks>
    public sealed class SettingDefinition
    {
        public SettingDefinition(string name, string type, SettingKind kind, string section, string help,
            string defaultValue, bool isFlags, bool runtimeOnly, IReadOnlyList<EnumMember> enumMembers)
        {
            Name         = name ?? string.Empty;
            Type         = type ?? string.Empty;
            Kind         = kind;
            Section      = string.IsNullOrEmpty(section) ? "Other settings" : section;
            Help         = help ?? string.Empty;
            DefaultValue = defaultValue;
            IsFlags      = isFlags;
            RuntimeOnly  = runtimeOnly;
            EnumMembers  = enumMembers ?? new EnumMember[0];
        }

        public string Name { get; }

        /// <summary>The declared C# type, shown to the user for anything that cannot be edited in a form.</summary>
        public string Type { get; }

        public SettingKind Kind { get; }

        /// <summary>The heading it sits under in Database.tt, which is what groups it in the editor.</summary>
        public string Section { get; }

        /// <summary>The trailing comment from Database.tt, which is the only per-setting documentation there is.</summary>
        public string Help { get; }

        /// <summary>The right-hand side the generator itself declares, or null when there is none.</summary>
        public string DefaultValue { get; }

        /// <summary>Combined with | rather than chosen from, so it needs a checklist and not a dropdown.</summary>
        public bool IsFlags { get; }

        /// <summary>Set by the generator while it runs, so writing it into a .tt achieves nothing.</summary>
        public bool RuntimeOnly { get; }

        public IReadOnlyList<EnumMember> EnumMembers { get; }

        /// <summary>
        ///     True when a form can represent the value without losing anything. Everything else is shown and
        ///     labelled rather than hidden, because a user who wrote a lambda wants to see that it is still there.
        /// </summary>
        public bool IsEditable =>
            !RuntimeOnly &&
            (Kind == SettingKind.Text || Kind == SettingKind.Boolean || Kind == SettingKind.Number ||
             Kind == SettingKind.Character || Kind == SettingKind.Enumeration);

        /// <summary>Why the editor will not offer to change it, or null when it will.</summary>
        public string ReadOnlyReason
        {
            get
            {
                if (RuntimeOnly)
                    return "Set by the generator while it runs.";

                switch (Kind)
                {
                    case SettingKind.Callback:   return "A callback you write in code - edit it in the editor.";
                    case SettingKind.StringList: return "A list built in code - edit it in the editor.";
                    case SettingKind.Complex:    return "Built in code from " + Type + " - edit it in the editor.";
                    case SettingKind.Unknown:    return "This version of the editor does not know how to change it.";
                    default:                     return null;
                }
            }
        }

        public EnumMember FindMember(string name)
        {
            return EnumMembers.FirstOrDefault(m => string.Equals(m.Name, name, StringComparison.Ordinal));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
