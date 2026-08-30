using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Efrpg.Gui
{
    /// <summary>
    ///     Runs a child process and collects its output, without blocking the caller's thread.
    /// </summary>
    /// <remarks>
    ///     Asynchronous because the caller is a Visual Studio dialog on the UI thread and 'dotnet tool install' can
    ///     take the best part of a minute against a slow feed. A synchronous wait here would freeze the IDE.
    ///
    ///     Output is drained through the OutputDataReceived events rather than ReadToEnd, for the same reason
    ///     EfrpgToolRunner drains on two threads: a process that fills the stdout pipe while the parent is blocked
    ///     reading stderr deadlocks, and neither side ever times out.
    /// </remarks>
    public sealed class ProcessRunner : IProcessRunner
    {
        public async Task<ProcessResult> RunAsync(string fileName, string arguments, CancellationToken cancellationToken)
        {
            var startInfo = new ProcessStartInfo(fileName, arguments)
            {
                UseShellExecute        = false,
                RedirectStandardOutput = true,
                RedirectStandardError  = true,
                CreateNoWindow         = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding  = Encoding.UTF8
            };

            var standardOutput = new StringBuilder();
            var standardError  = new StringBuilder();
            var exited         = new TaskCompletionSource<int>();

            using (var process = new Process { StartInfo = startInfo, EnableRaisingEvents = true })
            {
                process.OutputDataReceived += (_, e) => Append(standardOutput, e.Data);
                process.ErrorDataReceived  += (_, e) => Append(standardError, e.Data);
                process.Exited             += (_, __) => exited.TrySetResult(process.ExitCode);

                try
                {
                    process.Start();
                }
                catch (Win32Exception ex)
                {
                    // The executable is not on the PATH, or not at the path given. This is the case the gate exists
                    // to report, so it is a result rather than an exception.
                    return ProcessResult.FailedToStart(ex.Message);
                }

                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                using (cancellationToken.Register(() => Kill(process)))
                {
                    var exitCode = await exited.Task.ConfigureAwait(false);

                    // Exited fires before the last output events are guaranteed to have been raised; this flushes them.
                    process.WaitForExit();

                    return ProcessResult.Completed(exitCode, standardOutput.ToString(), standardError.ToString());
                }
            }
        }

        private static void Append(StringBuilder builder, string line)
        {
            if (line == null)
                return; // End of stream

            if (builder.Length > 0)
                builder.Append(Environment.NewLine);

            builder.Append(line);
        }

        private static void Kill(Process process)
        {
            try
            {
                if (!process.HasExited)
                    process.Kill();
            }
            catch (Exception)
            {
                // The process ended between the check and the kill, or we are not allowed to end it. Either way the
                // caller is already abandoning this run, so there is nothing useful to do or report.
            }
        }
    }
}
