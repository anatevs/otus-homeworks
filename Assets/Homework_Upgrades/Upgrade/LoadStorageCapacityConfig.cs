using LevelTables;
using UnityEngine;

namespace Upgrade
{
    [CreateAssetMenu(
        fileName = "StorageCapacityUpgradeConfig",
        menuName = "Configs/New StorageCapacity Upgrade Config"
        )]

    public class LoadStorageCapacityConfig : UpgradeConfig
    {
        [SerializeField]
        private LinerInterpTable _upgradeTable;

        private void OnValidate()
        {
            _upgradeTable.Init(MaxLevel);
        }

        public int GetLevelCapacity(int level)
        {
            return _upgradeTable.GetValueInt(level);
        }
    }
}