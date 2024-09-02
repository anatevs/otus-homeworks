using LevelTables;
using UnityEngine;
using VContainer;

namespace Upgrades
{
    [CreateAssetMenu(
        fileName = "LoadStorageCapacityUpgradeConfig",
        menuName = "Configs/Conveyor/New LoadStorageCapacity Upgrade Config"
        )]

    public sealed class LoadStorageCapacityConfig : UpgradeConfig
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

        public override Upgrade CreateUpgrade(IObjectResolver objectResolver)
        {
            var upgrade = new LoadStorageCapacityUpgrade(this);
            objectResolver.Inject(upgrade);

            return upgrade;
        }
    }
}