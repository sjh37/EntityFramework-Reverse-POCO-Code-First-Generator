using System;
using System.Collections.Generic;
using System.Linq;

namespace Efrpg.Gui
{
    /// <summary>
    ///     One row in the settings editor: what the generator says the setting is, what this template currently
    ///     sets it to, and whether a form may change it.
    /// </summary>
    /// <remarks>
    ///     Three things have to line up before a setting is editable, and all three are decided here rather than in
    ///     the dialog so they can be tested: the generator must declare it as something a form can represent, the
    ///     template must actually assign it on a single active line, and the value in the file must parse as the
    ///     shape the metadata promises. The third is the one that catches real templates - a string setting holding
    ///     <c>DefaultNamespace</c>, or <c>Path.Combine(...)</c>, is code the user wrote and is left alone.
    /// </remarks>
    public sealed class SettingEditorItem
    {
        private string _newValueText;
        private bool? _pendingAssigned;

        internal SettingEditorItem(SettingDefinition definition, SettingAssignment assignment)
            : this(definition, assignment, null)
        {
        }

        internal SettingEditorItem(SettingDefinition definition, SettingAssignment assignment, string statementText)
        {
            Definition    = definition;
            Assignment    = assignment;
            StatementText = statementText;
        }

        /// <summary>
        ///     True for the settings that hold code rather than a value: callbacks, lists and objects built in code.
        ///     These are switched on and off as a whole rather than edited in a form.
        /// </summary>
        public bool IsCode =>
            Kind == SettingKind.Callback || Kind == SettingKind.Complex;

        /// <summary>The template assigns this and the assignment is live, once any pending switch is counted.</summary>
        public bool IsAssigned => _pendingAssigned ?? (Assignment != null && !Assignment.IsCommentedOut);

        /// <summary>
        ///     The statement as it stands in the template, marker and all when it is commented out; or, when the
        ///     template lacks it, the statement switching it on would write.
        /// </summary>
        public string Code
        {
            get
            {
                if (StatementText != null)
                    return StatementText;

                var body = Definition.DefaultValue;
                return body == null ? null : "Settings." + Name + " = " + body.Trim() + ";";
            }
        }

        /// <summary>The line the statement starts on in the template as loaded, or 0 when it is not there.</summary>
        public int LineNumber => Assignment != null ? Assignment.LineNumber : 0;

        /// <summary>The full statement text the session read for this item, or null when absent.</summary>
        internal string StatementText { get; }

        /// <summary>
        ///     Switches a code setting on or off. Off comments the statement out, so the generator's default runs
        ///     and the user's code stays in the file; on uncomments it, or writes the default body when the
        ///     template has nothing to uncomment.
        /// </summary>
        public void SetAssigned(bool assigned)
        {
            if (!IsCode)
                throw new InvalidOperationException("Settings." + Name + " is edited as a value, not switched on and off.");

            if (assigned && Assignment == null && Definition.DefaultValue == null)
                throw new InvalidOperationException("Settings." + Name + " has no default body to write.");

            var fileSays = Assignment != null && !Assignment.IsCommentedOut;
            _pendingAssigned = assigned == fileSays ? (bool?) null : assigned;
        }

        /// <summary>True when a switch is pending that the file does not already say.</summary>
        public bool IsAssignmentChanged => _pendingAssigned.HasValue;

        /// <summary>The state a pending switch will write, for the session; null when there is none.</summary>
        internal bool? PendingAssigned => _pendingAssigned;

        public SettingDefinition Definition { get; }

        /// <summary>Where it sits in this template, or null when the template does not mention it.</summary>
        public SettingAssignment Assignment { get; }

        public string Name => Definition.Name;

        public string Section => Definition.Section;

        public string Help => Definition.Help;

        public SettingKind Kind => Definition.Kind;

        /// <summary>The value as written in the file, or the generator's own default when the file is silent.</summary>
        public string CurrentValueText =>
            _newValueText ?? (Assignment != null ? Assignment.ValueText.Trim() : Definition.DefaultValue ?? string.Empty);

        /// <summary>The template has no line for this setting. Changing it adds one.</summary>
        public bool IsAbsent => Assignment == null;

        /// <summary>The template's line is commented out. Changing it switches the line on.</summary>
        public bool IsCommentedOut => Assignment != null && Assignment.IsCommentedOut;

        /// <summary>
        ///     True once a new value has been set that the file does not already say. For a setting the file lacks
        ///     or has commented out, any value at all is a change, because writing it is what the user asked for.
        /// </summary>
        public bool IsChanged =>
            _pendingAssigned.HasValue ||
            (_newValueText != null && (Assignment == null || Assignment.IsCommentedOut || _newValueText != Assignment.ValueText));

        public bool IsEditable => ReadOnlyReason == null;

        /// <summary>
        ///     What changing this row will do to the file when it is more than replacing a value, or null.
        /// </summary>
        public string Hint
        {
            get
            {
                if (!IsEditable)
                    return null;

                if (IsAbsent)
                    return "Not set in this template. Changing it adds the line beside its neighbours.";

                return IsCommentedOut ? "Commented out in the template. Changing it switches the line on." : null;
            }
        }

        /// <summary>Why this row is not editable, or null when it is. Shown next to the value.</summary>
        public string ReadOnlyReason
        {
            get
            {
                var byType = Definition.ReadOnlyReason;
                if (byType != null)
                    return byType;

                // Absent is fine: the generator's default is shown, and a change adds the line.
                if (Assignment == null)
                    return null;

                // A list of strings is read whole however many lines it takes; every other kind must fit one line.
                if (Assignment.SpansMultipleLines && Kind != SettingKind.StringList)
                    return (Assignment.IsCommentedOut ? "Commented out, and w" : "W") + "ritten across several lines - edit it in the editor.";

                if (CanParse())
                    return null;

                return (Assignment.IsCommentedOut ? "Commented out, and s" : "S") + "et to an expression rather than a plain value - edit it in the editor.";
            }
        }

