namespace Efrpg.Filtering
{
    public interface IFilter
    {
    }

    public interface IFilterType<in T> : IFilter
    {
        bool IsExcluded(T item);
    }
}