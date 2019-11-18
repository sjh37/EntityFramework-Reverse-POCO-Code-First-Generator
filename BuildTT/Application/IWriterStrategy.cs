using System.Collections.Generic;

namespace BuildTT.Application
{
    public interface IWriterStrategy
    {
        List<string> Usings(List<string> usings);
        List<string> Code(List<string> code);
    }
}