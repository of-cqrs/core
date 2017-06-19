using System.Threading.Tasks;
using CQRS.Core.Provider.Interfaces;

namespace CQRS.Core.Queries
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IQueryProvider _queryProvider;

        public QueryDispatcher(IQueryProvider queryProvider)
        {
            _queryProvider = queryProvider;
        }

        public TResult Dispatch<TQuery, TResult>(TQuery query)
        {
            var handler = _queryProvider.GetQuery<IQueryHandler<TQuery, TResult>, TQuery, TResult>();
            return handler.Retrieve(query);
        }

        public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query)
        {
            var handler = _queryProvider.GetAsyncQuery<IAsyncQueryHandler<TQuery, TResult>, TQuery, TResult>();
            return await handler.Retrieve(query);
        }
    }
}