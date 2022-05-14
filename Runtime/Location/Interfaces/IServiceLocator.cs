using System;

namespace Depra.DI.Services.Runtime.Location.Interfaces
{
    public interface IServiceLocator : IServiceProvider
    {
        TService RegisterSingle<TService>(TService service) where TService : new();

        TService GetInstance<TService>() where TService : new();
        
        //object[] GetAllInstances(Type serviceType);
    }

    public interface INamedServiceLocator
    {
        TService RegisterSingle<TService>(TService service, string serviceName) where TService : new();

        TService GetInstance<TService>(string serviceName);
    }
}