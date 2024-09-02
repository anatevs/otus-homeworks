using Game.GamePlay.Conveyor;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Upgrades
{
    public sealed class UpgradeSystem : MonoBehaviour
    {
        [SerializeField]
        private UpgradeConfig[] _configs;

        [SerializeField]
        private ConveyorEntity _conveyor;

        [SerializeField]
        MoneyStorage _moneyStorage;

        [ShowInInspector]
        private readonly List<string> _idHelper = new();

        private readonly Dictionary<string, Upgrade> _upgrades = new();

        [Inject]
        public void Construct(IObjectResolver objectResolver)
        {
            foreach (var config in _configs)
            {
                var upgrade = config.CreateUpgrade(objectResolver);
                _upgrades.Add(config.Id, upgrade);

                _idHelper.Add(config.Id);
            }
        }

        [Button]
        private void OnLevelUp(string Id)
        {
            var upgrade = _upgrades[Id];

            if (CanLevelUp(upgrade))
            {
                _moneyStorage.SpendMoney(upgrade.NextPrice);
                upgrade.LevelUp();
            }
        }

        private bool CanLevelUp(Upgrade upgrade)
        {
            if (!upgrade.CanLevelUp)
            {
                return false;
            }

            var price = upgrade.NextPrice;
            return _moneyStorage.CanSpendMoney(price);
        }
    }
}