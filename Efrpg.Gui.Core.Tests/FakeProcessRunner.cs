using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Efrpg.Gui;

namespace Efrpg.Gui.Tests
{
    /// <summary>
    ///     Hand-rolled rather than mocked: the interface has one method, and every state the gate must cope with is
    ///     just a canned answer to a (file, arguments) pair.
    /// </summary>
    internal sealed class FakeProcessRunner : IProcessRunner
    {
        private readonly Dictionary<string, ProcessResult> _answers = new();

        /// <summary>
        ///     Every command asked for, in order, so a test can assert which executable the gate actually tried and
        ///     in what order it fell back.
        /// </summary>
        public List<string> Calls { get; } = new();

        public FakeProcessRunner Answer(string fileName, string arguments, ProcessResult result)
        {
            _answers[Key(fileName, arguments)] = result;
            return this;
        }

        /// <summary>
        ///     Anything not explicitly answered is treated as "no such executable", which is the honest default: a
        ///     machine without the tool is exactly a machine where Process.Start throws.
        /// </summary>
        public Task<ProcessResult> RunAsync(string fileName, string arguments, CancellationToken cancellationToken)
        {
            Calls.Add(Key(fileName, arguments));

            return Task.FromResult(_answers.TryGetValue(Key(fileName, arguments), out var result)
                ? result
                : ProcessResult.FailedToStart("The system cannot find the file specified"));
        }

        private static string Key(string fileName, string arguments) => fileName + " " + arguments;
    }
}
