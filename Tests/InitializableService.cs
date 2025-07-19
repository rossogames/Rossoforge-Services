using Rossoforge.Core.Services;

namespace Rossoforge.Services.Tests
{
    public class InitializableService : IService, IInitializable
    {
        public bool Initialized { get; private set; }
        public void Initialize()
        {
            Initialized = true;
        }
    }
}
