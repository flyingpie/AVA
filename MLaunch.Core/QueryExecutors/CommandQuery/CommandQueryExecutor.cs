using MUI.DI;
using System.Linq;

namespace MLaunch.Core.QueryExecutors.CommandQuery
{
    [Service]
    public abstract class CommandQueryExecutor : IQueryExecutor
    {
        public abstract string[] CommandPrefixes { get; }

        public abstract string Description { get; }

        public abstract string Name { get; }

        public abstract string ExampleUsage { get; }

        public virtual int Order => 0;

        public abstract void Draw();

        public virtual bool TryExecute(string term) => false;

        public virtual bool TryHandle(string term)
        {
            term = term.ToLowerInvariant();

            return CommandPrefixes.Any(cp => term.StartsWith(cp));
        }
    }
}