using System;
using System.Collections.Generic;

namespace Depra.DI.Services.Runtime.Containers.Collection
{
    public sealed class ServiceCollection<T> : Dictionary<Type, T>
    {
        public ServiceCollection() : base(new EmbeddedTypeAwareTypeComparer())
        {
        }

        private sealed class EmbeddedTypeAwareTypeComparer : IEqualityComparer<Type>
        {
            public bool Equals(Type x, Type y) => x.IsEquivalentTo(y);

            public int GetHashCode(Type obj) => obj.FullName.GetHashCode();
        }
    }
}