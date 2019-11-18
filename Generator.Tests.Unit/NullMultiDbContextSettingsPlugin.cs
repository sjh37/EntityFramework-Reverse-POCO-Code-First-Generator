using System.Collections.Generic;
using Efrpg.Filtering;
using Efrpg.Readers;

namespace Generator.Tests.Unit
{
    public class NullMultiContextSettingsPlugin : IMultiContextSettingsPlugin
    {
        public List<MultiContextSettings> ReadSettings()
        {
            return null;
        }
    }
}