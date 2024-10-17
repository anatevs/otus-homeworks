using UnityEngine;

namespace ResourcesStorage
{
    [CreateAssetMenu(fileName = "ResourceStorageConfig",
        menuName = "Configs/ResourceStorageConfig")]
    public sealed class ResourceStorageConfig : ScriptableObject
    {
        [field: SerializeField]
        public ResourceInfo Info { get; set; }
    }
}