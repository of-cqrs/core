using System.Threading.Tasks;

namespace CQRS.Core.Queries
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TQuery, TResult>(TQuery query);
        Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query);
    }
}