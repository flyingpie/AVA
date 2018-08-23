using MUI.DI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AVA.Indexing.Sources.FileSystem
{
    [Service]
    public class FileSystemIndexerSource : IIndexerSource
    {
        [Dependency] public FileSystemIndexerSourceSettings Settings { get; set; }

        public IEnumerable<IndexedItem> GetItems() => Settings.Paths
            .Select(f => Environment.ExpandEnvironmentVariables(f))
            .GetFilesRecursive()
            .Select(path => new FileSystemIndexedItem()
            {
                Name = Path.GetFileNameWithoutExtension(path),
                Description = path,
                Extension = Path.GetExtension(path),

                Path = path,
            })
            .ToList()
        ;
    }
}