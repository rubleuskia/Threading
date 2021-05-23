using System;
using System.IO;
using System.Security.Cryptography;

namespace GC
{
    // https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
    // Pattern - split managed and unmanaged resources
    public class SomeClass : IDisposable
    {
        private IntPtr _handle;
        private MemoryStream _stream;
        private bool _disposed;

        public SomeClass()
        {
            _stream = new MemoryStream();
        }
        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _stream.Dispose();
                }

                _handle = IntPtr.Zero;
                _disposed = true;
            }
        }

        ~SomeClass()
        {
            Dispose(false);
        }
    }
}
