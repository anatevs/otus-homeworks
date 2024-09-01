using Game.GamePlay.Conveyor.Components;
using Game.GamePlay.Conveyor;
using UnityEngine;

namespace Upgrade
{
    public sealed class UnloadStorageCapacityUpgrade : Upgrade
    {
        private readonly StorageCapacityConfig _config;
        private readonly ConveyorEntity _conveyor;

        public UnloadStorageCapacityUpgrade(StorageCapacityConfig config, ConveyorEntity conveyor) : base(config)
        {
            _config = config;
            _conveyor = conveyor;
        }

        protected override void OnUpgrade(int level)
        {
            var capacity = _config.GetLevelCapacity(level);

            _conveyor.Get<Conveyor_SetUnloadStorageComponent>()
                .SetUnloadStorage(capacity);

            //spend money

            Debug.Log($"unload storage capacity has been upgraded to level {level} with capacity {capacity}");
        }
    }
}