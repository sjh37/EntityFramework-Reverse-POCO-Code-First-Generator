using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Efrpg.Gui
{
    /// <summary>
    ///     The names of everything the efrpg tool found, pulled out of its XML payload.
    /// </summary>
    /// <remarks>
    ///     **Not a second EfrpgResultXmlReader.** That reader builds the generator's full object model - columns,
    ///     types, keys, indexes, sequences, triggers - and must stay plain source under Generator/ so BuildTT can
    ///     concatenate it into the .ttinclude. This reads four attributes and ignores the rest, because a picker
    ///     needs names and nothing else. Duplicating the real reader here would be a parallel copy of the whole
    ///     wire contract; reading a handful of attribute names is not.
    ///
    ///     Attributes it does not recognise are ignored, which is what makes a newer tool work with an older GUI -
    ///     the same forward-compatibility rule the wire format is built on.
    /// </remarks>
    public sealed class DatabaseSchema
    {
        private DatabaseSchema(IReadOnlyList<DatabaseObject> objects, IReadOnlyList<string> errors,
            string defaultSchema, bool canReadStoredProcedures, int schemaVersion, string toolVersion)
        {
            Objects                 = objects;
            Errors                  = errors;
            DefaultSchema           = defaultSchema;
            CanReadStoredProcedures = canReadStoredProcedures;
            SchemaVersion           = schemaVersion;
            ToolVersion             = toolVersion;
        }

        public IReadOnlyList<DatabaseObject> Objects { get; }

        /// <summary>
        ///     Problems the tool reported while reading, such as a table named in the multi-context settings that
        ///     does not exist. A healthy run emits none, and they are never fatal on their own.
        /// </summary>
        public IReadOnlyList<string> Errors { get; }

        public string DefaultSchema { get; }

        /// <summary>
        ///     False when the account can connect but is not permitted to read stored procedure definitions, which
        ///     is worth saying out loud rather than showing an empty list.
        /// </summary>
        public bool CanReadStoredProcedures { get; }

        public int SchemaVersion { get; }

        public string ToolVersion { get; }

        public IEnumerable<DatabaseObject> Of(DatabaseObjectKind kind)
        {
            return Objects.Where(o => o.Kind == kind);
        }

        public int Count(DatabaseObjectKind kind)
        {
            return Objects.Count(o => o.Kind == kind);
        }

        /// <summary>
        ///     Parses a payload. Throws <see cref="FormatException"/> when the text is not an EfrpgResult document
        ///     at all, which is how a tool that wrote an error to stdout instead of stderr is caught.
        /// </summary>
        public static DatabaseSchema Parse(string xml)
        {
            XElement root;

            try
            {
                root = XDocument.Parse(xml ?? string.Empty).Root;
            }
            catch (Exception ex)
            {
                throw new FormatException("The efrpg tool did not return valid XML: " + ex.Message, ex);
            }

            if (root == null || root.Name.LocalName != "EfrpgResult")
                throw new FormatException("The efrpg tool did not return an EfrpgResult document.");

            var objects = Tables(root).Concat(Routines(root)).OrderBy(o => o).ToList();

            return new DatabaseSchema(
                objects,
                root.Elements("Errors").Elements("Row").Select(r => Attribute(r, "message")).Where(m => m.Length > 0).ToList(),
                Attribute(root, "defaultSchema"),
                Attribute(root, "canReadStoredProcedures") == "true",
                ParseInt(Attribute(root, "schemaVersion")),
                Attribute(root, "toolVersion"));
        }

        /// <summary>
        ///     Every row under Tables is a <em>column</em>, so the same table appears once per column and has to be
        ///     collapsed. Synonyms are skipped: they are an alias for something already listed.
        /// </summary>
        private static IEnumerable<DatabaseObject> Tables(XElement root)
        {
            return root.Elements("Tables").Elements("Row")
                .Where(r => Attribute(r, "isSynonym") != "true")
                .Select(r => new
                {
                    Schema = Attribute(r, "schemaName"),
                    Name   = Attribute(r, "tableName"),
                    Kind   = Attribute(r, "isView") == "true" ? DatabaseObjectKind.View : DatabaseObjectKind.Table
                })
                .Where(r => r.Name.Length > 0)
                .GroupBy(r => r.Schema + "." + r.Name, StringComparer.OrdinalIgnoreCase)
                .Select(g => new DatabaseObject(g.First().Schema, g.First().Name, g.First().Kind));
        }

        /// <summary>
        ///     Stored procedures and both flavours of function arrive in the same element, told apart by flags.
        /// </summary>
        private static IEnumerable<DatabaseObject> Routines(XElement root)
        {
            return root.Elements("StoredProcedures").Elements("Row")
                .Select(r => new DatabaseObject(
                    Attribute(r, "schema"),
                    Attribute(r, "name"),
                    Attribute(r, "isStoredProcedure") == "true"
                        ? DatabaseObjectKind.StoredProcedure
                        : DatabaseObjectKind.Function))
                .Where(o => o.Name.Length > 0);
        }

        private static string Attribute(XElement element, string name)
        {
            var attribute = element.Attribute(name);

            return attribute == null ? string.Empty : attribute.Value;
        }

        private static int ParseInt(string value)
        {
            int parsed;
            return int.TryParse(value, out parsed) ? parsed : 0;
        }
    }
}
