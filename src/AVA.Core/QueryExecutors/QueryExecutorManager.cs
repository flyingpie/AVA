using MUI.DI;
using MUI.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AVA.Core.QueryExecutors
{
	[Service]
	public class QueryExecutorManager : IQueryExecutorManager
	{
		[Dependency] public IQueryExecutor[] QueryExecutors { get; set; }

		[Dependency] public IAsyncQueryExecutor[] AsyncQueryExecutors { get; set; }

		private ILog _log;
		private List<IAsyncQueryExecutor> _queryExecutors;
		private IAsyncQueryExecutor _activeQueryExecutor;

		[RunAfterInject]
		private void Init()
		{
			_log = Log.Get(this);

			_queryExecutors = new List<IAsyncQueryExecutor>();
			_queryExecutors.AddRange(QueryExecutors.Select(qe => new SynchronousQueryExecutor(qe)));
			_queryExecutors.AddRange(AsyncQueryExecutors);

			_queryExecutors = _queryExecutors.OrderBy(qe => qe.Order).ToList();
		}

		public bool TryHandle(QueryContext query)
		{
			_activeQueryExecutor = _queryExecutors.FirstOrDefault(qe =>
			{
				try
				{
					return qe.TryHandle(query);
				}
				catch (Exception ex)
				{
					_log.Error($"Error while calling 'TryHandle' on query executor '{qe}': '{ex.Message}'", ex);
				}

				return false;
			});

			_log.Info($"Term '{query}', selected query executor: {_activeQueryExecutor}");

			return _activeQueryExecutor != null;
		}

		public bool TryExecuteAsync(QueryContext query)
		{
			try
			{
				return _activeQueryExecutor?.TryExecuteAsync(query) ?? false;//?? Task.FromResult(false);
			}
			catch (Exception ex)
			{
				_log.Error($"Error while calling 'TryExecute' on query executor '{_activeQueryExecutor}': '{ex.Message}'", ex);
			}

			//return Task.FromResult(false);
			return false;
		}

		public void Draw()
		{
			_activeQueryExecutor?.Draw();
		}
	}
}