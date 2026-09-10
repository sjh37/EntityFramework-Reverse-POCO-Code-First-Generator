namespace Efrpg.Gui
{
    /// <summary>
    ///     How a template spells a list of strings, kept on rewrite so the diff is the items and not the style.
    /// </summary>
    public enum StringListForm
    {
        /// <summary><c>new List&lt;string&gt; { ... }</c></summary>
        List,

        /// <summary><c>new string[] { ... }</c></summary>
        Array
    }
}
