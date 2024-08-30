using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrade
{
    public abstract class Upgrade
    {
        public string Id => _config.Id;

        public int MaxLevel => _config.MaxLevel;

        public int Level => _level;

        public bool CanLevelUp => _level < _config.MaxLevel;

        public int NextPrice => _config.GetPrice(_level + 1);

        private int _level;

        private readonly UpgradeConfig _config;

        public Upgrade(UpgradeConfig config)
        {
            _config = config;

            _level = 1;
        }

        protected abstract void OnUpgrade(int level);

        public void LevelUp()
        {
            if (!CanLevelUp)
            {
                return;
            }

            _level++;
            OnUpgrade(_level);
        }
    }
}
