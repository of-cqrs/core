using CQRS.Core.Models;
using CQRS.Core.Queries;

namespace CQRS.Core.Provider.Interfaces
{
    public interface IQueryProvider
    {
        T GetQuery<T, TQuery, TResult>() where T : IQueryHandler<TQuery, TResult>;
        T GetAsyncQuery<T, TQuery, TResult>() where T : IAsyncQueryHandler<TQuery, TResult> where TQuery : ActionBase where TResult : ActionResult;
    }
}