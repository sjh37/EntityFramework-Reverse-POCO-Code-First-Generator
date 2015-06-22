using System;
using System.Linq;
using EntityFramework_Reverse_POCO_Generator;

namespace Tester.BusinessLogic
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IMyDbContext _context;

        public CustomersRepository(IMyDbContext context)
        {
            if(context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        public IQueryable<Customer> GetTop10()
        {
            return _context.Customers.Take(10).OrderBy(x => x.CustomerId);
        }

        public int Count()
        {
            return _context.Customers.Count();
        }

        public Customer FindById(string id)
        {
            return _context.Customers.FirstOrDefault(x => x.CustomerId == id);
        }
    }
}