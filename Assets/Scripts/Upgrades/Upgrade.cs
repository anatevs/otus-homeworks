using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    public abstract class Upgrade
    {
        public event Action<int> OnLevelUp;

        [ShowInInspector, ReadOnly]
        public PlayerStatType Id => this.config.id;

        [ShowInInspector, ReadOnly]
        public int Level => this.currentLevel;

        [ShowInInspector, ReadOnly]
        public int MaxLevel => this.config.maxLevel;

        public bool IsMaxLevel => this.currentLevel == this.config.maxLevel;

        [ShowInInspector, ReadOnly]
        public float Progress => (float)this.currentLevel / this.config.maxLevel;

        [ShowInInspector, ReadOnly]
        public int NextPrice => this.config.GetPrice(this.Level + 1);

        [ShowInInspector]
        public Upgrade[] ruleUpgrades;

        private readonly UpgradeConfig config;

        private int currentLevel;

        protected Upgrade(UpgradeConfig config)
        {
            this.config = config;
            this.currentLevel = 1;
        }

        public void SetupLevel(int level)
        {
            this.currentLevel = level;
        }

        public bool IsOthersRuleTrue()
        {
            return config.CanLevelUpRule(Level, ruleUpgrades);
        }

        [Button]
        public void LevelUp()
        {
            if (this.Level >= this.MaxLevel)
            {
                throw new Exception($"Level is reached Max for upgrade {this.config.id}!");
            }

            if (!IsOthersRuleTrue())
            {
                Debug.Log($"rule condition is not true for upgrade {config.id}");
                return;
            }

            var nextLevel = this.Level + 1;
            this.currentLevel = nextLevel;
            this.LevelUp(nextLevel);
            this.OnLevelUp?.Invoke(nextLevel);
        }

        protected abstract void LevelUp(int level);
    }
}