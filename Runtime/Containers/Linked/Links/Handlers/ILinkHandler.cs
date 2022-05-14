namespace Depra.DI.Services.Runtime.Providing.Linked.Links.Handlers
{
    public interface ILinkHandler
    {
        bool IsActive { get; }
        bool IsDestroyed { get; }
    }
}
