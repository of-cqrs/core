using System;
using System.Threading.Tasks;
using CQRS.Core.Commands;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using CQRS.Core.Extensions;

namespace CQRS.Core.Tests
{
    public class UseCQRSTests
    {
        [Fact]
        public void AddCQRS_NoError()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCQRS();
        }

        [Fact]
        public void AddCQRS_ResolveActionFacade_ShouldntBeNull()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCQRS();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var actionFacade = serviceProvider.GetService<IActionDispatcher>();

            Assert.NotNull(actionFacade);
        }

        [Fact]
        public async Task AddCQRS_WithAnonymusMiddleware_ShouldeCalled()
        {
            bool triggerred = false;
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCQRS(builder => builder.UseMiddleware((next) =>
            {
                return async (context) =>
                {
                    triggerred = true;
                    await next(context);
                };
            }));

            serviceCollection.AddTransient<IAsyncCommandHandler<TestCommand, TestCommandResult>, RunCommandTests.TestAsyncCommandHandler>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var actionFacade = serviceProvider.GetService<IActionDispatcher>();

            await actionFacade.RunAsync<TestCommand, TestCommandResult>(new TestCommand());

            Assert.True(triggerred);
        }

        [Fact]
        public async Task AddCQRS_WithAnonymusMiddleware_ResultSuccess()
        {
            bool triggerred = false;
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCQRS(builder => builder.UseMiddleware((next) =>
            {
                return async (context) =>
                {
                    triggerred = true;
                    await next(context);
                };
            }));

            serviceCollection.AddTransient<IAsyncCommandHandler<TestCommand, TestCommandResult>, RunCommandTests.TestAsyncCommandHandler>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var actionFacade = serviceProvider.GetService<IActionDispatcher>();

            var result = await actionFacade.RunAsync<TestCommand, TestCommandResult>(new TestCommand());

            Assert.True(result.IsSucceed);
        }

    }
}
