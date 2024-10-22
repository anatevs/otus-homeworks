using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Engine
{
    [RequireComponent(typeof(ResourcesContainer))]
    public sealed class TakeResourceComponent : MonoBehaviour
    {
        [SerializeField]
        private int _extractCount;

        private ResourcesContainer _storages;

        private void Start()
        {
            _storages = GetComponent<ResourcesContainer>();
        }

        [Button]
        public bool TakeResources(GameObject target, string id)
        {
            if (!target.TryGetComponent<ResourceStorage>(out var targetResourceStorage))
            {
                return false;
            }

            if (targetResourceStorage.ResourceID != id)
            {
                return false;
            }

            if (targetResourceStorage.IsEmpty)
            {
                return false;
            }

            int extractCount = Math.Min(_extractCount, targetResourceStorage.Count);

            targetResourceStorage.RemoveResource(extractCount);
            
            var storage = _storages.GetResourceStorage(id);
            storage.AddResource(extractCount);
            
            return true;
        }
    }
}