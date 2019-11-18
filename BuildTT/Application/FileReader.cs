using System.Collections.Generic;

namespace BuildTT.Application
{
    public class FileReader : IFileReader
    {
        private List<string> _usings;
        private List<string> _code;
        private string _namespace;

        private string[] _raw;
        private readonly IReaderStrategy _reader;

        public FileReader(IReaderStrategy reader)
        {
            _reader = reader;
        }

        public List<string> Usings()
        {
            return _usings;
        }

        public List<string> Code()
        {
            return _code;
        }

        public string Namespace()
        {
            return _namespace;
        }

        public bool ReadFile(string inputSource)
        {
            _usings = new List<string>();
            _code = new List<string>();
            var (success, data) = _reader.ReadInput(inputSource);
            if (!success || data == null)
                return false;

            _raw = data;
            ParseCode();
            return true;
        }

        private void ParseCode()
        {
            const string namespaceSearchDot = "namespace Efrpg.";
            const string namespaceSearch = "namespace Efrpg";
            const string usingSearch = "using ";
            const string efrpgSearch = "Efrpg.";

            var foundNamespace = false;
            var foundCode = false;
            int count = _raw.Length;

            foreach (var line in _raw)
            {
                count--;
                var isUsing = false;
                if (!foundNamespace)
                {
                    if (line.StartsWith(usingSearch))
                    {
                        isUsing = true;

                        if (line.Contains(efrpgSearch))
                            continue;

                        _usings.Add(line
                            .Replace(";", string.Empty)
                            .Replace(usingSearch, string.Empty));
                    }

                    if (line.StartsWith(namespaceSearch))
                    {
                        foundNamespace = true;

                        _namespace = line
                            .Replace(namespaceSearchDot, string.Empty)
                            .Replace(namespaceSearch, string.Empty)
                            .Replace(";", string.Empty);

                        continue;
                    }
                }

                if (foundNamespace && !foundCode && line.StartsWith("{"))
                {
                    foundCode = true;
                    continue; // namespace starting brace
                }

                if (count < 2 && line.StartsWith("}"))
                    return; // namespace ending brace

                if(!isUsing)
                    _code.Add(line);
            }
        }
    }
}