using UnityEngine;

namespace Rossoforge.Services.Samples.Updater
{
    public class ServiceLateUpdater : MonoBehaviour
    {
        private void LateUpdate()
        {
            ServiceLocator.LateUpdate();
        }
    }
}
