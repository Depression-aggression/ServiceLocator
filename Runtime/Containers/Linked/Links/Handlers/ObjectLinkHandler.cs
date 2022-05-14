using System;

namespace Depra.DI.Services.Runtime.Providing.Linked.Links.Handlers
{
    public struct ObjectLinkHandler<T> : ILinkHandler
    {
        private readonly WeakReference _objectRef;

        public bool IsActive => IsDestroyed == false;
        public bool IsDestroyed => _objectRef.IsAlive == false || (T)_objectRef.Target == null;
        
        public ObjectLinkHandler(T obj)
        {
            _objectRef = new WeakReference(obj);
        }
    }
}