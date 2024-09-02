using LevelTables;
using UnityEngine;
using VContainer;

namespace Upgrades
{
    [CreateAssetMenu(
        fileName = "ProduceTimeUpgradeConfig",
        menuName = "Configs/Conveyor/New ProduceTime Upgrade Config"
        )]

    public sealed class ProduceTimeConfig : UpgradeConfig
    {
        [SerializeField]
        private LinerInterpTable _upgradeTable;

        protected override void OnValidate()
        {
            base.OnValidate();

            _upgradeTable.Init(MaxLevel);
        }

        public float GetLevelCapacity(int level)
        {
            return _upgradeTable.GetValue(level);
        }

        public override Upgrade CreateUpgrade(IObjectResolver objectResolver)
        {
            var upgrade = new ProduceTimeUpgrade(this);
            objectResolver.Inject(upgrade);

            return upgrade;
        }
    }
}