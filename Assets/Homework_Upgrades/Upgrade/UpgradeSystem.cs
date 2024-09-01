using Game.GamePlay.Conveyor;
using Game.GamePlay.Upgrades;
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
        private StorageCapacityConfig _capacityConfig;

        [SerializeField]
        private ConveyorEntity _conveyor;

        [SerializeField]
        MoneyStorage _moneyStorage;

        //private readonly List<Upgrade> _upgrades = new();

        private LoadStorageCapacityUpgrade _upgrade;

        private void Awake()
        {
            _upgrade = new LoadStorageCapacityUpgrade(_capacityConfig, _conveyor);

            _moneyStorage.EarnMoney(500);
        }

        [Button]
        private void OnLevelUp()
        {
            if (CanLevelUp())
            {
                _moneyStorage.SpendMoney(_upgrade.NextPrice);
                _upgrade.LevelUp();
            }
        }

        private bool CanLevelUp()
        {
            if (!_upgrade.CanLevelUp)
            {
                return false;
            }

            var price = _upgrade.NextPrice;
            return _moneyStorage.CanSpendMoney(price);
        }
    }
}