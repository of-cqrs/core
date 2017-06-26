using System;
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
            var actionFacade = serviceProvider.GetService<IActionFacade>();

            Assert.NotNull(actionFacade);
        }

        [Fact]
        public void AddCQRS_WithAnonymusMiddleware_ShouldeCalled()
        {
            bool triggerred = false;
            var servceCollection = new ServiceCollection();
            servceCollection.AddCQRS(builder => builder.UseMiddleware((next) =>
            {
                return async (context) =>
                {
                    triggerred = true;
                    await next(context);
                };
            }));

            //TODO Dispatch some action
            Assert.True(triggerred);
        }
    }
}
