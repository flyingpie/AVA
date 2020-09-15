using MUI.DI;
using System.Linq;

namespace AVA.Core.QueryExecutors.CommandQuery
{
	[Service]
	public abstract class CommandQueryExecutor : IQueryExecutor
	{
		public abstract string[] CommandPrefixes { get; }

		public virtual int Order => 0;

		public abstract void Draw();

		public virtual bool TryExecute(QueryContext query) => false;

		public virtual bool TryHandle(QueryContext query)
		{
			var term = query.Text.ToLowerInvariant();

			return CommandPrefixes.Any(cp => term.StartsWith(cp));
		}
	}
}