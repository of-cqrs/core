using System.Threading.Tasks;
using CQRS.Core.Commands;
using CQRS.Core.Models;

namespace CQRS.Core.Tests
{
    public partial class RunCommandTests
    {
        public class TestAsyncCommandHandler : IAsyncCommandHandler<TestCommand, TestCommandResult>
        {
            public Task<TestCommandResult> ExecuteAsync(TestCommand command)
            {
                command.Called = true;
                return Task.FromResult(new TestCommandResult(true));
            }
        }
    }

    public class TestCommandResult : ActionResult
    {
        public TestCommandResult(bool isSucceed) : base(isSucceed)
        {
        }
    }

    public class TestCommand : ActionBase
    {
        public bool Called { get; set; }
    }
}