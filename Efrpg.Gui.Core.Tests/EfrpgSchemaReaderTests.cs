using System.Linq;
using System.Threading;
using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    [TestFixture]
    public class EfrpgSchemaReaderTests
    {
        private const string ConnectionString = "Data Source=(local);Initial Catalog=Northwind;Integrated Security=True";

        private static string Arguments()
        {
            return "--database SqlServer --timeout " + EfrpgSchemaReader.CommandTimeoutSeconds +
                   " --secrets-stdin --stored-procedures";
        }

        private static SchemaReadResult Read(FakeProcessRunner runner, string connectionString = ConnectionString)
        {
            return new EfrpgSchemaReader(runner, "efrpg")
                .ReadAsync("SqlServer", connectionString, CancellationToken.None)
                .GetAwaiter().GetResult();
        }

        /// <summary>
        ///     The whole reason stdin exists on IProcessRunner. A connection string on the command line is captured
        ///     by process listings and by command-line audit logging, which forwards it to a SIEM and to everyone
        ///     with access to one.
        /// </summary>
        [Test]
        public void TheConnectionStringGoesOverStdinAndNeverOntoTheCommandLine()
        {
            var runner = new FakeProcessRunner()
                .Answer("efrpg", Arguments(), ProcessResult.Completed(0, RepositoryFiles.WireContractPayload(), string.Empty));

            Read(runner);

            Assert.That(runner.Calls.Single(), Does.Not.Contain("Northwind"));
            Assert.That(runner.StandardInput.Single(), Does.Contain(ConnectionString));
            Assert.That(runner.StandardInput.Single(), Does.StartWith("<Secrets>"));
        }

        [Test]
        public void ASuccessfulReadReturnsTheSchema()
        {
            var runner = new FakeProcessRunner()
                .Answer("efrpg", Arguments(), ProcessResult.Completed(0, RepositoryFiles.WireContractPayload(), string.Empty));

            var result = Read(runner);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Schema.Count(DatabaseObjectKind.Table), Is.GreaterThan(0));
        }

        /// <summary>
        ///     Nothing is invoked at all: the tool rejects the placeholder without connecting, and saying so here is
        ///     both faster and clearer than relaying its error.
        /// </summary>
        [Test]
        public void APlaceholderConnectionStringFailsWithoutRunningAnything()
        {
            var runner = new FakeProcessRunner();

            var result = Read(runner, "Data Source=(local);Initial Catalog=" + TemplateSettingsFile.Placeholder);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Error, Does.Contain(TemplateSettingsFile.Placeholder));
            Assert.That(runner.Calls, Is.Empty);
        }

        [Test]
        public void AnEmptyConnectionStringFailsWithoutRunningAnything()
        {
            var runner = new FakeProcessRunner();

            var result = Read(runner, "   ");

            Assert.That(result.Succeeded, Is.False);
            Assert.That(runner.Calls, Is.Empty);
        }

        [Test]
        public void AToolThatCannotStartIsReportedRatherThanThrown()
        {
            var result = Read(new FakeProcessRunner());

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Error, Does.Contain("could not be started"));
        }

        /// <summary>
        ///     The database's own message is almost always more useful than anything this code could say instead,
        ///     so it is relayed rather than summarised.
        /// </summary>
        [Test]
        public void AFailedReadRelaysWhatTheToolSaid()
        {
            var runner = new FakeProcessRunner()
                .Answer("efrpg", Arguments(), ProcessResult.Completed(1, string.Empty,
                    "Cannot open database \"Northwind\" requested by the login."));

            var result = Read(runner);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Error, Is.EqualTo("Cannot open database \"Northwind\" requested by the login."));
        }

        /// <summary>
        ///     Exit code 0 with no output is the shape a crashed tool leaves behind, and parsing it as an empty
        ///     database would tell the user their schema is empty.
        /// </summary>
        [Test]
        public void SilenceIsAFailureEvenOnExitCodeZero()
        {
            var runner = new FakeProcessRunner()
                .Answer("efrpg", Arguments(), ProcessResult.Completed(0, string.Empty, string.Empty));

            var result = Read(runner);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Error, Is.Not.Empty);
        }

        [Test]
        public void OutputThatIsNotAPayloadIsAFailure()
        {
            var runner = new FakeProcessRunner()
                .Answer("efrpg", Arguments(), ProcessResult.Completed(0, "Unhandled exception: boom", string.Empty));

            var result = Read(runner);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Error, Does.Contain("efrpg tool"));
        }

        /// <summary>
        ///     Visual Studio caches its environment at launch, so a tool installed after VS started is on disk but
        ///     not on this process's PATH. The gate finds the working path; this must use it rather than resolving
        ///     the bare name again.
        /// </summary>
        [Test]
        public void TheExecutablePathFromTheGateIsUsedVerbatim()
        {
            const string fullPath = @"C:\Users\someone\.dotnet\tools\efrpg.exe";

            var runner = new FakeProcessRunner()
                .Answer(fullPath, Arguments(), ProcessResult.Completed(0, RepositoryFiles.WireContractPayload(), string.Empty));

            var result = new EfrpgSchemaReader(runner, fullPath)
                .ReadAsync("SqlServer", ConnectionString, CancellationToken.None)
                .GetAwaiter().GetResult();

            Assert.That(result.Succeeded, Is.True);
            Assert.That(runner.Calls.Single(), Does.StartWith(fullPath));
        }

        [Test]
        public void TheDatabaseTypeIsPassedThroughAsTheEnumMemberName()
        {
            foreach (var target in DatabaseTarget.All)
            {
                var runner = new FakeProcessRunner();

                new EfrpgSchemaReader(runner, "efrpg")
                    .ReadAsync(target.Name, ConnectionString, CancellationToken.None)
                    .GetAwaiter().GetResult();

                Assert.That(runner.Calls.Single(), Does.Contain("--database " + target.Name));
            }
        }
    }
}
