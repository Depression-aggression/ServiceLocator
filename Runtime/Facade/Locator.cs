using System;
using Depra.DI.Services.Runtime.Containers.Manual.Implementation;
using Depra.DI.Services.Runtime.Interfaces;
using Depra.DI.Services.Runtime.Location.Implementations;
using UnityEngine;

namespace Depra.DI.Services.Runtime.Facade
{
    public class Locator
    {
        private static Locator _instance;
        public static Locator Instance => _instance ?? new Locator();
        
        private readonly ServiceLocator _innerLocator;
        private bool _disposed;

        public static TService Register<TService>(TService service) where TService : IService, new() =>
            Instance._innerLocator.RegisterSingle(service);

        public static TService Resolve<TService>() where TService : class, new() =>
            Instance._innerLocator.GetService<TService>();

        public Locator()
        {
            _innerLocator = new ServiceLocator(new ServiceContainer());
        }

        ~Locator()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _innerLocator.Dispose();
            }
            else
            {
                Debug.LogWarning("Locator did not dispose correctly and was cleaned up by the GC.");
            }

            _innerLocator.Dispose();
            _disposed = true;
        }
    }
}