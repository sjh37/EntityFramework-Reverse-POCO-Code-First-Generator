namespace Efrpg.Gui
{
    /// <summary>
    ///     One edit the v3 to v4 upgrade made, in a form the confirmation dialog can show before anything is
    ///     written.
    /// </summary>
    public sealed class TemplateUpgradeChange
    {
        public TemplateUpgradeChange(string description, string before, string after)
        {
            Description = description;
            Before      = before ?? string.Empty;
            After       = after ?? string.Empty;
        }

        /// <summary>Why this edit is needed, in one line the user can judge.</summary>
        public string Description { get; }

        public string Before { get; }

        /// <summary>Empty when the edit deletes something.</summary>
        public string After { get; }

        public override string ToString()
        {
            return Description;
        }
    }
}
