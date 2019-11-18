using System;
using System.Collections.Generic;
using System.IO;

namespace Efrpg.Mustache
{
    /// <summary>
    /// Defines the attributes of a custom tag.
    /// </summary>
    public abstract class TagDefinition
    {
        private readonly string _tagName;

        /// <summary>
        /// Initializes a new instance of a TagDefinition.
        /// </summary>
        /// <param name="tagName">The name of the tag.</param>
        /// <exception cref="System.ArgumentException">The name of the tag is null or blank.</exception>
        protected TagDefinition(string tagName)
            : this(tagName, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of a TagDefinition.
        /// </summary>
        /// <param name="tagName">The name of the tag.</param>
        /// <param name="isBuiltIn">Specifies whether the tag is built-in or not. Checks are not performed on the names of built-in tags.</param>
        internal TagDefinition(string tagName, bool isBuiltIn)
        {
            if (!isBuiltIn && !RegexHelper.IsValidIdentifier(tagName))
            {
                throw new ArgumentException(Resources.BlankTagName, "tagName");
            }
            _tagName = tagName;
        }

        /// <summary>
        /// Gets the name of the tag.
        /// </summary>
        public string Name
        {
            get { return _tagName; }
        }

        internal bool IsSetter
        {
            get { return GetIsSetter(); }
        }

        protected virtual bool GetIsSetter()
        {
            return false;
        }

        /// <summary>
        /// Gets whether the tag is limited to the parent tag's context.
        /// </summary>
        internal bool IsContextSensitive
        {
            get { return GetIsContextSensitive(); }
        }

        /// <summary>
        /// Gets whether a tag is limited to the parent tag's context.
        /// </summary>
        protected virtual bool GetIsContextSensitive()
        {
            return false;
        }

        /// <summary>
        /// Gets the parameters that are defined for the tag.
        /// </summary>
        internal IEnumerable<TagParameter> Parameters
        {
            get { return GetParameters(); }
        }

        /// <summary>
        /// Specifies which parameters are passed to the tag.
        /// </summary>
        /// <returns>The tag parameters.</returns>
        protected virtual IEnumerable<TagParameter> GetParameters()
        {
            return new TagParameter[] { };
        }

        /// <summary>
        /// Gets whether the tag contains content.
        /// </summary>
        internal bool HasContent
        {
            get { return GetHasContent(); }
        }

        /// <summary>
        /// Gets whether tag has content.
        /// </summary>
        /// <returns>True if the tag has content; otherwise, false.</returns>
        protected abstract bool GetHasContent();

        /// <summary>
        /// Gets the tags that can indicate that the tag has closed.
        /// This field is only used if no closing tag is expected.
        /// </summary>
        internal IEnumerable<string> ClosingTags
        {
            get  { return GetClosingTags(); }
        }

        protected virtual IEnumerable<string> GetClosingTags()
        {
            if (HasContent)
            {
                return new string[] { Name };
            }
            else
            {
                return new string[] { };
            }
        }

        /// <summary>
        /// Gets the tags that are in scope within the current tag.
        /// </summary>
        internal IEnumerable<string> ChildTags
        {
            get { return GetChildTags(); }
        }

        /// <summary>
        /// Specifies which tags are scoped under the current tag.
        /// </summary>
        /// <returns>The child tag definitions.</returns>
        protected virtual IEnumerable<string> GetChildTags()
        {
            return new string[] { };
        }

        /// <summary>
        /// Gets the parameter that will be used to create a new child scope.
        /// </summary>
        /// <returns>The parameter that will be used to create a new child scope -or- null if no new scope is created.</returns>
        public abstract IEnumerable<TagParameter> GetChildContextParameters();

        /// <summary>
        /// Gets the context to use when building the inner text of the tag.
        /// </summary>
        /// <param name="writer">The text writer passed</param>
        /// <param name="keyScope">The current key scope.</param>
        /// <param name="arguments">The arguments passed to the tag.</param>
        /// <param name="contextScope">The scope context.</param>
        /// <returns>The scope to use when building the inner text of the tag.</returns>
        public virtual IEnumerable<NestedContext> GetChildContext(
            TextWriter writer, 
            Scope keyScope, 
            Dictionary<string, object> arguments,
            Scope contextScope)
        {
            NestedContext context = new NestedContext() 
            { 
                KeyScope = keyScope, 
                Writer = writer,
                ContextScope = contextScope.CreateChildScope()
            };
            yield return context;
        }

        /// <summary>
        /// Applies additional formatting to the inner text of the tag.
        /// </summary>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="arguments">The arguments passed to the tag.</param>
        /// <param name="context">The data associated to the context.</param>
        public virtual void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope context)
        {
        }

        /// <summary>
        /// Consolidates the text in the given writer to a string, using the given arguments as necessary.
        /// </summary>
        /// <param name="writer">The writer containing the text to consolidate.</param>
        /// <param name="arguments">The arguments passed to the tag.</param>
        /// <returns>The consolidated string.</returns>
        public virtual string ConsolidateWriter(TextWriter writer, Dictionary<string, object> arguments)
        {
            return writer.ToString();
        }

        /// <summary>
        /// Requests which generator group to associate the given tag type.
        /// </summary>
        /// <param name="definition">The child tag definition being grouped.</param>
        /// <returns>The name of the group to associate the given tag with.</returns>
        public virtual bool ShouldCreateSecondaryGroup(TagDefinition definition)
        {
            return false;
        }

        /// <summary>
        /// Gets whether the group with the given name should have text generated for them.
        /// </summary>
        /// <param name="arguments">The arguments passed to the tag.</param>
        /// <returns>True if text should be generated for the group; otherwise, false.</returns>
        public virtual bool ShouldGeneratePrimaryGroup(Dictionary<string, object> arguments)
        {
            return true;
        }
    }
}
