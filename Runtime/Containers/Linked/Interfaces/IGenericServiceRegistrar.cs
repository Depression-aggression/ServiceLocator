namespace Depra.DI.Services.Runtime.Containers.Interfaces
{
    public interface IGenericServiceRegistrar
    {
        TService RegisterSingle<TService>(TService serviceInstance, bool throwError) where TService : new();

        TService RegisterSingle<TService>(TService service, string serviceName, bool throwError) where TService : new();
    }
}