namespace Efrpg.Filtering
{
    public interface IFilter
    {
    }
    public interface IFilterType<in T> : IFilter
        //where T : HasName
    {
        bool IsExcluded(T item);
    }
}