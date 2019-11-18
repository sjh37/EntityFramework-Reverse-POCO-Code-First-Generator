using System.Linq;

namespace Efrpg.Filtering
{
    public class PeriodFilter : IFilterType<EntityName>
    {
        public bool IsExcluded(EntityName item)
        {
            return item.DbName.Contains('.');
        }
    }
}