using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using MUI.DI;
using MUI.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Diag = System.Diagnostics;

namespace AVA.Indexing
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

        private ILog _log = Log.Get<Indexer>();

        public Indexer()
        {
            _directory = FSDirectory.Open($@"index\{Environment.MachineName.ToLowerInvariant()}");
            Open();
        }

        private void Open()
        {
            var sw = new Diag.Stopwatch();
            sw.Start();

            var stopWords = new Lucene.Net.Analysis.Util.CharArraySet(LuceneVersion, 0, false);

            _analyzer = new StandardAnalyzer(LuceneVersion, stopWords);

            _indexWriter = new IndexWriter(_directory, new IndexWriterConfig(LuceneVersion, _analyzer));
            _indexWriter.Commit();

            _indexReader = DirectoryReader.Open(_directory);

            _indexSearcher = new IndexSearcher(_indexReader);

            _queryParser = new QueryParser(LuceneVersion, "filename", _analyzer);

            _log.Info($"Opened index in {sw.Elapsed}");
        }

        private void Close()
        {
            var sw = new Diag.Stopwatch();
            sw.Start();

            _analyzer?.Dispose();
            _analyzer = null;

            _indexWriter?.Commit();
            _indexWriter?.Dispose();
            _indexWriter = null;

            _indexReader?.Dispose();
            _indexReader = null;

            _indexSearcher = null;

            _log.Info($"Closed index in {sw.Elapsed}");
        }

        public void Rebuild()
        {
            _log.Info("Rebuilding index...");

            Close();

            _directory.ListAll().ToList().ForEach(f => _directory.DeleteFile(f));

            Open();

            var sww = new Diag.Stopwatch();
            sww.Start();

            var folders = new[]
            {
                @"%ProgramData%\Microsoft\Windows\Start Menu\Programs",
                @"%APPDATA%\Microsoft\Windows\Start Menu",
                @"%NEXTCLOUD%"
            }.Select(f => Environment.ExpandEnvironmentVariables(f)).ToList();

            var files = folders.GetFilesRecursive();

            foreach (var path in files)
            {
                var filename = System.IO.Path.GetFileName(path);

                var fileName_l = System.IO.Path.GetFileName(path).ToLowerInvariant();
                var fileNameNoExt_l = System.IO.Path.GetFileNameWithoutExtension(fileName_l).ToLowerInvariant();
                var ext_l = System.IO.Path.GetExtension(fileName_l).ToLowerInvariant();

                var doc = new Document();

                doc.AddStringField("filename", filename, Field.Store.YES);

                doc.AddTextField("filename_l", fileName_l, Field.Store.YES);
                doc.AddStringField("filename_l.keyword", fileName_l, Field.Store.YES);

                doc.AddTextField("filename_l_no-ext", fileNameNoExt_l, Field.Store.YES);
                doc.AddStringField("filename_l_no-ext.keyword", fileNameNoExt_l, Field.Store.YES);

                doc.AddTextField("path", path, Field.Store.YES);
                doc.AddStringField("path.keyword", path, Field.Store.YES);

                doc.AddStringField("ext_l", ext_l, Field.Store.YES);

                _indexWriter.AddDocument(doc);
            }

            _indexWriter.Commit();
            _indexWriter.Flush(true, true);

            Close();
            Open();

            sww.Stop();

            _log.Info($"Indexed {files.Count} documents in {sww.Elapsed}");
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
                _log.Info($"Term: '{term}'");

                var sw = new Diag.Stopwatch();
                sw.Start();

                var docs = Query(term);

                sw.Stop();

                foreach (var doc in docs)
                {
                    _log.Info($"{doc.ScoreDoc.Score} {doc.Document.Fields.First(f => f.Name == "path").GetStringValue()}");
                }

                _log.Info(sw.Elapsed.ToString());
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
            // "tail" -> "TailBlazer" (1)

            // Explain
            /////////

            var bq = new BooleanQuery();
            // Exact match
            //bq.Add(new BooleanClause(new TermQuery(new Term("filename.keyword", term)) { Boost = 8 }, Occur.SHOULD));
            //bq.Add(new BooleanClause(new TermQuery(new Term("filename", term)) { Boost = 1 }, Occur.SHOULD));

            //bq.Add(new BooleanClause(new TermQuery(new Term("filename_no-ext.keyword", term)) { Boost = 8 }, Occur.SHOULD));
            //bq.Add(new BooleanClause(new TermQuery(new Term("filename_no-ext", term)) { Boost = 1 }, Occur.SHOULD));

            {
                var b1 = new BooleanQuery();

                // Starts with
                b1.Add(new BooleanClause(new PrefixQuery(new Term("filename_l.keyword", term)) { Boost = 6 }, Occur.SHOULD));

                // Contains
                b1.Add(new BooleanClause(new WildcardQuery(new Term("filename_l", $"*{term}*")) { Boost = 4 }, Occur.SHOULD));

                bq.Add(new BooleanClause(b1, Occur.MUST));
            }

            {
                // Prefer .exe
                bq.Add(new BooleanClause(new TermQuery(new Term("ext_l", ".exe")) { Boost = 16 }, Occur.SHOULD));

                // And don't mind .lnk
                bq.Add(new BooleanClause(new TermQuery(new Term("ext_l", ".lnk")) { Boost = 10 }, Occur.SHOULD));
            }

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