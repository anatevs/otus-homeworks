using UnityEngine;
using VContainer;

namespace Sample
{
    [CreateAssetMenu(
        fileName = "DamageConfig",
        menuName = "Sample/Player Upgrade Configs/New DamageConfig"
    )]
    public sealed class DamageConfig : UpgradeConfig
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
    }
}