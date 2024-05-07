using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    public abstract class Upgrade
    {
        public event Action<int> OnLevelUp;

        [ShowInInspector, ReadOnly]
        public PlayerStatType Id => _config.Id;

        [ShowInInspector, ReadOnly]
        public int Level => _currentLevel;

        [ShowInInspector, ReadOnly]
        public int MaxLevel => _config.MaxLevel;

        public bool IsMaxLevel => _currentLevel == _config.MaxLevel;

        [ShowInInspector, ReadOnly]
        public float Progress => (float)_currentLevel / _config.MaxLevel;

        [ShowInInspector, ReadOnly]
        public int NextPrice => _config.GetPrice(Level + 1);

        [ShowInInspector]
        public Upgrade[] ConstraintUpgrades;

        private readonly UpgradeConfig _config;

        private int _currentLevel;

        protected Upgrade(UpgradeConfig initConfig)
        {
            _config = initConfig;
            _currentLevel = 1;
        }

        public void SetupLevel(int level)
        {
            this._currentLevel = level;
        }

        public bool IsNoConstraints()
        {
            return _config.CanLevelUpFromConstraints(Level, ConstraintUpgrades);
        }

        [Button]
        public void LevelUp()
        {
            if (Level >= MaxLevel)
            {
                throw new Exception($"Level is reached Max for upgrade {_config.Id}!");
            }

            if (!IsNoConstraints())
            {
                Debug.Log($"constraints condition is not true for upgrade {_config.Id}");
                return;
            }

            var nextLevel = Level + 1;
            _currentLevel = nextLevel;
            LevelUp(nextLevel);
            OnLevelUp?.Invoke(nextLevel);
        }

        protected abstract void LevelUp(int level);
    }
}