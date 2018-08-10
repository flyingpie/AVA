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
        public IEnumerable<IndexedItem> GetItems()
        {
            // TODO: Load from settings
            var folders = new[]
            {
                @"%ProgramData%\Microsoft\Windows\Start Menu\Programs",
                @"%APPDATA%\Microsoft\Windows\Start Menu",
                @"%NEXTCLOUD%"
            }.Select(f => Environment.ExpandEnvironmentVariables(f)).ToList();

            return folders
                .GetFilesRecursive()
                .Select(path => new FileSystemIndexedItem()
                {
                    Name = Path.GetFileNameWithoutExtension(path),
                    Description = path,
                    Extension = Path.GetExtension(path),

                    Path = path,
                })
                .ToList();
        }
    }
}