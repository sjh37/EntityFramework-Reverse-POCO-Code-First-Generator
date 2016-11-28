using System;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Tester.UnitTest
{
    public class ToDisplayNameTests
    {
        // This class should exactly match the code in EF.Reverse.POCO.Core.ttinclude
        private static readonly Func<string, string> ToDisplayName = (str) =>
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            var sb = new StringBuilder();
            Console.WriteLine(str);
            str = Regex.Replace(str, @"[^a-zA-Z0-9]", " "); // Anything that is not a letter or digit, convert to a space
            str = Regex.Replace(str, @"[A-Z]{2,}", " $+ "); // Any word that is upper case
            Console.WriteLine(str);

            var hasUpperCased = false;
            var lastChar = '\0';
            foreach (var original in str.Trim())
            {
                var c = original;
                if (lastChar == '\0')
                {
                    c = char.ToUpperInvariant(original);
                }
                else
                {
                    var isLetter = char.IsLetter(original);
                    var isDigit = char.IsDigit(original);
                    var isWhiteSpace = !isLetter && !isDigit;

                    // Is this char is different to last time
                    var isDifferent = false;
                    if (isLetter && !char.IsLetter(lastChar))
                        isDifferent = true;
                    else if (isDigit && !char.IsDigit(lastChar))
                        isDifferent = true;
                    else if (char.IsUpper(original) && !char.IsUpper(lastChar))
                        isDifferent = true;

                    if (isDifferent || isWhiteSpace)
                        sb.Append(' '); // Add a space

                    if (hasUpperCased && isLetter)
                        c = char.ToLowerInvariant(original);
                }
                lastChar = original;
                if (!hasUpperCased && char.IsUpper(c))
                    hasUpperCased = true;
                sb.Append(c);
            }
            str = sb.ToString();
            Console.WriteLine(str);
            str = Regex.Replace(str, @"\s+", " ").Trim(); // Multiple white space to one space
            str = Regex.Replace(str, @"\bid\b", "ID"); //  Make ID word uppercase
            return str;
        };

        [Test]
        [TestCase("HelloWorldTest", "Hello world test")]
        [TestCase("Hello\rWorld\r\nTest", "Hello world test")]
        [TestCase("Hello_World_Test", "Hello world test")]
        [TestCase("Hello_World_Test$", "Hello world test")]
        [TestCase("Hello-World-Test", "Hello world test")]
        [TestCase("Hello World Test", "Hello world test")]
        [TestCase("$Hello World Test", "Hello world test")]
        [TestCase("hello world test", "Hello world test")]
        [TestCase("          hello world              Test        ", "Hello world test")]
        [TestCase("HelloWORLD", "Hello world")]
        [TestCase("$HelloWORLD", "Hello world")]
        [TestCase("123 HelloWORLD", "123 Hello world")]
        [TestCase("123HelloWORLD", "123 Hello world")]
        [TestCase("Helloworld", "Helloworld")]
        [TestCase("$Helloworld", "Helloworld")]
        [TestCase("helloworld", "Helloworld")]
        [TestCase("$helloworld", "Helloworld")]
        [TestCase("HELLOWORLD", "Helloworld")]

        [TestCase("HelloWorldTest1", "Hello world test 1")]
        [TestCase("$HelloWorldTest1", "Hello world test 1")]
        [TestCase("Hello1_World_Test", "Hello 1 world test")]
        [TestCase("Hello_1_World_Test$", "Hello 1 world test")]

        [TestCase("HelloWorldTest", "Hello world test")]
        [TestCase("      HelloWorldTest         ", "Hello world test")]
        [TestCase("Hello_World_Test", "Hello world test")]
        [TestCase("Hello-World-Test", "Hello world test")]
        [TestCase("Hello World Test", "Hello world test")]
        [TestCase("Hello world test", "Hello world test")]
        [TestCase("HelloWORLD", "Hello world")]
        [TestCase("Helloworld", "Helloworld")]
        [TestCase("helloworld", "Helloworld")]
        [TestCase("HELLOWORLD", "Helloworld")]

        [TestCase("SomeId", "Some ID")]
        [TestCase("Some-Id", "Some ID")]
        [TestCase("Some_Id", "Some ID")]
        [TestCase("Some_ID", "Some ID")]
        [TestCase("Some ID", "Some ID")]
        [TestCase("Some-ID", "Some ID")]
        [TestCase("Some-id", "Some ID")]
        [TestCase("Some_id", "Some ID")]
        [TestCase("Some id", "Some ID")]
        public void DisplayName(string test, string expected)
        {
            var displayName = ToDisplayName(test);

            // Assert
            Assert.AreEqual(expected, displayName);
        }
    }
}