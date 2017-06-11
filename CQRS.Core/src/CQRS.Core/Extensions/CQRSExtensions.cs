﻿using CQRS.Core.Commands;
using CQRS.Core.Provider;
using CQRS.Core.Provider.Interfaces;
using CQRS.Core.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Core.Extensions
{
    public static class CQRSExtensions
    {
        public static IServiceCollection AddCQRS(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            serviceCollection.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            serviceCollection.AddSingleton<ICommandProvider, DefaultCommandProvider>();
            serviceCollection.AddSingleton<IQueryProvider, DefaultQueryProvider>();
            serviceCollection.AddSingleton<IMiddlewareResolver, DefaultMiddlewareResolver>();

            return serviceCollection;
        }
    }
}
