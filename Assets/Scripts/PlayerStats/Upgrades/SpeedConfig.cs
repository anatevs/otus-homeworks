using UnityEngine;
using VContainer;

namespace Sample
{
    [CreateAssetMenu(
        fileName = "SpeedConfig",
        menuName = "Sample/Player Upgrade Configs/New SpeedConfig"
    )]

    public sealed class SpeedConfig : UpgradeConfig
    {
        [field: SerializeField]
        public int UpgradeAmount { get; private set; }

        public SpeedConfig()
        {
            Id = PlayerStatType.Speed;
        }

        public override Upgrade InstantiateUpgrade(IObjectResolver objectResolver)
        {
            SpeedUpgrade upgrade = new SpeedUpgrade(this);
            objectResolver.Inject(upgrade);

            return upgrade;
        }
    }
}