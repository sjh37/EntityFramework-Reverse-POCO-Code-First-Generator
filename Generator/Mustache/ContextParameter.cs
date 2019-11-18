namespace Efrpg.Mustache
{
    /// <summary>
    /// Holds information describing a parameter that creates a new context.
    /// </summary>
    public sealed class ContextParameter
    {
        /// <summary>
        /// Initializes a new instance of a ContextParameter.
        /// </summary>
        /// <param name="parameter">The parameter that is used to create a new context.</param>
        /// <param name="argument">The key whose corresponding value will be used to create the context.</param>
        internal ContextParameter(string parameter, string argument)
        {
            Parameter = parameter;
            Argument = argument;
        }

        /// <summary>
        /// Gets the parameter that is used to create a new context.
        /// </summary>
        public string Parameter { get; private set; }

        /// <summary>
        /// Gets the key whose corresponding value will be used to create the context.
        /// </summary>
        public string Argument { get; private set; }
    }
}
