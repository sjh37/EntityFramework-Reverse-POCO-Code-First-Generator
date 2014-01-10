using System;
using System.Linq;
using EntityFramework_Reverse_POCO_Generator;
using Tester.BusinessLogic;

namespace Tester
{
    public class Program
    {
        static void Main()
        {
            try
            {
                using(var db = new MyDbContext())
                {
                    Console.WriteLine("*** Using EF directly ***");
                    var data = db.Customers.Take(10);
                    foreach(var row in data)
                    {
                        Console.WriteLine(row.CompanyName);
                    }

                    Console.WriteLine();

                    Console.WriteLine("*** Using EF via a repository ***");
                    ICustomersRepository customersRepository = new CustomersRepository(db);
                    var repoData = customersRepository.GetTop10();
                    foreach(var row in repoData)
                    {
                        Console.WriteLine(row.CompanyName);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("**************************************************");
                Console.WriteLine("********************** Exception *****************");
                Console.WriteLine();
                Console.WriteLine(e.Message);
                Console.WriteLine();
                Console.WriteLine(e.InnerException);
            }
        }
    }
}
