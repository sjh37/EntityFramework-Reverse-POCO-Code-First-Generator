using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework_Reverse_POCO_Generator;
using Microsoft.EntityFrameworkCore;

namespace Tester.BusinessLogic
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IMyDbContext _context;

        public CustomersRepository(IMyDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Customer> GetTop10()
        {
            return _context.Customers.OrderBy(x => x.CustomerId).Take(10);
        }

        public int Count()
        {
            return _context.Customers.Count();
        }

        public Customer FindById(string id)
        {
            return _context.Customers.FirstOrDefault(x => x.CustomerId == id);
        }

        public Customer Find(string id)
        {
            return _context.Customers.Find(id);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }
}