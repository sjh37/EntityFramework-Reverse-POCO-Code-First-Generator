namespace BuildTT
{
    public class Program
    {
        static void Main()
        {
            const string generatorRoot = "..\\..\\..\\..\\Generator\\Generator";
            const string ttRoot = "..\\..\\..\\..\\Generator\\EntityFramework.Reverse.POCO.Generator";

            BuildTT.Create(generatorRoot, ttRoot);
        }
    }
}
