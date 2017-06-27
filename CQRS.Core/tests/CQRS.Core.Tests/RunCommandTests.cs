using System;
using System.Threading.Tasks;
using CQRS.Core.Commands;
using CQRS.Core.Extensions;
using CQRS.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CQRS.Core.Tests
{
    public partial class RunCommandTests
    {
        [Fact]
        public async Task AsyncHandlerIsRegistered_ResultSucceed()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCQRS();

            serviceCollection.AddTransient<IAsyncCommandHandler<TestCommand, TestCommandResult>, TestAsyncCommandHandler>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var actionFacade = serviceProvider.GetService<IActionDispatcher>();

            var result = await actionFacade.RunAsync<TestCommand, TestCommandResult>(new TestCommand());

            Assert.True(result.IsSucceed);
        }

        [Fact]
        public async Task AsyncHandlerIsNotRegistered_ArgumentException()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCQRS();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var actionFacade = serviceProvider.GetService<IActionDispatcher>();

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => actionFacade.RunAsync<TestCommand, TestCommandResult>(new TestCommand()));

            Assert.Equal($"Handler for {typeof(TestCommand)} and with expected result {typeof(TestCommandResult)} is not registered.", ex.Message);
        }

        [Fact]
        public void HandlerIsRegistered_ResultSucceed()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCQRS();

            serviceCollection.AddTransient<ICommandHandler<TestCommand, TestCommandResult>, TestCommandHandler>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var actionFacade = serviceProvider.GetService<IActionDispatcher>();

            var result = actionFacade.Run<TestCommand, TestCommandResult>(new TestCommand());

            Assert.True(result.IsSucceed);
        }

        [Fact]
        public void HandlerIsNotRegistered_ArgumentException()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCQRS();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var actionFacade = serviceProvider.GetService<IActionDispatcher>();

            var ex = Assert.Throws<ArgumentException>(() => actionFacade.Run<TestCommand, TestCommandResult>(new TestCommand()));

            Assert.Equal($"Handler for {typeof(TestCommand)} and with expected result {typeof(TestCommandResult)} is not registered.", ex.Message);
        }
    }

   
}