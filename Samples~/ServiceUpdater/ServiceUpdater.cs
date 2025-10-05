using UnityEngine;

namespace Rossoforge.Services.Samples.Updater
{
    public class ServiceUpdater : MonoBehaviour
    {
        private void Update()
        {
            ServiceLocator.Update();
        }
    }
}
