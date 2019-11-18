namespace Efrpg.Mustache
{
    public static class Resources
    {
        public const string BlankParameterName = "An attempt was made to define a parameter with a null or an invalid identifier.";
        public const string BlankTagName = "An attempt was made to define a tag with a null or an invalid identifier.";
        //public const string DuplicateParameter = "A parameter with the same name already exists within the tag.";
        public const string DuplicateTagDefinition = "The {0} tag has already been registered.";
        public const string MissingClosingTag = "Expected a matching {0} tag but none was found.";
        public const string PartialNotDefined = "A partial template named {0} could not be found.";
        public const string UnknownTag = "Encountered an unknown tag: {0}. It was either not registered or exists in a different context.";
        public const string WrongNumberOfArguments = "The wrong number of arguments were passed to an {0} tag.";
    }
}