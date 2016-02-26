using System.Collections.Generic;
using System.Linq;

namespace Tester.UnitTest
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