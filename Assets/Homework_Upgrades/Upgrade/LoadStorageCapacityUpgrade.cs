using Game.GamePlay.Conveyor;
using Game.GamePlay.Conveyor.Components;
using UnityEngine;

namespace Upgrade
{
    public sealed class LoadStorageCapacityUpgrade : Upgrade
    {
        private readonly StorageCapacityConfig _config;
        private readonly ConveyorEntity _conveyor;

        public LoadStorageCapacityUpgrade(StorageCapacityConfig config, ConveyorEntity conveyor) : base(config)
        {
            _config = config;
            _conveyor = conveyor;
        }

        protected override void OnUpgrade(int level)
        {
            var capacity = _config.GetLevelCapacity(level);

            _conveyor.Get<Conveyor_SetLoadStorageComponent>()
                .SetLoadStorage(capacity);

            //spend money

            Debug.Log($"conv storage capacity has been upgraded to level {level} with capacity {capacity}");
        }
    }
}