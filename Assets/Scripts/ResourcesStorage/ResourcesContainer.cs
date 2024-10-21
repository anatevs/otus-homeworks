using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace ResourcesStorage
{
    public sealed class ResourcesContainer : MonoBehaviour
    {
        [ShowInInspector, ReadOnly]
        private readonly Dictionary<string, ResourceStorage> _resources = new();

        private void Awake()
        {
            var storages = GetComponents<ResourceStorage>();
            //var storages = GetComponentsInChildren<ResourceStorage>();

            foreach (var storage in storages)
            {
                if (!_resources.TryAdd(storage.ResourceID, storage))
                {
                    Debug.Log($"ResourceStorage with id {storage.ResourceID} is also exist in {gameObject.name}");
                }
            }
        }

        public void RemoveResource(string resourceID)
        {
            if (!_resources.Remove(resourceID))
            {
                Debug.Log($"there is no resource {resourceID} in {gameObject.name} resources");
            }
        }

        public bool TryGetResourceStorage(string resourceID, out ResourceStorage storage)
        {
            return _resources.TryGetValue(resourceID, out storage);
        }

        public ResourceStorage GetResourceStorage(string resourceID)
        {
            return _resources[resourceID];
        }
    }
}