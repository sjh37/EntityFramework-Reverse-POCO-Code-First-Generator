using System;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Tester.UnitTest
{
    // This class should exactly match the code in EF.Reverse.POCO.Core.ttinclude
    public static class Inflector
    {
        public static IPluralizationService PluralizationService = null;

        /// <summary>
        /// Makes the plural.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public static string MakePlural(string word)
        {
            try
            {
                if (string.IsNullOrEmpty(word))
                    return string.Empty;
                if (PluralizationService == null)
                    return word;

                if (word.Contains('_')) return MakePluralHelper(word, '_');
                if (word.Contains(' ')) return MakePluralHelper(word, ' ');
                if (word.Contains('-')) return MakePluralHelper(word, '-');

                return PluralizationService.Pluralize(word);
            }
            catch (Exception)
            {
                return word;
            }
        }

        private static string MakePluralHelper(string word, char split)
        {
            if (string.IsNullOrEmpty(word))
                return string.Empty;
            var parts = word.Split(split);
            parts[parts.Length - 1] = PluralizationService.Pluralize(parts[parts.Length - 1]); // Pluralize just the last word
            return string.Join(split.ToString(), parts);
        }

        /// <summary>
        /// Makes the singular.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public static string MakeSingular(string word)
        {
            try
            {
                if (string.IsNullOrEmpty(word))
                    return string.Empty;

                if (PluralizationService == null)
                    return word;

                if (word.Contains('_')) return MakeSingularHelper(word, '_');
                if (word.Contains(' ')) return MakeSingularHelper(word, ' ');
                if (word.Contains('-')) return MakeSingularHelper(word, '-');

                return PluralizationService.Singularize(word);
            }
            catch (Exception)
            {
                return word;
            }
        }

        private static string MakeSingularHelper(string word, char split)
        {
            if (string.IsNullOrEmpty(word))
                return string.Empty;
            var parts = word.Split(split);
            parts[parts.Length - 1] = PluralizationService.Singularize(parts[parts.Length - 1]); // Pluralize just the last word
            return string.Join(split.ToString(), parts);
        }

        /// <summary>
        /// Converts the string to title case.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public static string ToTitleCase(string word)
        {
            if (string.IsNullOrEmpty(word))
                return string.Empty;

            var s = Regex.Replace(ToHumanCase(AddUnderscores(word)), @"\b([a-z])", match => match.Captures[0].Value.ToUpperInvariant());
            var digit = false;
            var sb = new StringBuilder();
            foreach (var c in s)
            {
                if (char.IsDigit(c))
                {
                    digit = true;
                    sb.Append(c);
                }
                else
                {
                    if (digit && char.IsLower(c))
                        sb.Append(char.ToUpperInvariant(c));
                    else
                        sb.Append(c);
                    digit = false;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts the string to human case.
        /// </summary>
        /// <param name="lowercaseAndUnderscoredWord">The lowercase and underscored word.</param>
        /// <returns></returns>
        public static string ToHumanCase(string lowercaseAndUnderscoredWord)
        {
            if (string.IsNullOrEmpty(lowercaseAndUnderscoredWord))
                return string.Empty;
            return MakeInitialCaps(Regex.Replace(lowercaseAndUnderscoredWord, @"_", " "));
        }


        /// <summary>
        /// Adds the underscores.
        /// </summary>
        /// <param name="pascalCasedWord">The pascal cased word.</param>
        /// <returns></returns>
        public static string AddUnderscores(string pascalCasedWord)
        {
            if (string.IsNullOrEmpty(pascalCasedWord))
                return string.Empty;
            return Regex.Replace(Regex.Replace(Regex.Replace(pascalCasedWord, @"([A-Z]+)([A-Z][a-z])", "$1_$2"), @"([a-z\d])([A-Z])", "$1_$2"), @"[-\s]", "_").ToLowerInvariant();
        }

        /// <summary>
        /// Makes the initial caps.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public static string MakeInitialCaps(string word)
        {
            if (string.IsNullOrEmpty(word))
                return string.Empty;
            return string.Concat(word.Substring(0, 1).ToUpperInvariant(), word.Substring(1).ToLowerInvariant());
        }

        /// <summary>
        /// Makes the initial character lowercase.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public static string MakeInitialLower(string word)
        {
            if (string.IsNullOrEmpty(word))
                return string.Empty;
            return string.Concat(word.Substring(0, 1).ToLowerInvariant(), word.Substring(1));
        }

        public static string MakeLowerIfAllCaps(string word)
        {
            if (string.IsNullOrEmpty(word))
                return string.Empty;
            return IsAllCaps(word) ? word.ToLowerInvariant() : word;
        }

        public static bool IsAllCaps(string word)
        {
            if (string.IsNullOrEmpty(word))
                return false;
            return word.All(char.IsUpper);
        }
    }
}