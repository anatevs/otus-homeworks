using Game.GamePlay.Conveyor.Components;
using Game.GamePlay.Conveyor;
using UnityEngine;
using VContainer;

namespace Upgrades
{
    public sealed class UnloadStorageCapacityUpgrade : Upgrade
    {
        private readonly UnloadStorageCapacityConfig _config;
        private ConveyorEntity _conveyor;

        public UnloadStorageCapacityUpgrade(UnloadStorageCapacityConfig config) : base(config)
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

            _conveyor.Get<Conveyor_SetUnloadStorageComponent>()
                .SetUnloadStorage(capacity);

            Debug.Log($"unload storage capacity has been upgraded to level {level} with capacity {capacity}");
        }
    }
}