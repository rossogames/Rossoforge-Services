using UnityEngine;
using UnityEngine.UI;

namespace Rossoforge.Services.Samples.PlayerHealth
{
    public class HPLabel : MonoBehaviour
    {
        private IPlayerHealthService playerHealthService;

        [SerializeField]
        private Text _label;

        private void Start()
        {
            playerHealthService = ServiceLocator.Get<IPlayerHealthService>();
            playerHealthService.HPChanged += PlayerHealthService_HPChanged;

            RefreshHP(playerHealthService.CurrentHealth);
        }

        private void OnDestroy()
        {
            playerHealthService.HPChanged -= PlayerHealthService_HPChanged;
        }

        private void PlayerHealthService_HPChanged(int currentHP)
        {
            RefreshHP(currentHP);
        }

        private void RefreshHP(int currentHP)
        {
            _label.text = $"Player HP: {currentHP}";
        }
    }
}