        public bool BooleanValue
        {
            get
            {
                bool value;
                return SettingValue.TryReadBoolean(CurrentValueText, out value) && value;
            }
        }

        public string TextValue
        {
            get
            {
                string value;
                bool verbatim;
                return SettingValue.TryReadText(CurrentValueText, out value, out verbatim) ? value : string.Empty;
            }
        }

        public int NumberValue
        {
            get
            {
                int value;
                return SettingValue.TryReadNumber(CurrentValueText, out value) ? value : 0;
            }
        }

        public string CharacterValue
        {
            get
            {
                string value;
                return SettingValue.TryReadCharacter(CurrentValueText, out value) ? value : string.Empty;
            }
        }

        /// <summary>The enum members currently selected. One for a plain enum, one or more for a flags setting.</summary>
        public IReadOnlyList<string> SelectedMembers
        {
            get
            {
                IReadOnlyList<string> members;
                return SettingValue.TryReadEnum(CurrentValueText, Definition, out members)
                    ? members
                    : new string[0];
            }
        }

        public void SetBoolean(bool value)
        {
            Set(SettingValue.WriteBoolean(value));
        }

        public void SetText(string value)
        {
            string existing;
            bool verbatim;

            // Keeps whichever literal form the template already used, so the diff is the value and not the style.
            // A setting that ships as null, or that the file switches off with null, goes back to null when
            // cleared: for those, blank means off, not an empty suffix.
            var fileValue = Assignment != null ? Assignment.ValueText : null;
            SettingValue.TryReadText(fileValue, out existing, out verbatim);

            Set(string.IsNullOrEmpty(value) && (SettingValue.IsNull(fileValue) || SettingValue.IsNull(Definition.DefaultValue))
                ? SettingValue.Null
                : SettingValue.WriteText(value, verbatim));
        }

        public void SetNumber(int value)
        {
            Set(SettingValue.WriteNumber(value));
        }

        /// <summary>The items of a list setting, or empty when the file holds none or something unreadable.</summary>
        public IReadOnlyList<string> StringListValue
        {
            get
            {
                IReadOnlyList<string> items;
                StringListForm form;
                return SettingValue.TryReadStringList(CurrentValueText, out items, out form) ? items : new string[0];
            }
        }

        /// <summary>Replaces the items, keeping the list or array spelling the file used.</summary>
        public void SetStringList(IEnumerable<string> items)
        {
            IReadOnlyList<string> existing;
            StringListForm form;
            if (!SettingValue.TryReadStringList(Assignment != null ? Assignment.ValueText : Definition.DefaultValue, out existing, out form))
                form = StringListForm.List;

            var indent  = Assignment != null ? Assignment.Indent : "    ";
            var newLine = Assignment != null && Assignment.ValueText.Contains("\r\n") ? "\r\n" : Assignment != null && Assignment.ValueText.Contains("\n") ? "\n" : "\r\n";

            Set(SettingValue.WriteStringList(items, form, indent, newLine));
        }

        public void SetCharacter(string value)
        {
            Set(SettingValue.WriteCharacter(value));
        }

        public void SetMembers(IEnumerable<string> members)
        {
            var names = (members ?? new string[0]).ToList();

            // An empty flags selection is a legitimate choice and has to be expressible. Every flags enum in the
            // generator declares a zero member for it; without one there is nothing to write, so the change is
            // dropped rather than guessed at.
            if (names.Count == 0)
            {
                var none = Definition.EnumMembers.FirstOrDefault(m => m.Value == 0);
                if (none == null)
                    return;

                names.Add(none.Name);
            }

            Set(SettingValue.WriteEnum(Definition, names));
        }

        /// <summary>Puts the value back to whatever the file said when it was loaded.</summary>
        public void Revert()
        {
            _newValueText    = null;
            _pendingAssigned = null;
        }

        private void Set(string valueText)
        {
            if (!IsEditable)
                throw new InvalidOperationException("Settings." + Name + " is not editable: " + ReadOnlyReason);

            _newValueText = valueText;
        }

        /// <summary>The new right-hand side to write, or null when nothing changed.</summary>
        internal string PendingValueText => IsChanged ? _newValueText : null;

        private bool CanParse()
        {
            var text = Assignment.ValueText;

            switch (Definition.Kind)
            {
                case SettingKind.Boolean:
                {
                    bool value;
                    return SettingValue.TryReadBoolean(text, out value);
                }
                case SettingKind.Text:
                {
                    string value;
                    bool verbatim;
                    return SettingValue.TryReadText(text, out value, out verbatim);
                }
                case SettingKind.Number:
                {
                    int value;
                    return SettingValue.TryReadNumber(text, out value);
                }
                case SettingKind.Character:
                {
                    string value;
                    return SettingValue.TryReadCharacter(text, out value);
                }
                case SettingKind.Enumeration:
                {
                    IReadOnlyList<string> members;
                    return SettingValue.TryReadEnum(text, Definition, out members);
                }
                case SettingKind.StringList:
                {
                    IReadOnlyList<string> items;
                    StringListForm form;
                    return SettingValue.TryReadStringList(text, out items, out form);
                }
                default:
                    return false;
            }
        }

        public override string ToString()
        {
            return Name + " = " + CurrentValueText;
        }
    }
}
