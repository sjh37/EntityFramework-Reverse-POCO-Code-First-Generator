using System.Collections.Generic;
using System.Linq;
using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    /// <summary>
    ///     Every editable setting the shipped Database.tt assigns, changed through the editor one at a time: exactly
    ///     that line changes, and reloading the result reads the new value back. The other fixtures cover each kind
    ///     of value once; this covers each line, so a setting whose line is shaped in a way the reader accepts but
    ///     the writer gets wrong cannot hide behind a representative that happens to be fine.
    /// </summary>
    [TestFixture]
    public class EveryEditableSettingRoundTripTests
    {
        private static SettingsCatalogue V4 => SettingsCatalogue.Load(RepositoryFiles.SettingsMetadata("v4"));

        private static SettingsEditSession Shipped()
        {
            return SettingsEditSession.Load(RepositoryFiles.DatabaseTemplate(), V4);
        }

        /// <summary>Every setting the shipped template assigns, commented out or not, that the editor will change.</summary>
        public static IEnumerable<string> EditableSettings()
        {
            return Shipped().Items
                .Where(i => i.IsEditable && i.Assignment != null)
                .Select(i => i.Name)
                .OrderBy(n => n)
                .ToList();
        }

        [Test]
        public void TheShippedTemplateHasPlentyToWalk()
        {
            Assert.That(EditableSettings().Count(), Is.GreaterThan(60));
        }

        [TestCaseSource(nameof(EditableSettings))]
        public void ChangingIt_RewritesOnlyItsOwnLine_AndReadsBack(string name)
        {
            var session  = Shipped();
            var original = session.Document.Text;
            var item     = session.Find(name);
            var expected = ChangeToSomethingElse(item);

            var text = session.Apply();

            var before = original.Split('\n');
            var after  = text.Split('\n');
            Assert.That(after.Length, Is.EqualTo(before.Length), "the line count must not change");

            var changed = Enumerable.Range(0, before.Length).Where(i => before[i] != after[i]).ToList();
            Assert.That(changed.Count, Is.EqualTo(1), "exactly one line may differ");
            Assert.That(after[changed[0]], Does.Contain("Settings." + name));
            Assert.That(after[changed[0]].TrimStart(), Does.Not.StartWith("//"), "the assignment is live afterwards");

            var reloaded = SettingsEditSession.Load(text, V4).Find(name);
            Assert.That(reloaded.IsEditable, Is.True, "the rewritten line must still be one the editor can read");
            Assert.That(Describe(reloaded), Is.EqualTo(expected));
        }

        /// <summary>Sets a value of the item's own kind that differs from what it holds, and returns how it should read back.</summary>
        private static string ChangeToSomethingElse(SettingEditorItem item)
        {
            switch (item.Kind)
            {
                case SettingKind.Boolean:
                    item.SetBoolean(!item.BooleanValue);
                    break;

                case SettingKind.Text:
                    item.SetText(item.TextValue + "Changed");
                    break;

                case SettingKind.Number:
                    item.SetNumber(item.NumberValue + 1);
                    break;

                case SettingKind.Character:
                    item.SetCharacter(item.CharacterValue == "~" ? "#" : "~");
                    break;

                case SettingKind.Enumeration:
                    item.SetMembers(DifferentMembers(item));
                    break;

                default:
                    Assert.Fail("Settings." + item.Name + " is editable but has no editor for " + item.Kind);
                    break;
            }

            return Describe(item);
        }

        private static IReadOnlyList<string> DifferentMembers(SettingEditorItem item)
        {
            var current = item.SelectedMembers;
            var candidates = item.Definition.EnumMembers
                .Where(m => m.Value != 0 || !item.Definition.IsFlags)
                .Select(m => m.Name)
                .ToList();

            if (!item.Definition.IsFlags)
                return new[] { candidates.First(m => !current.Contains(m)) };

            // Flags: drop a selected member, or add an unselected one when there is nothing to drop
            var unselected = candidates.FirstOrDefault(m => !current.Contains(m));
            return unselected != null
                ? current.Concat(new[] { unselected }).ToList()
                : current.Skip(1).ToList();
        }

        private static string Describe(SettingEditorItem item)
        {
            switch (item.Kind)
            {
                case SettingKind.Boolean:     return item.BooleanValue.ToString();
                case SettingKind.Text:        return item.TextValue;
                case SettingKind.Number:      return item.NumberValue.ToString();
                case SettingKind.Character:   return item.CharacterValue;
                case SettingKind.Enumeration: return string.Join("|", item.SelectedMembers.OrderBy(m => m));
                default:                      return item.Kind.ToString();
            }
        }
    }
}
