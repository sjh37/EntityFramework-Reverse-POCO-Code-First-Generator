using System;
using System.Linq;
using EntityFramework_Reverse_POCO_Generator;

namespace Tester.BusinessLogic
{
    public class AspnetSchemaVersionsRepository : IAspnetSchemaVersionsRepository
    {
        private readonly IMyDbContext _context;

        public AspnetSchemaVersionsRepository(IMyDbContext context)
        {
            if(context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        public IQueryable<AspnetSchemaVersions> GetTop10()
        {
            return _context.AspnetSchemaVersions.Take(10);
        }
    }
}