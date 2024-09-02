using LevelTables;
using UnityEngine;
using VContainer;

namespace Upgrades
{
    [CreateAssetMenu(
        fileName = "UnloadStorageCapacityUpgradeConfig",
        menuName = "Configs/Conveyor/New UnloadStorageCapacity Upgrade Config"
        )]

    public sealed class UnloadStorageCapacityConfig : UpgradeConfig
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
            var upgrade = new UnloadStorageCapacityUpgrade(this);
            objectResolver.Inject(upgrade);

            return upgrade;
        }
    }
}