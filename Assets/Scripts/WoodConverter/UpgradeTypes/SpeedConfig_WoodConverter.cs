using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;


namespace Sample
{
    [CreateAssetMenu(
        fileName = "SpeedConfig",
        menuName = "Sample/Upgrade Configs/New SpeedConfig"
    )]
    public class SpeedConfig_WoodConverter : UpgradeConfig
    {
        public float upgradePersent;

        public override Upgrade InstantiateUpgrade(IObjectResolver objectResolver)
        {
            SpeedUpgrade_WoodConverter upgrade = new SpeedUpgrade_WoodConverter(this);
            objectResolver.Inject(upgrade);

            return upgrade;
        }
    }
}