using Depra.DI.Services.Runtime.Providing.Linked.Initialization;
using Depra.DI.Services.Runtime.Providing.Linked.Links.Interfaces;

namespace Depra.DI.Services.Runtime.Interfaces
{
    public interface IServiceLinker
    {
        IServiceLink<TInterface> For<TInterface>();

        IServiceLink<TInterface> For<TInterface>(string instanceName);

        IObjectLink Use<TObject>(bool lazyInstance = true, bool threadScope = true) where TObject : class, new();

        IInjectionLink Use<TObject>(IInitializationDependency dependencies, bool threadScope = true)
            where TObject : class, new();
    }
}