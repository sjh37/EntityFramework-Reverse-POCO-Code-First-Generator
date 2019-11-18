using System;
using System.Collections.Generic;
using Efrpg.Mustache;
using Efrpg.Templates;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture]
    public class TemplateTransformationTests
    {
        [Test]
        [TestCase("{{hello}}", "world")]
        [TestCase("x{{hello}}x", "xworldx")]
        [TestCase("x {{hello}} x", "x world x")]
        [TestCase(" x {{hello}} x ", " x world x ")]
        public void SimpleTest(string template, string expected)
        {
            // Arrange
            var data = new Dictionary<string, object>
            {
                ["hello"] = "world",
            };

            // Act
            var output = Template.Transform(template, data);
            Console.WriteLine(output);

            // Assert
            Assert.IsNotNull(output);
            Assert.AreEqual(expected, output);
        }

        [Test]
        [TestCase("{{hello}}", "world")]
        [TestCase("x{{hello}}x", "xworldx")]
        [TestCase("x {{hello}} x", "x world x")]
        [TestCase(" x {{hello}} x ", " x world x ")]
        [TestCase("{{hello1}}", "worldIsBlue")]
        [TestCase("x{{hello1}}x", "xworldIsBluex")]
        [TestCase("x {{hello1}} x", "x worldIsBlue x")]
        [TestCase(" x {{hello1}} x ", " x worldIsBlue x ")]
        public void MultipleDataEntriesTest(string template, string expected)
        {
            // Arrange
            var data = new Dictionary<string, object>
            {
                ["hello"] = "world",
                ["hello1"] = "worldIsBlue",
            };

            // Act
            var output = Template.Transform(template, data);
            Console.WriteLine(output);

            // Assert
            Assert.IsNotNull(output);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void MultiLineTest()
        {
            // Arrange
            var data = new Dictionary<string, object>
            {
                ["classModifier"] = "public",
                ["contextName"] = "TestContext"
            };

            var template = @"
{{classModifier}} class {{contextName}}Factory : System.Data.Entity.Infrastructure.IDbContextFactory<{{contextName}}>{{#newline}}
{{{#newline}}
    public {{contextName}} Create(){{#newline}}
    {{{#newline}}
        return new {{contextName}}();{{#newline}}
    }{{#newline}}
}";

            // Act
            var output = Template.Transform(template, data);
            Console.WriteLine(output);
            
            // Assert
            Assert.AreEqual(@"public class TestContextFactory : System.Data.Entity.Infrastructure.IDbContextFactory<TestContext>
{
    public TestContext Create()
    {
        return new TestContext();
    }
}", output);
        }

        [Test]
        public void LoopTest()
        {
            // Arrange
            var data = new
            {
                beginning = "First line",
                tables = new[]
                {
                    new { name = "Hello", hasComment = true, comment = "World" },
                    new { name = "Hi",    hasComment = false, comment = "" },
                    new { name = "Foo",   hasComment = true, comment = "Bar" }
                },
                ending = "Last line"
            };

            var template = @"{{beginning}}{{#newline}}
{{#each tables}}    List<{{name}}> {{name}} = null;{{#if hasComment}} // {{comment}}{{/if}}{{#newline}}
{{/each}}{{ending}}";

            // Act
            var parser = new FormatCompiler();
            var mustacheGenerator = parser.Compile(template);
            var output = mustacheGenerator.Render(data);
            Console.WriteLine(output);
            
            // Assert
            Assert.AreEqual(@"First line
    List<Hello> Hello = null; // World
    List<Hi> Hi = null;
    List<Foo> Foo = null; // Bar
Last line", output);
        }
    }
}