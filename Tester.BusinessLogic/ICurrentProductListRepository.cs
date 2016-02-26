using System.Linq;
using EntityFramework_Reverse_POCO_Generator;

namespace Tester.BusinessLogic
{
    public interface ICurrentProductListRepository
    {
        int GetCount();
        CurrentProductList GetProductByID(int id);
        IQueryable<CurrentProductList> All();
        bool UpdateProductName(int id, string newName);
    }
}