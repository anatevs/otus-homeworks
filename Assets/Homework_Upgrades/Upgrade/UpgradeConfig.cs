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

        [ShowInInspector]
        private Dictionary<int, int> _priceTable = new();

        public int GetPrice(int level)
        {
            if (level <= MaxLevel)
            {
                return _priceTable[level];
            }
            else
            {
                return 0;
            }
        }

        public void CreateUpgrade()
        {

        }
    }
}