using System;

namespace Depra.DI.Services.Runtime.Containers.Manual.Interfaces
{
    public interface IServiceRegistrar
    {
        object RegisterSingle(Type serviceType, object serviceInstance, bool throwError);
    }
}