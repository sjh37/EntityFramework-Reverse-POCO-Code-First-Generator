using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tester.UnitTest
{
    public class MockRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private int _identityValue;
        private readonly List<TEntity> _list = new List<TEntity>();

        public void AddData(IEnumerable<TEntity> values)
        {
            _list.AddRange(values);
        }

        public void ClearData()
        {
            _list.Clear();
        }

        public virtual IQueryable<TEntity> All
        {
            get { return _list.AsQueryable(); }
        }

        public virtual void InsertOnSubmit(TEntity entity)
        {
            _list.Add(entity);

            // HACK: Non-robust implementation of autonumber fields, using negative values
            var idProperty = entity.GetType()
                                   .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                   .SingleOrDefault(p => System.String.Compare(p.Name, "id", System.StringComparison.OrdinalIgnoreCase) == 0);

            if (idProperty == null || idProperty.PropertyType != typeof (System.Int32))
                return;
            
            if((int)idProperty.GetValue(entity, null) == 0)
            {
                idProperty.SetValue(entity, --_identityValue, BindingFlags.Default, null, null, null);
            }
        }

        public virtual void DeleteAllOnSubmit(IEnumerable<TEntity> entities)
        {
            foreach(var entity in entities)
            {
                _list.Remove(entity);
            }
        }

        public virtual void DeleteOnSubmit(TEntity entity)
        {
            _list.Remove(entity);
        }

        public virtual void SubmitChanges()
        {
            // Do nothing
        }
    }
}