using System;
using CQRS.Core.Commands;
using CQRS.Core.Models;
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

        public T GetAsyncCommand<T, TCommand, TResult>() where T : IAsyncCommandHandler<TCommand, TResult> where TCommand : ActionBase where TResult : ActionResult
        {
            return (T) _serviceProvider.GetService(typeof(IAsyncCommandHandler<TCommand, TResult>));
        }

        public T GetCommand<T, TCommand, TResult>() where T : ICommandHandler<TCommand, TResult> where TCommand : ActionBase where TResult : ActionResult
        {
            return (T)_serviceProvider.GetService(typeof(ICommandHandler<TCommand, TResult>));
        }
    }
}
