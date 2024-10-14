using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Resources
{
    public class ResourcesContainer : MonoBehaviour
    {
        [ShowInInspector]
        private Dictionary<string, ResourceStorage> _resources;

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
    }

    [Serializable]
    public struct ResourceInfo
    {
        public string ID;

        public int Capacity;

        public int Count;
    }
}