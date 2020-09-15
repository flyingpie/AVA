using System;
using System.Threading.Tasks;

namespace AVA.Core.QueryExecutors
{
	internal class SynchronousQueryExecutor : IAsyncQueryExecutor
	{
		public int Order => _executor.Order;

		private IQueryExecutor _executor;

		public SynchronousQueryExecutor(IQueryExecutor executor)
		{
			_executor = executor ?? throw new ArgumentNullException(nameof(executor));
		}

		public bool TryHandle(QueryContext query) => _executor.TryHandle(query);

		public Task<bool> TryExecuteAsync(QueryContext query) => Task.FromResult(_executor.TryExecute(query));

		public void Draw() => _executor.Draw();

		public override string ToString() => _executor.ToString();
	}
}