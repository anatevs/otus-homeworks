using UnityEngine;
using VContainer;

namespace Upgrades
{
    public sealed class ProduceTimeUpgrade : Upgrade
    {
        private readonly ProduceTimeConfig _config;
        private ConveyorModel _conveyorModel;

        public ProduceTimeUpgrade(ProduceTimeConfig config) : base(config)
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
            var value = _config.GetLevelCapacity(level);

            _conveyorModel.ProduceTime.Value = value;

            Debug.Log($"produce time has been upgraded to level {level} with value {value}");
        }
    }
}