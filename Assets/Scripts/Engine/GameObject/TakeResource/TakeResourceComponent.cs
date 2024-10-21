using System;
using ResourcesStorage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Engine
{
    [RequireComponent(typeof(ResourcesContainer))]
    public sealed class TakeResourceComponent : MonoBehaviour
    {
        //private ResourceStorage _storage;

        //[SerializeField]
        //private ResourceID _resourceIDConfig;

        [SerializeField]
        private int _extractCount;

        //private string _id;

        private ResourcesContainer _storages;

        private void Start()
        {
            //_id = _resourceIDConfig.ID;

            _storages = GetComponent<ResourcesContainer>();
            //_storage = storages.GetResourceStorage(_id);
        }

        [Button]
        //public bool TakeResources(GameObject target)
        //{
        //    if (!target.TryGetComponent<ResourceStorage>(out var targetResourceStorage))
        //    {
        //        if (!target.TryGetComponent<ResourcesContainer>(out var targetResources))
        //        {
        //            return false;
        //        }

        //        targetResourceStorage = targetResources.GetResourceStorage(_id);
        //    }

        //    if (targetResourceStorage.ResourceID != _resourceIDConfig.ID)
        //    {
        //        return false;
        //    }

        //    if (targetResourceStorage.IsEmpty)
        //    {
        //        return false;
        //    }

        //    int extractCount = Math.Min(_extractCount, targetResourceStorage.Count);

        //    targetResourceStorage.RemoveResource(extractCount);
        //    _storage.AddResource(extractCount);
        //    return true;
        //}

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