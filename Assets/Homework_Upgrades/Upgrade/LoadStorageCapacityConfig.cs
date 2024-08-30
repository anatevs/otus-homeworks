using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrade
{
    [CreateAssetMenu(
        fileName = "StorageCapacityUpgradeConfig",
        menuName = "Configs/New StorageCapacity Upgrade Config"
        )]
    public class LoadStorageCapacityConfig : UpgradeConfig
    {
        [ShowInInspector]
        private Dictionary<int, int> _levelsCapacity = new();

        public int GetLevelCapacity(int level)
        {
            return _levelsCapacity[level];
        }
    }
}