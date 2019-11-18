using System;

namespace Efrpg.Filtering
{
    public class HasNameFilter : IFilterType<EntityName>
    {
        private readonly FilterType _filterType;

        public HasNameFilter(FilterType filterType)
        {
            _filterType = filterType;
        }

        public bool IsExcluded(EntityName item)
        {
            // Example: Exclude a schema with a name of 'audit'
            //if (_filterType == FilterType.Schema && item.Name.Equals("audit", StringComparison.InvariantCultureIgnoreCase))
            //    return false;

            // Example: Exclude any item with 'audit' anywhere in its name.
            //if (item.Name.ToLowerInvariant().Contains("audit"))
            //    return true;

            // Example: Exclude any table which starts with 'audit'
            //if (_filterType == FilterType.Table && item.Name.ToLowerInvariant().StartsWith("audit"))
            //    return true;

            // TODO: Add your code here

            return false;
        }
    }
}