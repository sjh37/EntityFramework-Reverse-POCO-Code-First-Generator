using System;
using System.IO;

namespace BuildTT
{
    public class Program
    {
        static void Main()
        {
            const string generatorRoot = "..\\..\\..\\Generator";
            const string ttRoot        = "..\\..\\..\\EntityFramework.Reverse.POCO.Generator";
            
            var version = File.ReadAllText("version.txt").Trim();

            BuildTT.Create(generatorRoot, ttRoot, version);
            //var vs = new VersionSetter("..\\..\\..\\", version);
            //vs.SetVersions();

            Console.WriteLine("Version: " + version);
        }
    }
}
