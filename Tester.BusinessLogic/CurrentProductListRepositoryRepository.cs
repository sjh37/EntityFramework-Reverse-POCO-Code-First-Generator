using System;
using System.Linq;
using EntityFramework_Reverse_POCO_Generator;

namespace Tester.BusinessLogic
{
    public class CurrentProductListRepositoryRepository : ICurrentProductListRepository
    {
        private readonly IMyDbContext _context;

        public CurrentProductListRepositoryRepository(IMyDbContext context)
        {
            if(context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        public int GetCount()
        {
            return All().Count();
        }

        public CurrentProductList GetProductByID(int id)
        {
            return _context.CurrentProductLists.FirstOrDefault(x => x.ProductId == id);
        }

        public IQueryable<CurrentProductList> All()
        {
            return _context.CurrentProductLists;
        }

        public bool UpdateProductName(int id, string newName)
        {
            var row = GetProductByID(id);
            if (row == null)
                return false;

            row.ProductName = newName;
            _context.SaveChanges();
            return true;
        }
    }
}