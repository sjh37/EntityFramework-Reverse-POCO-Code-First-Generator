namespace Efrpg.Mustache
{
    /// <summary>
    /// Represents a context within a template.
    /// </summary>
    public sealed class Context
    {
        /// <summary>
        /// Initializes a new instance of a Context.
        /// </summary>
        /// <param name="tagName">The name of the tag that created the context.</param>
        /// <param name="parameters">The context parameters.</param>
        internal Context(string tagName, params ContextParameter[] parameters)
        {
            TagName = tagName;
            Parameters = parameters;
        }

        /// <summary>
        /// Gets the tag that created the context.
        /// </summary>
        public string TagName { get; private set; }

        /// <summary>
        /// Gets the argument used to create the context.
        /// </summary>
        public ContextParameter[] Parameters { get; private set; }
    }
}
