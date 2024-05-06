using UnityEngine;
using VContainer;

namespace Sample
{
    [CreateAssetMenu(
        fileName = "SpeedConfig",
        menuName = "Sample/Player Upgrade Configs/New SpeedConfig"
    )]

    public class SpeedConfig : UpgradeConfig
    {
        public int upgradeAmount;

        public override Upgrade InstantiateUpgrade(IObjectResolver objectResolver)
        {
            SpeedUpgrade upgrade = new SpeedUpgrade(this);
            objectResolver.Inject(upgrade);

            return upgrade;
        }
    }
}