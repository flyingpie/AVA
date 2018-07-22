using MLaunch.Core.QueryExecutors.ListQuery;
using MUI;
using MUI.DI;
using MUI.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MLaunch.Plugins.EnvVars
{
    public class EnvironmentVariablesQueryExecutor : ListQueryExecutor
    {
        [Dependency]
        private ResourceManager ResourceManager { get; set; }

        private List<EnvironmentVariable> _vars;
        private Image _icon;

        public override int Order => 0;

        public override string Prefix => "env";

        [RunAfterInject]
        private void Init()
        {
            _icon = ResourceManager.LoadImage(@"Resources\Images\crashlog-doom.png");

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

        public override IList<IListQueryResult> GetQueryResults(string term)
        {
            var split = term.Split(new[] { ' ' });
            var filter = split.Length > 1 ? split[1] : "";

            return _vars
                .Where(env => env.Scope.ContainsCaseInsensitive(filter) || env.Name.ContainsCaseInsensitive(filter) || env.Value.ContainsCaseInsensitive(filter))
                .Select(env => (IListQueryResult)new ListQueryResult()
                {
                    Name = $"{env.Scope} - {env.Name}",
                    Description = env.Value,
                    Icon = _icon
                })
                .ToList();
        }

        public class EnvironmentVariable
        {
            public string Scope { get; set; }

            public string Name { get; set; }

            public string Value { get; set; }
        }
    }
}