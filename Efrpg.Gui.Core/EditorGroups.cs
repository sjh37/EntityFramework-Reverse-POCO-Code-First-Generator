using System;
using System.Collections.Generic;

namespace Efrpg.Gui
{
    /// <summary>
    ///     The group the settings editor shows a setting under, when that is not the heading Database.tt keeps it
    ///     beneath. The template's layout is the generator's business and is left alone; a banner over one or two
    ///     lines reads fine in a file but makes a page of one row in the editor.
    /// </summary>
    /// <remarks>
    ///     This is display only. Where a setting is written into a template still follows
    ///     <see cref="SettingDefinition.Section"/>, so a line added from the editor lands under the same banner the
    ///     shipped template would have put it under.
    /// </remarks>
    public static class EditorGroups
    {
        private static readonly Dictionary<string, string> Overrides = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            { "PrependSchemaName",                           "Other settings" },
            { "AdditionalReverseNavigationsDataAnnotations", "Other settings" },
            { "AdditionalForeignKeysDataAnnotations",        "Other settings" }
        };

        public static string For(SettingDefinition definition)
        {
            if (definition == null)
                throw new ArgumentNullException(nameof(definition));

            string group;
            return Overrides.TryGetValue(definition.Name, out group) ? group : definition.Section;
        }
    }
}
