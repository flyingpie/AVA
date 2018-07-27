using ImGuiNET;
using MLaunch.Core;
using MLaunch.Core.QueryExecutors;
using MUI.DI;
using System;

namespace MLaunch.Plugins.FuzzyFileSystemBrowser
{
    [Service]
    public class FuzzyFileSystemBrowserQueryExecutor : IQueryExecutor
    {
        public string Name => "Fuzzy file system browser";

        public string Description => "C:/winows/sytem23/divers/ect/hots";

        public string ExampleUsage => "^^";

        public int Order => 0;

        private System.Uri _uri;

        public bool TryHandle(string term)
        {
            return System.Uri.TryCreate(term, UriKind.Absolute, out _uri) && _uri.IsFile;
        }

        public bool TryExecute(QueryContext query)
        {
            throw new NotImplementedException();
        }

        public void Draw()
        {
            ImGui.Text("URI: " + _uri);
        }
    }
}