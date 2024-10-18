using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace ResourcesStorage
{
    public sealed class CharacterResources : MonoBehaviour
    {
        [ShowInInspector]
        private Dictionary<string, ResourceStorage> _resources = new();

        [SerializeField]
        private ResourceStorageConfig[] _configs;

        private void OnEnable()
        {
            foreach (var config in _configs)
            {
                AddResource(config.Info);
            }
        }

        public void AddResource(ResourceInfo resource)
        {
            var storage = new ResourceStorage(resource.Capacity, resource.Count);

            if (!_resources.TryAdd(resource.ID, storage))
            {
                _resources[resource.ID] = storage;
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
    }
}