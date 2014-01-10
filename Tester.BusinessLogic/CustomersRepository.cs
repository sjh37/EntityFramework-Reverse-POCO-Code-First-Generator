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

        public IQueryable<Customers> GetTop10()
        {
            return _context.Customers.Take(10);
        }
    }
}