using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrade
{
    [CreateAssetMenu(
        fileName = "UpgradeConfig",
        menuName = "Configs/New Upgrade Config"
        )]

    public class UpgradeConfig : ScriptableObject
    {
        public string Id;

        public int MaxLevel;

        [SerializeField]
        private PriceTable _priceTable;

        private void OnValidate()
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

        public void CreateUpgrade()
        {
            //_priceTable.Init(MaxLevel);


        }
    }
}