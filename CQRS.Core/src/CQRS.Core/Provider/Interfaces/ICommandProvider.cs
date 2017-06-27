using CQRS.Core.Commands;
using CQRS.Core.Models;

namespace CQRS.Core.Provider.Interfaces
{
    public interface ICommandProvider
    {
        T GetAsyncCommand<T, TCommand, TResult>() where T : IAsyncCommandHandler<TCommand, TResult> where TCommand : ActionBase where TResult : ActionResult;
        T GetCommand<T, TCommand, TResult>() where T : ICommandHandler<TCommand, TResult> where TCommand : ActionBase where TResult : ActionResult;
    }
}