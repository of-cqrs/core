using System;
using System.Collections.Generic;
using CQRS.Core.Middleware;
using CQRS.Core.Provider.Interfaces;

namespace CQRS.Core.Provider
{
    class DefaultMiddlewareResolver : IMiddlewareResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultMiddlewareResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<IMiddleware> Resolve()
        {
            return (IEnumerable<IMiddleware>)_serviceProvider.GetService(typeof(IEnumerable<IMiddleware>));
        }
    }
}
