using System.Collections.Generic;
using System.Linq;

namespace Generator.Tests.Unit.EF6
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> All { get; }
        void InsertOnSubmit(TEntity entity);
        void DeleteOnSubmit(TEntity entity);
        void DeleteAllOnSubmit(IEnumerable<TEntity> entities);
        void SubmitChanges();
    }
}