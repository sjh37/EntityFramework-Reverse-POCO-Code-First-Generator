using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Efrpg.Readers;

namespace Efrpg.Gui
{
    /// <summary>
    ///     Asks the efrpg tool to read a database, for the Test button and for the object picker.
    /// </summary>
    /// <remarks>
    ///     The same binary, flags and wire format the T4 template uses at generation time, so what the GUI shows is
    ///     what the template will get. Testing a connection any other way - opening a SqlConnection here, say -
    ///     would prove something subtly different from what actually happens on save, which is the failure mode
    ///     "it tested fine but generation fails" is made of.
    ///
    ///     **The connection string goes over stdin, never on the command line.** Command lines are captured by
    ///     process listings and, more importantly, by command-line audit logging - Sysmon event 1, EDR telemetry,
    ///     ETW - which forwards them to a SIEM and to everyone with access to it. <see cref="SecretsXml"/> is the
    ///     shared source file that formats it, linked into this assembly rather than copied.
    ///
    ///     The executable path comes from <see cref="EfrpgToolGate"/> rather than being resolved again here.
    ///     Visual Studio caches its environment at launch, so a tool installed after VS started is on disk but not
    ///     on this process's PATH; the gate already found the working path, including that fallback.
    /// </remarks>
    public sealed class EfrpgSchemaReader
    {
        /// <summary>
        ///     Seconds. The generator takes this from Settings.CommandTimeout, which this assembly cannot read - it
        ///     is not a string setting and the .tt may compute it. A read that has not answered in two minutes is
        ///     not going to, and the user is sitting in front of a modal dialog.
        /// </summary>
        public const int CommandTimeoutSeconds = 120;

        private readonly IProcessRunner _runner;
        private readonly string _executablePath;

        public EfrpgSchemaReader(IProcessRunner runner, string executablePath)
        {
            if (runner == null)
                throw new ArgumentNullException(nameof(runner));

            _runner         = runner;
            _executablePath = string.IsNullOrEmpty(executablePath) ? EfrpgToolGate.ExecutableName : executablePath;
        }

        /// <summary>
        ///     Reads the database named by the connection string. Never throws for a bad connection string - that is
        ///     the expected case and comes back as a failed <see cref="SchemaReadResult"/>.
        /// </summary>
        public async Task<SchemaReadResult> ReadAsync(string databaseTypeName, string connectionString,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                return SchemaReadResult.Failure("There is no connection string to test.");

            if (connectionString.IndexOf(TemplateSettingsFile.Placeholder, StringComparison.Ordinal) >= 0)
                return SchemaReadResult.Failure(
                    "The connection string still contains " + TemplateSettingsFile.Placeholder +
                    ", so there is nothing to connect to yet.");

            var result = await _runner.RunAsync(_executablePath, Arguments(databaseTypeName),
                SecretsXml.Write(connectionString, null), cancellationToken).ConfigureAwait(false);

            if (!result.Started)
                return SchemaReadResult.Failure("The efrpg tool could not be started. " + result.StandardError);

            // The tool writes its whole document or nothing at all - it builds the XML string before writing a byte
            // to stdout - so empty output means it failed, and stderr is where it said why.
            if (result.ExitCode != 0 || string.IsNullOrWhiteSpace(result.StandardOutput))
                return SchemaReadResult.Failure(result.StandardError);

            try
            {
                return SchemaReadResult.Success(DatabaseSchema.Parse(result.StandardOutput));
            }
            catch (FormatException ex)
            {
                return SchemaReadResult.Failure(ex.Message);
            }
        }

        private static string Arguments(string databaseTypeName)
        {
            // --stored-procedures is asked for even by the Test button, because "can this account read stored
            // procedure definitions" is one of the things that goes wrong at generation time and is worth finding
            // out now rather than on first save.
            return string.Format(CultureInfo.InvariantCulture,
                "--database {0} --timeout {1} --secrets-stdin --stored-procedures",
                databaseTypeName, CommandTimeoutSeconds);
        }
    }
}
