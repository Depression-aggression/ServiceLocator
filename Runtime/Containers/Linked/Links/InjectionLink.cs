using System;
using System.Collections.Generic;
using System.Threading;
using Depra.DI.Services.Runtime.Providing.Linked.Initialization;
using Depra.DI.Services.Runtime.Providing.Linked.Links.Abstract;
using Depra.DI.Services.Runtime.Providing.Linked.Links.Interfaces;

namespace Depra.DI.Services.Runtime.Providing.Linked.Links
{
    public class InjectionLink<TObject> : GenericLink<TObject>, IInjectionLink where TObject : class, new()
    {
        private static readonly object Lock = new();
        private Func<object[], TObject> _getService;

        public InjectionLink() 
        {
            ThreadInstances = new Dictionary<int, TObject>();
        }

        public void Use(IInitializationDependency dependencies, bool threadScope = true)
        {
            IsThreadScope = threadScope;
            ConstructorDependency = dependencies;

            _getService = parameters => ConstructorDependency.GetInstance<TObject>(parameters);
        }

        internal override TObject Invoke(bool requiresNew = false, object[] parameters = null)
        {
            if (_getService == null)
            {
                return default(TObject);
            }
            
            if (IsThreadScope)
            {
                lock (Lock)
                {
                    if (requiresNew || !ThreadInstances.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                        ThreadInstances[Thread.CurrentThread.ManagedThreadId] = _getService(parameters);
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

            return default(TObject);
        }

        internal override object InvokeObject(bool requiresNew = false, object[] parameters = null)
        {
            return Invoke(requiresNew, parameters);
        }
    }
}