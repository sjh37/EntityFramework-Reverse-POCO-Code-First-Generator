﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Efrpg.FileManagement
{
    /// <summary>
    ///     This class is responsible for:
    ///     1. Recording what files were created.
    ///     2. Write this log to into a text file, same name as the database.tt filename, but with .txt file extension.
    ///     3. Read this file back in at the start, and to delete the previous runs files prior to re-generating.
    /// </summary>
    public class FileAuditService
    {
        // List of file, including paths, relative to Settings.Root
        private readonly List<string> _files;
        private readonly DateTime _start;

        public FileAuditService()
        {
            _start = DateTime.Now;
            _files = new List<string>
            {
                "# This file contains a list of the files generated by the " + Settings.TemplateFile + ".tt file.",
                "# Please do not edit this file. It is used to delete files that may get filtered out during the next run.",
                "# Time start = " + _start.ToLocalTime()
            };
        }

        public void AddFile(string file)
        {
            _files.Add(file);
        }

        public void WriteAuditFile()
        {
            var end = DateTime.Now;
            _files.Add($"# Time end   = {end.ToLocalTime()}, duration = {(end - _start).TotalSeconds:F} seconds.");
            var filename = GetFilename();
            DeleteFilesNotRecreated(filename);
            File.WriteAllLines(filename, _files);
        }

        private void DeleteFilesNotRecreated(string filename)
        {
            if (!File.Exists(filename))
                return;

            var filesToDelete = File.ReadAllLines(filename)
                .Where(x => !x.StartsWith("#") &&
                            !string.IsNullOrWhiteSpace(x) &&
                            !_files.Contains(x))
                .ToList();

            foreach (var file in filesToDelete)
            {
                try
                {
                    var path = Path.Combine(Settings.Root, file);
                    if (File.Exists(path))
                        File.Delete(path);
                }
                catch (Exception)
                {
                    // Ignore if cannot delete, or file is missing
                }
            }
        }

        private static string GetFilename()
        {
            return Path.Combine(Settings.Root, Settings.TemplateFile + "Audit.txt");
        }
    }
}