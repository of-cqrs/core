using System.Threading.Tasks;
using CQRS.Core.Models;

namespace CQRS.Core.Commands
{
    public interface ICommandDispatcher
    {
        TResult Dispatch<TCommand, TResult>(TCommand command) where TCommand : ActionBase where TResult : ActionResult;
        Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command) where TCommand : ActionBase where TResult : ActionResult;
    }
}