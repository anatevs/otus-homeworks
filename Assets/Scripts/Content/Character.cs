using Game.Engine;
using ResourcesStorage;
using System;
using UnityEngine;

namespace Game.Content
{
    public sealed class Character : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField]
        private MoveComponent _moveComponent;

        [SerializeField]
        private LookComponent _lookComponent;

        [Header("Harvesting")]
        [SerializeField]
        private HarvestComponent _harvestComponent;

        [SerializeField]
        private OverlapSphereComponent _overlapSphereComponent;

        [SerializeField]
        private TakeResourceComponent _takeResourceComponent;

        [Header("Storage")]
        [SerializeField]
        private ResourcesContainer _characterResources;

        [SerializeField]
        private ResourceID _resourceIDConfig;

        private ResourceStorage _harvestingStorage;

        private void OnEnable()
        {
            _moveComponent.OnMove += OnMove;
        }

        private void OnDisable()
        {
            _moveComponent.OnMove -= OnMove;
        }

        private void OnMove()
        {
            _lookComponent.Direction = _moveComponent.MoveDirection;
        }

        private void Start()
        {
            _harvestingStorage = _characterResources.GetResourceStorage(_resourceIDConfig.ID);

            _harvestComponent.AddCondition(_harvestingStorage.IsNotFull);

            foreach (var id in _characterResources.IDs)
            {
                var storage = _characterResources.GetResourceStorage(id);
                _harvestComponent.AddCondition(id, storage.IsNotFull);
            }

            _harvestComponent.SetProcessAction(RaycastResources);

            _harvestComponent.SetProcessActionID(RaycastResourcesID);
        }

        private void RaycastResources()
        {
            _overlapSphereComponent.OverlapSphere(HarvestResource);
        }

        private bool HarvestResource(GameObject target)
        {
            return target.CompareTag(GameObjectTags.Tree) &&
                   target.activeSelf &&
                   _takeResourceComponent.TakeResources(target, _resourceIDConfig.ID);
        }

        private void RaycastResourcesID(string id)
        {
            _overlapSphereComponent.OverlapSphere(HarvestResourceID(id));
        }

        private Predicate<GameObject> HarvestResourceID(string id)
        {
            return (target) => target.activeSelf &&
            (target.CompareTag(GameObjectTags.Tree) || target.CompareTag(GameObjectTags.UnloadConveyor)) &&
            _takeResourceComponent.TakeResources(target, id);
        }
    }
}