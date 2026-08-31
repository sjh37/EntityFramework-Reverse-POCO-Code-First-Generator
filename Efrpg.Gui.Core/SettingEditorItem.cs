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

        internal SettingEditorItem(SettingDefinition definition, SettingAssignment assignment)
        {
            Definition = definition;
            Assignment = assignment;
        }

        public SettingDefinition Definition { get; }

        /// <summary>Where it sits in this template, or null when the template does not mention it.</summary>
        public SettingAssignment Assignment { get; }

        public string Name => Definition.Name;

        public string Section => Definition.Section;

        public string Help => Definition.Help;

        public SettingKind Kind => Definition.Kind;

        /// <summary>The value as written in the file, or the generator's own default when the file is silent.</summary>
        public string CurrentValueText =>
            _newValueText ?? (Assignment != null ? Assignment.ValueText.Trim() : Definition.DefaultValue);

        /// <summary>True once a new value has been set that differs from what the file says.</summary>
        public bool IsChanged =>
            _newValueText != null && Assignment != null && _newValueText != Assignment.ValueText;

        public bool IsEditable => ReadOnlyReason == null;

        /// <summary>Why this row is not editable, or null when it is. Shown next to the value.</summary>
        public string ReadOnlyReason
        {
            get
            {
                var byType = Definition.ReadOnlyReason;
                if (byType != null)
                    return byType;

                if (Assignment == null)
                    return "Not set in this template - add it to the .tt to override the default.";

                if (Assignment.IsCommentedOut)
                    return "Commented out in the template. Remove the // to use it.";

                if (Assignment.SpansMultipleLines)
                    return "Written across several lines - edit it in the editor.";

                return CanParse() ? null : "Set to an expression rather than a plain value - edit it in the editor.";
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
            SettingValue.TryReadText(Assignment != null ? Assignment.ValueText : null, out existing, out verbatim);

            Set(SettingValue.WriteText(value, verbatim));
        }

        public void SetNumber(int value)
        {
            Set(SettingValue.WriteNumber(value));
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
            _newValueText = null;
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
