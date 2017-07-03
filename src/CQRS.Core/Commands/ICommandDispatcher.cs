using System.Threading.Tasks;

namespace CQRS.Core.Commands
{
    public interface ICommandDispatcher
    {
        TResult Dispatch<TCommand, TResult>(TCommand command);
        Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command);
    }
}