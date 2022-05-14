using System;

namespace Depra.DI.Services.Runtime.Providing.Linked.Initialization
{
    public interface IInitializationDependency
    {
        Type[] Parameters { get;}

        TObject GetInstance<TObject>(object[] parameters) where TObject : class, new();
    }
}