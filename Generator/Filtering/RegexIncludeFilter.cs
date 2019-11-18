using System.Text.RegularExpressions;

namespace Efrpg.Filtering
{
    /// <summary>
    /// Items matching the regex are included.
    /// </summary>
    public class RegexIncludeFilter : IFilterType<EntityName>
    {
        private readonly Regex _filter;

        /// <summary>
        /// A standard Regex will be created for the include filter.
        /// </summary>
        public RegexIncludeFilter(string filterInclude)
        {
            _filter = new Regex(filterInclude);
        }

        /// <summary>
        /// Allow you to provide your own custom defined Regex
        /// </summary>
        public RegexIncludeFilter(Regex filter)
        {
            _filter = filter;
        }

        public bool IsExcluded(EntityName item)
        {
            return !_filter.IsMatch(item.DbName);
        }
    }
}