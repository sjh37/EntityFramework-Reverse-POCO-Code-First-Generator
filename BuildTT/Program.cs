namespace BuildTT
{
    public class Program
    {
        static void Main()
        {
            const string generatorRoot = "..\\..\\..\\Generator";
            const string ttRoot        = "..\\..\\..\\EntityFramework.Reverse.POCO.Generator";

            BuildTT.Create(generatorRoot, ttRoot);
        }
    }
}
