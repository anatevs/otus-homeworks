using Game.GamePlay.Conveyor;
using Game.GamePlay.Conveyor.Components;
using System.Collections;
using UnityEngine;

namespace Upgrade
{
    public class LoadStorageCapacityUpgrade : Upgrade
    {
        private readonly LoadStorageCapacityConfig _config;
        private readonly ConveyorEntity _conveyor;

        public LoadStorageCapacityUpgrade(LoadStorageCapacityConfig config, ConveyorEntity conveyor) : base(config)
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