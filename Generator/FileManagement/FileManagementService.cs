using System;
using System.Collections.Generic;
using System.IO;
using Efrpg.Filtering;

namespace Efrpg.FileManagement
{
    public class FileManagementService : IFileManager
    {
        private readonly GeneratedTextTransformation _outer;
        private readonly Dictionary<string, IFileManager> _fileManagers;
        private IFileManager _fileManager;
        private VisualStudioFileManager _visualStudioFileManager;
        private readonly FileAuditService _auditService;
        private bool _writeToOuter;
        public bool ForceWriteToOuter;

        public FileManagementService(GeneratedTextTransformation outer)
        {
            if (outer == null) throw new ArgumentNullException(nameof(outer));

            _outer = outer;
            _fileManagers = new Dictionary<string, IFileManager>();
            _fileManager = null;
            _visualStudioFileManager = null;
            _auditService = new FileAuditService();
        }

        public static void DeleteFile(string filename)
        {
            try
            {
                var path = Path.Combine(Settings.Root, filename);
                File.Delete(path);
            }
            catch
            {
                // ignored
            }
        }

        public void Init(Dictionary<string, IDbContextFilter> filters, Type fileManagerType)
        {
            Settings.FilterCount = filters.Count;

            _writeToOuter = Settings.GenerateSingleDbContext && !Settings.GenerateSeparateFiles;

            // For debug
            /*var a = _writeToOuter;
            var b = Settings.FilterCount;
            var c = Settings.GenerateSeparateFiles;
            var d = Settings.TemplateType;
            var e = Settings.GenerateSingleDbContext;
            var f = filters.First().Key;*/

            if (fileManagerType == typeof(VisualStudioFileManager))
            {
                _visualStudioFileManager = (VisualStudioFileManager)Activator.CreateInstance(fileManagerType);
                _visualStudioFileManager.Init(_outer);

                // Switch to the EfCoreFileManager for the rest
                fileManagerType = typeof(EfCoreFileManager);
            }

            foreach (var filter in filters)
            {
                var fileManager = (IFileManager)Activator.CreateInstance(fileManagerType);
                fileManager.Init(_outer);
                if (!string.IsNullOrWhiteSpace(filter.Key))
                    fileManager.StartNewFile(filter.Key + Settings.FileExtension);
                _fileManagers.Add(filter.Key, fileManager);
            }
        }

        public void UseFileManager(string key)
        {
            _fileManager = _fileManagers[key];
        }

        public void Error(string error)
        {
            // Write any errors to the primary output cs file
            _outer.WriteLine(error);
        }

        public void WriteLine(string text)
        {
            if (_writeToOuter || ForceWriteToOuter)
                _outer.WriteLine(text);
            else
                _fileManager.WriteLine(text);
        }

        public void Init(GeneratedTextTransformation textTransformation)
        {
            throw new NotImplementedException();
        }

        public void StartHeader()
        {
            _fileManager.StartHeader();
        }

        public void StartFooter()
        {
            _fileManager.StartFooter();
        }

        public void EndBlock()
        {
            _fileManager.EndBlock();
        }

        public void Process(bool split)
        {
            _auditService.WriteAuditFile();

            foreach (var fileManager in _fileManagers)
            {
                if (_visualStudioFileManager == null)
                {
                    fileManager.Value.Process(split);
                    continue;
                }

                if (fileManager.Value.GetType() == typeof(EfCoreFileManager))
                    ((EfCoreFileManager)fileManager.Value).ProcessToAnotherFileManager(_visualStudioFileManager, _outer);
                else
                    fileManager.Value.Process(split);
            }

            _visualStudioFileManager?.Process(split);
        }

        public void StartNewFile(string name)
        {
            _fileManager.StartNewFile(name);
            _auditService.AddFile(name);
        }
    }
}