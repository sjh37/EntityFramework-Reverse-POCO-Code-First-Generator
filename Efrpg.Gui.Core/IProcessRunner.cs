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
        Task<ProcessResult> RunAsync(string fileName, string arguments, CancellationToken cancellationToken);
    }
}
