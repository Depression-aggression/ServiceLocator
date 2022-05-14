using System.Collections.Generic;
using System.Threading;
using Depra.DI.Services.Runtime.Providing.Linked.Links.Abstract;
using Depra.DI.Services.Runtime.Providing.Linked.Links.Interfaces;

namespace Depra.DI.Services.Runtime.Providing.Linked.Links
{
    public class ObjectLink<TObject> : GenericLink<TObject>, IObjectLink where TObject : class, new()
    {
        private static readonly object Lock = new();

        public ObjectLink()
        {
            ThreadInstances = new Dictionary<int, TObject>();
        }
        
        public void Use(bool lazyInstance = true, bool threadScope = true)
        {
            IsThreadScope = threadScope;

            if (lazyInstance)
            {
                return;
            }

            if (IsThreadScope)
            {
                ThreadInstances.Add(Thread.CurrentThread.ManagedThreadId, new TObject());
            }
            else
            {
                ServiceInstance = new TObject();
            }
        }

        internal override TObject Invoke(bool requiresNew = false, object[] parameters = null)
        {
            if (IsThreadScope)
            {
                lock (Lock)
                {
                    if (requiresNew || ThreadInstances.ContainsKey(Thread.CurrentThread.ManagedThreadId) == false)
                    {
                        ThreadInstances[Thread.CurrentThread.ManagedThreadId] = new TObject();
                    }
                }

                return ThreadInstances[Thread.CurrentThread.ManagedThreadId];
            }

            lock (Lock)
            {
                if (requiresNew)
                {
                    return new TObject();
                }

                if (ServiceInstance == null)
                {
                    ServiceInstance = new TObject();
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