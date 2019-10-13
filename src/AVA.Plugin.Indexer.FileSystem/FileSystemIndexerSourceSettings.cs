using AVA.Core.Settings;
using MUI.Utils;
using System;
using System.Collections.Generic;

namespace AVA.Plugin.Indexer.FileSystem
{
    [Section("Indexer.FileSystem")]
    public class FileSystemIndexerSourceSettings : Settings
    {
        public FileSystemIndexerSourcePath[] Paths { get; set; } = new FileSystemIndexerSourcePath[0];
    }

    public class FileSystemIndexerSourcePath
    {
        public string Path { get; set; }

        public string[] Includes { get; set; }

        public string[] Excludes { get; set; }

        public ICollection<string> GetFiles() => Glob.Execute(Environment.ExpandEnvironmentVariables(Path), Includes, Excludes);
    }
}