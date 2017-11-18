using System;
using CQRS.Core.Models;

namespace CQRS.Core.Builders
{
    public interface ICQRSBuilder
    {
        void UseMiddleware(Func<DispatchAction, DispatchAction> func);
        DispatchAction BuildEndpoint(DispatchAction action);
    }
}