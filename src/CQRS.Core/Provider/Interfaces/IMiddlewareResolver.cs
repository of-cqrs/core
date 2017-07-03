using System.Collections.Generic;
using CQRS.Core.Middleware;

namespace CQRS.Core.Provider.Interfaces
{
    public interface IMiddlewareResolver
    {
        IEnumerable<IMiddleware> Resolve();
    }
}