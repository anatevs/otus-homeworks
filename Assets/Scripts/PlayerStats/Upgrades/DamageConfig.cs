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
        public int upgradeAmount;

        public override Upgrade InstantiateUpgrade(IObjectResolver objectResolver)
        {
            DamageUpgrade damageUpgrade = new DamageUpgrade(this);
            objectResolver.Inject(damageUpgrade);

            return damageUpgrade;
        }
    }
}