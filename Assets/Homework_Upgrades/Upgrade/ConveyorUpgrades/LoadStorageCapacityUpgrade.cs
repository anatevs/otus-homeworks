using Game.GamePlay.Conveyor;
using Game.GamePlay.Conveyor.Components;
using UnityEngine;
using VContainer;

namespace Upgrades
{
    public sealed class LoadStorageCapacityUpgrade : Upgrade
    {
        private readonly LoadStorageCapacityConfig _config;
        private ConveyorEntity _conveyor;

        public LoadStorageCapacityUpgrade(LoadStorageCapacityConfig config) : base(config)
        {
            _config = config;
        }

        [Inject]
        public void Construct(ConveyorEntity conveyor)
        {
            _conveyor = conveyor;
        }

        protected override void OnUpgrade(int level)
        {
            var capacity = _config.GetLevelCapacity(level);

            _conveyor.Get<Conveyor_SetLoadStorageComponent>()
                .SetLoadStorage(capacity);

            Debug.Log($"conv storage capacity has been upgraded to level {level} with capacity {capacity}");
        }
    }
}