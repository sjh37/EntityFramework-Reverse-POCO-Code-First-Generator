using System.Collections.Generic;

namespace BuildTT.Application
{
    public interface IFileReader
    {
        List<string> Usings();
        List<string> Code();
        string Namespace();
        bool ReadFile(string inputSource);
    }
}