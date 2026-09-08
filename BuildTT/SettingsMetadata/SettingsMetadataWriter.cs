using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace BuildTT.SettingsMetadata
{
    /// <summary>
    ///     Writes settings-metadata.v4.json alongside Database.tt: the list of settings the GUI may offer, each with
    ///     its type, its enum members, the default the shipped template uses and the help text that documents it.
    /// </summary>
    /// <remarks>
    ///     Two sources, each answering what only it can. Reflection over Efrpg.Settings is the authority on which
    ///     settings exist and what type each one is - it cannot go stale, because it is the same assembly the
    ///     generator runs. The source text of Database.tt and Settings.cs is the authority on documentation and on
    ///     the value a newly added template starts with, neither of which survives compilation.
    ///
    ///     Values are emitted as the source text that produced them, never as reflected runtime values. The runtime
    ///     value is frequently not what a template should contain: Settings.Namespace evaluates to "Efrpg" where the
    ///     template says DefaultNamespace. The GUI writes C# into a .tt file, so the source text is the thing it
    ///     actually needs.
    ///
    ///     Nothing here reads a field's value, so the Settings static constructor never runs.
    /// </remarks>
    public static class SettingsMetadataWriter
    {
        /// <summary>
        ///     Version of this file's own shape. Bump it when the GUI would misread an older file; it is unrelated to
        ///     the efrpg wire format's SchemaVersion.
        /// </summary>
        public const int MetadataVersion = 1;

        public const string Filename = "settings-metadata.v4.json";

        /// <summary>
        ///     Members of Settings that the generator fills in at run time. They are emitted, because dropping a
        ///     member silently is the worse failure, but flagged so the GUI does not offer them for editing.
        /// </summary>
        private static readonly Dictionary<string, string> RuntimeOnly = new Dictionary<string, string>
        {
            { "Root",          "Set by the T4 host from Host.ResolvePath." },
            { "TemplateFile",  "Set by the T4 host from Host.TemplateFile." },
            { "DefaultSchema", "Set by the database reader." }
        };

        public static void Create(string generatorRoot, string ttRoot, string version)
        {
            var fromTemplate = SettingsSourceParser
                .ParseTemplate(File.ReadAllText(Path.Combine(ttRoot, "Database.tt")))
                .ToDictionary(x => x.Name, x => x);

            var fromCode = SettingsSourceParser
                .ParseDeclarations(File.ReadAllText(Path.Combine(generatorRoot, "Settings.cs")))
                .ToDictionary(x => x.Name, x => x);

            var members = SettingsMembers().ToDictionary(x => x.Key, x => x.Value);

            // Written by Generator.Tests.Unit/DocSamples/build_index.py from the same table that builds the wiki's
            // Settings-Reference page, so the GUI links each setting to the page that actually documents it.
            var wikiPages = ReadWikiPages(Path.Combine(generatorRoot, "..", "BuildTT", "SettingsMetadata", "wiki-pages.json"));

            foreach (var orphan in fromTemplate.Keys.Where(name => !members.ContainsKey(name)))
                Console.WriteLine("WARNING: Database.tt assigns Settings." + orphan + ", which does not exist on Efrpg.Settings.");

            var json = new JsonBuilder();
            json.StartObject(null);
            json.Number("metadataVersion", MetadataVersion);
            json.String("generatorVersion", version);
            json.String("templateVersion", "v4");
            json.String("include", "EF.Reverse.POCO.v4.ttinclude");
            json.StartArray("settings");

            foreach (var name in Ordered(members.Keys, fromTemplate))
            {
                SettingSource template;
                SettingSource code;
                fromTemplate.TryGetValue(name, out template);
                fromCode.TryGetValue(name, out code);

                Type type;
                members.TryGetValue(name, out type);

                string wikiPage;
                wikiPages.TryGetValue(name, out wikiPage);

                Write(json, name, type, template, code, wikiPage);
            }

            json.EndArray();
            json.EndObject();

            File.WriteAllText(Path.Combine(ttRoot, Filename), json.ToString(), new UTF8Encoding(false));
        }

        /// <summary>
        ///     Settings the shipped template mentions come first, in template order, so the file reads the way
        ///     Database.tt does; the rest follow alphabetically. Reflection order is not stable, so it is never used.
        /// </summary>
        private static IEnumerable<string> Ordered(IEnumerable<string> names, Dictionary<string, SettingSource> fromTemplate)
        {
            var all = new SortedSet<string>(names, StringComparer.Ordinal);
            foreach (var name in fromTemplate.Keys)
                all.Add(name);

            return all
                .OrderBy(name => fromTemplate.ContainsKey(name) ? 0 : 1)
                .ThenBy(name => fromTemplate.ContainsKey(name) ? fromTemplate[name].Line : 0)
                .ThenBy(name => name, StringComparer.Ordinal);
        }

        /// <summary>
        ///     One line per setting, <c>"Name": "Page"</c>. A setting the file does not list links to the index page,
        ///     which always exists, so a missing entry costs a click rather than a broken link.
        /// </summary>
        private static Dictionary<string, string> ReadWikiPages(string path)
        {
            var pages = new Dictionary<string, string>(StringComparer.Ordinal);
            if (!File.Exists(path))
            {
                Console.WriteLine("WARNING: " + path + " not found; run Generator.Tests.Unit/DocSamples/build_index.py. Every wiki link will point at the index.");
                return pages;
            }

            foreach (var match in Regex.Matches(File.ReadAllText(path), "\"(?<name>[^\"]+)\"\\s*:\\s*\"(?<page>[^\"]+)\"").Cast<Match>())
                pages[match.Groups["name"].Value] = match.Groups["page"].Value;

            return pages;
        }

        private static void Write(JsonBuilder json, string name, Type type, SettingSource template, SettingSource code, string wikiPage)
        {
            var preferred = template ?? code;

            json.StartObject(null);
            json.String("name", name);
            json.String("wikiPage", wikiPage ?? "Settings-Reference");
            json.String("type", type == null ? null : TypeName(type));
            json.String("kind", Kind(type));
            json.String("section", preferred == null ? null : preferred.Section);
            json.String("help", First(template == null ? null : template.Help, code == null ? null : code.Help));
            json.String("defaultValue", template != null
                ? template.DefaultValue
                : code == null ? null : code.MultiLine ? QualifyForTemplate(code.DefaultValue) : code.DefaultValue);
            json.Bool("inDatabaseTt", template != null);
            json.Bool("commentedOut", template != null && template.CommentedOut);
            json.Bool("multiLine", preferred != null && preferred.MultiLine);
            json.Bool("runtimeOnly", RuntimeOnly.ContainsKey(name));

            if (type != null && type.IsEnum)
            {
                json.Bool("isFlags", type.GetCustomAttributes(typeof(FlagsAttribute), false).Any());
                json.StartArray("enumMembers");
                foreach (var member in EnumMembers(type))
                {
                    json.StartObject(null);
                    json.String("name", member.Key);
                    json.Number("value", member.Value);
                    json.EndObject();
                }
                json.EndArray();
            }

            json.EndObject();
        }

        /// <summary>
        ///     The help text and the default are taken from Database.tt where it documents the setting, and from
        ///     Settings.cs otherwise. Database.tt is the copy a user reads, so it wins when both have something.
        /// </summary>
        private static string First(string preferred, string fallback)
        {
            return string.IsNullOrEmpty(preferred) ? (string.IsNullOrEmpty(fallback) ? null : fallback) : preferred;
        }

        /// <summary>
        ///     A body written inside the Settings class names its members bare - <c>IsEfCore8Plus()</c>,
        ///     <c>HiLoSequences</c> - which does not compile once the GUI pastes it into a .tt, where the same code
        ///     runs inside the template class. Every bare member reference gets its <c>Settings.</c> prefix.
        /// </summary>
        private static string QualifyForTemplate(string body)
        {
            if (string.IsNullOrEmpty(body))
                return body;

            var settings = typeof(Efrpg.Settings);
            var names = settings.GetFields(BindingFlags.Public | BindingFlags.Static).Select(f => f.Name)
                .Concat(settings.GetProperties(BindingFlags.Public | BindingFlags.Static).Select(p => p.Name))
                .Concat(settings.GetMethods(BindingFlags.Public | BindingFlags.Static).Select(m => m.Name))
                .Distinct()
                .OrderByDescending(n => n.Length);

            foreach (var name in names)
            {
                // Not already qualified, not part of a longer identifier, and not the type of the same name being
                // used as an enum (DatabaseType.MySql), which is what a following dot means for those settings.
                var pattern = @"(?<![\w.])" + Regex.Escape(name) + @"\b(?!\s*\.)";
                body = Regex.Replace(body, pattern, "Settings." + name);
            }

            return body;
        }

        private static IEnumerable<KeyValuePair<string, Type>> SettingsMembers()
        {
            var settings = typeof(Efrpg.Settings);

            foreach (var field in settings.GetFields(BindingFlags.Public | BindingFlags.Static).Where(x => !x.IsLiteral))
                yield return new KeyValuePair<string, Type>(field.Name, field.FieldType);

            foreach (var property in settings.GetProperties(BindingFlags.Public | BindingFlags.Static).Where(x => x.CanWrite))
                yield return new KeyValuePair<string, Type>(property.Name, property.PropertyType);
        }

        /// <summary>
        ///     Ordered by value then name so the file is byte-stable; Enum.GetNames makes no ordering promise the
        ///     runtime is obliged to keep.
        /// </summary>
        private static IEnumerable<KeyValuePair<string, long>> EnumMembers(Type type)
        {
            return Enum
                .GetNames(type)
                .Select(x => new KeyValuePair<string, long>(x, Convert.ToInt64(Enum.Parse(type, x))))
                .OrderBy(x => x.Value)
                .ThenBy(x => x.Key, StringComparer.Ordinal);
        }

        /// <summary>
        ///     How the GUI should render the setting. "callback" and "complex" are the ones it must show read-only,
        ///     because no dialog can represent a lambda.
        /// </summary>
        private static string Kind(Type type)
        {
            if (type == null)                              return "unknown";
            if (type.IsEnum)                               return "enum";
            if (typeof(Delegate).IsAssignableFrom(type))   return "callback";
            if (type == typeof(bool))                      return "bool";
            if (type == typeof(string))                    return "string";
            if (type == typeof(char))                      return "char";
            if (type == typeof(string[]))                  return "stringList";
            if (type == typeof(List<string>))              return "stringList";
            if (type == typeof(int) || type == typeof(short) || type == typeof(long)) return "number";

            return "complex";
        }

        /// <summary>
        ///     The type as it would be written in C#, because that is what the GUI has to put in the .tt file.
        /// </summary>
        private static string TypeName(Type type)
        {
            if (type == typeof(bool))   return "bool";
            if (type == typeof(string)) return "string";
            if (type == typeof(int))    return "int";
            if (type == typeof(char))   return "char";
            if (type == typeof(short))  return "short";
            if (type == typeof(long))   return "long";
            if (type == typeof(object)) return "object";

            if (type.IsArray)
                return TypeName(type.GetElementType()) + "[]";

            if (!type.IsGenericType)
                return type.Name;

            var name = type.Name.Substring(0, type.Name.IndexOf('`'));
            return name + "<" + string.Join(", ", type.GetGenericArguments().Select(TypeName).ToArray()) + ">";
        }
    }
}
