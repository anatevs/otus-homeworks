using UnityEngine;
using VContainer;

namespace Upgrades
{
    public sealed class LoadStorageCapacityUpgrade : Upgrade
    {
        private readonly LoadStorageCapacityConfig _config;
        private ConveyorModel _conveyorModel;

        public LoadStorageCapacityUpgrade(LoadStorageCapacityConfig config) : base(config)
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

            _conveyorModel.LoadStorageCapacity.Value = capacity;

            Debug.Log($"conv storage capacity has been upgraded to level {level} with capacity {capacity}");
        }
    }
}