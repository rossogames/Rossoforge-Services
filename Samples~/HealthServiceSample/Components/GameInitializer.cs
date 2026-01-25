using UnityEngine;

namespace Rossoforge.Services.Samples.PlayerHealth
{
    public class GameInitializer : MonoBehaviour
    {
        void Awake()
        {
            var locator = new DefaultServiceLocator();
            ServiceLocator.SetLocator(locator);

            var playerHealthService = new PlayerHealthService();
            locator.Register<IPlayerHealthService>(playerHealthService);

            ServiceLocator.Initialize();
        }
    }
}