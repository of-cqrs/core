using System;
using System.Threading.Tasks;
using CQRS.Core.Commands;
using CQRS.Core.Extensions;
using CQRS.Core.Models;
using CQRS.Core.Queries;

namespace CQRS.Core
{
    public class ActionFacade : IActionFacade
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public ActionFacade(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, ICQRSBuilder builder)
        {
            _commandDispatcher = commandDispatcher ?? throw new ArgumentException(nameof(commandDispatcher));
            _queryDispatcher = queryDispatcher ?? throw new ArgumentException(nameof(queryDispatcher));
        }

        public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query) where TResult : ActionResult
        {
            var context = new ActionContext<TQuery, TResult>() { Type = ActionType.Query, Action = query };
            await DispatchAsync(context);

            return context.Result;
        }

        public TResult Query<TQuery, TResult>(TQuery query) where TResult : ActionResult
        {
            var context = new ActionContext<TQuery, TResult>() { Type = ActionType.Query, Action = query };
            Dispatch(context);
            return context.Result;
        }

        public async Task<TCommandResult> RunAsync<TCommand, TCommandResult>(TCommand command) where TCommandResult : ActionResult
        {
            var context = new ActionContext<TCommand, TCommandResult>() { Type = ActionType.Command, Action = command };
            await DispatchAsync(context);

            return context.Result;
        }

        public TCommandResult Run<TCommand, TCommandResult>(TCommand command) where TCommandResult : ActionResult
        {
            var context = new ActionContext<TCommand, TCommandResult>() { Type = ActionType.Command, Action = command };
            Dispatch(context);
            return context.Result;
        }

        private async Task DispatchAsync(ActionContextBase context)
        {

        }

        private async Task DispatchAsync<T, Z>(ActionContext<T, Z> context)
        {
            switch (context.Type)
            {
                case ActionType.Query:
                    context.Result = await _queryDispatcher.DispatchAsync<T, Z>(context.Action);
                    break;
                case ActionType.Command:
                    context.Result = await _commandDispatcher.DispatchAsync<T, Z>(context.Action);
                    break;
            }
        }

        private void Dispatch<T, Z>(ActionContext<T, Z> context)
        {
            switch (context.Type)
            {
                case ActionType.Query:
                    context.Result = _queryDispatcher.Dispatch<T, Z>(context.Action);
                    break;
                case ActionType.Command:
                    context.Result = _commandDispatcher.Dispatch<T, Z>(context.Action);
                    break;
            }
        }
    }
}