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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diag = System.Diagnostics;

namespace AVA.Indexing
{
    [Service(ServiceLifetime.Singleton)]
    public class Indexer
    {
        public static readonly LuceneVersion LuceneVersion = LuceneVersion.LUCENE_48;

        [Dependency] public IIndexerSource[] IndexerSources { get; set; }

        private Directory _directory;
        private Analyzer _analyzer;

        private IndexReader _indexReader;
        private IndexWriter _indexWriter;

        private IndexSearcher _indexSearcher;

        private QueryParser _queryParser;

        private ILog _log = Log.Get<Indexer>();

        private Dictionary<string, Type> _indexedItemTypes;

        public Indexer()
        {
            _directory = FSDirectory.Open($@"index\{Environment.MachineName.ToLowerInvariant()}".FromAppRoot());
            Open();
        }

        [RunAfterInject]
        public void Init()
        {
            _indexedItemTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(IndexedItem).IsAssignableFrom(t))
                .ToDictionary(t => t.FullName, t => t)
            ;
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

        public Task RebuildAsync()
        {
            return Task.Run(() =>
            {
                _log.Info("Rebuilding index...");

                Close();

                _directory.ListAll().ToList().ForEach(f => _directory.DeleteFile(f));

                Open();

                var sw = new Diag.Stopwatch();
                sw.Start();

                var count = 0;
                foreach (var item in IndexerSources.SelectMany(s => s.GetItems()))
                {
                    count++;

                    var doc = new Document();

                    var name = item.Name.ToLowerInvariant();
                    var ext = item.Extension?.ToLowerInvariant();

                    var type = item.GetType().FullName;
                    var obj = JsonConvert.SerializeObject(item);

                    doc.AddTextField("name", name, Field.Store.YES);
                    doc.AddStringField("name.keyword", name, Field.Store.YES);

                    if (!string.IsNullOrWhiteSpace(ext))
                        doc.AddStringField("ext", ext, Field.Store.YES);

                    doc.AddStringField("obj_type", type, Field.Store.YES);
                    doc.AddStringField("obj", obj, Field.Store.YES);

                    _indexWriter.AddDocument(doc);
                }

                _indexWriter.Commit();
                _indexWriter.Flush(true, true);

                Close();
                Open();

                sw.Stop();

                _log.Info($"Indexed {count} documents in {sw.Elapsed}");
            });
        }

        public List<IndexedItem> Query(string term)
        {
            // TODO: Can we use an analyzer for this?
            term = term.ToLowerInvariant();

            var query = new BooleanQuery();

            // Name
            {
                var nameMatch = new BooleanQuery();

                // Starts with
                nameMatch.Add(new BooleanClause(new PrefixQuery(new Term("name.keyword", term)) { Boost = 6 }, Occur.SHOULD));

                // Contains
                nameMatch.Add(new BooleanClause(new WildcardQuery(new Term("name", $"*{term}*")) { Boost = 4 }, Occur.SHOULD));

                query.Add(new BooleanClause(nameMatch, Occur.MUST));
            }

            // Type (extension)
            {
                // Prefer .exe
                query.Add(new BooleanClause(new TermQuery(new Term("ext", ".exe")) { Boost = 16 }, Occur.SHOULD));

                // And don't mind .lnk
                query.Add(new BooleanClause(new TermQuery(new Term("ext", ".lnk")) { Boost = 10 }, Occur.SHOULD));
            }

            var hits = _indexSearcher.Search(query, 15);
            var docs = hits.ScoreDocs
                .Select(sd =>
                {
                    var d = _indexSearcher.Doc(sd.Doc);

                    var type = d.Get("obj_type");
                    var obj = d.Get("obj");

                    if (!_indexedItemTypes.ContainsKey(type)) return null;

                    var tt = _indexedItemTypes[type];
                    var ii = JsonConvert.DeserializeObject(obj, tt);

                    var item = ii as IndexedItem;

                    if (item == null) return null;

                    item.Id = sd.Doc;
                    item.Score = sd.Score;

                    return item;
                })
                .Where(ii => ii != null)
                .OrderByDescending(ii => ii.Score)
                .ThenBy(ii => ii.Name)
                .ToList();

            return docs;
        }
    }
}