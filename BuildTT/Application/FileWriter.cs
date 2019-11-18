using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BuildTT.Application
{
    public class FileWriter
    {
        private readonly TextWriter _stream;
        private readonly IWriterStrategy _writerStrategy;
        private readonly List<string> _usings;
        private readonly List<string> _code;

        public FileWriter(TextWriter stream, IWriterStrategy writerStrategy, List<IFileReader> readers)
        {
            _stream = stream;
            _writerStrategy = writerStrategy;

            _usings = readers
                .SelectMany(x => x.Usings())
                .Distinct()
                .ToList();

            _code = readers
                .SelectMany(x => x.Code())
                .ToList();
        }

        public void WriteUsings()
        {
            foreach (var u in _writerStrategy.Usings(_usings))
            {
                _stream.WriteLine(u);
            }
        }

        public void WriteCode()
        {
            foreach (var u in _writerStrategy.Code(_code))
            {
                _stream.WriteLine(u);
            }
        }
    }
}