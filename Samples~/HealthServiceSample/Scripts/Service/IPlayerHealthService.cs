using Rossoforge.Services.Service;

namespace Rossoforge.Services.Samples.PlayerHealth
{
    public interface IPlayerHealthService : IService
    {
        int CurrentHealth { get; }
        void TakeDamage(int amount);
        void Heal(int amount);

        event HPDelegate HPChanged;
    }
}
