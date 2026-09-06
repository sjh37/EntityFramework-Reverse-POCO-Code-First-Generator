using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Efrpg.Gui
{
    /// <summary>
    ///     Which tables, views, stored procedures and functions a template generates, as a set of ticks the user can
    ///     change, and the rules for turning those ticks back into FilterSettings code.
    /// </summary>
    /// <remarks>
    ///     **It mirrors the generator's filter, it does not replace it.** The starting state is what the template
    ///     generates right now, worked out by running the same tests SingleContextFilter runs: every exclude regex
    ///     against the raw name, every include regex or-ed together, the schema filters, the period rule, the
    ///     reserved MultiContext schema, and the five on/off flags. A filter the user wrote is shown as the reason
    ///     an object is locked, never overridden - the picker adds to the file, it does not fight it.
    ///
    ///     **A choice is saved as an include list, never an exclude list.** Ticking a subset means "these, and only
    ///     these": a table added to the database later does not appear in the generated code until somebody ticks
    ///     it, which for code in source control is the predictable behaviour. Ticking everything writes nothing at
    ///     all, so a template nobody has narrowed keeps generating whatever the database grows to hold.
    ///
    ///     Whole categories switch off through the flags rather than through a regex - unticking every view writes
    ///     <c>FilterSettings.IncludeViews = false</c> - because that is what the template's own comments tell a user
    ///     to do, and it is what they would expect to read afterwards.
    /// </remarks>
    public sealed class ObjectSelection
    {
        /// <summary>
        ///     Longest include pattern written on one line before starting another. The lines are merged by the
        ///     generator, so splitting is purely for a file somebody can read and diff.
        /// </summary>
        private const int PatternLineLength = 100;

        private readonly TemplateFilterDocument _document;
        private readonly List<Entry> _entries;
        private readonly Dictionary<DatabaseObjectKind, bool> _kindEnabled = new Dictionary<DatabaseObjectKind, bool>();

        private ObjectSelection(TemplateFilterDocument document, List<Entry> entries)
        {
            _document = document;
            _entries  = entries;

            foreach (DatabaseObjectKind kind in Enum.GetValues(typeof(DatabaseObjectKind)))
                _kindEnabled[kind] = FlagOn(document, kind);
        }

        /// <summary>True once the user has changed anything. Saving before that would be a no-op at best.</summary>
        public bool HasChanges { get; private set; }

        public IReadOnlyList<ObjectChoice> Choices => _entries.Select(Describe).ToList();

        public static ObjectSelection Create(DatabaseSchema schema, TemplateFilterDocument document)
        {
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));

            if (document == null)
                throw new ArgumentNullException(nameof(document));

            if (document.RefusalReason != null)
                throw new InvalidOperationException(document.RefusalReason);

            return new ObjectSelection(document, schema.Objects.Select(o => Classify(o, document)).ToList());
        }

        public ObjectChoice Choice(DatabaseObject databaseObject)
        {
            return Describe(Find(databaseObject));
        }

        /// <summary>Whether the generator will read this category at all, given the ticks so far.</summary>
        public bool IsKindEnabled(DatabaseObjectKind kind)
        {
            return _kindEnabled[kind];
        }

        /// <summary>
        ///     Ticks or unticks one object. Ticking anything switches its category on; unticking the last free
        ///     object in a category switches the category off, unless one of the user's own filters still includes
        ///     something in it.
        /// </summary>
        public void Select(DatabaseObject databaseObject, bool selected)
        {
            var entry = Find(databaseObject);

            if (entry.Standing != Standing.Free)
                throw new InvalidOperationException(entry.Reason);

            entry.Ticked = selected;
            HasChanges   = true;
            Recompute(entry.Object.Kind);
        }

        /// <summary>Ticks or unticks every free object of one kind, and switches the kind with it.</summary>
        public void SelectAll(DatabaseObjectKind kind, bool selected)
        {
            SelectAll(_entries.Where(e => e.Object.Kind == kind).Select(e => e.Object).ToList(), selected);
        }

        /// <summary>
        ///     Ticks or unticks the free objects among these - what a search box has narrowed the list to, say.
        ///     Locked objects are skipped rather than refused, because a bulk action over a mixed list is the
        ///     normal case.
        /// </summary>
        public void SelectAll(IEnumerable<DatabaseObject> objects, bool selected)
        {
            var kinds = new HashSet<DatabaseObjectKind>();

            foreach (var databaseObject in objects)
            {
                var entry = Find(databaseObject);
                if (entry.Standing != Standing.Free)
                    continue;

                entry.Ticked = selected;
                HasChanges   = true;
                kinds.Add(entry.Object.Kind);
            }

            foreach (var kind in kinds)
                Recompute(kind);
        }

        /// <summary>
        ///     The template text with the ticks written into it: the flags for whole categories, and one include
        ///     list per filter list where the ticks are narrower than the flags alone express.
        /// </summary>
        public string Apply()
        {
            var document = WriteFlags(_document);

            document = WritePatterns(document, FilterList.Table);
            document = WritePatterns(document, FilterList.StoredProcedure);

            return document.Text;
        }

        private TemplateFilterDocument WriteFlags(TemplateFilterDocument document)
        {
            var anyRoutine = _entries.Any(e => IsRoutine(e.Object.Kind));

            document = WriteFlag(document, FilterFlag.IncludeViews, DatabaseObjectKind.View, Has(DatabaseObjectKind.View));
            document = WriteFlag(document, FilterFlag.IncludeTableValuedFunctions, DatabaseObjectKind.TableValuedFunction, Has(DatabaseObjectKind.TableValuedFunction));
            document = WriteFlag(document, FilterFlag.IncludeScalarValuedFunctions, DatabaseObjectKind.ScalarValuedFunction, Has(DatabaseObjectKind.ScalarValuedFunction));

            // The generator forces IncludeStoredProcedures on whenever either function flag is on, and the
            // template's own comment tells the user to set it themselves. Written explicitly so the file says
            // what actually happens.
            if (anyRoutine)
            {
                var wanted = _kindEnabled[DatabaseObjectKind.StoredProcedure]
                             || _kindEnabled[DatabaseObjectKind.TableValuedFunction]
                             || _kindEnabled[DatabaseObjectKind.ScalarValuedFunction];

                if (wanted != document.Flag(FilterFlag.IncludeStoredProcedures))
                    document = document.WithFlag(FilterFlag.IncludeStoredProcedures, wanted);
            }

            return document;
        }

        private TemplateFilterDocument WriteFlag(TemplateFilterDocument document, FilterFlag flag, DatabaseObjectKind kind, bool present)
        {
            // A category the database does not have is left alone: there is nothing to say about it, and
            // flipping a flag for objects that do not exist would be noise in the user's diff.
            if (!present || _kindEnabled[kind] == document.Flag(flag))
                return document;

            return document.WithFlag(flag, _kindEnabled[kind]);
        }

        private TemplateFilterDocument WritePatterns(TemplateFilterDocument document, FilterList list)
        {
            // Nothing is written for a list this dialog could not fully evaluate: no choice was offered there, so
            // there is no choice to save.
            if (document.In(list).Any(f => !f.CanEvaluate) || document.In(FilterList.Schema).Any(f => !f.CanEvaluate))
                return document;

            var free   = _entries.Where(e => ListFor(e.Object.Kind) == list && e.Standing == Standing.Free && WillBeRead(e.Object.Kind)).ToList();
            var ticked = free.Where(e => e.Ticked).ToList();

            // With no include filter of the user's, everything free is generated unless narrowed, so a line is
            // needed only when something is unticked. With one, nothing free is generated unless added, so a line
            // is needed only when something is ticked. Either way, the include lines merge with the user's.
            var hasUserIncludes = document.In(list).Any(f => f.IsInclude && !f.IsPickerOwned);
            var needsLine       = hasUserIncludes ? ticked.Count > 0 : ticked.Count != free.Count;

            return document.WithPickerPatterns(list, needsLine ? Patterns(ticked.Select(e => e.Object.Name)) : new string[0]);
        }

        /// <summary>
        ///     Whether the generator will consider objects of this kind at all after the flags are written. Stored
        ///     procedures are read whenever any routine category is on, because the generator couples them.
        /// </summary>
        private bool WillBeRead(DatabaseObjectKind kind)
        {
            switch (kind)
            {
                case DatabaseObjectKind.Table:
                    return true;

                case DatabaseObjectKind.StoredProcedure:
                    return _kindEnabled[DatabaseObjectKind.StoredProcedure]
                           || _kindEnabled[DatabaseObjectKind.TableValuedFunction]
                           || _kindEnabled[DatabaseObjectKind.ScalarValuedFunction];

                default:
                    return _kindEnabled[kind];
            }
        }

        /// <summary>
        ///     One anchored alternation per line, names escaped and sorted so the same choice always produces the
        ///     same text and a one-table change is a one-line diff.
        /// </summary>
        public static IReadOnlyList<string> Patterns(IEnumerable<string> names)
        {
            var escaped  = names.Distinct(StringComparer.Ordinal).OrderBy(n => n, StringComparer.Ordinal).Select(Regex.Escape).ToList();
            var patterns = new List<string>();
            var current  = new List<string>();
            var length   = 0;

            foreach (var name in escaped)
            {
                if (current.Count > 0 && length + name.Length + 1 > PatternLineLength)
                {
                    patterns.Add(Pattern(current));
                    current = new List<string>();
                    length  = 0;
                }

                current.Add(name);
                length += name.Length + 1;
            }

            if (current.Count > 0)
                patterns.Add(Pattern(current));

            return patterns;
        }

        private static string Pattern(IEnumerable<string> escapedNames)
        {
            return "^(?:" + string.Join("|", escapedNames.ToArray()) + ")$";
        }

        private void Recompute(DatabaseObjectKind kind)
        {
            if (kind == DatabaseObjectKind.Table)
                return;

            var anyTicked = _entries.Any(e => e.Object.Kind == kind && e.Standing == Standing.Free && e.Ticked);
            var anyLocked = _entries.Any(e => e.Object.Kind == kind && e.Standing == Standing.LockedIn);

            // A category with something the user's own filter includes cannot be switched off from here, so it
            // stays on; the locked row says why.
            _kindEnabled[kind] = anyTicked || (anyLocked && _kindEnabled[kind]);
        }

        private ObjectChoice Describe(Entry entry)
        {
            var enabled = _kindEnabled[entry.Object.Kind];

            switch (entry.Standing)
            {
                case Standing.Free:
                    return new ObjectChoice(entry.Object, entry.Ticked, true, string.Empty);

                case Standing.LockedIn:
                    return new ObjectChoice(entry.Object, enabled, false,
                        enabled
                            ? entry.Reason
                            : entry.Reason + " It is not generated while " + Plural(entry.Object.Kind) + " are switched off; tick any to switch them on.");

                case Standing.LockedOut:
                    return new ObjectChoice(entry.Object, false, false, entry.Reason);

                default:
                    return new ObjectChoice(entry.Object, null, false, entry.Reason);
            }
        }

        private Entry Find(DatabaseObject databaseObject)
        {
            if (databaseObject == null)
                throw new ArgumentNullException(nameof(databaseObject));

            var entry = _entries.FirstOrDefault(e => ReferenceEquals(e.Object, databaseObject) || e.Object.CompareTo(databaseObject) == 0 && e.Object.Kind == databaseObject.Kind);
            if (entry == null)
                throw new ArgumentException("Not an object in this selection: " + databaseObject, nameof(databaseObject));

            return entry;
        }

        private bool Has(DatabaseObjectKind kind)
        {
            return _entries.Any(e => e.Object.Kind == kind);
        }

        private static bool IsRoutine(DatabaseObjectKind kind)
        {
            return ListFor(kind) == FilterList.StoredProcedure;
        }

        private static FilterList ListFor(DatabaseObjectKind kind)
        {
            return kind == DatabaseObjectKind.Table || kind == DatabaseObjectKind.View ? FilterList.Table : FilterList.StoredProcedure;
        }

        /// <summary>
        ///     The flag as the generator applies it. Stored procedures count as on whenever a function flag is on,
        ///     because SingleContextFilter forces exactly that.
        /// </summary>
        private static bool FlagOn(TemplateFilterDocument document, DatabaseObjectKind kind)
        {
            switch (kind)
            {
                case DatabaseObjectKind.Table:
                    return true;

                case DatabaseObjectKind.View:
                    return document.Flag(FilterFlag.IncludeViews);

                case DatabaseObjectKind.StoredProcedure:
                    return document.Flag(FilterFlag.IncludeStoredProcedures)
                           || document.Flag(FilterFlag.IncludeTableValuedFunctions)
                           || document.Flag(FilterFlag.IncludeScalarValuedFunctions);

                case DatabaseObjectKind.TableValuedFunction:
                    return document.Flag(FilterFlag.IncludeTableValuedFunctions);

                default:
                    return document.Flag(FilterFlag.IncludeScalarValuedFunctions);
            }
        }

        private static string Plural(DatabaseObjectKind kind)
        {
            switch (kind)
            {
                case DatabaseObjectKind.Table:                return "tables";
                case DatabaseObjectKind.View:                 return "views";
                case DatabaseObjectKind.StoredProcedure:      return "stored procedures";
                case DatabaseObjectKind.TableValuedFunction:  return "table-valued functions";
                default:                                      return "scalar functions";
            }
        }

        /// <summary>
        ///     The same decisions SingleContextFilter.IsExcluded makes, in the same order, with the reason kept.
        /// </summary>
        private static Entry Classify(DatabaseObject databaseObject, TemplateFilterDocument document)
        {
            var list    = ListFor(databaseObject.Kind);
            var schemas = document.In(FilterList.Schema).ToList();
            var filters = document.In(list).ToList();

            var unevaluable = schemas.Concat(filters).FirstOrDefault(f => !f.CanEvaluate);
            if (unevaluable != null)
                return new Entry(databaseObject, Standing.Unknown,
                    "The .tt has a filter this dialog cannot evaluate, so it cannot tell whether this is generated: " + unevaluable.Text);

            if (databaseObject.Schema.IndexOf('.') >= 0)
                return new Entry(databaseObject, Standing.LockedOut, "Schemas containing a period are always excluded, because Entity Framework cannot map them.");

            if (string.Equals(databaseObject.Schema, "MultiContext", StringComparison.OrdinalIgnoreCase))
                return new Entry(databaseObject, Standing.LockedOut, "The MultiContext schema is reserved by the generator.");

            if (list == FilterList.StoredProcedure && databaseObject.Name.IndexOf('.') >= 0)
                return new Entry(databaseObject, Standing.LockedOut, "Names containing a period are always excluded, because Entity Framework cannot map them.");

            var schemaExclude = schemas.FirstOrDefault(f => !f.IsInclude && f.Matches(databaseObject.Schema));
            if (schemaExclude != null)
                return new Entry(databaseObject, Standing.LockedOut, "Its schema is excluded by a filter in the .tt: " + schemaExclude.Text);

            var schemaIncludes = schemas.Where(f => f.IsInclude).ToList();
            if (schemaIncludes.Count > 0 && !schemaIncludes.Any(f => f.Matches(databaseObject.Schema)))
                return new Entry(databaseObject, Standing.LockedOut, "Its schema is not matched by the include filter in the .tt: " + schemaIncludes[0].Text);

            var exclude = filters.FirstOrDefault(f => !f.IsInclude && f.Matches(databaseObject.Name));
            if (exclude != null)
                return new Entry(databaseObject, Standing.LockedOut, "Excluded by a filter in the .tt: " + exclude.Text);

            var userInclude = filters.FirstOrDefault(f => f.IsInclude && !f.IsPickerOwned && f.Matches(databaseObject.Name));
            if (userInclude != null)
                return new Entry(databaseObject, Standing.LockedIn, "Included by a filter in the .tt: " + userInclude.Text);

            // Free. Ticked if the generator currently produces it: the category is on, and either nobody has
            // narrowed the list or the picker's own line names it.
            var includes       = filters.Where(f => f.IsInclude).ToList();
            var pickerIncludes = includes.Where(f => f.IsPickerOwned).ToList();
            var ticked         = FlagOn(document, databaseObject.Kind)
                                 && (includes.Count == 0 || pickerIncludes.Any(f => f.Matches(databaseObject.Name)));

            return new Entry(databaseObject, Standing.Free, string.Empty) { Ticked = ticked };
        }

        private enum Standing
        {
            Free,
            LockedIn,
            LockedOut,
            Unknown
        }

        private sealed class Entry
        {
            public Entry(DatabaseObject databaseObject, Standing standing, string reason)
            {
                Object   = databaseObject;
                Standing = standing;
                Reason   = reason;
            }

            public DatabaseObject Object { get; }
            public Standing Standing { get; }
            public string Reason { get; }
            public bool Ticked { get; set; }
        }
    }
}
