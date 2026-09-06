using Community.VisualStudio.Toolkit;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     Per-user, persisted in Visual Studio's own settings store, so "don't ask again" survives restarts and
    ///     extension updates.
    /// </summary>
    internal sealed class UpgradeOptions : BaseOptionModel<UpgradeOptions>
    {
        /// <summary>
        ///     Whether opening a v3 template shows the offer to upgrade it. Off once the user has said no, and the
        ///     right-click command remains for whenever they change their mind.
        /// </summary>
        public bool OfferUpgradeToV4 { get; set; } = true;
    }
}
