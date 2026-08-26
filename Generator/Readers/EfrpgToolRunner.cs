using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Efrpg.Readers
{
    // Runs the 'efrpg' dotnet tool as a child process. Connection strings go over stdin (see SecretsXml), never on
    // the command line. stdout and stderr are drained on separate threads to avoid a pipe-buffer deadlock.
    //
    // Every caller goes through here - the T4 templates, the enum second pass and the integration tests - so the
    // process plumbing exists once rather than being copied into each of them.
    public static class EfrpgToolRunner
    {
        // Full schema read. This is the first tool invocation of a run.
        public static EfrpgResult ReadDatabase(bool includeStoredProcedures, bool includeSynonyms, bool multiContext)
        {
            var args = string.Format("--database {0} --timeout {1} --secrets-stdin{2}{3}{4}",
                Settings.DatabaseType,
                Settings.CommandTimeout,
                includeStoredProcedures ? " --stored-procedures" : string.Empty,
                includeSynonyms ? " --synonyms" : string.Empty,
                multiContext ? " --multi-context" : string.Empty);

            var secrets = SecretsXml.Write(
                Settings.ConnectionString,
                multiContext ? Settings.MultiContextSettingsConnectionString : null);

            return EfrpgResultXmlReader.Read(Execute(args, secrets));
        }

        // Second invocation, once the Generator's AddEnum callbacks have resolved the enumeration specs. The specs
        // themselves are only table and column names, so they travel on the command line quite happily.
        public static string Run(string extraArgs)
        {
            var args = string.Format("--database {0} --timeout {1} --secrets-stdin {2}",
                Settings.DatabaseType, Settings.CommandTimeout, extraArgs);

            return Execute(args, SecretsXml.Write(Settings.ConnectionString, null));
        }

        public static string Execute(string args, string secretsXml)
        {
            var psi = new ProcessStartInfo("efrpg", args)
            {
                UseShellExecute         = false,
                RedirectStandardInput   = true,
                RedirectStandardOutput  = true,
                RedirectStandardError   = true,
                CreateNoWindow          = true,
                // The tool writes UTF-8 (it sets Console.OutputEncoding); without matching decode here, non-Latin
                // identifiers (Cyrillic table names, oe columns) become '?' before they reach the XML parser.
                StandardOutputEncoding  = Encoding.UTF8,
                StandardErrorEncoding   = Encoding.UTF8,
            };

            using (var proc = Process.Start(psi))
            {
                // Write the secrets and close stdin before draining the output streams. .NET Framework has no
                // StandardInputEncoding, so write UTF-8 bytes to the underlying stream rather than through the
                // StreamWriter's encoding. Closing is required or the tool's ReadToEnd never returns.
                var secretBytes = Encoding.UTF8.GetBytes(secretsXml ?? string.Empty);
                proc.StandardInput.BaseStream.Write(secretBytes, 0, secretBytes.Length);
                proc.StandardInput.BaseStream.Flush();
                proc.StandardInput.Close();

                string stdout = null, stderr = null;
                var tOut = new Thread(() => { stdout = proc.StandardOutput.ReadToEnd(); });
                var tErr = new Thread(() => { stderr = proc.StandardError.ReadToEnd(); });
                tOut.Start();
                tErr.Start();
                proc.WaitForExit();
                tOut.Join();
                tErr.Join();

                if (proc.ExitCode == 1 || string.IsNullOrEmpty(stdout))
                    throw new Exception(string.IsNullOrEmpty(stderr) ? "efrpg tool returned no output." : stderr);

                return stdout;
            }
        }
    }
}
