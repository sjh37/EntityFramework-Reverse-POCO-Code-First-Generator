using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BuildTT.Application;
using NUnit.Framework;

namespace BuildTT.UnitTests
{
    [TestFixture]
    public class FileWriterTests
    {
        private List<IFileReader> _fileReaders;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            _fileReaders = new List<IFileReader>();
            string[] data1 =
            {
                "using System.Text;",
                "using Efrpg.aaa;",
                "using System.Text;",
                "using System.Text;",
                "using System.Text;",
                "using System.Text;",
                "using Efrpg.bbb.ccc;",
                "using System.Fred;",
                "namespace Efrpg.FileManagement",
                "{",
                "    void abc()",
                "    {",
                "        var Efrpg.a = new Efrpg.blah();",
                "        var b = new Something(Efrpg.Hello);",
                "    }",
                "}"
            };
            string[] data2 =
            {
                "using System.XYZ;",
                "using Efrpg.ddd;",
                "using System.Crypt;",
                "using Efrpg.eee;",
                "using System.Text;",
                "using System.Text;",
                "using System.Text;",
                "namespace Efrpg.Generator",
                "{",
                "    void def()",
                "    {",
                "        var a = new Efrpg.something();",
                "        var Efrpg.b = new AnotherThing(Efrpg.World);",
                "    }",
                "}"
            };
            string[] data3 =
            {
                "using System.XYZ;",
                "",
                "namespace Efrpg",
                "{",
                "    public static class AssemblyHelper",
                "    {",
                "        // todo",
                "    }",
                "}"
            };
            var reader1 = new StringReaderStrategy(data1);
            var reader2 = new StringReaderStrategy(data2);
            var reader3 = new StringReaderStrategy(data3);
            _fileReaders.Add(new FileReader(reader1));
            _fileReaders.Add(new FileReader(reader2));
            _fileReaders.Add(new FileReader(reader3));
        }

        [Test]
        public void NamespaceReplacement()
        {
            // Arrange
            string[] expectedCode =
            {
                "using System.Crypt;",
                "using System.Fred;",
                "using System.Text;",
                "using System.XYZ;",
                "    void abc()",
                "    {",
                "        var a = new blah();",
                "        var b = new Something(Hello);",
                "    }",
                "    void def()",
                "    {",
                "        var a = new something();",
                "        var b = new AnotherThing(World);",
                "    }",
                "",
                "    public static class AssemblyHelper",
                "    {",
                "        // todo",
                "    }"
            };

            // Act
            var result = Act(new WriterStrategy());

            // Assert
            Assert.IsNotNull(result);
            foreach (var s in result)
            {
                Console.WriteLine(s);
            }
            Assert.AreEqual(expectedCode, result);
        }

        [Test]
        public void TTNamespaceReplacement()
        {
            // Arrange
            string[] expectedCode =
            {
                "<#@ import namespace=\"System.Crypt\" #>",
                "<#@ import namespace=\"System.Fred\" #>",
                "<#@ import namespace=\"System.Text\" #>",
                "<#@ import namespace=\"System.XYZ\" #>",
                "    void abc()",
                "    {",
                "        var a = new blah();",
                "        var b = new Something(Hello);",
                "    }",
                "    void def()",
                "    {",
                "        var a = new something();",
                "        var b = new AnotherThing(World);",
                "    }",
                "",
                "    public static class AssemblyHelper",
                "    {",
                "        // todo",
                "    }"
            };

            // Act
            var result = Act(new TTWriterStrategy());

            // Assert
            Assert.IsNotNull(result);
            foreach (var s in result)
            {
                Console.WriteLine(s);
            }
            Assert.AreEqual(expectedCode, result);
        }

        private string[] Act(IWriterStrategy writerStrategy)
        {
            foreach (var reader in _fileReaders)
            {
                var readerResult = reader.ReadFile(null);
                Assert.IsTrue(readerResult);
            }

            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                var writer = new FileWriter(sw, writerStrategy, _fileReaders);
                writer.WriteUsings();
                writer.WriteCode();
                sw.Close();
            }

            return sb
                .ToString()
                .Trim()
                .Replace(Environment.NewLine, "\r")
                .Split('\r');
        }
    }
}