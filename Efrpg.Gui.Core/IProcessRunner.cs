using System.Threading;
using System.Threading.Tasks;

namespace Efrpg.Gui
{
    /// <summary>
    ///     The single seam between the tool gate and the outside world, so every state the gate has to cope with -
    ///     tool missing, tool too old, no SDK, no network - is reachable from a unit test without installing or
    ///     uninstalling anything on the machine running them.
    /// </summary>
    public interface IProcessRunner
    {
        /// <summary>
        ///     Runs a process to completion. <paramref name="standardInput"/> is written to its stdin and stdin is
        ///     then closed; pass null for a process that reads none.
        /// </summary>
        /// <remarks>
        ///     stdin exists on this interface for one reason: the efrpg tool takes connection strings that way
        ///     rather than on the command line, so they never reach a process listing or the command-line audit
        ///     trail that Sysmon, EDR agents and ETW tracing forward to a SIEM.
        /// </remarks>
        Task<ProcessResult> RunAsync(string fileName, string arguments, string standardInput,
            CancellationToken cancellationToken);
    }
}
