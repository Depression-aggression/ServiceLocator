using System;
using Depra.DI.Services.Runtime.Containers.Manual.Interfaces;

namespace Depra.DI.Services.Runtime.Containers.Manual.Named.Interfaces
{
    public interface INamedServiceCollectionProvider : IServiceCollectionProvider
    {
        bool HasService(Type serviceType, string serviceName);
    }
}