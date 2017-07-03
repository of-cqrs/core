using System;
using CQRS.Core.Provider.Interfaces;
using CQRS.Core.Queries;

namespace CQRS.Core.Provider
{
    class DefaultQueryProvider : IQueryProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultQueryProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T GetQuery<T, TQuery, TResult>() where T : IQueryHandler<TQuery, TResult>
        {
            return (T)_serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>));
        }

        public T GetAsyncQuery<T, TQuery, TResult>() where T : IAsyncQueryHandler<TQuery, TResult>
        {
            return (T)_serviceProvider.GetService(typeof(IAsyncQueryHandler<TQuery, TResult>));
        }
    }
}
