using System;
using System.Threading.Tasks;
using CQRS.Core.Commands;
using CQRS.Core.Extensions;
using CQRS.Core.Models;
using CQRS.Core.Provider.Interfaces;
using CQRS.Core.Queries;

namespace CQRS.Core
{
    public class ActionFacade : IActionFacade
    {
        private readonly ICQRSBuilder _builder;
        private readonly ICommandProvider _commandProvider;
        private readonly IQueryProvider _queryProvider;


        public ActionFacade(ICommandProvider commandProvider, IQueryProvider queryProvider, ICQRSBuilder builder)
        {
            _commandProvider = commandProvider ?? throw new ArgumentException(nameof(commandProvider));
            _queryProvider = queryProvider ?? throw new ArgumentException(nameof(queryProvider));
            _builder = builder ?? throw new ArgumentException(nameof(builder));
        }

        public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query) where TResult : ActionResult where TQuery : ActionBase
        {
            var context = new ActionContext<TQuery, TResult>() { Type = ActionType.Query, Action = query };
            var handler = _queryProvider.GetAsyncQuery<IAsyncQueryHandler<TQuery, TResult>, TQuery, TResult>() as IAsyncQueryHandler<ActionBase, ActionResult>;
            if(handler == null)
                throw new ArgumentException($"Handler for {typeof(TQuery)} and with expected result {typeof(TResult)} is not registered.");

            async Task Action(ActionContextBase actionContext)
            {
                var result = await handler.Retrieve(actionContext.Action);
                actionContext.Result = result;
            }

            var endpoint = _builder.BuildEndpoint(Action);
            await endpoint(context);

            return context.Result as TResult;
        }

        public TResult Query<TQuery, TResult>(TQuery query) where TResult : ActionResult where TQuery : ActionBase
        {
            var context = new ActionContext<TQuery, TResult>() { Type = ActionType.Query, Action = query };
            var handler = _queryProvider.GetQuery<IQueryHandler<TQuery, TResult>, TQuery, TResult>() as IQueryHandler<ActionBase, ActionResult>;
            if (handler == null)
                throw new ArgumentException($"Handler for {typeof(TQuery)} and with expected result {typeof(TResult)} is not registered.");

            Task Action(ActionContextBase actionContext)
            {
                var result = handler.Retrieve(actionContext.Action);
                actionContext.Result = result;
                return Task.CompletedTask;
            }

            var endpoint = _builder.BuildEndpoint(Action);
            endpoint(context).Wait();

            return context.Result as TResult;
        }

        public async Task<TCommandResult> RunAsync<TCommand, TCommandResult>(TCommand command) where TCommandResult : ActionResult where TCommand : ActionBase
        {
            var context = new ActionContext<TCommand, TCommandResult>() { Type = ActionType.Command, Action = command };
            var handler = _commandProvider.GetAsyncCommand<IAsyncCommandHandler<TCommand, TCommandResult>, TCommand, TCommandResult>();
            if (handler == null)
                throw new ArgumentException($"Handler for {typeof(TCommand)} and with expected result {typeof(TCommandResult)} is not registered.");

            async Task Action(ActionContextBase actionContext)
            {
                var result = await handler.ExecuteAsync((TCommand) actionContext.Action );
                actionContext.Result = result;
            }

            var endpoint = _builder.BuildEndpoint(Action);
            await endpoint(context);

            return context.Result as TCommandResult;
        }

        public TCommandResult Run<TCommand, TCommandResult>(TCommand command) where TCommandResult : ActionResult where TCommand : ActionBase
        {
            var context = new ActionContext<TCommand, TCommandResult>() { Type = ActionType.Command, Action = command };
            var handler = _commandProvider.GetCommand<ICommandHandler<TCommand, TCommandResult>, TCommand, TCommandResult>();
            if (handler == null)
                throw new ArgumentException($"Handler for {typeof(TCommand)} and with expected result {typeof(TCommandResult)} is not registered.");

            Task Action(ActionContextBase actionContext)
            {
                var result = handler.Execute((TCommand)actionContext.Action);
                actionContext.Result = result;
                return Task.CompletedTask;
            }

            var endpoint = _builder.BuildEndpoint(Action);
            endpoint(context).Wait();

            return context.Result as TCommandResult;
        }

    }
}