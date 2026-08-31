using System;
using System.IO;
using System.Reflection;
using Efrpg.Gui;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     Finds the settings metadata that ships beside this assembly, and picks the one matching a template.
    /// </summary>
    /// <remarks>
    ///     Both files are in the VSIX: v3 and v4 declare different settings, and almost the whole installed base is
    ///     still on v3. Loading the wrong one would show a user settings their template cannot use and hide ones it
    ///     can, so the version is taken from the include directive on line 1 rather than assumed to be current.
    /// </remarks>
    internal static class SettingsMetadataFiles
    {
        /// <summary>
        ///     Loads the catalogue matching this template, or null when the file is missing from the installation.
        /// </summary>
        public static SettingsCatalogue For(string templateText, out string error)
        {
            error = null;

            var version = TemplateUpgrade.IsV3(templateText) ? "v3" : "v4";
            var path    = Path.Combine(Folder(), "settings-metadata." + version + ".json");

            if (!File.Exists(path))
            {
                error = "The settings metadata for " + version + " is missing from the extension: " + path;
                return null;
            }

            try
            {
                return SettingsCatalogue.Load(File.ReadAllText(path));
            }
            catch (Exception ex)
            {
                error = "The settings metadata could not be read: " + ex.Message;
                return null;
            }
        }

        private static string Folder()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        }
    }
}
