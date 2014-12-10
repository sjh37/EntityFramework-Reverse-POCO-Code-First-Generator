using System.Linq;
using System.Collections.Generic;

namespace Tester.Repository
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