using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Efrpg.Util
{
    /*public static class StringExtensions
    {
        public static string ToLiteral(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            using (var writer = new StringWriter())
            {
                using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer,
                        new CodeGeneratorOptions
                        {
                            BlankLinesBetweenMembers     = false,
                            ElseOnClosing                = false,
                            IndentString                 = string.Empty
                        });
                    var result                           = writer.ToString();
                    if (result.StartsWith("\"") && result.EndsWith("\""))
                        return result.Substring(1, result.Length - 2);
                    return result;
                }
            }
        }
    }*/

    /*public static class ReplaceString
    {
        static readonly IDictionary<string, string> m_replaceDict = new Dictionary<string, string>();

        const string ms_regexEscapes = @"[\a\b\f\n\r\t\v\\""]";

        static ReplaceString()
        {
            m_replaceDict.Add("\a", @"\a");
            m_replaceDict.Add("\b", @"\b");
            m_replaceDict.Add("\f", @"\f");
            m_replaceDict.Add("\n", @"\n");
            m_replaceDict.Add("\r", @"\r");
            m_replaceDict.Add("\t", @"\t");
            m_replaceDict.Add("\v", @"\v");

            m_replaceDict.Add("\\", @"\\");
            m_replaceDict.Add("\0", @"\0");

            // The SO parser gets fooled by the verbatim version 
            // of the string to replace - @"\"""
            // so use the 'regular' version
            m_replaceDict.Add("\"", "\\\"");
        }

        public static string StringLiteral(this string input)
        {
            return Regex.Replace(input, ms_regexEscapes, Match);
        }

        public static string CharLiteral(this char c)
        {
            return c == '\'' ? @"'\''" : string.Format("'{0}'", c);
        }

        private static string Match(Match m)
        {
            var match = m.ToString();
            if (m_replaceDict.ContainsKey(match))
                return m_replaceDict[match];

            return match;
        }
    }*/
}
