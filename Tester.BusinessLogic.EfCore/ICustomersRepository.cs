using System.Linq;
using EntityFramework_Reverse_POCO_Generator;

namespace Tester.BusinessLogic
{
    public interface ICustomersRepository
    {
        IQueryable<Customer> GetTop10();
        int Count();
        Customer FindById(string id);
        Customer Find(string id);
        void AddCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
    }
}