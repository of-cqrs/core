namespace CQRS.Core.Commands
{
    public class CommandBase
    {
        public virtual string Name => GetType()?.Name;
    }
}
