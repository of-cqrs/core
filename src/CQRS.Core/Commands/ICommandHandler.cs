using System.Threading.Tasks;

namespace CQRS.Core.Commands
{

    public interface IAsyncCommandHandler<TCommand, TResult>
    {
        Task<TResult> ExecuteAsync(TCommand command);
    }

    public interface ICommandHandler<TCommand, TResult>
    {
        TResult Execute(TCommand command);
    }
}