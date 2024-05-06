using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Sample
{
    public class SpeedUpgrade : Upgrade
    {
        private WoodConverter _converter;

        private readonly float _upSpeedCoef;

        public SpeedUpgrade(SpeedConfig config) : base(config)
        {
            _upSpeedCoef = config.upgradePersent / 100;
        }

        [Inject]
        public void Construct(WoodConverter converter)
        {
            _converter = converter;
        }

        protected override void LevelUp(int level)
        {
            
            _converter.Speed += _converter.Speed * _upSpeedCoef;
        }
    }
}