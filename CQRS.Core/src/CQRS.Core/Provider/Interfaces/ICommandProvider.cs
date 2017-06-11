using CQRS.Core.Commands;

namespace CQRS.Core.Provider.Interfaces
{
    public interface ICommandProvider
    {
        T GetCommand<T, TCommand>() where T : ICommandHandler<TCommand>;
        T GetAsyncCommand<T, TCommand>() where T : IAsyncCommandHandler<TCommand>; 
        T GetCommand<T, TCommand, TResult>() where T : ICommandHandler<TCommand, TResult>; 

    }
}