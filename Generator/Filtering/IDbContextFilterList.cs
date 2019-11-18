using System.Collections.Generic;
using Efrpg.Readers;

namespace Efrpg.Filtering
{
    public interface IDbContextFilterList
    {
        bool ReadDbContextSettings(DatabaseReader reader, string singleDbContextSubNamespace = null);
        Dictionary<string, IDbContextFilter> GetFilters();
        List<MultiContextSettings> GetMultiContextSettings();
        bool IncludeViews();
        bool IncludeSynonyms();
        bool IncludeStoredProcedures();
        bool IncludeTableValuedFunctions();
        bool IncludeScalarValuedFunctions();
    }
}