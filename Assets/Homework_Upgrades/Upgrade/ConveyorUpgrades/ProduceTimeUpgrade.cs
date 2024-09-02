using Game.GamePlay.Conveyor.Components;
using Game.GamePlay.Conveyor;
using UnityEngine;
using VContainer;

namespace Upgrades
{
    public sealed class ProduceTimeUpgrade : Upgrade
    {
        private readonly ProduceTimeConfig _config;
        private ConveyorEntity _conveyor;

        public ProduceTimeUpgrade(ProduceTimeConfig config) : base(config)
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
            var value = _config.GetLevelCapacity(level);

            _conveyor.Get<Conveyor_SetProduceTimeComponent>()
                .SetProduceTime(value);

            Debug.Log($"produce time has been upgraded to level {level} with value {value}");
        }
    }
}