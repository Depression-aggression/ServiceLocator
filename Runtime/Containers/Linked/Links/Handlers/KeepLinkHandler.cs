namespace Depra.DI.Services.Runtime.Providing.Linked.Links.Handlers
{
    public struct KeepLinkHandler : ILinkHandler
    {
        public bool IsActive => true;
        public bool IsDestroyed => false;
    }
}