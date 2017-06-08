using System.Threading.Tasks;

namespace CQRS.Core.Commands
{
    public interface ICommandHandler<TCommand>
    {
        void Execute(TCommand command);
    }

    public interface IAsyncCommandHandler<TCommand>
    {
        Task Execute(TCommand command);
    }

    public interface ICommandHandler<TCommand, TResult>
    {
        TResult Execute(TCommand command);
    }
}