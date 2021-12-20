using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Efrpg.FileManagement
{
    public class EfCoreFileManager : IFileManager
    {
        private readonly List<FileBlock> _blocks;
        private string _filename;
        private StringBuilder _sb;
        private const int StringBuilderSize = 2048;

        public EfCoreFileManager()
        {
            _blocks   = new List<FileBlock>();
            _filename = null;
            _sb       = new StringBuilder(StringBuilderSize);
        }

        public void Init(GeneratedTextTransformation textTransformation)
        {
        }

        public void StartHeader()
        {
        }

        public void StartFooter()
        {
        }

        public void EndBlock()
        {
            AddCurrentBlock();
        }

        public void Process(bool split)
        {
            AddCurrentBlock();

            foreach (var fileBlock in _blocks)
            {
                var filename = Path.Combine(Settings.Root, fileBlock.Filename);
                using (var file = new StreamWriter(filename))
                {
                    file.Write(fileBlock.Text);
                }
            }
        }

        public void ProcessToAnotherFileManager(IFileManager fileManager, GeneratedTextTransformation outer)
        {
            AddCurrentBlock();

            foreach (var fileBlock in _blocks)
            {
                fileManager.StartNewFile(fileBlock.Filename);
                outer.WriteLine(fileBlock.Text);
                fileManager.EndBlock();
            }
        }

        public void StartNewFile(string name)
        {
            _filename = name;
        }

        public void WriteLine(string text)
        {
            _sb.AppendLine(text);
        }

        private void AddCurrentBlock()
        {
            if (_sb.Length == 0)
                return;

            if (string.IsNullOrEmpty(_filename))
            {
                _sb = new StringBuilder(StringBuilderSize);
                return;
            }

            _blocks.Add(new FileBlock
            {
                Text = _sb.ToString(),
                Filename = _filename
            });

            _filename = null;
            _sb = new StringBuilder(StringBuilderSize);
        }

        private class FileBlock
        {
            public string Text { get; set; }
            public string Filename { get; set; }
        }
    }
}