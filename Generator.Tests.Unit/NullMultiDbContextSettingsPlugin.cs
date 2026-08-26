using System.Collections.Generic;
using Efrpg.Filtering;

namespace Generator.Tests.Unit
{
    public class NullMultiContextSettingsPlugin
    {
        public List<MultiContextSettings> ReadSettings()
        {
            return null;
        }
    }
}