using CQRS.Core.Commands;

namespace CQRS.Core.Provider.Interfaces
{
    public interface ICommandProvider
    {
        T GetAsyncCommand<T, TCommand, TResult>() where T : IAsyncCommandHandler<TCommand, TResult>;
        T GetCommand<T, TCommand, TResult>() where T : ICommandHandler<TCommand, TResult>;
    }
}