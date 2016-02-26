using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework_Reverse_POCO_Generator;

namespace Tester.BusinessLogic
{
    public interface ICustomersRepository
    {
        IQueryable<Customer> GetTop10();
        Task<List<Customer>> GetTop10Async();
        int Count();
        Customer FindById(string id);
        Customer Find(string id);
        void AddCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
    }
}