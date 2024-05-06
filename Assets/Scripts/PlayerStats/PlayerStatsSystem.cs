using System;
using UnityEngine;
using VContainer;

namespace Sample
{
    public sealed class PlayerStatsSystem : MonoBehaviour
    {
        [SerializeField]
        private PlayerStatsInit[] _initInfo;

        private PlayerStats _playerStats;

        private UpgradesManager _upgradesManager;

        private Upgrade[] _upgrades;

        private IObjectResolver _objectResolver;

        [Inject]
        public void Construct(IObjectResolver objectResolver, PlayerStats playerStats, UpgradesManager upgradesManager)
        {
            _objectResolver = objectResolver;
            _playerStats = playerStats;
            _upgradesManager = upgradesManager;
        }

        private void Awake()
        {
            InitStats();
        }

        private void InitStats()
        {
            _upgrades = new Upgrade[_initInfo.Length];

            for (int i = 0; i < _initInfo.Length; i++)
            {
                var config = _initInfo[i].upgradeConfig;
                var stat = _initInfo[i].value;

                _playerStats.AddStat(config.id, stat);

                _upgrades[i] = config.InstantiateUpgrade(_objectResolver);
            }

            _upgradesManager.Setup(_upgrades);
        }
    }

    [Serializable]
    public struct PlayerStatsInit
    {
        public UpgradeConfig upgradeConfig;
        public int value;
    }
}