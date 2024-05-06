using UnityEngine;
using VContainer;


namespace Sample
{
    [CreateAssetMenu(
        fileName = "HealthConfig",
        menuName = "Sample/Player Upgrade Configs/New HealthConfig"
    )]

    public class HealthConfig : UpgradeConfig
    {
        public int upgradeAmount;
        public int upgradeBase;

        public override Upgrade InstantiateUpgrade(IObjectResolver objectResolver)
        {
            HealthUpgrade upgrade = new HealthUpgrade(this);
            objectResolver.Inject(upgrade);

            return upgrade;
        }
    }
}