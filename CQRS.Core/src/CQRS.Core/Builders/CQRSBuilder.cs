using System;
using System.Collections.Generic;
using System.Linq;
using CQRS.Core.Models;

namespace CQRS.Core.Builders
{
    public class CQRSBuilder : ICQRSBuilder
    {
        private readonly List<Func<DispatchAction, DispatchAction>> _middlewares = new List<Func<DispatchAction, DispatchAction>>();

        public void UseMiddleware(Func<DispatchAction, DispatchAction> func)
        {
            _middlewares.Add(func);
        }

        public DispatchAction BuildEndpoint(DispatchAction action)
        {
            return Enumerable.Aggregate<Func<DispatchAction, DispatchAction>, DispatchAction>(_middlewares, action, (current, middleware) => middleware(current));
        }
    }
}