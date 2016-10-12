using System;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Tester.UnitTest
{
    [TestFixture]
    public class CleanUpTests
    {
        // This class should exactly match the code in EF.Reverse.POCO.Core.ttinclude
        private static readonly Regex RxCleanUp = new Regex(@"[^\w\d\s_-]", RegexOptions.Compiled);

        private static readonly Func<string, string> CleanUp = (str) =>
        {
            // Replace punctuation and symbols in variable names as these are not allowed.
            int len = str.Length;
            if (len == 0)
                return str;
            var sb = new StringBuilder();
            bool replacedCharacter = false;
            for (int n = 0; n < len; ++n)
            {
                char c = str[n];
                if (c != '_' && c != '-' && (char.IsSymbol(c) || char.IsPunctuation(c)))
                {
                    int ascii = c;
                    sb.AppendFormat("{0}", ascii);
                    replacedCharacter = true;
                    continue;
                }
                sb.Append(c);
            }
            if (replacedCharacter)
                str = sb.ToString();

            // Remove non alphanumerics
            str = RxCleanUp.Replace(str, "");
            if (char.IsDigit(str[0]))
                str = "C" + str;

            return str;
        };

        [Test]
        [TestCase("HelloWorldTest", "HelloWorldTest", true)]
        [TestCase("Hello_World_Test", "HelloWorldTest", true)]
        [TestCase("Hello-World-Test", "HelloWorldTest", true)]
        [TestCase("Hello World Test", "HelloWorldTest", true)]
        [TestCase("hello world test", "HelloWorldTest", true)]
        [TestCase("HelloWORLD", "HelloWorld", true)]
        [TestCase("Helloworld", "Helloworld", true)]
        [TestCase("helloworld", "Helloworld", true)]
        [TestCase("HELLOWORLD", "Helloworld", true)]

        [TestCase("HelloWorldTest", "HelloWorldTest", false)]
        [TestCase("Hello_World_Test", "Hello_World_Test", false)]
        [TestCase("Hello-World-Test", "Hello-World-Test", false)]
        [TestCase("Hello World Test", "HelloWorldTest", false)]
        [TestCase("Hello world test", "Helloworldtest", false)]
        [TestCase("HelloWORLD", "HelloWORLD", false)]
        [TestCase("Helloworld", "Helloworld", false)]
        [TestCase("helloworld", "helloworld", false)]
        [TestCase("HELLOWORLD", "HELLOWORLD", false)]
        public void Test(string test, string expected, bool useCamelCase)
        {
            // Act
            string clean = CleanUp(test);
            string singular = clean;
            string nameHumanCase = (useCamelCase ? Inflector.ToTitleCase(singular) : singular).Replace(" ", "").Replace("$", "").Replace(".", "");

            // Assert
            Assert.AreEqual(expected, nameHumanCase);
        }
    }
}