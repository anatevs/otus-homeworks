using System;
using ResourcesStorage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Engine
{
    [RequireComponent(typeof(CharacterResources))]
    public sealed class TakeResourceComponent : MonoBehaviour
    {
        private ResourceStorage _storage;

        [SerializeField]
        private ResourceID _resourceIDConfig;

        [SerializeField]
        private int _extractCount;

        private string _id;

        private void Start()
        {
            _id = _resourceIDConfig.ID;

            var storages = GetComponent<CharacterResources>();
            _storage = storages.GetResourceStorage(_id);
        }

        [Button]
        public bool TakeResources(GameObject target)
        {
            if (!target.TryGetComponent<ResourceStorage>(out var targetResourceStorage))
            {
                return false;
            }

            if (targetResourceStorage.ResourceID != _resourceIDConfig.ID)
            {
                return false;
            }

            if (targetResourceStorage.IsEmpty)
            {
                return false;
            }

            int extractCount = Math.Min(_extractCount, targetResourceStorage.Count);

            targetResourceStorage.RemoveResource(extractCount);
            _storage.AddResource(extractCount);
            return true;
        }
    }
}