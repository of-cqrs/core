using System.Threading.Tasks;
using CQRS.Core.Models;

namespace CQRS.Core.Commands
{

    public interface IAsyncCommandHandler<TCommand, TResult> where TCommand : ActionBase where TResult : ActionResult
    {
        Task<TResult> ExecuteAsync(TCommand command);
    }

    public interface ICommandHandler<TCommand, TResult> where TCommand : ActionBase where TResult : ActionResult
    {
        TResult Execute(TCommand command);
    }
}