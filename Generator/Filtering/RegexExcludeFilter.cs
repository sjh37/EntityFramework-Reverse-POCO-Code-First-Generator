using System.Text.RegularExpressions;

namespace Efrpg.Filtering
{
    /// <summary>
    /// Items matching the regex are excluded.
    /// </summary>
    public class RegexExcludeFilter : IFilterType<EntityName>
    {
        private readonly Regex _filter;

        /// <summary>
        /// A standard Regex will be created for the exclude filter.
        /// </summary>
        public RegexExcludeFilter(string filterExclude)
        {
            _filter = new Regex(filterExclude);
        }

        /// <summary>
        /// Allow you to provide your own custom defined Regex
        /// </summary>
        public RegexExcludeFilter(Regex filter)
        {
            _filter = filter;
        }

        public bool IsExcluded(EntityName item)
        {
            return _filter.IsMatch(item.DbName);
        }
    }
}