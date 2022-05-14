using Depra.DI.Services.Runtime.Interfaces;

namespace Depra.DI.Services.Runtime.Location.Implementations
{
    public class SimpleServiceLocator
    {
        public void RegisterSingle<TService>(TService implementation) where TService : IService
        {
            Implementation<TService>.ServiceInstance = implementation;
        }

        public TService Single<TService>() where TService : IService
        {
            return Implementation<TService>.ServiceInstance;
        }

        private class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance { get; set; }
        }
    }
}