using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Efrpg
{
    /// <summary>
    ///     Works out the namespace Visual Studio would have handed the template, for hosts that do not supply one.
    ///     Visual Studio's namespace hint is the project's root namespace plus the template's folder path within the
    ///     project. Rider and TextTransform.exe supply nothing, so this derives the same answer from the nearest
    ///     .csproj above the template.
    /// </summary>
    public static class ProjectNamespace
    {
        private static readonly Regex RootNamespaceElement = new Regex(@"<RootNamespace(?:\s[^>]*)?>\s*([^<]*?)\s*</RootNamespace>", RegexOptions.IgnoreCase);

        /// <summary>
        ///     Resolves the namespace for a template in <paramref name="templateFolder" />, or returns
        ///     <paramref name="fallback" /> when no project file is found above it.
        /// </summary>
        public static string Resolve(string templateFolder, string fallback)
        {
            if (string.IsNullOrEmpty(templateFolder) || !Directory.Exists(templateFolder))
                return fallback;

            var folders = new List<string>();
            var directory = new DirectoryInfo(templateFolder);
            while (directory != null)
            {
                var project = directory.GetFiles("*.csproj").OrderBy(x => x.Name).FirstOrDefault();
                if (project != null)
                {
                    folders.Insert(0, RootNamespace(project.FullName) ?? ToIdentifier(Path.GetFileNameWithoutExtension(project.Name)));
                    return string.Join(".", folders.Where(x => x.Length > 0));
                }

                folders.Insert(0, ToIdentifier(directory.Name));
                directory = directory.Parent;
            }

            return fallback;
        }

        private static string RootNamespace(string projectFile)
        {
            var match = RootNamespaceElement.Match(File.ReadAllText(projectFile));
            if (!match.Success)
                return null;

            // An MSBuild expression cannot be evaluated here, so treat it as unset and use the project name instead
            var value = match.Groups[1].Value;
            return value.Length == 0 || value.Contains("$(") ? null : value;
        }

        /// <summary>
        ///     Mirrors what Visual Studio does to a folder name it puts in a namespace: invalid characters become
        ///     underscores and a leading digit gets one in front of it.
        /// </summary>
        private static string ToIdentifier(string name)
        {
            var sb = new StringBuilder(name.Length + 1);
            foreach (var c in name)
                sb.Append(char.IsLetterOrDigit(c) || c == '_' ? c : '_');

            if (sb.Length > 0 && char.IsDigit(sb[0]))
                sb.Insert(0, '_');

            return sb.ToString();
        }
    }
}
