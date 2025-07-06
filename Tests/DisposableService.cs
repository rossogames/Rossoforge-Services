using Rossoforge.Core.Services;
using System;

namespace Rossoforge.Services.Tests
{
    public class DisposableService : IService, IDisposable
    {
        public bool Disposed { get; private set; }
        public void Dispose()
        {
            Disposed = true;
        }
    }
}
