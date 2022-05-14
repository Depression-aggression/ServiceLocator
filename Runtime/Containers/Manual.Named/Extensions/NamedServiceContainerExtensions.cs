using Depra.DI.Services.Runtime.Containers.Interfaces;
using Depra.DI.Services.Runtime.Containers.Manual.Named.Interfaces;

namespace Depra.DI.Services.Runtime.Containers.Extensions
{
    public static class NamedServiceContainerExtensions
    {
        public static TService GetService<TService>(this INamedServiceContainer container, string serviceName) =>
            (TService)container.GetService(typeof(TService), serviceName);

        public static TService AddService<TService>(this INamedServiceContainer container, TService serviceInstance,
            string serviceName) => (TService)container.AddService(typeof(TService), serviceInstance, serviceName);

        public static void RemoveService<TService>(this INamedServiceContainer container) =>
            container.RemoveService(typeof(TService));
    }
}