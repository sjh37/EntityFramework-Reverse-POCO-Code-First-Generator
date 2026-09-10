namespace Efrpg.Gui
{
    /// <summary>
    ///     What the gate found when it looked for the efrpg tool.
    /// </summary>
    public enum EfrpgToolState
    {
        /// <summary>
        ///     Not on the PATH and not at the per-user dotnet tools folder. The user needs to install it.
        /// </summary>
        NotFound,

        /// <summary>
        ///     The executable is there but did not answer --version usefully: a broken install, or something else
        ///     of the same name earlier on the PATH.
        /// </summary>
        NotUsable,

        /// <summary>
        ///     Found and working, but it emits an older wire format than this template can read. The user needs to
        ///     update it.
        /// </summary>
        SchemaTooOld,

        /// <summary>
        ///     Found, working, and new enough.
        /// </summary>
        Ready
    }
}
