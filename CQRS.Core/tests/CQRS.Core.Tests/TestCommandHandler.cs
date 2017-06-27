using CQRS.Core.Commands;

namespace CQRS.Core.Tests
{
    public class TestCommandHandler : ICommandHandler<TestCommand, TestCommandResult> 
    {
        public TestCommandResult Execute(TestCommand command)
        {
            command.Called = true;
            return new TestCommandResult(true);
        }
    }
}