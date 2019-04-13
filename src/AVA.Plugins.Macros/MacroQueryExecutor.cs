using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.ListQuery;
using FontAwesomeCS;
using MUI.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AVA.Plugins.UnitConverter
{
    [Help(Name = "Macros", Description = "Executes commands based on short combinations of letters", ExampleUsage = "37c to f", Icon = FAIcon.ThermometerEmptySolid)]
    public class MacroQueryExecutor : ListQueryExecutor
    {
        public override int Order => 0;

        private List<Macro> _macros = new List<Macro>();
        private ILog _log;

        public override void Initialize()
        {
            _log = Log.Get(this);

            var convertersFile = "macros.json".FromAppRoot();

            try
            {
                var convertersFileContent = File.ReadAllText(convertersFile);

                _macros = JsonConvert.DeserializeObject<List<Macro>>(convertersFileContent);
            }
            catch (Exception ex)
            {
                _log.Error($"Could not load macros from file '{convertersFile}': '{ex.Message}'");
            }

            _macros = new List<Macro>()
            {
                new StartProgramMacro()
                {
                    Command = "vsc",
                    FileName = "code",

                    Name = "Visual Studio Code",
                    Description = ""
                },
                new StartProgramMacro()
                {
                    Command = "rrd",
                    FileName = "https://www.reddit.com",

                    Name = "Reddit",
                    Description = ""
                }
            };
        }

        public override IEnumerable<IListQueryResult> GetQueryResults(string term)
        {
            return _macros
                .Where(m => m.Command.Equals(term, StringComparison.OrdinalIgnoreCase))
                .Select(m => new ListQueryResult()
                {
                    Name = m.Name,
                    Description = m.Description,
                    Icon = m.GetIcon(),
                    OnExecute = qc => m.Execute()
                })
                .ToList()
            ;
        }
    }
}