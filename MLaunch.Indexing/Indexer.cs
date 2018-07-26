using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using MUI.DI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MLaunch.Indexing
{
    [Service(ServiceLifetime.Singleton)]
    public class Indexer
    {
        public static readonly LuceneVersion LuceneVersion = LuceneVersion.LUCENE_48;

        private Directory _directory;
        private Analyzer _analyzer;

        private IndexReader _indexReader;
        private IndexWriter _indexWriter;

        private IndexSearcher _indexSearcher;

        private QueryParser _queryParser;

        public Indexer()
        {
            _directory = FSDirectory.Open("index");
            Open();
        }

        private void Open()
        {
            var sw = new Stopwatch();
            sw.Start();

            _analyzer = new StandardAnalyzer(LuceneVersion);

            _indexWriter = new IndexWriter(_directory, new IndexWriterConfig(LuceneVersion, _analyzer));
            _indexWriter.Commit();

            _indexReader = DirectoryReader.Open(_directory);

            _indexSearcher = new IndexSearcher(_indexReader);

            _queryParser = new QueryParser(LuceneVersion, "filename", _analyzer);

            Console.WriteLine($"Opened index in {sw.Elapsed}");
        }

        private void Close()
        {
            var sw = new Stopwatch();
            sw.Start();

            _analyzer?.Dispose();
            _analyzer = null;

            _indexWriter?.Commit();
            _indexWriter?.Dispose();
            _indexWriter = null;

            _indexReader?.Dispose();
            _indexReader = null;

            _indexSearcher = null;

            Console.WriteLine($"Closed index in {sw.Elapsed}");
        }

        public void Rebuild()
        {
            Console.WriteLine("Rebuilding index...");

            Close();

            _directory.ListAll().ToList().ForEach(f => _directory.DeleteFile(f));

            Open();

            var sww = new Stopwatch();
            sww.Start();

            var folders = new[]
            {
                @"%ProgramData%\Microsoft\Windows\Start Menu\Programs",
                @"%APPDATA%\Microsoft\Windows\Start Menu",
                @"%NEXTCLOUD%"
            }.Select(f => Environment.ExpandEnvironmentVariables(f)).ToList();

            //var files = folders.Select(folder = > System.IO.Directory.GetFiles(, "*", System.IO.SearchOption.AllDirectories);

            var files = folders.GetFilesRecursive();

            foreach (var path in files)
            {
                var fileName = System.IO.Path.GetFileName(path).ToLowerInvariant();
                var fileNameNoExt = System.IO.Path.GetFileNameWithoutExtension(fileName);
                var ext = System.IO.Path.GetExtension(fileName);

                var doc = new Document();

                doc.AddTextField("filename", fileName, Field.Store.YES);
                doc.AddStringField("filename.keyword", fileName, Field.Store.YES);

                doc.AddTextField("filename_no-ext", fileNameNoExt, Field.Store.YES);
                doc.AddStringField("filename_no-ext.keyword", fileNameNoExt, Field.Store.YES);

                doc.AddTextField("path", path, Field.Store.YES);
                doc.AddStringField("path.keyword", path, Field.Store.YES);

                doc.AddStringField("ext", ext, Field.Store.YES);

                _indexWriter.AddDocument(doc);
            }

            _indexWriter.Commit();
            _indexWriter.Flush(true, true);

            Close();
            Open();

            sww.Stop();

            Console.WriteLine($"Indexed {files.Count} documents in {sww.Elapsed}");
        }

        public void SearchRepl()
        {
            // Pry query
            Query("conem");

            var term = "blaz";

            do
            {
                Console.Write("> ");
                term = Console.ReadLine();
                Console.Clear();
                Console.WriteLine($"Term: '{term}'");

                var sw = new Stopwatch();
                sw.Start();

                var docs = Query(term);

                sw.Stop();

                foreach (var doc in docs)
                {
                    Console.WriteLine($"{doc.ScoreDoc.Score} {doc.Document.Fields.First(f => f.Name == "path").GetStringValue()}");
                }

                Console.WriteLine(sw.Elapsed.ToString());
            }
            while (!string.IsNullOrWhiteSpace(term));

            Console.ReadLine();
        }

        public List<QS> Query(string term)
        {
            // TODO: Can we use an analyzer for this?
            term = term.ToLowerInvariant();

            // TESTS
            // "code" -> Visual Studio Code.lnk (1 - 3)
            // "conem" => ConEmu.exe (1)
            // "remote" -> "Remote Desktop Connection", "mRemoteNG" (1, 2)

            // Explain
            /////////

            var bq = new BooleanQuery();
            // Exact match
            bq.Add(new BooleanClause(new TermQuery(new Term("filename.keyword", term)) { Boost = 8 }, Occur.SHOULD));
            bq.Add(new BooleanClause(new TermQuery(new Term("filename", term)) { Boost = 1 }, Occur.SHOULD));

            bq.Add(new BooleanClause(new TermQuery(new Term("filename_no-ext.keyword", term)) { Boost = 8 }, Occur.SHOULD));
            bq.Add(new BooleanClause(new TermQuery(new Term("filename_no-ext", term)) { Boost = 1 }, Occur.SHOULD));

            // Starts with
            bq.Add(new BooleanClause(new PrefixQuery(new Term("filename.keyword", term)) { Boost = 3 }, Occur.SHOULD));
            //bq.Add(new BooleanClause(new PrefixQuery(new Term("filename", term)) { Boost = 3 }, Occur.SHOULD));

            // General query
            //bq.Add(new BooleanClause(_queryParser.Parse(term), Occur.SHOULD));

            // Contains
            bq.Add(new BooleanClause(new WildcardQuery(new Term("filename", $"*{term}*")) { Boost = 8 }, Occur.SHOULD));

            // Prefer .exe
            bq.Add(new BooleanClause(new TermQuery(new Term("ext", ".exe")) { Boost = 12 }, Occur.SHOULD));

            // And don't mind .lnk
            bq.Add(new BooleanClause(new TermQuery(new Term("ext", ".lnk")) { Boost = 10 }, Occur.SHOULD));

            // Fuzzy
            //bq.Add(new BooleanClause(new FuzzyQuery(new Term("filename", term)), Occur.SHOULD));

            var hits = _indexSearcher.Search(bq, 15);
            var docs = hits.ScoreDocs
                .Select(sd => new QS { ScoreDoc = sd, Document = _indexSearcher.Doc(sd.Doc) })
                //.Where(sd => sd.ScoreDoc.Score >= 0.003f)
                .ToList();

            //System.IO.Directory.CreateDirectory("explain");
            //System.IO.Directory.GetFiles("explain").ToList().ForEach(f => System.IO.File.Delete(f));

            //for(int i = 0; i < docs.Count; i++)
            //{
            //    var doc = docs[i];

            //    var d0 = _indexSearcher.Explain(bq, doc.ScoreDoc.Doc);

            //    var html = $"{doc.ScoreDoc.Score}: {doc.Document.Fields.First().GetStringValue()}{Environment.NewLine}{d0.ToHtml()}";

            //    System.IO.File.WriteAllText($@"explain\{i.ToString("D4")}.html", html);
            //}

            return docs;
        }
    }

    public class QS
    {
        public Document Document { get; set; }

        public ScoreDoc ScoreDoc { get; set; }
    }

    public static class Extensions
    {
        public static List<string> GetFilesRecursive(this IEnumerable<string> folders, List<string> files = null)
        {
            files = files ?? new List<string>();

            foreach (var folder in folders)
            {
                try
                {
                    foreach (var app in System.IO.Directory.GetFiles(folder, "*", System.IO.SearchOption.TopDirectoryOnly))
                    {
                        files.Add(app);
                    }

                    var dd = System.IO.Directory.GetDirectories(folder);

                    GetFilesRecursive(dd, files);
                }
                catch { }
            }

            return files;
        }
    }
}