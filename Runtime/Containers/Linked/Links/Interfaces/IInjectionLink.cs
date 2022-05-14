using Depra.DI.Services.Runtime.Providing.Linked.Initialization;

namespace Depra.DI.Services.Runtime.Providing.Linked.Links.Interfaces
{
    public interface IInjectionLink
    {
        void Use(IInitializationDependency dependencies, bool threadScope = true);
    }
}