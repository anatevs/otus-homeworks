using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sample
{
    [CreateAssetMenu(
        fileName = "SpeedConfig",
        menuName = "Sample/Upgrade Configs/New SpeedConfig"
    )]
    public class ConverterSpeedConfig : UpgradeConfig
    {
        public override Upgrade InstantiateUpgrade()
        {
            throw new System.NotImplementedException();
        }
    }
}