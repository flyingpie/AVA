using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.ListQuery;
using FontAwesomeCS;
using MUI.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AVA.Plugins.UnitConverter
{
    [Help(Name = "Unit converter", Description = "Converts values from one unit to another", ExampleUsage = "37c to f", Icon = FAIcon.ThermometerEmptySolid)]
    public class UnitConverterQueryExecutor : ListQueryExecutor
    {
        public static readonly Regex UnitRegex = new Regex(@"^(?<value>[0-9\.-]*) ?(?<unitFrom>[A-z]*)( to )?(?<unitTo>[A-z]*)?");

        public override int Order => 0;

        private List<Converter> _converters = new List<Converter>();
        private ILog _log;

        public override void Initialize()
        {
            _log = Log.Get(this);

            var convertersFile = "converters.json".FromPluginRoot(typeof(UnitConverterQueryExecutor));

            try
            {
                var convertersFileContent = File.ReadAllText(convertersFile);

                _converters = JsonConvert.DeserializeObject<List<Converter>>(convertersFileContent);
            }
            catch (Exception ex)
            {
                _log.Error($"Could not load converters from file '{convertersFile}': '{ex.Message}'");
            }
        }

        public override IEnumerable<IListQueryResult> GetQueryResults(string term)
        {
            var match = UnitRegex.Match(term);
            if (!match.Success) return Enumerable.Empty<IListQueryResult>();

            var val = float.Parse(match.Groups["value"].Value);
            var idFrom = match.Groups["unitFrom"];
            var idTo = match.Groups["unitTo"];

            var conv = _converters
                .Where(c => c.HasUnitFrom(idFrom.Value))
                .Where(c => (!idTo.Success || string.IsNullOrWhiteSpace(idTo.Value)) || c.HasUnitTo(idTo.Value))
            ;

            if (!conv.Any()) return Enumerable.Empty<IListQueryResult>();

            return conv
                .Select(c => new ListQueryResult()
                {
                    Name = string.Format(c.Format, val, c.Convert(val)),
                    Description = c.Name
                })
                .ToList();
        }
    }
}