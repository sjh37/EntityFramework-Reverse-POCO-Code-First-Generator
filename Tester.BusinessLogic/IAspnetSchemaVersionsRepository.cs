using System.Linq;
using EntityFramework_Reverse_POCO_Generator;

namespace Tester.BusinessLogic
{
    public interface IAspnetSchemaVersionsRepository
    {
        IQueryable<AspnetSchemaVersions> GetTop10();
    }
}