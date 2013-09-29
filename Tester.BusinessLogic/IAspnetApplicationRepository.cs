using System.Collections.Generic;
using System.Linq;
using EntityFramework_Reverse_POCO_Generator;

namespace Tester.BusinessLogic
{
    public interface IAspnetApplicationRepository
    {
        int GetCount();
        AspnetApplications GetApplication(string name);
        IQueryable<AspnetApplications> All();
        bool UpdateApplicationName(string oldName, string newName);
    }
}