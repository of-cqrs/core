using System.Threading.Tasks;
using CQRS.Core.Models;
using CQRS.Core.Provider;
using CQRS.Core.Provider.Interfaces;

namespace CQRS.Core.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandProvider _commandProvider;

        public CommandDispatcher(ICommandProvider commandProvider)
        {
            _commandProvider = commandProvider;
        }

        public TResult Dispatch<TCommand, TResult>(TCommand command) where TCommand : ActionBase where TResult : ActionResult
        {
            var handler = _commandProvider.GetCommand<ICommandHandler<TCommand, TResult>, TCommand, TResult>();
            var result = handler.Execute(command);

            return result;
        }

        public async Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command) where TCommand : ActionBase where TResult : ActionResult
        {
            var handler = _commandProvider.GetAsyncCommand<IAsyncCommandHandler<TCommand, TResult>, TCommand, TResult>();
            var result = await handler.ExecuteAsync(command);

            return result;
        }
    }
}