using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Sample
{
    public sealed class PlayerStatsTest : MonoBehaviour
    {
        [SerializeField]
        private PlayerStatsInit[] _initInfo;

        [ShowInInspector]
        private PlayerStats _playerStats;

        [SerializeReference]
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
                var stat = _initInfo[i].statValue;

                _playerStats.AddStat(config.Id, stat);

                _upgrades[i] = config.InstantiateUpgrade(_objectResolver);
            }

            _upgradesManager.Setup(_upgrades);

            for (int i = 0; i < _initInfo.Length; i++)
            {
                var config = _initInfo[i].upgradeConfig;

                UpgradeConfig[] ruleConfigs = config.ConstraintConfigs;
                if (ruleConfigs.Length != 0)
                {
                    List<Upgrade> upgrades = new List<Upgrade>();

                    for (int j = 0;  j < ruleConfigs.Length; j++)
                    {
                        if (_upgradesManager.TryGetUpgrade(ruleConfigs[j].Id, out var upgrade))
                        {
                            upgrades.Add(upgrade);
                        }
                    }
                    _upgrades[i].ConstraintUpgrades = upgrades.ToArray();
                }
            }
        }
    }

    [Serializable]
    public struct PlayerStatsInit
    {
        public UpgradeConfig upgradeConfig;
        public int statValue;
    }
}