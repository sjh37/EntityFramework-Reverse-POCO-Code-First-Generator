using System;
using System.IO;
using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    /// <summary>
    ///     The first case is the real one: a tester template in this repository keeps its Azure credentials in an
    ///     environment variable, and the dialog once showed it the SQL Server placeholder instead.
    /// </summary>
    [TestFixture]
    public class ConnectionStringSourceTests
    {
        private const string Variable = "EFRPG_GUI_TEST_CONNECTION";

        [TearDown]
        public void ClearVariable()
        {
            Environment.SetEnvironmentVariable(Variable, null);
        }

        [Test]
        public void Read_TheRealAzureTemplateIsAnEnvironmentVariableAndNotEditable()
        {
            var source = ConnectionStringSource.Read(new TemplateSettingsFile(RepositoryFiles.AzureTemplate()));

            Assert.That(source.Kind, Is.EqualTo(ConnectionStringKind.EnvironmentVariable));
            Assert.That(source.VariableName, Is.EqualTo("ReversePoco"));
            Assert.That(source.Target, Is.EqualTo(EnvironmentVariableTarget.User));
            Assert.That(source.IsEditable, Is.False);
            Assert.That(source.CanResolve, Is.True);
            Assert.That(source.Description, Is.EqualTo("environment variable \"ReversePoco\" (user)"));
            Assert.That(source.Expression, Does.StartWith("Environment.GetEnvironmentVariable("));
        }

        [Test]
        public void Read_TheShippedTemplateIsALiteral()
        {
            var source = ConnectionStringSource.Read(new TemplateSettingsFile(RepositoryFiles.DatabaseTemplate()));

            Assert.That(source.Kind, Is.EqualTo(ConnectionStringKind.Literal));
            Assert.That(source.IsEditable, Is.True);
            Assert.That(source.Value, Is.EqualTo(DatabaseTarget.Default.ConnectionString));
        }

        [TestCase("\"Data Source=(local);Initial Catalog=Northwind\"", "Data Source=(local);Initial Catalog=Northwind")]
        [TestCase("@\"Data Source=.\\SQLEXPRESS;Initial Catalog=\"\"Quoted\"\"\"", "Data Source=.\\SQLEXPRESS;Initial Catalog=\"Quoted\"")]
        public void Parse_ALiteralInEitherFormIsEditable(string expression, string expected)
        {
            var source = ConnectionStringSource.Parse(expression);

            Assert.That(source.Kind, Is.EqualTo(ConnectionStringKind.Literal));
            Assert.That(source.Value, Is.EqualTo(expected));
            Assert.That(source.IsEditable, Is.True);
        }

        [TestCase("Environment.GetEnvironmentVariable(\"X\")", null)]
        [TestCase("System.Environment.GetEnvironmentVariable(\"X\")", null)]
        [TestCase("Environment.GetEnvironmentVariable( \"X\" , EnvironmentVariableTarget.Machine )", EnvironmentVariableTarget.Machine)]
        [TestCase("System.Environment.GetEnvironmentVariable(\"X\", System.EnvironmentVariableTarget.Process)", EnvironmentVariableTarget.Process)]
        public void Parse_RecognisesTheWaysAnEnvironmentVariableIsWritten(string expression, EnvironmentVariableTarget? target)
        {
            var source = ConnectionStringSource.Parse(expression);

            Assert.That(source.Kind, Is.EqualTo(ConnectionStringKind.EnvironmentVariable));
            Assert.That(source.VariableName, Is.EqualTo("X"));
            Assert.That(source.Target, Is.EqualTo(target));
            Assert.That(source.Trims, Is.False);
            Assert.That(source.Fallback, Is.Null);
        }

        [Test]
        public void Parse_RecognisesTrimAndAFallback()
        {
            var source = ConnectionStringSource.Parse("Environment.GetEnvironmentVariable(\"X\").Trim() ?? \"Data Source=fallback\"");

            Assert.That(source.Kind, Is.EqualTo(ConnectionStringKind.EnvironmentVariable));
            Assert.That(source.Trims, Is.True);
            Assert.That(source.Fallback, Is.EqualTo("Data Source=fallback"));
            Assert.That(source.Description, Does.EndWith(", with a fallback"));
        }

        [TestCase("File.ReadAllText(@\"C:\\secrets\\cs.txt\")", "C:\\secrets\\cs.txt", false)]
        [TestCase("System.IO.File.ReadAllText(\"C:\\\\cs.txt\").Trim()", "C:\\cs.txt", true)]
        public void Parse_RecognisesAFileWithALiteralPath(string expression, string path, bool trims)
        {
            var source = ConnectionStringSource.Parse(expression);

            Assert.That(source.Kind, Is.EqualTo(ConnectionStringKind.File));
            Assert.That(source.Path, Is.EqualTo(path));
            Assert.That(source.Trims, Is.EqualTo(trims));
            Assert.That(source.IsEditable, Is.False);
            Assert.That(source.CanResolve, Is.True);
        }

        /// <summary>
        ///     The line that is not to be crossed. A variable argument, a configuration lookup or a helper call is
        ///     each one step from evaluating arbitrary C#, so they are shown and never guessed at.
        /// </summary>
        [TestCase("File.ReadAllText(path)")]
        [TestCase("Environment.GetEnvironmentVariable(name)")]
        [TestCase("Environment.GetEnvironmentVariable(\"A\") + \";Password=\" + Environment.GetEnvironmentVariable(\"B\")")]
        [TestCase("ConfigurationManager.ConnectionStrings[\"Northwind\"].ConnectionString")]
        [TestCase("GetConnectionString()")]
        [TestCase("Environment.GetEnvironmentVariable(\"A\") ?? Environment.GetEnvironmentVariable(\"B\")")]
        public void Parse_AnythingElseIsOpaque(string expression)
        {
            var source = ConnectionStringSource.Parse(expression);

            Assert.That(source.Kind, Is.EqualTo(ConnectionStringKind.Expression));
            Assert.That(source.IsEditable, Is.False);
            Assert.That(source.CanResolve, Is.False);
            Assert.That(source.Expression, Is.EqualTo(expression));

            string error;
            Assert.That(source.Resolve(out error), Is.Null);
            Assert.That(error, Does.Contain(expression));
        }

        [Test]
        public void Parse_NullMeansTheTemplateHasNoSuchLine()
        {
            var source = ConnectionStringSource.Parse(null);

            Assert.That(source.Kind, Is.EqualTo(ConnectionStringKind.Missing));
            Assert.That(source.IsEditable, Is.True);
            Assert.That(source.CanResolve, Is.False);
        }

        [Test]
        public void Resolve_ReadsTheEnvironmentVariableAndTrimsWhenTheLineDoes()
        {
            Environment.SetEnvironmentVariable(Variable, "  Data Source=(local);Initial Catalog=Northwind  ");

            string error;
            var value = ConnectionStringSource.Parse("Environment.GetEnvironmentVariable(\"" + Variable + "\").Trim()").Resolve(out error);

            Assert.That(error, Is.Null);
            Assert.That(value, Is.EqualTo("Data Source=(local);Initial Catalog=Northwind"));
        }

        [Test]
        public void Resolve_SaysWhichVariableIsMissing()
        {
            string error;
            var value = ConnectionStringSource.Parse("Environment.GetEnvironmentVariable(\"" + Variable + "\", EnvironmentVariableTarget.Process)").Resolve(out error);

            Assert.That(value, Is.Null);
            Assert.That(error, Does.Contain(Variable).And.Contain("process").And.Contain("not set"));
        }

        [Test]
        public void Resolve_UsesTheFallbackWhenTheVariableIsMissing()
        {
            string error;
            var value = ConnectionStringSource.Parse("Environment.GetEnvironmentVariable(\"" + Variable + "\") ?? \"Data Source=fallback\"").Resolve(out error);

            Assert.That(error, Is.Null);
            Assert.That(value, Is.EqualTo("Data Source=fallback"));
        }

        [Test]
        public void Resolve_ReadsTheFile()
        {
            var path = System.IO.Path.GetTempFileName();
            try
            {
                File.WriteAllText(path, "Data Source=(local);Initial Catalog=Northwind\r\n");

                string error;
                var value = ConnectionStringSource.Parse("File.ReadAllText(@\"" + path + "\").Trim()").Resolve(out error);

                Assert.That(error, Is.Null);
                Assert.That(value, Is.EqualTo("Data Source=(local);Initial Catalog=Northwind"));
            }
            finally
            {
                File.Delete(path);
            }
        }

        [Test]
        public void Resolve_SaysWhichFileCouldNotBeRead()
        {
            var path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".txt");

            string error;
            var value = ConnectionStringSource.Parse("File.ReadAllText(@\"" + path + "\")").Resolve(out error);

            Assert.That(value, Is.Null);
            Assert.That(error, Does.Contain(path));
        }
    }
}
