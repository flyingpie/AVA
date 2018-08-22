using AVA.Core;
using AVA.Core.QueryExecutors.ListQuery;
using MoreLinq;
using MUI;
using MUI.Win32.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace AVA.Plugins.Cd
{
    public class CdQueryExecutor : ListQueryExecutor
    {
        private static readonly Regex IsDriveLetterRegex = new Regex(@"^[A-Za-z]:\\");

        // Settings
        private static readonly string DefaultDrive = "C:";

        private static bool IncludeHiddenFiles = true;

        private QueryContext _qc; // TODO: Make nicer
        private Stack<string> _last = new Stack<string>();
        private FileSystemInfo _selected;

        public override IEnumerable<IListQueryResult> GetQueryResults(string term)
        {
            term = term.Replace("/", "\\");

            if (term.StartsWith("\\"))
            {
                term = DefaultDrive + term;
            }

            if (IsDriveLetterRegex.IsMatch(term) && System.Uri.TryCreate(term, UriKind.RelativeOrAbsolute, out var uri) && uri.Segments.Length >= 2)
            {
                var segments = uri.Segments
                    .Skip(1)
                    .Where(s => s.EndsWith("/"))
                    .Select(s => WebUtility.UrlDecode(s))
                    .ToList();

                var pattern = uri.Segments.Last();
                if (pattern.EndsWith("/")) pattern = string.Empty;

                var items = FindFileSystemItems(string.Join("", segments), pattern);

                _selected = items.FirstOrDefault();

                return items.Select(item => new ListQueryResult()
                {
                    Icon = ResourceManager.Instance.LoadImageFromIcon(item.FullName),
                    Name = item.Name,
                    Description = item.FullName,
                    OnExecute = t => Process.Start(item.FullName).Dispose(),
                    Mode = ListMode.Small
                });
            }

            return Enumerable.Empty<IListQueryResult>();
        }

        private static IEnumerable<FileSystemInfo> FindFileSystemItems(string path, string pattern)
        {
            var wildCarded = $"*{pattern}*";
            var dirInfo = new DirectoryInfo(path);

            if (dirInfo.Exists)
            {
                var files = dirInfo
                    .GetFiles(wildCarded, SearchOption.TopDirectoryOnly)
                    .Where(f => IncludeHiddenFiles || !f.Attributes.HasFlag(FileAttributes.Hidden))
                    .OrderBy(f => !f.Name.ToLowerInvariant().StartsWith(pattern.ToLowerInvariant()))
                    .ThenBy(f => f.Name)
                    .Take(25)
                    .Cast<FileSystemInfo>()
                ;

                var dirs = dirInfo
                    .GetDirectories(wildCarded, SearchOption.TopDirectoryOnly)
                    .Where(f => IncludeHiddenFiles || !f.Attributes.HasFlag(FileAttributes.Hidden))
                    .OrderBy(f => !f.Name.ToLowerInvariant().StartsWith(pattern.ToLowerInvariant()))
                    .ThenBy(f => f.Name)
                    .Take(25)
                    .Cast<FileSystemInfo>()
                ;

                return dirs.Concat(files);
            }

            return Enumerable.Empty<FileSystemInfo>();
        }

        public override void Draw()
        {
            if (Input.IsKeyPressed(Keys.Tab) && _selected != null)
            {
                if (Input.IsControlDown && _last.Any())
                {
                    _qc.Text = _last.Pop();
                }
                else
                {
                    _last.Push(_qc.Text);

                    _qc.Text = _selected.FullName + (_selected.Attributes == FileAttributes.Directory ? "\\" : "");
                }

                _qc.Focus = true;
            }

            base.Draw();
        }

        public override bool TryHandle(QueryContext query)
        {
            _qc = query;

            return base.TryHandle(query);
        }
    }
}