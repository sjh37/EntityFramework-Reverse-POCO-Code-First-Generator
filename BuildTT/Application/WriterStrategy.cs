using System.Collections.Generic;
using System.Linq;

namespace BuildTT.Application
{
    public class WriterStrategy : BaseWriterStrategy, IWriterStrategy
    {
        public List<string> Usings(List<string> usings)
        {
            return usings
                .OrderBy(x => x)
                .Select(u => $"using {u};")
                .ToList();
        }
    }
}