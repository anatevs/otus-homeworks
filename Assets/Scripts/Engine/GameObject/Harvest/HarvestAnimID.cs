using System.Collections.Generic;
using UnityEngine;

namespace Game.Engine
{
    [RequireComponent(typeof(Animator))]
    public sealed class HarvestAnimID : MonoBehaviour
    {
        private static readonly Dictionary<string, int> AnimHashes = new();

        private Animator _animator;

        [SerializeField]
        private HarvestComponent _harvestComponent;

        [SerializeField]
        private HarvestAnimConfig _harvestAnimConfig;

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            foreach (var info in _harvestAnimConfig.Names)
            {
                var animHash = Animator.StringToHash(info.AnimName);

                AnimHashes.Add(info.IDconfig.ID, animHash);
            }
        }

        private void OnEnable()
        {
            _harvestComponent.OnStartedID += OnHarvestStarted;
        }

        private void OnDisable()
        {
            _harvestComponent.OnStartedID -= OnHarvestStarted;
        }

        private void OnHarvestStarted(string id)
        {
            _animator.SetTrigger(AnimHashes[id]);
        }
    }
}