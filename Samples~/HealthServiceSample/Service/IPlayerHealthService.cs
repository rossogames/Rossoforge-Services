using RossoForge.Core.Services;

namespace RossoForge.Services.Samples.PlayerHealth
{
    public interface IPlayerHealthService : IService, IInitializable
    {
        int CurrentHealth { get; }
        void TakeDamage(int amount);
        void Heal(int amount);

        event HPDelegate HPChanged;
    }
}
