using System;
using CQRS.Core.Commands;
using CQRS.Core.Provider.Interfaces;

namespace CQRS.Core.Provider
{
    class DefaultCommandProvider : ICommandProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultCommandProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T GetCommand<T, TCommand>() where T : ICommandHandler<TCommand>
        {
            return (T)_serviceProvider.GetService(typeof(ICommandHandler<TCommand>));
        }

        public T GetAsyncCommand<T, TCommand>() where T : IAsyncCommandHandler<TCommand>
        {
            return (T)_serviceProvider.GetService(typeof(IAsyncCommandHandler<TCommand>));
        }

        public T GetCommand<T, TCommand, TResult>() where T : ICommandHandler<TCommand, TResult>
        {
            return (T)_serviceProvider.GetService(typeof(ICommandHandler<TCommand, TResult>));
        }
    }
}
