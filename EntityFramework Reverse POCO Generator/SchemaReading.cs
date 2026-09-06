using System.Threading;
using System.Threading.Tasks;
using Efrpg.Gui;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     The one way the extension reads a database: find the efrpg tool the way the gate does, then ask it the
    ///     way the T4 template does.
    /// </summary>
    /// <remarks>
    ///     Shared by the Test button and the object picker so that both see exactly what generation will see. The
    ///     executable path comes from the gate rather than from PATH resolution because Visual Studio caches its
    ///     environment at launch: a tool installed a minute ago is on disk but not on this process's PATH.
    /// </remarks>
    internal static class SchemaReading
    {
        public static async Task<SchemaReadResult> ReadAsync(string databaseTypeName, string connectionString,
            CancellationToken cancellationToken)
        {
            var runner = new ProcessRunner();
            var status = await new EfrpgToolGate(runner).CheckAsync(cancellationToken);

            return await new EfrpgSchemaReader(runner, status.ExecutablePath)
                .ReadAsync(databaseTypeName, connectionString, cancellationToken);
        }
    }
}
