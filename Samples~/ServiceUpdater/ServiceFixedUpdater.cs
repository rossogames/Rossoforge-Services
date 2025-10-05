using UnityEngine;

namespace Rossoforge.Services.Samples.Updater
{
    public class ServiceFixedUpdater : MonoBehaviour
    {
        private void FixedUpdate()
        {
            ServiceLocator.FixedUpdate();
        }
    }
}
