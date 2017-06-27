using System.Threading.Tasks;
using CQRS.Core.Models;

namespace CQRS.Core.Queries
{
    public interface IQueryHandler<in TQuery, out TResult>
    {
        TResult Retrieve(TQuery query);
    }

    public interface IAsyncQueryHandler<in TQuery, TResult> where TQuery : ActionBase where TResult : ActionResult
    {
        Task<TResult> Retrieve(TQuery query);
    }

    public abstract class AsyncQueryHander : IAsyncQueryHandler<ActionBase, ActionResult>
    {
        public Task<ActionResult> Retrieve(ActionBase query)
        {
            throw new System.NotImplementedException();
        }
    }
}