using System;
using System.Collections.Generic;
using Depra.DI.Services.Runtime.Containers.Interfaces;
using Depra.DI.Services.Runtime.Containers.Manual.Interfaces;
using UnityEngine.Assertions;

namespace Depra.DI.Services.Runtime.Location.Abstract
{
    public abstract class ServiceLocator<TContainer> : IServiceLocator, IDisposable
        where TContainer : class, IServiceCollectionProvider
    {
        internal TContainer Container { get; private set; }

        public abstract object GetService(Type serviceType);
        
        public void SetContainer(TContainer container)
        {
            Assert.IsNotNull(container);
            Container = container;
        }

        public bool CanResolve<TService>() => (this as IServiceLocator).CanResolve(typeof(TService));
        
        bool IServiceLocator.CanResolve(Type serviceType)
        {
            if (serviceType.IsInterface)
            {
                throw new ArgumentException("Type argument must be an interface type!");
            }

            return Container.HasService(serviceType);
        }
        
        IEnumerable<object> IServiceLocator.GetAllServices() => Container.GetAllServices();

        protected ServiceLocator(TContainer container)
        {
            SetContainer(container);
        }

        ~ServiceLocator()
        {
            Dispose();
        }

        public void Dispose()
        {
        }
    }
}