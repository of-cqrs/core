using System;
using System.Collections.Generic;
using CQRS.Core.Commands;
using CQRS.Core.Models;
using CQRS.Core.Provider;
using CQRS.Core.Provider.Interfaces;
using CQRS.Core.Queries;
using Microsoft.Extensions.DependencyInjection;
using Enumerable = System.Linq.Enumerable;

namespace CQRS.Core.Extensions
{
    public static class CQRSExtensions
    {
        public static IServiceCollection AddCQRS(this IServiceCollection serviceCollection, Action<CQRSBuilder> build = null)
        {
            var builder = new CQRSBuilder();
            build?.Invoke(builder);

            serviceCollection.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            serviceCollection.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            serviceCollection.AddSingleton<ICommandProvider, DefaultCommandProvider>();
            serviceCollection.AddSingleton<IQueryProvider, DefaultQueryProvider>();
            serviceCollection.AddSingleton<IMiddlewareResolver, DefaultMiddlewareResolver>();
            serviceCollection.AddSingleton<IActionFacade, ActionFacade>();
            serviceCollection.AddSingleton(builder);

            return serviceCollection;
        }
    }

    public interface ICQRSBuilder
    {
        void UseMiddleware(Func<DispatchAction, DispatchAction> func);
        DispatchAction BuildEndpoint(DispatchAction action);
    }

    public class CQRSBuilder : ICQRSBuilder
    {
        private readonly List<Func<DispatchAction, DispatchAction>> _middlewares = new List<Func<DispatchAction, DispatchAction>>();

        public void UseMiddleware(Func<DispatchAction, DispatchAction> func)
        {
            _middlewares.Add(func);
        }

        public DispatchAction BuildEndpoint(DispatchAction action)
        {
            return Enumerable.Aggregate(_middlewares, action, (current, middleware) => middleware(current));
        }
    }
}
