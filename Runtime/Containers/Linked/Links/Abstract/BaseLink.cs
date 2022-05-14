using System;
using Depra.DI.Services.Runtime.Providing.Linked.Initialization;

namespace Depra.DI.Services.Runtime.Providing.Linked.Links.Abstract
{
    public abstract class BaseLink
    {
        protected bool IsThreadScope { get; set; }
        protected IInitializationDependency ConstructorDependency { get; set; }

        public bool HasDependencies => ConstructorDependency != null;
        public Type[] Dependencies => ConstructorDependency.Parameters;

        internal abstract object InvokeObject(bool requiresNew = false, object[] parameters = null);
    }
}