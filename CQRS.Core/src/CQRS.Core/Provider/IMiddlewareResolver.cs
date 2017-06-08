using System.Collections.Generic;
using CQRS.Core.Middleware;

namespace CQRS.Core.Provider
{
    public interface IMiddlewareResolver
    {
        IEnumerable<IMiddleware> Resolve();
    }
}