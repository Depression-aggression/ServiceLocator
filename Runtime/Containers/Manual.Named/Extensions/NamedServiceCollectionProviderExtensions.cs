using Depra.DI.Services.Runtime.Containers.Interfaces;
using Depra.DI.Services.Runtime.Containers.Manual.Named.Interfaces;

namespace Depra.DI.Services.Runtime.Containers.Extensions
{
    public static class NamedServiceCollectionProviderExtensions
    {
        public static bool HasService<TService>(this INamedServiceContainer container) =>
            container.HasService(typeof(TService));
        
        public static bool HasService<TService>(this INamedServiceCollectionProvider container, string serviceName) =>
            container.HasService(typeof(TService), serviceName);
    }
}