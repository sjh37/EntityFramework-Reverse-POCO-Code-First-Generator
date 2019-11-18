using System.Collections.Generic;
using System.Linq;

namespace BuildTT.Application
{
    public abstract class BaseWriterStrategy
    {
        public List<string> Code(List<string> code)
        {
            const string efrpgSearch = "Efrpg.";
            return code
                .Select(u => u.Replace(efrpgSearch, string.Empty))
                .ToList();
        }
    }
}