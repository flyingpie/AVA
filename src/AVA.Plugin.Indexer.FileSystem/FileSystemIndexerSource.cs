using AVA.Plugin.Indexer;
using MUI.DI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AVA.Plugin.Indexer.FileSystem
{
    [Service]
    public class FileSystemIndexerSource : IIndexerSource
    {
        [Dependency] public FileSystemIndexerSourceSettings Settings { get; set; }

        public IEnumerable<IndexedItem> GetItems() => Settings.Paths
            .Select(f => Environment.ExpandEnvironmentVariables(f))
            .GetFilesRecursiveX()
            .Select(path => new FileSystemIndexedItem()
            {
                IndexerName = Path.GetFileNameWithoutExtension(path),
                Description = path,

                Path = path,
            })
            .ToList()
        ;
    }

    public static class Ext
    {
        public static List<string> GetFilesRecursiveX(this IEnumerable<string> folders, List<string> files = null)
        {
            files = files ?? new List<string>();

            foreach (var folder in folders)
            {
                try
                {
                    var subFiles = Directory.GetFiles(folder, "*", SearchOption.TopDirectoryOnly);

                    if (TryGetAvaJson(subFiles, out var avaSettings))
                    {
                        if (!avaSettings.Index) continue;
                    }

                    foreach (var file in subFiles)
                    {
                        files.Add(file);
                    }

                    var dd = Directory.GetDirectories(folder);

                    GetFilesRecursiveX(dd, files);
                }
                catch { }
            }

            return files;
        }

        public static bool TryGetAvaJson(string[] files, out AvaSettings settings)
        {
            settings = null;

            var avaJson = files.FirstOrDefault(s => Path.GetFileName(s).Equals("ava.json", StringComparison.OrdinalIgnoreCase));

            if (avaJson != null)
            {
                try
                {
                    settings = JsonConvert.DeserializeObject<AvaSettings>(File.ReadAllText(avaJson));
                    return settings != null;
                }
                catch
                {
                    // TODO
                }
            }

            return false;
        }
    }
}