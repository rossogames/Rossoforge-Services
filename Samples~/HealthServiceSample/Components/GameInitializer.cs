using UnityEngine;

namespace RossoForge.Services.Samples.PlayerHealth
{
    public class GameInitializer : MonoBehaviour
    {
        void Awake()
        {
            var locator = new DefaultServiceLocator();
            locator.Register<IPlayerHealthService>(new PlayerHealthService());

            ServiceLocator.SetLocator(locator);
            ServiceLocator.Initialize();
        }
    }
}