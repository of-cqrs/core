using System.Threading.Tasks;

namespace CQRS.Core.Commands
{
    public interface ICommandDispatcher
    {
        void Dispatch<TCommand>(TCommand command) where TCommand:CommandBase;
        TResult Dispatch<TCommand, TResult>(TCommand command) where TCommand : CommandBase;
        Task DispatchAsync<TCommand>(TCommand command) where TCommand : CommandBase;
    }
}