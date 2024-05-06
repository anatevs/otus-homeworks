using Sample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Sample
{
    public class UnloadAreaUpgrade : Upgrade
    {
        private WoodConverter _converter;

        public UnloadAreaUpgrade(UpgradeConfig config) : base(config)
        {
        }

        [Inject]
        public void Construct(WoodConverter converter)
        {
            _converter = converter;
        }

        protected override void LevelUp(int level)
        {
            _converter.LoadArea *= level;
        }
    }
}