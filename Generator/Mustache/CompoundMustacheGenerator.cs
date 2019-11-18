using System;
using System.Collections.Generic;
using System.IO;

namespace Efrpg.Mustache
{
    /// <summary>
    /// Builds text by combining the output of other generators.
    /// </summary>
    internal sealed class CompoundMustacheGenerator : IMustacheGenerator
    {
        private readonly TagDefinition _definition;
        private readonly ArgumentCollection _arguments;
        private readonly List<IMustacheGenerator> _primaryGenerators;
        private IMustacheGenerator _subMustacheGenerator;

        /// <summary>
        /// Initializes a new instance of a CompoundGenerator.
        /// </summary>
        /// <param name="definition">The tag that the text is being generated for.</param>
        /// <param name="arguments">The arguments that were passed to the tag.</param>
        public CompoundMustacheGenerator(TagDefinition definition, ArgumentCollection arguments)
        {
            _definition = definition;
            _arguments = arguments;
            _primaryGenerators = new List<IMustacheGenerator>();
        }

        /// <summary>
        /// Adds the given generator. 
        /// </summary>
        /// <param name="mustacheGenerator">The generator to add.</param>
        public void AddGenerator(IMustacheGenerator mustacheGenerator)
        {
            addGenerator(mustacheGenerator, false);
        }

        /// <summary>
        /// Adds the given generator, determining whether the generator should
        /// be part of the primary generators or added as an secondary generator.
        /// </summary>
        /// <param name="definition">The tag that the generator is generating text for.</param>
        /// <param name="mustacheGenerator">The generator to add.</param>
        public void AddGenerator(TagDefinition definition, IMustacheGenerator mustacheGenerator)
        {
            bool isSubGenerator = _definition.ShouldCreateSecondaryGroup(definition);
            addGenerator(mustacheGenerator, isSubGenerator);
        }

        private void addGenerator(IMustacheGenerator mustacheGenerator, bool isSubGenerator)
        {
            if (isSubGenerator)
            {
                _subMustacheGenerator = mustacheGenerator;
            }
            else
            {
                _primaryGenerators.Add(mustacheGenerator);
            }
        }

        void IMustacheGenerator.GetText(TextWriter writer, Scope keyScope, Scope contextScope, Action<Substitution> postProcessor)
        {
            Dictionary<string, object> arguments = _arguments.GetArguments(keyScope, contextScope);
            IEnumerable<NestedContext> contexts = _definition.GetChildContext(writer, keyScope, arguments, contextScope);
            List<IMustacheGenerator> generators;
            if (_definition.ShouldGeneratePrimaryGroup(arguments))
            {
                generators = _primaryGenerators;
            }
            else
            {
                generators = new List<IMustacheGenerator>();
                if (_subMustacheGenerator != null)
                {
                    generators.Add(_subMustacheGenerator);
                }
            }
            foreach (NestedContext context in contexts)
            {
                foreach (IMustacheGenerator generator in generators)
                {
                    generator.GetText(context.Writer ?? writer, context.KeyScope ?? keyScope, context.ContextScope, postProcessor);
                }

                if (context.WriterNeedsConsidated)
                {
                    writer.Write(_definition.ConsolidateWriter(context.Writer ?? writer, arguments));
                }
            }
        }
    }
}