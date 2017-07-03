using System.Threading.Tasks;
using CQRS.Core.Models;

namespace CQRS.Core
{
    public interface IActionFacade
    {
        Task<TResult> QueryAsync<TQuery, TResult>(TQuery query) where TResult : ActionResult;
        TResult Query<TQuery, TResult>(TQuery query) where TResult : ActionResult;
        Task<TCommandResult> RunAsync<TCommand, TCommandResult>(TCommand command) where TCommandResult : ActionResult;
        TCommandResult Run<TCommand, TCommandResult>(TCommand command) where TCommandResult : ActionResult;
    }
}