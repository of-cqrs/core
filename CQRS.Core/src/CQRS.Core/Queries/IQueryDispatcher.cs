using System;
using System.Threading.Tasks;
using CQRS.Core.Models;

namespace CQRS.Core.Queries
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TQuery, TResult>(TQuery query);
        Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query);

        Task<object> DispatchAsync(ActionContextBase contextBase);
    }
}