using Game.GamePlay.Conveyor;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrade
{
    public class UpgradeSystem : MonoBehaviour
    {
        //[SerializeField]
        //private UpgradeConfig[] _configs;

        [SerializeField]
        private LoadStorageCapacityConfig _capacityConfig;

        [SerializeField]
        private ConveyorEntity _conveyor;

        //private readonly List<Upgrade> _upgrades = new();

        private LoadStorageCapacityUpgrade _upgrade;

        private void Awake()
        {
            //_conveyor = conveyor;

            _upgrade = new LoadStorageCapacityUpgrade(_capacityConfig, _conveyor);
        }

        [Button]
        private void OnLevelUp()
        {
            _upgrade.LevelUp();
        }
    }
}