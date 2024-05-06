using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Sample
{
    [CreateAssetMenu(
        fileName = "LoadAreaConfig",
        menuName = "Sample/Upgrade Configs/New LoadAreaConfig"
        )]

    public class LoadAreaConfig : UpgradeConfig
    {
        public override Upgrade InstantiateUpgrade(IObjectResolver objectResolver)
        {
            LoadAreaUpgrade upgrade = new LoadAreaUpgrade(this);
            objectResolver.Inject(upgrade);

            return upgrade;
        }

        public override bool CanLevelUpRule(int level, Upgrade[] ruleUpgrades)
        {
            if (ruleUpgrades == null || ruleUpgrades.Length == 0)
            {
                Debug.Log($"no rule upgrades in config {this}");
                return true;
            }

            for (int i = 0; i < ruleUpgrades.Length; i++)
            {
                if (ruleUpgrades[i].Level <= level && !ruleUpgrades[i].IsMaxLevel)
                {
                    return false;
                }
            }

            return true;
        }
    }
}