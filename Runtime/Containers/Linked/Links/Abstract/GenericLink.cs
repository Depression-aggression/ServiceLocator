using System.Collections.Generic;

namespace Depra.DI.Services.Runtime.Providing.Linked.Links.Abstract
{
    public abstract class GenericLink<TInterface> : BaseLink
    {
        protected TInterface ServiceInstance { get; set; }
        protected Dictionary<int, TInterface> ThreadInstances { get; set; }
        
        public bool HasInstance => ServiceInstance != null;
        
        internal abstract TInterface Invoke(bool requiresNew = false, object[] parameters = null);
    }
}