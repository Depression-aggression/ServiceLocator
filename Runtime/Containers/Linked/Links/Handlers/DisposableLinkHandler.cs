using System;
using UnityEngine;

namespace Depra.DI.Services.Runtime.Providing.Linked.Links.Handlers
{
    public class DisposableLinkHandler : ILinkHandler, IDisposable
    {
        private readonly bool _showUndisposedWarning;
        private bool _disposed;

        public bool IsActive => !_disposed;
        public bool IsDestroyed => _disposed;

        public DisposableLinkHandler(bool showUndisposedWarning)
        {
            _showUndisposedWarning = showUndisposedWarning;
        }

        ~DisposableLinkHandler()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            
            _disposed = true;

            if (disposing == false && _showUndisposedWarning)
            {
                Debug.LogError("A link handler did not dispose correctly and was cleaned up by the GC.");
            }
        }
    }
}