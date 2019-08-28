using AVA.Core.QueryExecutors;
using AVA.Core.QueryExecutors.ListQuery;
using MUI;
using MUI.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using FontAwesomeCS;

namespace AVA.Plugin.EnvironmentVariables
{
    [Help(Name = "Environment variables", Description = "List and filters through environment variables", ExampleUsage = "env path", Icon = FAIcon.AlignJustifySolid)]
    public class EnvironmentVariablesQueryExecutor : ListQueryExecutor
    {
        [Dependency]
        public ResourceManager ResourceManager { get; set; }

        public override int Order => 0;

        public override string Prefix => "env ";

        private List<EnvironmentVariable> _vars;

        [RunAfterInject]
        private void Init()
        {
            // Load environment variables
            _vars = new List<EnvironmentVariable>();

            _vars.AddRange(GetEnvironmentVariablesFor(EnvironmentVariableTarget.Machine));
            _vars.AddRange(GetEnvironmentVariablesFor(EnvironmentVariableTarget.User));
        }

        private IEnumerable<EnvironmentVariable> GetEnvironmentVariablesFor(EnvironmentVariableTarget target)
        {
            var envVars = Environment.GetEnvironmentVariables(target);

            foreach (var key in envVars.Keys)
            {
                yield return new EnvironmentVariable()
                {
                    Scope = target.ToString(),
                    Name = key.ToString(),
                    Value = envVars[key].ToString()
                };
            }
        }

        public override IEnumerable<IListQueryResult> GetQueryResults(string term)
        {
            var split = term.Split(new[] { ' ' });
            var filter = split.Length > 1 ? split[1] : "";

            return _vars
                .Where(env => env.Scope.ContainsCaseInsensitive(filter) || env.Name.ContainsCaseInsensitive(filter) || env.Value.ContainsCaseInsensitive(filter))
                .Select(env => (IListQueryResult)new ListQueryResult()
                {
                    Name = $"{env.Scope} - {env.Name}",
                    Description = env.Value
                });
        }

        public class EnvironmentVariable
        {
            public string Scope { get; set; }

            public string Name { get; set; }

            public string Value { get; set; }
        }
    }
}