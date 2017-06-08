using System.Threading.Tasks;

namespace CQRS.Core.Queries
{
    public interface IQueryHandler<in TQuery, out TResult>
    {
        TResult Retrieve(TQuery query);
    }

    public interface IAsyncQueryHandler<in TQuery, TResult>
    {
        Task<TResult> Retrieve(TQuery query);
    }
}