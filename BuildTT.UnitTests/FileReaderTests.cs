using System.Collections.Generic;
using BuildTT.Application;
using NUnit.Framework;

namespace BuildTT.UnitTests
{
    [TestFixture]
    public class FileReaderTests
    {
        [Test]
        public void NoUsings()
        {
            // Arrange
            string[] data =
            {
                "namespace Efrpg.FileManagement",
                "{",
                "    public interface IFileManager",
                "    {",
                "        void StartHeader();",
                "        void Process(bool split = true);",
                "        void StartNewFile(string name);",
                "    }",
                "}"
            };
            string[] expected =
            {
                "    public interface IFileManager",
                "    {",
                "        void StartHeader();",
                "        void Process(bool split = true);",
                "        void StartNewFile(string name);",
                "    }"
            };
            var reader = new StringReaderStrategy(data);
            var fileReader = new FileReader(reader);

            // Act
            var result = fileReader.ReadFile(null);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("FileManagement", fileReader.Namespace());
            Assert.IsEmpty(fileReader.Usings());
            Assert.AreEqual(expected, fileReader.Code());
        }

        [Test]
        public void WithComments()
        {
            // Arrange
            string[] data =
            {
                "file comment",
                "another file comment",
                "namespace Efrpg.Simon",
                "{",
                "    // Code comment",
                "    blah",
                "}"
            };
            string[] expected =
            {
                "file comment",
                "another file comment",
                "    // Code comment",
                "    blah"
            };
            var reader = new StringReaderStrategy(data);
            var fileReader = new FileReader(reader);

            // Act
            var result = fileReader.ReadFile(null);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("Simon", fileReader.Namespace());
            Assert.IsEmpty(fileReader.Usings());
            Assert.AreEqual(expected, fileReader.Code());
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void WithUsings(bool includeBlanks)
        {
            // Arrange
            var data = new List<string>
            {
                "using Hello;",
                "using World;",
                "using Efrpg.Gear;"
            };

            if (includeBlanks)
                data.AddRange(new List<string> { "", "", "", "" });

            var expectedCode = new List<string>
            {
                "    public interface IFileManager",
                "    {",
                "        void StartHeader();",
                "        void Process(bool split = true);",
                "        void StartNewFile(string name);",
                "    }"
            };

            data.Add("namespace Efrpg.FileManagement");
            data.Add("{");
            data.AddRange(expectedCode);
            data.Add("}");

            if (includeBlanks)
                expectedCode.InsertRange(0, new List<string> { "", "", "", "" });

            var expectedUsings = new List<string>
            {
                "Hello",
                "World"
            };
            var reader = new StringReaderStrategy(data.ToArray());
            var fileReader = new FileReader(reader);

            // Act
            var result = fileReader.ReadFile(null);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("FileManagement", fileReader.Namespace());
            Assert.AreEqual(expectedUsings, fileReader.Usings());
            Assert.AreEqual(expectedCode, fileReader.Code());
        }
    }
}
