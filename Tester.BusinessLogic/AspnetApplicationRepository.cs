using System;
using System.Linq;
using EntityFramework_Reverse_POCO_Generator;

namespace Tester.BusinessLogic
{
    public class AspnetApplicationRepository : IAspnetApplicationRepository
    {
        private readonly IMyDbContext _context;

        public AspnetApplicationRepository(IMyDbContext context)
        {
            if(context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        public int GetCount()
        {
            return All().Count();
        }

        public AspnetApplications GetApplication(string name)
        {
            return _context.AspnetApplications.FirstOrDefault(x => x.ApplicationName == name);
        }

        public IQueryable<AspnetApplications> All()
        {
            return _context.AspnetApplications;
        }

        public bool UpdateApplicationName(string oldName, string newName)
        {
            var application = GetApplication(oldName);
            if (application == null)
                return false;

            application.ApplicationName = newName;
            _context.SaveChanges();
            return true;
        }
    }
}