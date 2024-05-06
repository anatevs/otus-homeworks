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
    public class SpeedConfig : UpgradeConfig
    {
        public float upgradePersent;

        public override Upgrade InstantiateUpgrade(IObjectResolver objectResolver)
        {
            SpeedUpgrade upgrade = new SpeedUpgrade(this);
            objectResolver.Inject(upgrade);

            return upgrade;
        }
    }
}