using MUI;
using MUI.DI;
using System;
using System.Linq;

namespace MLaunch.Core.QueryExecutors
{
    [Service]
    public class QueryExecutorManager : IQueryExecutorManager
    {
        [Dependency] public ResourceManager ResourceManager { get; set; }

        [Dependency] public IQueryExecutor[] QueryExecutors { get; set; }

        [RunAfterInject]
        private void Init()
        {
            QueryExecutors = QueryExecutors.OrderBy(qe => qe.Order).ToArray();
        }

        public IQueryExecutor GetQueryExecutor(string term)
        {
            var qex = QueryExecutors.FirstOrDefault(qe => qe.TryHandle(term));

            Console.WriteLine(qex.ToString());

            return qex;
        }
    }
}