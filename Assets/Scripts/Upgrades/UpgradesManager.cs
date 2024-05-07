using System;
using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public sealed class UpgradesManager
    {
        public event Action<Upgrade> OnLevelUp;

        [ShowInInspector]
        private readonly MoneyStorage _moneyStorage;

        [ReadOnly, ShowInInspector]
        private Dictionary<PlayerStatType, Upgrade> _upgrades = new();

        public UpgradesManager(MoneyStorage moneyStorage)
        {
            _moneyStorage = moneyStorage;
        }

        public void Setup(Upgrade[] upgrades)
        {
            _upgrades = new Dictionary<PlayerStatType, Upgrade>();
            for (int i = 0, count = upgrades.Length; i < count; i++)
            {
                var upgrade = upgrades[i];
                _upgrades[upgrade.Id] = upgrade;
            }
        }

        public Upgrade GetUpgrade(PlayerStatType id)
        {
            return _upgrades[id];
        }

        public bool TryGetUpgrade(PlayerStatType id, out Upgrade upgrade)
        {
            if (_upgrades.TryGetValue(id, out upgrade))
            {
                return true;
            }

            return false;
        }

        public Upgrade[] GetAllUpgrades()
        {
            return _upgrades.Values.ToArray<Upgrade>();
        }

        public bool CanLevelUp(Upgrade upgrade)
        {
            if (upgrade.IsMaxLevel || !upgrade.IsNoConstraints())
            {
                return false;
            }

            var price = upgrade.NextPrice;
            return _moneyStorage.CanSpendMoney(price);
        }

        public void LevelUp(Upgrade upgrade)
        {
            if (!CanLevelUp(upgrade))
            {
                throw new Exception($"Can not level up {upgrade.Id}");
            }

            var price = upgrade.NextPrice;
            _moneyStorage.SpendMoney(price);

            upgrade.LevelUp();
            OnLevelUp?.Invoke(upgrade);
        }

        [Title("Methods")]
        [Button]
        public bool CanLevelUp(PlayerStatType id)
        {
            var upgrade = _upgrades[id];
            return CanLevelUp(upgrade);
        }

        [Button]
        public void LevelUp(PlayerStatType id)
        {
            var upgrade = _upgrades[id];
            LevelUp(upgrade);
        }
    }
}