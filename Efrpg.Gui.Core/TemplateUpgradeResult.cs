using System.Collections.Generic;

namespace Efrpg.Gui
{
    /// <summary>
    ///     The outcome of upgrading a v3 template: either the new text and the list of edits that produced it, or
    ///     the reasons it was refused.
    /// </summary>
    /// <remarks>
    ///     Refusing is the important half. A half-applied migration leaves a template that neither compiles nor
    ///     matches the upgrade guide, which is worse than not offering the button - so anything that does not match
    ///     the shape this knows how to edit comes back as a blocker with the guide to follow by hand.
    /// </remarks>
    public sealed class TemplateUpgradeResult
    {
        private TemplateUpgradeResult(string text, IReadOnlyList<TemplateUpgradeChange> changes,
            IReadOnlyList<string> blockers)
        {
            Text     = text;
            Changes  = changes;
            Blockers = blockers;
        }

        /// <summary>The upgraded template, or null when it was refused.</summary>
        public string Text { get; }

        public IReadOnlyList<TemplateUpgradeChange> Changes { get; }

        /// <summary>What stopped the upgrade, in the user's terms. Empty on success.</summary>
        public IReadOnlyList<string> Blockers { get; }

        public bool Succeeded => Blockers.Count == 0 && Text != null;

        public static TemplateUpgradeResult Upgraded(string text, IReadOnlyList<TemplateUpgradeChange> changes)
        {
            return new TemplateUpgradeResult(text, changes, new string[0]);
        }

        public static TemplateUpgradeResult Refused(IReadOnlyList<string> blockers)
        {
            return new TemplateUpgradeResult(null, new TemplateUpgradeChange[0], blockers);
        }
    }
}
