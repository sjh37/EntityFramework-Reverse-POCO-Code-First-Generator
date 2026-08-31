using System;
using System.Collections.Generic;
using System.Linq;

namespace Efrpg.Gui
{
    /// <summary>
    ///     Everything the generator declares, loaded from a settings-metadata file.
    /// </summary>
    /// <remarks>
    ///     There is one of these per template version. v3 and v4 have different settings - v4 removed
    ///     FileManagerType and DatabaseReaderPlugin, among others - so the editor loads the file matching the
    ///     include directive on line 1 rather than assuming the current one. Almost the whole installed base is
    ///     still on v3, so getting that wrong would mean showing most users settings their template cannot use.
    /// </remarks>
    public sealed class SettingsCatalogue
    {
        private readonly Dictionary<string, SettingDefinition> _byName;

        private SettingsCatalogue(string version, IReadOnlyList<SettingDefinition> settings)
        {
            Version  = version;
            Settings = settings;
            _byName  = settings.ToDictionary(s => s.Name, StringComparer.Ordinal);
        }

        /// <summary>"v3" or "v4", as recorded in the metadata file itself.</summary>
        public string Version { get; }

        /// <summary>In the order BuildTT emitted them, which is the order they appear in Database.tt.</summary>
        public IReadOnlyList<SettingDefinition> Settings { get; }

        /// <summary>The section headings, in the order they first appear.</summary>
        public IReadOnlyList<string> Sections =>
            Settings.Select(s => s.Section).Distinct(StringComparer.Ordinal).ToList();

        public SettingDefinition Find(string name)
        {
            SettingDefinition definition;

            return name != null && _byName.TryGetValue(name, out definition) ? definition : null;
        }

        public static SettingsCatalogue Load(string metadataJson)
        {
            var root = Json.Parse(metadataJson);

            var settings = root["settings"].Items
                .Select(Read)
                .Where(s => s.Name.Length > 0)
                .ToList();

            return new SettingsCatalogue(Text(root["templateVersion"]) ?? "v4", settings);
        }

        private static SettingDefinition Read(Json setting)
        {
            return new SettingDefinition(
                Text(setting["name"]),
                Text(setting["type"]),
                ParseKind(Text(setting["kind"])),
                Text(setting["section"]),
                Text(setting["help"]),
                Text(setting["defaultValue"]),
                Flag(setting["isFlags"]),
                Flag(setting["runtimeOnly"]),
                Members(setting["enumMembers"]));
        }

        private static IReadOnlyList<EnumMember> Members(Json members)
        {
            if (members == null)
                return new EnumMember[0];

            return members.Items
                .Select(m => new EnumMember(Text(m["name"]), m["value"] == null ? 0 : m["value"].AsInteger))
                .ToList();
        }

        /// <summary>
        ///     An unrecognised kind maps to Unknown, which the editor treats as read-only. A metadata file written
        ///     by a newer BuildTT must never make an older editor offer to change something it does not understand.
        /// </summary>
        private static SettingKind ParseKind(string kind)
        {
            switch (kind)
            {
                case "string":     return SettingKind.Text;
                case "bool":       return SettingKind.Boolean;
                case "number":     return SettingKind.Number;
                case "char":       return SettingKind.Character;
                case "enum":       return SettingKind.Enumeration;
                case "stringList": return SettingKind.StringList;
                case "callback":   return SettingKind.Callback;
                case "complex":    return SettingKind.Complex;
                default:           return SettingKind.Unknown;
            }
        }

        private static string Text(Json value)
        {
            return value == null || value.IsNull ? null : value.AsString;
        }

        private static bool Flag(Json value)
        {
            return value != null && value.AsBoolean;
        }
    }
}
