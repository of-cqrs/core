using System;
using CQRS.Core.Builders;
using CQRS.Core.Commands;
using CQRS.Core.Provider;
using CQRS.Core.Provider.Interfaces;
using CQRS.Core.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Core.Extensions
{
    public static class CQRSExtensions
    {
        public static IServiceCollection AddCQRS(this IServiceCollection serviceCollection, Action<CQRSBuilder> build = null)
        {
            var builder = new CQRSBuilder();
            build?.Invoke(builder);

            serviceCollection.AddSingleton<ICommandProvider, DefaultCommandProvider>();
            serviceCollection.AddSingleton<IQueryProvider, DefaultQueryProvider>();
            serviceCollection.AddSingleton<IMiddlewareResolver, DefaultMiddlewareResolver>();
            serviceCollection.AddSingleton<IActionDispatcher, ActionDispatcher>();
            serviceCollection.AddSingleton<ICQRSBuilder>(builder);

            return serviceCollection;
        }
    }
}
