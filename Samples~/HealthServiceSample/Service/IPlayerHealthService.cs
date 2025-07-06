using Rossoforge.Core.Services;

namespace Rossoforge.Services.Samples.PlayerHealth
{
    public interface IPlayerHealthService : IService, IInitializable
    {
        int CurrentHealth { get; }
        void TakeDamage(int amount);
        void Heal(int amount);

        event HPDelegate HPChanged;
    }
}
