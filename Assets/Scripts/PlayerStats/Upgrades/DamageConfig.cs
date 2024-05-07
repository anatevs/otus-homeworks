using UnityEngine;
using VContainer;

namespace Sample
{
    [CreateAssetMenu(
        fileName = "DamageConfig",
        menuName = "Sample/Player Upgrade Configs/New DamageConfig"
    )]
    public class DamageConfig : UpgradeConfig
    {
        public int UpgradeAmount;

        public DamageConfig()
        {
            Id = PlayerStatType.Damage;
        }

        public override Upgrade InstantiateUpgrade(IObjectResolver objectResolver)
        {
            DamageUpgrade damageUpgrade = new DamageUpgrade(this);
            objectResolver.Inject(damageUpgrade);

            return damageUpgrade;
        }

        //public override bool CanLevelUpFromConstraints(int level, Upgrade[] constraintUpgrades)
        //{
        //    if (constraintUpgrades == null || constraintUpgrades.Length == 0)
        //    {
        //        Debug.Log($"no constraint upgrades in upgrade for {Id}");
        //        return true;
        //    }

        //    for (int i = 0; i < constraintUpgrades.Length; i++)
        //    {
        //        if (constraintUpgrades[i].Level <= level && !constraintUpgrades[i].IsMaxLevel)
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}
    }
}