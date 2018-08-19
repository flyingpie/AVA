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
using System.Text.RegularExpressions;

namespace AVA.Plugins.Cd
{
    public class CdQueryExecutor : ListQueryExecutor
    {
        private static readonly Regex IsDriveLetterRegex = new Regex(@"^[A-Za-z]:\\");

        private static readonly string DefaultDrive = "C:";

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
                var segments = uri.Segments.ToList().Skip(1).Where(s => s.EndsWith("/")).ToList();

                var pattern = uri.Segments.Last();
                if (pattern.EndsWith("/")) pattern = string.Empty;
                pattern = $"{pattern}*";

                var currentRoot = new DirectoryInfo(string.Join("", segments));

                if (currentRoot.Exists)
                {
                    var files = currentRoot.GetFiles(pattern, SearchOption.TopDirectoryOnly).Take(10).Cast<FileSystemInfo>();
                    var dirs = currentRoot.GetDirectories(pattern, SearchOption.TopDirectoryOnly).Take(10).Cast<FileSystemInfo>();
                    var items = dirs.Concat(files);

                    _selected = items.FirstOrDefault();

                    return items.Select(item => new ListQueryResult()
                    {
                        Icon = ResourceManager.Instance.LoadImageFromIcon(item.FullName),
                        Name = item.Name,
                        Description = item.FullName,
                        OnExecute = t => Process.Start(item.FullName).Dispose()
                    });
                }
            }

            return Enumerable.Empty<IListQueryResult>();
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

                    _qc.Text = _selected.FullName + "\\";
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