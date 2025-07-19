using UnityEngine;

namespace Rossoforge.Services.Samples.PlayerHealth
{
    public class PlayerHealthService : IPlayerHealthService
    {
        private int _maxHP = 100;
        private int _currentHP;

        public int CurrentHealth
        {
            get => _currentHP;
            set
            {
                _currentHP = value;
                HPChanged?.Invoke(value);
            }
        }

        public event HPDelegate HPChanged;

        public void Initialize()
        {
            CurrentHealth = _maxHP;
            Debug.Log("PlayerHealthService initialized with full health.");
        }

        public void TakeDamage(int amount)
        {
            CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
            Debug.Log($"Player took {amount} damage. Current health: {CurrentHealth}");
        }

        public void Heal(int amount)
        {
            CurrentHealth = Mathf.Min(CurrentHealth + amount, _maxHP);
            Debug.Log($"Player healed {amount}. Current health: {CurrentHealth}");
        }
    }
    public delegate void HPDelegate(int currentHP);
}