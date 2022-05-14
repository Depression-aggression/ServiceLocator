namespace Depra.DI.Services.Runtime.Providing.Linked.Links.Interfaces
{
    public interface IObjectLink
    {
        void Use(bool lazyInstance = true, bool threadScope = true);
    }
}