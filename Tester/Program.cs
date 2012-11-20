using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework_Reverse_POCO_Generator;

namespace Tester
{
    class Program
    {
        static void Main()
        {
            try
            {
                using(var db = new MyDbContext())
                {
                    var data = db.AspnetApplications.Take(5);
                    foreach(var row in data)
                    {
                        Console.WriteLine(row.ApplicationName);
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
