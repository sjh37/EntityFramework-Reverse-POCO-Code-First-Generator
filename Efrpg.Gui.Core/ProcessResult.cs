namespace Efrpg.Gui
{
    /// <summary>
    ///     What happened when a child process was run: whether it started at all, and what it said.
    /// </summary>
    /// <remarks>
    ///     "Failed to start" is a first-class outcome rather than an exception, because it is the normal case the
    ///     gate exists to detect - the efrpg tool not being installed shows up as a Win32Exception from
    ///     Process.Start, which is information, not a fault.
    /// </remarks>
    public sealed class ProcessResult
    {
        private ProcessResult(bool started, int exitCode, string standardOutput, string standardError)
        {
            Started        = started;
            ExitCode       = exitCode;
            StandardOutput = standardOutput ?? string.Empty;
            StandardError  = standardError ?? string.Empty;
        }

        /// <summary>
        ///     False when the executable could not be launched at all, usually because it is not on the PATH.
        /// </summary>
        public bool Started { get; }

        public int ExitCode { get; }

        public string StandardOutput { get; }

        /// <summary>
        ///     Never swallowed and never summarised. The gate shows it to the user exactly as the tool wrote it,
        ///     because the useful part of a failed 'dotnet tool install' is usually the proxy or feed error inside it.
        /// </summary>
        public string StandardError { get; }

        public bool Succeeded => Started && ExitCode == 0;

        public static ProcessResult Completed(int exitCode, string standardOutput, string standardError)
        {
            return new ProcessResult(true, exitCode, standardOutput, standardError);
        }

        public static ProcessResult FailedToStart(string reason)
        {
            return new ProcessResult(false, -1, string.Empty, reason);
        }
    }
}
