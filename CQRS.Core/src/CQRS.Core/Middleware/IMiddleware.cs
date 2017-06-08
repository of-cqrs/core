using CQRS.Core.Commands;

namespace CQRS.Core.Middleware
{
    public interface IMiddleware
    {
        void Apply(CommandBase command);
    }
}