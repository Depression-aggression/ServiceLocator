using Depra.DI.Services.Runtime.Providing.Linked.Initialization;

namespace Depra.DI.Services.Runtime.Providing.Linked.Links.Interfaces
{
    public interface IServiceLink<in TInterface>
    {
        void Use<TObject>(bool lazyInstance = true, bool threadScope = true) where TObject : TInterface, new();

        void Use<TObject>(ConstructorDependency dependencies, bool threadScope = true)
            where TObject : class, TInterface, new();
    }
}