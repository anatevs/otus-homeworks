﻿using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace ResourcesStorage
{
    public sealed class PlayerResources : MonoBehaviour
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

        public ResourceStorage GetResource(string resourceID)
        {
            if (!_resources.TryGetValue(resourceID, out var storage))
            {
                Debug.Log($"there is no resource {resourceID} in {name}");
                return null;
            }

            return storage;
        }
    }
}