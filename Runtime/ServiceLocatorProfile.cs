using Unity.Collections;
using UnityEngine;

namespace RossoForge.Service
{
    [CreateAssetMenu(fileName = nameof(ServiceLocatorProfile), menuName = "RossoForge/Services/ServiceLocatorProfile")]
    public class ServiceLocatorProfile : ScriptableObject
    {
        [field: SerializeField]
        [field: ReadOnly]
        public string[] InitializedServices { get; set; }
    }
}