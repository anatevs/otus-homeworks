using LevelTables;
using UnityEngine;

namespace Upgrade
{
    [CreateAssetMenu(
        fileName = "StorageCapacityUpgradeConfig",
        menuName = "Configs/New StorageCapacity Upgrade Config"
        )]

    public sealed class StorageCapacityConfig : UpgradeConfig
    {
        [SerializeField]
        private LinerInterpTable _upgradeTable;


        protected override void OnValidate()
        {
            base.OnValidate();

            _upgradeTable.Init(MaxLevel);
        }

        public int GetLevelCapacity(int level)
        {
            return _upgradeTable.GetValueInt(level);
        }
    }
}