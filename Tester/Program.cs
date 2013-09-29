using System;
using System.Linq;
using EntityFramework_Reverse_POCO_Generator;
using Tester.BusinessLogic;
using Tester.Repository;

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
                    Console.WriteLine("*** Using EF directly ***");
                    var data = db.AspnetSchemaVersions.Take(10);
                    foreach(var row in data)
                    {
                        Console.WriteLine(row.Feature);
                    }

                    Console.WriteLine();

                    Console.WriteLine("*** Using EF via a repository ***");
                    IAspnetSchemaVersionsRepository aspnetSchemaVersionsRepository = new AspnetSchemaVersionsRepository(db);
                    var moqData = aspnetSchemaVersionsRepository.GetTop10();
                    foreach(var row in moqData)
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
