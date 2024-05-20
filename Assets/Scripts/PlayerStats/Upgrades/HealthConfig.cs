using UnityEngine;
using VContainer;

namespace Sample
{
    [CreateAssetMenu(
        fileName = "HealthConfig",
        menuName = "Sample/Player Upgrade Configs/New HealthConfig"
    )]

    public sealed class HealthConfig : UpgradeConfig
    {
        [field: SerializeField]
        public int UpgradeBase { get; private set; }

        public HealthConfig()
        {
            Id = PlayerStatType.HP;
        }

        public override Upgrade InstantiateUpgrade(IObjectResolver objectResolver)
        {
            HealthUpgrade upgrade = new HealthUpgrade(this);
            objectResolver.Inject(upgrade);

            return upgrade;
        }
    }
}