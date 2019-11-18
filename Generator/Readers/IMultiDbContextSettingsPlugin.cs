using System.Collections.Generic;
using Efrpg.Filtering;

namespace Efrpg.Readers
{
    public interface IMultiContextSettingsPlugin
    {
        List<MultiContextSettings> ReadSettings();
    }
}