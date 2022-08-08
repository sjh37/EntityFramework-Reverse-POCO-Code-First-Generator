using System;
using Efrpg;
using Efrpg.Readers;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture]
    [Category(Constants.CI)]
    public class SchemaReaderTests
    {
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
            var clean = DatabaseReader.CleanUp(test);
            var singular = clean;
            var nameHumanCase = (useCamelCase ? Inflector.ToTitleCase(singular) : singular).Replace(" ", "").Replace("$", "").Replace(".", "");

            // Assert
            Assert.AreEqual(expected, nameHumanCase);
        }

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
        
        [TestCase("MPANCore", "Mpan core")]
        [TestCase("MPAN Core", "Mpan core")]
        [TestCase("MPAN core", "Mpan core")]
        [TestCase("mpan core", "Mpan core")]
        [TestCase("FOOBarBAZ", "Foo bar baz")]
        [TestCase("FooBARBaz", "Foo bar baz")]
        [TestCase("FooBarBaz", "Foo bar baz")]
        [TestCase("ABC", "Abc")]
        public void DisplayName(string test, string expected)
        {
            // Act
            Console.WriteLine(test);
            var displayName = Column.ToDisplayName(test);

            // Assert
            Assert.AreEqual(expected, displayName);
        }
    }
}