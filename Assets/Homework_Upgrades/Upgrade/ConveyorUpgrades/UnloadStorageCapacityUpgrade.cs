using UnityEngine;
using VContainer;

namespace Upgrades
{
    public sealed class UnloadStorageCapacityUpgrade : Upgrade
    {
        private readonly UnloadStorageCapacityConfig _config;
        private ConveyorModel _conveyorModel;

        public UnloadStorageCapacityUpgrade(UnloadStorageCapacityConfig config) : base(config)
        {
            _config = config;
        }

        [Inject]
        public void Construct(ConveyorModel conveyorModel)
        {
            _conveyorModel = conveyorModel;
        }

        protected override void OnUpgrade(int level)
        {
            var capacity = _config.GetLevelCapacity(level);

            _conveyorModel.UnloadStorageCapacity.Value = capacity;

            Debug.Log($"unload storage capacity has been upgraded to level {level} with capacity {capacity}");
        }
    }
}