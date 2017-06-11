using System.Threading.Tasks;
using CQRS.Core.Provider;
using CQRS.Core.Provider.Interfaces;

namespace CQRS.Core.Commands
{
    //TODO proper middleware flow

    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandProvider _commandProvider;
        private readonly IMiddlewareResolver _middlewareResolver;

        public CommandDispatcher(ICommandProvider commandProvider, IMiddlewareResolver middlewareResolver)
        {
            _commandProvider = commandProvider;
            _middlewareResolver = middlewareResolver;
        }

        public void Dispatch<TCommand>(TCommand command) where TCommand : CommandBase
        {
            var middlewares = _middlewareResolver.Resolve();

            var handler = _commandProvider.GetCommand<ICommandHandler<TCommand>, TCommand>();
            handler.Execute(command);

            foreach (var middleware in middlewares)
                middleware.Apply(command);
        }

        public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : CommandBase
        {
            var handler = _commandProvider.GetAsyncCommand<IAsyncCommandHandler<TCommand>, TCommand>();
            await handler.Execute(command);
        }

        public TResult Dispatch<TCommand, TResult>(TCommand command) where TCommand : CommandBase
        {
            var middlewares = _middlewareResolver.Resolve();

            // TOOD simplify this stuff
            var handler = _commandProvider.GetCommand<ICommandHandler<TCommand, TResult>, TCommand, TResult>();
            var result = handler.Execute(command);

            foreach (var middleware in middlewares)
                middleware.Apply(command);

            return result;
        }
    }
}