using System.Collections.Generic;
using System.Linq;

namespace BuildTT.Application
{
    public class TTWriterStrategy : BaseWriterStrategy, IWriterStrategy
    {
        public List<string> Usings(List<string> usings)
        {
            return usings
                .OrderBy(x => x)
                .Select(u => $"<#@ import namespace=\"{u}\" #>")
                .ToList();
        }
    }
}