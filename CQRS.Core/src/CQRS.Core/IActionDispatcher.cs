using System.Threading.Tasks;
using CQRS.Core.Models;

namespace CQRS.Core
{
    public interface IActionDispatcher
    {
        Task<TResult> QueryAsync<TQuery, TResult>(TQuery query) where TResult : ActionResult where TQuery : ActionBase;
        TResult Query<TQuery, TResult>(TQuery query) where TResult : ActionResult where TQuery : ActionBase;
        Task<TCommandResult> RunAsync<TCommand, TCommandResult>(TCommand command) where TCommandResult : ActionResult where TCommand : ActionBase;
        TCommandResult Run<TCommand, TCommandResult>(TCommand command) where TCommandResult : ActionResult where TCommand : ActionBase;
    }
}