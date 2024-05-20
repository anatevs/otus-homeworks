using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Sample
{
    public abstract class UpgradeConfig : ScriptableObject
    {
        protected const float SPACE_HEIGHT = 10.0f;

        [ShowInInspector, ReadOnly]
        public PlayerStatType Id;

        [ShowInInspector]
        public UpgradeConfig[] ConstraintConfigs;

        [Range(2, 99)]
        [SerializeField]
        public int MaxLevel = 2;

        [Space(SPACE_HEIGHT)]
        [SerializeField]
        private PriceTable priceTable;

        public abstract Upgrade InstantiateUpgrade(IObjectResolver objectResolver);

        private void OnValidate()
        {
            try
            {
                this.Validate();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        protected virtual void Validate()
        {
            this.priceTable.OnValidate(this.MaxLevel);
        }

        public bool CanLevelUpFromConstraints(int level, Upgrade[] constraintUpgrades)
        {
            if (ConstraintConfigs == null || ConstraintConfigs.Length == 0)
            {
                return true;
            }

            if (constraintUpgrades == null || constraintUpgrades.Length == 0)
            {
                Debug.Log($"there must be constraint upgrades in upgrade {Id}");
                return true;
            }

            for (int i = 0; i < constraintUpgrades.Length; i++)
            {
                if (constraintUpgrades[i].Level <= level && !constraintUpgrades[i].IsMaxLevel)
                {
                    return false;
                }
            }

            return true;
        }

        public int GetPrice(int level)
        {
            return this.priceTable.GetPrice(level);
        }

        [Serializable]
        public sealed class PriceTable
        {
            [Space]
            [SerializeField]
            private int basePrice;

            [Space]
            [ListDrawerSettings(OnBeginListElementGUI = "DrawLevels")]
            [SerializeField]
            private int[] levels;

            public int GetPrice(int level)
            {
                var index = level - 1;
                index = Mathf.Clamp(index, 0, this.levels.Length - 1);
                return this.levels[index];
            }

            private void DrawLevels(int index)
            {
                GUILayout.Space(8);
                GUILayout.Label($"Level #{index + 1}");
            }

            public void OnValidate(int maxLevel)
            {
                this.EvaluatePriceTable(maxLevel);
            }

            private void EvaluatePriceTable(int maxLevel)
            {
                var table = new int[maxLevel];
                table[0] = new int();
                for (var level = 2; level <= maxLevel; level++)
                {
                    var price = this.basePrice * level;
                    table[level - 1] = price;
                }

                this.levels = table;
            }
        }
    }
}