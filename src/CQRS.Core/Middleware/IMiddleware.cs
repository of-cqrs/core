using CQRS.Core.Models;

namespace CQRS.Core.Middleware
{
    public interface IMiddleware
    {
        void Apply<T,Z>(ActionContext<T,Z> command);
    }
}