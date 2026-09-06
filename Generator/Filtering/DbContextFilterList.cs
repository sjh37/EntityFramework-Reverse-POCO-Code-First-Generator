using System.Collections.Generic;
using System.Linq;
using Efrpg.Readers;

namespace Efrpg.Filtering
{
    public class DbContextFilterList : IDbContextFilterList
    {
        private Dictionary<string, IDbContextFilter> _filters;  // Key = database context name, which also becomes the sub-namespace used to encapsulate the classes

        public bool ReadDbContextSettings(EfrpgResult result, string singleDbContextSubNamespace = "")
        {
            _filters = new Dictionary<string, IDbContextFilter>();

            // No need to read the database for settings, as they are provided by the user customisable class SingleContextFilter
            var filter = new SingleContextFilter { SubNamespace = singleDbContextSubNamespace };
            _filters.Add(string.IsNullOrWhiteSpace(singleDbContextSubNamespace) ? string.Empty : singleDbContextSubNamespace, filter);

            return true;
        }

        public Dictionary<string, IDbContextFilter> GetFilters()
        {
            return _filters;
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
