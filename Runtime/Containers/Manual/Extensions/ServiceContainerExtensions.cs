using Depra.DI.Services.Runtime.Containers.Interfaces;
using Depra.DI.Services.Runtime.Containers.Manual.Interfaces;

namespace Depra.DI.Services.Runtime.Containers.Extensions
{
    public static class ServiceContainerExtensions
    {
        public static TService GetService<TService>(this IServiceContainer container) =>
            (TService)container.GetService(typeof(TService));

        public static TService AddService<TService>(this IServiceContainer container, TService serviceInstance) =>
            (TService)container.AddService(typeof(TService), serviceInstance);

        public static void RemoveService<TService>(this IServiceContainer container) =>
            container.RemoveService(typeof(TService));
    }
}