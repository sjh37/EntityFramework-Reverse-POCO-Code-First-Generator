using System;
using System.Linq;
using EntityFramework_Reverse_POCO_Generator;

namespace Tester.UnitTests
{
    class Program
    {
        static void Main()
        {
            try
            {
                using(var db = new MyDbContext())
                {
                    var data = db.AspnetSchemaVersions.Take(10);
                    foreach(var row in data)
                    {
                        Console.WriteLine(row.Feature);
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
