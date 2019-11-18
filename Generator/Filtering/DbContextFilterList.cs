using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Efrpg.Readers;
using Efrpg.Util;

namespace Efrpg.Filtering
{
    public class DbContextFilterList : IDbContextFilterList
    {
        private List<MultiContextSettings> _multiContextSettings;
        private Dictionary<string, IDbContextFilter> _filters;  // Key = database context name, which also becomes the sub-namespace used to encapsulate the many db contexts

        public bool ReadDbContextSettings(DatabaseReader reader, string singleDbContextSubNamespace = "")
        {
            _filters = new Dictionary<string, IDbContextFilter>();

            if (Settings.GenerateSingleDbContext)
            {
                // No need to read the database for settings, as they are provided by the user customisable class SingleContextFilter
                var filter = new SingleContextFilter { SubNamespace = singleDbContextSubNamespace };
                _filters.Add(string.IsNullOrWhiteSpace(singleDbContextSubNamespace) ? string.Empty : singleDbContextSubNamespace, filter);

                return true;
            }

            // Read in multi database context settings
            if (!string.IsNullOrWhiteSpace(Settings.MultiContextSettingsPlugin))
            {
                // Use plugin
                // We know the plugin implements (IMultiContextSettingsPlugin) interface but we can't direct cast it so we copy the object
                var plugin = AssemblyHelper.LoadPlugin(Settings.MultiContextSettingsPlugin);

                var pluginType = plugin.GetType();
                // remoteSettingsListObject is a List<MultiContextSettings> object
                var remoteSettingsListObject = (IList) pluginType.InvokeMember("ReadSettings", System.Reflection.BindingFlags.InvokeMethod, null, plugin, null);

                _multiContextSettings = new List<MultiContextSettings>();
                var remoteSettingsList = remoteSettingsListObject.Cast<object>();

                foreach (var remoteSettings in remoteSettingsList)
                {
                    var multiContextSettings = new MultiContextSettings();
                    MultiContextSettingsCopy.Copy(remoteSettings, multiContextSettings);
                    _multiContextSettings.Add(multiContextSettings);
                }
            }
            else
            {
                if (reader == null)
                    return false;

                // Read from database
                _multiContextSettings = reader.ReadMultiContextSettings();
            }

            if (_multiContextSettings == null || _multiContextSettings.Count == 0)
                return false;

            foreach (var setting in _multiContextSettings)
            {
                var filter = new MultiContextFilter(setting);
                if(!string.IsNullOrWhiteSpace(setting.Filename) && !_filters.ContainsKey(setting.Filename))
                    _filters.Add(setting.Filename, filter);
                else
                    _filters.Add(setting.Name, filter);
            }

            return true;
        }

        public Dictionary<string, IDbContextFilter> GetFilters()
        {
            return _filters;
        }

        public List<MultiContextSettings> GetMultiContextSettings()
        {
            return _multiContextSettings;
        }

        public bool IncludeViews()
        {
            return _filters.Any(x => x.Value.IncludeViews);
        }

        public bool IncludeSynonyms()
        {
            return _filters.Any(x => x.Value.IncludeSynonyms);
        }

        public bool IncludeStoredProcedures()
        {
            return _filters.Any(x => x.Value.IncludeStoredProcedures);
        }

        public bool IncludeTableValuedFunctions()
        {
            return _filters.Any(x => x.Value.IncludeTableValuedFunctions);
        }

        public bool IncludeScalarValuedFunctions()
        {
            return _filters.Any(x => x.Value.IncludeScalarValuedFunctions);
        }
    }
}