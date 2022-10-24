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
            const string templatesRoot = "..\\..\\..\\_File based templates";
            
            var version = File.ReadAllText("version.txt").Trim();

            TemplateFiles.Create(templatesRoot);
            BuildTT.Create(generatorRoot, ttRoot, version);
            //var vs = new VersionSetter("..\\..\\..\\", version);
            //vs.SetVersions();

            Console.WriteLine("Version: " + version);
        }
    }
}
