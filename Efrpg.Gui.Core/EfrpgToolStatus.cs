namespace Efrpg.Gui
{
    /// <summary>
    ///     The answer to "can this machine generate code right now, and if not, what exactly does the user type".
    /// </summary>
    public sealed class EfrpgToolStatus
    {
        public EfrpgToolStatus(EfrpgToolState state, string executablePath, string toolVersion, int schemaVersion,
            bool dotnetSdkPresent, string dotnetSdkVersion, string diagnostics)
        {
            State            = state;
            ExecutablePath   = executablePath;
            ToolVersion      = toolVersion;
            SchemaVersion    = schemaVersion;
            DotnetSdkPresent = dotnetSdkPresent;
            DotnetSdkVersion = dotnetSdkVersion;
            Diagnostics      = diagnostics ?? string.Empty;
        }

        public EfrpgToolState State { get; }

        /// <summary>
        ///     How the tool was launched, and what the wizard must launch it by. Null when it was not found.
        /// </summary>
        /// <remarks>
        ///     When this is not the bare name "efrpg", the tool was found only via the fallback path, which means it
        ///     is not on the PATH Visual Studio inherited at launch. EfrpgToolRunner resolves the bare name, so
        ///     generation from a saved .tt will still fail until Visual Studio is restarted.
        /// </remarks>
        public string ExecutablePath { get; }

        /// <summary>
        ///     The NuGet package version, purely so messages can name it. Nothing branches on it - see SchemaVersion.
        /// </summary>
        public string ToolVersion { get; }

        /// <summary>
        ///     The wire format version the tool emits. Zero when it predates the version handshake, which is the same
        ///     reading the XML reader gives a payload with no schemaVersion attribute.
        /// </summary>
        public int SchemaVersion { get; }

        /// <summary>
        ///     'dotnet tool install' needs the SDK, not just a runtime, so a machine with only the runtime can see a
        ///     working 'dotnet' and still be unable to install anything.
        /// </summary>
        public bool DotnetSdkPresent { get; }

        public string DotnetSdkVersion { get; }

        /// <summary>
        ///     Whatever the tool or dotnet wrote to stderr, verbatim.
        /// </summary>
        public string Diagnostics { get; }

        /// <summary>
        ///     True when the tool is on the PATH under its bare name, which is what EfrpgToolRunner relies on.
        /// </summary>
        public bool IsOnPath => ExecutablePath == EfrpgToolGate.ExecutableName;

        /// <summary>
        ///     The exact command to fix this, to be shown next to the button that runs it. It is displayed even when
        ///     the button will work, because it is the escape hatch for anyone behind a proxy, on an internal feed,
        ///     or without permission to install.
        /// </summary>
        public string FixCommand
        {
            get
            {
                switch (State)
                {
                    case EfrpgToolState.NotFound:
                        return EfrpgToolGate.InstallCommand;

                    case EfrpgToolState.SchemaTooOld:
                    case EfrpgToolState.NotUsable:
                        return EfrpgToolGate.UpdateCommand;

                    default:
                        return null;
                }
            }
        }
    }
}
