using UnityEngine;
using VContainer;

namespace Upgrades
{
    public abstract class UpgradeConfig : ScriptableObject
    {
        public string Id;

        public int MaxLevel;

        [SerializeField]
        private PriceTable _priceTable;

        protected virtual void OnValidate()
        {
            _priceTable.Init(MaxLevel);
        }

        public int GetPrice(int level)
        {
            if (level <= MaxLevel)
            {
                return (int)_priceTable.GetPrice(level);
            }
            else
            {
                return 0;
            }
        }

        public abstract Upgrade CreateUpgrade(IObjectResolver objectResolver);
    }
}