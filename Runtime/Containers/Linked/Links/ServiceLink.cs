using System;
using System.Collections.Generic;
using System.Threading;
using Depra.DI.Services.Runtime.Providing.Linked.Initialization;
using Depra.DI.Services.Runtime.Providing.Linked.Links.Abstract;
using Depra.DI.Services.Runtime.Providing.Linked.Links.Interfaces;

namespace Depra.DI.Services.Runtime.Providing.Linked.Links
{
    public class ServiceLink<TInterface> : GenericLink<TInterface>, IServiceLink<TInterface>
    {
        private static readonly object Lock = new();
        private Func<object[], TInterface> _getService;

        public ServiceLink()
        {
            ThreadInstances = new Dictionary<int, TInterface>();
        }

        public void Use<TObject>(bool lazyInstance = true, bool threadScope = true) where TObject : TInterface, new()
        {
            IsThreadScope = threadScope;
            _getService = parameters => new TObject();

            if (lazyInstance)
            {
                return;
            }

            if (IsThreadScope)
            {
                ThreadInstances.Add(Thread.CurrentThread.ManagedThreadId, _getService(null));
            }
            else
            {
                ServiceInstance = _getService(null);
            }
        }

        public void Use<TObject>(ConstructorDependency dependencies, bool threadScope = true)
            where TObject : class, TInterface, new()
        {
            IsThreadScope = threadScope;
            ConstructorDependency = dependencies;

            _getService = parameters => ConstructorDependency.GetInstance<TObject>(parameters);
        }

        internal override TInterface Invoke(bool requiresNew = false, object[] parameters = null)
        {
            if (_getService == null)
            {
                return default(TInterface);
            }

            if (IsThreadScope)
            {
                lock (Lock)
                {
                    if (requiresNew || ThreadInstances.ContainsKey(Thread.CurrentThread.ManagedThreadId) == false)
                    {
                        ThreadInstances[Thread.CurrentThread.ManagedThreadId] = _getService(parameters);
                    }
                }

                return ThreadInstances[Thread.CurrentThread.ManagedThreadId];
            }

            lock (Lock)
            {
                if (requiresNew)
                {
                    return _getService(parameters);
                }

                if (ServiceInstance == null)
                {
                    ServiceInstance = _getService(parameters);
                    return ServiceInstance;
                }
            }

            return default(TInterface);
        }

        internal override object InvokeObject(bool requiresNew = false, object[] parameters = null)
        {
            return Invoke(requiresNew, parameters);
        }
    }
}