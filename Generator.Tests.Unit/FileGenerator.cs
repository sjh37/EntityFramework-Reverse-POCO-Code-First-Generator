using Efrpg;

namespace Generator.Tests.Unit
{
    internal static class FileGenerator
    {
        internal static string GetFileOnSettings(string database)
        {
            return
                $"{database}_{Settings.DatabaseType}_{Settings.TemplateType}_Fk{Settings.ForeignKeyNamingStrategy}.cs";
        }
    }
}