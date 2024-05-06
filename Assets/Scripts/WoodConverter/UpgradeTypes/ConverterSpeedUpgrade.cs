using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sample
{
    public class ConverterSpeedUpgrade : Upgrade
    {
        private WoodConverter _converter;

        private readonly float _upSpeedCoef = 0.1f;

        public ConverterSpeedUpgrade(UpgradeConfig config) : base(config)
        {
        }

        //Inject...
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