using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Sample
{
    public class TestUpgrade : MonoBehaviour
    {
        [SerializeField]
        private UpgradeConfig _config;

        [ShowInInspector]
        [SerializeReference]
        private Upgrade _testUpgrade;

        //[ShowInInspector]
        //[SerializeReference]
        private Upgrade[] _ruleUpgrades;

        private IObjectResolver _objectResolver;

        [Inject]
        public void Constuct(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        private void Awake()
        {
            _testUpgrade = _config.InstantiateUpgrade(_objectResolver);

            if (_config.ruleUpgradeConfigs != null && _config.ruleUpgradeConfigs.Length != 0 )
            {
                _ruleUpgrades = new Upgrade[_config.ruleUpgradeConfigs.Length];

                for (int i = 0; i < _ruleUpgrades.Length; i++)
                {
                    var config = _config.ruleUpgradeConfigs[i];
                    var upgrade = config.InstantiateUpgrade(_objectResolver);

                    _ruleUpgrades[i] = upgrade;
                }
            }

            _testUpgrade.ruleUpgrades = _ruleUpgrades;
        }
    }
}